<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_Master.aspx.vb" Inherits="Product_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Product Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Product_List.aspx">Product List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Product Master</li>
        </ol>
    </div>

    <script src="js2/demo/datatables-demo.js"></script>

    <script type="text/javascript" lang="javascript">

        function IsNumberWithOneDecimal(txt, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8)) {
                return false;
            } else {
                var len = txt.value.length;
                var index = txt.value.indexOf('.');
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    if ((len + 1) - index > 3) {
                        return false;
                    }
                }

            }
            return true;
        }

        function OnClientFileSize() {
           /* alert('Image size is more than 100 MB. Compress the image file size and try again.');*/

        }

        function OnClientFileSelectedHandler(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] ) {
                    var fileSize = input.files[0].size;
                    var minSize =0; // 10 KB
                    var maxSize = 100 * 1024 * 1024; // 100 MB


                    console.log("File size (bytes):", fileSize);  
                    console.log("Min size (bytes):", minSize);    
                    console.log("Max size (bytes):", maxSize);


                    if (fileSize < minSize || fileSize > maxSize) {
                        console.log('File size:', fileSize)
                       /* alert('File size should be between 10 MB and 100 MB. Please select a valid file.');*/
                        eventArgs.set_cancel(true);
                        return;
                    }

                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=Image1.ClientID%>').prop('src', e.target.result)
                            .width(150)
                            .height(50);
                    };
                 /*   alert(input.files[0]);*/
                    reader.readAsDataURL(input.files[0]);
                }
            } else {
                /*alert('Invalid file type. Please select a valid image file.');*/
                eventArgs.set_cancel(true);
            }
        }
        function OnTextKeyPress(objEvent) {
            var $th = objEvent.value;
            $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {

                return '';
            }));
        }

        function openWindow(url) {
            var w = window.open(url, '', 'width=1000,height=700,top=80,left=300,toolbar=0,status=0,location=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Gridbusiness();

        });

        function Gridbusiness() {
            
            $("#Psummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var tableexp = $('#Psummary').DataTable(); {
                tableexp.column(0).visible(false);
            }

            var table = $('#Psummary').DataTable({

                "bPaginate": false,
                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel',
                    {
                        extend: 'pdfHtml5',
                        title: "Business Done report",
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                        }
                    }

                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    //{
                    //    "searchable": false, "targets": 1,
                    //    "visible": false
                    //    , "targets": +groupCol,
                    //"render": function (data, type, full, meta) {
                    //    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    //}
                    { "visible": true, "targets": +groupCol },
                    {
                        //'targets': [1], /* column index */
                        'orderable': false,
                    },

                ],
                "order": [[groupCol, 'asc']],
                "displayLength": 50,
                "drawCallback": function (settings) {

                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;


                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {

                        if (last !== group) {
                            $(rows).eq(i).before(
                                //'<tr class="group"><td colspan="8"> Location Group : ' + group + '</td> </tr>'
                            );
                            last = group;
                        }
                    });
                }
            });
        }


        function selectTab(txt) {

            var nodeId = txt.value;
            alert(nodeId);

        }

    </script>

    <style type="text/css">
        .DynTabCSS {
            height: 27px;
        }

        .rlbItem {
            float: left !important;
        }

        .rlbGroup, .RadListBox {
            width: auto !important;
        }

        /*#RadListBox1 {
            border: 1px solid black;
        }*/

        .RadListBox_Silk .rlbGroup {
            border: none !important;
        }

        .lbl {
            color: #777777;
            font-size: 13px;
            line-height: 1.42857;
            font-weight: normal;
            font-family: "Open Sans",sans-serif;
        }

        .rgHeader.rgCheck > input {
            display: none;
        }

        .RadAutoCompleteBoxPopup {
            width: auto !important;
            overflow-y: scroll;
            max-height: 262px;
        }

        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: Gray;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #363670 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btn_ProductIngredient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdcopyProduct" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgUpload">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imgUpload" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btn_ProductCondiment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadCondiment" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

</asp:Content>

 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Product Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>
                                            Name <span class="text-danger">*</span>
                                            <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtPName"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Name is required">
                                            </asp:RequiredFieldValidator></label><div class="clearfix"></div>
                                        <%--<telerik:RadTextBox ID="txtPName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%" onkeyup="OnTextKeyPress(this)"></telerik:RadTextBox>--%>
                                        <asp:TextBox runat="server" ID="txtPName" CssClass="form-control" placeholder="Name" Width="100%" onkeyup="OnTextKeyPress(this)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>
                                            Product Group <span class="text-danger">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radCategory"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT" ErrorMessage="Product group is required">
                                            </asp:RequiredFieldValidator>

                                        </label>
                                        <div class="clearfix"></div>
                                        <%-- <telerik:RadComboBox ID="radCategory" runat="server" Width="100%">
                                        </telerik:RadComboBox>--%>
                                        <asp:DropDownList ID="radCategory" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Base Size <span class="text-danger">*</span>
                                        </label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox runat="server" ID="txtBase" MinValue="0" MaxLength="9" Width="100%" CssClass="form-control" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>
                                        <%--  <telerik:RadNumericTextBox MinValue="0" MaxValue="999999999" ID="txtBase" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" MaxLength="8">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>--%>
                                        <asp:RequiredFieldValidator ID="RqBase" runat="server" ControlToValidate="txtBase"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Base is requried">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="col-md-6">
                                        <label>Base Unit<span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <%--<telerik:RadComboBox ID="radUnit" runat="server" Width="100%" OnSelectedIndexChanged="radUnit_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>--%>
                                        <asp:DropDownList ID="radUnit" runat="server" Width="100%" OnSelectedIndexChanged="radUnit_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="Rqunit" runat="server" ControlToValidate="radUnit"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Unit is requried" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12" style="padding-bottom: 10px;">
                                        <label>Description</label><div class="clearfix"></div>
                                        <%--<telerik:RadTextBox ID="txtdescription" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Description" Rows="4" Width="100%"></telerik:RadTextBox>--%>
                                        <asp:TextBox runat="server" ID="txtdescription" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Description" Width="100%"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6">
                                       <%-- <label id="lbldep" visible="false">Department </label>--%> 
                                        <asp:Label ID="lbldep"  runat="server" Text="Department"></asp:Label>
                                      <%--  <asp:Label ID="lbldeplite" visible="false" runat="server" Text="Group Categories Departments"></asp:Label>--%>
                                    
                                        <div class="clearfix"></div>
                                        <%-- <telerik:RadComboBox ID="radDept" runat="server" Width="100%">
                                        </telerik:RadComboBox>--%>
                                        <asp:DropDownList ID="radDept" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                  
                                    <div class="col-md-6" visible="false">
                                        <asp:Label ID="Course" runat="server" Text="Course"></asp:Label>
                                        <%--<label  visible="false">Course </label>--%>
                                        <div class="clearfix"></div>
                                        <%-- <telerik:RadComboBox ID="radCourse" runat="server" Width="100%">
                                        </telerik:RadComboBox>--%>
                                        <asp:DropDownList ID="radCourse" runat="server" Width="100%" Visible="false">
                                        </asp:DropDownList>
                                    </div>
                                       
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6" visible="false" style="margin-top:20px;">
                                        <asp:Label ID="printer" runat="server" Text="Printer" Visible="false"></asp:Label>
                                       <%-- <label visible="false">Printer</label>--%>
                                        <div class="clearfix"></div>

                                        <telerik:RadComboBox ID="radPrinter" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                            Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" AutoPostBack="true"  visible="false">
                                        </telerik:RadComboBox>
                                    </div>
                                  


                                      <div class="clearfix"></div>

                                </div>
                                  <div class="clearfix"></div>
                                     


                            </div>


                            <div class="col-md-6" style="margin-top:16px;">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <label class="lbl">Upload :&nbsp;</label>
                                                        <telerik:RadAsyncUpload ID="fileupload" runat="server" MaxFileSize="1000000" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif"
                                                            OnClientFileSelected="OnClientFileSelectedHandler" OnClientValidationFailed="OnClientFileSize">
                                                            <FileFilters>
                                                                <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                            </FileFilters>
                                                        </telerik:RadAsyncUpload>
                                                        <i style="font-weight: lighter">
                                                            <asp:Label CssClass="lbl" ID="Label2" runat="server" Text=""></asp:Label>
                                                            <asp:Label CssClass="lbl" ID="Label1" runat="server" Text=""></asp:Label>
                                                        </i>
                                                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />
                                                        <div class="clearfix"></div>
                                                        <asp:HiddenField ID="hdlogo" runat="server" />
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <br />
                                                    <div class="col-md-12">
                                                        <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="150px" Style="display: none" />
                                                        <telerik:RadGrid runat="server" ID="RadGrid1"
                                                            AutoGenerateColumns="False" Width="97%">
                                                            <MasterTableView DataKeyNames="Id">
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn>
                                                                        <ItemTemplate>
                                                                            <telerik:RadBinaryImage runat="server" ID="RadBinaryImage2" DataValue='<%#Eval("image_name")%>'
                                                                                AutoAdjustImageControlSize="false" Height="80px" Width="80px" ToolTip='<%#Eval("image_name", "Photo of {0}")%>'
                                                                                AlternateText='<%#Eval("image_name", "Photo of {0}")%>'></telerik:RadBinaryImage>
                                                                        </ItemTemplate>

                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdimage_Id" runat="server" Value='<%#Eval("id")%>' />
                                                                            <asp:LinkButton ID="IbADelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                                CommandName="DeleteVal" CommandArgument='<%#Eval("id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
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
                    <telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="RadPanelBar1" Width="100%" ExpandMode="MultipleExpandedItems">
                        <Items>

                            <telerik:RadPanelItem Expanded="true" Text="Selling Sizes and Prices">
                                <ContentTemplate>
                                    <div class="form-body pal">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group" >
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="validSize"
                                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                                    <div class="col-md-4"  >
                                                        <asp:Label ID="location"  class="lbl" runat="server" Text="Label">Location  <span class="text-danger">*</span></asp:Label>
                                                     <%--   <label class="lbl" visible="false">Location <span class="text-danger">*</span></label>--%>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddLocation"
                                                            ValidationGroup="validSize" Display="none" CssClass="text-danger" ErrorMessage="Location is required" InitialValue="SELECT" Visible="false">
                                                        </asp:RequiredFieldValidator>
                                                        <div class="clearfix"></div>
                                                        <%-- <telerik:RadComboBox ID="ddLocation" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddLocation_SelectedIndexChanged">
                                                        </telerik:RadComboBox>--%>
                                                        <asp:DropDownList ID="ddLocation" runat="server" Width="100%" visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddLocation_SelectedIndexChanged1">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-4" >
                                                       
                                                         <asp:Label ID="lblprice" runat="server" Text="Prices" ></asp:Label>
                                                       <%-- <label class="lbl">Prices </label>--%>
                                                        <div class="clearfix"></div>

                                                        <%--<telerik:RadComboBox ID="ddPrices" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddPrices_SelectedIndexChanged"></telerik:RadComboBox>--%>
                                                        <asp:DropDownList ID="ddPrices" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddPrices_SelectedIndexChanged1">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">&nbsp;</label>
                                                        <div class="clearfix"></div>
                                                        <asp:Button ID="btnAddNewSize" class="btn btn-primary"  runat="server" ValidationGroup="validSize" Text="Add New Size & Price" OnClick="btnAddNewSize_Click" ToolTip="Click here to Add New Size & Price" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-md-6" style="display: none;">

                                                <div class="col-md-4">
                                                    <label class="lbl">&nbsp;</label>
                                                    <div class="clearfix"></div>
                                                    <asp:Button ID="btn_copy" class="btn btn-primary" runat="server" OnClick="btn_copy_Click" ValidationGroup="validSize" Text="Price Level Copy To" ToolTip="Click here to copy level" />
                                                </div>
                                            </div>
                                           <%-- <div class="clearfix"></div>--%>
                                            <div class="col-md-3">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                     
                                                      <asp:Label ID="lblcost" runat="server" Text="Cost :" Visible="false"></asp:Label>
                                                        <%--<label id="lblcost" class="lbl">Cost : </label>--%>
                                                        <div class="clearfix"></div>
                                                        <asp:Label ID="lblLocCOst" runat="server" Text="0.00"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3" >
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        
                                                        <asp:Label ID="lblque" runat="server" Text="Quantity on Hand :" ></asp:Label>
                                                      <%--  <label class="lbl">Quantity on Hand : </label>--%>
                                                        <div class="clearfix"></div>
                                                        <asp:Label ID="lblLocQty" runat="server" Text="0" ></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <br />
                                            <div class="col-md-12">
                                                <asp:PlaceHolder ID="PC1" runat="server" Visible="false"></asp:PlaceHolder>
                                            </div>

                                            <%--<telerik:RadTabStrip Visible="false" RenderMode="Lightweight" ID="RadTabStrip1" runat="server" onclick="selectTab(this)">
                                                    <telerik:RadTabStrip RenderMode="Lightweight" ID="RadTabStrip1" runat="server" OnTabClick="RadTabStrip1_TabClick">
                                                </telerik:RadTabStrip>                                                --%>
                                            <br />
                                            <br />
                                            <div class="clearfix"></div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-12" style="overflow-y: auto; overflow-x: auto; width: 100%;">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" CssClass="Grid" OnRowCommand="GridView1_RowCommand"
                                                            OnPageIndexChanging="OnPageIndexChanging" OnRowDataBound="GridView1_RowDataBound" PageSize="10" EmptyDataText="No data in the data source." ShowHeaderWhenEmpty="True">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Location">
                                                                    <HeaderStyle Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdFLoc" runat="server" Value='<%#Eval("Location_id")%>' />
                                                                        <%--<telerik:RadComboBox ID="ddGvLocation" runat="server" placeholder="Location" Width="100%" Enabled="false">
                                                                        </telerik:RadComboBox>--%>
                                                                        <asp:DropDownList ID="ddGvLocation" runat="server" placeholder="Location" Width="100%" Enabled="false">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtSizeName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                                                            MaxLength="15" Text='<%# Eval("Name") %>' onkeyup="OnTextKeyPress(this)"
                                                                            OnTextChanged="txtSizeName_TextChanged" AutoPostBack="true">  <%--onchange = "Confirm()"--%>
                                                                        </asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Size *">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%--Value='<%#Eval("Size")%>'--%>
                                                                        <asp:HiddenField ID="hdnSize_Id" runat="server" Value='<%#Eval("Size_Id")%>' />
                                                                        <asp:TextBox ID="txtSize" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                                                            MaxLength="15" Text='<%# Eval("Size") %>' onkeyup="OnTextKeyPress(this)" AutoPostBack="true" OnTextChanged="txtsize_TextChanged">
                                                                        </asp:TextBox>
                                                                        <%--   <asp:TextBox MinValue="0" MaxValue="999999999" ID="txtsize" Skin="" CssClass="form-control"
                                                                            runat="server" Width="100%" placeholder="Size" MaxLength="8" DbValue='<%#Eval("Size")%>'>                                                                            
                                                                        </asp:TextBox>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty *">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%--Value='<%# Eval("Unit") %>'--%>
                                                                        <asp:HiddenField ID="hdnUnit_Id" runat="server" Value='<%#Eval("Unit_id")%>' />
                                                                        <%--<telerik:RadComboBox ID="ddUnit" runat="server" Width="100%" OnSelectedIndexChanged="ddUnit_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>--%>
                                                                        <asp:DropDownList ID="ddUnit" runat="server" Width="100%" OnSelectedIndexChanged="ddUnit_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Price">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnPrice_Id" runat="server" Value='<%#Eval("Price_Id")%>' />
                                                                        <asp:TextBox ID="txtPriceLocation" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                                                            MaxLength="15" Text='<%# Eval("Price") %>' OnTextChanged="txtPriceLocation_TextChanged" AutoPostBack="true">
                                                                        </asp:TextBox>
                                                                        <%--<asp:TextBox MinValue="0" MaxValue="999999999" ID="txtPriceLocation" Skin="" CssClass="form-control"
                                                                            runat="server" placeholder="Price" MaxLength="8" Width="100%" DbValue='<%#Eval("Price")%>'>
                                                                            runat="server" placeholder="Price" MaxLength="8" Width="100%" DbValue='<%#Eval("Price")%>'>
                                                                            <numberformat groupseparator="" decimaldigits="2" keeptrailingzerosonfocus="true" />
                                                                            <incrementsettings interceptmousewheel="false" />
                                                                        </asp:TextBox>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tax">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%--Value='<%# Eval("Unit") %>'--%>
                                                                        <asp:HiddenField ID="hdnTax_Id" runat="server" Value='<%#Eval("Tax_id")%>' />
                                                                        <%--<telerik:RadComboBox ID="ddTaxx" runat="server" Width="100%" OnSelectedIndexChanged="ddTax_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>--%>
                                                                        <asp:DropDownList ID="ddTaxx" runat="server" Width="100%" OnSelectedIndexChanged="ddTaxx_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Tax 2">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnTax_Id2" runat="server" Value='<%#Eval("Tax_id2")%>' />
                                                                        <asp:DropDownList ID="ddTaxx_2" runat="server" Width="100%" OnSelectedIndexChanged="ddTaxx_2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Sorting No.">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtSortingNum" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                                                            MaxLength="15" Text='<%# Eval("sorting_no") %>' OnTextChanged="txtSortingNum_TextChanged" AutoPostBack="true">
                                                                        </asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Click and Collect">
                                                                    <HeaderStyle Width="7%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnIsclickcollect" runat="server" Value='<%#Eval("click_and_collect")%>' />
                                                                        <asp:CheckBox ID="chkIsClickcollect" runat="server" Width="100%" AutoPostBack="true" OnCheckedChanged="chkIsClickcollect_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Deliver">
                                                                    <HeaderStyle Width="7%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnIsDeliver" runat="server" Value='<%#Eval("deliver")%>' />
                                                                        <asp:CheckBox ID="chkIsDeliver" runat="server" Width="100%" AutoPostBack="true" OnCheckedChanged="chkIsDeliver_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Order at table">
                                                                    <HeaderStyle Width="7%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnIsorderattable" runat="server" Value='<%#Eval("Order_at_table")%>' />
                                                                        <asp:CheckBox ID="chkIsOrderattable" runat="server" Width="100%" AutoPostBack="true" OnCheckedChanged="chkIsOrderattable_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Is Active">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnIsactive" runat="server" Value='<%#Eval("active")%>' />
                                                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" OnCheckedChanged="chkIsActive_CheckedChanged" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <HeaderStyle Width="5%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                            CommandName="DeleteVal" CommandArgument='<%#Eval("row_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div align="center">No records found.</div>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>

                                        </div>
                                    </div>
                                </ContentTemplate>

                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem>
                                <ItemTemplate>
                                    <br />
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        
                            <telerik:RadPanelItem id ="buylbl"  Expanded="false" Text="Buying Sizes and Cost" visible="false" >
                                <ContentTemplate >
                                    <div class="form-body pal">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="validSize"
                                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                                    <div class="col-md-8">
                                                        <asp:Label ID="Label3" runat="server" Text="Label">Location <span class="text-danger">*</span></asp:Label>
                                                      <%--  <label class="lbl">Location <span class="text-danger">*</span></label>--%>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlocation2"
                                                            ValidationGroup="validSize" Display="none" CssClass="text-danger" ErrorMessage="Location is required" InitialValue="SELECT">
                                                        </asp:RequiredFieldValidator>
                                                        <div class="clearfix"></div>
                                                        <%--<telerik:RadComboBox ID="ddlocation2" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddLocation_SelectedIndexChanged"></telerik:RadComboBox>--%>
                                                        <asp:DropDownList ID="ddlocation2" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddLocation_SelectedIndexChanged1"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">&nbsp;</label>
                                                        <div class="clearfix"></div>
                                                        <asp:Button ID="btnNewSizeCost" class="btn btn-primary" runat="server" ValidationGroup="validSize" Text="Add New Size & Cost" OnClick="btnNewSizeCost_Click" ToolTip="Click here to Add New Size & Cost" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12">

                                                <div class="form-group">
                                                    <div class="col-md-12" style="overflow-y: auto; overflow-x: auto; width: 100%;">
                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" AllowPaging="true" CssClass="Grid" OnRowCommand="GridView2_RowCommand"
                                                            OnPageIndexChanging="OnPageIndexChanging2" OnRowDataBound="GridView2_RowDataBound" PageSize="10" EmptyDataText="No data in the data source." ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Location">
                                                                    <HeaderStyle Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdFLoc2" runat="server" Value='<%#Eval("Location_id")%>' />
                                                                        <%--  <telerik:RadComboBox ID="ddGvLocation2" runat="server" placeholder="Location" Width="100%" Enabled="false">
                                                                        </telerik:RadComboBox>--%>
                                                                        <asp:DropDownList ID="ddGvLocation2" runat="server" placeholder="Location" Width="100%" Enabled="false">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtSizeName2" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                                                            MaxLength="15" Text='<%# Eval("Name") %>' onkeyup="OnTextKeyPress(this)" OnTextChanged="txtSizeName2_TextChanged" AutoPostBack="true">
                                                                        </asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Size *">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%--Value='<%#Eval("Size")%>'--%>
                                                                        <asp:HiddenField ID="hdnSize_Id" runat="server" Value='<%#Eval("Size_Id")%>' />
                                                                        <asp:TextBox ID="txtsize2" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                                                            MaxLength="15" Text='<%# Eval("Size") %>' onkeyup="OnTextKeyPress(this)" AutoPostBack="true"
                                                                            OnTextChanged="txtsize2_TextChanged">
                                                                        </asp:TextBox>
                                                                        <%--   <asp:TextBox MinValue="0" MaxValue="999999999" ID="txtsize" Skin="" CssClass="form-control"
                                                                            runat="server" Width="100%" placeholder="Size" MaxLength="8" DbValue='<%#Eval("Size")%>'>                                                                            
                                                                        </asp:TextBox>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty *">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%--Value='<%# Eval("Unit") %>'--%>
                                                                        <asp:HiddenField ID="hdnUnit_Id" runat="server" Value='<%#Eval("Unit_id")%>' />
                                                                        <%--<telerik:RadComboBox ID="ddUnit2" runat="server" Width="100%"
                                                                            OnSelectedIndexChanged="ddUnit2_SelectedIndexChanged" AutoPostBack="true">
                                                                        </telerik:RadComboBox>--%>
                                                                        <asp:DropDownList ID="ddUnit2" runat="server" Width="100%"
                                                                            OnSelectedIndexChanged="ddUnit2_SelectedIndexChanged1" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cost">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnPrice_Id" runat="server" Value='<%#Eval("Price_Id")%>' />
                                                                        <asp:TextBox ID="txtPriceLocation2" CssClass="form-control" Skin="" runat="server" placeholder="Cost" Width="100%"
                                                                            MaxLength="15" Text='<%# Eval("Price") %>' OnTextChanged="txtPriceLocation2_TextChanged" AutoPostBack="true">
                                                                        </asp:TextBox>
                                                                        <%--<asp:TextBox MinValue="0" MaxValue="999999999" ID="txtPriceLocation" Skin="" CssClass="form-control"
                                                                            runat="server" placeholder="Price" MaxLength="8" Width="100%" DbValue='<%#Eval("Price")%>'>
                                                                            <numberformat groupseparator="" decimaldigits="2" keeptrailingzerosonfocus="true" />
                                                                            <incrementsettings interceptmousewheel="false" />
                                                                        </asp:TextBox>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tax">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%--Value='<%# Eval("Unit") %>'--%>
                                                                        <asp:HiddenField ID="hdnTax_Id" runat="server" Value='<%#Eval("Tax_id")%>' />
                                                                        <%-- <telerik:RadComboBox ID="ddTax2" runat="server" Width="100%"
                                                                            OnSelectedIndexChanged="ddTax2_SelectedIndexChanged" AutoPostBack="true">
                                                                        </telerik:RadComboBox>--%>
                                                                        <asp:DropDownList ID="ddTax2" runat="server" Width="100%"
                                                                            OnSelectedIndexChanged="ddTax2_SelectedIndexChanged1" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <HeaderStyle Width="5%" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                            CommandName="DeleteVal" CommandArgument='<%#Eval("row_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div align="center">No records found.</div>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <telerik:RadPanelItem>
                                <ItemTemplate>
                                    <br />
                                </ItemTemplate>
                            </telerik:RadPanelItem>

                            <telerik:RadPanelItem Expanded="false" Text="Product Settings">
                                <ContentTemplate>
                                    <div class="form-body pal">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <label class="lbl" runat="server" id="lbl_error" style="color: red;"></label>

                                                    </div>
                                                    <div class="col-md-4" runat="server" visible="false">
                                                        <label class="lbl">Key Map</label>
                                                        <div class="clearfix"></div>
                                                        <asp:DropDownList ID="ddKeyMap" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                                                            &nbsp; Is Active
                                                        </label>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chkstock" runat="server" Checked="false" />&nbsp; Stocked 
                                            
                                                        </label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chksizezero" runat="server" Checked="false" />&nbsp; Zero Priced
                                                        </label>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chckIsCondiment" runat="server" Checked="false" />&nbsp; Condiment Only 
                                                        </label>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chk_IsPkgProduct" runat="server" Checked="false" AutoPostBack="true"
                                                                OnCheckedChanged="chk_IsPkgProduct_CheckedChanged" />&nbsp; Package Product 
                                                        </label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chk_IsOutOfStock" runat="server" Checked="false" />&nbsp;Sold Out 
                                                        </label>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chk_IsHouse" runat="server" Checked="false" />&nbsp; Highlighted Product 
                                                        </label>
                                                    </div>

                                                    <div class="col-md-4">

                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chk_PriceOnScaleWeight" runat="server" Checked="false" AutoPostBack="true"
                                                                OnCheckedChanged="chk_PriceOnScaleWeight_CheckedChanged" />&nbsp; Price by Weight 
                                                        </label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chckCloakRoom" runat="server" Checked="false" />&nbsp; Cloak Room 
                                                        </label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chk_ForKiosk" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chk_ForKiosk_CheckedChanged" />&nbsp;Kiosk 
                                            
                                                        </label>
                                                    </div>

                                                    <div class="col-md-4" style="visibility: hidden">
                                                        <label class="lbl">
                                                            <asp:CheckBox ID="chk_additionalTax" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chk_additionalTax_CheckedChanged" />&nbsp;Apply Additional Tax 
                                            
                                                        </label>
                                                    </div>

                                                    <div class="col-md-4" id="divDV" runat="server" visible="false">
                                                        <label class="lbl">Is DanceVoucher</label>
                                                        <div class="clearfix"></div>
                                                        <asp:DropDownList runat="server" ID="radIs_DanceVoucher" Width="100%">
                                                            <asp:ListItem Text="SELECT" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Sales Handling Fee" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Payment Handling Fee" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="clearfix"></div>

                                                    <div class="col-md-6">

                                                        <label class="lbl">Other Size </label>
                                                        <div class="clearfix"></div>
                                                        <asp:TextBox runat="server" ID="txtOtherSize" CssClass="form-control" placeholder="Other Size" Width="100%"></asp:TextBox>
                                                        <%--<telerik:RadTextBox ID="txtOtherSize" CssClass="form-control" Skin="" runat="server" placeholder="Other Size" Width="100%"></telerik:RadTextBox>--%>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOtherSize"
                                                            ErrorMessage="Enter only numeric and characters in other size" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="lbl">Other Id</label>
                                                        <div class="clearfix"></div>
                                                        <asp:TextBox runat="server" ID="txtOtherId" CssClass="form-control" placeholder="Other Id" Width="100%"></asp:TextBox>
                                                        <%-- <telerik:RadTextBox ID="txtOtherId" CssClass="form-control" Skin="" runat="server" placeholder="Other Id" Width="100%"></telerik:RadTextBox>--%>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOtherId"
                                                            ErrorMessage="Enter only numeric and characters in other id" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="clearfix"></div>

                                                    <div class="col-md-6">

                                                        <label class="lbl">Sorting No</label>
                                                        <div class="clearfix"></div>
                                                        <asp:TextBox runat="server" ID="txtSortingNo" CssClass="form-control" placeholder="Sorting No" Width="100%"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSortingNo"
                                                            ErrorMessage="Enter only numeric in Sorting No" ValidationGroup="valid" ValidationExpression="[0-9]*$" Display="None"></asp:RegularExpressionValidator>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="lbl">List </label>
                                                        <div class="clearfix"></div>
                                                        <asp:TextBox ID="txtList" MaxLength="9" MinValue="0" CssClass="form-control"
                                                            runat="server" Width="100%" placeholder="List" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>

                                                    </div>

                                                    <div class="clearfix"></div>
                                                    <br />
                                                    <div class="col-md-12">
                                                        <asp:LinkButton CssClass="btn btn-primary" ID="lbn_Associated" runat="server" OnClick="lbn_Associated_Click">Show Product Associated To</asp:LinkButton>
                                                        <div class="clearfix"></div>
                                                        <asp:GridView ID="AttachedTo" runat="server" Visible="false" AutoGenerateColumns="false" AllowPaging="true" CssClass="Grid" OnRowCommand="AttachedTo_RowCommand"
                                                            OnPageIndexChanging="AttachedTo_PageIndexChanging" PageSize="10" EmptyDataText="No data in the data source." ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Product Name">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("product_id") %>'
                                                                            CommandName="Edit" OnClientClick="return true;"><%# Eval("PName") %></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%# Eval("Qty") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Price">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%# Eval("Price") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Size Name">
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%# Eval("SName") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div align="center">No records found.</div>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </div>

                                       
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem>
                                <ItemTemplate>
                                    <br />
                                </ItemTemplate>
                            </telerik:RadPanelItem>

                            <telerik:RadPanelItem Expanded="false" Text="Barcode">

                                <ContentTemplate>
                                    <div class="form-body pal">
                                        <div class="row">

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="validBarcode"
                                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                                    <div class="col-md-12">
                                                        <div class="col-md-8">
                                                            <label class="lbl">Barcode <span class="text-danger">*</span></label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBarcodeM"
                                                                ValidationGroup="validBarcode" Display="none" CssClass="text-danger" ErrorMessage="Barcode is required">
                                                            </asp:RequiredFieldValidator>
                                                            <div class="clearfix"></div>
                                                            <%--<telerik:RadTextBox ID="txtBarcodeM" CssClass="form-control" Skin="" runat="server" placeholder="Barcode" Width="100%"></telerik:RadTextBox>--%>
                                                            <asp:TextBox runat="server" ID="txtBarcodeM" CssClass="form-control" placeholder="Barcode" Width="100%"></asp:TextBox>

                                                        </div>
                                                        <div class="col-md-8">
                                                            <label class="lbl">Size <span class="text-danger">*</span></label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddBarcodeSize"
                                                                ValidationGroup="validBarcode" Display="none" CssClass="text-danger" ErrorMessage="Size is required" InitialValue="SELECT">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddBarcodeSize"
                                                                ValidationGroup="validBarcode" Display="none" CssClass="text-danger" ErrorMessage="Please fill size from size master" InitialValue="">
                                                            </asp:RequiredFieldValidator>
                                                            <div class="clearfix"></div>
                                                            <%--<telerik:RadComboBox ID="ddBarcodeSize" runat="server" Width="100%"></telerik:RadComboBox>--%>
                                                            <asp:DropDownList ID="ddBarcodeSize" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <label></label>
                                                            <div class="clearfix"></div>
                                                            <asp:Button ID="btnBarcode" class="btn btn-primary" runat="server" ValidationGroup="validBarcode" Text="Save" ToolTip="Click here to Save Details" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <asp:HiddenField ID="hdBarcode_size" runat="server" Value='<%#Eval("Barcode_Size_Id")%>' />
                                                        <div class="form-group">

                                                            <telerik:RadGrid ID="rdBarcodeSize" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                                                PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1"
                                                                PagerStyle-AlwaysVisible="false" Skin="MetroTouch" Visible="false" AllowAutomaticDeletes="false" 
                                                                AllowAutomaticUpdates="false" Width="100%"  OnItemCommand="rdBarcodeSize_ItemCommand"  >
                                                                <ClientSettings>
                                                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="200px" />
                                                                </ClientSettings>

                                                                <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false">
                                                                    <Columns>

                                                                        <telerik:GridTemplateColumn HeaderText="Barcode" UniqueName="sizeTemp145">
                                                                            <HeaderStyle Width="50%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBarcodesize" runat="server" Text='<%# Eval("Barcode_Size")%>' />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Barcodefds" UniqueName="sizeTemp145" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBarcodesizefds" runat="server" Text='<%# Eval("Barcode_Size_Id")%>' />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Size Id" UniqueName="size2" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSizedsa" runat="server" Text='<%# Eval("Size_Id")%>' />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Size" UniqueName="sizeTemp2">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsize" runat="server" Text='<%# Eval("Size_Unit")%>' />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                                            <ItemTemplate>

                                                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                                                    CommandArgument='<%#Eval("Barcode_Size_Id")%>' CommandName="EditBarcode" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandName="DeleteBarcode" CommandArgument='<%#Eval("Barcode_Size_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem>
                                <ItemTemplate>
                                    <br />
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem id ="ingredientslbl" Expanded="false" Text="Ingredients" Visible="false">

                                <ContentTemplate>
                                    <div class="form-body pal">

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <label class="lbl">&nbsp;</label>
                                                        <div class="clearfix"></div>
                                                        <asp:Button ID="btnAddNewIngredient" class="btn btn-primary" runat="server" ValidationGroup="validSize" Text="Add New Ingredient" OnClick="btnAddNewIngredient_Click" ToolTip="Click here to Add New Ingredient" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12">
                                                <div class="form-group">

                                                    <asp:UpdatePanel ID="upWall" runat="server" UpdateMode="conditional">
                                                        <ContentTemplate>
                                                            <telerik:RadGrid ID="rdIngredient" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                                                PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1"
                                                                PagerStyle-AlwaysVisible="false" Skin="MetroTouch" AllowAutomaticDeletes="false" AllowAutomaticUpdates="false" Width="100%">
                                                                <ClientSettings>
                                                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="200px" />
                                                                </ClientSettings>
                                                                <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn HeaderText="Name" UniqueName="Product">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnProductSize_Id" runat="server" Value='<%#Eval("product_id")%>' />


                                                                                <%-- <telerik:RadTextBox ID="radAutoIngredient" Skin="MetroTouch" CssClass="form-control" Enabled="false"
                                                                                    runat="server" Width="100%" Text='<%#Eval("Product")%>'>
                                                                                </telerik:RadTextBox>--%>
                                                                                <asp:TextBox runat="server" ID="radAutoIngredient" CssClass="form-control" Enabled="false" Width="100%" Text='<%#Eval("Product")%>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>


                                                                        <telerik:GridTemplateColumn HeaderText="Base Size" UniqueName="sizeTemp">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <%-- <telerik:RadTextBox ID="txtIngredientBaseSize" Skin="MetroTouch" CssClass="form-control" Enabled="false"
                                                                                    runat="server" Width="100%" Text='<%#Eval("Base_Size")%>'>
                                                                                </telerik:RadTextBox>--%>
                                                                                <asp:TextBox ID="txtIngredientBaseSize" CssClass="form-control" Enabled="false" runat="server" Width="100%" Text='<%#Eval("Base_Size")%>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Size" UniqueName="SizeTemp">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtIngredientQtyLocation" runat="server" Text='<%#Eval("Qty")%>' placeholder="Size" MaxLength="8" Width="100%" MinValue="0"
                                                                                    OnTextChanged="txtIngredientQtyLocation_TextChanged" AutoPostBack="true" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>
                                                                                <%--<telerik:RadNumericTextBox MinValue="0" MaxValue="999999999" ID="txtIngredientQtyLocation" Skin="" CssClass="form-control"
                                                                                    runat="server" placeholder="Size" MaxLength="8" Width="100%" DbValue='<%#Eval("Qty")%>' OnTextChanged="txtIngredientQtyLocation_TextChanged" AutoPostBack="true">
                                                                                    <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                                                                    <IncrementSettings InterceptMouseWheel="false" />
                                                                                </telerik:RadNumericTextBox>--%>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="sizeTemp">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <%--<telerik:RadComboBox ID="ddIngredientUnit" runat="server" Width="100%" OnSelectedIndexChanged="ddIngredientUnit_SelectedIndexChanged1" AutoPostBack="true"></telerik:RadComboBox>--%>
                                                                                <asp:DropDownList ID="ddIngredientUnit" runat="server" Width="100%" OnSelectedIndexChanged="ddIngredientUnit_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                                            <HeaderStyle Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnIngredientrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                                                <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("row_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem>
                                <ItemTemplate>
                                    <br />
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Expanded="false" ID="RadPanelItem1" Text="Condiments" visible="false">

                                <ContentTemplate>
                                    <div class="form-body pal">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-6">
                                                        <label class="lbl">&nbsp;</label>
                                                        <div class="clearfix"></div>
                                                        <asp:Button ID="btnAddNewCondiment" class="btn btn-primary" runat="server" ValidationGroup="validSize" Text="Add New Condiment" OnClick="btnAddNewCondiment_Click" ToolTip="Click here to Add New Condiment" />
                                                        <asp:Button ID="btnAddViewCondiment" class="btn btn-primary" runat="server" ValidationGroup="validSize" Text="Add or View Condiment" OnClick="btnAddViewCondiment_Click" ToolTip="Click here to Add or View Condiment" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:UpdatePanel ID="up_Condiment" runat="server" UpdateMode="conditional">
                                                        <ContentTemplate>

                                                            <telerik:RadGrid ID="rdCondiment" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                                                PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1" AutoGenerateEditColumn="false"
                                                                PagerStyle-AlwaysVisible="false" Skin="MetroTouch" AllowAutomaticDeletes="false" AllowAutomaticUpdates="false" Width="100%">
                                                                <ClientSettings>
                                                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="200px" />
                                                                </ClientSettings>
                                                                <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false" AllowAutomaticDeletes="false" AllowAutomaticUpdates="false" >
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn HeaderText="Name" UniqueName="Condiment">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnCondiment_Id" runat="server" Value='<%#Eval("Condiment_Id")%>' />

                                                                                <%-- <telerik:RadTextBox ID="radAutoCondiment" Skin="MetroTouch" CssClass="form-control" Enabled="false"
                                                                                    runat="server" Width="100%" Text='<%#Eval("Condiment")%>'>
                                                                                </telerik:RadTextBox>--%>
                                                                                <asp:TextBox runat="server" ID="radAutoCondiment" CssClass="form-control" Enabled="false" Width="100%" Text='<%#Eval("Condiment")%>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridTemplateColumn HeaderText="Product Name" UniqueName="Product">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>

                                                                                <asp:Label ID="radAutoProductName" CssClass="form-control"
                                                                                    runat="server" Width="100%" Text='<%#Eval("Product")%>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridTemplateColumn HeaderText="Base Size" UniqueName="sizeTemp" Visible="false">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <%--<telerik:RadTextBox ID="txtIngredientBaseSize" Skin="MetroTouch" CssClass="form-control" Enabled="false"
                                                                                    runat="server" Width="100%" Text='<%#Eval("Base_Size")%>'>
                                                                                </telerik:RadTextBox>--%>
                                                                                <asp:TextBox runat="server" ID="txtIngredientBaseSize" CssClass="form-control" Enabled="false" Width="100%" Text='<%#Eval("Base_Size")%>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridTemplateColumn HeaderText="Price" UniqueName="PriceTemp">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtCondimentPriceLocation" runat="server" MinValue="0" MaxLength="9" placeholder="Price" CssClass="form-control"
                                                                                    OnTextChanged="txtCondimentPriceLocation_TextChanged" AutoPostBack="true" Text='<%#Eval("Price")%>' onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>
                                                                                <%-- <telerik:RadNumericTextBox MinValue="0" MaxValue="999999999" ID="txtCondimentPriceLocation" Skin="" CssClass="form-control"
                                                                                    runat="server" placeholder="Price" MaxLength="8" Width="100%" DbValue='<%#Eval("Price")%>' OnTextChanged="txtCondimentPriceLocation_TextChanged" AutoPostBack="true">
                                                                                    <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                                                                    <IncrementSettings InterceptMouseWheel="false" />
                                                                                </telerik:RadNumericTextBox>--%>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Choice" UniqueName="ChoiceSelect">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hfieldChoice" runat="server" Value='<%#Eval("choice")%>' />
                                                                                <asp:DropDownList ID="ddlchoices" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlchoices_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="1" Text="Choice 1" Selected="true"></asp:ListItem>
                                                                                    <asp:ListItem Value="2" Text="Choice 2"></asp:ListItem>
                                                                                    <asp:ListItem Value="3" Text="Choice 3"></asp:ListItem>
                                                                                    <asp:ListItem Value="4" Text="Choice 4"></asp:ListItem>
                                                                                    <asp:ListItem Value="5" Text="Choice 5"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Min selected" UniqueName="minselected">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox MinValue="0" ID="txtminselect" runat="server" placeholder="min selected" OnTextChanged="txtminselect_TextChanged"
                                                                                    AutoPostBack="true" MaxLength="8" Width="100%" Text='<%#Eval("min_select")%>' CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Max selected" UniqueName="maxselected">
                                                                            <HeaderStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtmaxselect" MinValue="0" CssClass="form-control"
                                                                                    runat="server" placeholder="max selected" OnTextChanged="txtmaxselect_TextChanged"
                                                                                    AutoPostBack="true" MaxLength="8" Width="100%" Text='<%#Eval("max_select")%>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                                            <HeaderStyle Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnCondimentrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                                                    CommandName="EditCondi" CommandArgument='<%#Eval("Condiment_Id")%>' OnClientClick="return true;">
                                                                                    <i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("row_Id")%>'
                                                                                    OnClientClick="return confirm('Are you sure you want to Delete this Record ?');">
                                                                                    <i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem>
                                <ItemTemplate>
                                    <br />
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                      
                            <telerik:RadPanelItem id="allergylbl" Expanded="false" Text="Allergy"  Visible="false">

                                <ContentTemplate>
                                    <div class="form-body pal">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-6">
                                                        <label class="lbl">&nbsp;</label>
                                                        <div class="clearfix"></div>
                                                        <asp:Button ID="btnAllergyAdd" class="btn btn-primary" runat="server" OnClick="btnAllergyAdd_Click" ValidationGroup="validSize" Text="Add New Allergy" ToolTip="Click here to Add New Allergy" />

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:UpdatePanel ID="upallergyLIst" runat="server" UpdateMode="conditional">
                                                        <ContentTemplate>

                                                            <telerik:RadGrid ID="rdSAllergy" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                                                PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1"
                                                                PagerStyle-AlwaysVisible="false" Skin="MetroTouch" AllowAutomaticDeletes="false" AllowAutomaticUpdates="false" Width="100%">
                                                                <ClientSettings>
                                                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="200px" />
                                                                </ClientSettings>
                                                                <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Allergy Name" SortExpression="Name"
                                                                            ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                            ShowSortIcon="false" ReadOnly="true">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                                            <HeaderStyle Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnallergy_Id" runat="server" Value='<%#Eval("allergy_Id")%>' />
                                                                                <asp:LinkButton ID="IbADelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("allergy_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                        </Items>
                    </telerik:RadPanelBar>
                 
                    <br />
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Product Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Product Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Product Details" />
                    </div>
                </div>

            </div>

        </telerik:RadAjaxPanel>

        <telerik:RadWindow runat="server" ID="rwCopyLevel" Modal="true" Width="700px"
            Height="450px" KeepInScreenBounds="True" Skin="Bootstrap" VisibleStatusbar="False"
            ReloadOnShow="true" Behaviors="Close" Title="" CssClass="rwEntry" EnableEmbeddedSkins="false">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="RadAjaxPanel5" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div class="panel panel-yellow">
                        <div class="panel-heading">Copy Price Level</div>
                        <div class="panel-body pan">
                            <div class="form-body pal">

                                <br />
                                <div class="row" id="div3" runat="server">
                                    <div class="col-lg-12 " style="overflow-y: auto; height: 230px;">
                                        <div class="table-responsive">
                                            <div class="col-lg-6">
                                                <label>From Price Level</label>
                                                &nbsp;
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlFromPricelvl" AutoPostBack="true" OnSelectedIndexChanged="ddlFromPricelvl_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-6">
                                                <label>To Price Level</label>
                                                &nbsp;
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlToPricelvl" AutoPostBack="true" OnSelectedIndexChanged="ddlToPricelvl_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-lg-6">
                                                <label>Price Type</label>
                                                &nbsp;
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlPriceType">
                                                            <asp:ListItem Text="Amount" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                            </div>

                                            <div class="col-lg-6">
                                                <label>Value</label>
                                                &nbsp;
                                                        <asp:TextBox Width="100%" runat="server" ID="txt_Value"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div style="padding-top: 10px; text-align: center;">
                                    <asp:Button ID="btn_levelSave" class="btn btn-primary" OnClick="btn_levelSave_Click" runat="server" Text="Save" ToolTip="Click here for copy level" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_levelCancel" class="btn btn-primary" OnClick="btn_levelCancel_Click" runat="server" Text="Cancel" ToolTip="Click here for Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>



        <telerik:RadWindow runat="server" ID="rwEntryDetails" Modal="true" Width="800px"
            Height="650px" KeepInScreenBounds="True" Skin="Bootstrap" VisibleStatusbar="False"
            ReloadOnShow="true" Behaviors="Close" Title="" EnableEmbeddedSkins="false" Style="overflow: hidden !important;">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div class="panel panel-yellow">
                        <div class="panel-heading">Product List For Ingredients</div>
                        <div class="panel-body pan">
                            <div class="form-body pal">

                                <br />
                                <div class="row" id="divPGroup" runat="server">
                                    <div class="col-lg-12 " style="overflow-y: auto; height: 445px;">
                                        <div class="table-responsive">
                                            <asp:UpdatePanel ID="up_Pro_Ingredient" runat="server" UpdateMode="conditional">
                                                <ContentTemplate>
                                                    <telerik:RadGrid ID="rdcopyProduct" AutoGenerateColumns="False" AllowPaging="False"
                                                        AllowSorting="True" runat="server" CellSpacing="0"
                                                        GridLines="None" AllowMultiRowSelection="true" AllowFilteringByColumn="True"
                                                        Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true" GroupingEnabled="true" MasterTableView-GroupsDefaultExpanded="false" RetainExpandStateOnRebind="true">
                                                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="false" AllowDragToGroup="true" AllowGroupExpandCollapse="False">
                                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="600px"/>--%>
                                                        </ClientSettings>
                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                        <SortingSettings EnableSkinSortStyles="false" />

                                                        <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="product_id" EnableHeaderContextMenu="true"
                                                            TableLayout="Fixed">
                                                            <GroupByExpressions>
                                                                <telerik:GridGroupByExpression>
                                                                    <SelectFields>
                                                                        <telerik:GridGroupByField FieldAlias="Group" FieldName="Cat_Name"></telerik:GridGroupByField>
                                                                    </SelectFields>
                                                                    <GroupByFields>
                                                                        <telerik:GridGroupByField FieldName="Cat_Name"></telerik:GridGroupByField>
                                                                    </GroupByFields>
                                                                </telerik:GridGroupByExpression>
                                                            </GroupByExpressions>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="10%" />
                                                                <telerik:GridBoundColumn DataField="pro_Name" HeaderText="Product Name" SortExpression="name"
                                                                    ReadOnly="True" FilterControlAltText="Filter product name column" ShowFilterIcon="false"
                                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                                    AutoPostBackOnFilter="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn DataField="base_unit" HeaderText="Base Unit" SortExpression="base_unit"
                                                                    ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                    ShowSortIcon="false" ReadOnly="true">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-top: 10px; text-align: center;">
                                    <asp:Button ID="btn_ProductIngredient" class="btn btn-primary" runat="server" Text="Save" ToolTip="Click here Add Product For Ingredient" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_ProductIngrdientCancel" class="btn btn-primary" runat="server" Text="Cancel" ToolTip="Click here Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>

        <telerik:RadWindow runat="server" ID="rwCondimentDetail" Modal="true" Width="800px"
            Height="750px" KeepInScreenBounds="True" Skin="Bootstrap" VisibleStatusbar="False"
            ReloadOnShow="true" Behaviors="Close" Title="" CssClass="rwEntry" EnableEmbeddedSkins="false">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div class="panel panel-yellow">
                        <div class="panel-heading">Condiment List</div>
                        <div class="panel-body pan">
                            <div class="form-body pal">

                                <br />
                                <div class="row" id="div1" runat="server">
                                    <div class="col-lg-12 " style="overflow-y: auto; height: 500px;">
                                        <div class="table-responsive">
                                            <asp:UpdatePanel ID="up_Pro_Condiment" runat="server" UpdateMode="conditional">
                                                <ContentTemplate>
                                                    <telerik:RadGrid ID="RadCondiment" AutoGenerateColumns="False" AllowPaging="False"
                                                        AllowSorting="True" runat="server" CellSpacing="0"
                                                        GridLines="None" AllowMultiRowSelection="true" AllowFilteringByColumn="True"
                                                        Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true">
                                                        <ClientSettings Selecting-AllowRowSelect="true">
                                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="600px"/>--%>
                                                        </ClientSettings>
                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                        <SortingSettings EnableSkinSortStyles="false" />

                                                        <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Condiment_Id" EnableHeaderContextMenu="true"
                                                            TableLayout="Fixed">

                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="10%" />
                                                                <telerik:GridBoundColumn DataField="Condiment" HeaderText="Condiment" SortExpression="Condiment"
                                                                    ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                    ShowSortIcon="false" ReadOnly="true">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="product_group" HeaderText="Product Group" SortExpression="product_group"
                                                                    ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                    ShowSortIcon="false" ReadOnly="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn DataField="Product" HeaderText="Product Name" SortExpression="name"
                                                                    ReadOnly="True" ShowFilterIcon="true"
                                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                                    AutoPostBackOnFilter="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn DataField="base_unit" HeaderText="Base Unit" SortExpression="base_unit"
                                                                    ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                    ShowSortIcon="false" ReadOnly="true">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-top: 10px; text-align: center;">
                                    <asp:Button ID="btn_ProductCondiment" class="btn btn-primary" runat="server" Text="Save" ToolTip="Click here Add Product For Condiment" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_ProductCondimentCancel" class="btn btn-primary" runat="server" Text="Cancel" ToolTip="Click here Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>

        <telerik:RadWindow runat="server" ID="rdwinAllergy" Modal="true" Width="800px"
            Height="750px" KeepInScreenBounds="True" Skin="Bootstrap" VisibleStatusbar="False"
            ReloadOnShow="true" Behaviors="Close" Title="" CssClass="rwEntry" EnableEmbeddedSkins="false">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div class="panel panel-yellow">
                        <div class="panel-heading">Allergy List</div>
                        <div class="panel-body pan">
                            <div class="form-body pal">

                                <br />
                                <div class="row" id="div2" runat="server">
                                    <div class="col-lg-12 " style="overflow-y: auto; height: 500px;">
                                        <div class="table-responsive">
                                            <asp:UpdatePanel ID="upAllergy" runat="server" UpdateMode="conditional">
                                                <ContentTemplate>
                                                    <telerik:RadGrid ID="rdAllergy" AutoGenerateColumns="False" AllowPaging="False"
                                                        AllowSorting="True" runat="server" CellSpacing="0"
                                                        GridLines="None" AllowMultiRowSelection="true" AllowFilteringByColumn="True"
                                                        Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true">
                                                        <ClientSettings Selecting-AllowRowSelect="true">
                                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="600px"/>--%>
                                                        </ClientSettings>
                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                        <SortingSettings EnableSkinSortStyles="false" />

                                                        <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Allergy_id,name" EnableHeaderContextMenu="true"
                                                            TableLayout="Fixed">
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="10%" />
                                                                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name"
                                                                    ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                    ShowSortIcon="false" ReadOnly="true">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-top: 10px; text-align: center;">
                                    <asp:Button ID="btnSaveAllergy" class="btn btn-primary" OnClick="btnSaveAllergy_Click" runat="server" Text="Save" ToolTip="Click here Add Allergy" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelAllergy" class="btn btn-primary" OnClick="btnCancelAllergy_Click" runat="server" Text="Cancel" ToolTip="Click here Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>



</asp:Content>


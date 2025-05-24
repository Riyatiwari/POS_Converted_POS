<%@ Page Title="Product List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_List.aspx.vb" Inherits="Product_List" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Product List  
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Product List</li>
        </ol>
    </div>
    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script type="text/javascript" language="javascript">
        function redirect(id) {

            //alert(id);
            //sessionStorage.setItem("product_id",id);  
            //alert(sessionStorage.getItem("product_id"));

            //localStorage.setItem("product_id", id);


            //location.href = "Product_Master.aspx";
            //var val = $('[id*="hdProId"]').val();
            //alert($.session.get("product_id"));
            //var value = sessionStorage.getItem("product_id");            
            //location.href = "Product_Master.aspx";
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

        function checkCondiment() {
            var txt;
            var r = confirm("Do you want to copy all stores ?");
            if (r == true) {
                var z = confirm("Do you want to copy condiments ?");
                if (z == true) {

                    document.getElementById('<%= hdCondiments.ClientID %>').value = "1";
                    return true;
                }
                else {

                    document.getElementById('<%= hdCondiments.ClientID %>').value = "0";
                    return true;
                }
            } else {
                return false;
            }
            return false;
        }
    </script>


    <script type="text/javascript">
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnDownload") >= 0 ||
                args.get_eventTarget().indexOf("btnDownload") >= 0 ||
                args.get_eventTarget().indexOf("btnDownload") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>

    <script type="text/javascript">

        $(document).ready(function () {


            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Grid();
            GridSize();
            GridCondiment();
            GridBarcode();
            GridPrinter();

            $("[id*=lblSzName0]").hide();
            $("[id*=lblPrc0]").hide();
            $("[id*=lblSize0]").hide();
            $("[id*=lblUnit0]").hide();

            $("[id*=lblClickAndCollect0]").hide();
            $("[id*=lblDeliver0]").hide();
            $("[id*=lblOrderAtTable0]").hide();

            $("[id*=lblTx0]").hide();


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
                "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },

                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel', 'pdf'

                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }
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
                                '<tr class="group"><td colspan="5"> Product Group : ' + group + '</td> </tr>'

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
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

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
            
            $('input[type="search"]').on('keyup', function () {
                // alert($(this).val());
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }


        function GridSize() {

            var buttonCommon = {
                exportOptions: {

                    format: {
                        body: function (data, row, column, node) {

                            var txt = data;
                            var rex = /(<([^>]+)>)/ig;
                            var newData = (txt.replace(rex, ""));
                            var rplcData = (newData.replace("SELECT20% VAT5% VATVAT Free", ""));
                            var selData = (rplcData.replace("select", ""));

                            return selData;
                            //if (column == 11) {
                            //    //var txt = data;
                            //    //var rex = /(<([^>]+)>)/ig;
                            //    //var newData = (txt.replace(rex, ""));
                            //    var rplcData = (newData.replace("SELECT20% VAT5% VATVAT Free", ""));
                            //    var selData = (rplcData.replace("select", ""));
                            //}

                        }
                    }
                }
            };

            $("#bSizeDetails tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#bSizeDetails').DataTable({
                "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },
                orderCellsTop: true,

                dom: 'Bfrtip',

                buttons: [
                    $.extend(true, {}, buttonCommon, {
                        extend: 'excelHtml5'
                    })
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }

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
                                '<tr class="group"><td colspan="16"> Product Group : ' + group + '</td> </tr>'

                            );
                            last = group;
                        }
                    });

                }

            });

            var table1 = $('#bSizeDetails').DataTable();
            $('#bSizeDetails thead tr:eq(1) th').each(function (i) {
                stateSave: true
                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

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

            $('input[type="search"]').on('keyup', function () {
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });

        }

        function GridCondiment() {
            
            $("#bCondimentDetails tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#bCondimentDetails').DataTable({
                "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },

                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel', 'pdf'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }
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
                                '<tr class="group"><td colspan="7"> Product : ' + group + '</td> </tr>'

                            );
                            last = group;
                        }
                    });
                }
            });
            
            var table1 = $('#bCondimentDetails').DataTable();
            $('#bCondimentDetails thead tr:eq(1) th').each(function (i) {
                stateSave: true
                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

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
            $('input[type="search"]').on('keyup', function () {
                // alert($(this).val());
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });

        }

        function GridBarcode() {
            $("#bBarcodeDetails tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#bBarcodeDetails').DataTable({
                "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },

                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel', 'pdf'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }
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
                                '<tr class="group"><td colspan="7"> Product : ' + group + '</td> </tr>'

                            );
                            last = group;
                        }
                    });
                }
            });
            
            var table1 = $('#bBarcodeDetails').DataTable();
            $('#bBarcodeDetails thead tr:eq(1) th').each(function (i) {
                stateSave: true
                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

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
            $('input[type="search"]').on('keyup', function () {
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });

        }
        
        function GridPrinter() {
            $("#bPrinterDetail tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#bPrinterDetail').DataTable({
                "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },

                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel', 'pdf'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }
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
                                '<tr class="group"><td colspan="16"> Product : ' + group + '</td> </tr>'

                            );
                            last = group;
                        }
                    });
                }
            });

            var table1 = $('#bPrinterDetail').DataTable();
            $('#bPrinterDetail thead tr:eq(1) th').each(function (i) {
                stateSave: true
                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

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
            $('input[type="search"]').on('keyup', function () {
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }

    </script>

    <script type="text/javascript">

        var checkbox = document.getElementById("chbx");
        function checkbox_changed(txt) {

            var nodeId = txt.id;
            var idVal = nodeId.replace("ContentPlaceHolder1_rptSizeDetails_txtSizeName_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_txtPrice_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_txtSize_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_txtUnit_", "");
            //idVal = idVal.replace("ctl00_ContentPlaceHolder1_rptSizeDetails_ddlTx", "");      
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_chkClickAndCollect_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_ChkDeliver_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_ChkOrderAtTable_", "");
            var ids = "ContentPlaceHolder1_rptSizeDetails_chbx_";
            checkbox = document.getElementById(ids.concat(idVal));
            checkbox.checked = true;
        }
    </script>

    <script type="text/javascript">
        function getCheckBoxValues() {
            $('#rptSizeDetails input[type="checkbox"]').each(function () {
                if ($(this).prop('checked') == true) {
                    alert($(this).val());
                }
            });
        }
    </script>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:HiddenField ID="hdCondiments" runat="server" Value="0" />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Product List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                            <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Product</asp:LinkButton>
                            <asp:LinkButton ID="lnkCopy" runat="server" class="btn btn-primary" Visible="false"><i class="fa fa-copy"></i>&nbsp;Copy Product</asp:LinkButton>
                            <asp:LinkButton ID="lnkgrp" runat="server" class="btn btn-primary" Visible="false"><i class="fa fa-plus"></i>&nbsp;New Group</asp:LinkButton>
                            <asp:LinkButton ID="lnkdep" runat="server" class="btn btn-primary" Visible="false"><i class="fa fa-plus"></i>&nbsp;Department</asp:LinkButton>
                           
                            <asp:LinkButton ID="lnkChangeProductGroup" runat="server" class="btn btn-primary"><i class="fa fa-copy"></i>&nbsp;Change Product Group</asp:LinkButton>
                            <asp:LinkButton ID="lnkchangesize" runat="server" class="btn btn-primary" Visible="false"><i class="fa fa-copy" ></i>&nbsp;Change Size </asp:LinkButton>
                            <asp:LinkButton ID="lnk_Import" runat="server" class="btn btn-primary" OnClick="lnk_Import_Click"><i class="fa fa-copy"></i>&nbsp;Import Product</asp:LinkButton>
                            &nbsp&nbsp
                            <label>Views:</label>
                            <asp:DropDownList ID="ddCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddCategory_SelectedIndexChanged">
                                <asp:ListItem Text="Basic" Value="0" />
                                <asp:ListItem Text="Price" Value="1" />
                                <asp:ListItem Text="Condiment" Value="2" />
                                <asp:ListItem Text="Barcode" Value="3" />
                                <asp:ListItem Text="Printer" Value="4" />
                            </asp:DropDownList>
                            <div style="float: right; visibility: hidden">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="25%">Import file : 
                                                </td>
                                                <td width="60%" style="margin-left: 10px;">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </td>
                                                <td width="20%">
                                                    <asp:LinkButton ID="btnUpload" runat="server" class="btn btn-primary" Text="Upload"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <a href="../Files/Product_Sample_Old.xlsx" style="color: blue; text-decoration: underline; float: right; margin-top: 10px;">Download Template</a>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="row">
                                <label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;       Active &nbsp;
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" OnCheckedChanged="chkActive_CheckedChanged" AutoPostBack="true" /></label>
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkChangePrice" Visible="false" OnClientClick="return true;" runat="server" class="btn btn-primary"><i class="fa fa-copy"></i>&nbsp; Update Price </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-lg-12 ">
                            <div id="dv_Mapping" style="float: left; margin-top: 10px;" visible="false" runat="server">
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
                                        <td width="25%" style="padding-left: 20px;">
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
                                        <td width="20%" style="padding-left: 20px;">
                                            <asp:LinkButton ID="btnSave" runat="server" Style="margin-left: 10px; margin-top: 20px;" class="btn btn-primary"><i class="fa fa-copy"></i>&nbsp;Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </div>
                        </div>
                    </div>

                    <br />
                   <div class="row" id="divPGroup" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:Panel runat="server" ID="PnlPsummary">
                                    <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                        <asp:Repeater ID="rdProduct" runat="server">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th>Product Group</th>
                                                        <th>Product</th>
                                                        <th>Department</th>
                                                        <th>Base</th>
                                                        <th>Created Datetime</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="background-color: #ffffff;"><%#Eval("category_name") %></td>
                                                    <td style="background-color: #ffffff;">
                                                        <asp:LinkButton ID="lbtnProductEdit" runat="server" CausesValidation="False" PostBackUrl='<%#"Product_Master.aspx?product_id=" & Eval("product_id") %>'
                                                            CommandName="ProductEdit" OnClientClick="return true;"><%#Eval("name") %></asp:LinkButton>
                                                    </td>
                                                    <td style="background-color: #ffffff;"><%#Eval("Department") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("BaseSize") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("created_date") %></td>
                                                    <td style="background-color: #ffffff;">
                                                        <asp:HiddenField ID="hfCategory_id" runat="server" Value='<%#Eval("product_id")%>' />
                                                        <asp:HiddenField ID="hdProId" runat="server" Value='<%#Eval("product_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit" PostBackUrl='<%#"Product_Master.aspx?product_id=" & Eval("product_id") %>'
                                                            CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                            CommandArgument='<%#Eval("product_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa  fa-trash"></i></asp:LinkButton>

                                                        <asp:LinkButton ID="lnkCopyone" runat="server" CausesValidation="False" ToolTip="Copy this product"
                                                            CommandArgument='<%#Eval("product_id")%>' CommandName="Copy" OnClientClick="return checkCondiment();"><i class="fa fa-copy fa-lg"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnisAct" runat="server" CausesValidation="False" ToolTip="Act"
                                                            CommandArgument='<%#Eval("product_id")%>' CommandName="Act" OnClientClick="return confirm('Are you sure you want to Change Status ?');"><i class="glyphicon glyphicon-sort"></i></asp:LinkButton>

                                                        <asp:Label ID="lblProdId" runat="server" Text='<%# Eval("product_id") %>' Visible="false" />

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>
                                <tr>
                                    <th>Product Group</th>
                                    <th>Product</th>
                                    <th>Department</th>
                                    <th>Base</th>
                                    <th>Created Datetime</th>
                                    <th>Action</th>

                                </tr>
                                </table>
                            </tfoot>

                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>

                                <asp:Panel runat="server" ID="PnlSizeDetails" Visible="false">

                                    <div class="card-body" style="overflow-x: scroll">
                                        <div class="table-responsive">
                                            <table class="table table-bordered" id="bSizeDetails" width="100%" cellspacing="0">
                                                <asp:Repeater ID="rptSizeDetails" runat="server" OnItemDataBound="rptSizeDetails_ItemDataBound">
                                                    <HeaderTemplate>

                                                        <thead>
                                                            <tr>
                                                                <th>Product Group</th>
                                                                <th>Product</th>
                                                                <th>Base</th>
                                                                <th>Location</th>
                                                                <th>Name</th>
                                                                <th>Size Name</th>
                                                                <th>Price</th>
                                                                <th>Active    </th>
                                                                <th>Size    </th>
                                                                <th>Unit   </th>
                                                                <th>Actual Price   </th>
                                                                <th>Tax   </th>
                                                                <th>Click and Collect</th>
                                                                <th>Deliver</th>
                                                                <th>Order at table</th>
                                                                <th>Action</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="background-color: #ffffff;"><%#Eval("Product Group") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("name") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("BaseSize") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Location") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Name") %></td>
                                                            <td style="background-color: #ffffff;">
                                                                <asp:Label ID="lblSzName0" runat="server" Visible="true" Text='<%# Eval("Size") %>' />
                                                                <asp:TextBox ID="txtSizeName" onChange="checkbox_changed(this)" BorderStyle="None" runat="server" Text='<%#Eval("Size") %>' />
                                                            </td>
                                                            <td style="background-color: #ffffff;">
                                                                <asp:Label ID="lblPrc0" runat="server" Visible="true" Text='<%# Eval("Price") %>' />
                                                                <asp:TextBox ID="txtPrice" onChange="checkbox_changed(this)" BorderStyle="None" runat="server" Text='<%#Eval("Price") %>'>  </asp:TextBox>
                                                            </td>

                                                            <td style="background-color: #ffffff;"><%#Eval("Active") %></td>

                                                            <td style="background-color: #ffffff;">
                                                                <asp:Label ID="lblSize0" runat="server" Visible="true" Text='<%# Eval("Size_Unit") %>' />
                                                                <asp:TextBox ID="txtSize" onChange="checkbox_changed(this)" BorderStyle="None" runat="server" Text='<%#Eval("Size_Unit") %>'>  </asp:TextBox>
                                                            </td>
                                                            <td style="background-color: #ffffff;">
                                                                <asp:Label ID="lblUnit0" runat="server" Visible="true" Text='<%# Eval("Unit") %>' />
                                                                <asp:TextBox ID="txtUnit" onChange="checkbox_changed(this)" BorderStyle="None" runat="server" Text='<%#Eval("Unit") %>'>  </asp:TextBox>
                                                            </td>

                                                            <td style="background-color: #ffffff;"><%#Eval("Actual_Price") %></td>

                                                            <td style="background-color: #ffffff;">
                                                                <asp:Label ID="lblTx0" runat="server" Visible="true" />

                                                                <asp:DropDownList ID="ddlTx" onChange="checkbox_changed(this)" runat="server" Visible="false">
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td style="background-color: #ffffff;">
                                                                <asp:Label ID="lblClickAndCollect0" runat="server" Visible="true" />
                                                                <asp:CheckBox runat="server" ID="chkClickAndCollect" onChange="checkbox_changed(this)" />
                                                            </td>

                                                            <td style="background-color: #ffffff;">
                                                                <asp:Label ID="lblDeliver0" runat="server" Visible="true" />
                                                                <asp:CheckBox runat="server" ID="ChkDeliver" onChange="checkbox_changed(this)" />
                                                            </td>

                                                            <td style="background-color: #ffffff;">
                                                                <asp:Label ID="lblOrderAtTable0" runat="server" Visible="true" />
                                                                <asp:CheckBox runat="server" ID="ChkOrderAtTable" onChange="checkbox_changed(this)" />
                                                            </td>

                                                            <td style="background-color: #ffffff; width: 500px;">
                                                                <asp:HiddenField ID="hfCategory_id" runat="server" Value='<%#Eval("product_id")%>' />

                                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit" PostBackUrl='<%#"Product_Master.aspx?product_id=" & Eval("product_id") %>'
                                                                    CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                               <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa  fa-trash"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCopyone" runat="server" CausesValidation="False" ToolTip="Copy this product"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Copy" OnClientClick="return checkCondiment();"><i class="fa fa-copy fa-lg"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnisAct" runat="server" CausesValidation="False" ToolTip="Act"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Act"><i class="glyphicon glyphicon-sort"></i></asp:LinkButton>
                                                                <asp:CheckBox runat="server" ID="chbx" />
                                                                <asp:Label ID="lblProdId" runat="server" Text='<%# Eval("product_id") %>' Visible="false" />
                                                                <asp:Label ID="lblPriceId" runat="server" Text='<%# Eval("Cost") %>' Visible="false" />
                                                                <asp:HiddenField ID="hdfSizeId" runat="server" Value='<%#Eval("size_id")%>' />
                                                                <asp:HiddenField ID="hdfUnitId" runat="server" Value='<%#Eval("unit_id")%>' />
                                                                <asp:HiddenField ID="hdfTaxId" runat="server" Value='<%#Eval("Tax_id")%>' />

                                                                <asp:HiddenField ID="HdClickAndCollect" runat="server" Value='<%#Eval("Click and Collect")%>' />
                                                                <asp:HiddenField ID="HdChkDeliver" runat="server" Value='<%#Eval("deliver")%>' />
                                                                <asp:HiddenField ID="HdChkOrderAtTable" runat="server" Value='<%#Eval("Order at table")%>' />
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
                                </asp:Panel>


                                <asp:Panel runat="server" ID="PnlCondimentDetails" Visible="false">
                                    <div class="card-body" style="overflow-x: scroll">
                                        <div class="table-responsive">
                                            <table class="table table-bordered" id="bCondimentDetails" width="100%" cellspacing="0">
                                                <asp:Repeater ID="rptzCondimentDetails" runat="server">
                                                    <HeaderTemplate>

                                                        <thead>
                                                            <tr>
                                                                <th>Product</th>
                                                                <th>Condiment</th>
                                                                <th>Active</th>
                                                                <th>Qty</th>
                                                                <th>Unit</th>
                                                                <th>Product Group</th>
                                                                <th>Choices</th>
                                                                <th>Action</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="background-color: #ffffff;"><%#Eval("Product") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Condiment") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Active") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("quantity") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Unit") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("product group") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("choices") %></td>

                                                            <td style="background-color: #ffffff;">
                                                                <asp:HiddenField ID="hfCategory_id" runat="server" Value='<%#Eval("product_id")%>' />
                                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit" PostBackUrl='<%#"Product_Master.aspx?product_id=" & Eval("product_id") %>'
                                                                    CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                               <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa  fa-trash"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCopyone" runat="server" CausesValidation="False" ToolTip="Copy this product"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Copy" OnClientClick="return checkCondiment();"><i class="fa fa-copy fa-lg"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnisAct" runat="server" CausesValidation="False" ToolTip="Act"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Act"><i class="glyphicon glyphicon-sort"></i></asp:LinkButton>
                                                                <asp:Label ID="lblProdId" runat="server" Text='<%# Eval("product_id") %>' Visible="false" />
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
                                </asp:Panel>

                                <asp:Panel runat="server" ID="PnlPrinterDetail" Visible="false">
                                    <div class="card-body" style="overflow-x: scroll">
                                        <div class="table-responsive">
                                            <table class="table table-bordered" id="bPrinterDetail" width="100%" cellspacing="0">
                                                <asp:Repeater ID="rptPrinterDetail" runat="server">
                                                    <HeaderTemplate>

                                                        <thead>
                                                            <tr>
                                                                <th>Product</th>
                                                                <th>Printer Name</th>
                                                                <th>Action</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="background-color: #ffffff;"><%#Eval("Product") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("PrinterName") %></td>
                                                            <td style="background-color: #ffffff;">
                                                                <asp:HiddenField ID="hfCategory_id" runat="server" Value='<%#Eval("product_id")%>' />
                                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit" PostBackUrl='<%#"Product_Master.aspx?product_id=" & Eval("product_id") %>'
                                                                    CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                               <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa  fa-trash"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCopyone" runat="server" CausesValidation="False" ToolTip="Copy this product"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Copy" OnClientClick="return checkCondiment();"><i class="fa fa-copy fa-lg"></i></asp:LinkButton>

                                                                <asp:Label ID="lblProdId" runat="server" Text='<%# Eval("product_id") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnisAct" runat="server" CausesValidation="False" ToolTip="Act"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Act">   <i class="glyphicon glyphicon-sort"></i></asp:LinkButton>
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
                                </asp:Panel>

                                <asp:Panel runat="server" ID="PnlBarcodeDetails" Visible="false">
                                    <div class="card-body" style="overflow-x: scroll">
                                        <div class="table-responsive">
                                            <table class="table table-bordered" id="bBarcodeDetails" width="100%" cellspacing="0">
                                                <asp:Repeater ID="rptzBarcodeDetails" runat="server">
                                                    <HeaderTemplate>

                                                        <thead>
                                                            <tr>
                                                                <th>Product</th>
                                                                <th>Size</th>
                                                                <th>Barcode</th>
                                                                <th>Action</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="background-color: #ffffff;"><%#Eval("Product") %></td>
                                                           <td style="background-color: #ffffff;"><%#Eval("Size") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Barcode_Size") %></td>
                                                           <td style="background-color: #ffffff;">
                                                                <asp:HiddenField ID="hfCategory_id" runat="server" Value='<%#Eval("product_id")%>' />
                                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit" PostBackUrl='<%#"Product_Master.aspx?product_id=" & Eval("product_id") %>'
                                                                    CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                               <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa  fa-trash"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCopyone" runat="server" CausesValidation="False" ToolTip="Copy this product"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Copy" OnClientClick="return checkCondiment();"><i class="fa fa-copy fa-lg"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnisAct" runat="server" CausesValidation="False" ToolTip="Act"
                                                                    CommandArgument='<%#Eval("product_id")%>' CommandName="Act"><i class="glyphicon glyphicon-sort"></i></asp:LinkButton>
                                                                <asp:Label ID="lblProdId" runat="server" Text='<%# Eval("product_id") %>' Visible="false" />

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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnsearchvalue" />
    <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />
    <asp:HiddenField runat="server" ID="hdsearchvalueSize" />
    <asp:HiddenField runat="server" ID="hdsearchvalueafterEditSize" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

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




<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Stock_Detail.aspx.vb" Inherits="Stock_Detail" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Stock Change
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
   
   
 
     <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <%--  <li class="active"><a href="Tax_List.aspx">Tax List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>--%>
            <li class="active">Stock Change</li>
        </ol>
    </div>
     
 
    <style>
        #ctl00_ContentPlaceHolder1_rwEntryDetails_C_btn_Next:focus {
            border: 2px solid #4cff00;
        }

        #ctl00_ContentPlaceHolder1_rwEntryDetails_C_btn_Cancel:focus {
            border: 2px solid #4cff00;
        }

        #ctl00_ContentPlaceHolder1_rwEntryDetails_C_btn_Save:focus {
            border: 2px solid #4cff00;
        }
    </style>

    <script>
        function check() {
            var input = document.getElementById("ctl00_ContentPlaceHolder1_rwEntryDetails_C_txt_Qty");

            input.addEventListener("keyup", function (event) {
                if (event.keyCode === 13) {
                    event.preventDefault();

                    var input2 = document.getElementById("ctl00_ContentPlaceHolder1_rwEntryDetails_C_btn_Next");

                    if (input2 == null) {
                        var input3 = document.getElementById("ctl00_ContentPlaceHolder1_rwEntryDetails_C_btn_Save");
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
 <%--    <script>
         $(document).ready(function () {
             $('#searchBox').on('keyup', function () {
                 var value = $(this).val().toLowerCase();
                 $('#<%= rdproduct.ClientID %> tr').filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        });
    });
</script>--%>


<style>
    .table > tbody > tr > th{
        line-height: 2.429;
        vertical-align  :central;
    }
   .th {
  text-align: center;
}

     .custom-dropdown {
            -webkit-appearance: none; 
            -moz-appearance: none;  
            appearance: none;  
            background: transparent;
            border: none;  
            padding: 0;  
            height: 100%; 
            box-sizing: border-box;  
        }

        .custom-dropdown::-ms-expand {
            display: none;  
        }

       
        .grid-cell {
            padding: 0;
        }

        .grid-cell .custom-dropdown {
            height: 100%;  
        }
         .form-control {
        display: block;
        width: 100%;
        padding: 6px 12px;
        font-size: 14px;
        line-height: 1.42857143;
        color: #555;
        background-color: #fff;
        background-image: none;
        border: 1px solid #ccc;
        border-radius: 4px;
        -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
        -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
    }
    </style>

   <script type="text/javascript">
       window.onload = function () {
           var ddlStockDetails = document.getElementById('ddlstockdetails');
           var txtCountQuantityElement = document.getElementById('txtcountquantity');

           console.log("ddlStockDetails: ", ddlStockDetails);
           console.log("txtCountQuantityElement: ", txtCountQuantityElement);
       };



   </script>
     <script>

         $(document).ready(function () {
             $('#searchBox').on('keyup', function () {
                 var searchText = $(this).val().toLowerCase();
                 console.log("Search text:", searchText); // Check if search text is captured correctly
                 $('#<%=rdproduct.ClientID %> tbody tr').each(function () {
            var row = $(this);
            var productGroup = row.find("[id$='ddlproductgroup']").text().toLowerCase();
            var product = row.find("[id$='ddlproduct']").text().toLowerCase();
            var baseUnit = row.find("[id$='baseunit']").val().toLowerCase();
            var stockOnHand = row.find("[id$='txtbasesize']").val().toLowerCase();
            var quantity = row.find("[id$='txttotalStock']").val().toLowerCase();
            var countQuantity = row.find("[id$='txtcountquantity']").val().toLowerCase();

            console.log("Product Group:", productGroup); 
            console.log("Product:", product);  
            console.log("Base Unit:", baseUnit); 
            console.log("Stock On Hand:", stockOnHand); 
            console.log("Quantity:", quantity);  
            console.log("Count Quantity:", countQuantity);  

            if (productGroup.indexOf(searchText) > -1 || product.indexOf(searchText) > -1 || baseUnit.indexOf(searchText) > -1 || stockOnHand.indexOf(searchText) > -1 || quantity.indexOf(searchText) > -1 || countQuantity.indexOf(searchText) > -1) {
                row.show();
            } else {
                row.hide();
            }
        });
    });
         });
     </script>
     <script type="text/javascript">
         function updateLabel(dropdown) {
             var selectedText = dropdown.options[dropdown.selectedIndex].text;
             var label = dropdown.parentElement.querySelector('label');
             if (label) {
                 label.textContent = selectedText;
             }
         }
</script>
     <style>
            .center-text {
        text-align: center;
    }
  @media (max-width: 768px) {
        .product-column {
            width: 20% !important;  
        }
    }

    .form-control {
        display: block;
        width: 100%;
        padding: 6px 12px;
        font-size: 14px;
        line-height: 1.42857143;
        color: #555;
        background-color: #fff;
        background-image: none;
        border: 1px solid #ccc;
        border-radius: 4px;
        -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
        -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
    }

    .custom-dropdown {
        -webkit-appearance: none;
        -moz-appearance: none;
        appearance: none;
        background: transparent;
        border: none;
        padding: 0;
        height: 100%;
        box-sizing: border-box;
    }

    .custom-dropdown::-ms-expand {
        display: none;
    }

    .grid-cell {
        padding: 0;
    }

    .grid-cell .custom-dropdown {
        height: 100%;
        text-align:center;
    }
         </style>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    $(document).ready(function () {
        var $rows = $('#rdproduct tbody tr');

        $('#SearchBox').on('input', function () {
            var searchText = $(this).val().toLowerCase();
            $rows.each(function () {
                var $row = $(this);
                var productGroup = $row.find('[id*=ddlproductgroup] option:selected').text().toLowerCase();
                var product = $row.find('[id*=ddlproduct] option:selected').text().toLowerCase();
                var baseUnit = $row.find('[id*=baseunit]').val().toLowerCase();
                var stockOnHand = $row.find('[id*=txtbasesize]').val().toLowerCase();
                var quantity = $row.find('[id*=txttotalStock]').val().toLowerCase();
                var countQuantity = $row.find('[id*=txtcountquantity]').val().toLowerCase();
                var containsText = productGroup.includes(searchText) || product.includes(searchText) || baseUnit.includes(searchText) || stockOnHand.includes(searchText) || quantity.includes(searchText) || countQuantity.includes(searchText);
                $row.toggle(containsText);
            });
        });
    });

</script>
 
    <div class="col-lg-12 ">
        <%--  <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Stock Change</div>
            <div class="panel-body pan">

                <div class="form-body pal">
                    <div class="row">
                        <%--<div class="col-md-12">--%>
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
                                    <label>Location <span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="rdlocation" runat="server" Width="100%" Skin="MetroTouch" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rflocation" runat="server" ControlToValidate="rdlocation"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Location is required" CssClass="text-danger" InitialValue="SELECT">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    <label>For Date<span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>
                                    <telerik:RadDatePicker ID="txtdate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="100%">
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                        <Calendar runat="server" FirstDayOfWeek="Monday">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator runat="server" ID="rqfordate" ControlToValidate="txtdate" ValidationGroup="valid" Display="None"
                                        ErrorMessage="Date is required"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-2">

                                    <label>Stock Detail<span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="ddlstockdetails" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlstockdetails_SelectedIndexChanged" Skin="MetroTouch">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="SELECT" Value="0" />
                                            <telerik:RadComboBoxItem Text="Wastage" Value="2" />
                                            <telerik:RadComboBoxItem Text="Stock take" Value="3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rqstockdetail" ControlToValidate="ddlstockdetails" ValidationGroup="valid" Display="None" InitialValue="SELECT"
                                        ErrorMessage="Stock Detail is required"></asp:RequiredFieldValidator>

                                </div>

                                <div class="col-md-1">
                                    <label>&nbsp;</label>
                                    <div class="clearfix"></div>
                                    <asp:Button ID="btnaddProduct" class="btn btn-primary" runat="server" ValidationGroup="validSize" Text="+" ToolTip="Click here to Add New Stock Change" />
                                </div>


                                <div class="col-md-1">
                                    <label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </label>
                                    <div class="clearfix"></div>
                                    <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" ValidationGroup="valid"
                                        Text="Upload" ToolTip="Click here to Upload Stock" OnClick="btnUpload_Click" />
                                </div>

                                <div class="col-md-1">
                                    <label>&nbsp;</label>
                                    <div class="clearfix"></div>
                                    <asp:Button ID="btnDownload" class="btn btn-primary" runat="server"
                                        Text="Download" ToolTip="Click here to Download Stock" OnClick="btnDownload_Click" />
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-4">
                                    <label>Stock Description </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="rdStockDesc" CssClass="form-control" Skin="" runat="server" placeholder="Stock Description" Width="100%" TextMode="MultiLine" Rows="4"></telerik:RadTextBox>
                                </div>

                                <div class="clearfix"></div>
                                  <br />

                                <div class="col-md-12">
                                    <label><b>NOTE : </b>When importing a stock count, if the quantity remains as “0 Qty” it will be ignored as per as a partial stock , to mark a stock as zero on hand replace the “0 Qty” with the number 0.</label>
                                </div>
                                <br />
                                <div class="clearfix"></div>
                                <div class="col-md-12">
                                    <%--<asp:UpdatePanel ID="upWall" runat="server" UpdateMode="conditional">
                                        <ContentTemplate>--%>

    <%--                                        <div>
    <table id="productTable" class="display">
        <thead>
            <tr>
                <th>Product Group</th>
                <th>Product</th>
                <th>Stock On Hand</th>
                <th>Quantity</th>
                <th>Count Quantity</th>
                <th>Volume</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rdproduct" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdproductgroup" runat="server" Value='<%# Eval("Product_group") %>' />
                            <asp:DropDownList ID="ddlproductgroup" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdproduct" runat="server" Value='<%# Eval("Product_id") %>' />
                            <asp:DropDownList ID="ddlproduct" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdstockonhand" runat="server" Value='<%# Eval("stock_on_hand") %>' />
                            <asp:TextBox ID="txtstockonhand" runat="server" ReadOnly="true" Text='<%# Eval("stock_on_hand") %>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txttotalStock" runat="server" Text='<%# Eval("Total_Stock") %>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcountquantity" runat="server" Text='<%# Eval("count_quantity") %>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdunit" runat="server" Value='<%# Eval("unit_id") %>' />
                            <asp:DropDownList ID="ddltotalunit" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:LinkButton ID="IbDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("row_Id") %>'>Delete</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>--%>





                                           <%-- <telerik:RadGrid ID="rdproduct" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                                PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1"
                                                PagerStyle-AlwaysVisible="false" Skin="MetroTouch" AllowAutomaticDeletes="false" AllowAutomaticUpdates="false" Width="100%">
                                                <ClientSettings>
                                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="200px" />
                                                </ClientSettings>
                                                <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false" Font-Size="90%">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Product Group *" UniqueName="Product_Group">
                                                            <HeaderStyle Width="13%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdproductgroup" runat="server" Value='<%#Eval("Product_group")%>' />
                                                                <telerik:RadComboBox ID="ddlproductgroup" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlproductgroup_SelectedIndexChanged"></telerik:RadComboBox>
                                                            </ItemTemplate>

                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Product *" UniqueName="Product_id">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdproduct" runat="server" Value='<%#Eval("Product_id")%>' />
                                                                <telerik:RadComboBox ID="ddlproduct" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged"></telerik:RadComboBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Stock On Hand" UniqueName="stock_on_hand">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdstockonhand" runat="server" Value='<%#Eval("stock_on_hand")%>' />
                                                                <telerik:RadTextBox ID="txtstockonhand" CssClass="form-control" Skin="" runat="server" placeholder="Stock On Hand" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("stock_on_hand")%>' AutoPostBack="true" OnTextChanged="txtstockonhand_TextChanged" ReadOnly="true">
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>


                                                        <telerik:GridTemplateColumn HeaderText="Quantity " UniqueName="Quantity">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <%--Value='<%#Eval("Size")%>'--%>
                                                                <%--  <asp:HiddenField ID="hdnSize_Id" runat="server" Value='<%#Eval("Product_id")%>' />--%>
                                                              <%--  <telerik:RadTextBox ID="txttotalStock" CssClass="form-control" Skin="" runat="server" placeholder="Total Stock" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("Total_Stock") %>' AutoPostBack="true" OnTextChanged="txttotalStock_TextChanged">
                                                                </telerik:RadTextBox>

                                                            </ItemTemplate>

                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Count Quantity " UniqueName="sizeTemp">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdcountqty" runat="server" Value='<%#Eval("count_quantity")%>' />
                                                                <telerik:RadTextBox ID="txtcountquantity" CssClass="form-control" Skin="" runat="server" placeholder="Count Quantity" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("count_quantity")%>' AutoPostBack="true" OnTextChanged="txtcountquantity_TextChanged">
                                                                </telerik:RadTextBox>

                                                            </ItemTemplate>

                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Volume" UniqueName="sizeTemp">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdunit" runat="server" Value='<%#Eval("unit_id")%>' />
                                                                <telerik:RadComboBox ID="ddltotalunit" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddltotalunit_SelectedIndexChanged"></telerik:RadComboBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                                <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("row_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid> --%>
                                 
                                    <div style="overflow-x: auto;">
                                    <br />
                                    <asp:Label> Search:
                                        <asp:TextBox ID="SearchBox"   placeholder="Search..." runat="server" AutoPostBack="true" OnTextChanged="SearchBox_TextChanged1"></asp:TextBox>
                                    </asp:Label>
<br />
<br />
 
<asp:UpdatePanel ID="upWall" runat="server" UpdateMode="conditional">
                                        <ContentTemplate>
        <asp:GridView ID="rdproduct" AutoGenerateColumns="false" runat="server" OnRowDataBound="rdproduct_RowDataBound"   Width="100%" CssClass="table table-bordered gridview-header" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="Product&nbsp;Group * "  ItemStyle-CssClass="grid-column center-text">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdproductgroup" runat="server" Value='<%# Eval("Product_group") %>' />
                        <asp:DropDownList ID="ddlproductgroup"   Enabled="false" Width="100%" runat="server" AutoPostBack="true" cssclass="form-control custom-dropdown" OnSelectedIndexChanged="ddlproductgroup_SelectedIndexChanged"></asp:DropDownList>
                    <lable ID="lblproductgroup" CssClass="form-control" runat="server"></lable>
                    </ItemTemplate>
                       <HeaderStyle   Width="3%" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Product&nbsp;&nbsp;&nbsp;*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" ItemStyle-CssClass="grid-column center-text">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdproduct" runat="server" Value='<%# Eval("Product_id") %>' />
                        <asp:DropDownList ID="ddlproduct" Width="100%" Enabled="false"  runat="server" AutoPostBack="true" CssClass="form-control custom-dropdown" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged"></asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle  Width="3%" />
                 
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Base&nbsp;&nbsp;Unit" ItemStyle-CssClass="grid-column center-text">
                    <ItemTemplate>
                      <%--  <asp:HiddenField ID="txt_baseunit" runat="server" Value='<%# Eval("stock_on_hand") %>' />--%>
                        <asp:TextBox ID="baseunit" placeholder="Stock On Hand" runat="server" Width="100%" MaxLength="15" Text='<%# Eval("Base_size") %>' ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Width="4%" />
                    <HeaderStyle   height="30px" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Quantity " ItemStyle-CssClass="grid-column center-text">
                    <ItemTemplate>
                        <asp:TextBox ID="txttotalStock" runat="server"  MaxLength="15" Text='<%# Eval("Total_Stock") %>' AutoPostBack="true" OnTextChanged="txttotalStock_TextChanged" CssClass="form-control"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle  Width="2%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Count&nbsp;&nbspQuantity  " ItemStyle-CssClass="grid-column center-text">
                    <ItemTemplate>
                        <asp:TextBox ID="txtcountquantity" runat="server"  Text='<%# Eval("count_quantity") %>' AutoPostBack="true" OnTextChanged="txtcountquantity_TextChanged" CssClass="form-control"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle  Width="2%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stock&nbsp;&nbspOn&nbsp;&nbspHand " ItemStyle-CssClass="grid-column center-text">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdstockonhand" runat="server" Value='<%# Eval("stock_on_hand") %>' />
                        <asp:TextBox ID="txtstockonhand" placeholder="Stock On Hand" runat="server" Width="100%" MaxLength="15" Text='<%# Eval("stock_on_hand") %>' ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </ItemTemplate>
                     <HeaderStyle Width="3%" />
                    <HeaderStyle   height="30px" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Variance" ItemStyle-CssClass="grid-column center-text">
            <ItemTemplate>
                <asp:Label ID="lblVariance" runat="server" CssClass="form-control"></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="6%" />
            <HeaderStyle CssClass="center-text" height="40px" />
        </asp:TemplateField>
                <asp:TemplateField visible="false"  HeaderText="Volume" ItemStyle-CssClass="grid-column center-text">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdunit" runat="server" Value='<%# Eval("unit_id") %>' />
                        <asp:DropDownList  visible="false"   OnChange="updateLabel(this)" ID="ddltotalunit" Enabled="false" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddltotalunit_SelectedIndexChanged" CssClass="form-control custom-dropdown"></asp:DropDownList>
                    
                           <%--<asp:TextBox ID="txtVolume" runat="server" Text='<%# Eval("unit_id") %>' ReadOnly="true" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>--%>
                    </ItemTemplate>
                    <HeaderStyle   Width="13%" />
                </asp:TemplateField>

         <%-- <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <td style="text-align: center;">
                <asp:HiddenField ID="hdnrow_Id" runat="server" Value='<%# Eval("row_Id") %>' />
                <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete" CommandName="DeleteVal" CommandArgument='<%# Eval("row_Id") %>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');">
                    <i class="fa fa-minus-square-o fa-lg"></i>
                </asp:LinkButton>
                    </td>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
        </asp:TemplateField>--%>
<asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-CssClass="grid-column center-text">
    <ItemTemplate>
        <div style="display: flex; justify-content: center;">
            <asp:HiddenField ID="hdnrow_Id" runat="server" Value='<%# Eval("row_Id") %>' />
            <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete" CommandName="DeleteVal" CommandArgument='<%# Eval("row_Id") %>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');">
                <i class="fa fa-minus-square-o fa-lg"></i>

            </asp:LinkButton>
        </div>

    </ItemTemplate>
    <HeaderStyle CssClass="center-text" />
</asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
</div>

<%--                                        </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>


                <div class="col-lg-12 ">

                    <asp:GridView ID="grid_view" EmptyDataText="No Records Found." runat="server" Border="1" BorderColor="Black"
                        AutoGenerateColumns="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                        CssClass="mGrid"
                        PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt">
                        <Columns>
                        </Columns>

                    </asp:GridView>

                </div>

                <%--  <div class="col-md-6">
                            <div class="clearfix"></div>
                        </div>--%>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Stock Change" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Submit" ValidationGroup="valid" ToolTip="Click here to Submit Stock Change" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Stock Change" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Stock Change" />
                </div>

            </div>

            <%-- <div class="col-md-6">--%>
            <%--   <div class="clearfix"></div>
                        </div>--%>
        </div>


        <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <telerik:RadWindow runat="server" ID="rwEntryDetails" Modal="true" Width="800px"
        Height="650px" KeepInScreenBounds="True" Skin="Bootstrap" VisibleStatusbar="False"
        ReloadOnShow="true" Behaviors="Close" Title="" EnableEmbeddedSkins="false" Style="overflow: hidden !important;">
        <ContentTemplate>
            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                <div class="panel panel-yellow">
                    <div class="panel-heading">Product List For Stock Change</div>
                    <div class="panel-body pan">
                        <div class="form-body pal">

                            <br />
                            <div id="dv_prodctlist" runat="server">

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
                                                            TableLayout="Fixed" HierarchyDefaultExpanded="False">
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
                                                                <%--<telerik:GridBoundColumn DataField="Cat_Name" HeaderText="Product Group" SortExpression="Cat_Name"
                                                                    ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                    ShowSortIcon="false" ReadOnly="true">
                                                                </telerik:GridBoundColumn>--%>

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
                                    <asp:Button ID="btn_ProductIngredient" class="btn btn-primary" runat="server" Text="OK" ToolTip="Click here Add Product For Stock change" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_ProductIngrdientCancel" class="btn btn-primary" runat="server" Text="Cancel" ToolTip="Click here Cancel" />
                                </div>
                            </div>


                            <div id="dv_wizard" runat="server" visible="false">
                                <div class="col-md-6">
                                    <label>
                                        Name &nbsp;
                                       <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hf_Product_Id" runat="server" Visible="false" />
                                        <asp:HiddenField ID="hf_Category_Id" runat="server" Visible="false" />
                                        <asp:HiddenField ID="hf_Unit_Id" runat="server" Visible="false" />
                                        <asp:HiddenField ID="hf_Row_Id" runat="server" Visible="false" />
                                    </label>
                                </div>
                                 <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label enabled="false">
                                        Base &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <%-- <asp:Label ID="txt_baseunit" runat="server"></asp:Label>--%>
                                        <asp:TextBox Enabled="false" ID="txt_baseunit" runat="server"></asp:TextBox>
                                        <%--<asp:TextBox ID="txt_baseunit" runat="server" Enabled="false"></asp:TextBox>--%>
                                       <%--<asp:TextBox Datafield="base_unit" ID="TextBox1" runat="server"></asp:TextBox>--%></label>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Qty &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:TextBox  ID="txt_Qty" runat="server"></asp:TextBox></label>
                                </div>
                                  
                                <div class="clearfix"></div>
                                <br />
                                <div style="padding-top: 10px; text-align: center;">
                                    <asp:Button ID="btn_Cancel" class="btn btn-primary" runat="server" Text="Cancel" ToolTip="Click here Cancel" />&nbsp;&nbsp;
                                     <asp:Button ID="btn_Next" class="btn btn-primary" runat="server" Text="Next" ToolTip="Click here Next" />&nbsp;&nbsp;
                                     <asp:Button ID="btn_Save" class="btn btn-primary" runat="server" Text="Save" ToolTip="Click here Save" Visible="false" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


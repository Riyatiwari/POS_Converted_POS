<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Customer_Master.aspx.vb" Inherits="Customer_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Customer Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Customer_List.aspx">Customer List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Customer Master</li>
        </ol>
    </div>
    <style>

            .overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(0, 0, 0, 0.5); /* Semi-transparent background */
        color: #fff;
        display: none; /* Hidden by default */
        justify-content: center;
        align-items: center;
        z-index: 9999; /* Ensure it is on top */
    }
    .overlay .message {
        font-size: 20px;
        text-align: center;
    }
        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgb(0, 0, 0);
            background-color: rgba(0, 0, 0, 0.4);
        }


        .modal-content {
            background-color: #fefefe;
            margin: 4% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 27%;
            height: 57%;
            border-radius: 10px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.3);
            animation: fadeIn 0.5s;
        }

        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }


            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }

        .label {
            display: block;
            margin-bottom: 10px;
            font-weight: bold;
        }

        .input-field {
            width: 100%;
            padding: 10px;
            margin: 10px 0 20px 0;
            display: inline-block;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .modal-button {
            width: 100%;
            background-color: #207cb8;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

            .modal-button:hover {
                background-color: #326ebc;
            }

        @keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }
    </style>
    <script>
        function OnClientFileSize() {
            const fi = document.getElementById("<%=fileupload.ClientID%>");
            const fsize = fi.files.item(0).size;
            const file = Math.round((fsize / 1024));
            if (file > 5) {
                alert('Image size is more than 5kb.Compress image file size and Try again.');

                document.getElementById("<%=fileupload.ClientID%>").value = '';
            }
        }
    </script>

    <%--     <script type="text/javascript">
         function openModal() {
             document.getElementById('paymentModal').style.display = 'block';
         }

         function closeModal() {
             document.getElementById('paymentModal').style.display = 'none';
         }

         function submitAmount() {
             var amount = document.getElementById('amountInput').value;

             var xhr = new XMLHttpRequest();
             xhr.open('POST', 'YourPageName.aspx/SubmitAmount', true);
             xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
             xhr.setRequestHeader('Accept', 'application/json');
             xhr.onreadystatechange = function () {
                 if (xhr.readyState === 4 && xhr.status === 200) {
                     alert('Amount submitted successfully');
                     closeModal();
                 }
             };
             xhr.send(JSON.stringify({ amount: amount }));
         }
</script>--%>

    <script type="text/javascript">
    <%-- function openModal() {
        document.getElementById('paymentModal').style.display = 'block';
        document.getElementById('<%= txtExpDate.ClientID %>').style.display = 'none';
    }

    function closeModal() {
        document.getElementById('paymentModal').style.display = 'none';
        document.getElementById('<%= txtExpDate.ClientID %>').style.display = 'block';
    }

    function submitAmount() {
        var amount = document.getElementById('amountInput').value;

        var xhr = new XMLHttpRequest();
        xhr.open('POST', 'Customer_Master/SubmitAmount', true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
        xhr.setRequestHeader('Accept', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                alert('Amount submitted successfully');
                closeModal();
            }
        };
        xhr.send(JSON.stringify({ amount: amount }));
    }--%>

        function openModal() {
            document.getElementById('myModal').style.display = 'block';
            document.getElementById('<%= txtExpDate.ClientID %>').style.display = 'none';

            // Load balance when modal opens
            loadBalance();
        }

        function closeModal() {
            document.getElementById('myModal').style.display = 'none';
            document.getElementById('<%= txtExpDate.ClientID %>').style.display = 'block';
        }

        function submitAmount() {
            var amount = document.getElementById('amountInput').value;
            var paymentMethod = parseInt(document.getElementById('paymentMethod').value, 10); // Ensure the value is sent as a number

            var xhr = new XMLHttpRequest();
            xhr.open('POST', 'Customer_Master.aspx/SubmitAmount', true);
            xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
            xhr.setRequestHeader('Accept', 'application/json');
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    var response = JSON.parse(xhr.responseText);
                    alert(response.d);
                    if (response.d === "Record inserted successfully") {
                        closeModal();
                    }
                }
            };
            xhr.send(JSON.stringify({ amount: parseFloat(amount), paymentType: paymentMethod }));
        }
        function loadBalance() {
            var xhr = new XMLHttpRequest();
            xhr.open('POST', 'Customer_Master.aspx/GetBalance', true); // Assuming you have a method to get balance
            xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
            xhr.setRequestHeader('Accept', 'application/json');
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    var response = JSON.parse(xhr.responseText);
                    document.getElementById('balanceLabel').textContent = response.d;
                }
            };
            xhr.send(null);
        }

        function showOverlay() {
            // Show the overlay
            document.getElementById('loadingOverlay').style.display = 'flex';

            // Disable all inputs and buttons
            var elements = document.querySelectorAll('input, button, select, textarea');
            elements.forEach(function (el) {
                el.disabled = true;
            });
        }

        function hideOverlay() {
            // Hide the overlay
            document.getElementById('loadingOverlay').style.display = 'none';

            // Enable all inputs and buttons
            var elements = document.querySelectorAll('input, button, select, textarea');
            elements.forEach(function (el) {
                el.disabled = false;
            });
        }

        document.getElementById('btnpay').addEventListener('click', function () {
            showOverlay();

            // Simulate processing (replace this with actual processing logic)
            setTimeout(function () {
                hideOverlay();
            }, 3000); // Adjust timeout as needed
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

            <div class="panel panel-yellow">
                <div class="panel-heading">Customer Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>First Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <asp:TextBox ID="txtFName" CssClass="form-control" Skin="" runat="server" placeholder="First Name" Width="100%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Customer name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFName"
                                            ErrorMessage="Enter only numeric and characters in first name" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Last Name </label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox ID="txtLName" CssClass="form-control" Skin="" runat="server" placeholder="Last Name" Width="100%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLName"
                                            ErrorMessage="Enter only numeric and characters in last name" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Email </label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" Skin="" runat="server" placeholder="Email" Width="100%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="valid" Display="None"
                                            CssClass="text-danger" ErrorMessage="Invalid email" ValidationExpression="([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Contact </label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox MinValue="0" ID="txtContact" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Contact" MaxLength="14">
                                            <%--<NumberFormat GroupSeparator="" DecimalDigits="0" />--%>
                                        </asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                            ControlToValidate="txtContact"
                                            ErrorMessage="Invalid contact no" ForeColor="Red" Display="None"
                                            ValidationExpression="^[0-9]*$" ValidationGroup="valid">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Other Id </label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox ID="txtOther_id" CssClass="form-control" Skin="" runat="server" placeholder="Other Id" Width="100%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOther_id"
                                            ErrorMessage="Enter only numeric and characters in other id" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Bonus Point</label>
                                        <div class="clearfix"></div>
                                        <b>
                                            <asp:Label ID="lblBonus" runat="server" Text="0" CssClass="form-control"></asp:Label></b>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Profile </label>
                                        <div class="clearfix"></div>
                                        <asp:DropDownList ID="radprofile" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Credit Account</label>&nbsp&nbsp&nbsp
                                     <asp:CheckBox ID="chkCredit" runat="server" />
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Card Number</label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox ID="txt_cardNumber" CssClass="form-control" Skin="" runat="server" placeholder="Card Number" Width="100%"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                        ControlToValidate="txt_cardNumber"
                                        ErrorMessage="Invalid card number" ForeColor="Red" Display="None"
                                        ValidationExpression="^[0-9]*$" ValidationGroup="valid">
                                    </asp:RegularExpressionValidator>--%>
                                    </div>


                                    <div class="col-md-6">
                                        <label>Balance</label>
                                        <div class="clearfix"></div>
                                       <asp:TextBox Enabled="false" CssClass="form-control" ID="btnblnc" runat="server"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                        ControlToValidate="txt_cardNumber"
                                        ErrorMessage="Invalid card number" ForeColor="Red" Display="None"
                                        ValidationExpression="^[0-9]*$" ValidationGroup="valid">
                                    </asp:RegularExpressionValidator>--%>
                                    </div>
                                      
                                    <div class="clearfix"></div>
                                    <br />



                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-6" style="margin-top: 10px;">
                                                    <div class="clearfix"></div>
                                                    <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="120px" />
                                                </div>
                                                <div class="clearfix"></div>
                                                <br />
                                                <div class="col-md-12" style="margin-top: 10px;">
                                                    <div class="clearfix"></div>
                                                    <br />
                                                    <label style="float: left;">Upload :  &nbsp;</label>
                                                    <asp:FileUpload ID="fileupload" runat="server" onChange="OnClientFileSize();" />
                                                    <%-- <asp:Button ID="btnUpload" Text="Upload" runat="server" />--%>

                                                    <%-- <telerik:RadAsyncUpload ID="fileupload" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandler"
                                            MaxFileSize="5000" OnClientValidationFailed="OnClientFileSize">
                                            <FileFilters>
                                                <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                            </FileFilters>
                                        </telerik:RadAsyncUpload>
                                        <i style="font-weight: lighter">
                                            <asp:Label ID="Label2" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label><br />
                                            <asp:Label ID="Label1" runat="server" Text="File size should not exceed 5Kb"></asp:Label></i>
                                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />--%>
                                                    <div class="clearfix"></div>
                                                    <asp:HiddenField ID="hdlogo" runat="server" />
                                                    <div class="col-md-6" style="margin-top: 10px;">
                                                        <div class="clearfix"></div>
                                                        <asp:Image ID="Image2" runat="server" Visible="false" Height="50px" Width="150px" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="col-md-12">
                                    <label>Address</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtAddress" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Address" Rows="4" Width="100%"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Country </label>
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="radCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radCountry_SelectedIndexChanged" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label>State </label>
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="radState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radState_SelectedIndexChanged" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>City</label><div class="clearfix"></div>
                                    <asp:DropDownList ID="radCity" runat="server" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </asp:DropDownList>
                                </div>

                              
                                <div class="col-md-6">
                                    <label>Postal Code </label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtPostal" CssClass="form-control" Skin="" runat="server" placeholder="Postal Code" Width="100%"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPostal"
                                        ErrorMessage="Enter only numeric and characters in postal code" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                </div>
                                 
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                  
                                    <div class="col-md-6">
                                        <label>Expiry Date</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtExpDate" runat="server" DateInput-EmptyMessage="Expiry date" MinDate="01/01/1000" MaxDate="01/01/3000" Width="100%" Skin="MetroTouch">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </div>
 
                               
                               
                           
                                <div class="col-md-6">
                                    <label>Price Level</label><div class="clearfix"></div>
                                    <asp:DropDownList ID="ddlPriceLevel" runat="server" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>

                        <div class="row">
                        </div>
                        <div class="row">
                        </div>

                    </div>
                    <%--              <telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="RadPanelBar1" Width="100%" ExpandMode="MultipleExpandedItems">
                        <Items>
                <telerik:RadPanelItem Expanded="false" Text="Creadit Cart List">
                    <ContentTemplate>
                        <div class="form-body pal">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                              <div class="clearfix"></div>
                                            
                                        
                                            <asp:GridView ID="GridView1" runat="server">
                                                <Columns>
                                                   <asp:TemplateField HeaderText="CraditCard No.">

                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSizeName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                                                MaxLength="12" Text='<%# Eval("CardNumber") %>'
                                                                AutoPostBack="true"> 
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CraditCard Name">

                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSizeName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                                                MaxLength="15" Text='<%# Eval("full_name") %>'
                                                                AutoPostBack="true"> 
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                   
                                                    <asp:TemplateField HeaderText="Action">
                                                     
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                            <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                 CommandArgument='<%#Eval("row_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
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
                  </Items>
                    </telerik:RadPanelBar>  --%>
                    <div id="loadingOverlay" class="overlay">
    <div class="message">Processing, please wait...</div>
</div>
                    <div id="myModal" class="modal">
                        <div class="modal-content">
                            <span class="close" onclick="closeModal()">&times;</span>
                            <label for="amountInput">Enter Amount:</label>
                            <input type="text" id="amountInput" class="input-field" />

                            <label for="paymentMethod">Payment Method:</label>
                            <select id="paymentMethod" class="input-field">
                                <option value="0">Cash</option>
                                <option value="1">Card</option>
                            </select>

                            <label for="balanceLabel">Balance:</label>
                            <asp:TextBox CssClass="input-field" placeholder="Balance" Enabled="false" ID="txtbalance" runat="server"></asp:TextBox>
                            <span id="balanceLabel"></span>
                            <button id="btnpay" class="modal-button" onclick="submitAmount()">Submit</button>
                        </div>

                    </div>

                    <%--<div id="paymentModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="closeModal()">&times;</span>
        <h2>Enter Amount</h2>
        <input type="text" id="amountInput" class="form-control" />
        <button onclick="submitAmount()" class="btn btn-primary">Submit</button>
    </div>
</div>--%>

                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnpay" class="btn btn-primary" runat="server" Text="Pay" ValidationGroup="valid" ToolTip="Click here to Add Credit Amount" OnClientClick="openModal(); return false;" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnlist" class="btn btn-primary" OnClick="btnlist_Click" runat="server" Text="Show transactions" ValidationGroup="valid" ToolTip="Click here to Show Credit List" />&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Customer Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Customer Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Customer Details" />
                    </div>
                </div>

            </div>
        </telerik:RadAjaxPanel>
        <%--</telerik:RadAjaxPanel>--%>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


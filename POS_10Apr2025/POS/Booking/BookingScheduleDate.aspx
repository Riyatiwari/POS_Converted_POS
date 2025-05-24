<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BookingScheduleDate.aspx.vb"
    Inherits="BookingEasy_BookingScheduleDate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href='<%# ResolveUrl("~/Theme/assets/bootstrap/css/bootstrap.min.css") %>'
        rel="stylesheet" type="text/css" />
</head>
<body>
    <link rel="stylesheet" href="../Theme/assets/jquery-ui/jquery-ui.min.css">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm2" runat="server">
    </asp:ScriptManager>
    <div id="targetDiv">
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box">
                <div class="box-content">
                    <div class="form-horizontal">
                        <div>
                            <div>
                                <asp:Label ID="lblHdr" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div>
                                <telerik:RadDropDownList ID="drpYear" runat="server" Skin="MetroTouch" AutoPostBack="True"
                                    DropDownHeight="200px" DefaultMessage="- Select Year -">
                                </telerik:RadDropDownList>
                                <telerik:RadDropDownList ID="drpMonth" runat="server" Skin="MetroTouch" AutoPostBack="True"
                                    DropDownHeight="200px" DefaultMessage="- Select Month -">
                                </telerik:RadDropDownList>
                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
                            </div>
                        </div>
                        <div>
                            <asp:Panel ID="pnlDate" runat="server" Visible="false">
                                <div>
                                    <asp:GridView ID="gvDates" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                        DataKeyNames="BookingScheduleDateId" CssClass="table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ScheduleDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle Width="40px" />
                                                <ControlStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDayName" runat="server" Text='<%# Convert.ToDateTime(Eval("ScheduleDate")).ToString("dddd") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle Width="40px" />
                                                <ControlStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available?">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsAvailableShow" runat="server" Checked='<%# Eval("IsAvailable") %>'
                                                        Enabled="false" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chkIsAvailable" runat="server" Checked='<%# Eval("IsAvailable") %>' />
                                                </EditItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle Width="40px" />
                                                <ControlStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartTime" runat="server" Text='<%# Eval("StartTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTimePicker ID="tpStartTime" runat="server" EnableTyping="true" Width="100%"
                                                        Skin="MetroTouch" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                        TimeView-TimeFormat="HH:mm" SelectedTime='<%# Eval("StartTime") %>'>
                                                    </telerik:RadTimePicker>
                                                    <asp:RequiredFieldValidator ID="reqStartTime" runat="server" ControlToValidate="tpStartTime"
                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle Width="40px" />
                                                <ControlStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndTime" runat="server" Text='<%# Eval("EndTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTimePicker ID="tpEndTime" runat="server" EnableTyping="true" Width="100%"
                                                        Skin="MetroTouch" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                        TimeView-TimeFormat="HH:mm" SelectedTime='<%# Eval("EndTime") %>'>
                                                    </telerik:RadTimePicker>
                                                    <asp:RequiredFieldValidator ID="reqEndTime" runat="server" ControlToValidate="tpEndTime"
                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle Width="40px" />
                                                <ControlStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No Of Cover">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoOfCover" runat="server" Text='<%# Eval("NumberOfCover") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtNoOfCover" runat="server" Height="32px" Type="Number"
                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="50px" DataType="Integer"
                                                        CssClass="form-control tg" Text='<%# Eval("NumberOfCover") %>'>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="reqNoOfCover" runat="server" ControlToValidate="txtNoOfCover"
                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                                <ControlStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Duration(in Minutes)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceDuration" runat="server" Text='<%# Eval("ServiceDuration") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%--<asp:TextBox ID="txtServiceDuration" runat="server" class="form-control tg" Text='<%# Eval("ServiceDuration") %>'></asp:TextBox>--%>
                                                    <telerik:RadNumericTextBox ID="txtServiceDuration" runat="server" Height="32px" Type="Number"
                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="50px" DataType="Integer"
                                                        CssClass="form-control tg" Text='<%# Eval("ServiceDuration") %>'>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="reqServiceDuration" runat="server" ControlToValidate="txtServiceDuration"
                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                                <ControlStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time Span(in Minutes)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTimeSpan" runat="server" Text='<%# Eval("TimeSpan") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%--<asp:TextBox ID="txtTimeSpan" runat="server" class="form-control tg isNumeric" Text='<%# Eval("TimeSpan") %>'></asp:TextBox>--%>
                                                    <telerik:RadNumericTextBox ID="txtTimeSpan" runat="server" Height="32px" Type="Number"
                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="50px" DataType="Integer"
                                                        CssClass="form-control tg" Text='<%# Eval("TimeSpan") %>'>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="reqTimeSpan" runat="server" ControlToValidate="txtTimeSpan"
                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                                <ControlStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadDropDownList ID="drpPaymentType" Skin="MetroTouch" runat="server" DefaultMessage="--Select--">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="1" Text="Percentage Of Total Bill" />
                                                            <telerik:DropDownListItem Value="2" Text="Open Deposit" />
                                                            <telerik:DropDownListItem Value="3" Text="Deposit Amount" />
                                                            <telerik:DropDownListItem Value="4" Text="Deposit Per Cover" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="reqPaymentType" runat="server" ControlToValidate="drpPaymentType"
                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic"
                                                        InitialValue="">
                                                    </asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                                <ControlStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deposit %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercentage" runat="server" Text='<%# Eval("DepositPercentage") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%--<asp:TextBox ID="txtPercentage" runat="server" class="form-control tg" Text='<%# Eval("DepositPercentage") %>'></asp:TextBox>--%>
                                                    <telerik:RadNumericTextBox ID="txtPercentage" runat="server" Height="32px" Type="Number"
                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="2" Width="50px" CssClass="form-control tg"
                                                        Text='<%# Eval("DepositPercentage") %>'>
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle Width="40px" />
                                                <ControlStyle Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deposit Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("DepositAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%--<asp:TextBox ID="txtAmount" runat="server" class="form-control tg" Text='<%# Eval("DepositAmount") %>'></asp:TextBox>--%>
                                                    <telerik:RadNumericTextBox ID="txtAmount" runat="server" Height="32px" Type="Number"
                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="2" Width="50px" CssClass="form-control tg"
                                                        Text='<%# Eval("DepositAmount") %>'>
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle Width="40px" />
                                                <ControlStyle Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CausesValidation="false">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/Icons/edit.png" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" ValidationGroup="Schedule">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="Images/Icons/save.png" />
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="Images/Icons/cancel.png" />
                                                    </asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href='<%# "RecurringSchedule.aspx?BookingScheduleDateId=" + Eval("BookingScheduleDateId").ToString() %>'
                                                        id="aRecurring" runat="server" class="recurring-sch" title="Recurring"><asp:Image ID="imgRecurring" runat="server" Height="16" Width="16" ImageUrl="Images/Icons/Recurring.png" /> </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                                <asp:Label ID="lblMessage" runat="server" Text="No Date(s) Found For Selected Period"
                                    Font-Bold="true"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>--%>
    <%--<link rel="stylesheet" href="../css/jquery-ui.css">--%>
    <script src="../Theme/js/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../Theme/js/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        $('.recurring-sch').click(function () {   //bind handlers
            var url = $(this).attr('href');
            showDialog(url);

            return false;
        });

        $("#targetDiv").dialog({  //create dialog, but keep it closed
            autoOpen: false,
            height: 500,
            width: 550,
            modal: true
        });

        function showDialog(url) {  //load content and open dialog
            $("#targetDiv").load(url);
            $("#targetDiv").dialog("open");
        }
    </script>
    </form>
</body>
</html>

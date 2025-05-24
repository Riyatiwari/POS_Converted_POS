<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RecurringSchedule.aspx.vb"
    Inherits="BookingEasy_RecurringSchedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Datepicker/css/datepicker.css") %>' />
</head>
<body>
    <form id="form3" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div>
                    Recurring Schedule
                </div>
                <div>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
                </div>
                <br />
                <div>
                    <table border="1px solid black">
                        <tr>
                            <td>
                                <asp:Label ID="lblStartTime" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEndTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNoOfCover" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblServiceDuration" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTimeSpan" runat="server"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div style="width: 100%; float: left">
                    <div style="width: 20%; float: left">
                        <table border="1px solid black">
                            <tr>
                                <td>
                                    <table style="width: 100px; margin-left: 10px;">
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdoDaily" runat="server" Text="Daily" GroupName="SchedulePattern"
                                                    Checked="true" CssClass="recurrencePref" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdoWeekly" runat="server" Text="Weekly" GroupName="SchedulePattern" />
                                                <asp:Label ID="lblWeekMsg" runat="server" CssClass="recurrencePref"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdoMonthly" runat="server" Text="Monthly" GroupName="SchedulePattern" />
                                                <asp:Label ID="lblMonthMsg" runat="server" CssClass="recurrencePref"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdoYearly" runat="server" Text="Yearly" GroupName="SchedulePattern" />
                                                <asp:Label ID="lblYearMsg" runat="server" CssClass="recurrencePref"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 80%; float: left; padding-left: 20px;  " id="divDays">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkMonday" runat="server" Text="Monday" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkTuesday" runat="server" Text="Tuesday" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkWednesday" runat="server" Text="Wednesday" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkThursday" runat="server" Text="Thursday" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkFriday" runat="server" Text="Friday" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkSaturday" runat="server" Text="Saturday" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkSunday" runat="server" Text="Sunday" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <br />
                <table border="1px solid black" style="width: 100%;">
                    <tr>
                        <td>
                            <div>
                                <table style="margin-left: 10px;">
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdoDateRange" runat="server" Text="Range" Checked="true" GroupName="ScheduleRange" />
                                            <div>
                                                From
                                                <asp:TextBox class="date-picker" ID="dtFrom" runat="server" date-format="dd/mm/yyyy"
                                                    Width="120px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqFrom" runat="server" ErrorMessage="*" Display="Dynamic"
                                                    ControlToValidate="dtFrom" ForeColor="Red" ValidationGroup="RecurranceGroup"></asp:RequiredFieldValidator>
                                                To
                                                <asp:TextBox class="date-picker" ID="dtTo" runat="server" date-format="dd/mm/yyyy"
                                                    Width="120px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqTo" runat="server" ErrorMessage="*" Display="Dynamic"
                                                    ControlToValidate="dtTo" ForeColor="Red" ValidationGroup="RecurranceGroup"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdoOccurance" runat="server" Text="Occurance" GroupName="ScheduleRange" />
                                            <div>
                                                No Of Occurance
                                                
                                                <asp:TextBox ID="txtOccurance" runat="server" Width="50px" MaxLength="2" Text="0"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqNoOfOccurance" runat="server" ErrorMessage="*"
                                                    Display="Dynamic" ControlToValidate="txtOccurance" ForeColor="Red" ValidationGroup="RecurranceGroup"></asp:RequiredFieldValidator>
                                                <%--<asp:RangeValidator ID="rangeOccurance" runat="server" ErrorMessage="Value must be between 1 to 99."
                                                    Display="Dynamic" ControlToValidate="txtOccurance" MinimumValue="1" MaximumValue="99"
                                                    ForeColor="Red" ValidationGroup="RecurranceGroup"></asp:RangeValidator>--%>
                                                <asp:CompareValidator ID="compNoOfOccurance" runat="server" ErrorMessage="Only Numeric Value Allow."
                                                    Display="Dynamic" ControlToValidate="txtOccurance" Operator="DataTypeCheck" Type="Integer"
                                                    ForeColor="Red" ValidationGroup="RecurranceGroup"></asp:CompareValidator>
                                                Start From
                                                <asp:TextBox class="date-picker" ID="dtOccuStartFrom" runat="server" date-format="dd/mm/yyyy"
                                                    Width="120px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqOccuranceStart" runat="server" ErrorMessage="*"
                                                    Display="Dynamic" ControlToValidate="dtOccuStartFrom" ForeColor="Red" ValidationGroup="RecurranceGroup"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:Button ID="btnRecurring" runat="server" Text="Create Recurring" ValidationGroup="RecurranceGroup" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="custom_progress">
                <div id="overlay_load">
                </div>
                <div id="loading">
                    <img src="Images/ajax-loader.gif " alt="" />
                    <br />
                    Please Wait...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript" src='<%= ResolveUrl("~/Datepicker/js/bootstrap-datepicker.js") %>'></script>
    <script type="text/javascript">
        $('.date-picker').datepicker({
            format: "dd/mm/yyyy",
            startDate: "<%= strScheduleStartDate %>",
            endDate: "<%= strScheduleEndDate %>"
        });

        $("input[name=SchedulePattern]:radio").on('change', function () {
            if ($(this).is(':checked') && this.id == "rdoWeekly") {
                $("#divDays").attr('style', 'width: 80%; float: left; padding-left: 20px;display');
            }
            else {
                $("#divDays").attr('style', 'display:none');
            }
        });
    </script>
    </form>
</body>
</html>

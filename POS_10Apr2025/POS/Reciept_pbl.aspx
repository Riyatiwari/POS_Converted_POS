<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="Reciept_pbl.aspx.vb" Inherits="Reciept_pbl" %>
<%--<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Scheduler_BarExchange.aspx.vb" Inherits="Scheduler_BarExchange" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Receipt Page</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            font-size: 14px;
            margin: 0;
            padding: 0;
            background-color: #f9f9f9;
        }

        #receiptContainer {
            width: 100%;
            max-width: 800px;
            margin: 20px auto;
            padding: 20px;
            background-color: #fff;
            border: 1px solid #ddd;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .receipt-header, .receipt-footer {
            text-align: center;
            background-color: #f1f1f1;
            padding: 10px 0;
        }

        .receipt-header img, .receipt-footer img {
            max-width: 100%;
            height: auto;
        }

        .receipt-content {
            padding: 20px;
        }

        .receipt-content h1 {
            font-size: 24px;
            margin: 0 0 10px;
        }

        .receipt-content p {
            margin: 0 0 10px;
        }

        .receipt-content table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }

        .receipt-content table, .receipt-content th, .receipt-content td {
            border: 1px solid #ddd;
            padding: 8px;
        }

        .receipt-content th {
            background-color: #f1f1f1;
            text-align: left;
        }

        @media screen and (max-width: 600px) {
            .receipt-content {
                padding: 10px;
            }

            .receipt-content h1 {
                font-size: 20px;
            }

            .receipt-content table, .receipt-content th, .receipt-content td {
                font-size: 12px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="receiptContainer" runat="server">
           
        </div>
    </form>
</body>
</html>



 <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
Scheduler_BarExchange
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">--%>
    <%--<div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">BarStock Exchange</li>
        </ol>
    </div>
    <script type="text/javascript">
         
 
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />



         <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow" runat="server" id="div5">
                <div class="panel-heading">Bar Stock Exchange</div>
       

                    <div class="form-actions text-center pal" id="Div6" runat="server">
                       <%--  <asp:Button ID="ButtonCategories" runat="server" Text="Send Categories"  />--%>


<%--                  <asp:Button ID="ButtonCategories" class="btn btn-primary" runat="server" Text="Send Categories" OnClick="ButtonCategories_Click"  />
                <br />
                <br />
                        
                        <asp:Button ID="ButtonItems" class="btn btn-primary" runat="server" Text="Send Items" OnClick="ButtonItems_Click"  />
--%>

              
                      <%--<asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Save"  />&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="Button2" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Company Details" />--%>
<%--                    </div>

                </div>
 </telerik:RadAjaxPanel>
            </div>--%> 


   <%-- <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow" runat="server" id="divCompany">
                <div class="panel-heading">Sync</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                             

                                     <div class="col-sm-6 ">
                                   <label> Date :</label> &nbsp;
                                <telerik:RadDatePicker ID="txtForDate" style="width:160px;margin-top:20px;"  runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch"   AutoPostBack="true">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday" >
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                 
                                <%--    To Date : &nbsp;
                                <telerik:RadDatePicker ID="txtTo" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday" >
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>--%>
                                
                                    <%--&nbsp
                                    <div class="col-sm-6 col-sm-6 ">
                                        <label>  Location  &nbsp;  </label>
                                        <telerik:RadComboBox ID="radLocation" ValidationGroup="register" runat="server" ExpandDirection="Down" Width="100%">
                                        </telerik:RadComboBox>
                                    </div>

                              </div>
                           
                            </div>
                         
                        </div>
                    </div>

                    <div class="form-actions text-right pal" id="Div_Button" runat="server">
                       
              
                      <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save"  />&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Company Details" />
                    </div>

                </div>
 </telerik:RadAjaxPanel>
            </div>--%> 


<%--     <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow" runat="server" id="div1">
                <div class="panel-heading">Sales And Price</div>
                <div class="panel-body pan">

                   
                    </div>

                    <div class="form-actions text-center pal" id="Div2" runat="server">
                       
               <asp:Button ID="sales" type="submit" class="btn btn-primary" runat="server" Text="Send Sales" OnClick="sales_Click" />&nbsp;&nbsp;&nbsp;
                <br />
            
            
                <br />
                        
                        <asp:Button ID="price" type="submit" class="btn btn-primary" runat="server" Text="Get Price" OnClick="price_Click" />&nbsp;&nbsp;&nbsp;
              
                    </div>

                </div>
 </telerik:RadAjaxPanel>
            </div>--%>

 
<%--</asp:Content>--%>


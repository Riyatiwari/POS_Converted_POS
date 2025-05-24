<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Switch_Account.aspx.vb" Inherits="Switch_Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   Switch Account
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

<style>
        body {
            background-color: #f8f9fa; 
        }

        #psummary {
            width: 80%;
            margin: auto;
            background-color: #ffffff; 
            border: 1px solid #dee2e6; 
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        #psummary th, #psummary td {
            text-align: center;
            vertical-align: middle;
            padding: 15px; 
        }

        #psummary thead th {
            background-color: #207cb8;
            color: #ffffff; 
        }

        #psummary tbody tr {
            height: 60px; 
        }

        #psummary tbody tr:hover {
            background-color: #f2f2f2;
        }

        #psummary td {
            border-bottom: 1px solid #dee2e6; 
        }

        #psummary tbody td:last-child {
            border: none; 
        }

        #psummary th, #psummary td {
            border-right: 1px solid #dee2e6; 
        }

        #psummary th:last-child, #psummary td:last-child {
            border-right: none; 
        }
        #searchInput {
            margin-bottom: 20px;
            padding: 10px;
            width: 16%;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            margin-left: 74%;
        }
    </style>
      <script>
        $(document).ready(function() {
            $("#searchInput").on("keyup", function() {
                var value = $(this).val().toLowerCase();
                $("#psummary tbody tr").filter(function() {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <input type="text" id="searchInput" placeholder="🔎Search for stores..." />
  
     <%--<input type="text" id="searchInput" placeholder="Search for stores..." style="margin-bottom: 20px; padding: 10px; width: 80%;">--%>
    <table class="table table-bordered" id="psummary" width="100%" cellspacing="0">
        <asp:Repeater ID="rdstaff" runat="server">
        <HeaderTemplate>
                <thead>
                    <tr>
                         <th style="width: 10%;">#</th>
                        <th style="width: 70%;">Stores</th>
                        <th style="width: 70%;" hidden>StoreUUID</th>
                        <th style="width: 30%;">Switch Account</th>
                    </tr>
                </thead>
                <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <%-- <td><%# Container.ItemIndex + 1 %></td>--%>
                   <%-- <td style="background-color: #ffffff;"><%#Eval("store_name") %></td>--%>
        
                   <%-- <td style="background-color: #ffffff;">
                        <asp:LinkButton ID="switchAcoount" runat="server" CausesValidation="False" ToolTip="Switch Account" OnClick="switchAcoount_Click"
                            CommandName="Switch" OnClientClick="return confirm('Are you sure you want to switch to this account?')">
                            <i class="fa fa-exchange fa-lg"></i>
                           
                        </asp:LinkButton>
                    </td>--%>

                    <td><%# Container.ItemIndex + 1 %></td>
        <td style="background-color: #ffffff;">
            <asp:Label ID="storeNameLabel" runat="server" Text='<%# Eval("store_name") %>'></asp:Label>
           
        </td>
        <td style="background-color: #ffffff;"  hidden>
                     <asp:Label ID="storeUUIDLabel" runat="server" Text='<%# Eval("Store_UUID") %>'></asp:Label>
           </td>
        <td style="background-color: #ffffff;">
            <asp:LinkButton ID="switchAccount" runat="server" CausesValidation="False" ToolTip="Switch Account" OnClick="switchAccount_Click"
                CommandName="Switch" OnClientClick="return confirm('Are you sure you want to switch to this account?')">
                <i class="fa fa-exchange fa-lg"></i>
               
            </asp:LinkButton>
        </td>
                    <%--<td>
                   <asp:LinkButton ID="switchAccount" runat="server" CausesValidation="False" ToolTip="Switch Account" OnClick="switchAccount_Click"
    CommandName="Switch" OnClientClick='<%# "return confirm(\Are you sure you want To switch To this account?\);" %>'
    CommandArgument='<%# Eval("Store_UUID").ToString() %>'>
    <i class="fa fa-exchange fa-lg"></i>
</asp:LinkButton>
                    </td>--%>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
            </FooterTemplate>
        </asp:Repeater>
    </table>





</asp:Content>








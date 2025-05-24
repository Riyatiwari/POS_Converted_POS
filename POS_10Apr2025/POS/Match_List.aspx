<%@ Page Title="Product List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Match_List.aspx.vb" Inherits="Match_List" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Match List  
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
     <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">match List</li>
        </ol>
    </div>
    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

  

<script type="text/javascript" src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.updateCheckbox').change(function () {
            var row = $(this).closest('tr');
            var txtDepartment = row.find('.txtDepartment').val();
            var hidCatID = row.find('.hidCatID').val();
      
            HandleCheckBoxChangeServer(txtDepartment, hidCatID);
        });
    });
</script>





 


</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


     <asp:Panel runat="server" ID="PnlPsummary">
         <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
             <asp:Repeater ID="rdProduct" runat="server">
                 <HeaderTemplate>
                     <thead>
                         <tr>
                             <th>Groups Cats</th>

                             <th>Department</th>
                             <th>update</th>

                         </tr>
                     </thead>
                     <tbody>
                 </HeaderTemplate>
                <ItemTemplate>
        <tr>
            <td style="background-color: #ffffff;"><%# Eval("category_name") %></td>
            <td style="background-color: #ffffff;" hidden><%# Eval("cat_ID") %></td>
            <td>
                <asp:TextBox ID="txtDepartment" runat="server" Text='<%# Eval("department_name") %>'></asp:TextBox>
                <asp:HiddenField ID="hiddepID" runat="server" Value='<%# Eval("dep_ID") %>' />
            </td>
            <td>
               <asp:CheckBox ID="chkUpdate" runat="server" Text="Update" CssClass="updateCheckbox" />
            </td>
        </tr>
    </ItemTemplate>
                 <FooterTemplate>


                     <tr>
                         <td colspan="4">
                             <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
                         </td>
                     </tr>
                     </tbody>
                            
                 </FooterTemplate>
             </asp:Repeater>
         </table>
     </asp:Panel>
                            




  </asp:Content>




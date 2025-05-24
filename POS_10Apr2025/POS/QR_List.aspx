<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="QR_List.aspx.vb" Inherits="QR_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    QR List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">QR List</li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>


    <script type="text/javascript">

        $(document).ready(function () {


            $('#Lsummary thead tr').clone(true).appendTo('#Lsummary thead');

            Grid();

        });

        function Grid() {


            $("#Lsummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#Lsummary').DataTable({


                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel', 'pdf'
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
                        'targets': [4,5], /* column index */
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

            var table1 = $('#Lsummary').DataTable();
            $('#Lsummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                if (i == 3) {
                    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                }
                else {

                    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                }

                $('input', this).on('keyup', function () {

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
     <style type="text/css">
    .commonButton {
        width: 150px;  
        height: 30px;  
       
    }
</style>
    

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
   
     <div class="col-lg-12"


      

                     <div class="row" id="divDepartment" runat="server">
                      <div class="col-lg-12">
                             <div class="panel panel-yellow">
                                 <div class="panel-heading">QR List</div>
                                 <div class="panel-body pan">
                                     <div class="form-body pal">
                                         <div class="row" id="div1" runat="server">
                                             <div class="col-lg-12">
                                                 <div class="table-responsive">
                                                     <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                                                         <thead>
                                                             <tr>
                                                                 <th>Till Name</th>
                                                                 <th>TillUUID</th>
                                                                 <th>Location Name</th>
                                                                 <th>Venue Name</th>
                                                                 <th>Text QRcode</th>
                                                                 <th>Link QRcode</th>
                                                                 <th>AllInOne QR Code</th>
                                                                 <th>Action</th>
                                                             </tr>
                                                         </thead>
                                                         <tbody>
                                                             <asp:Repeater ID="rdDepartment" runat="server" OnItemCommand="rdDepartment_ItemCommand">
                                                                 <ItemTemplate>
                                                                     <tr>
                                                                         <td style="background-color: #ffffff;"><%# Eval("machine_name") %></td>
                                                                       <asp:Label ID="lblMachineName" visible="false" runat="server" Text='<%# Eval("machine_name") %>'></asp:Label>
                                                                         <td style="background-color: #ffffff;"><%# Eval("TillUUID") %></td>
                                                                          <asp:HiddenField ID="hfmachine_id" runat="server" Value='<%#Eval("Machine_id")%>' />
                                                                          <asp:HiddenField ID="hfvenue_id" runat="server" Value='<%# Eval("venue_id") %>' />
                                                                          <asp:HiddenField ID="hflocation_id" runat="server" Value='<%# Eval("location_id") %>' />
                                                                         <td style="background-color: #ffffff;"><%# Eval("location_name") %></td>
                                                                         <td style="background-color: #ffffff;"><%# Eval("Venue_Name") %></td>
                                                                         <td style="background-color: #ffffff;">
                                                                       
                                                                             <asp:Image ID="imgQR" runat="server" ImageUrl='<%# GenerateQRCodeUrl(Eval("TillUUID")) %>' />
                                                                           </td>
                                                                             <td style="background-color: #ffffff;">
                                                                             
                                                                            <asp:Image ID="LinkQR" runat="server" ImageUrl='<%# GenerateLinkQRCodeUrl(Eval("TillUUID")) %>' /><br />
                                                                           
                                                                         </td>
                                                                         <td style="background-color: #ffffff;">
                                                                            <asp:Image ID="ModifiedQR" runat="server" ImageUrl='<%# GenerateAllinOneQRCodeUrl(Eval("TillUUID")) %>' />
                                                                        </td>
                                                                        
                                                                         <td>
                                                                             <asp:Literal ID="litGeneratedCodeTop" runat="server" Visible="true"></asp:Literal>
                                                                             <asp:Literal ID="litGeneratedCodeTop1" runat="server" Visible="true"></asp:Literal>
                                                                       <asp:Button ToolTip="Click here to Generate code" ID="GenerateCode" runat="server" Text="Generate Code" CssClass="commonButton" OnClick="GenerateCode_Click" CommandArgument='<%# Eval("TillUUID") %>' Visible='<%# Not String.IsNullOrEmpty(Eval("TillUUID").ToString()) %>' />
                                                                             <br /><asp:Literal ID="litGeneratedCodeBottom" runat="server" Visible="true"></asp:Literal>

                                                                             <%-- <asp:Button  ToolTip="Click here to Gnerate code" ID="GenerateCode" runat="server" Visible='<%# String.IsNullOrEmpty(Eval("TillUUID").ToString()) %>'  Text="Generate Code" OnClick="GenerateCode_Click" CommandArgument='<%# Eval("TillUUID") %>' />--%>
                                                                            
                                                                             <%-- <asp:Button ID="btnRegisterforQR" class="btn btn-primary" Onclick="btnRegisterforQR_Click" runat="server" Text="Register For QR"   ToolTip="Click here to Register For QR" />--%>
                                                                             <asp:Button  ToolTip="Click here to Register For QR" ID="btnGenerateQR" runat="server" Visible='<%# String.IsNullOrEmpty(Eval("TillUUID").ToString()) %>' CssClass="commonButton" Text="Generate QR" OnClick="btnGenerateQR_Click" CommandArgument='<%# Eval("TillUUID") %>' />
                                                                         </td>
                                                                        
                                                                     </tr>
                                                                 </ItemTemplate>
                                                             </asp:Repeater>
                                                         </tbody>
                                                         <tfoot>
                                                             <tr>
                                                                <th>Till Name</th>
                                                                 <th>TillUUID</th>
                                                                 <th>Location Name</th>
                                                                 <th>Venue Name</th>
                                                                 <th>Text QRcode</th>
                                                                 <th>Link QRcode</th>
                                                                   <th>AllInOne QR Code</th>
                                                                 <th>Action</th>
                                                             </tr>
                                                         </tfoot>
                                                     </table>
                                                 </div>
                                             </div>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                     </div>
                 </div>
 

  

</asp:Content>


Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Machine_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsMachine As New Controller_clsMachine()
    Dim oclsMachine_Detail As New Controller_clsMachine_Detail()
    Dim oclsFunction As New Controller_clsFunction()
    Dim mainFun_id As Integer = 0
    Dim isPopupShown As Boolean = False
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                If Session("cmp_id") = Nothing Then

                    Response.Redirect("SignIn.aspx", False)
                End If
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If

                bindprinter_Device()
                bindKeymap()
                oclsBind.BindLocation(ddLocation, Val(Session("cmp_id")))
                oclsBind.Bindtill(mastertill, Val(Session("cmp_id")), Val(Session("machine_id")))

                oclsMachine.Bindfunctionmapradio(RadCombofunction, Val(Session("cmp_id")))
                'oclsMachine.Keymap(Radkeymap, Val(Session("cmp_id")))
                If Not Session("machine_id") = Nothing Then
                    BindMachine()
                    txtCode.ReadOnly = True
                End If
                If Session("currentTillUUID").ToString() = "" OrElse Session("currentTillUUID").ToString().ToLower() = "null" OrElse Session("currentTillUUID").ToString() = "0" Then
                    btnRegisterforQR.Visible = True
                Else
                    btnRegisterforQR.Visible = False
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Machine_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Private Sub BindMachine()
        Try
            oclsMachine.cmp_id = Val(Session("cmp_id"))
            oclsMachine.machine_id = Val(Session("machine_id"))
            Dim dtMachine As DataTable = oclsMachine.Select()
            If dtMachine.Rows.Count > 0 Then
                txtCode.Text = dtMachine.Rows(0)("code").ToString
                txtMName.Text = dtMachine.Rows(0)("name").ToString
                hdmnm.Value = dtMachine.Rows(0)("name").ToString
                txtmacaddress.Text = dtMachine.Rows(0)("mac_address").ToString
                txtSerialNo.Text = dtMachine.Rows(0)("serial_no").ToString
                'txtmodel.SelectedValue = dtMachine.Rows(0)("model").ToString
                Dim modelValueFromDB As String = dtMachine.Rows(0)("model").ToString()
                txtmodel.ClearSelection()
                Dim selectedItem As ListItem = txtmodel.Items.FindByValue(modelValueFromDB)

                '  txtmodel.Text = modelValueFromDB
                ' If selectedItem IsNot Nothing Then

                ' selectedItem.Selected = True
                'End If

                For Each item As ListItem In txtmodel.Items

                    If item.Text = modelValueFromDB Then

                        item.Selected = True
                        Exit For
                    End If
                Next
                'Dim modelValueFromDB As String = dtMachine.Rows(0)("model").ToString()
                'Dim selectedItem As ListItem = txtmodel.Items.FindByValue(modelValueFromDB)
                'For Each item As ListItem In txtmodel.Items

                '    If item.Text = modelValueFromDB Then

                '        item.Selected = True
                '        Exit For
                '    End If
                'Next
                txtReciptHeader.Content = dtMachine.Rows(0)("Receipt_Header").ToString
                txtReciptFooter.Content = dtMachine.Rows(0)("Receipt_Footer").ToString
                RadTxtipaddress.Text = dtMachine.Rows(0)("tillip_address").ToString

                mastertill.ClearSelection()
                mastertill.SelectedValue = dtMachine.Rows(0)("master_till").ToString()
                chktillsrvr.Checked = IIf(dtMachine.Rows(0)("till_server").ToString() = "Yes", True, False)
                chkboxtablesharing.Checked = IIf(dtMachine.Rows(0)("table_sharing").ToString() = "Yes", True, False)
                chkboxprintersharing.Checked = IIf(dtMachine.Rows(0)("printer_sharing").ToString() = "Yes", True, False)
                ddLocation.ClearSelection()
                ddLocation.SelectedValue = dtMachine.Rows(0)("location_id").ToString()
                txttime.Text = dtMachine.Rows(0)("sync_time").ToString()
                ViewState("TillUUID") = dtMachine.Rows(0)("TillUUID").ToString()
                If dtMachine.Rows(0)("is_assign") = "Yes" Then
                    chkAssign.Checked = True
                Else
                    chkAssign.Checked = False
                End If

                chkminipos.Checked = IIf(dtMachine.Rows(0)("is_minipos").ToString() = "Yes", True, False)
                chkwebSaleMaster.Checked = IIf(dtMachine.Rows(0)("is_master_self").ToString() = "Yes", True, False)
                If dtMachine.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
                If dtMachine.Rows(0)("back_to_main_function_on_till").ToString() = "Yes" Then
                    chckbacktomainfun.Checked = True
                Else
                    chckbacktomainfun.Checked = False
                End If
                If dtMachine.Rows(0)("extraSurcharge").ToString() = "Yes" Then
                    chkExtraasSurcharge.Checked = True
                Else
                    chkExtraasSurcharge.Checked = False
                End If

                If dtMachine.Rows(0)("Only_table_trans").ToString() = "Yes" Then
                    chkOnlyTables.Checked = True
                Else
                    chkOnlyTables.Checked = False
                End If

                chkAutoSurcharge.Checked = IIf(dtMachine.Rows(0)("AutoSurcharge").ToString() = "Yes", True, False)
                chkTables.Checked = IIf(dtMachine.Rows(0)("AutoSurchargeTables").ToString() = "Yes", True, False)
                chkNonTables.Checked = IIf(dtMachine.Rows(0)("AutoSurchargeNonTables").ToString() = "Yes", True, False)
                chkCloakroom.Checked = IIf(dtMachine.Rows(0)("AutoSurchargeCloakroom").ToString() = "Yes", True, False)
                chkChips.Checked = IIf(dtMachine.Rows(0)("AutoSurchargeChips").ToString() = "Yes", True, False)
                chkNoCashDrawer.Checked = IIf(dtMachine.Rows(0)("NoCashDrawer").ToString() = "Yes", True, False)
                chkReSync_Request.Checked = IIf(dtMachine.Rows(0)("ReSync_Request").ToString() = "Yes", True, False)
                chkService_Controller_Start.Checked = IIf(dtMachine.Rows(0)("Service_Controller_Start").ToString() = "Yes", True, False)
                chkService_Websale_print.Checked = IIf(dtMachine.Rows(0)("Service_Websale_print").ToString() = "Yes", True, False)
                chkService_Print_Share.Checked = IIf(dtMachine.Rows(0)("Service_Print_Share").ToString() = "Yes", True, False)
                chkService_print_Share_Other_Till.Checked = IIf(dtMachine.Rows(0)("Service_print_Share_Other_Till").ToString() = "Yes", True, False)
                chk_NoLogout.Checked = IIf(dtMachine.Rows(0)("Is_NoLogout").ToString() = "Yes", True, False)
                chk_PrintServer.Checked = IIf(dtMachine.Rows(0)("Is_PrintServer").ToString() = "Yes", True, False)
                chk_booking.Checked = IIf(dtMachine.Rows(0)("Is_ServiceBooking").ToString() = "Yes", True, False)
                chk_OnlineZreport.Checked = IIf(dtMachine.Rows(0)("Is_OnlineZreport").ToString() = "Yes", True, False)
                chk_IsKiosk.Checked = IIf(dtMachine.Rows(0)("IsKiosk").ToString() = "Yes", True, False)
                txt_TranLimit.Text = dtMachine.Rows(0)("TblTranLimit").ToString()
                txt_gtway_TID.Text = dtMachine.Rows(0)("gtway_TID").ToString()

                chk_QuickTran.Checked = IIf(dtMachine.Rows(0)("QuickTran").ToString() = "Yes", True, False)
                chk_TillReq.Checked = IIf(dtMachine.Rows(0)("TillRequest").ToString() = "Yes", True, False)
                chk_kioskReq.Checked = IIf(dtMachine.Rows(0)("KioskRequest").ToString() = "Yes", True, False)
                chk_kitchenPrint.Checked = IIf(dtMachine.Rows(0)("kitchenPrint").ToString() = "Yes", True, False)
                chk_ReceiptPrint.Checked = IIf(dtMachine.Rows(0)("ReceiptPrint").ToString() = "Yes", True, False)
                chk_ElinaTran.Checked = IIf(dtMachine.Rows(0)("ElinaTran").ToString() = "Yes", True, False)

                If dtMachine.Rows(0)("IsKiosk").ToString() = "Yes" Then
                    Div_kiosk.Visible = True
                Else
                    Div_kiosk.Visible = False
                End If

                If dtMachine.Rows(0)("QuickTran").ToString() = "Yes" Then
                    div_quickTran.Visible = True
                Else
                    div_quickTran.Visible = False
                End If

                If dtMachine.Rows(0)("AutoSurcharge").ToString() = "Yes" Then
                    chk.Visible = True
                Else
                    chk.Visible = False
                End If

            End If


            Dim GetFunction As DataTable = oclsMachine.GetFunction()

            If GetFunction.Rows.Count > 0 Then

                RadCombofunction.SelectedValue = GetFunction.Rows(0)("function_id").ToString()

                'For Each item As ListItem In RadCombofunction.Items
                '    For Each row As DataRow In GetFunction.Rows
                '        If item.Value = Val(row("function_id")) Then
                '            item.Selected = True
                '        End If
                '    Next
                'Next

            End If

            If dtMachine.Rows(0)("poslite").ToString() = "Yes" Then
                chkPOSLite.Checked = True
            Else
                chkPOSLite.Checked = False
            End If

            If dtMachine.Rows(0)("is_sunmi_second_screen").ToString() = "Yes" Then
                chkSecondScreen.Checked = True
            Else
                chkSecondScreen.Checked = False
            End If

            ViewState("secondscreenImage1") = dtMachine.Rows(0)("second_screen_image_1")
            ViewState("secondscreenImage2") = dtMachine.Rows(0)("second_screen_image_2")
            ViewState("secondscreenvideo") = dtMachine.Rows(0)("sunmi_video_path")

            ddlHardwareType.SelectedValue = dtMachine.Rows(0)("hardware_type").ToString()

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Machine_Master:BindMachine:" + ex.Message)
        End Try

    End Sub
    Public Sub bindprinter_Device()
        Try
            oclsMachine_Detail.cmp_id = Val(Session("cmp_id"))
            Dim dtMachine As DataTable = oclsMachine_Detail.Select()
            If dtMachine.Rows.Count > 0 Then
                rdMachineDetail.DataSource = dtMachine
                rdMachineDetail.DataBind()
            Else
                rdMachineDetail.DataSource = Nothing
                rdMachineDetail.DataBind()
            End If
        Catch ex As Exception
            LogHelper.Error("Machine_Master:bindprinter_Device:" + ex.Message)
        End Try

    End Sub

    Public Sub bindKeymap()
        Try
            oclsMachine.cmp_id = Val(Session("cmp_id"))
            Dim dtMachine As DataTable = oclsMachine.[Select_CHK]()
            If dtMachine.Rows.Count > 0 Then
                rdKeymap.DataSource = dtMachine
                rdKeymap.DataBind()
            Else
                rdKeymap.DataSource = Nothing
                rdKeymap.DataBind()
            End If

            For Each row As GridViewRow In rdKeymap.Rows

                Dim key_map_id As HiddenField = CType(row.FindControl("hdKeymap_id"), HiddenField)
                Dim chkKeymap As CheckBox = CType(row.FindControl("chkKeymap"), CheckBox)
                Dim lbl_name As Label = CType(row.FindControl("lbl_name"), Label)
                Dim shorting_num As TextBox = CType(row.FindControl("txtShortingNum"), TextBox)

                oclsMachine.cmp_id = Val(Session("cmp_id"))
                oclsMachine.machine_id = Val(Session("machine_id"))
                Dim dt As DataTable = oclsMachine.GetKeymap()
                If dt.Rows.Count > 0 Then
                    For Each row1 As DataRow In dt.Rows

                        If Val(key_map_id.Value) = Val(row1("keymap_id")) Then
                            chkKeymap.Checked = True
                            shorting_num.Text = row1("shorting_num").ToString
                        End If

                    Next
                End If

            Next

        Catch ex As Exception
            LogHelper.Error("Machine_Master:bindprinter_Device:" + ex.Message)
        End Try

    End Sub

    Public Sub selectPrinterDevice()
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("machine_id") = Nothing Then
                Clear()
            Else
                BindMachine()
            End If
        Catch ex As Exception
            LogHelper.Error("Machine_Master:btnReset_Click:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Try
            Response.Redirect("Machine_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Machine_Master:btnCancel_Click:" + ex.Message)
        End Try

    End Sub

    Private Sub Clear()
        Try
            txtCode.Text = ""
            txtMName.Text = ""
            txtmacaddress.Text = ""
            txtmodel.ClearSelection()
            txtSerialNo.Text = ""
            ddLocation.ClearSelection()
            chkAssign.Checked = False
            mastertill.ClearSelection()
            chkminipos.Checked = False
            chkActive.Checked = True
            txtReciptFooter.Content = ""
            txtReciptHeader.Content = ""
            chkwebSaleMaster.Checked = False
        Catch ex As Exception
            LogHelper.Error("Machine_Master:Clear:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsMachine.cmp_id = Val(Session("cmp_id"))
            oclsMachine.code = txtCode.Text.Trim()
            oclsMachine.name = txtMName.Text.Trim()
            oclsMachine.mac_address = txtmacaddress.Text
            oclsMachine.serial_no = txtSerialNo.Text
            'oclsMachine.model = txtmodel.Text
            oclsMachine.model = txtmodel.SelectedItem.Text
            oclsMachine.ip_address = Request.UserHostAddress
            oclsMachine.login_id = Val(Session("login_id"))
            oclsMachine.tillip_address = RadTxtipaddress.Text
            oclsMachine.location_id = Val(ddLocation.SelectedItem.Value)
            oclsMachine.is_assign = IIf(chkAssign.Checked = True, 1, 0)
            oclsMachine.is_minipos = IIf(chkminipos.Checked = True, 1, 0)
            oclsMachine.is_master_self = IIf(chkwebSaleMaster.Checked = True, 1, 0)
            oclsMachine.master_till = Val(mastertill.SelectedItem.Value)
            oclsMachine.till_server = IIf(chktillsrvr.Checked = True, 1, 0)
            oclsMachine.table_sharing = IIf(chkboxtablesharing.Checked = True, 1, 0)
            oclsMachine.printer_sharing = IIf(chkboxprintersharing.Checked = True, 1, 0)
            oclsMachine.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsMachine.Receipt_Header = WebUtility.HtmlDecode(txtReciptHeader.Content)
            oclsMachine.Receipt_Footer = WebUtility.HtmlDecode(txtReciptFooter.Content)
            oclsMachine.sync_time = txttime.Text
            oclsMachine.back_to_main_function_on_till = IIf(chckbacktomainfun.Checked = True, 1, 0)
            oclsMachine.extraSurcharge = IIf(chkExtraasSurcharge.Checked = True, 1, 0)
            oclsMachine.onlytabletrans = IIf(chkOnlyTables.Checked = True, 1, 0)
            oclsMachine.NoCashDrawer = IIf(chkNoCashDrawer.Checked = True, 1, 0)
            oclsMachine.ReSync_Request = IIf(chkReSync_Request.Checked = True, 1, 0)
            oclsMachine.Service_Controller_Start = IIf(chkService_Controller_Start.Checked = True, 1, 0)
            oclsMachine.Service_Websale_print = IIf(chkService_Websale_print.Checked = True, 1, 0)
            oclsMachine.Service_Print_Share = IIf(chkService_Print_Share.Checked = True, 1, 0)
            oclsMachine.Service_print_Share_Other_Till = IIf(chkService_print_Share_Other_Till.Checked = True, 1, 0)
            oclsMachine.Is_NoLogout = IIf(chk_NoLogout.Checked = True, 1, 0)
            oclsMachine.Is_PrintServer = IIf(chk_PrintServer.Checked = True, 1, 0)
            oclsMachine.Is_ServiceBooking = IIf(chk_booking.Checked = True, 1, 0)
            oclsMachine.Is_OnlineZreport = IIf(chk_OnlineZreport.Checked = True, 1, 0)
            oclsMachine.IsKiosk = IIf(chk_IsKiosk.Checked = True, 1, 0)
            oclsMachine.TranLimit = IIf(txt_TranLimit.Text = "", 0, Val(txt_TranLimit.Text))
            oclsMachine.gtway_TID = txt_gtway_TID.Text
            oclsMachine.QuickTran = IIf(chk_QuickTran.Checked = True, 1, 0)
            oclsMachine.tillRequest = IIf(chk_TillReq.Checked = True, 1, 0)
            oclsMachine.KioskRequest = IIf(chk_kioskReq.Checked = True, 1, 0)
            oclsMachine.kitchenPrint = IIf(chk_kitchenPrint.Checked = True, 1, 0)
            oclsMachine.ReceiptPrint = IIf(chk_ReceiptPrint.Checked = True, 1, 0)
            oclsMachine.ElinaTran = IIf(chk_ElinaTran.Checked = True, 1, 0)

            If chkAutoSurcharge.Checked = True Then
                oclsMachine.AutoSurcharge = IIf(chkAutoSurcharge.Checked = True, 1, 0)
                oclsMachine.AutoSurchargeTables = IIf(chkTables.Checked = True, 1, 0)
                oclsMachine.AutoSurchargeNonTables = IIf(chkNonTables.Checked = True, 1, 0)
                oclsMachine.AutoSurchargeCloakroom = IIf(chkCloakroom.Checked = True, 1, 0)
                oclsMachine.AutoSurchargeChips = IIf(chkChips.Checked = True, 1, 0)
            Else
                oclsMachine.AutoSurcharge = 0
                oclsMachine.AutoSurchargeTables = 0
                oclsMachine.AutoSurchargeNonTables = 0
                oclsMachine.AutoSurchargeCloakroom = 0
                oclsMachine.AutoSurchargeChips = 0

            End If


            oclsMachine.SunmiSecondScreen = IIf(chkSecondScreen.Checked = True, 1, 0)
            oclsMachine.POSlite = IIf(chkPOSLite.Checked = True, 1, 0)


            Dim o As Int32 = 0
            For Each file As UploadedFile In rauploadSSImage1.UploadedFiles
                o = o + 1
            Next
            If o > 0 Then
                RadBinaryImage3.DataValue = Nothing
                For Each file As UploadedFile In rauploadSSImage1.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = rauploadSSImage1.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    rauploadSSImage1.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryImage3.DataValue = image
                    ImageTable.Visible = True
                    Dim bytes As Byte() = RadBinaryImage3.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    ImageTable.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next
            Else
                Try
                    If Not ViewState("secondscreenImage1").ToString() = Nothing Then
                        RadBinaryImage3.DataValue = ViewState("secondscreenImage1")
                    End If
                Catch ex As Exception

                End Try

            End If

            oclsMachine.SecondScreenImage1 = RadBinaryImage3.DataValue


            o = 0
            For Each file As UploadedFile In rauploadSSImage2.UploadedFiles
                o = o + 1
            Next
            If o > 0 Then
                RadBinaryImage4.DataValue = Nothing
                For Each file As UploadedFile In rauploadSSImage2.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = rauploadSSImage2.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    rauploadSSImage2.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryImage4.DataValue = image
                    Image1.Visible = True
                    Dim bytes As Byte() = RadBinaryImage4.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next
            Else
                Try
                    If Not ViewState("secondscreenImage2").ToString() = Nothing Then
                        RadBinaryImage4.DataValue = ViewState("secondscreenImage2")
                    End If
                Catch ex As Exception

                End Try

            End If

            oclsMachine.SecondScreenImage2 = RadBinaryImage4.DataValue
            Try
                oclsMachine.sunmi_video_path = ViewState("secondscreenvideo").ToString()
            Catch ex As Exception

            End Try

            oclsMachine.hardware_type = Val(ddlHardwareType.SelectedValue.ToString())
            oclsMachine.IsKiosk = 0
            oclsMachine.is_minipos = 0

            If ddlHardwareType.SelectedValue = "4" Then
                oclsMachine.IsKiosk = 1
            ElseIf ddlHardwareType.SelectedValue = "1" Then
                oclsMachine.is_minipos = 1
            End If



            If Session("machine_id") = Nothing Then
                oclsMachine.machine_id = 0
                Dim machine_id As Integer = oclsMachine.Insert()
                Saveprinter_device(machine_id)
                SaveMainFunction_check(machine_id)
                'Dim machine_id As Integer = 0

                '----Comment on 19/08/2021----
                'SaveMainFunction_Till(machine_id)
                'SaveFunction_Till(machine_id)



                Session("Success") = "Record inserted successfully"
            Else
                oclsMachine.machine_id = Val(Session("machine_id"))
                oclsMachine.Update()

                Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("poscontrollerconnectionstring").ConnectionString)
                strcon.Open()

                Dim cmd As SqlCommand = New SqlCommand("p_m_qr_update", strcon)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@venue_id", Val(Session("venue_id")))
                cmd.Parameters.AddWithValue("@machine_id", Val(Session("machine_id")))
                cmd.Parameters.AddWithValue("@location_id", Val(ddLocation.SelectedItem.Value))
                cmd.Parameters.AddWithValue("@tilluuid", ViewState("TillUUID"))
                cmd.Parameters.AddWithValue("@storeuuid", Session("storeuuid"))


                cmd.executenonquery()

                Saveprinter_device(Val(Session("machine_id")))
                SaveMainFunction_check(Val(Session("machine_id")))
                Session("Success") = "Record updated successfully"
            End If
            Session("machine_id") = Nothing
            Response.Redirect("Machine_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Machine_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub rdMachineDetail_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rdMachineDetail.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then

                Dim device_id As HiddenField = CType(e.Item.FindControl("hddevice_id"), HiddenField)
                Dim ddlSize As RadComboBox = DirectCast(e.Item.FindControl("ddlDevice"), RadComboBox)
                Dim printer_id As HiddenField = CType(e.Item.FindControl("hdprinter_id"), HiddenField)

                oclsMachine_Detail.cmp_id = Val(Session("cmp_id"))
                Dim dt As DataTable = oclsMachine_Detail.Select_Device()
                If dt.Rows.Count > 0 Then
                    ddlSize.DataTextField = "Device"
                    ddlSize.DataValueField = "device_id"
                    ddlSize.DataSource = dt
                    ddlSize.DataBind()
                End If

                Dim li As RadComboBoxItem = New RadComboBoxItem("No printer", "0")
                ddlSize.Items.Insert(0, li)

                oclsMachine_Detail.cmp_id = Val(Session("cmp_id"))
                oclsMachine_Detail.Machine_id = Val(Session("machine_id"))
                Dim dtMachine As DataTable = oclsMachine_Detail.SelectPrinterDeviceByMachine()
                If dtMachine.Rows.Count > 0 Then
                    For Each row As DataRow In dtMachine.Rows

                        If Val(printer_id.Value) = Val(row("printer_id")) Then
                            ddlSize.SelectedValue = row("Device_id")
                        End If
                    Next
                End If


            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Saveprinter_device(ByVal id As Integer)
        Try

            For Each item As GridDataItem In rdMachineDetail.Items
                Dim ddlSize As RadComboBox = DirectCast(item.FindControl("ddlDevice"), RadComboBox)

                Dim firstName As String = TryCast(item("Device").FindControl("ddlDevice"), RadComboBox).Text
                Dim device_id As Integer = ddlSize.SelectedValue
                Dim printer_id As Integer = TryCast(item("printer").FindControl("hdPrinter_id"), HiddenField).Value

                If device_id <> 0 Then
                    oclsMachine_Detail.Machine_id = id
                    oclsMachine_Detail.Machine_Detail_id = 0
                    oclsMachine_Detail.Printer_id = printer_id
                    oclsMachine_Detail.Device_id = device_id
                    oclsMachine_Detail.cmp_id = Val(Session("cmp_id"))
                    oclsMachine_Detail.ip_address = Request.UserHostAddress
                    oclsMachine_Detail.Login_id = Val(Session("Login_id"))

                    oclsMachine_Detail.Insert()
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveFunction_Till(ByVal id As Integer)
        Try

            oclsMachine.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsMachine.SelectFunctionByCmp()

            For Each row As DataRow In dt.Rows

                oclsFunction.cmp_id = Val(Session("cmp_id"))
                oclsFunction.name = row("name")
                oclsFunction.code = row("code")
                oclsFunction.caption_type = row("caption_type")
                oclsFunction.is_active = row("is_active")
                oclsFunction.shorting_no = row("shorting_no")
                oclsFunction.ip_address = Request.UserHostAddress
                oclsFunction.login_id = Val(Session("login_id"))
                oclsFunction.item = row("item")
                oclsFunction.for_color = row("for_color")
                oclsFunction.back_color = row("back_color")
                oclsFunction.machine_id = id
                oclsFunction.big_button = row("big_button")
                oclsFunction.Payment_id = Val(row("Payment_id"))
                oclsFunction.pay_type = Val(row("pay_type"))
                oclsFunction.pay_sub_type = row("pay_sub_type")
                oclsFunction.isgroupbydept = row("is_groupby_dept")
                oclsFunction.isgroupbycourse = row("is_groupby_course")

                oclsFunction.dept_id = row("dept_id")
                oclsFunction.course_id = row("course_id")
                oclsFunction.Panel_id = row("Panel_id")
                oclsFunction.Parent_id = Val(row("Parent_id"))
                oclsFunction.mainfunction_id = mainFun_id
                oclsFunction.Insert()
            Next


            oclsFunction.cmp_id = Val(Session("cmp_id"))
            oclsFunction.machine_id = id
            oclsFunction.mainfunction_id = mainFun_id
            oclsFunction.Update_Function_ParentBy_Till()

        Catch ex As Exception
            LogHelper.Error("Machine_Master:bindprinter_Device:" + ex.Message)
        End Try
    End Sub
    Private Sub SaveMainFunction_Till(ByVal id As Integer)
        Try

            oclsFunction.mainfunction_id = 0
            oclsFunction.cmp_id = Val(Session("cmp_id"))
            oclsFunction.name = txtMName.Text.Trim()
            oclsFunction.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsFunction.ip_address = Request.UserHostAddress
            oclsFunction.login_id = Val(Session("login_id"))
            oclsFunction.machine_id = id
            mainFun_id = oclsFunction.InsertMainFunction()

        Catch ex As Exception
            LogHelper.Error("Machine_Master:bindprinter_Device:" + ex.Message)
        End Try
    End Sub

    Protected Sub rdMachineDetail_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdMachineDetail.NeedDataSource
        Try
            oclsMachine_Detail.cmp_id = Val(Session("cmp_id"))
            Dim dtMachine As DataTable = oclsMachine_Detail.Select()
            If dtMachine.Rows.Count > 0 Then
                rdMachineDetail.DataSource = dtMachine
            Else
                rdMachineDetail.DataSource = Nothing
            End If
        Catch ex As Exception
            LogHelper.Error("Machine_Master:bindprinter_Device:" + ex.Message)
        End Try

    End Sub

    Private Sub SaveMainFunction_check(ByVal id As Integer)
        Try

            If (oclsMachine.machine_id <> 0) Then
                oclsMachine.delete_keymap_Id()

                oclsMachine.delete_function_Id()
            End If


            'oclsMachine.mainfunction_id = 0
            oclsMachine.cmp_id = Val(Session("cmp_id"))
            oclsMachine.machine_id = id
            oclsMachine.function_id = Val(RadCombofunction.SelectedValue.ToString())
            oclsMachine.InsertMainFunctionchk()

            'For Each item As ListItem In RadCombofunction.Items
            '    If item.Selected Then
            '        oclsMachine.function_id = item.Value
            '        oclsMachine.InsertMainFunctionchk()
            '    End If
            'Next



            For Each item As GridViewRow In rdKeymap.Rows

                If CType(item.FindControl("chkKeymap"), CheckBox).Checked Then
                    oclsMachine.keymap_id = Convert.ToInt32(CType(item.FindControl("hdKeymap_id"), HiddenField).Value.ToString())

                    If CType(item.FindControl("txtShortingNum"), TextBox).Text.ToString() = "" Then
                        oclsMachine.shorting_num = Convert.ToInt32(0)

                    Else
                        oclsMachine.shorting_num = Convert.ToInt32(CType(item.FindControl("txtShortingNum"), TextBox).Text.ToString())

                    End If
                    oclsMachine.Insertkeymapcheck()
                End If
            Next


            'For Each item As ListItem In Radkeymap.Items
            '    If item.Selected Then
            '        oclsMachine.keymap_id = item.Value
            '        oclsMachine.Insertkeymapcheck()
            '    End If
            'Next

            oclsMachine.cmp_id = Val(Session("cmp_id"))

            'oclsMachine.updateMainFunctionchk()
            'mainFun_id = oclsMachine.InsertMainFunctionchk()

        Catch ex As Exception
            LogHelper.Error("Machine_Master:" + ex.Message)
        End Try

    End Sub

    'Private Sub Savekepmap_check()
    '    Try

    '        'oclsMachine.mainfunction_id = 0
    '        oclsMachine.cmp_id = Val(Session("cmp_id"))


    '        For Each item As ListItem In Radkeymap.Items
    '            If item.Selected Then
    '                oclsMachine.keymap_id = item.Value
    '                oclsMachine.Insertkeymapcheck()
    '            End If
    '        Next

    '        'mainFun_id = oclsMachine.InsertMainFunctionchk()

    '    Catch ex As Exception
    '        LogHelper.Error("Machine_Master:" + ex.Message)
    '    End Try
    '    Throw New NotImplementedException
    'End Sub


    Protected Sub chkAutoSurcharge_CheckedChanged(sender As Object, e As EventArgs)
        Try

            If chkAutoSurcharge.Checked = True Then
                chk.Visible = True
            Else
                chk.Visible = False

            End If

        Catch ex As Exception
            LogHelper.Error("Machine_Master:" + ex.Message)
        End Try
    End Sub
    Protected Sub chk_QuickTran_CheckedChanged(sender As Object, e As EventArgs)
        Try

            If chk_QuickTran.Checked = True Then
                div_quickTran.Visible = True
            Else
                chk_kitchenPrint.Checked = False
                chk_ReceiptPrint.Checked = False

                div_quickTran.Visible = False

            End If

        Catch ex As Exception
            LogHelper.Error("Machine_Master:chk_QuickTran_CheckedChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub lnk_Settings_Click(sender As Object, e As EventArgs)
        Try

            Response.Redirect("Kiosk_Settings.aspx", False)

        Catch ex As Exception
            LogHelper.Error("Machine_Master:lnk_Settings_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub chk_IsKiosk_CheckedChanged(sender As Object, e As EventArgs)
        Try

            If chk_IsKiosk.Checked = True Then

                Div_kiosk.Visible = True
            Else
                Div_kiosk.Visible = False

            End If
        Catch ex As Exception
            LogHelper.Error("Machine_Master:chk_IsKiosk_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub rauploadSSvideo_FileUploaded(sender As Object, e As FileUploadedEventArgs)
        Try
            rauploadSSvideo.TargetFolder = "~/video/"
            Dim newfilename As String = e.File.GetNameWithoutExtension() + User.Identity.Name.Replace("\\", String.Empty) + System.DateTime.Now.ToShortDateString().Replace(" ", "").Replace(":", "").Replace("/", "").Replace("-", "").Trim() + e.File.GetExtension()
            e.File.SaveAs(Path.Combine(Server.MapPath(rauploadSSvideo.TargetFolder), newfilename))
            ViewState("secondscreenvideo") = newfilename
        Catch ex As Exception
            LogHelper.Error("Machine_Master: rauploadSSvideo_FileUploaded:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddlHardwareType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ddlHardwareType.SelectedValue = "4" Then
                Div_kiosk.Visible = True
            Else
                Div_kiosk.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRegisterforQR_Click(sender As Object, e As EventArgs)
        '' showstoreuuidpopup()
        'If isPopupShown Then
        '    Return
        'End If
    

        If Session("conStoreuuid").ToString() = "" OrElse Session("conStoreuuid").ToString().ToLower() = "null" OrElse Session("conStoreuuid").ToString() = "0" Then
            Dim strcone As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcone.Open()
            Dim n As String = Session("Storename")
            Dim enable As String = Guid.NewGuid().ToString

            Dim cmdt As SqlCommand = New SqlCommand("Storeuuid_enable", strcone)
            cmdt.CommandType = CommandType.StoredProcedure

            cmdt.Parameters.AddWithValue("@store_name", n)
            cmdt.Parameters.AddWithValue("@store_uuid", enable)
            cmdt.ExecuteNonQuery()
            Session("conStoreuuid") = enable
            If Session("conStoreuuid").ToString() IsNot Nothing Then



                Dim enlogin As New Controller_clsLogin()
                enlogin.Store_UUID = Session("conStoreuuid")

                Dim dtlogin As DataTable = enlogin.enablelogin()
            End If
            'showstoreuuidpopup()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Enabled Register for QR Successfully');", True)

        Else
            Dim storeUuid As String = Session("conStoreuuid").ToString()
            Dim objclsLogin As New Controller_clsLogin()
            objclsLogin.Store_UUID = storeUuid

            Dim dtlogin As DataTable = objclsLogin.enablelogin()
            Session("StoreUUID") = Session("conStoreuuid")
            'showstoreuuidpopup()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Enabled Register for QR Successfully');", True)

        End If




        Dim TillUUID = Guid.NewGuid().ToString
        Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
        strcon.Open()

        Dim cmd As SqlCommand = New SqlCommand("P_M_QR_Register", strcon)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@venue_id", Val(Session("venue_id")))
        cmd.Parameters.AddWithValue("@machine_id", Val(Session("machine_id")))
        cmd.Parameters.AddWithValue("@location_id", Val(ddLocation.SelectedItem.Value))
        cmd.Parameters.AddWithValue("@TillUUID", TillUUID.ToString)
        cmd.Parameters.AddWithValue("@StoreUUID", Session("StoreUUID"))
        Dim outputParam As SqlParameter = New SqlParameter("@ExistingTillUUID", SqlDbType.VarChar, 50)
        outputParam.Direction = ParameterDirection.Output
        cmd.Parameters.Add(outputParam)
        cmd.ExecuteNonQuery()

        Dim newtillUUID As String = outputParam.Value.ToString()
        If Not String.IsNullOrEmpty(newtillUUID) Then
            Dim oldTillUUID As String = TillUUID.ToString

            If oldTillUUID <> newtillUUID Then
                TillUUID = newtillUUID
            End If
        Else
            TillUUID = TillUUID
        End If








        oclsMachine.cmp_id = Val(Session("cmp_id"))
        oclsMachine.code = txtCode.Text.Trim()
        oclsMachine.name = txtMName.Text.Trim()
        oclsMachine.mac_address = txtmacaddress.Text
        oclsMachine.serial_no = txtSerialNo.Text
        oclsMachine.model = txtmodel.Text
        oclsMachine.ip_address = Request.UserHostAddress
        oclsMachine.login_id = Val(Session("login_id"))
        oclsMachine.tillip_address = RadTxtipaddress.Text
        oclsMachine.location_id = Val(ddLocation.SelectedItem.Value)
        oclsMachine.is_assign = IIf(chkAssign.Checked = True, 1, 0)
        oclsMachine.is_minipos = IIf(chkminipos.Checked = True, 1, 0)
        oclsMachine.is_master_self = IIf(chkwebSaleMaster.Checked = True, 1, 0)
        oclsMachine.till_server = IIf(chktillsrvr.Checked = True, 1, 0)
        oclsMachine.till_server = Val(mastertill.SelectedItem.Value)
        oclsMachine.table_sharing = IIf(chkboxtablesharing.Checked = True, 1, 0)
        oclsMachine.printer_sharing = IIf(chkboxprintersharing.Checked = True, 1, 0)
        oclsMachine.is_active = IIf(chkActive.Checked = True, 1, 0)
        oclsMachine.Receipt_Header = WebUtility.HtmlDecode(txtReciptHeader.Content)
        oclsMachine.Receipt_Footer = WebUtility.HtmlDecode(txtReciptFooter.Content)
        oclsMachine.sync_time = txttime.Text
        oclsMachine.back_to_main_function_on_till = IIf(chckbacktomainfun.Checked = True, 1, 0)
        oclsMachine.extraSurcharge = IIf(chkExtraasSurcharge.Checked = True, 1, 0)
        oclsMachine.onlytabletrans = IIf(chkOnlyTables.Checked = True, 1, 0)
        oclsMachine.NoCashDrawer = IIf(chkNoCashDrawer.Checked = True, 1, 0)
        oclsMachine.ReSync_Request = IIf(chkReSync_Request.Checked = True, 1, 0)
        oclsMachine.Service_Controller_Start = IIf(chkService_Controller_Start.Checked = True, 1, 0)
        oclsMachine.Service_Websale_print = IIf(chkService_Websale_print.Checked = True, 1, 0)
        oclsMachine.Service_Print_Share = IIf(chkService_Print_Share.Checked = True, 1, 0)
        oclsMachine.Service_print_Share_Other_Till = IIf(chkService_print_Share_Other_Till.Checked = True, 1, 0)
        oclsMachine.Is_NoLogout = IIf(chk_NoLogout.Checked = True, 1, 0)
        oclsMachine.Is_PrintServer = IIf(chk_PrintServer.Checked = True, 1, 0)
        oclsMachine.Is_ServiceBooking = IIf(chk_booking.Checked = True, 1, 0)
        oclsMachine.Is_OnlineZreport = IIf(chk_OnlineZreport.Checked = True, 1, 0)
        oclsMachine.IsKiosk = IIf(chk_IsKiosk.Checked = True, 1, 0)
        oclsMachine.TranLimit = IIf(txt_TranLimit.Text = "", 0, Val(txt_TranLimit.Text))
        oclsMachine.gtway_TID = txt_gtway_TID.Text
        oclsMachine.QuickTran = IIf(chk_QuickTran.Checked = True, 1, 0)
        oclsMachine.kitchenPrint = IIf(chk_kitchenPrint.Checked = True, 1, 0)
        oclsMachine.ReceiptPrint = IIf(chk_ReceiptPrint.Checked = True, 1, 0)
        oclsMachine.ElinaTran = IIf(chk_ElinaTran.Checked = True, 1, 0)
        oclsMachine.TillUUID = TillUUID.ToString


        If chkAutoSurcharge.Checked = True Then
            oclsMachine.AutoSurcharge = IIf(chkAutoSurcharge.Checked = True, 1, 0)
            oclsMachine.AutoSurchargeTables = IIf(chkTables.Checked = True, 1, 0)
            oclsMachine.AutoSurchargeNonTables = IIf(chkNonTables.Checked = True, 1, 0)
            oclsMachine.AutoSurchargeCloakroom = IIf(chkCloakroom.Checked = True, 1, 0)
            oclsMachine.AutoSurchargeChips = IIf(chkChips.Checked = True, 1, 0)
        Else
            oclsMachine.AutoSurcharge = 0
            oclsMachine.AutoSurchargeTables = 0
            oclsMachine.AutoSurchargeNonTables = 0
            oclsMachine.AutoSurchargeCloakroom = 0
            oclsMachine.AutoSurchargeChips = 0

        End If


        oclsMachine.SunmiSecondScreen = IIf(chkSecondScreen.Checked = True, 1, 0)
        oclsMachine.POSlite = IIf(chkPOSLite.Checked = True, 1, 0)


        Dim o As Int32 = 0
        For Each file As UploadedFile In rauploadSSImage1.UploadedFiles
            o = o + 1
        Next
        If o > 0 Then
            RadBinaryImage3.DataValue = Nothing
            For Each file As UploadedFile In rauploadSSImage1.UploadedFiles
                Dim image As Byte()
                Dim fileLength As Long = rauploadSSImage1.UploadedFiles(0).InputStream.Length
                image = New Byte(fileLength - 1) {}
                rauploadSSImage1.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                RadBinaryImage3.DataValue = image
                ImageTable.Visible = True
                Dim bytes As Byte() = RadBinaryImage3.DataValue
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                ImageTable.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
            Next
        Else
            Try
                If Not ViewState("secondscreenImage1").ToString() = Nothing Then
                    RadBinaryImage3.DataValue = ViewState("secondscreenImage1")
                End If
            Catch ex As Exception

            End Try

        End If

        oclsMachine.SecondScreenImage1 = RadBinaryImage3.DataValue


        o = 0
        For Each file As UploadedFile In rauploadSSImage2.UploadedFiles
            o = o + 1
        Next
        If o > 0 Then
            RadBinaryImage4.DataValue = Nothing
            For Each file As UploadedFile In rauploadSSImage2.UploadedFiles
                Dim image As Byte()
                Dim fileLength As Long = rauploadSSImage2.UploadedFiles(0).InputStream.Length
                image = New Byte(fileLength - 1) {}
                rauploadSSImage2.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                RadBinaryImage4.DataValue = image
                Image1.Visible = True
                Dim bytes As Byte() = RadBinaryImage4.DataValue
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
            Next
        Else
            Try
                If Not ViewState("secondscreenImage2").ToString() = Nothing Then
                    RadBinaryImage4.DataValue = ViewState("secondscreenImage2")
                End If
            Catch ex As Exception

            End Try

        End If

        oclsMachine.SecondScreenImage2 = RadBinaryImage4.DataValue
        Try
            oclsMachine.sunmi_video_path = ViewState("secondscreenvideo").ToString()
        Catch ex As Exception

        End Try

        oclsMachine.hardware_type = Val(ddlHardwareType.SelectedValue.ToString())
        oclsMachine.IsKiosk = 0
        oclsMachine.is_minipos = 0

        If ddlHardwareType.SelectedValue = "4" Then
            oclsMachine.IsKiosk = 1
        ElseIf ddlHardwareType.SelectedValue = "1" Then
            oclsMachine.is_minipos = 1
        End If



        'If Session("machine_id") = Nothing Then
        '    oclsMachine.machine_id = 0
        '    Dim machine_id As Integer = oclsMachine.registerwithQR()
        '    Saveprinter_device(machine_id)
        '    SaveMainFunction_check(machine_id)
        '    Session("Success") = "Register for QR successfully"
        'Else
        oclsMachine.machine_id = Val(Session("machine_id"))
            oclsMachine.registerwithQR()
            Saveprinter_device(Val(Session("machine_id")))
            SaveMainFunction_check(Val(Session("machine_id")))
        Session("Success") = "Register for QR successfully"
        'End If
        Session("machine_id") = Nothing
        Response.Redirect("Machine_List.aspx", False)






    End Sub

    'Private Sub showstoreuuidpopup()
    '    If Session("storeuuid") Is Nothing OrElse Session("storeuuid").ToString() = "" OrElse Session("storeuuid").ToString().ToLower() = "null" OrElse Session("constoreuuid").ToString() = "" OrElse Session("constoreuuid").ToString().ToLower() = "null" Then

    '        'pnlstoreuuidpopup.visible = true
    '        isPopupShown = True
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "emailerror", "alert('Contact the Support Team');", True)

    '    Else
    '        ' pnlstoreuuidpopup.visible = false
    '        isPopupShown = False
    '    End If
    'End Sub
End Class

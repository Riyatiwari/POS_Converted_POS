Imports System.Data
Imports System.Security.Cryptography
Imports DocumentFormat.OpenXml.Spreadsheet
Imports Microsoft.Office.Core
Imports Telerik.Web.UI
Imports Crop = Microsoft.Office.Core.Crop

Partial Class Device_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsDevice As New Controller_clsDevice()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx")
                End If
                oclsBind.BindMachine(ddMachine, Val(Session("cmp_id")))
                oclsBind.BindDeviceCategory(ddlCategory, Val(Session("cmp_id")))
                'oclsBind.BindDeviceType(ddDeviceType, Val(Session("cmp_id")))
                If Not Session("device_id") = Nothing Then
                    BindDevice()
                    txtCode.ReadOnly = True
                    rvPassword.Enabled = False
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Device_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindDevice()
        Try
            oclsDevice.cmp_id = Val(Session("cmp_id"))
            oclsDevice.device_id = Val(Session("device_id"))
            Dim dtDevice As DataTable = oclsDevice.Select()
            If dtDevice.Rows.Count > 0 Then
                txtDName.Text = dtDevice.Rows(0)("name").ToString
                txtCode.Text = dtDevice.Rows(0)("code").ToString
                ddMachine.ClearSelection()
                ddMachine.SelectedValue = Val(dtDevice.Rows(0)("machine_id"))
                txtSerialNo.Text = dtDevice.Rows(0)("serial_no").ToString

                If Not Val(dtDevice.Rows(0)("device_category")) = 0 Then
                    ddlCategory.ClearSelection()
                    ddlCategory.SelectedValue = Val(dtDevice.Rows(0)("device_category"))
                    oclsBind.BindDeviceType(ddDeviceType, Val(Session("cmp_id")), Val(ddlCategory.SelectedValue))
                Else
                    oclsBind.BindDeviceType(ddDeviceType, Val(Session("cmp_id")))
                End If


                ddDeviceType.ClearSelection()
                ddDeviceType.SelectedValue = Val(dtDevice.Rows(0)("Device_Type_id"))
                    ddDeviceSubType.ClearSelection()
                    ddDeviceSubType.SelectedValue = Val(dtDevice.Rows(0)("Device_SubType"))


                    If dtDevice.Rows(0)("Active").ToString() = "Yes" Then
                        chkActive.Checked = True
                    Else
                        chkActive.Checked = False
                    End If

                    txtwidth.Text = Val(dtDevice.Rows(0)("width"))

                If (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "PRINTER" Or (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "CASH CAMERA" Then
                    If (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "PRINTER" Then
                        Device_SubType.Visible = True
                    Else
                        Device_SubType.Visible = False
                    End If
                    divPrinter.Visible = True
                    ddNetworkType.SelectedValue = dtDevice.Rows(0)("network_type").ToString

                    If ddNetworkType.SelectedValue.ToString() = "Serial Port" Then
                        divSerialPort.Visible = True
                        divLAN.Visible = False
                        divUSB.Visible = False
                        txtBudrate.Text = dtDevice.Rows(0)("budrate")
                        txtDeviceName.Text = dtDevice.Rows(0)("device_name")
                    ElseIf ddNetworkType.SelectedValue.ToString() = "USB" Then
                        divSerialPort.Visible = False
                        divLAN.Visible = False
                        divUSB.Visible = False
                        txtvenderid.Text = Val(dtDevice.Rows(0)("vender_id"))
                    ElseIf ddNetworkType.SelectedValue.ToString() = "LAN" Then
                        divSerialPort.Visible = False
                        divLAN.Visible = True
                        divUSB.Visible = False
                        txtIpAddress.Text = dtDevice.Rows(0)("printer_ip_address")
                        txtport.Text = Val(dtDevice.Rows(0)("port"))
                    End If
                    divEVO.Visible = False
                ElseIf (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "EVO" Or (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "PAY WORKS" Then
                    Device_SubType.Visible = False
                    divEVO.Visible = True
                    txtUserName.Text = dtDevice.Rows(0)("user_name")
                    txtpassword.Text = dtDevice.Rows(0)("password")
                    txtblutoothName.Text = dtDevice.Rows(0)("bluetooth_name")
                    TxtAPI.Text = dtDevice.Rows(0)("application_profile_id")
                    txtServicekey.Text = dtDevice.Rows(0)("service_key")
                ElseIf (ddDeviceType.SelectedItem.Text).ToString = "Kinetic" Or (ddDeviceType.SelectedItem.Text).ToString = "Kinetic Saturn" Then
                    Device_SubType.Visible = False
                    divKinetic.Visible = True
                    divKinetic1.Visible = False
                    txtUserName1.Text = dtDevice.Rows(0)("user_name")
                    txtpassword1.Text = dtDevice.Rows(0)("password")
                    'txtpassword1.Attributes.Add("value", txtpassword1.Text)
                    hfPassword.Value = txtpassword1.Text
                    txtPortKinetic.Text = dtDevice.Rows(0)("port")
                    txtIpAddress1.Text = dtDevice.Rows(0)("printer_ip_address")
                ElseIf (ddDeviceType.SelectedItem.Text).ToString = "Castles Pay" Or (ddDeviceType.SelectedItem.Text).ToString = "Nucleus" Or (ddDeviceType.SelectedItem.Text).ToString = "Pax Communicator" Or (ddDeviceType.SelectedItem.Text).ToString = "Pax A920" Or (ddDeviceType.SelectedItem.Text).ToString = "Ingenico" Then
                    Device_SubType.Visible = False
                    divKinetic1.Visible = True
                    divKinetic.Visible = False
                    txtPortKinetic1.Text = dtDevice.Rows(0)("port")
                    txtIpAddress2.Text = dtDevice.Rows(0)("printer_ip_address")

                Else
                    Device_SubType.Visible = False
                        divPrinter.Visible = False
                        divEVO.Visible = False
                    End If
                End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Device_Master:BindDevice" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("device_id") = Nothing Then
                Clear()
            Else
                BindDevice()
            End If
        Catch ex As Exception
            LogHelper.Error("Device_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Try
            Response.Redirect("Device_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Device_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtDName.Text = ""
            txtCode.Text = ""
            txtSerialNo.Text = ""
            ddMachine.SelectedIndex = 0
            ddDeviceType.SelectedIndex = 0
            chkActive.Checked = True
            txtBudrate.Text = ""
            txtDeviceName.Text = ""
            txtIpAddress.Text = ""
            txtport.Text = ""
            txtvenderid.Text = ""
            ddNetworkType.ClearSelection()
            txtwidth.Text = ""
            txtUserName.Text = ""
            txtpassword.Text = ""
            TxtAPI.Text = ""
            txtServicekey.Text = ""
            txtblutoothName.Text = ""
            divEVO.Visible = False
            txtUserName1.Text = ""
            txtpassword1.Text = ""
            txtIpAddress1.Text = ""
            txtIpAddress2.Text = ""
        Catch ex As Exception
            LogHelper.Error("Device_Master:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsDevice.cmp_id = Val(Session("cmp_id"))
            oclsDevice.name = txtDName.Text.Trim()
            oclsDevice.code = txtCode.Text.Trim()
            oclsDevice.machine_id = Val(ddMachine.SelectedValue)
            oclsDevice.serial_no = txtSerialNo.Text
            oclsDevice.ip_address = Request.UserHostAddress
            oclsDevice.login_id = Val(Session("login_id"))
            oclsDevice.Device_Type_id = Val(ddDeviceType.SelectedValue)
            oclsDevice.cat_id = Val(ddlCategory.SelectedValue)

            oclsDevice.is_active = IIf(chkActive.Checked = True, 1, 0)

            If (ddDeviceType.SelectedItem.Text).ToString = "Kinetic" Or (ddDeviceType.SelectedItem.Text).ToString = "Kinetic Saturn" Then
                oclsDevice.User_Name = txtUserName1.Text
                If txtpassword1.Text = "" Then
                    oclsDevice.Password = hfPassword.Value
                Else
                    oclsDevice.Password = txtpassword1.Text
                End If
                oclsDevice.Device_SubType = Val(0)
            Else
                If (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "PRINTER" Then
                    oclsDevice.Device_SubType = Val(ddDeviceSubType.SelectedValue)
                Else
                    oclsDevice.Device_SubType = Val(0)
                End If
                oclsDevice.User_Name = txtUserName.Text
                oclsDevice.Password = txtpassword.Text
            End If
            If (ddDeviceType.SelectedItem.Text).ToString = "Castles Pay" Or (ddDeviceType.SelectedItem.Text).ToString = "Nucleus" Or (ddDeviceType.SelectedItem.Text).ToString = "Pax Communicator" Or (ddDeviceType.SelectedItem.Text).ToString = "Pax A920" Or (ddDeviceType.SelectedItem.Text).ToString = "Ingenico" Then


                oclsDevice.Device_SubType = Val(0)

            Else
                If (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "PRINTER" Then
                    oclsDevice.Device_SubType = Val(ddDeviceSubType.SelectedValue)
                Else
                    oclsDevice.Device_SubType = Val(0)
                End If
            End If
            oclsDevice.Bluetooth_Name = txtblutoothName.Text
            oclsDevice.API = TxtAPI.Text
            oclsDevice.Service_Key = txtServicekey.Text

            oclsDevice.network_type = IIf(ddNetworkType.SelectedItem.Value = "SELECT", "", ddNetworkType.SelectedItem.Value)

            If txtwidth.Text = "" Or txtwidth.Text = "0" Then
                If ddNetworkType.SelectedItem.Value.ToString() = "Serial Port" Then
                    oclsDevice.Width = 40
                ElseIf ddNetworkType.SelectedItem.Value.ToString() = "LAN" Then
                    oclsDevice.Width = 48
                Else
                    oclsDevice.Width = IIf(txtwidth.Text = "", 0, txtwidth.Text)
                End If
            Else
                oclsDevice.Width = IIf(txtwidth.Text = "", 0, txtwidth.Text)
            End If


            If (ddDeviceType.SelectedItem.Text).ToString = "Kinetic" Or (ddDeviceType.SelectedItem.Text).ToString = "Kinetic Saturn" Then
                oclsDevice.printer_ip_address = txtIpAddress1.Text
                oclsDevice.port = txtPortKinetic.Text
                oclsDevice.budrate = ""
                oclsDevice.device_name = ""
                oclsDevice.vender_id = 0
            ElseIf (ddDeviceType.SelectedItem.Text).ToString = "Castles Pay" Or (ddDeviceType.SelectedItem.Text).ToString = "Nucleus" Or ddDeviceType.SelectedItem.Text.ToString = "Pax Communicator" Or (ddDeviceType.SelectedItem.Text).ToString = "Pax A920" Or (ddDeviceType.SelectedItem.Text).ToString = "Ingenico" Then
                oclsDevice.printer_ip_address = txtIpAddress2.Text
                oclsDevice.port = txtPortKinetic1.Text
                oclsDevice.budrate = ""
                oclsDevice.device_name = ""
                oclsDevice.vender_id = 0

            Else
                If ddNetworkType.SelectedItem.Value.ToString() = "Serial Port" Then
                    oclsDevice.budrate = txtBudrate.Text
                    oclsDevice.device_name = txtDeviceName.Text
                    oclsDevice.vender_id = 0
                    oclsDevice.printer_ip_address = ""
                    oclsDevice.port = 0
                ElseIf ddNetworkType.SelectedItem.Value.ToString() = "USB" Then
                    oclsDevice.vender_id = Val(txtvenderid.Text)
                    oclsDevice.printer_ip_address = ""
                    oclsDevice.port = 0
                    oclsDevice.budrate = ""
                    oclsDevice.device_name = ""
                ElseIf ddNetworkType.SelectedItem.Value.ToString() = "LAN" Then
                    oclsDevice.printer_ip_address = txtIpAddress.Text
                    oclsDevice.port = Val(txtport.Text)
                    oclsDevice.budrate = ""
                    oclsDevice.device_name = ""
                    oclsDevice.vender_id = 0
                Else
                    oclsDevice.printer_ip_address = ""
                    oclsDevice.port = 0
                    oclsDevice.budrate = ""
                    oclsDevice.device_name = ""
                    oclsDevice.vender_id = 0
                End If
            End If

            'If (ddDeviceType.SelectedItem.Text).ToString = "Castles Pay" Or (ddDeviceType.SelectedItem.Text).ToString = "Nucleus" Or ddDeviceType.SelectedItem.Text.ToString = "Pax Communicator" Then
            '    oclsDevice.printer_ip_address = txtIpAddress2.Text
            '    oclsDevice.port = txtPortKinetic1.Text
            '    oclsDevice.budrate = ""
            '    oclsDevice.device_name = ""
            '    oclsDevice.vender_id = 0
            'Else
            '    If ddNetworkType.SelectedItem.Value.ToString() = "Serial Port" Then
            '        oclsDevice.budrate = txtBudrate.Text
            '        oclsDevice.device_name = txtDeviceName.Text
            '        oclsDevice.vender_id = 0
            '        oclsDevice.printer_ip_address = ""
            '        oclsDevice.port = 0
            '    ElseIf ddNetworkType.SelectedItem.Value.ToString() = "USB" Then
            '        oclsDevice.vender_id = Val(txtvenderid.Text)
            '        oclsDevice.printer_ip_address = ""
            '        oclsDevice.port = 0
            '        oclsDevice.budrate = ""
            '        oclsDevice.device_name = ""
            '    ElseIf ddNetworkType.SelectedItem.Value.ToString() = "LAN" Then
            '        oclsDevice.printer_ip_address = txtIpAddress.Text
            '        oclsDevice.port = Val(txtport.Text)
            '        oclsDevice.budrate = ""
            '        oclsDevice.device_name = ""
            '        oclsDevice.vender_id = 0
            '    Else
            '        oclsDevice.printer_ip_address = ""
            '        oclsDevice.port = 0
            '        oclsDevice.budrate = ""
            '        oclsDevice.device_name = ""
            '        oclsDevice.vender_id = 0
            '    End If
            'End If

            If Session("device_id") = Nothing Then
                oclsDevice.device_id = 0
                oclsDevice.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsDevice.device_id = Val(Session("device_id"))
                oclsDevice.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("device_id") = Nothing
            Response.Redirect("Device_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Device_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub ddDeviceType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddDeviceType.SelectedIndexChanged
        Try
            If (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "PRINTER" Or ddDeviceType.SelectedItem.Text.ToString.ToUpper = "CASH CAMERA" Then
                If (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "PRINTER" Then
                    Device_SubType.Visible = True
                Else
                    Device_SubType.Visible = False
                End If
                divPrinter.Visible = True
                ddNetworkType.SelectedIndex = 0
                divSerialPort.Visible = False
                divLAN.Visible = False
                divUSB.Visible = False
                divEVO.Visible = False
                divKinetic.Visible = False
                divKinetic1.Visible = False
            ElseIf (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "EVO" Or (ddDeviceType.SelectedItem.Text).ToString.ToUpper = "PAY WORKS" Then
                Device_SubType.Visible = False
                divEVO.Visible = True
                divPrinter.Visible = False
                divSerialPort.Visible = False
                divLAN.Visible = False
                divUSB.Visible = False
                divKinetic.Visible = False
                divKinetic1.Visible = False
                txtUserName.Text = ""
                txtpassword.Text = ""
                TxtAPI.Text = ""
                txtServicekey.Text = ""
                txtblutoothName.Text = ""
            ElseIf (ddDeviceType.SelectedItem.Text).ToString = "Kinetic" Or (ddDeviceType.SelectedItem.Text).ToString = "Kinetic Saturn" Then
                Device_SubType.Visible = False
                divKinetic.Visible = True
                divKinetic1.Visible = False

                divEVO.Visible = False
                divPrinter.Visible = False
                divSerialPort.Visible = False
                divLAN.Visible = False
                divUSB.Visible = False
                txtUserName.Text = ""
                txtpassword.Text = ""
                txtIpAddress1.Text = ""

            ElseIf (ddDeviceType.SelectedItem.Text).ToString = "Castles Pay" Or (ddDeviceType.SelectedItem.Text).ToString = "Nucleus" Or (ddDeviceType.SelectedItem.Text).ToString = "Pax Communicator" Or (ddDeviceType.SelectedItem.Text).ToString = "Pax A920" Or (ddDeviceType.SelectedItem.Text).ToString = "Ingenico" Then
                Device_SubType.Visible = False
                divKinetic.Visible = False
                divKinetic1.Visible = True

                divEVO.Visible = False
                divPrinter.Visible = False
                divSerialPort.Visible = False
                divLAN.Visible = False
                divUSB.Visible = False
                txtUserName.Text = ""
                txtpassword.Text = ""
                txtIpAddress2.Text = ""

            Else
                Device_SubType.Visible = False
                divPrinter.Visible = False
                divEVO.Visible = False
                divKinetic.Visible = False
                divKinetic1.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddNetworkType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddNetworkType.SelectedIndexChanged
        Try
            If ddNetworkType.SelectedItem.Value.ToString() = "Serial Port" Then
                divSerialPort.Visible = True
                divLAN.Visible = False
                divUSB.Visible = False
                divEVO.Visible = False
                txtDeviceName.Text = "/dev/ttyS1"
                txtwidth.Text = "40"
            ElseIf ddNetworkType.SelectedItem.Value.ToString() = "USB" Then
                divSerialPort.Visible = False
                divLAN.Visible = False
                divUSB.Visible = False
                divEVO.Visible = False
                txtwidth.Text = ""
            ElseIf ddNetworkType.SelectedItem.Value.ToString() = "LAN" Then
                divSerialPort.Visible = False
                divLAN.Visible = True
                divUSB.Visible = False
                divEVO.Visible = False
                txtwidth.Text = "48"
            End If
        Catch ex As Exception
            LogHelper.Error("Printer_Master:ddNetworkType_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindDeviceType(ddDeviceType, Val(Session("cmp_id")), Val(ddlCategory.SelectedValue))
        Catch ex As Exception
            LogHelper.Error("Printer_Master:ddlCategory_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
End Class

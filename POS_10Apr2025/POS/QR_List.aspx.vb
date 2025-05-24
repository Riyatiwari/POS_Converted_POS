Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Drawing.Imaging
Imports ZXing
Imports ZXing.Common
Imports System.Web.UI.WebControls
Imports System.Globalization
Imports System.Web.Configuration

Partial Class QR_List

    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsDept As New Controller_clsDepartment
    Protected imgQR As System.Web.UI.WebControls.Image
    Protected linkQR As System.Web.UI.WebControls.Image
    Private oclsMachine As Object

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindMachineList()
            End If


            For Each item As RepeaterItem In rdDepartment.Items
                Dim tillUUIDLabel As Label = TryCast(item.FindControl("lblTillUUID"), Label)
                Dim imgQRCode As System.Web.UI.WebControls.Image = TryCast(item.FindControl("imgQR"), System.Web.UI.WebControls.Image)
                Dim linkQRCode As System.Web.UI.WebControls.Image = TryCast(item.FindControl("LinkQR"), System.Web.UI.WebControls.Image)


                If tillUUIDLabel IsNot Nothing AndAlso imgQRCode IsNot Nothing Then
                    Dim tillUUID As String = tillUUIDLabel.Text

                    If Not String.IsNullOrEmpty(tillUUID) Then
                        Dim qrCode As Bitmap = GenerateQRCode(tillUUID)
                        DisplayQRCode(qrCode, imgQRCode)
                        Dim linkQRCodeUrl As String = GenerateLinkQRCodeUrl(tillUUID)
                        linkQRCode.ImageUrl = linkQRCodeUrl

                    Else

                        imgQRCode.Visible = False

                        'tt.visible = False
                        'type1.visible = False
                        LogHelper.Error("TillUUID is null or empty for item with machine_name: " & TryCast(item.FindControl("lblMachineName"), Label).Text)
                    End If
                End If
            Next


        Catch ex As Exception
            LogHelper.Error("Qr_List:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub BindMachineList()
        oclsDept.Cmp_id = Val(Session("cmp_id"))
        Dim dtQR As DataTable = oclsDept.QrList()

        If dtQR.Rows.Count > 0 Then
            rdDepartment.DataSource = dtQR
            rdDepartment.DataBind()
        Else
            rdDepartment.DataSource = String.Empty
            rdDepartment.DataBind()
        End If
    End Sub
    Protected Sub rdDepartment_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdDepartment.ItemCommand


        If e.CommandName = "GenerateQR" Then
            Dim tillUUID As String = e.CommandArgument.ToString()
            Dim qrCode As Bitmap = GenerateQRCode(tillUUID)

            DisplayQRCode(qrCode, TryCast(e.Item.FindControl("imgQR"), System.Web.UI.WebControls.Image))
            Dim linkqrCode As Bitmap = GenerateLinkQRCode(tillUUID)
            DisplayQRCode(qrCode, TryCast(e.Item.FindControl("LinkQR"), System.Web.UI.WebControls.Image))

        End If
    End Sub

    Protected Function GetMachineListFromDatabase(ByVal cmpId As Integer) As DataTable

        Return Nothing
    End Function



    Private Sub DisplayQRCode(ByVal qrCode As Bitmap, ByVal imgQR As System.Web.UI.WebControls.Image)
        Try
            If imgQR IsNot Nothing AndAlso qrCode IsNot Nothing Then
                Dim stream As New MemoryStream()


                qrCode.Save(stream, Imaging.ImageFormat.Png)
                imgQR.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(stream.ToArray())
            Else
                imgQR.ImageUrl = "path_to_error_image.png"
            End If
        Catch ex As Exception
            LogHelper.Error("QR_List:DisplayQRCode - " + ex.Message)
        End Try
    End Sub

    Protected Sub btnGenerateQR_Click(sender As Object, e As EventArgs)
        Try

            Dim btnGenerateQR As Button = DirectCast(sender, Button)

            Dim item As RepeaterItem = DirectCast(btnGenerateQR.NamingContainer, RepeaterItem)

            If item IsNot Nothing Then
                Dim machineId As HiddenField = TryCast(item.FindControl("hfmachine_id"), HiddenField)
                Dim venueId As HiddenField = TryCast(item.FindControl("hfvenue_id"), HiddenField)
                Dim locationId As HiddenField = TryCast(item.FindControl("hflocation_id"), HiddenField)
                Dim machineNameLabel As Label = TryCast(item.FindControl("lblMachineName"), Label)

                If machineNameLabel IsNot Nothing AndAlso machineId IsNot Nothing AndAlso venueId IsNot Nothing AndAlso locationId IsNot Nothing Then
                    Dim machineName As String = machineNameLabel.Text
                    Dim branchIdValue As String = machineId.Value
                    Dim venueIdValue As String = venueId.Value
                    Dim locationIdValue As String = locationId.Value

                    If Not String.IsNullOrEmpty(machineName) AndAlso Not String.IsNullOrEmpty(branchIdValue) AndAlso Not String.IsNullOrEmpty(venueIdValue) AndAlso Not String.IsNullOrEmpty(locationIdValue) Then

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
                            Session("StoreUUID") = Session("conStoreuuid")
                            If Session("conStoreuuid").ToString() IsNot Nothing Then

                                Session("StoreUUID") = Session("conStoreuuid")

                                Dim enlogin As New Controller_clsLogin()
                                enlogin.Store_UUID = Session("conStoreuuid")

                                Dim dtlogin As DataTable = enlogin.enablelogin()

                            End If

                        Else
                            Dim storeUuid As String = Session("conStoreuuid").ToString()
                            Dim objclsLogin As New Controller_clsLogin()
                            objclsLogin.Store_UUID = storeUuid

                            Dim dtlogin As DataTable = objclsLogin.enablelogin()
                            Session("StoreUUID") = Session("conStoreuuid")


                        End If


                        Dim TillUUID = Guid.NewGuid().ToString
                        Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
                        strcon.Open()

                        Dim cmd As SqlCommand = New SqlCommand("P_M_QR_Register", strcon)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@venue_id", Val(venueId.Value))

                        cmd.Parameters.AddWithValue("@machine_id", Val(branchIdValue))
                        cmd.Parameters.AddWithValue("@location_id", Val(locationIdValue))
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

                        Dim oclsMachine As New clsMachine()

                        oclsMachine.cmp_id = Val(Session("cmp_id"))
                        oclsMachine.name = machineName

                        oclsMachine.login_id = Val(Session("login_id"))
                        oclsMachine.machine_id = Val(branchIdValue)
                        oclsMachine.location_id = Val(locationIdValue)
                        oclsMachine.TillUUID = TillUUID

                        oclsMachine.generateQR()


                        Session("Success") = "QR successfully Generated"
                        'End If
                        Session("machine_id") = Nothing
                        ' Response.Redirect("Machine_List.aspx", False)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "alert('Invalid data for generating QR code');", True)
                    End If
                End If
            End If

            BindMachineList()

        Catch ex As Exception
            LogHelper.Error("QR_List:btnGenerateQR_Click:" + ex.Message)
        End Try
    End Sub
    Private Function GenerateQRCode(ByVal data As String) As Bitmap
        Dim writer As BarcodeWriter = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE


        writer.Options = New ZXing.Common.EncodingOptions With {
       .Width = 150,
       .Height = 150
          }

        Try
            Return writer.Write(data)
        Catch ex As Exception
            LogHelper.Error("QR_List:GenerateQRCode: " + ex.Message)
            Return Nothing
        End Try
    End Function
    Private Function GenerateLinkQRCode(ByVal data As String) As Bitmap
        Dim writer As BarcodeWriter = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE

        Dim formattedData As String = String.Format("http://{0}.com", data)
        writer.Options = New ZXing.Common.EncodingOptions With {
    .Width = 150,
    .Height = 150
}

        Try
            Return writer.Write(formattedData)
        Catch ex As Exception
            LogHelper.Error("QR_List:GenerateLinkQRCode: " + ex.Message)
            Return Nothing
        End Try
    End Function
    Private Function GenerateLinkQRCode1(ByVal data As String) As Bitmap
        Dim writer As BarcodeWriter = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE
        Dim str = WebConfigurationManager.AppSettings("QR_URL")
        Dim formattedData As String = String.Format("{0}#http://{1}.com", str, data)
        writer.Options = New ZXing.Common.EncodingOptions With {
    .Width = 150,
    .Height = 150
}

        Try
            Return writer.Write(formattedData)
        Catch ex As Exception
            LogHelper.Error("QR_List:GenerateLinkQRCode: " + ex.Message)
            Return Nothing
        End Try
    End Function

    Protected Function GenerateQRCodeUrl(ByVal tillUUID As Object) As String
        If tillUUID IsNot Nothing AndAlso Not String.IsNullOrEmpty(tillUUID.ToString()) Then
            Dim qrCode As Bitmap = GenerateQRCode(tillUUID.ToString())

            If qrCode IsNot Nothing Then
                Dim stream As New MemoryStream()
                qrCode.Save(stream, Imaging.ImageFormat.Png)
                Return "data:image/png;base64," + Convert.ToBase64String(stream.ToArray())
            End If
        End If


        Return "path_to_error_image.png"
    End Function

    Protected Function GenerateLinkQRCodeUrl(ByVal tillUUID As Object) As String
        If tillUUID IsNot Nothing AndAlso Not String.IsNullOrEmpty(tillUUID.ToString()) Then
            Dim link_qrCode As Bitmap = GenerateLinkQRCode(tillUUID.ToString())

            If link_qrCode IsNot Nothing Then
                Dim stream As New MemoryStream()
                link_qrCode.Save(stream, Imaging.ImageFormat.Png)
                Dim base64String As String = Convert.ToBase64String(stream.ToArray())       '"http://tilluuid.com/" & tillUUID.ToString()
                Return "data:image/png;base64," + base64String

            End If
        End If


        Return "path_to_error_image.png"

    End Function
    Protected Function GenerateAllinOneQRCodeUrl(ByVal tillUUID As Object) As String
        If tillUUID IsNot Nothing AndAlso Not String.IsNullOrEmpty(tillUUID.ToString()) Then
            Dim link_qrCode As Bitmap = GenerateLinkQRCode1(tillUUID.ToString())

            If link_qrCode IsNot Nothing Then
                Dim stream As New MemoryStream()
                link_qrCode.Save(stream, Imaging.ImageFormat.Png)
                Dim base64String As String = Convert.ToBase64String(stream.ToArray())       '"http://tilluuid.com/" & tillUUID.ToString()
                Return "data:image/png;base64," + base64String

            End If
        End If


        Return "path_to_error_image.png"

    End Function
    Protected Sub GenerateCode(ByVal tillUUID As Object, ByVal item As RepeaterItem)
        Try
            If tillUUID IsNot Nothing AndAlso Not String.IsNullOrEmpty(tillUUID.ToString()) Then
                Dim tillUUIDLabel As Label = TryCast(item.FindControl("lblTillUUID"), Label)
                Dim tillUUIDlbl As String = tillUUIDLabel.Text
                Dim generatedCode As String
                Dim validTime As DateTime
                Dim litGeneratedCodeTop As Literal = TryCast(item.FindControl("litGeneratedCodeTop"), Literal)
                Dim litGeneratedCodeTop1 As Literal = TryCast(item.FindControl("litGeneratedCodeTop1"), Literal)
                Dim litGeneratedCodeBottom As Literal = TryCast(item.FindControl("litGeneratedCodeBottom"), Literal)


                GetGeneratedCode(tillUUIDlbl, generatedCode, validTime)

                If Not String.IsNullOrEmpty(generatedCode) AndAlso validTime >= DateTime.Now Then
                    litGeneratedCodeTop.Text = " Code: " & generatedCode
                    Dim str = WebConfigurationManager.AppSettings("QR_URL")
                    litGeneratedCodeTop1.Text = "  <br/>AllInOne Code:<br/> " & str & "#" & generatedCode
                    litGeneratedCodeBottom.Text = "(Valid for 30 min)"
                    litGeneratedCodeTop1.Visible = True
                    litGeneratedCodeTop.Visible = True
                    litGeneratedCodeBottom.Visible = True
                Else
                    litGeneratedCodeTop1.Visible = False
                    litGeneratedCodeTop.Visible = False
                    litGeneratedCodeBottom.Visible = False
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Error in GenerateCode: " + ex.Message)
        End Try
    End Sub
    Protected Sub GenerateCode_Click(sender As Object, e As EventArgs)
        Try
            Dim tillUUID As String = TryCast((CType(sender, Button)).CommandArgument, String)

            If Not String.IsNullOrEmpty(tillUUID) Then
                If CanButtonClick(tillUUID) Then
                    Dim random As New Random()
                    Dim generatedCode As Integer = random.Next(100000, 999999)

                    Using strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
                        strcon.Open()

                        Using cmd As SqlCommand = New SqlCommand("P_M_generateforCode_controller", strcon)
                            cmd.CommandType = CommandType.StoredProcedure

                            cmd.Parameters.AddWithValue("@Generate_code", generatedCode)
                            cmd.Parameters.AddWithValue("@TillUUID", tillUUID)
                            cmd.Parameters.AddWithValue("@Valid", DateTime.Now.AddMinutes(30))

                            cmd.ExecuteNonQuery()


                            Dim litGeneratedCodeTop As Literal = DirectCast(sender.NamingContainer.FindControl("litGeneratedCodeTop"), Literal)
                            Dim litGeneratedCodeTop1 As Literal = DirectCast(sender.NamingContainer.FindControl("litGeneratedCodeTop1"), Literal)
                            Dim litGeneratedCodeBottom As Literal = DirectCast(sender.NamingContainer.FindControl("litGeneratedCodeBottom"), Literal)
                            UpdateGeneratedCodeLabels(tillUUID, litGeneratedCodeTop, litGeneratedCodeTop1, litGeneratedCodeBottom)
                            litGeneratedCodeTop.Text = " Code: " & generatedCode
                            Dim str = WebConfigurationManager.AppSettings("QR_URL")
                            litGeneratedCodeTop1.Text = "  <br/>AllInOne Code:<br/> " & str & "#" & generatedCode
                            litGeneratedCodeBottom.Text = "(Valid for 30 min)"
                        End Using
                    End Using
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonClickError", "alert('Button cannot be clicked again within 30 minutes.');", True)
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("QR_List:GenerateCode_Click:" + ex.Message)
        End Try
    End Sub
    Private Sub HideGeneratedCode(state As Object)

        Dim tupleState As Tuple(Of Literal, Literal) = DirectCast(state, Tuple(Of Literal, Literal))

        If tupleState IsNot Nothing Then
            Dim litGeneratedCodeTop As Literal = tupleState.Item1
            Dim litGeneratedCodeTop1 As Literal = tupleState.Item1
            Dim litGeneratedCodeBottom As Literal = tupleState.Item2
            litGeneratedCodeTop.Visible = False
            litGeneratedCodeTop1.Visible = False
            litGeneratedCodeBottom.Visible = False
        End If
    End Sub

    Private Function CanButtonClick(tillUUID As String) As Boolean

        Return True
    End Function


    Private Sub UpdateGeneratedCodeLabels(tillUUID As String, litGeneratedCodeTop As Literal, litGeneratedCodeTop1 As Literal, litGeneratedCodeBottom As Literal)
        Try
            Dim generatedCode As String
            Dim validTime As DateTime
            GetGeneratedCode(tillUUID, generatedCode, validTime)

            'Dim litGeneratedCodeTop As Literal = TryCast(item.FindControl("litGeneratedCodeTop"), Literal)
            ' Dim litGeneratedCodeBottom As Literal = TryCast(item.FindControl("litGeneratedCodeBottom"), Literal)

            If Not String.IsNullOrEmpty(generatedCode) AndAlso validTime >= DateTime.Now Then
                litGeneratedCodeTop.Text = " Code: " & generatedCode
                Dim str = WebConfigurationManager.AppSettings("QR_URL")
                litGeneratedCodeTop1.Text = "  <br/>AllInOne Code:<br/> " & str & "#" & generatedCode
                litGeneratedCodeBottom.Text = "(Valid for 30 min)"
                litGeneratedCodeTop.Visible = True
                litGeneratedCodeTop1.Visible = True
                litGeneratedCodeBottom.Visible = True
            Else
                litGeneratedCodeTop.Visible = False
                litGeneratedCodeTop1.Visible = False
                litGeneratedCodeBottom.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("QR_List:UpdateGeneratedCodeLabels - " + ex.Message)
        End Try
    End Sub

    Protected Sub GetGeneratedCode(tillUUID As String, ByRef generatedCode As String, ByRef validTime As DateTime)
        Try
            Using strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
                strcon.Open()

                Dim cmd As SqlCommand = New SqlCommand("GetGeneratedCode", strcon)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@TillUUID", tillUUID)
                cmd.Parameters.Add("@GenerateCode", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Valid", SqlDbType.DateTime).Direction = ParameterDirection.Output

                cmd.ExecuteNonQuery()

                generatedCode = Convert.ToString(cmd.Parameters("@GenerateCode").Value)
                validTime = Convert.ToDateTime(cmd.Parameters("@Valid").Value)

            End Using

        Catch ex As Exception
            LogHelper.Error("QR_List:GetGeneratedCode:" + ex.Message)
        End Try
    End Sub





    Protected Sub rdDepartment_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rdDepartment.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Try
                Dim tillUUID As String = DataBinder.Eval(e.Item.DataItem, "TillUUID").ToString()

                If CanButtonClick(tillUUID) Then
                    Dim generatedCode As String = ""
                    Dim validTime As DateTime

                    GetGeneratedCode(tillUUID, generatedCode, validTime)

                    Dim litGeneratedCodeTop As Literal = DirectCast(e.Item.FindControl("litGeneratedCodeTop"), Literal)
                    Dim litGeneratedCodeTop1 As Literal = DirectCast(e.Item.FindControl("litGeneratedCodeTop1"), Literal)
                    Dim litGeneratedCodeBottom As Literal = DirectCast(e.Item.FindControl("litGeneratedCodeBottom"), Literal)
                    If Not String.IsNullOrEmpty(generatedCode) AndAlso validTime >= DateTime.Now Then
                        litGeneratedCodeTop.Text = " Code: " & generatedCode
                        Dim str = WebConfigurationManager.AppSettings("QR_URL")
                        litGeneratedCodeTop1.Text = "  <br/>AllInOne Code:<br/> " & str & "#" & generatedCode
                        litGeneratedCodeBottom.Text = "(Valid for 30 min)"
                        litGeneratedCodeTop.Visible = True
                        litGeneratedCodeTop1.Visible = True
                        litGeneratedCodeBottom.Visible = True
                    Else
                        litGeneratedCodeTop.Visible = False
                        litGeneratedCodeTop1.Visible = False
                        litGeneratedCodeBottom.Visible = False
                    End If
                Else

                End If
            Catch ex As Exception
                LogHelper.Error("QR_List:ItemDataBound:" + ex.Message)
            End Try
        End If
    End Sub




End Class
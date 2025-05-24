Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Net
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports System.IO

Partial Class API_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim objclsLocation As New clsLocation()
    Dim oclsLocation As New Controller_clsLocation()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))

                divCloud_Key.Visible = False
                divCloud_Value.Visible = False
                divCloud_url.Visible = False

                divbarurl.Visible = False

                divbartoken.Visible = False
                'txtCLDvalue.Visible = False
                'txtUrl.Visible = False
                'txtcloudkey.Visible = False

                If Not Session("cloud_id") = Nothing Then
                    BindLocation()


                End If

            End If
        Catch ex As Exception
            LogHelper.Error("API_Master:Page_Load" + ex.Message)
        End Try
    End Sub



    Private Sub BindLocation()
        Try
            Dim sender As Object
            Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            oclsLocation.location_id = Val(Session("location_id"))
            oclsLocation.cloud_id = Val(Session("cloud_id"))
            Dim dtLocation As DataTable = oclsLocation.SelectAllApi()
            If dtLocation.Rows.Count > 0 Then



                Try



                    If Not Val(dtLocation.Rows(0)("onlinepayment")) = Nothing Then

                        ddlcloud.FindItemByValue(Val(dtLocation.Rows(0)("onlinepayment"))).Selected = True
                        If ((dtLocation.Rows(0)("onlinepayment").ToString() = "1")) Then
                            'divJudoPayid.Visible = True
                            'divJudoTokenid.Visible = True
                            'divJudoSecretid.Visible = True
                            divCloud_Key.Visible = True
                            divCloud_Value.Visible = True
                            divCloud_url.Visible = True
                            divbarurl.Visible = False

                            divbartoken.Visible = False
                        ElseIf ((dtLocation.Rows(0)("onlinepayment").ToString() = "2")) Then
                            divCloud_Key.Visible = False
                            divCloud_Value.Visible = False
                            divCloud_url.Visible = False
                            divbarurl.Visible = True

                            divbartoken.Visible = True
                        End If

                    End If

                Catch ex As Exception
                    LogHelper.Error("Location_Master:BindLocation" + ex.Message)
                End Try



                If dtLocation.Rows(0)("onlinepayment").ToString = "1" Then
                    ddlcloud.SelectedValue = 1

                    divCloud_Key.Visible = True
                    divCloud_Value.Visible = True
                    divCloud_url.Visible = True
                    divbarurl.Visible = False

                    divbartoken.Visible = False
                ElseIf dtLocation.Rows(0)("onlinepayment").ToString = "2" Then
                    ddlcloud.SelectedValue = 2
                    divCloud_Key.Visible = False
                    divCloud_Value.Visible = False
                    divCloud_url.Visible = False
                    divbarurl.Visible = True

                    divbartoken.Visible = True


                End If
                txtCLDvalue.Text = dtLocation.Rows(0)("value").ToString
                txtcloudkey.Text = dtLocation.Rows(0)("api_key").ToString
                txtUrl.Text = dtLocation.Rows(0)("url").ToString


                txtbarToken.Text = dtLocation.Rows(0)("value").ToString
                txtbarurl.Text = dtLocation.Rows(0)("url").ToString
                ' Session("")
                If Not Val(dtLocation.Rows(0)("cloud_id")) = 0 Then
                    radLocation.ClearSelection()
                    radLocation.FindItemByValue(Val(dtLocation.Rows(0)("Location_ID"))).Selected = True
                End If




            End If





        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("API_Master:BindLocation:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnreset_click(sender As Object, e As EventArgs) Handles btnreset.click
        Try
            If Session("cloud_id") = Nothing Then
                Clear()
            Else
                BindLocation()
            End If
        Catch ex As Exception
            LogHelper.Error("API_Master:btnreset_click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Api_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Api_Master:btnCancel_Click" + ex.Message)
        End Try

    End Sub

    Private Sub Clear()
        Try



        Catch ex As Exception
            LogHelper.Error("API_Master:Clear" + ex.Message)
        End Try

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))

            oclsLocation.onlinepayment = Val(IIf(ddlcloud.SelectedValue = "", 0, ddlcloud.SelectedValue))

            oclsLocation.location_id = IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0")


            'for BarStock




            If ddlcloud.SelectedValue = "1" Then
                oclsLocation.cloudValue = txtCLDvalue.Text
                oclsLocation.cloudKey = txtcloudkey.Text
                oclsLocation.cloudurl = txtUrl.Text
            ElseIf ddlcloud.SelectedValue = "2" Then
                oclsLocation.cloudValue = txtbarToken.Text
                oclsLocation.cloudKey = "Token"

                oclsLocation.cloudurl = txtbarurl.Text
            End If



            If Session("cloud_id") = Nothing Then

                oclsLocation.CloudApiInsert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsLocation.cloud_id = Val(Session("cloud_id"))
                oclsLocation.CloudApiupdate()
                Session("Success") = "Record updated successfully"

            End If



            Session("cloud_id") = Nothing
            Response.Redirect("Api_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("API_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub



    Protected Sub ddlcloud_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim Check As String = ddlcloud.SelectedItem.Text

            If Check = "Cloud Net API" Then
                txtCLDvalue.Visible = True
                txtUrl.Visible = True
                txtcloudkey.Visible = True
                divCloud_url.Visible = True
                divCloud_Key.Visible = True
                divCloud_Value.Visible = True
                divbartoken.Visible = False
                divbarurl.Visible = False
            ElseIf Check = "BarStock Exchange" Then
                divCloud_url.Visible = False
                divCloud_Key.Visible = False
                divCloud_Value.Visible = False


                divbartoken.Visible = True
                divbarurl.Visible = True

                'ElseIf Check = "CustomPay" Then
                '    divJudoPayid.Visible = False
                '    divJudoTokenid.Visible = False
                '    divJudoSecretid.Visible = False
                '    divCashflowId.Visible = False
                '    divCashflowUrl.Visible = False
                '    divCashflowAPIKey.Visible = False
                '    divCustomPayId.Visible = True
                '    divCustomPaySecret.Visible = True
                '    divCustomPayToken.Visible = True
                '    divCustomPayBase64.Visible = True
                '    divCustomPayURL.Visible = True
                'ElseIf Check = "CardStream" Then
                '    divJudoPayid.Visible = False
                '    divJudoTokenid.Visible = False
                '    divJudoSecretid.Visible = False
                '    divCashflowId.Visible = True
                '    divCashflowUrl.Visible = True
                '    divCashflowAPIKey.Visible = True
                '    divCustomPayId.Visible = False
                '    divCustomPaySecret.Visible = False
                '    divCustomPayToken.Visible = False
                '    divCustomPayBase64.Visible = False
                '    divCustomPayURL.Visible = False
                'Else
                '    divJudoPayid.Visible = False
                '    divJudoTokenid.Visible = False
                '    divJudoSecretid.Visible = False
                '    divCashflowId.Visible = False
                '    divCashflowUrl.Visible = False
                '    divCashflowAPIKey.Visible = False
                '    divCustomPayId.Visible = False
                '    divCustomPaySecret.Visible = False
                '    divCustomPayToken.Visible = False
                '    divCustomPayBase64.Visible = False
                '    divCustomPayURL.Visible = False
            End If



        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("API_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    'Private Sub BindStore()
    '    Try
    '        Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
    '        strcon.Open()
    '        Dim cmd As SqlCommand = New SqlCommand("Get_StoreName", strcon)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
    '        Dim dt As DataTable = New DataTable()
    '        adp.Fill(dt)
    '        cmd.ExecuteNonQuery()
    '        strcon.Close()
    '        If dt.Rows.Count > 0 Then
    '            rad_StoreName.DataSource = dt
    '            rad_StoreName.DataTextField = "store_name"
    '            rad_StoreName.DataValueField = "store_id"
    '            rad_StoreName.DataBind()
    '        End If
    '        Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
    '        rad_StoreName.Items.Insert(0, li)

    '    Catch ex As Exception
    '        LogHelper.Error("Location_Master:BindStore" + ex.Message)
    '    End Try
    'End Sub

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function


End Class

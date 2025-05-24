Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO

Imports Telerik.Web.UI.OrgChartStyles



Partial Class Customer_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsCust As New Controller_clsCustomer



    Public Sub bindGrid()
        Try
            oclsCust.cmp_id = Val(Session("cmp_id"))
            Dim dtCustomer As DataTable = oclsCust.SelectAll()

            If dtCustomer.Rows.Count > 0 Then
                rdCustomer.DataSource = dtCustomer
                rdCustomer.DataBind()
            Else
                rdCustomer.DataSource = String.Empty
                rdCustomer.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Customer_List:bindGrid" + ex.Message)
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("add") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("add") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Or Val(ViewState("delete")) = 1 Then
                    divCustomer.Visible = True
                Else
                    divCustomer.Visible = False
                End If
                If ViewState("add") = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Customer_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Customer"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_add = 1 Then
                ViewState("add") = 1
            Else
                ViewState("add") = 0
            End If
            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If
            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception

            LogHelper.Error("Customer_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    Protected Sub rdCustomer_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("customer_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Customer_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                delete(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Customer_List:rdCustomer_ItemCommand" + ex.Message)
        End Try
    End Sub


    Protected Sub delete(ByVal id As Integer)
        Try
            oclsCust.customer_id = Val(id)
            oclsCust.cmp_id = Val(Session("cmp_id"))
            oclsCust.first_name = ""
            oclsCust.last_name = ""
            oclsCust.email = ""
            oclsCust.contact_no = ""
            oclsCust.address = ""
            oclsCust.country_id = 0
            oclsCust.state_id = 0
            oclsCust.city_id = 0
            oclsCust.postal_code = ""
            'oclsCust.is_active = 1
            'oclsCust.ip_address = ""
            'oclsCust.login_id = 0
            oclsCust.other_id = ""
            'oclsCust.machine_id = 0
            oclsCust.profile_id = 0
            oclsCust.AccountID = Val(id)
            oclsCust.Is_credit = 0
            oclsCust.CardNumber = 0
            oclsCust.ExpDate = System.DateTime.Now
            'oclsCust.CustomerProfile = ""
            oclsCust.Tran_Type = "D"
            oclsCust.Delete_account()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Customer_List:deleteRole" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("customer_id") = Nothing
            Response.Redirect("Customer_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Customer_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If FileUpload1.PostedFile.FileName = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('File is required');", True)
                Return
            Else
                Dim excelPath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
                FileUpload1.SaveAs(excelPath)
                Dim conString As String = String.Empty
                Dim storedProc As String = String.Empty
                Dim sheet1 As String = String.Empty
                Dim extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

                Select Case extension
                    Case ".xls" 'Excel 97-03.
                        storedProc = "spx_ImportFromExcel03"
                        conString = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                    Case ".xlsx" 'Excel 07 or higher.
                        storedProc = "spx_ImportFromExcel07"
                        conString = ConfigurationManager.ConnectionStrings("Excel07+ConString").ConnectionString
                End Select

                'Read the Sheet Name.
                Dim dtExcelData As New DataTable()
                conString = String.Format(conString, excelPath)
                Using excel_con As OleDbConnection = New OleDbConnection(conString)
                    excel_con.Open()
                    LogHelper.Error("Customer_List:btnUpload_Click   under excel_con.Open()")
                    sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing).Rows(0)("TABLE_NAME").ToString()
                    excel_con.Close()
                    LogHelper.Error("Customer_List:btnUpload_Click   under excel_con.Open()")
                    Using oda As New OleDbDataAdapter((Convert.ToString("SELECT * FROM [") & sheet1) + "]", excel_con)

                        oda.Fill(dtExcelData)

                    End Using
                End Using

                ViewState("Excel_Data") = dtExcelData

                Dim names As List(Of String) = New List(Of String)

                'For Each column As DataColumn In dtExcelData.Columns
                For i As Integer = 0 To dtExcelData.Rows.Count - 1
                    Try
                        Dim firstName As String = dtExcelData.Rows(i)("first_name").ToString().Trim()
                        If String.IsNullOrEmpty(firstName) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('First name is missing or blank in row {0}');", i + 1), True)
                            Return
                        End If
                        oclsCust.cmp_id = Val(Session("cmp_id"))
                        oclsCust.first_name = dtExcelData.Rows(i)("first_name").ToString()
                        oclsCust.last_name = dtExcelData.Rows(i)("last_name").ToString()
                        oclsCust.email = dtExcelData.Rows(i)("email").ToString()
                        oclsCust.contact_no = dtExcelData.Rows(i)("contact_no").ToString
                        oclsCust.address = dtExcelData.Rows(i)("address").ToString

                        If Session("customer_id") = Nothing Then

                            Dim dt As DataTable = oclsCust.Insert_account_UPLOAD()

                            If dt.Rows.Count > 0 Then
                                '  oclsCust.AccountID = Val(dt.Rows(0)("AccountID"))
                                LogHelper.Error("Customer_List:btnUpload_Click inside for Insert_upload  ")
                                oclsCust.customer_id = 0
                                oclsCust.Insert_upload()

                                ' Session("Success") = "Record inserted successfully"
                            End If

                            'Else
                            '    oclsCust.customer_id = Val(Session("customer_id"))
                            '    oclsCust.Update()

                            '    oclsCust.Update_account()
                            '    '  Session("Success") = "Record updated successfully"
                        End If
                    Catch ex As Exception
                        LogHelper.Error("Error processing row " & (i + 1) & ": " & ex.Message)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error processing row " & (i + 1) & ": " & ex.Message & "');", True)
                        Return
                    End Try

                Next
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record inserted successfully');", True)

            End If
            Session("customer_id") = Nothing

            LogHelper.Error("Customer_List:btnUpload_Click oprocess end  ")
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
        End Try
    End Sub


End Class

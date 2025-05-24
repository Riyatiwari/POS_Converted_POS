Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class GtwayPayment_Import
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsBind As New clsBinding
    Public oSessionManager As New SessionManager
    Dim oclsLocation As Controller_clsLocation = New Controller_clsLocation()
    Dim oclsGtwayPayment As New Controller_clsGtwayPayment

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
            End If

        Catch ex As Exception
            LogHelper.Error("GtwayPayment_Import:Page_Load:" + ex.Message)
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
                    sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing).Rows(0)("TABLE_NAME").ToString()
                    excel_con.Close()
                    Using oda As New OleDbDataAdapter((Convert.ToString("SELECT * FROM [") & sheet1) + "]", excel_con)
                        oda.Fill(dtExcelData)
                    End Using
                End Using

                ViewState("Excel_Data") = dtExcelData

                '---------------------Insert Recored ----------------------------
                Dim dt1 As DataTable = DirectCast(ViewState("Excel_Data"), DataTable)

                For Each row As DataRow In dt1.Rows

                    oclsGtwayPayment.MerchantID = row("MerchantID").ToString()
                    oclsGtwayPayment.StoreID = row("StoreID").ToString()
                    oclsGtwayPayment.TID = row("TerminalID").ToString()
                    oclsGtwayPayment.Date_val = Format(row("Date"), "yyyy/MM/dd hh:mm:ss tt").ToString()
                    oclsGtwayPayment.Time = Format(row("Time"), "hh:mm:ss tt").ToString()
                    oclsGtwayPayment.Ref = row("Ref").ToString()
                    oclsGtwayPayment.RelatedTransaction = row("RelatedTransaction").ToString()
                    oclsGtwayPayment.AcquirerRef = row("AcquirerRef").ToString()
                    oclsGtwayPayment.Type = row("Type").ToString()
                    oclsGtwayPayment.Class_val = row("Class").ToString()
                    oclsGtwayPayment.Currency = row("Currency").ToString()
                    oclsGtwayPayment.Amount = row("Amount").ToString()
                    oclsGtwayPayment.CartID = row("CartID").ToString()
                    oclsGtwayPayment.CartDesc = row("CartDesc").ToString()

                    oclsGtwayPayment.Name = row("Name").ToString()
                    oclsGtwayPayment.Address = row("Address").ToString()
                    oclsGtwayPayment.Country = row("Country").ToString()
                    oclsGtwayPayment.Postcode = row("Postcode").ToString()
                    oclsGtwayPayment.Email = row("Email").ToString()
                    oclsGtwayPayment.Phone = row("Phone").ToString()
                    oclsGtwayPayment.Card = row("Card").ToString()

                    oclsGtwayPayment.Card_Type = row("CardType").ToString()
                    oclsGtwayPayment.CardCountry = row("CardCountry").ToString()
                    oclsGtwayPayment.Region = row("Region").ToString()
                    oclsGtwayPayment.Expiry = row("Expiry").ToString()
                    oclsGtwayPayment.AuthStatus = row("AuthStatus").ToString()
                    oclsGtwayPayment.SettleStatus = row("SettleStatus").ToString()
                    oclsGtwayPayment.AuthCode = row("AuthCode").ToString()

                    oclsGtwayPayment.AuthMessage = row("AuthMessage").ToString()
                    oclsGtwayPayment.AVS_CVV_IVR = row("AVSCVVIVR").ToString()
                    oclsGtwayPayment.DS_Version = row("3DSVersion").ToString()
                    oclsGtwayPayment.ECI = row("ECI").ToString()
                    oclsGtwayPayment.DSCAVV = row("3DSCAVV").ToString()
                    oclsGtwayPayment.DSXID = row("3DSXID").ToString()
                    oclsGtwayPayment.DSTRANSID = row("DSTRANSID").ToString()

                    oclsGtwayPayment.ImportData_new()

                Next
                Session("Success") = "Record imported successfully"
                Response.Redirect("GtwayPayment_Import.aspx", False)
            End If

        Catch ex As Exception
            LogHelper.Error("GtwayPayment_Import:btnUpload_Click:" + ex.Message)
        End Try
    End Sub
End Class

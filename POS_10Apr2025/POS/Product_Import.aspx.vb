Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class Product_Import
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProduct As New Controller_clsProduct
    Dim oclsCategory As Controller_clsCategory = New Controller_clsCategory()
    Dim oclsBind As New clsBinding
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Product_List"
    Dim oclsLocation As Controller_clsLocation = New Controller_clsLocation()
    Dim dtSize As DataTable
    Dim dtCondiment As DataTable
    Dim dtBarcodeDetails As DataTable
    Dim dtPrinter As DataTable

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Product_List:Page_Load:" + ex.Message)
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
                    If row("Product Group").ToString IsNot "" Then
                        oclsProduct.cmp_id = Val(Session("cmp_id"))
                        oclsProduct.MainCategoryName = row("Product Group Category").ToString
                        oclsProduct.GroupName = row("Product Group").ToString
                        oclsProduct.name = row("Product Name").ToString
                        oclsProduct.UnitName = row("Unit").ToString
                        oclsProduct.department_name = row("Department").ToString
                        oclsProduct.Printer_name1 = row("Printer 1").ToString
                        oclsProduct.Printer_name2 = row("Printer 2").ToString
                        oclsProduct.Printer_name3 = row("Printer 3").ToString

                        If row("Price").ToString IsNot "" Then
                            oclsProduct.price = Val(row("Price").ToString())
                        Else
                            oclsProduct.price = Val(0)
                        End If

                        If row("Base Size").ToString IsNot "" Then
                            oclsProduct.Base = row("Base Size").ToString
                        Else
                            oclsProduct.Base = Val(1)
                        End If

                        If row("Size 1 Unit").ToString = "" And row("Size 1 Qty").ToString = "" And
                        row("Size 1 Name").ToString = "" And row("Size 1 Price").ToString = "" Then

                            oclsProduct.size1name = ""
                            oclsProduct.size1unit = ""
                            oclsProduct.size1 = Val(0)
                            oclsProduct.size1price = Val(-1)
                        Else

                            oclsProduct.size1name = row("Size 1 Name").ToString

                            If row("Size 1 Unit").ToString IsNot "" Then
                                oclsProduct.size1unit = row("Size 1 Unit").ToString
                            Else
                                oclsProduct.size1unit = "Qty"
                            End If

                            If row("Size 1 Qty").ToString IsNot "" Then
                                oclsProduct.size1 = row("Size 1 Qty").ToString
                            Else
                                oclsProduct.size1 = Val(1)
                            End If

                            If row("Size 1 Price").ToString IsNot "" Then
                                oclsProduct.size1price = Val(row("Size 1 Price").ToString)
                            Else
                                oclsProduct.size1price = Val(0)
                            End If

                        End If

                        If row("Size 2 Unit").ToString = "" And row("Size 2 Qty").ToString = "" And
                       row("Size 2 Name").ToString = "" And row("Size 2 Price").ToString = "" Then

                            oclsProduct.size2name = ""
                            oclsProduct.size2unit = ""
                            oclsProduct.size2 = Val(0)
                            oclsProduct.size2price = Val(-1)
                        Else

                            oclsProduct.size2name = row("Size 2 Name").ToString

                            If row("Size 2 Unit").ToString IsNot "" Then
                                oclsProduct.size2unit = row("Size 2 Unit").ToString
                            Else
                                oclsProduct.size2unit = "Qty"
                            End If

                            If row("Size 2 Qty").ToString IsNot "" Then
                                oclsProduct.size2 = row("Size 2 Qty").ToString
                            Else
                                oclsProduct.size2 = Val(1)
                            End If

                            If row("Size 2 Price").ToString IsNot "" Then
                                oclsProduct.size2price = Val(row("Size 2 Price").ToString)
                            Else
                                oclsProduct.size2price = Val(0)
                            End If

                        End If

                        If row("Size 3 Unit").ToString = "" And row("Size 3 Qty").ToString = "" And
                       row("Size 3 Name").ToString = "" And row("Size 3 Price").ToString = "" Then

                            oclsProduct.size3name = ""
                            oclsProduct.size3unit = ""
                            oclsProduct.size3 = Val(0)
                            oclsProduct.size3price = Val(-1)
                        Else
                            oclsProduct.size3name = row("Size 3 Name").ToString
                            If row("Size 3 Unit").ToString IsNot "" Then
                                oclsProduct.size3unit = row("Size 3 Unit").ToString
                            Else
                                oclsProduct.size3unit = "Qty"
                            End If

                            If row("Size 3 Qty").ToString IsNot "" Then
                                oclsProduct.size3 = row("Size 3 Qty").ToString
                            Else
                                oclsProduct.size3 = Val(1)
                            End If

                            If row("Size 3 Price").ToString IsNot "" Then
                                oclsProduct.size3price = Val(row("Size 3 Price").ToString)
                            Else
                                oclsProduct.size3price = Val(0)
                            End If

                        End If

                        If row("Size 4 Unit").ToString = "" And row("Size 4 Qty").ToString = "" And
                       row("Size 4 Name").ToString = "" And row("Size 4 Price").ToString = "" Then

                            oclsProduct.size4name = ""
                            oclsProduct.size4unit = ""
                            oclsProduct.size4 = Val(0)
                            oclsProduct.size4price = Val(-1)
                        Else
                            oclsProduct.size4name = row("Size 4 Name").ToString
                            If row("Size 4 Unit").ToString IsNot "" Then
                                oclsProduct.size4unit = row("Size 4 Unit").ToString
                            Else
                                oclsProduct.size4unit = "Qty"
                            End If

                            If row("Size 4 Qty").ToString IsNot "" Then
                                oclsProduct.size4 = row("Size 4 Qty").ToString
                            Else
                                oclsProduct.size4 = Val(1)
                            End If

                            If row("Size 4 Price").ToString IsNot "" Then
                                oclsProduct.size4price = Val(row("Size 4 Price").ToString)
                            Else
                                oclsProduct.size4price = Val(0)
                            End If

                        End If

                        oclsProduct.barcode1 = row("Barcode 1").ToString
                        oclsProduct.barcode2 = row("Barcode 2").ToString
                        oclsProduct.barcode3 = row("Barcode 3").ToString
                        oclsProduct.barcode4 = row("Barcode 4").ToString

                        oclsProduct.ImportData_new()
                        Session("Success") = "Record imported successfully"
                    End If
                Next
                Response.Redirect("Product_List.aspx", False)

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            Dim dt1 As DataTable = DirectCast(ViewState("Excel_Data"), DataTable)

            For Each row As DataRow In dt1.Rows
                If row("Product Group").ToString IsNot "" Then
                    oclsProduct.cmp_id = Val(Session("cmp_id"))
                    oclsProduct.MainCategoryName = row("Product Group Category").ToString
                    oclsProduct.GroupName = row("Product Group").ToString
                    oclsProduct.name = row("Product Name").ToString
                    oclsProduct.UnitName = row("Unit").ToString
                    oclsProduct.department_name = row("Department").ToString
                    oclsProduct.Printer_name1 = row("Printer 1").ToString
                    oclsProduct.Printer_name2 = row("Printer 2").ToString
                    oclsProduct.Printer_name3 = row("Printer 3").ToString

                    If row("Price").ToString IsNot "" Then
                        oclsProduct.price = Val(row("Price").ToString())
                    Else
                        oclsProduct.price = Val(0)
                    End If

                    If row("Base Size").ToString IsNot "" Then
                        oclsProduct.Base = row("Base Size").ToString
                    Else
                        oclsProduct.Base = Val(1)
                    End If

                    If row("Size 1 Unit").ToString = "" And row("Size 1 Qty").ToString = "" And
                        row("Size 1 Name").ToString = "" And row("Size 1 Price").ToString = "" Then

                        oclsProduct.size1name = ""
                        oclsProduct.size1unit = ""
                        oclsProduct.size1 = Val(0)
                        oclsProduct.size1price = Val(-1)
                    Else

                        oclsProduct.size1name = row("Size 1 Name").ToString

                        If row("Size 1 Unit").ToString IsNot "" Then
                            oclsProduct.size1unit = row("Size 1 Unit").ToString
                        Else
                            oclsProduct.size1unit = "Qty"
                        End If

                        If row("Size 1 Qty").ToString IsNot "" Then
                            oclsProduct.size1 = row("Size 1 Qty").ToString
                        Else
                            oclsProduct.size1 = Val(1)
                        End If

                        If row("Size 1 Price").ToString IsNot "" Then
                            oclsProduct.size1price = Val(row("Size 1 Price").ToString)
                        Else
                            oclsProduct.size1price = Val(0)
                        End If

                    End If

                    If row("Size 2 Unit").ToString = "" And row("Size 2 Qty").ToString = "" And
                       row("Size 2 Name").ToString = "" And row("Size 2 Price").ToString = "" Then

                        oclsProduct.size2name = ""
                        oclsProduct.size2unit = ""
                        oclsProduct.size2 = Val(0)
                        oclsProduct.size2price = Val(-1)
                    Else

                        oclsProduct.size2name = row("Size 2 Name").ToString

                        If row("Size 2 Unit").ToString IsNot "" Then
                            oclsProduct.size2unit = row("Size 2 Unit").ToString
                        Else
                            oclsProduct.size2unit = "Qty"
                        End If

                        If row("Size 2 Qty").ToString IsNot "" Then
                            oclsProduct.size2 = row("Size 2 Qty").ToString
                        Else
                            oclsProduct.size2 = Val(1)
                        End If

                        If row("Size 2 Price").ToString IsNot "" Then
                            oclsProduct.size2price = Val(row("Size 2 Price").ToString)
                        Else
                            oclsProduct.size2price = Val(0)
                        End If

                    End If

                    If row("Size 3 Unit").ToString = "" And row("Size 3 Qty").ToString = "" And
                       row("Size 3 Name").ToString = "" And row("Size 3 Price").ToString = "" Then

                        oclsProduct.size3name = ""
                        oclsProduct.size3unit = ""
                        oclsProduct.size3 = Val(0)
                        oclsProduct.size3price = Val(-1)
                    Else
                        oclsProduct.size3name = row("Size 3 Name").ToString
                        If row("Size 3 Unit").ToString IsNot "" Then
                            oclsProduct.size3unit = row("Size 3 Unit").ToString
                        Else
                            oclsProduct.size3unit = "Qty"
                        End If

                        If row("Size 3 Qty").ToString IsNot "" Then
                            oclsProduct.size3 = row("Size 3 Qty").ToString
                        Else
                            oclsProduct.size3 = Val(1)
                        End If

                        If row("Size 3 Price").ToString IsNot "" Then
                            oclsProduct.size3price = Val(row("Size 3 Price").ToString)
                        Else
                            oclsProduct.size3price = Val(0)
                        End If

                    End If

                    If row("Size 4 Unit").ToString = "" And row("Size 4 Qty").ToString = "" And
                       row("Size 4 Name").ToString = "" And row("Size 4 Price").ToString = "" Then

                        oclsProduct.size4name = ""
                        oclsProduct.size4unit = ""
                        oclsProduct.size4 = Val(0)
                        oclsProduct.size4price = Val(-1)
                    Else
                        oclsProduct.size4name = row("Size 4 Name").ToString
                        If row("Size 4 Unit").ToString IsNot "" Then
                            oclsProduct.size4unit = row("Size 4 Unit").ToString
                        Else
                            oclsProduct.size4unit = "Qty"
                        End If

                        If row("Size 4 Qty").ToString IsNot "" Then
                            oclsProduct.size4 = row("Size 4 Qty").ToString
                        Else
                            oclsProduct.size4 = Val(1)
                        End If

                        If row("Size 4 Price").ToString IsNot "" Then
                            oclsProduct.size4price = Val(row("Size 4 Price").ToString)
                        Else
                            oclsProduct.size4price = Val(0)
                        End If

                    End If

                    oclsProduct.barcode1 = row("Barcode 1").ToString
                    oclsProduct.barcode2 = row("Barcode 2").ToString
                    oclsProduct.barcode3 = row("Barcode 3").ToString
                    oclsProduct.barcode4 = row("Barcode 4").ToString

                    oclsProduct.ImportData_new()
                    Session("Success") = "Record imported successfully"
                End If
            Next
            Response.Redirect("Product_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
        End Try
    End Sub
End Class

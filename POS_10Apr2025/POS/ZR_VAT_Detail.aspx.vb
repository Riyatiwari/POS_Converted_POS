Imports System.Data
Imports Telerik.Web.UI

Partial Class ZR_VAT_Detail
    Inherits BaseClass

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                bindGrid()

            End If
        Catch ex As Exception
            LogHelper.Error("ZR_VAT_Detail:Page_Load:" + ex.Message)
        End Try

    End Sub

    Public Sub bindGrid()
        Try

            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            Dim FromDate As String
            Dim ToDate As String
            FromDate = DateTime.Now.ToString()
            ToDate = DateTime.Now.ToString()

            If Request.QueryString("S_Date") IsNot Nothing Then
                oColSqlparram.Add("@date1", Convert.ToDateTime(Request.QueryString("S_Date")).ToString("yyyy-MM-dd hh:mm tt"))
            Else
                oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
            End If

            If Request.QueryString("E_Date") IsNot Nothing Then
                oColSqlparram.Add("@date2", Convert.ToDateTime(Request.QueryString("E_Date")).ToString("yyyy-MM-dd hh:mm tt"))
            Else
                oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            End If

            If Request.QueryString("T_ID") IsNot Nothing Then
                oColSqlparram.Add("@Tax_id", Val(Request.QueryString("T_ID").ToString()))
            Else
                oColSqlparram.Add("@Tax_id", Val(0))
            End If

            If Request.QueryString("deptid") IsNot Nothing And Request.QueryString("deptid") IsNot "" Then
                oColSqlparram.Add("@dept_id", Val(Request.QueryString("deptid").ToString()))

            End If
            If Request.QueryString("L_ID") IsNot Nothing And Request.QueryString("L_ID") IsNot "" Then
                oColSqlparram.Add("@location_id", Val(Request.QueryString("L_ID").ToString()))
            Else
                oColSqlparram.Add("@location_id", Val(0))
            End If

            If Request.QueryString("M_ID") IsNot Nothing And Request.QueryString("M_ID") IsNot "" Then
                oColSqlparram.Add("@machine_id", Val(Request.QueryString("M_ID").ToString()))
            Else
                oColSqlparram.Add("@machine_id", Val(0))
            End If

            If Request.QueryString("V_ID") IsNot Nothing And Request.QueryString("V_ID") IsNot "" Then
                oColSqlparram.Add("@venue_id", Val(Request.QueryString("V_ID").ToString()))
            Else
                oColSqlparram.Add("@venue_id", Val(0))
            End If

            If Request.QueryString("LID") IsNot Nothing And Request.QueryString("LID") IsNot "" Then
                oColSqlparram.Add("@login_id", Val(Request.QueryString("LID").ToString()))
            Else
                oColSqlparram.Add("@login_id", Val(0))
            End If

            If Request.QueryString("S_Type") IsNot Nothing Then
                oColSqlparram.Add("@salestype", Request.QueryString("S_Type").ToString())
            Else
                oColSqlparram.Add("@salestype", "All")
            End If

            If Request.QueryString("Sh_name") IsNot Nothing Then
                oColSqlparram.Add("@shift_name", Request.QueryString("Sh_name").ToString())
            Else
                oColSqlparram.Add("@shift_name", "0")
            End If

            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")))


            Dim dt As DataTable = oClsDal.GetdataTableSp("Get_Z_Report_VAT_Detail", oColSqlparram)
            'If (Request.QueryString("T_ID").ToString() <> "0" Then
            '    oColSqlparram.Add("@Tax_id", Val(Request.QueryString("T_ID").ToString()))
            'Else
            '    dt = oClsDal.GetdataTableSp("Get_Z_Report_VAT_Detail", oColSqlparram)
            'End If


            If dt.Rows.Count > 0 Then
                rdVAT_Detail.DataSource = dt
                rdVAT_Detail.DataBind()
            Else
                rdVAT_Detail.DataSource = String.Empty
                rdVAT_Detail.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("ZR_VAT_Detail:bindGrid:" + ex.Message)
        End Try

    End Sub

End Class


Imports System.Data

Partial Class Auto_Sync_History
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsAutoSyncRecored As Controller_clsAutoSyncRecored = New Controller_clsAutoSyncRecored()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                txtForDate.SelectedDate = System.DateTime.Now
                txtToDate.SelectedDate = System.DateTime.Now

                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            LogHelper.Error("ZReport:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radVenue.SelectedIndex = 0 Then
                radLocation.Items.Clear()
                radMachine.Items.Clear()
            Else
                oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))
                radMachine.Items.Clear()
            End If

        Catch ex As Exception
            LogHelper.Error("Auto_Sync_History:radVenue_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub radLocation_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radLocation.SelectedIndex = 0 Then
                radMachine.Items.Clear()
            Else
                oclsBind.BindMachineByLocation(radMachine, Val(Session("cmp_id")), Val(radLocation.SelectedValue))
            End If
        Catch ex As Exception
            LogHelper.Error("Auto_Sync_History:radLocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Auto_Sync_History:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnk_View_Click(sender As Object, e As EventArgs)
        Try

            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")))
            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)
            oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@location_id", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0"))
            oColSqlparram.Add("@machine_id", IIf(radMachine.SelectedIndex > 0, radMachine.SelectedValue, "0"))
            oColSqlparram.Add("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
            oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today"))

            Dim dt As DataTable

            dt = oClsDal.GetdataTableSp("get_M_AutoSyncRecord_Detail", oColSqlparram)

            If dt.Rows.Count > 0 Then
                rdHistory.DataSource = dt
                rdHistory.DataBind()
            Else
                rdHistory.DataSource = String.Empty
                rdHistory.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Auto_Sync_History:lnk_View_Click:" + ex.Message)
        End Try
    End Sub
End Class

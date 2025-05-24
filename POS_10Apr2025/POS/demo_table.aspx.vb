Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Imports System.Web.Services
Partial Class demo_table
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole
    Dim dt As DataTable

    <WebMethod>
    Public Sub BindData()
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet

            Dim dtwise As Integer = 0


            Dim n As String



            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@from_date", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@to_date", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@location_id", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0"))
            oColSqlparram.Add("@machine_id", IIf(radMachine.SelectedIndex > 0, radMachine.SelectedValue, "0"))
            oColSqlparram.Add("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
            'oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"))
            If hfSizelocation.Value = "0" Then
                dt = oClsDal.GetdataTableSp("Table_Transaction_Report", oColSqlparram)
            Else
                dt = oClsDal.GetdataTableSp("Table_Transaction_Report", oColSqlparram)
            End If

            gvCustomers.DataSource = dt
            gvCustomers.DataBind()





        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        End Try
    End Sub
    Public Sub binddll()
        Try
            If radVenue.SelectedIndex = -1 Then
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("TableReport:binddll:" + ex.Message)
        End Try
    End Sub


    Public Sub binddllloc()
        Try
            If radLocation.SelectedIndex = -1 Then
                oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("TableReport:binddll:" + ex.Message)
        End Try
    End Sub


    Public Sub binddllMachine()
        Try
            If radMachine.SelectedIndex = -1 Then
                oclsBind.BindMachine(radMachine, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("TableReport:binddll:" + ex.Message)
        End Try
    End Sub
    Private Sub PSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If
            binddll()
            binddllloc()
            binddllMachine()
            divcustom.Visible = True
            txtFrom.SelectedDate = System.DateTime.Now
            txtTo.SelectedDate = System.DateTime.Now
            '  BindData()




        End If
    End Sub
    Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radVenue.SelectedIndexChanged
        Try
            If radVenue.SelectedIndex = 0 Then
                radLocation.Items.Clear()
                radMachine.Items.Clear()
            Else
                oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))
                radMachine.Items.Clear()
            End If

        Catch ex As Exception
            LogHelper.Error("ZReport:radVenue_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub radLocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radLocation.SelectedIndexChanged
        Try
            If radLocation.SelectedIndex = 0 Then
                radMachine.Items.Clear()
            Else
                oclsBind.BindMachineByLocation(radMachine, Val(Session("cmp_id")), Val(radLocation.SelectedValue))
            End If
        Catch ex As Exception
            LogHelper.Error("ZReport:radLocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        BindData()
    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True

            Else
                divcustom.Visible = False

            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub


    'Private Sub rptProductSUmmary_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptProductSUmmary.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Header Then
    '        Dim rpcashSUmmary As Repeater = TryCast(e.Item.FindControl("Header1"), Repeater)
    '        rpcashSUmmary.DataSource = dt.Columns
    '        rpcashSUmmary.DataBind()
    '    End If

    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim rpcashSUmmary As Repeater = TryCast(e.Item.FindControl("Item1"), Repeater)
    '        Dim row = TryCast(e.Item.DataItem, System.Data.DataRowView)
    '        rpcashSUmmary.DataSource = row.Row.ItemArray
    '        rpcashSUmmary.DataBind()


    '    End If
    'End Sub


    <System.Web.Services.WebMethod()>
    Public Shared Function GetRcptDetails(ByVal ref_id As String, ByVal date1 As String, ByVal Location_Id As Int64) As String

        Dim oClsDal As ClsDataccess = New ClsDataccess()
        Dim ds As New DataSet
        Dim FromDate As String

        If date1.ToString = "" Then
            date1 = System.DateTime.Now
        End If

        FromDate = IIf(date1.ToString = "", date1 = DateTime.Now, date1)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        Dim d1 As String = Convert.ToDateTime(FromDate).ToString("yyyy-MMM-dd HH:mm")

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        oColSqlparram.Add("@ref_id", ref_id, SqlDbType.Int)
        oColSqlparram.Add("@from_date", d1, SqlDbType.DateTime)
        oColSqlparram.Add("@Location_id", Location_Id, SqlDbType.Int)

        Dim dt As DataTable = oClsDal.GetdataTableSp("Table_Report_T_Sales", oColSqlparram)


        If dt.Rows.Count > 0 Then

            'Return "Hello " & StkRcptId & Environment.NewLine & "The Current Time is: " & _
            '      DateTime.Now.ToString()

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In dt.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In dt.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next
            Return serializer.Serialize(rows)



        End If

        Return ""

    End Function

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ref_id As String = gvCustomers.DataKeys(e.Row.RowIndex).Value.ToString()

            Dim date1 As String = e.Row.Cells(7).Text
            Dim Location_Id As String = e.Row.Cells(8).Text


            Dim gvOrders As GridView = TryCast(e.Row.FindControl("gvOrders"), GridView)




            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet
            Dim FromDate As String

            If date1.ToString = "" Then
                date1 = System.DateTime.Now
            End If

            FromDate = IIf(date1.ToString = "", date1 = DateTime.Now, date1)
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            Dim d1 As String = Convert.ToDateTime(FromDate).ToString("yyyy-MMM-dd HH:mm")

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@ref_id", ref_id, SqlDbType.Int)
            oColSqlparram.Add("@from_date", d1, SqlDbType.DateTime)
            oColSqlparram.Add("@Location_id", Location_Id, SqlDbType.Int)

            Dim dt As DataTable = oClsDal.GetdataTableSp("Table_Report_T_Sales", oColSqlparram)


            If dt.Rows.Count > 0 Then

                gvOrders.DataSource = dt
                gvOrders.DataBind()

            End If




        End If
        e.Row.Cells(8).Visible = False


    End Sub

    Protected Sub OnRowDataBound_Child(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

        End If

    End Sub


End Class

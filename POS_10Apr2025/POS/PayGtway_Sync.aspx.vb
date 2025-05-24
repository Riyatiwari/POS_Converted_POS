Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class PayGtway_Sync
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

                If Not Session("StoreID") = Nothing Then
                    Dim dt As DataTable = oclsGtwayPayment.Select()

                    If dt.Rows.Count > 0 Then
                        If Val(dt.Rows(0)("Is_Exists").ToString()) = 1 Then
                            lbl_msg.InnerText = "Last record sync Date : " + Convert.ToDateTime(dt.Rows(0)("Date")).ToString("dd/MM/yyyy")
                        Else
                            lbl_msg.InnerText = "There is no record found."

                        End If

                    Else
                        lbl_msg.InnerText = "There is no record found."
                    End If
                End If

            End If

        Catch ex As Exception
            LogHelper.Error("PayGtway_Sync:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            If Not Session("StoreID") = Nothing Then
                Dim maxDate As DateTime
                Dim dt As DataTable = oclsGtwayPayment.Select()
                Dim Is_Exists As Integer = 0

                If dt.Rows.Count > 0 Then
                    maxDate = Convert.ToDateTime(dt.Rows(0)("Date")).ToString("dd/MM/yyyy")
                    Is_Exists = Val(dt.Rows(0)("Is_Exists").ToString())
                Else
                    maxDate = System.DateTime.Now
                End If

                '------------------------------------------

                Dim constring As String = ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString

                Dim str_Controller As SqlConnection = New SqlConnection(constring.ToString())

                str_Controller.Open()
                Dim store_name As String = "PaymentGateway"
                Dim s_cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", str_Controller)
                s_cmd.CommandType = CommandType.StoredProcedure
                s_cmd.Parameters.AddWithValue("@db_name", store_name)
                Dim s_adp As SqlDataAdapter = New SqlDataAdapter(s_cmd)
                Dim s_dt As DataTable = New DataTable()
                s_adp.Fill(s_dt)
                s_cmd.ExecuteNonQuery()
                str_Controller.Close()

                If (s_dt.Rows.Count > 0) Then

                    Dim strcon As SqlConnection = New SqlConnection("Data Source=" + s_dt.Rows(0)("db_server") + ";Initial Catalog=" + s_dt.Rows(0)("db_name") + ";User ID=" + s_dt.Rows(0)("db_username") + ";Password=" + Decode_data(s_dt.Rows(0)("db_password")) + ";")
                    strcon.Open()
                    Dim cmd As SqlCommand = New SqlCommand("Get_Recored_from_PayGtway", strcon)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@StoreID", Session("StoreID").ToString())
                    cmd.Parameters.AddWithValue("@maxDate", maxDate)
                    cmd.Parameters.AddWithValue("@Is_Exists", Is_Exists)
                    Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
                    Dim ds As DataSet = New DataSet()
                    adp.Fill(ds)
                    cmd.ExecuteNonQuery()
                    strcon.Close()

                    Dim rdt As DataTable = ds.Tables(0)

                    Dim mdt As DataTable = ds.Tables(1)

                    If rdt.Rows.Count > 0 Then

                        oclsGtwayPayment.dt = rdt
                        oclsGtwayPayment.InsertData()

                        Session("Success") = "Total " + rdt.Rows.Count.ToString() + " record sync."
                    Else

                        Session("Success") = "No record found for sync."
                    End If

                    If mdt.Rows.Count > 0 Then

                        For Each row As DataRow In mdt.Rows

                            oclsGtwayPayment.MerchantID = row("gtway_MerchantID").ToString()
                            oclsGtwayPayment.LocationID = row("gtway_LocationID").ToString()

                            oclsGtwayPayment.UpdateMerchatID()
                        Next

                    End If

                    Response.Redirect("PayGtway_Sync.aspx", False)
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("PayGtway_Sync:btnSubmit_Click:" + ex.Message)
        End Try
    End Sub

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class

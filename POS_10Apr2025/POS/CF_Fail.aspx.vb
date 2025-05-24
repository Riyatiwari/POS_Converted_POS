Imports System.Data
Imports System.Data.SqlClient
Partial Class CF_Fail
    Inherits System.Web.UI.Page

    Dim oclsSales As New Controller_clsSales()
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProductGroup As New Controller_clsCategory
    Dim str As String = ""
    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Try
            Dim Response As String = "Transaction Fail"
            lblDatetime.Text = System.DateTime.Now.ToString()
            ' lblResponse.InnerHtml = Response.ToString()
            If Not (Request.QueryString("paymentjobref") = Nothing) Then


                Dim FOrderRef As String = ""
                Dim FPaymentRef As String = ""
                LogHelper.Error("CF_SUCESS:paymentjobref :" + System.DateTime.Now.ToString() + ": " + Request.QueryString("paymentjobref").ToString())
                LogHelper.Error("CF_SUCESS: paymentref :" + System.DateTime.Now.ToString() + ": " + Request.QueryString("paymentref").ToString())
                Try


                    Dim paymentjobref As String = Request.QueryString("paymentjobref").ToString()
                    Dim paymentref As String = Request.QueryString("paymentref").ToString()

                    Dim arrOrdRef As String() = paymentjobref.Split(",")
                    Dim arrPayRef As String() = paymentref.Split(",")

                    FOrderRef = arrOrdRef(0).ToString()
                    FPaymentRef = arrPayRef(arrPayRef.Length - 1).ToString
                    lblTranRef.Text = FPaymentRef.ToString()


                    'Dim dt As DataTable = GetdataTableSp("P_M_SALES_update", oColSqlPar, "P_M_SALES_update", str)
                    'lbtnLink.HRef = dt.Rows(0)("Website").ToString()
                Catch ex As Exception
                    LogHelper.Error("CF_SUCESS: Payref Count :" + System.DateTime.Now.ToString())
                End Try


                CheckPay(FOrderRef)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "", Optional ByVal constr As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(constr)
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.Parameters.Clear()
                com.CommandTimeout = 0

                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                    Dim parameter As New SqlClient.SqlParameter
                    parameter.ParameterName = oClsSqlParameter.ParaName
                    parameter.SqlDbType = oClsSqlParameter.ParaType
                    parameter.Value = oClsSqlParameter.ParaValue
                    parameter.Direction = oClsSqlParameter.ParaDirection
                    com.Parameters.Add(parameter)
                Next

                SpAdepter = New SqlDataAdapter(com)
                sdr = New Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("Till:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing
        End Try
        Return sdr
    End Function
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Sub CheckStore()
        Try

            Dim n As String = Session("store_name")

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                str = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Decode_data(dt.Rows(0)("db_password")) + ";"
            Else


            End If
        Catch ex As Exception
            LogHelper.Error("Till:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
    Public Sub CheckPay(ByVal order_ref As String)
        Try

            strcon.Open()

            Dim cmd As SqlCommand = New SqlCommand("Get_M_Pay_check", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@order_ref", order_ref)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("store_name") = dt.Rows(0)("store_name")
                Session("sales_id") = dt.Rows(0)("sales_id")

                CheckStore()
            Else
            End If
        Catch ex As Exception
            LogHelper.Error("CF_success:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub


End Class

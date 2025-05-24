
Imports System.Data
Imports System.Data.SqlClient

Partial Class Register_Register
    Inherits BaseClass
    Dim oclsCustomer As New Controller_clsCustomer()
    Dim storeName As String = "JWManagement"
    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

    End Sub
    Public Sub AddAccount(sRandom As String)
        Try
            Dim dtS As DataTable = getStore(storeName)
            Dim conns As String = getconstr(dtS)
            Dim bytes As Byte()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@customer_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@first_name", txtName.Text)
            oColSqlparram.Add("@last_name", "")
            oColSqlparram.Add("@email", txtEmail.Text)
            oColSqlparram.Add("@contact_no", "")
            oColSqlparram.Add("@address", "")
            oColSqlparram.Add("@country_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@state_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@city_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@postal_code", txtPostcode.Text)
            oColSqlparram.Add("@other_id", "")
            oColSqlparram.Add("@profile_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@AccountID", 0, SqlDbType.Int)
            oColSqlparram.Add("@Is_credit", "0")
            oColSqlparram.Add("@CardNumber", sRandom)
            oColSqlparram.Add("@ExpDate", DateTime.MaxValue.ToString(), SqlDbType.DateTime)
            oColSqlparram.Add("@CustomerProfile", bytes, SqlDbType.Image)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Price_level_id", 0, SqlDbType.Int)
            Dim dtlogin As DataTable = GetdataTableSp("P_Account", oColSqlparram, "P_Account", conns)



        Catch ex As Exception
            LogHelper.Error("Register:AddAccount:" & ex.ToString())
        End Try
    End Sub
    Protected Sub btnRegister_Click(sender As Object, e As EventArgs)
        Try
            ' System.Threading.Thread.Sleep(300000)
            Dim email As String
            Dim body As StringBuilder = New StringBuilder()
            Dim Subject As String
            Dim rnNumber As Random = New Random()
            email = txtEmail.Text.ToString()
            Dim sRandom As String = rnNumber.Next(99999, 9999999).ToString()

            Dim dtS As DataTable = getStore(storeName)
            Dim conns As String = getconstr(dtS)


            oclsCustomer.email = txtEmail.Text.ToString.Trim()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@email", email)
            Dim dtCustomer As DataTable = GetdataTableSp("Get_M_Customer_email", oColSqlparram, "Get_M_Customer_email", conns)


            If (dtCustomer.Rows.Count > 0) Then
                'Dim alrtMsg As String = "Email already registered with us."
                'Dim script As String = "window.onload = function(){ alert('"
                'script &= alrtMsg
                'script &= "')};"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Email already registered with us');", True)

            Else


                AddAccount(sRandom)

                Subject = "Complimentary house drink "
                body.Append("Congratulations the below gets you a complimentary house drink to be redeemed at The Rylston between 1st and 5th of may.  We hope you enjoy!")
                body.Append("<br/><br/>")
                body.Append("The voucher number/code is : " + sRandom)
                body.Append("<br/><br/>")
                'LogHelper.Error("Z_Report:email_id:" & email)
                MailTo_receiptA(Val(0), Val(0), email, Subject, body.ToString(), "", "", "", "The Rylston")

                ' Email To Rylston
                body = New StringBuilder()
                Subject = "New Registration with code " + sRandom
                body.Append("Name :" + txtName.Text.ToString())
                body.Append("<br/>")
                body.Append("Email :" + txtEmail.Text.ToString())
                body.Append("<br/>")
                body.Append("PostCode :" + txtPostcode.Text.ToString())
                body.Append("<br/>")
                body.Append("<br/><br/>")
                body.Append("Given voucher number/code is : " + sRandom)
                body.Append("<br/><br/>")
                'LogHelper.Error("Z_Report:email_id:" & email)brendon@tenderpos.com
                MailTo_receiptA(Val(0), Val(0), "brendon@tenderpos.com,info@yellowpandapubco.com", Subject, body.ToString(), "", "", "", "The Rylston")

                'Dim alrtMsg As String = "Registered Sucessfully."
                'Dim script As String = "window.onload = function(){ alert('"
                'script &= alrtMsg
                'script &= "')};"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Registered Sucessfully.');", True)


                txtName.Text = ""
                txtEmail.Text = ""
                txtPostcode.Text = ""

                Response.Redirect("https://www.therylston.com/")
            End If


        Catch ex As Exception
            LogHelper.Error("Register:email_id:" & ex.ToString())
        End Try
    End Sub

    Public Function getStore(ByVal store_name As String) As DataTable
        Try
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            con.Open()
            Dim cmd As SqlCommand = New SqlCommand("WS_Get_Store", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@store_name", store_name)

            Dim adpter As New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adpter.Fill(dt)
            con.Close()
            Return dt

        Catch ex As Exception
            LogHelper.Error("POS_WS:getStore():" + ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function getconstr(ByVal dt As DataTable) As String
        Try
            Dim constr As String = "Data Source=" & dt.Rows(0)("db_server") & ";" & "Initial Catalog=" & dt.Rows(0)("db_name") & ";" & "User ID=" & dt.Rows(0)("db_username") & ";" & "Password=" & Decode_data(dt.Rows(0)("db_password")) & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
            Return constr

        Catch ex As Exception
            LogHelper.Error("POS_WS:getconstr():" + ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function
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
            LogHelper.Error("POS_WebServices:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing
        End Try
        Return sdr
    End Function
End Class

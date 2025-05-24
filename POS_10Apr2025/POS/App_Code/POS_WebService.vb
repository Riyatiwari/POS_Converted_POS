'Imports System.Web
'Imports System.Web.Services
'Imports System.Web.Services.Protocols
'Imports System.Data
'Imports System.Data.SqlClient
'Imports System.Configuration
'Imports System.Xml
'Imports Newtonsoft.Json
'Imports System
'Imports System.Text
'Imports System.Collections.Generic
'Imports Microsoft.VisualBasic

'' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
'' <System.Web.Script.Services.ScriptService()> _
'<WebService(Namespace:="http://tempuri.org/")>
'<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
'Public Class POS_WebService
'    Inherits BaseClass

'    Public _ConnectionString As String = ""

'    Public Function data_table(ByVal dt As DataTable) As String

'        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
'        Dim rows As New List(Of Dictionary(Of String, Object))
'        Dim row As Dictionary(Of String, Object)
'        For Each dr As DataRow In dt.Rows
'            row = New Dictionary(Of String, Object)
'            For Each col As DataColumn In dt.Columns
'                row.Add(col.ColumnName, IIf(dr(col).ToString = "", "", dr(col)))
'            Next
'            rows.Add(row)
'        Next
'        serializer.MaxJsonLength = Int32.MaxValue
'        Dim str As String = serializer.Serialize(rows)
'        Return str

'    End Function

'    Public Function data_table_encode(ByVal dt As DataTable) As String

'        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
'        Dim rows As New List(Of Dictionary(Of String, Object))
'        Dim row As Dictionary(Of String, Object)
'        For Each dr As DataRow In dt.Rows
'            row = New Dictionary(Of String, Object)
'            For Each col As DataColumn In dt.Columns
'                If (col.ColumnName = "username") Then
'                    row.Add(col.ColumnName, IIf(dr(col).ToString = "", "", Encode_data(dr(col))))
'                Else
'                    row.Add(col.ColumnName, IIf(dr(col).ToString = "", "", dr(col)))
'                End If
'            Next
'            rows.Add(row)
'        Next
'        Dim str As String = serializer.Serialize(rows)
'        Return str

'    End Function
'    Public Function Encode_data(ByVal str As String) As String
'        Try
'            Return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))
'        Catch ex As Exception
'            Return ""
'        End Try
'    End Function

'    Public Function Decode_data(ByVal str As String) As String
'        Try
'            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
'        Catch ex As Exception
'            Return ""
'        End Try
'    End Function
'    'Public Function getData(ByVal Cmp_Code As String) As DataTable
'    '    Dim CMD As New SqlCommand("W_Get_Login")
'    '    CMD.Parameters.AddWithValue("@Cmp_Code", Cmp_Code)
'    '    Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("HRMSController").ConnectionString)
'    '    CMD.Connection = connection
'    '    CMD.CommandType = CommandType.StoredProcedure

'    '    Dim adapter As New SqlDataAdapter(CMD)
'    '    adapter.SelectCommand.CommandTimeout = 300

'    '    'Fill the dataset
'    '    Dim Dt As DataTable
'    '    adapter.Fill(Dt)
'    '    connection.Close()

'    '    Return Dt

'    'End Function



'    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "", Optional ByVal constr As String = "") As Data.DataTable
'        Dim sdr As DataTable = Nothing
'        Dim com As New SqlCommand
'        Dim SpAdepter As New SqlDataAdapter
'        Try
'            Using connection As New SqlConnection(constr)
'                connection.Open()
'                com.CommandText = SPName
'                com.CommandType = CommandType.StoredProcedure
'                com.Connection = connection
'                com.Parameters.Clear()
'                com.CommandTimeout = 100

'                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
'                    Dim parameter As New SqlClient.SqlParameter
'                    parameter.ParameterName = oClsSqlParameter.ParaName
'                    parameter.SqlDbType = oClsSqlParameter.ParaType
'                    parameter.Value = oClsSqlParameter.ParaValue
'                    parameter.Direction = oClsSqlParameter.ParaDirection
'                    com.Parameters.Add(parameter)
'                Next

'                SpAdepter = New SqlDataAdapter(com)
'                sdr = New Data.DataTable
'                SpAdepter.Fill(sdr)
'                If strTableName <> "" Then sdr.TableName = strTableName
'            End Using
'        Catch ex As Exception : Throw ex
'            LogHelper.Error("POS_WebServices:GetdataTableSp:" & ex.Message)
'        Finally
'            com.Parameters.Clear()
'            com = Nothing
'            SpAdepter = Nothing
'        End Try
'        Return sdr
'    End Function

'    Public Sub ExecStoredProcedure(ByVal strProcedureName As String, ByVal oColSqlparram As ColSqlparram, ByVal constr As String)
'        Dim intParaCount As Integer
'        Dim com As New SqlClient.SqlCommand
'        Using connection As New SqlConnection(constr)
'            Dim sql_Tran As SqlClient.SqlTransaction
'            Try

'                connection.Open()
'                sql_Tran = connection.BeginTransaction
'                com.CommandText = strProcedureName
'                com.CommandType = CommandType.StoredProcedure
'                com.Connection = connection
'                'com.CommandTimeout = 0
'                com.CommandTimeout = 100
'                com.Transaction = sql_Tran
'                com.Parameters.Clear()
'                With oColSqlparram
'                    For intParaCount = 0 To .Count - 1
'                        With .Item(intParaCount)
'                            Dim parameter As New SqlClient.SqlParameter
'                            parameter.ParameterName = .ParaName
'                            parameter.SqlDbType = .ParaType
'                            If parameter.SqlDbType = SqlDbType.VarChar And .ParaDirection = ParameterDirection.Input Then
'                                parameter.Size = IIf(Len(.ParaValue) = 0, 1, Len(.ParaValue))
'                            ElseIf parameter.SqlDbType = SqlDbType.VarChar And .ParaDirection <> ParameterDirection.Input Then
'                                parameter.Size = IIf(Len(.ParaValue) = 0, 1000, Len(.ParaValue))
'                            End If
'                            parameter.Value = .ParaValue
'                            parameter.Direction = .ParaDirection
'                            com.Parameters.Add(parameter)
'                        End With
'                    Next
'                End With
'                'com.ResetCommandTimeout()
'                com.ExecuteNonQuery()
'                With oColSqlparram
'                    For intParaCount = 0 To .Count - 1
'                        With .Item(intParaCount)
'                            If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
'                                .ParaValue = CheckNull(com.Parameters(.ParaName).Value)
'                            End If
'                        End With
'                    Next
'                End With
'                sql_Tran.Commit()
'            Catch ex As Exception
'                sql_Tran.Rollback()
'                If UCase(Left(ex.Message, 27)) = "DELETE STATEMENT CONFLICTED" Or InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0 Then
'                    With oColSqlparram
'                        For intParaCount = 0 To .Count - 1
'                            With .Item(intParaCount)
'                                If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
'                                    .ParaValue = 0
'                                End If
'                            End With
'                        Next
'                    End With
'                Else
'                    Throw ex
'                End If
'            Finally
'                com = Nothing
'            End Try
'        End Using
'    End Sub

'    Public Function CheckNull(ByVal vData As Object) As String
'        Try
'            CheckNull = IIf(IsDBNull(vData), "", vData)
'        Catch ex As Exception
'            LogHelper.Error("POS_WebServices:CheckNull:" & ex.Message)
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Login(ByVal authUsername As String, ByVal authPassword As String, ByVal user As String, ByVal pass As String, ByVal IP_Address As String, ByVal mac_id As String, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    oColSqlparram.Add("@User_Name", user)
'                    oColSqlparram.Add("@Password", Encrypt(pass))
'                    oColSqlparram.Add("@IP_Address", IP_Address)
'                    oColSqlparram.Add("@Access_Status", 1)
'                    oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                    oColSqlparram.Add("@mac_id", mac_id)

'                    ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Username", Decode_data(user))
'                    oColSqlPar.Add("@Password", Encrypt(Decode_data(pass)))
'                    oColSqlPar.Add("@Login_URL", ConfigurationManager.AppSettings.Get("Login_URL"))
'                    Dim datatbl As DataTable = GetdataTableSp("WS_do_Login", oColSqlPar, "M_Login", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", user)
'                oColSqlparram.Add("@Password", pass)
'                oColSqlparram.Add("@IP_Address", IP_Address)
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If

'        Catch ex As Exception
'            LogHelper.Error("POS_WebServices:Login:" + ex.Message)
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Customer_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_M_Customer", oColSqlPar, "WS_get_M_Customer", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function KioskSettiong_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer,
'                                  ByVal store_name As String, ByVal TillID As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@TillID", TillID, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_KioskSettings", oColSqlPar, "WS_get_KioskSettings", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function AutoSync_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer,
'                                  ByVal store_name As String, ByVal TillID As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@TillID", TillID, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_M_AutoSyncRecord", oColSqlPar, "WS_get_M_AutoSyncRecord", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function AutoSync_Update(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer,
'                                  ByVal store_name As String, ByVal AutoSync_Id As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@AutoSync_Id", AutoSync_Id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Update_M_AutoSyncRecord", oColSqlPar, "WS_Update_M_AutoSyncRecord", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "[{""Message"":""Status Updated.""}]"
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Graphic_Table(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Int32) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_Graphic_table", oColSqlPar, "WS_get_Graphic_table", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Schedule_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Int32) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_Schedule_List", oColSqlPar, "WS_get_Schedule_List", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Customer_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal customer_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@customer_id", customer_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Customer_Details", oColSqlPar, "Get_M_Customer_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If

'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If

'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Customer_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal customer_id As Integer,
'                             ByVal first_name As String, ByVal last_name As String, ByVal email As String, ByVal contact_no As String, ByVal address As String, ByVal country_id As Integer,
'                             ByVal state_id As Integer, ByVal city_id As Integer, ByVal postal_code As String, ByVal is_active As Integer, ByVal ip_address As String, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal venue_id As Integer, ByVal other_id As String, ByVal store_name As String,
'                             ByVal machine_id As Integer) As String

'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@customer_id", customer_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@first_name", first_name)
'                    oColSqlPar.Add("@last_name", last_name)
'                    oColSqlPar.Add("@email", email)
'                    oColSqlPar.Add("@contact_no", contact_no)
'                    oColSqlPar.Add("@address", address)
'                    oColSqlPar.Add("@country_id", country_id, SqlDbType.Int)
'                    oColSqlPar.Add("@state_id", state_id, SqlDbType.Int)
'                    oColSqlPar.Add("@city_id", city_id, SqlDbType.Int)
'                    oColSqlPar.Add("@postal_code", postal_code)
'                    oColSqlPar.Add("@is_active", is_active)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@other_id", other_id)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Customer", oColSqlPar, "P_M_Customer", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Customer_Master_withCardNumber(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal customer_id As Integer,
'                             ByVal first_name As String, ByVal last_name As String, ByVal email As String, ByVal contact_no As String, ByVal address As String, ByVal country_id As Integer,
'                             ByVal state_id As Integer, ByVal city_id As Integer, ByVal postal_code As String, ByVal is_active As Integer, ByVal ip_address As String, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal venue_id As Integer, ByVal other_id As String, ByVal store_name As String,
'                             ByVal machine_id As Integer, ByVal Is_credit As Integer, ByVal CardNumber As String,
'                             ByVal DateTimeExpiry As DateTime) As String

'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@customer_id", customer_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@first_name", first_name)
'                    oColSqlPar.Add("@last_name", last_name)
'                    oColSqlPar.Add("@email", email)
'                    oColSqlPar.Add("@contact_no", contact_no)
'                    oColSqlPar.Add("@address", address)
'                    oColSqlPar.Add("@country_id", country_id, SqlDbType.Int)
'                    oColSqlPar.Add("@state_id", state_id, SqlDbType.Int)
'                    oColSqlPar.Add("@city_id", city_id, SqlDbType.Int)
'                    oColSqlPar.Add("@postal_code", postal_code)
'                    oColSqlPar.Add("@is_active", is_active)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@other_id", other_id)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_credit", Is_credit, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@CardNumber", CardNumber)
'                    oColSqlPar.Add("@DateTimeExpiry", DateTimeExpiry, SqlDbType.DateTime)
'                    ' oColSqlPar.Add("@CustomerProfile", CustomerProfile, SqlDbType.Image)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Customer", oColSqlPar, "P_M_Customer", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function User_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Login_URL", ConfigurationManager.AppSettings.Get("Login_URL"))
'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_M_Staff", oColSqlPar, "get_M_Staff", getconstr(dt))
'                    For i As Integer = 0 To datatbl.Rows.Count - 1
'                        datatbl.Rows(i)(24) = Encode_data(Decrypt(datatbl.Rows(i)(24).ToString))
'                    Next
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table_encode(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function User_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal staff_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@staff_id", staff_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Login_URL", ConfigurationManager.AppSettings.Get("Login_URL"))
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Staff_Details", oColSqlPar, "Get_M_Staff_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function User_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal staff_id As Integer,
'                             ByVal staff_code As String, ByVal name As String, ByVal joining_date As DateTime, ByVal email As String, ByVal contact_no As String,
'                             ByVal address As String, ByVal country_id As Integer, ByVal state_id As Integer, ByVal city_id As Integer, ByVal postal_code As String,
'                             ByVal is_active As Integer, ByVal role_id As Integer, ByVal ip_address As String, ByVal till_code As Integer, ByVal till_active As Integer, ByVal photo As String, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal other_id As String, ByVal store_name As String, ByVal machine_id As Integer, ByVal Authentication_Code As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@staff_id", staff_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@staff_code", staff_code)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@photo", photo)
'                    oColSqlPar.Add("@till_active", till_active)
'                    oColSqlPar.Add("@joining_date", joining_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@branch_id", 0, SqlDbType.Int)
'                    oColSqlPar.Add("@department_id", 0, SqlDbType.Int)
'                    oColSqlPar.Add("@designation_id", 0, SqlDbType.Int)
'                    oColSqlPar.Add("@address", address)
'                    oColSqlPar.Add("@country_id", country_id, SqlDbType.Int)
'                    oColSqlPar.Add("@state_id", state_id, SqlDbType.Int)
'                    oColSqlPar.Add("@city_id", city_id, SqlDbType.Int)
'                    oColSqlPar.Add("@national_id", postal_code)
'                    oColSqlPar.Add("@contact_no", contact_no)
'                    oColSqlPar.Add("@email", email)
'                    oColSqlPar.Add("@last_working_date", "1900-01-01", SqlDbType.DateTime)
'                    oColSqlPar.Add("@role_id", role_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@till_code", till_code, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@other_id", other_id)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Authentication_Code", Authentication_Code)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Staff_New", oColSqlPar, "WS_P_M_Staff_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_Group_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Till_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Till_id", Till_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Category", oColSqlPar, "Get_M_Category", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Group_category_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_all_Category", oColSqlPar, "WS_Get_all_Category", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Product_Group_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal category_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@category_id", category_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Category_Details", oColSqlPar, "WS_Get_M_Category_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_Group_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal Category_id As Integer, ByVal key_map_id As Integer,
'                             ByVal name As String, ByVal Description As String, ByVal is_active As Integer, ByVal ip_address As String, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal sorting_no As Integer, ByVal machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Category_id", Category_id, SqlDbType.Int)
'                    oColSqlPar.Add("@key_map_id", key_map_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@description", Description)
'                    oColSqlPar.Add("@Is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Ip_address", ip_address)
'                    oColSqlPar.Add("@Login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@sorting_no", sorting_no, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Category_New", oColSqlPar, "WS_P_M_Category_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Printer_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Printer", oColSqlPar, "WS_Get_M_Printer", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Printer_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal printer_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@printer_id", printer_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Printer_Details", oColSqlPar, "WS_Get_M_Printer_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Printer_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal printer_id As Integer,
'                             ByVal name As String, ByVal palias As String, ByVal is_active As Integer, ByVal ip_address As String, ByVal Tran_Type As String, ByVal mac_id As String,
'                             ByVal venue_id As Integer, ByVal store_name As String, ByVal machine_id As Integer, ByVal is_product_small_large As Integer, ByVal is_condiment_small_large As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@printer_id", printer_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@alias", palias)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_product_small_large", is_product_small_large, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_condiment_small_large", is_condiment_small_large, SqlDbType.TinyInt)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Printer_New", oColSqlPar, "WS_P_M_Printer_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Department_List(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Department", oColSqlPar, "WS_P_Get_Department", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Course_List(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Course", oColSqlPar, "WS_P_Get_Course", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Product", oColSqlPar, "WS_P_Get_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            ' Return "[{""Message"":" + ex.Message + "}]"
'            Return "[{""Message"":""No Data Found.""}]"
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Product_Image(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_All_Product_Image", oColSqlPar, "WS_Get_All_Product_Image", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Product_Group_Image(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Product_Group_Image", oColSqlPar, "WS_Get_Product_Group_Image", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Group_Category_Image(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Product_Category_Image", oColSqlPar, "WS_Get_Product_Category_Image", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_List_ForKiosk(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer,
'                                          ByVal store_name As String, ByVal location_id As Integer, ByVal machine_id As Integer,
'                                          ByVal IsKiosk As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@IsKiosk", IsKiosk, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Product_ForKiosk", oColSqlPar, "WS_P_Get_Product_ForKiosk", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal product_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Product_Details", oColSqlPar, "WS_P_Get_Product_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal product_id As Integer,
'                                ByVal Category_id As Integer, ByVal name As String, ByVal code As String, ByVal price As Decimal, ByVal barcode As String, ByVal Description As String,
'                                ByVal is_active As Integer, ByVal ip_address As String, ByVal department_id As Integer, ByVal course_id As Integer, ByVal printer_id As String, ByVal key_map_id As Integer,
'                                ByVal List As Integer, ByVal Actual_Price As Decimal, ByVal Tax_id As Integer, ByVal Tax As Decimal, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal other_id As String, ByVal other_size As String, ByVal store_name As String, ByVal Is_Ingredient As Integer, ByVal Is_Condiment As Integer, ByVal Base As String,
'                                ByVal Unit_id As Integer, ByVal size_zero As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Category_id", Category_id, SqlDbType.Int)
'                    oColSqlPar.Add("@key_map_id", key_map_id, SqlDbType.Int)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@price", price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@barcode", barcode)
'                    oColSqlPar.Add("@description", Description)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@department_id", department_id, SqlDbType.Int)
'                    oColSqlPar.Add("@course_id", course_id, SqlDbType.Int)
'                    oColSqlPar.Add("@list", List, SqlDbType.Int)
'                    oColSqlPar.Add("@printer_id", printer_id.Replace("@", "#"))
'                    oColSqlPar.Add("@Actual_Price ", Actual_Price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Tax_id", Tax_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tax ", Tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@other_id", other_id)
'                    oColSqlPar.Add("@other_size", other_size)
'                    oColSqlPar.Add("@Is_Ingredient", Is_Ingredient, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Is_Condiment", Is_Condiment, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Base", Base)
'                    oColSqlPar.Add("@Unit_id", Unit_id, SqlDbType.Int)
'                    oColSqlPar.Add("@size_zero", size_zero, SqlDbType.TinyInt)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Product_New", oColSqlPar, "WS_P_M_Product_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Product_IsOutOfStock(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal product_id As Integer, ByVal IsOutofStock As Integer,
'                                ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@IsOutofStock", IsOutofStock, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Product_IsOutOfStock", oColSqlPar, "WS_Product_IsOutOfStock", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Change_Password(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal old_pass As String, ByVal new_pass As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then

'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@password", Encrypt(Decode_data(old_pass)))
'                    oColSqlPar.Add("@NewPassword", Encrypt(Decode_data(new_pass)))
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_ChangePwd", oColSqlPar, "WS_Get_M_ChangePwd", getconstr(dt))
'                    Return "Success"

'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Role_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Role", oColSqlPar, "WS_Get_M_Role", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Countries_List(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Countries", oColSqlPar, "WS_Get_M_Countries", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function States_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Country_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Country_id", Country_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_States", oColSqlPar, "WS_Get_M_States", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Cities_List(ByVal authUsername As String, ByVal authPassword As String, ByVal State_Id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@State_Id", State_Id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Cities", oColSqlPar, "WS_Get_M_Cities", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_By_CatId(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_Id As Integer, ByVal category_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Cmp_id", cmp_Id, SqlDbType.Int)
'                    oColSqlPar.Add("@category_id", category_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Product_By_CatId", oColSqlPar, "WS_P_Get_Product_By_CatId", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    'Darshan
'    <WebMethod()>
'    Public Function Function_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Till_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Till_id", Till_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Function", oColSqlPar, "get_M_Function", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Sales", oColSqlPar, "get_M_Sales", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function TSales_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_T_Sales", oColSqlPar, "get_T_Sales", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales", oColSqlPar, "WS_P_M_Sales", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function TSales_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal tsales_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal sales_total_amount As Decimal, ByVal product_id As Integer,
'                             ByVal sales_discount As Decimal, ByVal quntity As Integer, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal price As Decimal, ByVal store_name As String, ByVal sales_net_amount As Decimal, ByVal sales_tax_amount As Decimal, ByVal sales_actual_price As Decimal, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal machine_id As Integer, ByVal location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@tsales_id", tsales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@sales_total_amount", sales_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sales_discount", sales_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@quntity", quntity, SqlDbType.Int)
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@price", price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sales_net_amount", sales_net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sales_tax_amount", sales_tax_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sales_actual_price", sales_actual_price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_T_Sales", oColSqlPar, "P_T_Sales", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Full(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                              ByVal values As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@values", values)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full", oColSqlPar, "WS_P_M_Sales_Full", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Sales_Master_Full_Discount(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                             ByVal Discount_mode As String, ByVal discount_name As String, ByVal values As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@values", values)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full", oColSqlPar, "WS_P_M_Sales_Full", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Sales_Master_Full_Discount_New(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                             ByVal Discount_mode As String, ByVal discount_name As String, ByVal values As String, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full_New", oColSqlPar, "WS_P_M_Sales_Full_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Table_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Table", oColSqlPar, "get_T_Table", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                        'Return
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Table_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal table_id As Integer, ByVal login_id As Integer, ByVal name As String,
'                             ByVal min_cover As Integer, ByVal max_cover As Integer, ByVal is_active As Integer, ByVal is_open As Integer, ByVal ip_address As String, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@table_id", table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@min_cover", min_cover, SqlDbType.Int)
'                    oColSqlPar.Add("@max_cover", max_cover, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_open", is_open, SqlDbType.TinyInt)
'                    ' oColSqlPar.Add("@shorting_no", shorting_no, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Table_New", oColSqlPar, "WS_P_M_Table_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Key_Map_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Location_id As Integer, ByVal Venue_id As Integer, ByVal Till_id As Integer) As String   'ByVal Venue_id As Integer, ByVal Till_id As Integer
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Location_id", Location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Venue_id", Venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Till_id", Till_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Key_Map", oColSqlPar, "get_T_Table", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_Group_By_KeyMapId(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_Id As Integer, ByVal key_map_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Cmp_id", cmp_Id, SqlDbType.Int)
'                    oColSqlPar.Add("@key_map_id", key_map_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Category_By_KeyMapId", oColSqlPar, "WS_Get_M_Category_By_KeyMapId", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Product_By_KeyMapId(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_Id As Integer, ByVal key_map_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Cmp_id", cmp_Id, SqlDbType.Int)
'                    oColSqlPar.Add("@key_map_id", key_map_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Product_By_KeyMapId", oColSqlPar, "WS_Get_M_Product_By_KeyMapId", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Location_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Venue_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Venue_id", Venue_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Location", oColSqlPar, "WS_P_M_Location", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Location_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal Location_id As Integer, ByVal login_id As Integer, ByVal name As String,
'                             ByVal address As String, ByVal code As String, ByVal city_id As Integer, ByVal state_id As Integer, ByVal country_id As Integer, ByVal ip_address As String,
'                             ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal store_name As String,
'                             ByVal till_auto_log_off As Integer, ByVal machine_id As Integer, ByVal is_active As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@location_id", Location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@address", address)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@city_id", city_id, SqlDbType.Int)
'                    oColSqlPar.Add("@state_id", state_id, SqlDbType.Int)
'                    oColSqlPar.Add("@country_id", country_id, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@till_auto_log_off", till_auto_log_off)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", 1)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Location_New", oColSqlPar, "WS_P_M_Location_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Machine_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Location_id", Location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Machine", oColSqlPar, "WS_Get_M_Machine", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Machine_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal machine_id As Integer, ByVal login_id As Integer, ByVal name As String,
'                             ByVal serial_no As Integer, ByVal code As String, ByVal mac_address As String, ByVal model As String, ByVal ip_address As String, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal location_id As Integer, ByVal is_assign As Byte, ByVal is_minipos As Byte, ByVal is_active As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@mac_address", mac_address)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@model", model)
'                    oColSqlPar.Add("@serial_no", serial_no, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_assign", is_assign, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_minipos", is_minipos, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Machine_New", oColSqlPar, "WS_P_M_Machine_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Device", oColSqlPar, "WS_Get_M_Device", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal device_id As Integer, ByVal machine_id As Integer, ByVal login_id As Integer, ByVal name As String,
'                             ByVal serial_no As String, ByVal code As String, ByVal ip_address As String, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal Device_Type_id As Integer, ByVal is_active As Integer, ByVal printer_ip_address As String, ByVal port As Integer,
'                             ByVal network_type As String, ByVal vender_id As Integer, ByVal budrate As String, ByVal device_name As String, ByVal Width As Integer, ByVal Printer_ID As Integer, ByVal Detail_device_ID As Integer, ByVal Detail_machine_ID As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@device_id", device_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@serial_no", serial_no)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@printer_ip_address", printer_ip_address)
'                    oColSqlPar.Add("@port", port, SqlDbType.Int)
'                    oColSqlPar.Add("@network_type", network_type)
'                    oColSqlPar.Add("@vender_id", vender_id, SqlDbType.Int)
'                    oColSqlPar.Add("@budrate", budrate)
'                    oColSqlPar.Add("@device_name", device_name)
'                    oColSqlPar.Add("@width", Width, SqlDbType.Int)
'                    oColSqlPar.Add("@Printer_ID", Printer_ID, SqlDbType.Int)
'                    oColSqlPar.Add("@Detail_device_ID", Detail_device_ID, SqlDbType.Int)
'                    oColSqlPar.Add("@Detail_machine_ID", Detail_machine_ID, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Device", oColSqlPar, "WS_P_M_Device", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Product_ProductGroup_keymapId(ByVal authUsername As String, ByVal authPassword As String, ByVal keymap_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@key_map_id", keymap_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Product_Category_By_KayMapId", oColSqlPar, "WS_P_Get_Product_Category_By_KayMapId", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function SignIn(ByVal authUsername As String, ByVal authPassword As String, ByVal till_code As Integer, ByVal IP_Address As String, ByVal store_name As String, ByVal Authentication_Code As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@till_code", till_code, SqlDbType.Int)
'                    oColSqlPar.Add("@Login_URL", ConfigurationManager.AppSettings.Get("Login_URL"))
'                    oColSqlPar.Add("@Authentication_Code", Authentication_Code)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_do_SignIn", oColSqlPar, "WS_do_SignIn", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", IP_Address)
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    '<WebMethod()> _
'    'Public Function Upd_StaffImg(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal Staff_id As Integer, ByVal Photo As String) As String
'    '    Try
'    '        Dim dtCode As DataTable = oClsDataccess.Getdatatable("select code as code from M_Company c inner join M_Staff e on e.cmp_id = c.Company_id  where e.Staff_id= " + Staff_id.ToString())
'    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'    '        oColSqlparram.Add("@code", dtCode.Rows(0)("code").ToString)
'    '        Dim dt As DataTable = GetdataTableSp("WS_Get_Login", oColSqlparram, "W_Get_Login")
'    '        If dt.Rows.Count > 0 Then
'    '            If Decode_data(authUsername) = "admin" And Decode_data(authPassword) = "admin@123" Then
'    '                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'    '                oColSqlPar.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
'    '                oColSqlPar.Add("@staff_id", Staff_id, SqlDbType.Int)
'    '                oColSqlPar.Add("@Photo", Photo)
'    '                Dim dtl As DataTable = GetdataTableSp("WS_Upd_StaffImg", oColSqlPar, "Type")
'    '                Dim str As String = data_table(dtl)
'    '                Return str
'    '                'Return "Success"
'    '                'Return data_table(datatbl)
'    '            Else
'    '                Return "Invalid UserName or Password"
'    '            End If
'    '        Else
'    '            oColSqlparram.Add("@User_Name", authUsername)
'    '            oColSqlparram.Add("@Password", authPassword)
'    '            oColSqlparram.Add("@IP_Address", "")
'    '            oColSqlparram.Add("@Access_Status", 0)
'    '            oColSqlparram.Add("@mac_id", "Web")
'    '            oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'    '            ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'    '            Return "Invalid Webservice User Access...!"
'    '        End If
'    '    Catch ex As Exception
'    '        Return ex.ToString
'    '    End Try
'    'End Function
'    <WebMethod()>
'    Public Function Upd_StaffImg(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal Staff_id As Integer, ByVal Photo As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then
'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@staff_id", Staff_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Photo", Photo)
'                    Dim dtl As DataTable = GetdataTableSp("WS_Upd_StudentImg", oColSqlPar, "Type", getconstr(dt))
'                    Dim str As String = data_table(dtl)
'                    Return str
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.ToString
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Key_Map_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal key_map_id As Integer, ByVal login_id As Integer, ByVal name As String,
'                             ByVal description As String, ByVal ip_address As String, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal machine_id As Integer, ByVal is_active As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@key_map_id", key_map_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@description", description)
'                    ' oColSqlPar.Add("@shorting_no", sorting_no)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Key_Map_New", oColSqlPar, "WS_P_M_Key_Map_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Sales_By_Date(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal date1 As DateTime, ByVal store_name As String, ByVal venue_id As Integer, ByVal location_id As Integer, ByVal machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@date1", date1, SqlDbType.DateTime)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Sales_By_Date", oColSqlPar, "WS_Get_M_Sales_By_Date", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Tax_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal Tax_id As Integer, ByVal login_id As Integer, ByVal Name As String,
'                             ByVal Mode As String, ByVal Value As Decimal, ByVal Effective_Date As DateTime, ByVal Is_active As Integer, ByVal ip_address As String, ByVal Tran_Type As String, ByVal mac_id As String, ByVal venue_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Tax_id", Tax_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Name", Name)
'                    oColSqlPar.Add("@Mode", Mode)
'                    oColSqlPar.Add("@Value", Value, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_active", Is_active)
'                    oColSqlPar.Add("@Effective_Date", Effective_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Tax", oColSqlPar, "WS_P_M_Tax", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Tax_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Tax", oColSqlPar, "WS_Get_M_Tax", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Sales_Z_Report(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal date1 As DateTime, ByVal date2 As DateTime, ByVal store_name As String, ByVal location_id As Integer, ByVal machine_id As Integer, ByVal venue_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@date1", date1, SqlDbType.DateTime)
'                    oColSqlPar.Add("@date2", date2, SqlDbType.DateTime)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Z_Report", oColSqlPar, "WS_Get_Z_Report", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Company_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Company_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Company_id", Company_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Company", oColSqlPar, "WS_Get_M_Company", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Company_List(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Company_List", oColSqlPar, "WS_Get_Company_List", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function TSales_By_Date(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal date1 As DateTime, ByVal store_name As String, ByVal venue_id As Integer, ByVal location_id As Integer, ByVal machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@date1", date1, SqlDbType.DateTime)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_T_Sales_By_Date", oColSqlPar, "WS_Get_T_Sales_By_Date", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function TSales_Master1(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal values As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@values", values)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_T_Sales1", oColSqlPar, "WS_P_T_Sales1", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                'oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Table_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal table_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@table_id", table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Table_Details", oColSqlPar, "WS_Get_M_Table_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Machine_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal machine_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Machine_Details", oColSqlPar, "WS_Get_M_Machine_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Location_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal location_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Location_Details", oColSqlPar, "WS_Get_M_Location_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function KeyMap_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal Key_map_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Key_map_id", Key_map_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Key_Map_Details", oColSqlPar, "WS_Get_M_Key_Map_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal device_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@device_id", device_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Device_Details", oColSqlPar, "WS_Get_M_Device_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Settings(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Settings", oColSqlPar, "WS_Get_Settings", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod>
'    Public Function Role_Wise_Menus(ByVal authUsername As String, ByVal authPassword As String, ByVal Role_id As Integer, ByVal Type As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)
'            If dt.Rows.Count > 0 Then
'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Role_Id", Role_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Type", Type, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Load_Form", oColSqlPar, "WS_P_Load_Form", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function
'    <WebMethod>
'    Public Function Role_Wise_Form(ByVal authUsername As String, ByVal authPassword As String, ByVal Role_id As Integer, ByVal Form_Name As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)
'            If dt.Rows.Count > 0 Then
'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Role_id", Role_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Form_Name", Form_Name)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Role", oColSqlPar, "WS_P_Get_Role", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function
'    <WebMethod()>
'    Public Function Venue_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Venue", oColSqlPar, "get_M_Customer", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Venue_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal venue_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Venue_Details", oColSqlPar, "WS_Get_M_Venue_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If

'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If

'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Venue_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal venue_id As Integer, ByVal venue_name As String,
'                             ByVal description As String, ByVal mac_id As String, ByVal Tran_Type As String, ByVal store_name As String, ByVal print_receipt As Integer, ByVal print_duplicate_receipt As Integer, ByVal machine_id As Integer, ByVal is_active As Integer, ByVal login_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@venue_name", venue_name)
'                    oColSqlPar.Add("@description", description)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@print_receipt", print_receipt)
'                    oColSqlPar.Add("@print_duplicate_receipt", print_duplicate_receipt)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Venue_New", oColSqlPar, "WS_P_M_Venue_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Sales_Type_List(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Sales_Type", oColSqlPar, "WS_Get_M_Sales_Type", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Machine_Assign_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal machine_id As Integer, ByVal store_name As String, ByVal is_assign As Byte) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_assign", is_assign, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Assign_Machine", oColSqlPar, "WS_Get_Assign_Machine", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If

'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If

'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Location_By_Venue(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal venue_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Location_By_Venue", oColSqlPar, "WS_Get_Location_By_Venue", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If

'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If

'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Machine_By_Location(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal location_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Machine_By_Location", oColSqlPar, "WS_Get_Machine_By_Location", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If

'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If

'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Prefix_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Prefix_id As Integer,
'                             ByVal Prefix As String, ByVal Tran_Type As String, ByVal store_name As String, ByVal cmp_id As Integer, ByVal login_id As Integer, ByVal ip_address As String, ByVal is_active As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Prefix_id", Prefix_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Prefix", Prefix)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Prefix_New", oColSqlPar, "WS_P_M_Prefix_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Prefix_List(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal cmp_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Prefix", oColSqlPar, "WS_Get_M_Prefix", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Prefix_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal Prefix_id As Integer, ByVal store_name As String, ByVal cmp_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Prefix_id", Prefix_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Prefix_Details", oColSqlPar, "WS_Get_M_Prefix_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_Type_List(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal cmp_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Device_Type", oColSqlPar, "WS_Get_M_Device_Type", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_Type_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal Device_Type_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Device_Type_Details", oColSqlPar, "WS_Get_M_Device_Type_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_Type_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal Device_Type_id As Integer, ByVal Device_Type As String, ByVal login_id As Integer, ByVal ip_address As String, ByVal Tran_Type As String, ByVal store_name As String, ByVal is_active As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Device_Type", Device_Type)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Device_Type", oColSqlPar, "WS_P_M_Device_Type", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Barcode_List(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal cmp_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Barcode_Size", oColSqlPar, "WS_Get_M_Barcode_Size", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Barcode_Details(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal Barcode_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Barcode_Size_id", Barcode_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Barcode_Size_Details", oColSqlPar, "WS_Get_M_Barcode_Size_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Barcode_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal Barcode_id As Integer, ByVal Barcode As String, ByVal product_id As Integer, ByVal login_id As Integer, ByVal ip_address As String, ByVal Tran_Type As String, ByVal store_name As String, ByVal Size_Id As Integer, ByVal is_active As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Barcode_Size_id", Barcode_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Barcode", Barcode)
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@Size_Id", Size_Id)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Barcode_Size_New", oColSqlPar, "WS_P_M_Barcode_Size_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Barcode_By_Product(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal product_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Barcode_Size_Product", oColSqlPar, "WS_Get_M_Barcode_Size_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Price_By_Size(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal Size_id As Integer, ByVal store_name As String, ByVal Location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Size_id", Size_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Location_id", Location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Price_Size", oColSqlPar, "WS_Get_M_Price_Size", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Ingredient_List(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal product_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Product_Ingredien_By_Product", oColSqlPar, "WS_Get_M_Product_Ingredien_By_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Condiment_List(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Condiment_List", oColSqlPar, "WS_Get_M_Condiment_List", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Product_Condiment_List(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal store_name As String, ByVal product_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Product_Condiment_List", oColSqlPar, "WS_Get_M_Product_Condiment_List", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Status"":""False"",""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Size_List(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal product_id As Integer, ByVal store_name As String, ByVal Location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Location_id", Location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Size_Product", oColSqlPar, "WS_Get_M_Size_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Payment_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal Payment_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Payment_id", Payment_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Payment", oColSqlPar, "WS_Get_M_Payment", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Printer_Detail_By_Product(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal Product_Id As Integer, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@product_id", Product_Id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Printer_Product", oColSqlPar, "WS_Get_M_Printer_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Printer_Detail_By_Machine(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal machine_id As Integer, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Printer_Machine", oColSqlPar, "WS_Get_M_Printer_Machine", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_List_With_Payment(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Till_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Till_id", Till_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Device_With_Payment", oColSqlPar, "WS_Get_M_Device_With_Payment", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function T_payment_with_card(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal sale_id As String,
'                                      ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As DateTime, ByVal status_message As String, ByVal transaction_id As String,
'                                      ByVal payment_account_data_token As String,
'                                       ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@sale_id", sale_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.DateTime)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_T_Payment_card", oColSqlPar, "WS_P_T_Payment_card", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Full_payment_with_card(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                             ByVal values As String, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                      ByVal payment_account_data_token As String,
'                                       ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal Discount_mode As String,
'                                       ByVal discount_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full_Payment_Card", oColSqlPar, "WS_P_M_Sales_Full_Payment_Card", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Full_payment_with_card_discount(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                             ByVal values As String, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                      ByVal payment_account_data_token As String, ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal Discount_mode As String,
'                                       ByVal discount_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full_Payment_Card", oColSqlPar, "WS_P_M_Sales_Full_Payment_Card", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Reason_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Reason", oColSqlPar, "WS_Get_M_Reason", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_chg_password(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal service_key As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@service_key", service_key, SqlDbType.NVarChar)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Device_Chg_Password", oColSqlPar, "WS_Device_Chg_Password", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function P_M_Store_version(ByVal store_name As String, ByVal version_no As String, ByVal device_sapce As String) As String
'        Try
'            Dim com As New SqlCommand
'            Dim SpAdepter As New SqlDataAdapter
'            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
'            con.Open()



'            Dim cmd As SqlCommand = New SqlCommand("WS_P_M_Version", con)
'            cmd.CommandType = CommandType.StoredProcedure
'            cmd.Parameters.AddWithValue("@store_name", store_name)
'            cmd.Parameters.AddWithValue("@version_no", version_no)
'            cmd.Parameters.AddWithValue("@device_space", device_sapce)

'            Dim adpter As New SqlDataAdapter(cmd)
'            Dim dt As DataTable = New DataTable()
'            adpter.Fill(dt)
'            con.Close()
'            If dt.Rows.Count > 0 Then
'                Return data_table(dt)
'            Else
'                Return "[{""Message"":""No Data Found.""}]"
'            End If

'        Catch ex As Exception
'            LogHelper.Error("POS_WS:P_M_Store_version():" + ex.Message)
'            Throw New Exception(ex.Message)
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function P_M_Sync_table(ByVal sync_id As Integer, ByVal sales_id As Integer, ByVal for_date As String, ByVal cmp_id As Integer, ByVal store_name As String, ByVal till_name As String,
'                                   ByVal venue_id As Integer, ByVal Tran_Type As String, ByVal machine_id As Integer, ByVal app_version As String, ByVal db_version As String, ByVal login_id As Integer,
'                                   ByVal ip_address As String, ByVal type As String, ByVal description As String) As String
'        Try
'            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
'            con.Open()
'            Dim cmd As SqlCommand = New SqlCommand("Ws_M_sync", con)
'            cmd.CommandType = CommandType.StoredProcedure
'            cmd.Parameters.AddWithValue("@sync_id", sync_id)
'            cmd.Parameters.AddWithValue("@sales_id", sales_id)
'            cmd.Parameters.AddWithValue("@for_date", for_date)
'            cmd.Parameters.AddWithValue("@cmp_id", cmp_id)
'            cmd.Parameters.AddWithValue("@store_name", store_name)
'            cmd.Parameters.AddWithValue("@till_name", till_name)
'            cmd.Parameters.AddWithValue("@venue_id", venue_id)
'            cmd.Parameters.AddWithValue("@Tran_Type", Tran_Type)
'            cmd.Parameters.AddWithValue("@machine_id", machine_id)
'            cmd.Parameters.AddWithValue("@app_version", app_version)
'            cmd.Parameters.AddWithValue("@db_version", db_version)
'            cmd.Parameters.AddWithValue("@login_id", login_id)
'            cmd.Parameters.AddWithValue("@ip_address", ip_address)
'            cmd.Parameters.AddWithValue("@type", type)
'            cmd.Parameters.AddWithValue("@description", description)

'            Dim adpter As New SqlDataAdapter(cmd)
'            Dim dt As DataTable = New DataTable()
'            adpter.Fill(dt)
'            con.Close()
'            If dt.Rows.Count > 0 Then
'                Return data_table(dt)
'            Else
'                Return "[{""Message"":""No Data Found.""}]"
'            End If

'        Catch ex As Exception
'            LogHelper.Error("POS_WS:P_M_Store_version():" + ex.Message)
'            Throw New Exception(ex.Message)
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function P_M_App_info(ByVal App_info_id As Integer, ByVal type As String, ByVal for_date As String, ByVal cmp_id As Integer, ByVal store_name As String, ByVal till_name As String,
'                                   ByVal venue_id As Integer, ByVal machine_id As Integer, ByVal app_version As String, ByVal db_version As String, ByVal login_id As Integer,
'                                  ByVal ip_address As String, ByVal Tran_Type As String) As String
'        Try
'            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
'            con.Open()
'            Dim cmd As SqlCommand = New SqlCommand("Ws_M_App_info", con)
'            cmd.CommandType = CommandType.StoredProcedure

'            cmd.Parameters.AddWithValue("@App_info_id", App_info_id)
'            cmd.Parameters.AddWithValue("@type ", type)
'            cmd.Parameters.AddWithValue("@for_date", for_date)
'            cmd.Parameters.AddWithValue("@cmp_id", cmp_id)
'            cmd.Parameters.AddWithValue("@store_name", store_name)
'            cmd.Parameters.AddWithValue("@till_name", till_name)
'            cmd.Parameters.AddWithValue("@venue_id", venue_id)
'            cmd.Parameters.AddWithValue("@machine_id", machine_id)
'            cmd.Parameters.AddWithValue("@app_version", app_version)
'            cmd.Parameters.AddWithValue("@db_version", db_version)
'            cmd.Parameters.AddWithValue("@login_id", login_id)
'            cmd.Parameters.AddWithValue("@ip_address", ip_address)
'            cmd.Parameters.AddWithValue("@Tran_Type", Tran_Type)

'            Dim adpter As New SqlDataAdapter(cmd)
'            Dim dt As DataTable = New DataTable()
'            adpter.Fill(dt)
'            con.Close()
'            If dt.Rows.Count > 0 Then
'                Return data_table(dt)
'            Else
'                Return "[{""Message"":""No Data Found.""}]"
'            End If

'        Catch ex As Exception
'            LogHelper.Error("POS_WS:P_M_App_info():" + ex.Message)
'            Throw New Exception(ex.Message)
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function P_M_Store_expire_list(ByVal store_name As String) As String
'        Try
'            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
'            con.Open()
'            Dim cmd As SqlCommand = New SqlCommand("P_M_Get_expire", con)
'            cmd.CommandType = CommandType.StoredProcedure
'            cmd.Parameters.AddWithValue("@store_name", store_name)

'            Dim adpter As New SqlDataAdapter(cmd)
'            Dim dt As DataTable = New DataTable()
'            adpter.Fill(dt)
'            con.Close()
'            If dt.Rows.Count > 0 Then
'                Return data_table(dt)
'            Else
'                Return "[{""Message"":""No Data Found.""}]"
'            End If

'        Catch ex As Exception
'            LogHelper.Error("POS_WS:P_M_Store_version():" + ex.Message)
'            Throw New Exception(ex.Message)
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function ResDiary_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    'oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_ResDiaryBooking", oColSqlPar, "WS_Get_ResDiaryBooking", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function ResDiary_update(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal BookingId As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    'oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@BookingId", BookingId, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Update_ResDiaryBooking", oColSqlPar, "WS_Update_ResDiaryBooking", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "Status Updated."
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Insert_Company_Detail(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Name As String,
'                                          ByVal Domain As String, ByVal Description As String, ByVal Receipt_Header As String, ByVal Receipt_Footer As String,
'                                          ByVal Venue_Name As String, ByVal log_off_time As Decimal, ByVal par_sale_par_operator As Decimal,
'                                          ByVal Website As String, ByVal Email As String, ByVal Contact As String, ByVal Vat_No As String,
'                                          ByVal Address As String, ByVal Tran_Type As String) As String

'        ', ByVal Starting_Date As DateTime, ByVal IP_Address As String,
'        '                                  ByVal Country As Decimal, ByVal State As Decimal, ByVal City As Decimal, ByVal Code As String,
'        '                                  ByVal Postal As String, ByVal Registration_no As Decimal,
'        '                                  ByVal GST_VAT As Decimal, ByVal CST_VAT As Decimal, ByVal Service_tax_no As Decimal, ByVal Pan_no As Decimal,
'        '                              ByVal Branch_Name As String, ByVal Synchronization As String, ByVal Currency_id As Decimal
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Company_id", Cmp_id)
'                    oColSqlPar.Add("@Name", Name)
'                    oColSqlPar.Add("@Address", Address)
'                    'oColSqlPar.Add("@Starting_Date", Starting_Date)
'                    oColSqlPar.Add("@Domain", Domain)
'                    'oColSqlPar.Add("@IP_Address", IP_Address)
'                    'oColSqlPar.Add("@Country", Country)
'                    'oColSqlPar.Add("@State", State)
'                    'oColSqlPar.Add("@City", City)
'                    'oColSqlPar.Add("@Code", Code)
'                    oColSqlPar.Add("@Email", Email)
'                    oColSqlPar.Add("@Description", Description)
'                    'oColSqlPar.Add("@Postal", Postal)
'                    oColSqlPar.Add("@Website", Website)
'                    oColSqlPar.Add("@Contact", Contact)
'                    'oColSqlPar.Add("@Registration_no", Registration_no)
'                    'oColSqlPar.Add("@GST_VAT", GST_VAT)
'                    'oColSqlPar.Add("@CST_VAT", CST_VAT)
'                    'oColSqlPar.Add("@Service_tax_no", Service_tax_no)
'                    'oColSqlPar.Add("@Pan_no", Pan_no)
'                    'oColSqlPar.Add("@Branch_Name", Branch_Name)
'                    'oColSqlPar.Add("@Synchronization", Synchronization)
'                    oColSqlPar.Add("@Venue_Name", Venue_Name)
'                    oColSqlPar.Add("@Vat_No", Vat_No)
'                    oColSqlPar.Add("@Receipt_Header", Receipt_Header)
'                    oColSqlPar.Add("@Receipt_Footer", Receipt_Footer)
'                    oColSqlPar.Add("@log_off_time", log_off_time)
'                    oColSqlPar.Add("@par_sale_par_operator", par_sale_par_operator)
'                    'oColSqlPar.Add("@Currency_id", Currency_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Company_new", oColSqlPar, "P_M_Company_new", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Function_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal function_id As Integer,
'                                 ByVal name As String, ByVal code As String, ByVal caption_type As String, ByVal item As Decimal, ByVal back_color As String,
'                                 ByVal for_color As String, ByVal big_button As Integer, ByVal payment_id As Decimal, ByVal is_groupby_dept As Integer,
'                                 ByVal is_groupby_course As Integer, ByVal dept_id As String, ByVal course_id As String, ByVal Ip_Address As String,
'                                  ByVal Login_id As Integer, ByVal Tran_Type As String, ByVal Panel_Id As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@function_id", function_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@caption_type", caption_type)
'                    oColSqlPar.Add("@item", item)
'                    oColSqlPar.Add("@back_color", back_color)
'                    oColSqlPar.Add("@for_color", for_color)
'                    oColSqlPar.Add("@big_button", big_button)
'                    oColSqlPar.Add("@Payment_id", payment_id)
'                    oColSqlPar.Add("@is_groupby_dept", is_groupby_dept)
'                    oColSqlPar.Add("@is_groupby_course", is_groupby_course)
'                    oColSqlPar.Add("@dept_id", dept_id)
'                    oColSqlPar.Add("@course_id", course_id)
'                    oColSqlPar.Add("@ip_address", Ip_Address)
'                    oColSqlPar.Add("@login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@Panel_id", Panel_Id)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Function_new", oColSqlPar, "P_M_Function_new", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Size_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal product_id As Integer,
'                                  ByVal Size_Id As Integer, ByVal Size_Unit As String, ByVal Size As Decimal, ByVal Name As String, ByVal Unit As String, ByVal Ip_Address As String,
'                                  ByVal Login_id As Integer, ByVal Tran_Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Size_Id", Size_Id)
'                    oColSqlPar.Add("@Size", Size)
'                    oColSqlPar.Add("@product_id", product_id)
'                    oColSqlPar.Add("@Size_Unit", Size_Unit)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@Name", Name)
'                    oColSqlPar.Add("@Unit", Unit)
'                    oColSqlPar.Add("@ip_address", Ip_Address)
'                    oColSqlPar.Add("@login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Size_new", oColSqlPar, "P_M_Size_new", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Ingredient_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal product_id As Decimal,
'                                  ByVal Ingredient_id As Decimal, ByVal Tran_Id As Decimal, ByVal Size_id As Decimal, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal Tran_Type As String, ByVal Qty As Decimal) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Tran_Id", Tran_Id)
'                    oColSqlPar.Add("@product_id", product_id)
'                    oColSqlPar.Add("@Ingredient_id", Ingredient_id)
'                    oColSqlPar.Add("@Size_Id", Size_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@ip_address", Ip_Address)
'                    oColSqlPar.Add("@login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@Price", 0)
'                    oColSqlPar.Add("@Qty", Qty)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Product_Ingredient_New", oColSqlPar, "P_M_Product_Ingredient_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Product_Condiment_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal product_id As Integer,
'                                  ByVal Condiment_Id As Integer, ByVal Tran_id As Decimal, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal Tran_Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Tran_id", Tran_id)
'                    oColSqlPar.Add("@Condiment_id", Condiment_Id)
'                    oColSqlPar.Add("@product_id", product_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@ip_address", Ip_Address)
'                    oColSqlPar.Add("@login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Product_Condiment_New", oColSqlPar, "P_M_Product_Condiment_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Condiment_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal product_id As Integer,
'                                  ByVal Condiment_Id As Integer, ByVal Condiment As String, ByVal is_add_substract As Integer, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal Tran_Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Condiment_id", Condiment_Id)
'                    oColSqlPar.Add("@product_id", product_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@Condiment", Condiment)
'                    oColSqlPar.Add("@is_add_substract", is_add_substract)
'                    oColSqlPar.Add("@ip_address", Ip_Address)
'                    oColSqlPar.Add("@login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Condiment_New", oColSqlPar, "P_M_Condiment_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Department_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal department_id As Integer,
'                                  ByVal name As String, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal Tran_Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Department_id", department_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@Name", name)
'                    'oColSqlPar.Add("@Value", Value)
'                    oColSqlPar.Add("@Ip_Address", Ip_Address)
'                    oColSqlPar.Add("@Login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Department_New", oColSqlPar, "P_M_Department_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function ResDiary_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal BookingId As Integer,
'                                  ByVal BookingReference As String, ByVal VisitDateTime As DateTime, ByVal Duration As Integer, ByVal TurnTime As Integer, ByVal Covers As Integer,
'                                  ByVal Tables As String, ByVal SpecialRequests As String, ByVal CustomerId As Integer, ByVal CustomerFullName As String, ByVal CustomerIsVip As Byte,
'                                  ByVal IsTableLocked As Byte, ByVal HasPromotions As Byte, ByVal HasPayments As Byte, ByVal IsLeaveTimeConfirmed As Byte, ByVal IsWalkIn As Byte,
'                                  ByVal NumberOfBookings As Integer, ByVal AreaId As Integer, ByVal ServiceId As Integer, ByVal Comments As String, ByVal ArrivalStatus As Integer,
'                                  ByVal MealStatus As Integer, ByVal IsDeleted As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@BookingId", BookingId)
'                    oColSqlPar.Add("@BookingReference", BookingReference)
'                    oColSqlPar.Add("@VisitDateTime", VisitDateTime)
'                    oColSqlPar.Add("@Duration", Duration)
'                    oColSqlPar.Add("@TurnTime", TurnTime)
'                    oColSqlPar.Add("@Covers", Covers)
'                    oColSqlPar.Add("@Tables", Tables)
'                    oColSqlPar.Add("@SpecialRequests", SpecialRequests)
'                    oColSqlPar.Add("@CustomerId", CustomerId)
'                    oColSqlPar.Add("@CustomerFullName", CustomerFullName)
'                    oColSqlPar.Add("@CustomerIsVip", CustomerIsVip)
'                    oColSqlPar.Add("@IsTableLocked", IsTableLocked)
'                    oColSqlPar.Add("@HasPromotions", HasPromotions)
'                    oColSqlPar.Add("@HasPayments", HasPayments)
'                    oColSqlPar.Add("@IsLeaveTimeConfirmed", IsLeaveTimeConfirmed)
'                    oColSqlPar.Add("@IsWalkIn", IsWalkIn)
'                    oColSqlPar.Add("@NumberOfBookings", NumberOfBookings)
'                    oColSqlPar.Add("@AreaId", AreaId)
'                    oColSqlPar.Add("@ServiceId", ServiceId)
'                    oColSqlPar.Add("@Comments", Comments)
'                    oColSqlPar.Add("@ArrivalStatus", ArrivalStatus)
'                    oColSqlPar.Add("@MealStatus", MealStatus)
'                    oColSqlPar.Add("@IsDeleted", IsDeleted)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_ResDiaryBooking", oColSqlPar, "P_M_ResDiaryBooking", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Payment_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Payment_Id As Integer,
'                                  ByVal Name As String, ByVal Hostname As String, ByVal Password As String, ByVal CurrencyCode As Decimal,
'                                  ByVal TransactionType As String, ByVal Ip_Address As String, ByVal Login_id As Decimal, ByVal Tran_Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Payment_Id", Payment_Id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@Name", Name)
'                    oColSqlPar.Add("@Hostname", Hostname)
'                    oColSqlPar.Add("@Password", Password)
'                    oColSqlPar.Add("@CurrencyCode", CurrencyCode)
'                    oColSqlPar.Add("@TransactionType", TransactionType)
'                    oColSqlPar.Add("@Login_id", Login_id)
'                    oColSqlPar.Add("@Ip_Address", Ip_Address)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Payment_New", oColSqlPar, "P_M_Payment_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Course_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Course_id As Integer,
'                                  ByVal name As String, ByVal Value As Decimal, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal Tran_Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Course_id", Course_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@Name", name)
'                    oColSqlPar.Add("@Value", Value)
'                    oColSqlPar.Add("@Ip_Address", Ip_Address)
'                    oColSqlPar.Add("@Login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Course_New", oColSqlPar, "P_M_Course_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Reason_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal reason_id As Integer,
'                                  ByVal reason As String, ByVal description As String, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal Tran_Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@reason_id", reason_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@reason", reason)
'                    oColSqlPar.Add("@description", description)
'                    oColSqlPar.Add("@ip_address", Ip_Address)
'                    oColSqlPar.Add("@login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Reason_New", oColSqlPar, "P_M_Reason_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Price_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Price_Id As Integer,
'                                  ByVal Price As Decimal, ByVal Location_Id As Integer, ByVal Size_Id As Integer, ByVal Actual_Price As Decimal, ByVal Tax As Decimal, ByVal Product_id As Decimal,
'                                  ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal Tran_Type As String, ByVal Tax_id As Decimal) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Price_Id", Price_Id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@Price", Price)
'                    oColSqlPar.Add("@Location_Id", Location_Id)
'                    oColSqlPar.Add("@Size_Id", Size_Id)
'                    oColSqlPar.Add("@Actual_Price", Actual_Price)
'                    oColSqlPar.Add("@Tax_id", Tax_id)
'                    oColSqlPar.Add("@Tax", Tax)
'                    oColSqlPar.Add("@Product_id", Product_id)
'                    oColSqlPar.Add("@ip_address", Ip_Address)
'                    oColSqlPar.Add("@login_id", Login_id)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Price_New", oColSqlPar, "P_M_Price_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    '<WebMethod()> _
'    'Public Function Role_Master(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Role_Id As Integer,
'    '                              ByVal Role_Name As String, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal Tran_Type As String) As String
'    '    Try

'    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'    '        Dim dt As DataTable = getStore(store_name)

'    '        If dt.Rows.Count > 0 Then

'    '            If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'    '                Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'    '                oColSqlPar.Add("@reason_id", Role_Id)
'    '                oColSqlPar.Add("@cmp_id", Cmp_id)
'    '                oColSqlPar.Add("@reason", Role_Name)
'    '                oColSqlPar.Add("@ip_address", Ip_Address)
'    '                oColSqlPar.Add("@login_id", Login_id)
'    '                oColSqlPar.Add("@Tran_Type", Tran_Type)

'    '                Dim datatbl As DataTable = GetdataTableSp("P_M_Reason", oColSqlPar, "P_M_Reason", getconstr(dt))
'    '                If datatbl.Rows.Count > 0 Then
'    '                    Return "success."
'    '                Else
'    '                    Return "No Data Found OR may be data available."
'    '                End If
'    '            Else
'    '                Return "Invalid UserName or Password"
'    '            End If
'    '        Else
'    '            oColSqlparram.Add("@User_Name", authUsername)
'    '            oColSqlparram.Add("@Password", authPassword)
'    '            oColSqlparram.Add("@IP_Address", "")
'    '            oColSqlparram.Add("@Access_Status", 0)
'    '            oColSqlparram.Add("@mac_id", "Web")
'    '            oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'    '            ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'    '            Return "Invalid Webservice User Access...!"
'    '        End If
'    '    Catch ex As Exception
'    '        Return ex.Message
'    '    End Try
'    'End Function


'    Public Function getStore(ByVal store_name As String) As DataTable
'        Try
'            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)

'            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'            oColSqlPar.Add("@store_name", store_name)
'            Dim dt As DataTable = GetdataTableSp("WS_Get_Store", oColSqlPar, "getStore", con.ConnectionString.ToString())

'            'con.Open()
'            'Dim cmd As SqlCommand = New SqlCommand("WS_Get_Store", con)
'            'cmd.CommandType = CommandType.StoredProcedure

'            'cmd.Parameters.AddWithValue("@store_name", store_name)

'            'Dim adpter As New SqlDataAdapter(cmd)
'            'Dim dt As DataTable = New DataTable()
'            'adpter.Fill(dt)
'            'con.Close()
'            Return dt

'        Catch ex As Exception
'            LogHelper.Error("POS_WS:getStore():" + ex.Message)
'            Throw New Exception(ex.Message)
'        End Try
'    End Function

'    Public Function getconstr(ByVal dt As DataTable) As String
'        Try
'            Dim constr As String = "Data Source=" & dt.Rows(0)("db_server") & ";" & "Initial Catalog=" & dt.Rows(0)("db_name") & ";" & "User ID=" & dt.Rows(0)("db_username") & ";" & "Password=" & Decode_data(dt.Rows(0)("db_password")) & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
'            Return constr

'        Catch ex As Exception
'            LogHelper.Error("POS_WS:getStore():" + ex.Message)
'            Throw New Exception(ex.Message)
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_Group_List_Till(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Category_till", oColSqlPar, "Get_M_Category", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Promotion_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Location_id As Integer, ByVal Venue_id As Integer, ByVal Till_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Location_id", Location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Venue_id", Venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Till_id", Till_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Promotion", oColSqlPar, "WS_Get_M_Promotion", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Promotion_Type_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Location_id As Integer, ByVal Venue_id As Integer, ByVal Till_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Location_id", Location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Venue_id", Venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Till_id", Till_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Promotion_Type", oColSqlPar, "WS_Get_M_Promotion_Type", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Full_Discount_Condiment(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                             ByVal Discount_mode As String, ByVal discount_name As String, ByVal values As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@values", values)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full_Condiment", oColSqlPar, "WS_P_M_Sales_Full_Condiment", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Full_Discount_New_Condiment(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                             ByVal Discount_mode As String, ByVal discount_name As String, ByVal values As String, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full_New_Condiment", oColSqlPar, "WS_P_M_Sales_Full_New_Condiment", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Full_payment_with_card_Condiment(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                             ByVal values As String, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                      ByVal payment_account_data_token As String,
'                                       ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal Discount_mode As String,
'                                       ByVal discount_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full_Payment_Card_Condiment", oColSqlPar, "WS_P_M_Sales_Full_Payment_Card_Condiment", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function KeyMap_Details_New(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Location_id As Integer, ByVal Venue_id As Integer, ByVal Till_id As Integer) As String  'ByVal Venue_id As Integer, ByVal Till_id As Integer
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Location_id", Location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Venue_id", Venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Till_id", Till_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Key_Map_Details_New", oColSqlPar, "WS_Get_M_Key_Map_Details_New", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Query_DATA_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Till_id As Integer, ByVal query As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    'Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    'oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    'oColSqlPar.Add("@Till_id", Till_id, SqlDbType.Int)
'                    ' Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Function", oColSqlPar, "get_M_Function", getconstr(dt))
'                    'If datatbl.Rows.Count > 0 Then
'                    'Return JsonConvert.SerializeObject(query, System.Xml.Formatting.Indented)
'                    Return query
'                    'Else
'                    'Return "[{""Message"":""No Data Found.""}]"
'                    'End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Full_Discount_New_Condiment_RoomPayment(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                             ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                             ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                             ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                             ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                             ByVal Discount_mode As String, ByVal discount_name As String, ByVal values As String, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full_New_Condiment_RoomPayment", oColSqlPar, "WS_P_M_Sales_Full_New_Condiment_RoomPayment", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Full_Discount_Condiment_RoomPayment(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Full_Condiment_RoomPayment", oColSqlPar, "WS_P_M_Sales_Full_Condiment_RoomPayment", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function cash_Declaration(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Till_id As Integer,
'                                  ByVal Fordate As DateTime, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal amount As Double, ByVal type As Int32) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@csh_id", 0, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_amount", amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@cash_amount", amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", Ip_Address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@login_id", Login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@amount_type", type, SqlDbType.Int)
'                    oColSqlPar.Add("@for_date", Fordate, SqlDbType.DateTime)
'                    oColSqlPar.Add("@machine_id", Till_id, SqlDbType.Int)
'                    oColSqlPar.Add("@tran_type", "I", SqlDbType.NVarChar)


'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Cash_Declaration_Till", oColSqlPar, "P_M_Cash_Declaration_Till", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function cash_Declaration_delete(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Till_id As Integer,
'                                 ByVal Fordate As DateTime, ByVal Ip_Address As String, ByVal Login_id As Integer, ByVal amount As Double, ByVal type As Int32) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@csh_id", 0, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_amount", amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@cash_amount", amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", Ip_Address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@login_id", Login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@amount_type", type, SqlDbType.Int)
'                    oColSqlPar.Add("@for_date", Fordate, SqlDbType.DateTime)
'                    oColSqlPar.Add("@machine_id", Till_id, SqlDbType.Int)
'                    oColSqlPar.Add("@tran_type", "I", SqlDbType.NVarChar)


'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Cash_Declaration_Delete", oColSqlPar, "P_M_Cash_Declaration_Delete", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Self_Sales_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Self_Sales", oColSqlPar, "WS_P_Get_Self_Sales", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Self_Sales_inactive(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Sales_id As Integer, ByVal uuid As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Sales_id", Sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@uuid", uuid)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Self_Sales_Inactive", oColSqlPar, "WS_P_Get_Self_Sales_Inactive", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Voucher_Range(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Get_Voucher_Range", oColSqlPar, "WS_P_Get_Voucher_Range", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Email_Setting(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Email_setting", oColSqlPar, "WS_Get_Email_setting", getconstr(dt))

'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table_encode(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Customer_Add(ByVal authUsername As String, ByVal authPassword As String,
'                                 ByVal Cmp_id As Integer, ByVal store_name As String, ByVal name As String,
'                                 ByVal mobile As String, ByVal email As String, ByVal cust_id As String,
'                                 ByVal Is_credit As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@first_name", name)
'                    oColSqlPar.Add("@contact_no", mobile)
'                    oColSqlPar.Add("@email", email)
'                    oColSqlPar.Add("@customer_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_credit", Is_credit, SqlDbType.TinyInt)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Customer", oColSqlPar, "WS_P_M_Customer", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Customer_Add_WithCardNumber(ByVal authUsername As String, ByVal authPassword As String,
'                                 ByVal Cmp_id As Integer, ByVal store_name As String, ByVal name As String,
'                                 ByVal mobile As String, ByVal email As String, ByVal cust_id As String,
'                                 ByVal Is_credit As Integer, ByVal CardNumber As String,
'                                 ByVal DateTimeExpiry As DateTime, ByVal profile_id As Integer, ByVal price_level_id As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@first_name", name)
'                    oColSqlPar.Add("@contact_no", mobile)
'                    oColSqlPar.Add("@email", email)
'                    oColSqlPar.Add("@customer_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_credit", Is_credit, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@CardNumber", CardNumber)
'                    oColSqlPar.Add("@DateTimeExpiry", DateTimeExpiry, SqlDbType.DateTime)
'                    oColSqlPar.Add("@profile_id", profile_id, SqlDbType.Int)
'                    oColSqlPar.Add("@price_level_id", price_level_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Customer", oColSqlPar, "WS_P_M_Customer", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Account(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                               ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                               ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                               ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                               ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                               ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Account", oColSqlPar, "WS_P_M_Sales_Account", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Sales_Master_Account_Update(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal temp_sale_id As Integer,
'                             ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal machine_id As Integer,
'                             ByVal location_id As Integer,
'                              ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_Account_Update", oColSqlPar, "WS_P_M_Sales_Account_Update", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", 0)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_45(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                               ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                               ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                               ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                               ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                               ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                      ByVal payment_account_data_token As String,
'                                       ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_45", oColSqlPar, "WS_P_M_Sales_ver_45", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_59(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                              ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                              ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                              ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                              ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                              ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                     ByVal payment_account_data_token As String,
'                                      ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_59", oColSqlPar, "WS_P_M_Sales_ver_59", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_60(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_60", oColSqlPar, "WS_P_M_Sales_ver_60", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Voucher_tran_add(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String,
'                                 ByVal sales_id As Integer, ByVal customer_id As Integer, ByVal Amount As Decimal,
'                                 ByVal VoucherRef_no As String, ByVal tran_date As DateTime, ByVal ip_address As String,
'                                 ByVal Tran_Type As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@customer_id", customer_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Amount", Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@VoucherRef_no", VoucherRef_no)
'                    oColSqlPar.Add("@tran_date", tran_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_M_VoucherTran", oColSqlPar, "WS_P_Get_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Get_Voucher_tran(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String,
'                                 ByVal customer_id As Integer, ByVal VoucherRef_no As String, ByVal VoucherRef_TYPE As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@customer_id", customer_id, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherRef_no", VoucherRef_no)
'                    oColSqlPar.Add("@VoucherRef_TYPE", VoucherRef_TYPE)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_VoucherTran", oColSqlPar, "WS_P_Get_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function




'    <WebMethod()>
'    Public Function Get_All_CustomerWithVoucher_Balance(ByVal authUsername As String, ByVal authPassword As String,
'                                     ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_all_customer_voucher", oColSqlPar, "WS_P_Get_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_63(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_63", oColSqlPar, "WS_P_M_Sales_ver_63", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Get_Sync_Request(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String,
'                                 ByVal Machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Machine_id", Machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Sync_Request", oColSqlPar, "WS_Get_Sync_Request", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Shift_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Machine_id", Machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Shifts", oColSqlPar, "WS_Get_M_Shifts", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_64(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_64", oColSqlPar, "WS_P_M_Sales_ver_64", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Prices_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal Product_Price_Id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Product_Price", oColSqlPar, "WS_Get_M_Product_Price", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_65(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_65", oColSqlPar, "WS_P_M_Sales_ver_65", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_651(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_651", oColSqlPar, "WS_P_M_Sales_ver_651", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_652(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_652", oColSqlPar, "WS_P_M_Sales_ver_652", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_653(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_653", oColSqlPar, "WS_P_M_Sales_ver_653", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_67(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                                    ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String, ByVal Cust_delivery_Addr As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_67", oColSqlPar, "WS_P_M_Sales_ver_67", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function credit_account_pay(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String,
'                                       ByVal customer_web_id As Decimal, ByVal customer_mobile_no As String, ByVal Amount As Decimal, ByVal credit_date As DateTime,
'                                          ByVal machine_id As Decimal, ByVal location_id As Decimal, ByVal pay_uuid As String, ByVal Tran_Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@customer_web_id", customer_web_id)
'                    oColSqlPar.Add("@customer_mobile_no", customer_mobile_no)
'                    oColSqlPar.Add("@Amount", Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@credit_date", credit_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@machine_id", machine_id)
'                    oColSqlPar.Add("@location_id", location_id)
'                    oColSqlPar.Add("@pay_uuid", pay_uuid)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Credit_Account_Pay", oColSqlPar, "P_M_Credit_Account_Pay", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Customer_Credit_Balance(ByVal authUsername As String, ByVal authPassword As String,
'                                 ByVal store_name As String, ByVal cust_id As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And
'                    authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_Customer_Credit_Balance", oColSqlPar, "WS_get_Customer_Credit_Balance", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_68(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                                    ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String, ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_68", oColSqlPar, "WS_P_M_Sales_ver_68", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_69(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                                   ByVal payment_account_data_token As String,
'                                    ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                                    ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String, ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_69", oColSqlPar, "WS_P_M_Sales_ver_69", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_691(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                              ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_691", oColSqlPar, "WS_P_M_Sales_ver_691", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_692(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                              ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_692", oColSqlPar, "WS_P_M_Sales_ver_692", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function



'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_693(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                              ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                              ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                              ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_693", oColSqlPar, "WS_P_M_Sales_ver_693", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_694(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                              ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                              ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                              ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_694", oColSqlPar, "WS_P_M_Sales_ver_694", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_695(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                              ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                              ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                              ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String, ByVal Elina_Room_No As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_695", oColSqlPar, "WS_P_M_Sales_ver_695", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_696(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                              ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                              ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                              ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String, ByVal Elina_Room_No As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_696", oColSqlPar, "WS_P_M_Sales_ver_696", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_697(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                              ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                              ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                              ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String, ByVal Elina_Room_No As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then



'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_697", oColSqlPar, "WS_P_M_Sales_ver_697", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_698(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                            ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                            ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                            ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String,
'                            ByVal Elina_Room_No As String, ByVal TIP_AMOUNT As Decimal) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    oColSqlPar.Add("@TIP_AMOUNT", TIP_AMOUNT, SqlDbType.Decimal)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_698", oColSqlPar, "WS_P_M_Sales_ver_698", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_699(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                            ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                            ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                            ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String,
'                            ByVal Elina_Room_No As String, ByVal TIP_AMOUNT As Decimal, ByVal CardPayType As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    oColSqlPar.Add("@TIP_AMOUNT", TIP_AMOUNT, SqlDbType.Decimal)
'                    oColSqlPar.Add("@CardPayType", CardPayType, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_699", oColSqlPar, "WS_P_M_Sales_ver_699", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_700(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                            ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                            ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                            ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String,
'                            ByVal Elina_Room_No As String, ByVal TIP_AMOUNT As Decimal, ByVal CardPayType As Integer, ByVal KINETIC_REF_NO As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    oColSqlPar.Add("@TIP_AMOUNT", TIP_AMOUNT, SqlDbType.Decimal)
'                    oColSqlPar.Add("@CardPayType", CardPayType, SqlDbType.Int)
'                    oColSqlPar.Add("@KINETIC_REF_NO", KINETIC_REF_NO)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_700", oColSqlPar, "WS_P_M_Sales_ver_700", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_701(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                            ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                            ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                            ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String,
'                            ByVal Elina_Room_No As String, ByVal TIP_AMOUNT As Decimal, ByVal CardPayType As Integer, ByVal KINETIC_REF_NO As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    oColSqlPar.Add("@TIP_AMOUNT", TIP_AMOUNT, SqlDbType.Decimal)
'                    oColSqlPar.Add("@CardPayType", CardPayType, SqlDbType.Int)
'                    oColSqlPar.Add("@KINETIC_REF_NO", KINETIC_REF_NO)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_701", oColSqlPar, "WS_P_M_Sales_ver_701", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function TableMaster_by_Location(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Sales_by_Location", oColSqlPar, "WS_Get_M_Sales_by_Location", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function TableSalesTransaction_by_Location(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Integer, ByVal table_uuid As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_uuid", table_uuid)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_T_Sales_by_Location", oColSqlPar, "WS_Get_T_Sales_by_Location", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function TableSalesMaster_by_Location(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Integer, ByVal table_uuid As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_uuid", table_uuid)
'                    'oColSqlPar.Add("@store_name", store_name)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Table_Sales_Master_by_Location", oColSqlPar, "WS_Get_Table_Sales_Master_by_Location", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function TableTransaction_by_Location(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal location_id As Integer, ByVal table_uuid As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_uuid", table_uuid)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Table_Transaction_by_Location", oColSqlPar, "WS_Get_Table_Transaction_by_Location", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function ZReport_OperatorWise(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal F_date As DateTime, ByVal login_id As String,
'                                          ByVal location_id As Integer, ByVal machine_id As Integer, ByVal venue_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@F_date", F_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Z_Report_Operatorwise_main", oColSqlPar, "WS_Get_Z_Report_Operatorwise_main", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function ZReport_BO_combine(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal F_date As DateTime, ByVal login_id As String,
'                                          ByVal location_id As Integer, ByVal machine_id As Integer, ByVal venue_id As Integer, ByVal machine_list As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@F_date", F_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_list", machine_list)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Z_Report_BO_Combine_main", oColSqlPar, "WS_Get_Z_Report_BO_Combine_main", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Customer_Profile(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Customer_Profile", oColSqlPar, "WS_Get_Customer_Profile", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Customer_Profile_Image(ByVal Cust_ID As String, ByVal store_name As String, ByVal Image As Byte()) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                'If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                oColSqlPar.Add("@AccountID", Val(Cust_ID), SqlDbType.Int)
'                oColSqlPar.Add("@CustomerProfile", Image, SqlDbType.Image)

'                Dim datatbl As DataTable = GetdataTableSp("Update_cust_profile_image", oColSqlPar, "Update_cust_profile_image", getconstr(dt))
'                If datatbl.Rows.Count > 0 Then
'                    Return data_table(datatbl)
'                Else
'                    Return "[{""Message"":""No Data Found.""}]"
'                End If
'                'Else
'                '    Return "Invalid UserName or Password"
'                'End If
'            Else
'                'oColSqlparram.Add("@User_Name", authUsername)
'                'oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Voucher_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_Voucher_List", oColSqlPar, "WS_Get_Voucher_List", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function IssueVoucher_Add(ByVal authUsername As String, ByVal authPassword As String,
'                                 ByVal Cmp_id As Integer, ByVal store_name As String, ByVal cust_id As String, ByVal Voucher_id As String,
'                                     ByVal deposit_amount As Decimal, ByVal ref_no As String, ByVal Voucher_duration As String,
'                                  ByVal start_date As DateTime, ByVal end_date As DateTime,
'                                      ByVal ip_address As String) As String  'ByVal issue_datetime As DateTime,
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher_id", Voucher_id)
'                    oColSqlPar.Add("@cust_id", cust_id)
'                    oColSqlPar.Add("@deposit_amount", deposit_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ref_no", ref_no)
'                    oColSqlPar.Add("@Voucher_duration", Voucher_duration)
'                    'oColSqlPar.Add("@issue_datetime", issue_datetime, SqlDbType.DateTime)
'                    oColSqlPar.Add("@start_date", start_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@end_date", end_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@ip_address", ip_address)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_IssueVoucher", oColSqlPar, "WS_P_M_IssueVoucher", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Key_Map_button_detail_update(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal key_map_detail_id As Integer, ByVal key_map_id As Integer, ByVal login_id As Integer,
'                             ByVal ip_address As String, ByVal Tran_Type As String, ByVal store_name As String, ByVal is_active As Integer,
'                             ByVal Product_Group_id As Integer, ByVal Product_id As Integer, ByVal Size_id As Integer, ByVal BG_Color As String,
'                             ByVal FG_Color As String, ByVal matrix As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@key_map_detail_id", key_map_detail_id, SqlDbType.Int)
'                    oColSqlPar.Add("@key_map_id", key_map_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Product_Group_id", Product_Group_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Product_id", Product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Size_id", Size_id, SqlDbType.Int)
'                    oColSqlPar.Add("@BG_Color", BG_Color)
'                    oColSqlPar.Add("@FG_Color", FG_Color)
'                    oColSqlPar.Add("@matrix", matrix)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_KeyMapDetails", oColSqlPar, "WS_P_M_KeyMapDetails", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_Master_import(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal store_name As String,
'                                           ByVal product_id As Integer, ByVal location_ID As Integer, ByVal department_ID As Integer,
'                                          ByVal Category_Id As Integer, ByVal printer_id1 As Integer, ByVal printer_id2 As Integer, ByVal printer_id3 As Integer,
'                                          ByVal Price_level As Integer, ByVal Product_name As String, ByVal BaseSize As Decimal, ByVal BaseUnit As Integer,
'                                           ByVal Size_id1 As Integer, ByVal size1name As String, ByVal size1unit As Integer, ByVal size1Qty As Decimal, ByVal size1price As Decimal,
'                                          ByVal Price_id1 As Integer, ByVal size1Taxid_1 As Integer, ByVal size1Taxid_2 As Integer,
'                                          ByVal Size_id2 As Integer, ByVal size2name As String, ByVal size2unit As Integer, ByVal size2Qty As Decimal, ByVal size2price As Decimal,
'                                          ByVal Price_id2 As Integer, ByVal size2Taxid_1 As Integer, ByVal size2Taxid_2 As Integer,
'                                          ByVal Size_id3 As Integer, ByVal size3name As String, ByVal size3unit As Integer, ByVal size3Qty As Decimal, ByVal size3price As Decimal,
'                                          ByVal Price_id3 As Integer, ByVal size3Taxid_1 As Integer, ByVal size3Taxid_2 As Integer,
'                                           ByVal Size_id4 As Integer, ByVal size4name As String, ByVal size4unit As Integer, ByVal size4Qty As Decimal, ByVal size4price As Decimal,
'                                          ByVal Price_id4 As Integer, ByVal size4Taxid_1 As Integer, ByVal size4Taxid_2 As Integer,
'                                          ByVal ip As String, ByVal Tran_Type As String, ByVal Is_OutOfStock As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_ID", location_ID, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@department_ID", department_ID, SqlDbType.Int)
'                    oColSqlPar.Add("@Category_Id", Category_Id, SqlDbType.Int)
'                    oColSqlPar.Add("@printer_id1", printer_id1, SqlDbType.Int)
'                    oColSqlPar.Add("@printer_id2", printer_id2, SqlDbType.Int)
'                    oColSqlPar.Add("@printer_id3", printer_id3, SqlDbType.Int)
'                    oColSqlPar.Add("@Price_level", Price_level, SqlDbType.Int)
'                    oColSqlPar.Add("@Product_name", Product_name)
'                    oColSqlPar.Add("@BaseSize", BaseSize, SqlDbType.Decimal)
'                    oColSqlPar.Add("@BaseUnit", BaseUnit, SqlDbType.Int)
'                    oColSqlPar.Add("@Size_id1", Size_id1, SqlDbType.Int)
'                    oColSqlPar.Add("@size1name", size1name)
'                    oColSqlPar.Add("@size1unit", size1unit, SqlDbType.Int)
'                    oColSqlPar.Add("@size1Qty", size1Qty, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size1price", size1price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Price_id1", Price_id1, SqlDbType.Int)
'                    oColSqlPar.Add("@size1Taxid_1", size1Taxid_1, SqlDbType.Int)
'                    oColSqlPar.Add("@size1Taxid_2", size1Taxid_2, SqlDbType.Int)
'                    oColSqlPar.Add("@Size_id2", Size_id2, SqlDbType.Int)
'                    oColSqlPar.Add("@size2name", size2name)
'                    oColSqlPar.Add("@size2unit", size2unit, SqlDbType.Int)
'                    oColSqlPar.Add("@size2Qty", size2Qty, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size2price", size2price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Price_id2", Price_id2, SqlDbType.Int)
'                    oColSqlPar.Add("@size2Taxid_1", size2Taxid_1, SqlDbType.Int)
'                    oColSqlPar.Add("@size2Taxid_2", size2Taxid_2, SqlDbType.Int)
'                    oColSqlPar.Add("@Size_id3", Size_id3, SqlDbType.Int)
'                    oColSqlPar.Add("@size3name", size3name)
'                    oColSqlPar.Add("@size3unit", size3unit, SqlDbType.Int)
'                    oColSqlPar.Add("@size3Qty", size3Qty, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size3price", size3price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Price_id3", Price_id3, SqlDbType.Int)
'                    oColSqlPar.Add("@size3Taxid_1", size3Taxid_1, SqlDbType.Int)
'                    oColSqlPar.Add("@size3Taxid_2", size3Taxid_2, SqlDbType.Int)
'                    oColSqlPar.Add("@Size_id4", Size_id4, SqlDbType.Int)
'                    oColSqlPar.Add("@size4name", size4name)
'                    oColSqlPar.Add("@size4unit", size4unit, SqlDbType.Int)
'                    oColSqlPar.Add("@size4Qty", size4Qty, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size4price", size4price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Price_id4", Price_id4, SqlDbType.Int)
'                    oColSqlPar.Add("@size4Taxid_1", size4Taxid_1, SqlDbType.Int)
'                    oColSqlPar.Add("@size4Taxid_2", size4Taxid_2, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@Is_OutOfStock", Is_OutOfStock, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Product_import_Ins_Update", oColSqlPar, "WS_P_M_Product_import_Ins_Update", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Product_Master_ins_update(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal login_id As Integer, ByVal store_name As String,
'                                           ByVal product_id As Integer, ByVal name As String, ByVal price_id As Integer, ByVal price As Decimal,
'                                               ByVal size_id As Integer, ByVal size As Integer, ByVal ip_address As String, ByVal Tran_Type As String,
'                                              ByVal Unit As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@price_id", price_id, SqlDbType.Int)
'                    oColSqlPar.Add("@price", price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size_id", size_id, SqlDbType.Int)
'                    oColSqlPar.Add("@size", size, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@UnitName", Unit)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Product_Ins_Update", oColSqlPar, "WS_P_M_Product_Ins_Update", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function Unit_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_M_Unit", oColSqlPar, "WS_get_M_Unit", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function staff_rules_master_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_staff_rules_master", oColSqlPar, "WS_get_staff_rules_master", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function staff_rules_detail_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_staff_rules_detail", oColSqlPar, "WS_get_staff_rules_detail", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try


'    End Function



'    <WebMethod()>
'    Public Function Booking_seated_List(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_get_booking_seated_detail", oColSqlPar, "WS_get_booking_seated_detail", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try


'    End Function


'    <WebMethod()>
'    Public Function Booking_seated_Insert(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal BookingRef_no As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@BookingRef_no", BookingRef_no)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Insert_booking_seated", oColSqlPar, "WS_Insert_booking_seated", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try


'    End Function


'    <WebMethod()>
'    Public Function InOut_Insert(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal store_name As String, ByVal staff_id As Integer, ByVal login_id As Integer,
'                                  ByVal InOut_Datetime As DateTime, ByVal IsInOut As Integer, ByVal Type As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@staff_id", staff_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@InOut_Datetime", InOut_Datetime, SqlDbType.DateTime)
'                    oColSqlPar.Add("@IsInOut", IsInOut, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Type", Type)

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_InOut", oColSqlPar, "P_M_InOut", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try


'    End Function

'    <WebMethod()>
'    Public Function Buying_Size(ByVal authUsername As String, ByVal authPassword As String, ByVal cmp_id As Integer, ByVal Product_id As Integer, ByVal store_name As String, ByVal Location_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Product_id", Product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Location_id", Location_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Get_M_Buying_Size", oColSqlPar, "WS_Get_M_Buying_Size", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Stock_Take(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal Id As String, ByVal Producte_id As Integer, ByVal product_name As String, ByVal Qty As Double, ByVal Receipt_no As String, ByVal purchase_date As DateTime, ByVal location_id As Integer, ByVal machine_id As Integer, ByVal is_purchase As Integer, ByVal buying_size_id As Integer, ByVal buying_size As Integer, ByVal buying_size_unit As String, ByVal cost As Decimal, ByVal buying_size_unit_id As Integer, ByVal selling_size As String, ByVal selling_size_unit As String, ByVal selling_size_unit_id As Integer, ByVal base_size As String, ByVal base_size_id As Integer, ByVal base_size_unit_id As Integer, ByVal cretaed_date As DateTime, ByVal modify_date As DateTime, ByVal user_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Producte_id", Producte_id, SqlDbType.Int)
'                    oColSqlPar.Add("@product_name", product_name)
'                    oColSqlPar.Add("@Qty", Qty, SqlDbType.Float)
'                    oColSqlPar.Add("@Receipt_no", Receipt_no)
'                    oColSqlPar.Add("@purche_date", purchase_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_purchase", is_purchase, SqlDbType.Int)
'                    oColSqlPar.Add("@buying_size_id", buying_size_id, SqlDbType.Int)
'                    oColSqlPar.Add("@buying_size", buying_size)
'                    oColSqlPar.Add("@buying_size_unit", buying_size_unit)
'                    oColSqlPar.Add("@cost", cost, SqlDbType.Float)
'                    oColSqlPar.Add("@buying_size_unit_id", buying_size_unit_id, SqlDbType.Int)
'                    oColSqlPar.Add("@selling_size", selling_size)
'                    oColSqlPar.Add("@selling_size_unit", selling_size_unit)
'                    oColSqlPar.Add("@selling_size_unit_id", selling_size_unit_id, SqlDbType.Int)
'                    oColSqlPar.Add("@base_size", base_size)
'                    oColSqlPar.Add("@base_size_id", base_size_id, SqlDbType.Int)
'                    oColSqlPar.Add("@base_size_unit_id", base_size_unit_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cretaed_date", cretaed_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@user_id", user_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Stock_Take_Request", oColSqlPar, "WS_P_M_Stock_Take_Request", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Add_Product_CRM(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String,
' ByVal GrpCat As String,
' ByVal GrpName As String,
' ByVal prdName As String,
' ByVal Unit As String,
' ByVal Base As String,
' ByVal SizeN1 As String,
' ByVal SizeU1 As String,
' ByVal SizeQ1 As String,
' ByVal Price1 As String,
' ByVal SizeN2 As String,
' ByVal SizeU2 As String,
' ByVal SizeQ2 As String,
' ByVal Price2 As String,
' ByVal SizeN3 As String,
' ByVal SizeU3 As String,
' ByVal SizeQ3 As String,
' ByVal Price3 As String,
' ByVal SizeN4 As String,
' ByVal SizeU4 As String,
' ByVal SizeQ4 As String,
' ByVal Price4 As String
') As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Producte_id", 0, SqlDbType.Int)
'                    oColSqlPar.Add("@name", prdName)
'                    oColSqlPar.Add("@price", 0, SqlDbType.Decimal)
'                    oColSqlPar.Add("@GroupName", GrpName)
'                    oColSqlPar.Add("@UnitName", Unit)
'                    oColSqlPar.Add("@MainCategoryName", GrpCat)
'                    oColSqlPar.Add("@Base", Base)
'                    oColSqlPar.Add("@department_name", "")
'                    oColSqlPar.Add("@Printer_name1", "")
'                    oColSqlPar.Add("@Printer_name2", "")
'                    oColSqlPar.Add("@Printer_name3", "")

'                    oColSqlPar.Add("@size1name", SizeN1)
'                    oColSqlPar.Add("@size1price", Price1, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size2name", SizeN2)
'                    oColSqlPar.Add("@size2price", Price2, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size3name", SizeN3)
'                    oColSqlPar.Add("@size3price", Price3, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size4name", SizeN4)
'                    oColSqlPar.Add("@size4price", Price4, SqlDbType.Decimal)

'                    oColSqlPar.Add("@size1unit", SizeU1)
'                    oColSqlPar.Add("@size1", SizeQ1, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size2unit", SizeU2)
'                    oColSqlPar.Add("@size2", SizeQ2, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size3unit", SizeU3)
'                    oColSqlPar.Add("@size3", SizeQ3, SqlDbType.Decimal)
'                    oColSqlPar.Add("@size4unit", SizeU4)
'                    oColSqlPar.Add("@size4", SizeQ4, SqlDbType.Decimal)

'                    oColSqlPar.Add("@Barcode1", "")
'                    oColSqlPar.Add("@Barcode2", "")
'                    oColSqlPar.Add("@Barcode3", "")
'                    oColSqlPar.Add("@Barcode4", "")

'                    Dim datatbl As DataTable = GetdataTableSp("P_M_Product_Add_API", oColSqlPar, "P_M_Product_Add_API", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Add_User_CRM(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String,
'        ByVal Username As String,
'        ByVal password As String,
'        ByVal email As String
'        ) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@username", Username)
'                    oColSqlPar.Add("@password", Encrypt(password))
'                    oColSqlPar.Add("@email", email)


'                    Dim datatbl As DataTable = GetdataTableSp("P_M_User_CRM", oColSqlPar, "P_M_User_CRM", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Voucher_Validate(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal VoucherCode As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@VoucherCode", VoucherCode)


'                    Dim datatbl As DataTable = GetdataTableSp("P_Account_Voucher", oColSqlPar, "P_Account_Voucher", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Voucher_Used(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal VoucherCode As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@VoucherCode", VoucherCode)


'                    Dim datatbl As DataTable = GetdataTableSp("P_Account_Voucher_Used", oColSqlPar, "P_Account_Voucher_Used", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function PaybyLink_Payment(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal machine_id As Int32) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@machine_id", machine_id)
'                    Dim datatbl As DataTable = GetdataTableSp("ws_get_Paybylink_payment_done", oColSqlPar, "ws_get_Paybylink_payment_done", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function PaybyLink_Payment_update(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal payUUID As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@pay_uuid", payUUID)


'                    Dim datatbl As DataTable = GetdataTableSp("ws_paybylink_payment_done_update", oColSqlPar, "ws_paybylink_payment_done_update", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function


'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_702(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                            ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                            ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                            ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String,
'                            ByVal Elina_Room_No As String, ByVal TIP_AMOUNT As Decimal, ByVal CardPayType As Integer, ByVal KINETIC_REF_NO As String, ByVal Pay_uuid As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    oColSqlPar.Add("@TIP_AMOUNT", TIP_AMOUNT, SqlDbType.Decimal)
'                    oColSqlPar.Add("@CardPayType", CardPayType, SqlDbType.Int)
'                    oColSqlPar.Add("@KINETIC_REF_NO", KINETIC_REF_NO)
'                    oColSqlPar.Add("@Pay_uuid", KINETIC_REF_NO)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_702", oColSqlPar, "WS_P_M_Sales_ver_702", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function



'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_703(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                            ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                            ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                            ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String,
'                            ByVal Elina_Room_No As String, ByVal TIP_AMOUNT As Decimal, ByVal CardPayType As Integer, ByVal KINETIC_REF_NO As String, ByVal Pay_uuid As String, ByVal No_OfCover As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    oColSqlPar.Add("@TIP_AMOUNT", TIP_AMOUNT, SqlDbType.Decimal)
'                    oColSqlPar.Add("@CardPayType", CardPayType, SqlDbType.Int)
'                    oColSqlPar.Add("@KINETIC_REF_NO", KINETIC_REF_NO)
'                    oColSqlPar.Add("@Pay_uuid", KINETIC_REF_NO)
'                    oColSqlPar.Add("@No_OfCover", No_OfCover, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_703", oColSqlPar, "WS_P_M_Sales_ver_703", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function WS_P_M_Sales_ver_704(ByVal authUsername As String, ByVal authPassword As String, ByVal Cmp_id As Integer, ByVal sales_id As Integer, ByVal login_id As Integer, ByVal total_amount As Decimal,
'                            ByVal total_discount As Decimal, ByVal net_amount As Decimal, ByVal ip_address As String, ByVal change As Decimal, ByVal tax As Decimal, ByVal Tran_Type As String,
'                            ByVal mac_id As String, ByVal ref_id As String, ByVal venue_id As Integer, ByVal store_name As String, ByVal actual_total_amount As Decimal, ByVal temp_sale_id As Integer,
'                            ByVal is_return As Integer, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal mode As Integer, ByVal value As Decimal, ByVal machine_id As Integer,
'                            ByVal location_id As Integer, ByVal input_amount As Decimal, ByVal sale_type As Integer, ByVal is_table As Integer, ByVal Payment_Date As DateTime, ByVal Payment_Amount As Decimal, ByVal Table_name As String, ByVal is_close As Integer,
'                            ByVal Discount_mode As String, ByVal discount_name As String, ByVal Room_Payment_Number As Integer, ByVal Room_Payment_Name As String, ByVal values As String, ByVal cust_id As Integer, ByVal cust_name As String, ByVal cust_contact As String, ByVal cust_email As String, ByVal used_bonus_Point As Decimal, ByVal Used_bonus_amount As Decimal, ByVal table_created_machine_id As Integer, ByVal table_close_machine_id As Integer, ByVal card_holdername As String, ByVal expiration_date As String, ByVal pan As String, ByVal cardtype As String, ByVal amount As String, ByVal type_ISO_currency_code As String, ByVal date1 As String, ByVal status_message As String, ByVal transaction_id As String,
'                            ByVal payment_account_data_token As String,
'                            ByVal retrieval_reference_number As String, ByVal merchant_id As String, ByVal terminal_id As String, ByVal card_method_type As String, ByVal surcharge_amount As Decimal, ByVal surcharge_value As Decimal, ByVal surcharge_name As String, ByVal GrandTotal As Decimal, ByVal EatOutAMount As Decimal, ByVal NoOfCovers As Integer, ByVal VoucherId As Integer, ByVal Voucher As String, ByVal VoucherBalance As Decimal, ByVal transaction_count As Integer, ByVal shift_name As String, ByVal is_cash_table As Int32, ByVal Table_id As Int64, ByVal Tran_UUID As String, ByVal Table_UUID As String,
'                            ByVal IS_Delivery As String, ByVal Cust_delivery_date As DateTime, ByVal Cust_delivery_time As String,
'                            ByVal Cust_delivery_Addr As String, ByVal sales_sub_type As Integer, ByVal Is_Email As Integer,
'                            ByVal Original_table_UUID As String, ByVal Transfered_Table_UUID As String,
'                            ByVal Integrated_Terminal_ID As String, ByVal Integrated_Merchant_ID As String,
'                            ByVal Integrated_SaleType As String, ByVal Integrated_Entry_Method As String,
'                            ByVal Elina_Room_No As String, ByVal TIP_AMOUNT As Decimal, ByVal CardPayType As Integer, ByVal KINETIC_REF_NO As String, ByVal Pay_uuid As String, ByVal No_OfCover As Integer, ByVal NON_TABLE_PART_PAYMENT As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@sales_id", sales_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@total_amount", total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@total_discount", total_discount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@change", change, SqlDbType.Decimal)
'                    oColSqlPar.Add("@tax", tax, SqlDbType.Decimal)
'                    oColSqlPar.Add("@net_amount", net_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_Type", Tran_Type)
'                    oColSqlPar.Add("@mac_id", mac_id)
'                    oColSqlPar.Add("@ref_id", ref_id)
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@actual_total_amount", actual_total_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@temp_sale_id", temp_sale_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_return", is_return, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@mode", mode, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@value", value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@input_amount", input_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@sale_type", sale_type, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@is_table", is_table, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Payment_Date", Payment_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Payment_Amount", Payment_Amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Table_name", Table_name)
'                    oColSqlPar.Add("@is_close", is_close, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Discount_mode", Discount_mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@discount_name", discount_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Room_Payment_Number", Room_Payment_Number, SqlDbType.Int)
'                    oColSqlPar.Add("@Room_Payment_Name", Room_Payment_Name)
'                    oColSqlPar.Add("@values", values)
'                    oColSqlPar.Add("@cust_id", cust_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cust_name", cust_name)
'                    oColSqlPar.Add("@cust_contact", cust_contact)
'                    oColSqlPar.Add("@cust_email", cust_email)
'                    oColSqlPar.Add("@used_bonus_Point", used_bonus_Point, SqlDbType.Int)
'                    oColSqlPar.Add("@Used_bonus_amount", Used_bonus_amount, SqlDbType.Int)
'                    oColSqlPar.Add("@table_created_machine_id", table_created_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@table_close_machine_id", table_close_machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@card_holdername", card_holdername, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@expiration_date", expiration_date, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@pan", pan, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cardtype", cardtype, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@amount", amount, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@type_ISO_currency_code", type_ISO_currency_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@date", date1, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@status_message", status_message, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@transaction_id", transaction_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@payment_account_data_token", payment_account_data_token, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@retrieval_reference_number", retrieval_reference_number, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@merchant_id", merchant_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@terminal_id", terminal_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@card_method_type", card_method_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@surcharge_amount", surcharge_amount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_value", surcharge_value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@surcharge_name", surcharge_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@GrandTotal", GrandTotal, SqlDbType.Decimal)
'                    oColSqlPar.Add("@EatOutAmount", EatOutAMount, SqlDbType.Decimal)
'                    oColSqlPar.Add("@NoOfCovers", NoOfCovers, SqlDbType.Int)
'                    oColSqlPar.Add("@VoucherId", VoucherId, SqlDbType.Int)
'                    oColSqlPar.Add("@Voucher", Voucher)
'                    oColSqlPar.Add("@VoucherBalance", VoucherBalance, SqlDbType.Int)
'                    oColSqlPar.Add("@transaction_count", transaction_count, SqlDbType.Int)
'                    oColSqlPar.Add("@shift_name", shift_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_cash_table", is_cash_table, SqlDbType.Int)
'                    oColSqlPar.Add("@Table_id", Table_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_uuid", Tran_UUID)
'                    oColSqlPar.Add("@table_uuid", Table_UUID)
'                    oColSqlPar.Add("@IS_Delivery", IS_Delivery)
'                    oColSqlPar.Add("@Cust_delivery_date", Cust_delivery_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Cust_delivery_time", Cust_delivery_time)
'                    oColSqlPar.Add("@Cust_delivery_Addr", Cust_delivery_Addr)
'                    oColSqlPar.Add("@sales_sub_type", sales_sub_type, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Email", Is_Email, SqlDbType.Int)
'                    oColSqlPar.Add("@Original_table_UUID", Original_table_UUID)
'                    oColSqlPar.Add("@Transfered_Table_UUID", Transfered_Table_UUID)
'                    oColSqlPar.Add("@Integrated_Terminal_ID", Integrated_Terminal_ID)
'                    oColSqlPar.Add("@Integrated_Merchant_ID", Integrated_Merchant_ID)
'                    oColSqlPar.Add("@Integrated_SaleType", Integrated_SaleType)
'                    oColSqlPar.Add("@Integrated_Entry_Method", Integrated_Entry_Method)
'                    oColSqlPar.Add("@Elina_Room_No", Elina_Room_No)
'                    oColSqlPar.Add("@TIP_AMOUNT", TIP_AMOUNT, SqlDbType.Decimal)
'                    oColSqlPar.Add("@CardPayType", CardPayType, SqlDbType.Int)
'                    oColSqlPar.Add("@KINETIC_REF_NO", KINETIC_REF_NO)
'                    oColSqlPar.Add("@Pay_uuid", KINETIC_REF_NO)
'                    oColSqlPar.Add("@No_OfCover", No_OfCover, SqlDbType.Int)
'                    oColSqlPar.Add("@NON_TABLE_PART_PAYMENT", NON_TABLE_PART_PAYMENT)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Sales_ver_704", oColSqlPar, "WS_P_M_Sales_ver_704", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", mac_id)
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function






'    <WebMethod()>
'    Public Function GetCommunicator_Request(ByVal authUsername As String, ByVal authPassword As String, ByVal location_id As Integer,
'                              ByVal store_name As String, ByVal TillID As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@TillID", TillID, SqlDbType.Int)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_getCommunicator_Request", oColSqlPar, "WS_getCommunicator_Request", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Record Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function GetCommunicator_Request_Confirmation(ByVal authUsername As String, ByVal authPassword As String, ByVal location_id As Integer,
'                          ByVal store_name As String, ByVal pay_uuid As String, ByVal status As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@pay_uuid", pay_uuid)
'                    oColSqlPar.Add("@is_status_accessed", status)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_getCommunicator_ConfirmationRequest", oColSqlPar, "WS_getCommunicator_ConfirmationRequest", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""Error""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function



'    <WebMethod()>
'    Public Function Communicator_Response(ByVal authUsername As String, ByVal authPassword As String, ByVal location_id As Integer,
'                          ByVal store_name As String, ByVal paymentRef As String, ByVal pay_uuid As String, ByVal status As Integer, ByVal order_ref As String) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@paymentRef", paymentRef)
'                    oColSqlPar.Add("@pay_uuid", pay_uuid)
'                    oColSqlPar.Add("@status", status, SqlDbType.Int)
'                    oColSqlPar.Add("@order_ref", order_ref)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_Communicator_Response", oColSqlPar, "WS_Communicator_Response", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""Error""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    '-------------------------------------------------------------------------26/12/2023 tue
'    <WebMethod()>
'    Public Function AddDPT_Lite(ByVal authUsername As String, ByVal authPassword As String,
'                          ByVal store_name As String, ByVal department_name As String, ByVal department_id As Integer, ByVal is_product As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@department_name", department_name)
'                    oColSqlPar.Add("@department_id", department_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_product", is_product, SqlDbType.Int)


'                    Dim datatbl As DataTable = GetdataTableSp("AddDPT_Lite", oColSqlPar, "AddDPT_Lite", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""Error""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Communicator_transactionData(ByVal authUsername As String, ByVal authPassword As String,
'                         ByVal store_name As String, ByVal location_id As Integer, ByVal id As Integer, ByVal pay_uuid As String, ByVal TOTAL_AMOUNT As String, ByVal LANGUAGE As String,
'                                                 ByVal TABLE_MASTER_REF_ID As Integer, ByVal TRAN_UUID As String, ByVal TABLE_UUID As String, ByVal CREATED_USER_ID As Integer, ByVal MACHINE_ID As Integer,
'                                                 ByVal IS_SYNC As Integer, ByVal IS_REPLAY As Integer, ByVal CREATED_DATE As DateTime, ByVal MODIFIED_DATE As DateTime, ByVal PAX_POS_ID As Integer,
'                                                 ByVal IP_ADDRESS As String,
'                                                 ByVal IS_COMPLETE_TASK As Integer, ByVal STATUS As String, ByVal IS_PROCESSING As Integer,
'                                                 ByVal PAX_UUID As String, ByVal IS_WEBOR_POS As Integer, ByVal Payment_Method As String,
'                                                 ByVal transactionType As String, ByVal is_lastdata As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@location_id", location_id, SqlDbType.Int)
'                    oColSqlPar.Add("@id", id, SqlDbType.Int)
'                    oColSqlPar.Add("@pay_uuid", pay_uuid)
'                    oColSqlPar.Add("@TOTAL_AMOUNT", TOTAL_AMOUNT)
'                    oColSqlPar.Add("@LANGUAGE", LANGUAGE)
'                    oColSqlPar.Add("@TABLE_MASTER_REF_ID", TABLE_MASTER_REF_ID)
'                    oColSqlPar.Add("@TRAN_UUID", TRAN_UUID)
'                    oColSqlPar.Add("@TABLE_UUID", TABLE_UUID)
'                    oColSqlPar.Add("@CREATED_USER_ID", CREATED_USER_ID)
'                    oColSqlPar.Add("@MACHINE_ID", MACHINE_ID, SqlDbType.Int)
'                    oColSqlPar.Add("@IS_SYNC", IS_SYNC)
'                    oColSqlPar.Add("@IS_REPLAY", IS_REPLAY)
'                    oColSqlPar.Add("@CREATED_DATE", CREATED_DATE, SqlDbType.DateTime)
'                    oColSqlPar.Add("@MODIFIED_DATE", MODIFIED_DATE, SqlDbType.DateTime)
'                    oColSqlPar.Add("@PAX_POS_ID", PAX_POS_ID)
'                    oColSqlPar.Add("@IP_ADDRESS", IP_ADDRESS)
'                    oColSqlPar.Add("@IS_COMPLETE_TASK", IS_COMPLETE_TASK)
'                    oColSqlPar.Add("@STATUS", STATUS)
'                    oColSqlPar.Add("@IS_PROCESSING", IS_PROCESSING)
'                    oColSqlPar.Add("@PAX_UUID", PAX_UUID)
'                    oColSqlPar.Add("@IS_WEBOR_POS", IS_WEBOR_POS)
'                    oColSqlPar.Add("@Payment_Method", Payment_Method)
'                    oColSqlPar.Add("@transactionType", transactionType)
'                    oColSqlPar.Add("@is_lastdata", is_lastdata, SqlDbType.Int)


'                    Dim datatbl As DataTable = GetdataTableSp("insert_Communicator_transactionData", oColSqlPar, "insert_Communicator_transactionData", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""Error""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Company_Master(ByVal authUsername As String,
'                                   ByVal authPassword As String,
'                                   ByVal Cmp_id As Integer,
'                                   ByVal store_name As String,
'                                   ByVal Name As String,
'                                          ByVal Domain As String,
'                                   ByVal Description As String,
'                                   ByVal Receipt_Header As String,
'                                   ByVal Receipt_Footer As String,
'                                          ByVal Venue_Name As String,
'                                   ByVal log_off_time As Decimal,
'                                   ByVal par_sale_par_operator As Decimal,
'                                          ByVal Website As String,
'                                   ByVal Email As String,
'                                   ByVal Contact As String,
'                                   ByVal Vat_No As String,
'                                          ByVal Address As String,
'                                   ByVal Starting_Date As DateTime,
'                                   ByVal IP_Address As String,
'                                          ByVal Country As Integer,
'                                   ByVal State As Integer,
'                                   ByVal City As Integer,
'                                   ByVal Code As String,
'                                          ByVal Postal As String,
'                                   ByVal Registration_no As Integer,
'                                          ByVal GST_VAT As Integer,
'                                   ByVal CST_VAT As Integer,
'                                   ByVal Service_tax_no As Integer,
'                                   ByVal Pan_no As Integer,
'                                      ByVal Branch_Name As String,
'                                   ByVal Synchronization As String,
'                                   ByVal Currency_id As Integer,
'                                      ByVal Store_UUID As String,
'                                   ByVal Created_date As DateTime,
'                                   ByVal Modify_Date As DateTime,
'                                   ByVal IsAddTax2 As Integer,
'                                   ByVal IsExclusiveTax As Integer) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Company_id", Cmp_id)
'                    oColSqlPar.Add("@Name", Name)
'                    oColSqlPar.Add("@Address", Address)
'                    oColSqlPar.Add("@Starting_Date", Starting_Date)
'                    oColSqlPar.Add("@Domain", Domain)
'                    oColSqlPar.Add("@IP_Address", IP_Address)
'                    oColSqlPar.Add("@Country", Country)
'                    oColSqlPar.Add("@State", State)
'                    oColSqlPar.Add("@City", City)
'                    oColSqlPar.Add("@Code", Code)
'                    oColSqlPar.Add("@Email", Email)
'                    oColSqlPar.Add("@Description", Description)
'                    oColSqlPar.Add("@Postal", Postal)
'                    oColSqlPar.Add("@Website", Website)
'                    oColSqlPar.Add("@Contact", Contact)
'                    oColSqlPar.Add("@Registration_no", Registration_no)
'                    oColSqlPar.Add("@GST_VAT", GST_VAT)
'                    oColSqlPar.Add("@CST_VAT", CST_VAT)
'                    oColSqlPar.Add("@Service_tax_no", Service_tax_no)
'                    oColSqlPar.Add("@Pan_no", Pan_no)
'                    oColSqlPar.Add("@Branch_Name", Branch_Name)
'                    oColSqlPar.Add("@Synchronization", Synchronization)
'                    oColSqlPar.Add("@Venue_Name", Venue_Name)
'                    oColSqlPar.Add("@Vat_No", Vat_No)
'                    oColSqlPar.Add("@Receipt_Header", Receipt_Header)
'                    oColSqlPar.Add("@Receipt_Footer", Receipt_Footer)
'                    oColSqlPar.Add("@log_off_time", log_off_time)
'                    oColSqlPar.Add("@par_sale_par_operator", par_sale_par_operator)
'                    oColSqlPar.Add("@Currency_id", Currency_id)
'                    oColSqlPar.Add("@Store_UUID", Store_UUID)
'                    oColSqlPar.Add("@Created_date", Created_date)
'                    oColSqlPar.Add("@Modify_Date", Modify_Date)
'                    oColSqlPar.Add("@IsAddTax2", IsAddTax2)
'                    oColSqlPar.Add("@IsExclusiveTax", IsExclusiveTax)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_M_Company", oColSqlPar, "WS_P_M_Company", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Venue_List_set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal venue_id As Integer, ByVal venue_name As String,
'                             ByVal description As String, ByVal sorting_no As Integer, ByVal cmp_id As Integer, ByVal mac_id As Integer,
'                             ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal print_receipt As Integer,
'                             ByVal print_duplicate_receipt As Integer, ByVal machine_id As Integer, ByVal is_active As Integer,
'                             ByVal login_id As Integer, ByVal group_by As Integer, ByVal group_by_with As Integer,
'                             ByVal consile_date As Integer, ByVal date_time As DateTime) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then
'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@venue_id", venue_id, SqlDbType.Int)
'                    oColSqlPar.Add("@venue_name", venue_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@description", description, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@sorting_no", sorting_no, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@mac_id", mac_id, SqlDbType.Int)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@print_receipt", print_receipt, SqlDbType.Int)
'                    oColSqlPar.Add("@print_duplicate_receipt", print_duplicate_receipt, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@group_by", group_by, SqlDbType.Int)
'                    oColSqlPar.Add("@group_by_with", group_by_with, SqlDbType.Int)
'                    oColSqlPar.Add("@consile_date", consile_date, SqlDbType.Int)
'                    oColSqlPar.Add("@date_time", date_time, SqlDbType.DateTime)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Venue", oColSqlPar, "WS_Set_M_Venue", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Function_List_Set(ByVal authUsername As String,
'                                      ByVal authPassword As String,
'                                         ByVal store_name As String,
'                                        ByVal Cmp_id As Integer,
'                                        ByVal function_id As Integer,
'                                        ByVal name As String,
'                                        ByVal code As String,
'                                        ByVal caption_type As String,
'                                        ByVal is_active As Integer,
'                                        ByVal shorting_no As Integer,
'                                        ByVal ip_address As String,
'                                        ByVal login_id As Integer,
'                                        ByVal item As Integer,
'                                        ByVal back_color As String,
'                                        ByVal for_color As String,
'                                        ByVal machine_id As Integer,
'                                        ByVal big_button As Integer,
'                                        ByVal payment_id As Integer,
'                                        ByVal is_groupby_course As Integer,
'                                        ByVal is_groupby_dept As Integer,
'                                        ByVal dept_id As Integer,
'                                        ByVal course_id As Integer,
'                                        ByVal Parent_Id As Integer,
'                                        ByVal platformAdd As String,
'                                        ByVal clientToken As String,
'                                        ByVal accessToken As String,
'                                        ByVal EOHelpOut_max_amount_each As String,
'                                        ByVal serviceid As Integer,
'                                        ByVal Total_Value As Integer,
'                                        ByVal Tax_Amount As Integer,
'                                        ByVal Sales_Handling_Fee As Integer,
'                                        ByVal Payment_Handling_Fee As Integer,
'                                        ByVal Def_Profile_Id As Integer,
'                                        ByVal Default_Exp_date As String,
'                                        ByVal ZR_VenueID As Integer,
'                                        ByVal ZR_LocationID As Integer,
'                                        ByVal ZR_TillID As Integer,
'                                        ByVal CardPayType As Integer,
'                                      ByVal created_date As DateTime, ByVal modify_date As DateTime) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@function_id", function_id)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@caption_type", caption_type)
'                    oColSqlPar.Add("@is_active", is_active)
'                    oColSqlPar.Add("@shorting_no", shorting_no)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@cmp_id", Cmp_id)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@item", item)
'                    oColSqlPar.Add("@back_color", back_color)
'                    oColSqlPar.Add("@for_color", for_color)
'                    oColSqlPar.Add("@machine_id", machine_id)
'                    oColSqlPar.Add("@big_button", big_button)
'                    oColSqlPar.Add("@payment_id", payment_id)
'                    oColSqlPar.Add("@is_groupby_course", is_groupby_course)
'                    oColSqlPar.Add("@is_groupby_dept", is_groupby_dept)
'                    oColSqlPar.Add("@dept_id", dept_id)
'                    oColSqlPar.Add("@course_id", course_id)
'                    oColSqlPar.Add("@Parent_Id", Parent_Id)
'                    oColSqlPar.Add("@platformAdd", platformAdd)
'                    oColSqlPar.Add("@clientToken", clientToken)
'                    oColSqlPar.Add("@accessToken", accessToken)
'                    oColSqlPar.Add("@EOHelpOut_max_amount_each", EOHelpOut_max_amount_each)
'                    oColSqlPar.Add("@serviceid", serviceid)
'                    oColSqlPar.Add("@Total_Value", Total_Value)
'                    oColSqlPar.Add("@Tax_Amount", Tax_Amount)
'                    oColSqlPar.Add("@Sales_Handling_Fee", Sales_Handling_Fee)
'                    oColSqlPar.Add("@Payment_Handling_Fee", Payment_Handling_Fee)
'                    oColSqlPar.Add("@Def_Profile_Id", Def_Profile_Id)
'                    oColSqlPar.Add("@Default_Exp_date", Default_Exp_date)
'                    oColSqlPar.Add("@ZR_VenueID", ZR_VenueID)
'                    oColSqlPar.Add("@ZR_LocationID", ZR_LocationID)
'                    oColSqlPar.Add("@ZR_TillID", ZR_TillID)
'                    oColSqlPar.Add("@CardPayType", CardPayType)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Function", oColSqlPar, "WS_Set_M_Function", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function



'    <WebMethod()>
'    Public Function Machine_List_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal Cmp_id As Integer, ByVal name As String,
'                                 ByVal machine_id As Integer, ByVal serial_no As String, ByVal mac_address As String, ByVal model As String,
'                                 ByVal code As String, ByVal created_date As DateTime, ByVal modify_date As DateTime, ByVal ip_address As String,
'                                 ByVal login_id As Integer, ByVal is_assign As Integer, ByVal location_id As Integer, ByVal is_minipos As Integer,
'                                 ByVal Active As Integer, ByVal Receipt_Header As String, ByVal Receipt_Footer As String, ByVal is_master As Integer,
'                                 ByVal tillip_address As String, ByVal till_server As String, ByVal table_sharing As Integer,
'                                 ByVal printer_sharing As Integer, ByVal sync_time As String, ByVal key_back_to_main As Integer,
'                                 ByVal ExtraSurcharge As Decimal, ByVal Only_table_trans As Integer, ByVal AutoSurcharge As Integer,
'                                 ByVal AutoSurchargeTables As Integer, ByVal AutoSurchargeNonTables As Integer, ByVal AutoSurchargeCloakroom As Integer,
'                                 ByVal AutoSurchargeChips As Integer, ByVal Sync_Request As Integer, ByVal Service_Controller_Start As Integer,
'                                 ByVal Service_Websale_print As Integer, ByVal Service_Print_Share As Integer, ByVal Service_print_Share_Other_Till As Integer,
'                                 ByVal Is_NoLogout As Integer, ByVal Is_PrintServer As Integer, ByVal Is_ServiceBooking As Integer,
'                                 ByVal Is_OnlineZreport As Integer, ByVal NoCashDrawer As Integer, ByVal IsKiosk As Integer,
'                                 ByVal TblTranLimit As Integer, ByVal QuickTran As Integer, ByVal kitchenPrint As Integer,
'                                 ByVal ReceiptPrint As Integer, ByVal ElinaTran As Integer, ByVal is_POSLite As Integer) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Cmp_id", Cmp_id)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@machine_id", machine_id)
'                    oColSqlPar.Add("@serial_no", serial_no)
'                    oColSqlPar.Add("@mac_address", mac_address)
'                    oColSqlPar.Add("@model", model)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@created_date", created_date)
'                    oColSqlPar.Add("@modify_date", modify_date)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@is_assign", is_assign)
'                    oColSqlPar.Add("@location_id", location_id)
'                    oColSqlPar.Add("@is_minipos", is_minipos)
'                    oColSqlPar.Add("@Active", Active)
'                    oColSqlPar.Add("@Receipt_Header", Receipt_Header)
'                    oColSqlPar.Add("@Receipt_Footer", Receipt_Footer)
'                    oColSqlPar.Add("@is_master", is_master)
'                    oColSqlPar.Add("@tillip_address", tillip_address)
'                    oColSqlPar.Add("@till_server", till_server)
'                    oColSqlPar.Add("@table_sharing", table_sharing)
'                    oColSqlPar.Add("@printer_sharing", printer_sharing)
'                    oColSqlPar.Add("@sync_time", sync_time)
'                    oColSqlPar.Add("@key_back_to_main", key_back_to_main)
'                    oColSqlPar.Add("@ExtraSurcharge", ExtraSurcharge, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Only_table_trans", Only_table_trans)
'                    oColSqlPar.Add("@AutoSurcharge", AutoSurcharge)
'                    oColSqlPar.Add("@AutoSurchargeTables", AutoSurchargeTables)
'                    oColSqlPar.Add("@AutoSurchargeNonTables", AutoSurchargeNonTables)
'                    oColSqlPar.Add("@AutoSurchargeCloakroom", AutoSurchargeCloakroom)
'                    oColSqlPar.Add("@AutoSurchargeChips", AutoSurchargeChips)
'                    oColSqlPar.Add("@Sync_Request", Sync_Request)
'                    oColSqlPar.Add("@Service_Controller_Start", Service_Controller_Start)
'                    oColSqlPar.Add("@Service_Websale_print", Service_Websale_print)
'                    oColSqlPar.Add("@Service_Print_Share", Service_Print_Share)
'                    oColSqlPar.Add("@Service_print_Share_Other_Till", Service_print_Share_Other_Till)
'                    oColSqlPar.Add("@Is_NoLogout", Is_NoLogout)
'                    oColSqlPar.Add("@Is_PrintServer", Is_PrintServer)
'                    oColSqlPar.Add("@Is_ServiceBooking", Is_ServiceBooking)
'                    oColSqlPar.Add("@Is_OnlineZreport", Is_OnlineZreport)
'                    oColSqlPar.Add("@NoCashDrawer", NoCashDrawer)
'                    oColSqlPar.Add("@IsKiosk", IsKiosk)
'                    oColSqlPar.Add("@TblTranLimit", TblTranLimit)
'                    oColSqlPar.Add("@QuickTran", QuickTran)
'                    oColSqlPar.Add("@kitchenPrint", kitchenPrint)
'                    oColSqlPar.Add("@ReceiptPrint", ReceiptPrint)
'                    oColSqlPar.Add("@ElinaTran", ElinaTran)
'                    oColSqlPar.Add("@is_POSLite", is_POSLite)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Machine", oColSqlPar, "WS_Set_M_Machine", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Product_List_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal Cmp_id As Integer, ByVal product_id As Integer,
'                                    ByVal category_id As Integer, ByVal code As String, ByVal name As String, ByVal price As Decimal,
'                                    ByVal barcode As String, ByVal description As String, ByVal created_date As DateTime, ByVal modify_date As DateTime,
'                                    ByVal is_active As Integer, ByVal ip_address As String, ByVal login_id As Integer, ByVal department_id As Integer,
'                                    ByVal course_id As Integer, ByVal list As Integer, ByVal printer_id As Integer,
'                                    ByVal key_map_id As Integer,
'                                    ByVal Actual_Price As Decimal,
'                                    ByVal Tax As Decimal,
'                                    ByVal Tax_id As Integer,
'                                    ByVal other_id As Integer,
'                                    ByVal other_size As Integer,
'                                    ByVal Base As Integer,
'                                    ByVal Unit_id As Integer,
'                                    ByVal size_zero As Integer,
'                                    ByVal Is_Condiment As Integer,
'                                    ByVal Cloak_Room As Integer,
'                                    ByVal Is_DanceVoucher As Integer,
'                                    ByVal By_Weight As Integer,
'                                    ByVal IsHouse As Integer,
'                                    ByVal SortingNo As Integer,
'                                    ByVal IsPkgProduct As Integer,
'                                    ByVal ForKiosk As Integer,
'                                    ByVal Is_OutOfStock As Integer) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Cmp_id", Cmp_id)
'                    oColSqlPar.Add("@product_id", product_id)
'                    oColSqlPar.Add("@category_id", category_id)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@price", price)
'                    oColSqlPar.Add("@barcode", barcode)
'                    oColSqlPar.Add("@description", description)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@is_active", is_active)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@department_id", department_id)
'                    oColSqlPar.Add("@course_id", course_id)
'                    oColSqlPar.Add("@list", list)
'                    oColSqlPar.Add("@printer_id", printer_id)
'                    oColSqlPar.Add("@key_map_id", key_map_id)
'                    oColSqlPar.Add("@Actual_Price", Actual_Price)
'                    oColSqlPar.Add("@Tax", Tax)
'                    oColSqlPar.Add("@Tax_id", Tax_id)
'                    oColSqlPar.Add("@other_id", other_id)
'                    oColSqlPar.Add("@other_size", other_size)
'                    oColSqlPar.Add("@Base", Base)
'                    oColSqlPar.Add("@Unit_id", Unit_id)
'                    oColSqlPar.Add("@size_zero", size_zero)
'                    oColSqlPar.Add("@Is_Condiment", Is_Condiment)
'                    oColSqlPar.Add("@Cloak_Room", Cloak_Room)
'                    oColSqlPar.Add("@Is_DanceVoucher", Is_DanceVoucher)
'                    oColSqlPar.Add("@By_Weight", By_Weight)
'                    oColSqlPar.Add("@IsHouse", IsHouse)
'                    oColSqlPar.Add("@SortingNo", SortingNo)
'                    oColSqlPar.Add("@IsPkgProduct", IsPkgProduct)
'                    oColSqlPar.Add("@ForKiosk", ForKiosk)
'                    oColSqlPar.Add("@Is_OutOfStock", Is_OutOfStock)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Set_Product", oColSqlPar, "WS_P_Set_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function



'    <WebMethod()>
'    Public Function ProductGroup_List_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal Cmp_id As Integer, ByVal group_id As Integer,
'                                            ByVal name As String,
'                                            ByVal description As String,
'                                            ByVal created_date As DateTime,
'                                            ByVal modify_date As DateTime,
'                                            ByVal is_active As Integer,
'                                            ByVal ip_address As String,
'                                            ByVal login_id As Integer,
'                                            ByVal key_map_id As Integer,
'                                            ByVal sorting_no As Integer,
'                                            ByVal maincategory_id As Integer) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Cmp_id", Cmp_id)
'                    oColSqlPar.Add("@group_id", group_id)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@description", description)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@is_active", is_active)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@key_map_id", key_map_id)
'                    oColSqlPar.Add("@sorting_no", sorting_no)
'                    oColSqlPar.Add("@maincategory_id", maincategory_id)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Category", oColSqlPar, "WS_Set_M_Category", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function

'    <WebMethod()>
'    Public Function Price_By_Size_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal Cmp_id As Integer,
'                                            ByVal Product_id As Integer,
'                                            ByVal Price As Decimal,
'                                            ByVal Price_Id As Integer,
'                                            ByVal Actual_Price As Decimal,
'                                            ByVal Tax As Decimal,
'                                            ByVal Location_Id As Integer,
'                                            ByVal Size_Id As Integer,
'                                            ByVal tax_id As Integer,
'                                            ByVal Product_Price_Id As Integer,
'                                            ByVal Tax2 As Decimal,
'                                            ByVal Tax_id2 As Integer) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Cmp_id", Cmp_id)
'                    oColSqlPar.Add("@Product_id", Product_id)
'                    oColSqlPar.Add("@Price", Price)
'                    oColSqlPar.Add("@Price_Id", Price_Id)
'                    oColSqlPar.Add("@Actual_Price", Actual_Price)
'                    oColSqlPar.Add("@Tax", Tax)
'                    oColSqlPar.Add("@Location_Id", Location_Id)
'                    oColSqlPar.Add("@Size_Id", Size_Id)
'                    oColSqlPar.Add("@tax_id", tax_id)
'                    oColSqlPar.Add("@Product_Price_Id", Product_Price_Id)
'                    oColSqlPar.Add("@Tax2", Tax2)
'                    oColSqlPar.Add("@Tax_id2", Tax_id2)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Price_Size", oColSqlPar, "WS_Set_M_Price_Size", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function

'    <WebMethod()>
'    Public Function Size_List_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal Name As String,
'                                    ByVal product_id As Integer,
'                                    ByVal Size_Id As Integer,
'                                    ByVal Size As Decimal,
'                                    ByVal Unit As Integer,
'                                    ByVal active As Integer,
'                                    ByVal sorting_no As Integer) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Name", Name)
'                    oColSqlPar.Add("@product_id", product_id)
'                    oColSqlPar.Add("@Size_Id", Size_Id)
'                    oColSqlPar.Add("@Size", Size)
'                    oColSqlPar.Add("@Unit", Unit)
'                    oColSqlPar.Add("@active", active)
'                    oColSqlPar.Add("@sorting_no", sorting_no)



'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Size_Product", oColSqlPar, "WS_Set_M_Size_Product", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function
'    <WebMethod()>
'    Public Function Key_Map_List_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal Cmp_id As Integer,
'                                              ByVal key_map_id As Integer,
'                                            ByVal shorting_no As Integer,
'                                            ByVal description As String,
'                                            ByVal is_active As Integer,
'                                            ByVal Font_Color As String,
'                                            ByVal BG_Color As String,
'                                            ByVal Display_Name As String,
'                                            ByVal ButtonStyle As String,
'                                            ByVal ImageStyle As String,
'                                            ByVal ImageOption As String,
'                                            ByVal login_id As Integer,
'                                            ByVal machine_id As Integer,
'                                            ByVal venue_id As Integer,
'                                            ByVal Location_id As Integer,
'                                            ByVal Keymap_size As String,
'                                            ByVal created_date As DateTime,
'                                            ByVal modify_date As DateTime) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Cmp_id", Cmp_id)
'                    oColSqlPar.Add("@key_map_id", key_map_id)
'                    oColSqlPar.Add("@shorting_no", shorting_no)
'                    oColSqlPar.Add("@description", description)
'                    oColSqlPar.Add("@is_active", is_active)
'                    oColSqlPar.Add("@Font_Color", Font_Color)
'                    oColSqlPar.Add("@BG_Color", BG_Color)
'                    oColSqlPar.Add("@Display_Name", Display_Name)
'                    oColSqlPar.Add("@ButtonStyle", ButtonStyle)
'                    oColSqlPar.Add("@ImageStyle", ImageStyle)
'                    oColSqlPar.Add("@ImageOption", ImageOption)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@machine_id", machine_id)
'                    oColSqlPar.Add("@venue_id", venue_id)
'                    oColSqlPar.Add("@Location_id", Location_id)
'                    oColSqlPar.Add("@Keymap_size", Keymap_size)
'                    oColSqlPar.Add("@created_date", created_date)
'                    oColSqlPar.Add("@modify_date", modify_date)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_Key_Map", oColSqlPar, "WS_Set_Key_Map", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function
'    <WebMethod()>
'    Public Function Printer_List_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal Cmp_id As Integer,
'                                          ByVal printer_id As Integer,
'                                            ByVal name As String,
'                                                ByVal PAlias As String,
'                                                ByVal is_active As Integer,
'                                                ByVal ip_address As String,
'                                                ByVal login_id As Integer,
'                                                ByVal machine_id As Integer,
'                                                ByVal printer_ip_address As String,
'                                                ByVal is_Default As Integer,
'                                                ByVal network_type As String,
'                                                ByVal budrate As String,
'                                                ByVal vender_id As Integer,
'                                                ByVal device_name As String,
'                                                ByVal is_product_small_large As Integer,
'                                                ByVal group_by As Integer,
'                                                ByVal group_by_with As Integer,
'                                                ByVal consile_date As Integer,
'                                                ByVal port As Integer,
'                                                ByVal OrderFlag As Integer,
'                                                ByVal created_date As DateTime,
'                                                ByVal modify_date As DateTime) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Cmp_id", Cmp_id)
'                    oColSqlPar.Add("@printer_id", printer_id)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@Alias", PAlias.ToString())
'                    oColSqlPar.Add("@is_active", is_active)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@machine_id", machine_id)
'                    oColSqlPar.Add("@printer_ip_address", printer_ip_address)
'                    oColSqlPar.Add("@is_Default", is_Default)
'                    oColSqlPar.Add("@network_type", network_type)
'                    oColSqlPar.Add("@budrate", budrate)
'                    oColSqlPar.Add("@vender_id", vender_id)
'                    oColSqlPar.Add("@device_name", device_name)
'                    oColSqlPar.Add("@is_product_small_large", is_product_small_large)
'                    oColSqlPar.Add("@group_by", group_by)
'                    oColSqlPar.Add("@group_by_with", group_by_with)
'                    oColSqlPar.Add("@consile_date", consile_date)
'                    oColSqlPar.Add("@port", port)
'                    oColSqlPar.Add("@OrderFlag", OrderFlag)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Printer", oColSqlPar, "WS_Set_M_Printer", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function
'    <WebMethod()>
'    Public Function GroupCategory_List_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String, ByVal maincategory_id As Integer,
'                                            ByVal name As String,
'                                            ByVal sorting_no As Integer) As String

'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@maincategory_id", maincategory_id)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@sorting_no", sorting_no)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_all_Category", oColSqlPar, "WS_Set_all_Category", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return "success."
'                    Else
'                        Return "[{""Message"":""No Data Found OR may be data available.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try

'    End Function
'    <WebMethod()>
'    Public Function Location_List_Set(ByVal authUsername As String,
'                                            ByVal authPassword As String,
'                                            ByVal store_name As String,
'                                            ByVal location_id As Integer,
'                                            ByVal Country_id As Integer,
'                                            ByVal City_ID As Integer,
'                                            ByVal State_Id As Integer,
'                                            ByVal name As String,
'                                            ByVal address As String,
'                                            ByVal code As String,
'                                            ByVal City As String,
'                                            ByVal State As String,
'                                            ByVal Country As String,
'                                            ByVal ip_address As String,
'                                            ByVal cmp_id As Integer,
'                                            ByVal created_date As DateTime,
'                                            ByVal modify_date As DateTime,
'                                            ByVal login_id As Integer,
'                                            ByVal venue_name As String,
'                                            ByVal venue_id As Integer,
'                                            ByVal till_auto_log_off As Integer,
'                                            ByVal Active As Integer,
'                                            ByVal is_email As Integer,
'                                            ByVal betweenSlot As Integer,
'                                            ByVal Is_Email_APK As Integer,
'                                            ByVal Table_As_Box As String) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@location_id", location_id)
'                    oColSqlPar.Add("@Country_id", Country_id)
'                    oColSqlPar.Add("@City_ID", City_ID)
'                    oColSqlPar.Add("@State_Id", State_Id)
'                    oColSqlPar.Add("@name", name)
'                    oColSqlPar.Add("@address", address)
'                    oColSqlPar.Add("@code", code)
'                    oColSqlPar.Add("@City", City)
'                    oColSqlPar.Add("@State", State)
'                    oColSqlPar.Add("@Country", Country)
'                    oColSqlPar.Add("@ip_address", ip_address)
'                    oColSqlPar.Add("@cmp_id", cmp_id)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@login_id", login_id)
'                    oColSqlPar.Add("@venue_name", venue_name)
'                    oColSqlPar.Add("@venue_id", venue_id)
'                    oColSqlPar.Add("@till_auto_log_off", till_auto_log_off)
'                    oColSqlPar.Add("@Active", Active)
'                    oColSqlPar.Add("@is_email", is_email)
'                    oColSqlPar.Add("@betweenSlot", betweenSlot)
'                    oColSqlPar.Add("@Is_Email_APK", Is_Email_APK)
'                    oColSqlPar.Add("@Table_As_Box", Table_As_Box)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Location", oColSqlPar, "WS_Set_M_Location", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function User_List_Set(ByVal authUsername As String,
'                                   ByVal authPassword As String,
'                                   ByVal store_name As String,
'                                   ByVal Cmp_id As Integer,
'                                   ByVal staff_id As Integer,
'                                   ByVal staff_code As String,
'                                   ByVal name As String,
'                                   ByVal joining_date As DateTime,
'                                   ByVal address As String,
'                                   ByVal country_id As Integer,
'                                   ByVal state_id As Integer,
'                                   ByVal city_id As Integer,
'                                   ByVal national_id As String,
'                                   ByVal contact_no As String,
'                                   ByVal email As String,
'                                   ByVal last_working_date As DateTime,
'                                   ByVal role_id As Integer,
'                                   ByVal created_date As DateTime,
'                                   ByVal modify_date As DateTime,
'                                   ByVal is_active As Byte,
'                                   ByVal User_Login As Integer,
'                                   ByVal ip_address As String,
'                                   ByVal till_code As String,
'                                   ByVal till_active As Byte,
'                                   ByVal other_id As String,
'                                   ByVal Username As String,
'                                   ByVal Password As String,
'                                   ByVal PAlias As String,
'                                   ByVal machine_id As Integer,
'                                   ByVal Authentication_Code As String,
'                                   ByVal is_trainee As Byte,
'                                   ByVal m_staff_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@staff_id", staff_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@staff_code", staff_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@name", name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@joining_date", joining_date, SqlDbType.Date)
'                    oColSqlPar.Add("@address", address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@country_id", country_id, SqlDbType.Int)
'                    oColSqlPar.Add("@state_id", state_id, SqlDbType.Int)
'                    oColSqlPar.Add("@city_id", city_id, SqlDbType.Int)
'                    oColSqlPar.Add("@national_id", national_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@contact_no", contact_no, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@email", email, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@last_working_date", last_working_date, SqlDbType.Date)
'                    oColSqlPar.Add("@role_id", role_id, SqlDbType.Int)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@User_Login", User_Login, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@till_code", till_code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@till_active", till_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@other_id", other_id, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Username", Username, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Password", Password, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Alias", PAlias, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Authentication_Code", Authentication_Code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_trainee", is_trainee, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@m_staff_id", m_staff_id, SqlDbType.Int)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Staff", oColSqlPar, "WS_Set_M_Staff", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Prices_List_set(ByVal authUsername As String,
'                                 ByVal authPassword As String,
'                                 ByVal store_name As String,
'                                 ByVal Cmp_id As Integer,
'                                 ByVal Product_Price_Id As Integer,
'                                 ByVal Name As String,
'                                 ByVal Is_Active As Integer,
'                                 ByVal Is_Default As Integer,
'                                 ByVal Login_id As Integer
'                                 ) As String

'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
'                    oColSqlPar.Add("@Name", Name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Is_Active", Is_Active, SqlDbType.Int)
'                    oColSqlPar.Add("@Is_Default", Is_Default, SqlDbType.Int)
'                    oColSqlPar.Add("@Login_id", Login_id, SqlDbType.Int)


'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Product_Price", oColSqlPar, "WS_Set_M_Product_Price", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Condiment_List_Set(ByVal authUsername As String,
'                                    ByVal authPassword As String,
'                                    ByVal cmp_id As Integer,
'                                    ByVal store_name As String,
'                                    ByVal Condiment_Id As Integer,
'                                    ByVal Condiment As String,
'                                    ByVal product_id As Integer,
'                                    ByVal is_active As Byte,
'                                    ByVal ip_address As String,
'                                    ByVal is_add_substract As Integer,
'                                    ByVal choices As Integer,
'                                    ByVal IsBySize As Integer,
'                                    ByVal sizeID As Integer,
'                                    ByVal UseProductCondi As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
'                    oColSqlPar.Add("@Condiment", Condiment, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@product_id", product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@ip_address", ip_address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_add_substract", is_add_substract, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@choices", choices, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@IsBySize", IsBySize, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@sizeID", sizeID, SqlDbType.Int)
'                    oColSqlPar.Add("@UseProductCondi", UseProductCondi, SqlDbType.TinyInt)

'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Condiment_List", oColSqlPar, "WS_Set_M_Condiment_List", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Department_List_Set(ByVal authUsername As String,
'                                     ByVal authPassword As String,
'                                     ByVal store_name As String,
'                                     ByVal department_id As Integer,
'                                     ByVal name As String,
'                                     ByVal department_category_id As Integer,
'                                     ByVal department_category_name As String
'                                     ) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@department_id", department_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@department_category_name", department_category_name)
'                    oColSqlPar.Add("@department_category_id", department_category_id, SqlDbType.Int)



'                    Dim datatbl As DataTable = GetdataTableSp("WS_P_Set_Department", oColSqlPar, "WS_P_Set_Department", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function KeyMap_Details_Set(ByVal authUsername As String,
'                                    ByVal authPassword As String,
'                                    ByVal store_name As String,
'                                    ByVal key_map_detail_id As Integer,
'                                    ByVal key_map_id As Integer,
'                                    ByVal Product_Group_id As Integer,
'                                    ByVal Product_id As Integer,
'                                    ByVal Size_id As Integer,
'                                    ByVal cmp_id As Integer,
'                                    ByVal login_id As Integer,
'                                    ByVal ip_address As String,
'                                    ByVal created_date As DateTime,
'                                    ByVal modify_date As DateTime,
'                                    ByVal is_active As Integer,
'                                    ByVal BG_Color As String,
'                                    ByVal FG_Color As String,
'                                    ByVal matrix As String,
'                                    ByVal maincategory_id As Integer
'                                     ) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@key_map_detail_id", key_map_detail_id, SqlDbType.Int)
'                    oColSqlPar.Add("@key_map_id", key_map_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Product_Group_id", Product_Group_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Product_id", Product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Size_id", Size_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@BG_Color", BG_Color, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@FG_Color", FG_Color, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@matrix", matrix, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@maincategory_id", maincategory_id, SqlDbType.Int)



'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Key_Map_Details", oColSqlPar, "WS_Set_M_Key_Map_Details", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Product_Condiment_List_Set(ByVal authUsername As String, ByVal authPassword As String, ByVal store_name As String,
'                                    ByVal Condiment_Id As Integer, ByVal Condiment As String, ByVal is_add_substract As Integer,
'                                    ByVal Product_id As Integer, ByVal Tran_id As Integer, ByVal Price As Decimal, ByVal choices As Integer,
'                                    ByVal min_select As Integer, ByVal max_select As Integer, ByVal UseProductCondi As Integer
'                                     ) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
'                    oColSqlPar.Add("@Condiment", Condiment, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@is_add_substract", is_add_substract, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@Product_id", Product_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Tran_id", Tran_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Price", Price, SqlDbType.Decimal)
'                    oColSqlPar.Add("@choices", choices, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@min_select", min_select, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@max_select", max_select, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@UseProductCondi", UseProductCondi, SqlDbType.TinyInt)



'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Product_Condiment_List", oColSqlPar, "WS_Set_M_Product_Condiment_List", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Device_List_Set(ByVal authUsername As String,
'                                ByVal authPassword As String,
'                                ByVal store_name As String,
'                                ByVal device_id As Integer,
'                                ByVal machine_id As Integer,
'                                ByVal cmp_id As Integer,
'                                ByVal name As String,
'                                ByVal serial_no As String,
'                                ByVal code As String,
'                                ByVal created_date As DateTime,
'                                ByVal modify_date As DateTime,
'                                ByVal ip_address As String,
'                                ByVal login_id As Integer,
'                                ByVal device_type_id As Integer,
'                                ByVal is_active As Integer,
'                                ByVal printer_ip_address As String,
'                                ByVal port As String,
'                                ByVal network_type As String,
'                                ByVal vender_id As Integer,
'                                ByVal budrate As Integer,
'                                ByVal device_name As String,
'                                ByVal width As Integer,
'                                ByVal user_name As String,
'                                ByVal password As String,
'                                ByVal application_profile_id As Integer,
'                                ByVal service_key As String,
'                                ByVal bluetooth_name As String,
'                                ByVal device_subtype As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@device_id", device_id, SqlDbType.Int)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@name", name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@serial_no", serial_no, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@code", code, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@ip_address", ip_address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@device_type_id", device_type_id, SqlDbType.Int)
'                    oColSqlPar.Add("@is_active", is_active, SqlDbType.TinyInt)
'                    oColSqlPar.Add("@printer_ip_address", printer_ip_address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@port", port, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@network_type", network_type, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@vender_id", vender_id, SqlDbType.Int)
'                    oColSqlPar.Add("@budrate", budrate, SqlDbType.Int)
'                    oColSqlPar.Add("@device_name", device_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@width", width, SqlDbType.Int)
'                    oColSqlPar.Add("@user_name", user_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@password", password, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@application_profile_id", application_profile_id, SqlDbType.Int)
'                    oColSqlPar.Add("@service_key", service_key, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@bluetooth_name", bluetooth_name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@device_subtype", device_subtype)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Device", oColSqlPar, "WS_Set_M_Device", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Tax_List_Set(ByVal authUsername As String,
'                              ByVal authPassword As String,
'                              ByVal Cmp_id As Integer,
'                              ByVal store_name As String,
'                              ByVal Tax_id As Integer,
'                              ByVal Name As String,
'                              ByVal Mode As String,
'                              ByVal Value As Decimal,
'                              ByVal Is_active As Integer,
'                              ByVal Effective_Date As DateTime,
'                              ByVal created_date As DateTime,
'                              ByVal Modified_date As DateTime,
'                              ByVal Login_id As Integer,
'                              ByVal Ip_address As String,
'                              ByVal machine_id As Integer) As String
'        Try
'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Tax_id", Tax_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Name", Name, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Mode", Mode, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@Value", Value, SqlDbType.Decimal)
'                    oColSqlPar.Add("@Is_active", Is_active, SqlDbType.Int)
'                    oColSqlPar.Add("@Effective_Date", Effective_Date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Modified_date", Modified_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@Login_id", Login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Ip_address", Ip_address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@machine_id", machine_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Tax", oColSqlPar, "WS_Set_M_Tax", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'    <WebMethod()>
'    Public Function Printer_Detail_By_Machine_Set(ByVal authUsername As String,
'                                               ByVal authPassword As String,
'                                               ByVal Cmp_id As Integer,
'                                               ByVal machine_id As Integer,
'                                               ByVal store_name As String,
'                                               ByVal Printer_id As Integer,
'                                               ByVal Device_id As Integer,
'                                               ByVal login_id As Integer,
'                                               ByVal ip_address As String,
'                                               ByVal created_date As DateTime,
'                                               ByVal modify_date As DateTime) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()

'                    oColSqlPar.Add("@Machine_id", machine_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Printer_id", Printer_id, SqlDbType.Int)
'                    oColSqlPar.Add("@Device_id", Device_id, SqlDbType.Int)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    oColSqlPar.Add("@ip_address", ip_address, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@created_date", created_date, SqlDbType.DateTime)
'                    oColSqlPar.Add("@modify_date", modify_date, SqlDbType.DateTime)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Printer_Machine", oColSqlPar, "WS_Set_M_Printer_Machine", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    <WebMethod()>
'    Public Function Unit_List_Set(ByVal authUsername As String,
'                               ByVal authPassword As String,
'                               ByVal Cmp_id As Integer,
'                               ByVal store_name As String,
'                               ByVal Unit_Id As Integer,
'                               ByVal Unit As String,
'                               ByVal login_id As Integer) As String
'        Try

'            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
'            Dim dt As DataTable = getStore(store_name)

'            If dt.Rows.Count > 0 Then

'                If authUsername = dt.Rows(0)("user_name").ToString() And authPassword = dt.Rows(0)("password").ToString() Then
'                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
'                    oColSqlPar.Add("@Unit_Id", Unit_Id, SqlDbType.Int)
'                    oColSqlPar.Add("@Unit", Unit, SqlDbType.NVarChar)
'                    oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int)
'                    oColSqlPar.Add("@login_id", login_id, SqlDbType.Int)
'                    Dim datatbl As DataTable = GetdataTableSp("WS_Set_M_Unit", oColSqlPar, "WS_Set_M_Unit", getconstr(dt))
'                    If datatbl.Rows.Count > 0 Then
'                        Return data_table(datatbl)
'                    Else
'                        Return "[{""Message"":""No Data Found.""}]"
'                    End If
'                Else
'                    Return "Invalid UserName or Password"
'                End If
'            Else
'                oColSqlparram.Add("@User_Name", authUsername)
'                oColSqlparram.Add("@Password", authPassword)
'                oColSqlparram.Add("@IP_Address", "")
'                oColSqlparram.Add("@Access_Status", 0)
'                oColSqlparram.Add("@mac_id", "Web")
'                oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime)
'                ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt))
'                Return "Invalid Webservice User Access...!"
'            End If
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'End Class
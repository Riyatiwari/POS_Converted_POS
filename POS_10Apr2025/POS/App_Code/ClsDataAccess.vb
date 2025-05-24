Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System
Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports System.Collections

Public Class ClsSqlParameter
    Private _ParaName As String
    Private _ParaValue As Object
    Private _ParaType As SqlDbType
    Private _ParaDirection As ParameterDirection
    Private _Parasize As Integer
    Public Property ParaValue() As Object
        Get
            Return _ParaValue
        End Get
        Set(ByVal value As Object)
            _ParaValue = value
        End Set
    End Property
    Public Property ParaName() As String
        Get
            Return _ParaName
        End Get
        Set(ByVal value As String)
            _ParaName = value
        End Set
    End Property

    Public Property ParaType() As SqlDbType
        Get
            Return _ParaType
        End Get
        Set(ByVal value As SqlDbType)
            _ParaType = value
        End Set
    End Property

    Public Property ParaDirection() As ParameterDirection
        Get
            Return _ParaDirection
        End Get
        Set(ByVal value As ParameterDirection)
            _ParaDirection = value
        End Set
    End Property

    Public Property ParaSize() As Integer
        Get
            Return _Parasize
        End Get
        Set(ByVal value As Integer)
            _Parasize = value
        End Set
    End Property

    Public Sub New(ByVal sqlParaName As String, ByVal SqlParaValue As Object, Optional ByVal SqlParaType As SqlDbType = SqlDbType.VarChar, Optional ByVal SqlParaDirection As ParameterDirection = ParameterDirection.Input, Optional ByVal SqlParaSize As Integer = -1)
        Try
            ParaName = sqlParaName
            ParaValue = SqlParaValue
            ParaType = SqlParaType
            ParaDirection = SqlParaDirection
            ParaSize = SqlParaSize
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
Public Class ColSqlparram
    Inherits System.Collections.ObjectModel.KeyedCollection(Of String, ClsSqlParameter)

    Private _Skey As String
    Private _SPName As String

    Public Property sKey() As String
        Get
            Return _Skey
        End Get
        Set(ByVal value As String)
            _Skey = value
        End Set
    End Property
    Public Property SPName() As String
        Get
            Return _SPName
        End Get
        Set(ByVal value As String)
            _SPName = value
        End Set
    End Property

    Protected Overrides Function GetKeyForItem(ByVal item As ClsSqlParameter) As String
        Return item.ParaName
    End Function
    Public Overloads Sub Add(ByVal sqlParaName As String, ByVal SqlParaValue As Object, Optional ByVal SqlParaType As SqlDbType = SqlDbType.VarChar, Optional ByVal SqlParaDirection As ParameterDirection = ParameterDirection.Input, Optional ByVal SqlParaSize As Integer = -1)
        Try
            Me.Add(New ClsSqlParameter(sqlParaName, SqlParaValue, SqlParaType, SqlParaDirection, SqlParaSize))
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Public Overloads Sub Add_Line_No102:" + ex.Message)
        End Try
    End Sub
End Class
Public Class ColStoredProcedure
    Inherits System.Collections.ObjectModel.KeyedCollection(Of String, ColSqlparram)
    Protected Overrides Function GetKeyForItem(ByVal item As ColSqlparram) As String
        Return item.sKey
    End Function
End Class

Public Class ClsDataccess
    Implements System.IDisposable

    Dim _intNoRecords As Integer
    Dim Strcon As String
    Dim Strcon_Max As String
    'Dim dbmgr As DBManager = dbmgr.GetSingleton()
    Dim dbmgr As New DBManager



    Public Property intNoRecords() As Integer
        Get
            Return _intNoRecords
        End Get
        Set(ByVal value As Integer)
            _intNoRecords = value
        End Set
    End Property
    Public Sub Dispose() Implements IDisposable.Dispose
        Try

            GC.SuppressFinalize(Me)
        Catch ex As Exception
            LogHelper.Error("ClsDataAccess.Dispose[Method]:" + ex.Message)

        End Try
    End Sub
    Public Sub Destroy()
        Try
            GC.SuppressFinalize(Me)
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Destroy" & ex.Message)
        End Try
    End Sub
    Public Sub directexecute(ByVal strqrye As String)
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                Dim com As New SqlClient.SqlCommand
                com.CommandText = strqrye
                com.CommandType = CommandType.Text
                com.Connection = connection
                com.ExecuteScalar()
            End Using
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:directexecute" & ex.Message)
        End Try
    End Sub
    Public Sub ExecStoredProcedure_Login(ByVal strProcedureName As String, ByVal oColSqlparram As ColSqlparram)
        Dim intParaCount As Integer
        Dim com As New SqlClient.SqlCommand
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                com.CommandText = strProcedureName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.CommandTimeout = 0
                com.Parameters.Clear()
                With oColSqlparram
                    For intParaCount = 0 To .Count - 1
                        With .Item(intParaCount)
                            Dim parameter As New SqlClient.SqlParameter
                            parameter.ParameterName = .ParaName
                            parameter.SqlDbType = .ParaType
                            If parameter.SqlDbType = SqlDbType.VarChar And .ParaDirection = ParameterDirection.Input Then
                                parameter.Size = IIf(Len(.ParaValue) = 0, 1, Len(.ParaValue))
                            ElseIf parameter.SqlDbType = SqlDbType.VarChar And .ParaDirection <> ParameterDirection.Input Then
                                parameter.Size = IIf(Len(.ParaValue) = 0, 1000, Len(.ParaValue))
                            End If
                            parameter.Value = .ParaValue
                            parameter.Direction = .ParaDirection
                            com.Parameters.Add(parameter)
                        End With
                    Next
                End With
                com.ExecuteScalar()
                With oColSqlparram
                    For intParaCount = 0 To .Count - 1
                        With .Item(intParaCount)
                            If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
                                .ParaValue = CheckNull(com.Parameters(.ParaName).Value)
                            End If
                        End With
                    Next
                End With
            End Using
        Catch ex As Exception
            If UCase(Left(ex.Message, 27)) = "DELETE STATEMENT CONFLICTED" Or InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0 Then
                With oColSqlparram
                    For intParaCount = 0 To .Count - 1
                        With .Item(intParaCount)
                            If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
                                .ParaValue = 0
                            End If
                        End With
                    Next
                End With
            Else
                If ex.Message.Contains("Your Version Expired Please Contact Administrator") Then
                    Throw ex
                Else
                    LogHelper.Error("ClsDataccess:ExecStoredProcedure_Login:" & ex.Message)
                End If
            End If
        Finally
            com = Nothing
        End Try
    End Sub
    Public Sub ExecStoredProcedure(ByVal strProcedureName As String, ByVal oColSqlparram As ColSqlparram)
        Dim intParaCount As Integer
        Dim com As New SqlClient.SqlCommand
        Using connection As New SqlConnection(dbmgr.get_first_ConnString)
            Dim sql_Tran As SqlClient.SqlTransaction
            Try

                connection.Open()
                sql_Tran = connection.BeginTransaction
                com.CommandText = strProcedureName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                'com.CommandTimeout = 0
                com.CommandTimeout = 600
                com.Transaction = sql_Tran
                com.Parameters.Clear()
                With oColSqlparram
                    For intParaCount = 0 To .Count - 1
                        With .Item(intParaCount)
                            Dim parameter As New SqlClient.SqlParameter
                            parameter.ParameterName = .ParaName
                            parameter.SqlDbType = .ParaType
                            If parameter.SqlDbType = SqlDbType.VarChar And .ParaDirection = ParameterDirection.Input Then
                                parameter.Size = IIf(Len(.ParaValue) = 0, 1, Len(.ParaValue))
                            ElseIf parameter.SqlDbType = SqlDbType.VarChar And .ParaDirection <> ParameterDirection.Input Then
                                parameter.Size = IIf(Len(.ParaValue) = 0, 1000, Len(.ParaValue))
                            End If
                            parameter.Value = .ParaValue
                            parameter.Direction = .ParaDirection
                            com.Parameters.Add(parameter)
                        End With
                    Next
                End With
                'com.ResetCommandTimeout()
                com.ExecuteNonQuery()
                With oColSqlparram
                    For intParaCount = 0 To .Count - 1
                        With .Item(intParaCount)
                            If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
                                .ParaValue = CheckNull(com.Parameters(.ParaName).Value)
                            End If
                        End With
                    Next
                End With
                sql_Tran.Commit()
            Catch ex As Exception
                sql_Tran.Rollback()
                If UCase(Left(ex.Message, 27)) = "DELETE STATEMENT CONFLICTED" Or InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0 Then
                    With oColSqlparram
                        For intParaCount = 0 To .Count - 1
                            With .Item(intParaCount)
                                If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
                                    .ParaValue = 0
                                End If
                            End With
                        Next
                    End With
                Else
                    Throw ex
                End If
            Finally
                com = Nothing

            End Try
        End Using
    End Sub
    Public Sub execBatchStoredProcedure(ByVal oClscollstoredprocedure As ColStoredProcedure, Optional ByVal blBreakonerror As Boolean = True)
        Try
            Dim intParaCount As Integer

            For intParaCount = 0 To oClscollstoredprocedure.Count - 1
                Try
                    ExecStoredProcedure(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount))
                Catch ex As Exception
                    If blBreakonerror Then
                        Throw ex
                    End If
                End Try
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub Salary_execBatchStoredProcedure(ByVal oClscollstoredprocedure As ColStoredProcedure, Optional ByVal blBreakonerror As Boolean = True, Optional ByRef flg As Integer = 0)
        Try
            Dim intParaCount As Integer

            For intParaCount = 0 To oClscollstoredprocedure.Count - 1
                Try
                    ExecStoredProcedure(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount))
                    flg = 1
                Catch ex As Exception
                    If blBreakonerror Then
                        Throw ex
                    End If
                End Try
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub execBatchStoredProcedure_New(ByVal oClscollstoredprocedure As ColStoredProcedure, Optional ByVal blBreakonerror As Boolean = True)
        Using connection As New SqlConnection(dbmgr.get_first_ConnString)
            Dim Sql_Tran As SqlClient.SqlTransaction
            Try
                Dim intParaCount As Integer

                connection.Open()
                Sql_Tran = connection.BeginTransaction
                For intParaCount = 0 To oClscollstoredprocedure.Count - 1
                    Try

                        ExecStoredProcedure_New(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount), oClscollstoredprocedure(0), connection, Sql_Tran)
                    Catch ex As Exception
                        If blBreakonerror Then

                            Throw ex
                        End If
                    End Try
                Next
                Sql_Tran.Commit()
            Catch ex As Exception
                Sql_Tran.Rollback()
                Throw ex
            Finally

            End Try
        End Using
    End Sub
    Public Sub ExecStoredProcedure_New(ByVal strProcedureName As String, ByVal oColSqlparram As ColSqlparram, ByVal oColSqlparram_Main As ColSqlparram, Optional ByVal sqlconnection As SqlConnection = Nothing, Optional ByVal Sql_Tran As SqlClient.SqlTransaction = Nothing)
        Dim intParaCount As Integer
        Dim strReturnValue As String
        Dim com As New SqlClient.SqlCommand
        Try

            com.CommandText = strProcedureName
            com.CommandType = CommandType.StoredProcedure
            com.Connection = sqlconnection
            com.Transaction = Sql_Tran
            com.CommandTimeout = 0
            com.Parameters.Clear()
            strReturnValue = ""
            With oColSqlparram
                For intParaCount = 0 To .Count - 1
                    If CheckValue_New(.Item(intParaCount).ParaValue, oColSqlparram_Main, strReturnValue) Then
                        With .Item(intParaCount)
                            Dim parameter As New SqlClient.SqlParameter
                            parameter.ParameterName = .ParaName
                            parameter.SqlDbType = .ParaType
                            If parameter.SqlDbType = SqlDbType.VarChar Then IIf(Len(parameter.Size) = 0, 1, Len(.ParaValue))
                            If .ParaType = SqlDbType.Int Or .ParaType = SqlDbType.Decimal Or .ParaType = SqlDbType.Float Then
                                parameter.Value = Val(strReturnValue)
                            Else
                                parameter.Value = strReturnValue
                            End If
                            parameter.Direction = .ParaDirection
                            com.Parameters.Add(parameter)
                        End With
                    Else
                        With .Item(intParaCount)
                            Dim parameter As New SqlClient.SqlParameter
                            parameter.ParameterName = .ParaName
                            parameter.SqlDbType = .ParaType
                            If parameter.SqlDbType = SqlDbType.VarChar Then IIf(Len(parameter.Size) = 0, 1, Len(.ParaValue))
                            parameter.Value = .ParaValue
                            parameter.Direction = .ParaDirection
                            com.Parameters.Add(parameter)
                        End With
                    End If

                Next
            End With
            com.ExecuteNonQuery()
            With oColSqlparram
                For intParaCount = 0 To .Count - 1
                    With .Item(intParaCount)
                        If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
                            .ParaValue = CheckNull(com.Parameters(.ParaName).Value)
                        End If
                    End With
                Next
            End With

        Catch ex As Exception

            If UCase(Left(ex.Message, 27)) = "DELETE STATEMENT CONFLICTED" Or InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0 Then
                With oColSqlparram
                    For intParaCount = 0 To .Count - 1
                        With .Item(intParaCount)
                            If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
                                .ParaValue = 0
                            End If
                        End With
                    Next
                End With
                Err.Raise(Err.Number, Err.Source, Err.Description)
            Else

                Throw ex
            End If

        Finally
            com = Nothing

        End Try
    End Sub
    Private Function CheckValue_New(ByVal strValue As String, ByVal oColSqlparram As ColSqlparram, ByRef strReturnValue As String) As Boolean
        Try
            Dim strFromStoredProcedure As String, strFromParameter As String, intStoredProcedurePosition As Integer, intParameterPosition As Integer
            CheckValue_New = False

            If UCase(Left(strValue, 11)) = UCase("::GetFrom::") Then
                intStoredProcedurePosition = InStr(Len("::GetFrom::") + 1, strValue, ",") - 2
                strFromStoredProcedure = Mid(strValue, Len("::GetFrom::(") + 1, intStoredProcedurePosition - Len("::GetFrom::"))

                intParameterPosition = InStr(intStoredProcedurePosition + 1, strValue, ")") - 2 - (intStoredProcedurePosition + 1)
                strFromParameter = Mid(strValue, intStoredProcedurePosition + 3, intParameterPosition)

                strReturnValue = oColSqlparram.Item(strFromParameter).ParaValue

                CheckValue_New = True
            Else
                Exit Function
            End If
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:CheckValue_New:" & ex.Message)
        End Try
    End Function

    Public Function CheckNull(ByVal vData As Object) As String
        Try
            CheckNull = IIf(IsDBNull(vData), "", vData)
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:CheckNull:" & ex.Message)
        End Try
    End Function

    Public Sub Bind_Ddl_Month(ByVal ddl_name As DropDownList)
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Month_ID")
            dt.Columns.Add("Month")
            Dim i As Integer
            For i = 1 To 12
                Dim row As DataRow
                row = dt.NewRow
                row("Month_ID") = i
                Select Case i
                    Case 1
                        row("Month") = "January"
                    Case 2
                        row("Month") = "February"
                    Case 3
                        row("Month") = "March"
                    Case 4
                        row("Month") = "April"
                    Case 5
                        row("Month") = "May"
                    Case 6
                        row("Month") = "June"
                    Case 7
                        row("Month") = "July"
                    Case 8
                        row("Month") = "August"
                    Case 9
                        row("Month") = "September"
                    Case 10
                        row("Month") = "October"
                    Case 11
                        row("Month") = "November"
                    Case 12
                        row("Month") = "December"
                End Select
                dt.Rows.Add(row)
            Next
            ddl_name.DataSource = dt
            ddl_name.DataTextField = "Month"
            ddl_name.DataValueField = "Month_ID"
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl_Month:" & ex.Message)
        End Try
    End Sub

    Public Sub Bind_Ddl_Month_Experience(ByVal ddl_name As DropDownList)
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Month_ID")
            dt.Columns.Add("Month")
            Dim i As Integer
            For i = 0 To 11
                Dim row As DataRow
                row = dt.NewRow
                row("Month_ID") = i
                row("Month") = i
                dt.Rows.Add(row)
            Next
            ddl_name.DataSource = dt
            ddl_name.DataTextField = "Month"
            ddl_name.DataValueField = "Month_ID"
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl_Month_Experience:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Ddl_Year_Experience(ByVal ddl_name As DropDownList)
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Year_ID")
            dt.Columns.Add("Year")
            Dim i As Integer
            For i = 0 To 20
                Dim row As DataRow
                row = dt.NewRow
                row("Year_ID") = i
                row("Year") = i
                dt.Rows.Add(row)
            Next
            ddl_name.DataSource = dt
            ddl_name.DataTextField = "Year"
            ddl_name.DataValueField = "Year_ID"
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl_Year_Experience:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Ddl_year(ByVal ddl_name As DropDownList)
        Try
            Dim dt As New DataTable
            dt.Columns.Add("year_ID")
            dt.Columns.Add("year")
            Dim i As Integer
            For i = 2005 To DateTime.Today.Year + 2
                Dim row As DataRow
                row = dt.NewRow
                row("year_ID") = i
                row("year") = i
                dt.Rows.Add(row)
            Next
            ddl_name.DataSource = dt
            ddl_name.DataTextField = "year"
            ddl_name.DataValueField = "year_ID"
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl_year:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Ddl_FYear(ByVal ddl_name As DropDownList)
        Try
            Dim dt As New DataTable
            dt.Columns.Add("year_ID")
            dt.Columns.Add("year")
            Dim i As Integer
            For i = 2005 To DateTime.Today.Year + 2
                Dim row As DataRow
                row = dt.NewRow
                row("year_ID") = i
                row("year") = i & "-" & i + 1
                dt.Rows.Add(row)
            Next
            ddl_name.DataSource = dt
            ddl_name.DataTextField = "year"
            ddl_name.DataValueField = "year_ID"
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl_FYear:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Ddl(ByVal ddl_name As Object, ByVal sqlquery As String, ByVal Textfield As String, ByVal Valuefield As String)
        Try
            Bind_Ddl(ddl_name, Getdatatable(sqlquery), Textfield, Valuefield)
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Ddl(ByVal ddl_name As DropDownList, ByVal sdr As SqlDataReader)
        Try
            AddDefaultField(ddl_name)
            ddl_name.DataSource = sdr
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Ddlwithout(ByVal ddl_name As DropDownList, ByVal sdr As SqlDataReader)
        Try
            ddl_name.Items.Clear()
            ddl_name.AppendDataBoundItems = True
            ddl_name.DataSource = sdr
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddlwithout:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_CheckboxList(ByVal chkl As CheckBoxList, ByVal sdr As SqlDataReader)
        Try
            chkl.DataSource = sdr
            chkl.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_CheckboxList:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Listbox(ByVal chkl As ListBox, ByVal Query As String, ByVal Textfield As String, ByVal Valuefield As String)
        Dim dt As Data.DataTable
        Try
            dt = Getdatatable(Query)
            chkl.DataSource = dt
            chkl.DataTextField = Textfield
            chkl.DataValueField = Valuefield
            chkl.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Listbox:" & ex.Message)
        End Try
    End Sub
    Public Sub BindCheckbox(ByVal lst_name As CheckBoxList, ByVal Query As String, ByVal Textfield As String, ByVal Valuefield As String)
        Dim dt As Data.DataTable
        Try
            dt = Getdatatable(Query)
            lst_name.DataSource = dt
            lst_name.DataTextField = Textfield
            lst_name.DataValueField = Valuefield
            lst_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:BindCheckbox:" & ex.Message)
        End Try
    End Sub
    Public Sub AddDefaultField(ByVal ddl_name As DropDownList, Optional ByVal strText As String = "Select")
        Try
            ddl_name.Items.Clear()
            ddl_name.Items.Insert(0, "------" & strText & "------")
            ddl_name.Items(0).Value = 0
            ddl_name.SelectedValue = 0
            ddl_name.AppendDataBoundItems = True
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:AddDefaultField:" & ex.Message)
        End Try
    End Sub
    Public Sub AddDefaultFieldSearch(ByVal ddl_name As DropDownList, Optional ByVal strText As String = "Select")
        Try
            ddl_name.Items.Clear()
            ddl_name.Items.Insert(0, "---" & strText & "---")
            ddl_name.Items(0).Value = 0
            ddl_name.SelectedValue = 0
            ddl_name.AppendDataBoundItems = True
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:AddDefaultFieldSearch:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Ddl(ByVal ddl_name As Object, ByVal dt As DataTable, ByVal Textfield As String, ByVal Valuefield As String)
        Try
            ddl_name.DataSource = dt
            ddl_name.DataTextField = Textfield
            ddl_name.DataValueField = Valuefield
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl:" & ex.Message)
        End Try
    End Sub
    Public Sub Bind_Ddl_New(ByVal ddl_name As Object, ByVal dt As DataTable, ByVal Textfield As String, ByVal Valuefield As String)
        Try
            ddl_name.DataSource = dt
            ddl_name.DataTextField = Textfield
            ddl_name.DataValueField = Valuefield
            ddl_name.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_Ddl_New:" & ex.Message)
        End Try
    End Sub
    Public Function Getdatareader(ByVal sqlquery As String, Optional ByVal SqlConnection As SqlConnection = Nothing) As SqlDataReader
        Dim glb_dr As SqlDataReader
        Dim glb_com As New SqlClient.SqlCommand()
        Try

            glb_com.CommandText = sqlquery
            glb_com.CommandType = CommandType.Text
            glb_com.Connection = SqlConnection
            glb_dr = glb_com.ExecuteReader()

        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Getdatareader:" & ex.Message)

        End Try
        Return glb_dr
    End Function
    Public Function Bind_DDL_Month(ByVal oGridView As Object, ByVal sqlquery As String) As DataTable
        Dim dttable As New DataTable
        Try
            dttable = Getdatatable(sqlquery)
            oGridView.DataSource = dttable
            oGridView.datamember = "table"
            oGridView.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Bind_DDL_Month:" & ex.Message)
        End Try
        Return dttable
    End Function
    Public Sub BindRepeater(ByVal rp As Repeater, ByVal sdr As SqlDataReader)
        Try
            rp.DataSource = sdr
            rp.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:BindRepeater:" & ex.Message)
        End Try
    End Sub
    Public Sub BindDatalist(ByVal ds As DataList, ByVal sdr As SqlDataReader)
        Try
            ds.DataSource = sdr
            ds.DataBind()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:BindDatalist:" & ex.Message)
        End Try
    End Sub
    Public Function Getdatatable(ByVal Sqlquery As String) As DataTable
        Dim glb_dt As New DataTable()
        Dim glb_com As New SqlClient.SqlCommand()
        Dim glb_adp As New SqlClient.SqlDataAdapter()
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                glb_com.CommandTimeout = 0
                glb_com.CommandText = Sqlquery
                glb_com.CommandType = CommandType.Text
                glb_com.Connection = connection
                glb_adp.SelectCommand = glb_com
                glb_dt.Clear()
                glb_adp.Fill(glb_dt)
            End Using
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Getdatatable:" & ex.Message)
        Finally

            glb_com = Nothing
            glb_adp = Nothing
        End Try
        Return glb_dt
    End Function
    Public Function GetDataview(ByVal Sqlquery As String) As DataView
        Dim Glb_ds As New DataSet
        Dim glb_dt As New DataView()
        Try
            Dim glb_com As New SqlClient.SqlCommand()
            Dim glb_adp As New SqlClient.SqlDataAdapter()

            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                glb_com.CommandText = Sqlquery
                glb_com.CommandType = CommandType.Text
                glb_com.Connection = connection
                glb_adp.SelectCommand = glb_com
                glb_adp.Fill(Glb_ds)
                glb_dt = Glb_ds.Tables(0).DefaultView
            End Using
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:GetDataview:" & ex.Message)
        Finally

        End Try
        Return glb_dt
    End Function
    Public Function Getdatatable_param(ByVal Sqlquery As String, ByVal Param As SqlClient.SqlParameter) As DataTable
        Dim glb_dt As New DataTable()
        Dim glb_com As New SqlClient.SqlCommand()
        Dim glb_adp As New SqlClient.SqlDataAdapter()
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                glb_com.CommandText = Sqlquery
                glb_com.Parameters.Add(Param)
                glb_com.CommandType = CommandType.Text
                glb_com.Connection = connection
                glb_adp.SelectCommand = glb_com
                glb_dt.Clear()
                glb_adp.Fill(glb_dt)
            End Using
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Getdatatable_param:" & ex.Message)
        Finally

            glb_com = Nothing
            glb_adp = Nothing
        End Try
        Return glb_dt
    End Function
    Public Function Getdataset(ByVal Sqlquery As String, ByVal TblAlias As String) As DataSet
        Dim glb_dst As New DataSet()
        Dim glb_com As New SqlClient.SqlCommand()
        Dim glb_adp As New SqlClient.SqlDataAdapter()
        Try

            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                glb_com.CommandText = Sqlquery
                glb_com.CommandType = CommandType.Text
                glb_com.Connection = connection
                glb_adp.SelectCommand = glb_com
                glb_dst.Clear()
                glb_adp.Fill(glb_dst, TblAlias)
            End Using
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:Getdataset:" & ex.Message)
        Finally

            glb_adp = Nothing
            glb_com = Nothing
        End Try
        Return glb_dst
    End Function
    Public Function GetdataList(ByVal Sqlquery As String, ByVal glb_dst As DataList) As DataList
        Try
            Dim glb_com As New SqlClient.SqlCommand()

            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                glb_com.CommandText = Sqlquery
                glb_com.CommandType = CommandType.Text
                glb_com.Connection = connection
                glb_dst.DataSource = glb_com.ExecuteReader()
                glb_dst.DataBind()
            End Using
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:GetdataList:" & ex.Message)
        Finally

        End Try
    End Function
    Public Function GetdatasetSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, ByVal TblAlias As String) As Data.DataSet
        Dim sdr As DataSet = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.CommandTimeout = 600
                com.Parameters.Clear()

                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                    Dim parameter As New SqlClient.SqlParameter
                    parameter.ParameterName = oClsSqlParameter.ParaName
                    parameter.SqlDbType = oClsSqlParameter.ParaType
                    parameter.Value = oClsSqlParameter.ParaValue
                    parameter.Direction = oClsSqlParameter.ParaDirection
                    com.Parameters.Add(parameter)
                Next
                SpAdepter = New SqlDataAdapter(com)
                sdr = New Data.DataSet
                SpAdepter.Fill(sdr)
                Return sdr
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("ClsDataccess:GetdatasetSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing
        End Try
        Return sdr
    End Function

    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.Parameters.Clear()
                com.CommandTimeout = 600

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
            LogHelper.Error("ClsDataccess:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function

    Public Function GetdataTableSp1(ByVal SPName As String, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.Parameters.Clear()
                com.CommandTimeout = 0

                SpAdepter = New SqlDataAdapter(com)
                sdr = New Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("ClsDataccess:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function
    Public Function GetdataView(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataView
        Dim sdr As DataTable = Nothing
        Dim sdr1 As DataView = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
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
                sdr1 = sdr.DefaultView
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
            Return sdr1
        Catch ex As Exception : Throw ex
            LogHelper.Error("ClsDataccess:GetdataView:" & ex.Message)
        Finally
            com.Parameters.Clear()

        End Try
        Return sdr1
    End Function
    Public Function getparentcategory(ByVal categoryid As Integer) As String
        Try
            Dim paramarr As New ArrayList
            paramarr.Add(New Object() {"@category_string", SqlDbType.VarChar, "", ParameterDirection.InputOutput})
            paramarr.Add(New Object() {"@category_id", SqlDbType.Int, categoryid, ParameterDirection.Input})
            Return getstring_fromprocedure("Sp_Get_Parent_Category", paramarr)
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:getparentcategory:" & ex.Message)
        End Try
    End Function
    Public Function getstring_fromprocedure(ByVal SPName As String, ByVal SPParameter As ArrayList) As String
        Dim output_value As String = String.Empty
        Dim glb_com As New SqlClient.SqlCommand()
        Try

            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()
                glb_com.CommandText = SPName
                glb_com.CommandType = CommandType.StoredProcedure
                glb_com.Connection = connection
                glb_com.Parameters.Clear()
                Dim param As Object
                For Each param In SPParameter
                    Dim parameter As New SqlClient.SqlParameter()
                    parameter.ParameterName = CType(param(0), String)
                    parameter.SqlDbType = CType(param(1), SqlDbType)
                    parameter.Value = param(2)
                    parameter.Direction = param(3)
                    If parameter.SqlDbType = SqlDbType.VarChar And parameter.Direction = ParameterDirection.InputOutput Then
                        parameter.Size = 150
                    End If
                    glb_com.Parameters.Add(parameter)
                Next
                glb_com.ExecuteScalar()
            End Using
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:getstring_fromprocedure:" & ex.Message)
        Finally

        End Try
        Return glb_com.Parameters(0).Value
    End Function
    Public Function gettable_fromprocedure(ByVal SPName As String, Optional ByVal SPParameter As ArrayList = Nothing) As DataTable
        Dim output_value As String = String.Empty
        Dim glb_dt As New DataTable
        Dim glb_com As New SqlClient.SqlCommand()
        Try
            Using connection As New SqlConnection(dbmgr.get_first_ConnString)
                connection.Open()

                glb_com.CommandText = SPName
                glb_com.CommandType = CommandType.StoredProcedure
                glb_com.Connection = connection
                glb_com.Parameters.Clear()
                Dim param As Object
                If Not SPParameter Is Nothing Then
                    For Each param In SPParameter
                        Dim parameter As New SqlClient.SqlParameter()
                        parameter.ParameterName = CType(param(0), String)
                        parameter.SqlDbType = CType(param(1), SqlDbType)
                        parameter.Value = param(2)
                        parameter.Direction = param(3)
                        If parameter.SqlDbType = SqlDbType.VarChar And parameter.Direction = ParameterDirection.InputOutput Then
                            parameter.Size = 150
                        End If
                        glb_com.Parameters.Add(parameter)
                    Next
                End If
                glb_dt.Load(glb_com.ExecuteReader())
            End Using
            Return glb_dt
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:gettable_fromprocedure:" & ex.Message)
        Finally

        End Try
        Return glb_dt
    End Function

    Public Function GetdatareaderSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal sqlconn As SqlConnection = Nothing) As SqlDataReader
        Dim sdr As SqlDataReader = Nothing
        Dim com As New SqlCommand()
        Try
            com.CommandText = SPName
            com.CommandType = CommandType.StoredProcedure
            If Not sqlconn Is Nothing Then
                com.Connection = sqlconn
            Else
            End If
            com.Parameters.Clear()
            For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                Dim parameter As New SqlClient.SqlParameter()
                parameter.ParameterName = oClsSqlParameter.ParaName
                parameter.SqlDbType = oClsSqlParameter.ParaType
                parameter.Value = oClsSqlParameter.ParaValue
                parameter.Direction = oClsSqlParameter.ParaDirection
                com.Parameters.Add(parameter)
            Next
            sdr = com.ExecuteReader()
        Catch ex As Exception
            LogHelper.Error("ClsDataccess:GetdatareaderSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
        End Try
        Return sdr

    End Function

End Class



'Imports Microsoft.VisualBasic
'Imports System.Data
'Imports System.Data.SqlClient
'Imports System.Configuration
'Imports System
'Imports System.Web.UI.Page
'Public Class ClsSqlParameter
'    Private _ParaName As String
'    Private _ParaValue As Object
'    Private _ParaType As SqlDbType
'    Private _ParaDirection As ParameterDirection
'    Private _Parasize As Integer
'    Public Property ParaValue() As Object
'        Get
'            Return _ParaValue
'        End Get
'        Set(ByVal value As Object)
'            _ParaValue = value
'        End Set
'    End Property
'    Public Property ParaName() As String
'        Get
'            Return _ParaName
'        End Get
'        Set(ByVal value As String)
'            _ParaName = value
'        End Set
'    End Property

'    Public Property ParaType() As SqlDbType
'        Get
'            Return _ParaType
'        End Get
'        Set(ByVal value As SqlDbType)
'            _ParaType = value
'        End Set
'    End Property

'    Public Property ParaDirection() As ParameterDirection
'        Get
'            Return _ParaDirection
'        End Get
'        Set(ByVal value As ParameterDirection)
'            _ParaDirection = value
'        End Set
'    End Property

'    Public Property ParaSize() As Integer
'        Get
'            Return _Parasize
'        End Get
'        Set(ByVal value As Integer)
'            _Parasize = value
'        End Set
'    End Property

'    Public Sub New(ByVal sqlParaName As String, ByVal SqlParaValue As Object, Optional ByVal SqlParaType As SqlDbType = SqlDbType.VarChar, Optional ByVal SqlParaDirection As ParameterDirection = ParameterDirection.Input, Optional ByVal SqlParaSize As Integer = -1)
'        Try
'            ParaName = sqlParaName
'            ParaValue = SqlParaValue
'            ParaType = SqlParaType
'            ParaDirection = SqlParaDirection
'            ParaSize = SqlParaSize
'        Catch ex As Exception
'            Throw ex
'        End Try
'    End Sub
'End Class
'Public Class ColSqlparram
'    Inherits System.Collections.ObjectModel.KeyedCollection(Of String, ClsSqlParameter)

'    Private _Skey As String
'    Private _SPName As String

'    Public Property sKey() As String
'        Get
'            Return _Skey
'        End Get
'        Set(ByVal value As String)
'            _Skey = value
'        End Set
'    End Property
'    Public Property SPName() As String
'        Get
'            Return _SPName
'        End Get
'        Set(ByVal value As String)
'            _SPName = value
'        End Set
'    End Property

'    Protected Overrides Function GetKeyForItem(ByVal item As ClsSqlParameter) As String
'        Return item.ParaName
'    End Function
'    Public Overloads Sub Add(ByVal sqlParaName As String, ByVal SqlParaValue As Object, Optional ByVal SqlParaType As SqlDbType = SqlDbType.VarChar, Optional ByVal SqlParaDirection As ParameterDirection = ParameterDirection.Input, Optional ByVal SqlParaSize As Integer = -1)
'        Try
'            Me.Add(New ClsSqlParameter(sqlParaName, SqlParaValue, SqlParaType, SqlParaDirection, SqlParaSize))
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Public Overloads Sub Add_Line_No102:" + ex.Message)
'        End Try
'    End Sub
'End Class
'Public Class ColStoredProcedure
'    Inherits System.Collections.ObjectModel.KeyedCollection(Of String, ColSqlparram)
'    Protected Overrides Function GetKeyForItem(ByVal item As ColSqlparram) As String
'        Return item.sKey
'    End Function
'End Class

'Public Class ClsDataccess
'    Implements System.IDisposable

'    Dim _intNoRecords As Integer
'    Dim Strcon As String
'    Dim Strcon_Max As String

'    Dim dbmgr As DBManager = DBManager.GetSingleton()
'    Public Property intNoRecords() As Integer
'        Get
'            Return _intNoRecords
'        End Get
'        Set(ByVal value As Integer)
'            _intNoRecords = value
'        End Set
'    End Property
'    Public Sub Dispose() Implements IDisposable.Dispose
'        Try

'            GC.SuppressFinalize(Me)
'        Catch ex As Exception
'            LogHelper.Error("ClsDataAccess.Dispose[Method]:" + ex.Message)

'        End Try
'    End Sub
'    Public Sub Destroy()
'        Try
'            GC.SuppressFinalize(Me)
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Destroy" & ex.Message)
'        End Try
'    End Sub
'    Public Sub directexecute(ByVal strqrye As String)
'        Try
'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                Dim com As New SqlClient.SqlCommand
'                com.CommandText = strqrye
'                com.CommandType = CommandType.Text
'                com.Connection = connection
'                com.ExecuteScalar()
'            End Using
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:directexecute" & ex.Message)
'        End Try
'    End Sub
'    Public Sub ExecStoredProcedure_Login(ByVal strProcedureName As String, ByVal oColSqlparram As ColSqlparram)
'        Dim intParaCount As Integer
'        Dim com As New SqlClient.SqlCommand
'        Try
'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                com.CommandText = strProcedureName
'                com.CommandType = CommandType.StoredProcedure
'                com.Connection = connection
'                com.CommandTimeout = 0
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
'                com.ExecuteScalar()
'                With oColSqlparram
'                    For intParaCount = 0 To .Count - 1
'                        With .Item(intParaCount)
'                            If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
'                                .ParaValue = CheckNull(com.Parameters(.ParaName).Value)
'                            End If
'                        End With
'                    Next
'                End With
'            End Using
'        Catch ex As Exception
'            If UCase(Left(ex.Message, 27)) = "DELETE STATEMENT CONFLICTED" Or InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0 Then
'                With oColSqlparram
'                    For intParaCount = 0 To .Count - 1
'                        With .Item(intParaCount)
'                            If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
'                                .ParaValue = 0
'                            End If
'                        End With
'                    Next
'                End With
'            Else
'                If ex.Message.Contains("Your Version Expired Please Contact Administrator") Then
'                    Throw ex
'                Else
'                    LogHelper.Error("ClsDataccess:ExecStoredProcedure_Login:" & ex.Message)
'                End If
'            End If
'        Finally
'            com = Nothing
'        End Try
'    End Sub
'    Public Sub ExecStoredProcedure(ByVal strProcedureName As String, ByVal oColSqlparram As ColSqlparram)
'        Dim intParaCount As Integer
'        Dim com As New SqlClient.SqlCommand
'        Using connection As New SqlConnection(DBManager.get_first_ConnString)
'            Dim sql_Tran As SqlClient.SqlTransaction
'            Try

'                connection.Open()
'                sql_Tran = connection.BeginTransaction
'                com.CommandText = strProcedureName
'                com.CommandType = CommandType.StoredProcedure
'                com.Connection = connection
'                'com.CommandTimeout = 0
'                com.CommandTimeout = 600
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
'    Public Sub execBatchStoredProcedure(ByVal oClscollstoredprocedure As ColStoredProcedure, Optional ByVal blBreakonerror As Boolean = True)
'        Try
'            Dim intParaCount As Integer

'            For intParaCount = 0 To oClscollstoredprocedure.Count - 1
'                Try
'                    ExecStoredProcedure(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount))
'                Catch ex As Exception
'                    If blBreakonerror Then
'                        Throw ex
'                    End If
'                End Try
'            Next
'        Catch ex As Exception
'            Throw ex
'        End Try
'    End Sub
'    Public Sub Salary_execBatchStoredProcedure(ByVal oClscollstoredprocedure As ColStoredProcedure, Optional ByVal blBreakonerror As Boolean = True, Optional ByRef flg As Integer = 0)
'        Try
'            Dim intParaCount As Integer

'            For intParaCount = 0 To oClscollstoredprocedure.Count - 1
'                Try
'                    ExecStoredProcedure(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount))
'                    flg = 1
'                Catch ex As Exception
'                    If blBreakonerror Then
'                        Throw ex
'                    End If
'                End Try
'            Next
'        Catch ex As Exception
'            Throw ex
'        End Try
'    End Sub

'    Public Sub execBatchStoredProcedure_New(ByVal oClscollstoredprocedure As ColStoredProcedure, Optional ByVal blBreakonerror As Boolean = True)
'        Using connection As New SqlConnection(DBManager.get_first_ConnString)
'            Dim Sql_Tran As SqlClient.SqlTransaction
'            Try
'                Dim intParaCount As Integer

'                connection.Open()
'                Sql_Tran = connection.BeginTransaction
'                For intParaCount = 0 To oClscollstoredprocedure.Count - 1
'                    Try

'                        ExecStoredProcedure_New(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount), oClscollstoredprocedure(0), connection, Sql_Tran)
'                    Catch ex As Exception
'                        If blBreakonerror Then

'                            Throw ex
'                        End If
'                    End Try
'                Next
'                Sql_Tran.Commit()
'            Catch ex As Exception
'                Sql_Tran.Rollback()
'                Throw ex
'            Finally

'            End Try
'        End Using
'    End Sub
'    Public Sub ExecStoredProcedure_New(ByVal strProcedureName As String, ByVal oColSqlparram As ColSqlparram, ByVal oColSqlparram_Main As ColSqlparram, Optional ByVal sqlconnection As SqlConnection = Nothing, Optional ByVal Sql_Tran As SqlClient.SqlTransaction = Nothing)
'        Dim intParaCount As Integer
'        Dim strReturnValue As String
'        Dim com As New SqlClient.SqlCommand
'        Try

'            com.CommandText = strProcedureName
'            com.CommandType = CommandType.StoredProcedure
'            com.Connection = sqlconnection
'            com.Transaction = Sql_Tran
'            com.CommandTimeout = 0
'            com.Parameters.Clear()
'            strReturnValue = ""
'            With oColSqlparram
'                For intParaCount = 0 To .Count - 1
'                    If CheckValue_New(.Item(intParaCount).ParaValue, oColSqlparram_Main, strReturnValue) Then
'                        With .Item(intParaCount)
'                            Dim parameter As New SqlClient.SqlParameter
'                            parameter.ParameterName = .ParaName
'                            parameter.SqlDbType = .ParaType
'                            If parameter.SqlDbType = SqlDbType.VarChar Then IIf(Len(parameter.Size) = 0, 1, Len(.ParaValue))
'                            If .ParaType = SqlDbType.Int Or .ParaType = SqlDbType.Decimal Or .ParaType = SqlDbType.Float Then
'                                parameter.Value = Val(strReturnValue)
'                            Else
'                                parameter.Value = strReturnValue
'                            End If
'                            parameter.Direction = .ParaDirection
'                            com.Parameters.Add(parameter)
'                        End With
'                    Else
'                        With .Item(intParaCount)
'                            Dim parameter As New SqlClient.SqlParameter
'                            parameter.ParameterName = .ParaName
'                            parameter.SqlDbType = .ParaType
'                            If parameter.SqlDbType = SqlDbType.VarChar Then IIf(Len(parameter.Size) = 0, 1, Len(.ParaValue))
'                            parameter.Value = .ParaValue
'                            parameter.Direction = .ParaDirection
'                            com.Parameters.Add(parameter)
'                        End With
'                    End If

'                Next
'            End With
'            com.ExecuteNonQuery()
'            With oColSqlparram
'                For intParaCount = 0 To .Count - 1
'                    With .Item(intParaCount)
'                        If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
'                            .ParaValue = CheckNull(com.Parameters(.ParaName).Value)
'                        End If
'                    End With
'                Next
'            End With

'        Catch ex As Exception

'            If UCase(Left(ex.Message, 27)) = "DELETE STATEMENT CONFLICTED" Or InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0 Then
'                With oColSqlparram
'                    For intParaCount = 0 To .Count - 1
'                        With .Item(intParaCount)
'                            If .ParaDirection = ParameterDirection.InputOutput Or .ParaDirection = ParameterDirection.Output Then
'                                .ParaValue = 0
'                            End If
'                        End With
'                    Next
'                End With
'                Err.Raise(Err.Number, Err.Source, Err.Description)
'            Else

'                Throw ex
'            End If

'        Finally
'            com = Nothing

'        End Try
'    End Sub
'    Private Function CheckValue_New(ByVal strValue As String, ByVal oColSqlparram As ColSqlparram, ByRef strReturnValue As String) As Boolean
'        Try
'            Dim strFromStoredProcedure As String, strFromParameter As String, intStoredProcedurePosition As Integer, intParameterPosition As Integer
'            CheckValue_New = False

'            If UCase(Left(strValue, 11)) = UCase("::GetFrom::") Then
'                intStoredProcedurePosition = InStr(Len("::GetFrom::") + 1, strValue, ",") - 2
'                strFromStoredProcedure = Mid(strValue, Len("::GetFrom::(") + 1, intStoredProcedurePosition - Len("::GetFrom::"))

'                intParameterPosition = InStr(intStoredProcedurePosition + 1, strValue, ")") - 2 - (intStoredProcedurePosition + 1)
'                strFromParameter = Mid(strValue, intStoredProcedurePosition + 3, intParameterPosition)

'                strReturnValue = oColSqlparram.Item(strFromParameter).ParaValue

'                CheckValue_New = True
'            Else
'                Exit Function
'            End If
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:CheckValue_New:" & ex.Message)
'        End Try
'    End Function

'    Public Function CheckNull(ByVal vData As Object) As String
'        Try
'            CheckNull = IIf(IsDBNull(vData), "", vData)
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:CheckNull:" & ex.Message)
'        End Try
'    End Function

'    Public Sub Bind_Ddl_Month(ByVal ddl_name As DropDownList)
'        Try
'            Dim dt As New DataTable
'            dt.Columns.Add("Month_ID")
'            dt.Columns.Add("Month")
'            Dim i As Integer
'            For i = 1 To 12
'                Dim row As DataRow
'                row = dt.NewRow
'                row("Month_ID") = i
'                Select Case i
'                    Case 1
'                        row("Month") = "January"
'                    Case 2
'                        row("Month") = "February"
'                    Case 3
'                        row("Month") = "March"
'                    Case 4
'                        row("Month") = "April"
'                    Case 5
'                        row("Month") = "May"
'                    Case 6
'                        row("Month") = "June"
'                    Case 7
'                        row("Month") = "July"
'                    Case 8
'                        row("Month") = "August"
'                    Case 9
'                        row("Month") = "September"
'                    Case 10
'                        row("Month") = "October"
'                    Case 11
'                        row("Month") = "November"
'                    Case 12
'                        row("Month") = "December"
'                End Select
'                dt.Rows.Add(row)
'            Next
'            ddl_name.DataSource = dt
'            ddl_name.DataTextField = "Month"
'            ddl_name.DataValueField = "Month_ID"
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl_Month:" & ex.Message)
'        End Try
'    End Sub

'    Public Sub Bind_Ddl_Month_Experience(ByVal ddl_name As DropDownList)
'        Try
'            Dim dt As New DataTable
'            dt.Columns.Add("Month_ID")
'            dt.Columns.Add("Month")
'            Dim i As Integer
'            For i = 0 To 11
'                Dim row As DataRow
'                row = dt.NewRow
'                row("Month_ID") = i
'                row("Month") = i
'                dt.Rows.Add(row)
'            Next
'            ddl_name.DataSource = dt
'            ddl_name.DataTextField = "Month"
'            ddl_name.DataValueField = "Month_ID"
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl_Month_Experience:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Ddl_Year_Experience(ByVal ddl_name As DropDownList)
'        Try
'            Dim dt As New DataTable
'            dt.Columns.Add("Year_ID")
'            dt.Columns.Add("Year")
'            Dim i As Integer
'            For i = 0 To 20
'                Dim row As DataRow
'                row = dt.NewRow
'                row("Year_ID") = i
'                row("Year") = i
'                dt.Rows.Add(row)
'            Next
'            ddl_name.DataSource = dt
'            ddl_name.DataTextField = "Year"
'            ddl_name.DataValueField = "Year_ID"
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl_Year_Experience:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Ddl_year(ByVal ddl_name As DropDownList)
'        Try
'            Dim dt As New DataTable
'            dt.Columns.Add("year_ID")
'            dt.Columns.Add("year")
'            Dim i As Integer
'            For i = 2005 To DateTime.Today.Year + 2
'                Dim row As DataRow
'                row = dt.NewRow
'                row("year_ID") = i
'                row("year") = i
'                dt.Rows.Add(row)
'            Next
'            ddl_name.DataSource = dt
'            ddl_name.DataTextField = "year"
'            ddl_name.DataValueField = "year_ID"
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl_year:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Ddl_FYear(ByVal ddl_name As DropDownList)
'        Try
'            Dim dt As New DataTable
'            dt.Columns.Add("year_ID")
'            dt.Columns.Add("year")
'            Dim i As Integer
'            For i = 2005 To DateTime.Today.Year + 2
'                Dim row As DataRow
'                row = dt.NewRow
'                row("year_ID") = i
'                row("year") = i & "-" & i + 1
'                dt.Rows.Add(row)
'            Next
'            ddl_name.DataSource = dt
'            ddl_name.DataTextField = "year"
'            ddl_name.DataValueField = "year_ID"
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl_FYear:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Ddl(ByVal ddl_name As Object, ByVal sqlquery As String, ByVal Textfield As String, ByVal Valuefield As String)
'        Try
'            Bind_Ddl(ddl_name, Getdatatable(sqlquery), Textfield, Valuefield)
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Ddl(ByVal ddl_name As DropDownList, ByVal sdr As SqlDataReader)
'        Try
'            AddDefaultField(ddl_name)
'            ddl_name.DataSource = sdr
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Ddlwithout(ByVal ddl_name As DropDownList, ByVal sdr As SqlDataReader)
'        Try
'            ddl_name.Items.Clear()
'            ddl_name.AppendDataBoundItems = True
'            ddl_name.DataSource = sdr
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddlwithout:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_CheckboxList(ByVal chkl As CheckBoxList, ByVal sdr As SqlDataReader)
'        Try
'            chkl.DataSource = sdr
'            chkl.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_CheckboxList:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Listbox(ByVal chkl As ListBox, ByVal Query As String, ByVal Textfield As String, ByVal Valuefield As String)
'        Dim dt As Data.DataTable
'        Try
'            dt = Getdatatable(Query)
'            chkl.DataSource = dt
'            chkl.DataTextField = Textfield
'            chkl.DataValueField = Valuefield
'            chkl.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Listbox:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub BindCheckbox(ByVal lst_name As CheckBoxList, ByVal Query As String, ByVal Textfield As String, ByVal Valuefield As String)
'        Dim dt As Data.DataTable
'        Try
'            dt = Getdatatable(Query)
'            lst_name.DataSource = dt
'            lst_name.DataTextField = Textfield
'            lst_name.DataValueField = Valuefield
'            lst_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:BindCheckbox:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub AddDefaultField(ByVal ddl_name As DropDownList, Optional ByVal strText As String = "Select")
'        Try
'            ddl_name.Items.Clear()
'            ddl_name.Items.Insert(0, "------" & strText & "------")
'            ddl_name.Items(0).Value = 0
'            ddl_name.SelectedValue = 0
'            ddl_name.AppendDataBoundItems = True
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:AddDefaultField:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub AddDefaultFieldSearch(ByVal ddl_name As DropDownList, Optional ByVal strText As String = "Select")
'        Try
'            ddl_name.Items.Clear()
'            ddl_name.Items.Insert(0, "---" & strText & "---")
'            ddl_name.Items(0).Value = 0
'            ddl_name.SelectedValue = 0
'            ddl_name.AppendDataBoundItems = True
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:AddDefaultFieldSearch:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Ddl(ByVal ddl_name As Object, ByVal dt As DataTable, ByVal Textfield As String, ByVal Valuefield As String)
'        Try
'            ddl_name.DataSource = dt
'            ddl_name.DataTextField = Textfield
'            ddl_name.DataValueField = Valuefield
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub Bind_Ddl_New(ByVal ddl_name As Object, ByVal dt As DataTable, ByVal Textfield As String, ByVal Valuefield As String)
'        Try
'            ddl_name.DataSource = dt
'            ddl_name.DataTextField = Textfield
'            ddl_name.DataValueField = Valuefield
'            ddl_name.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_Ddl_New:" & ex.Message)
'        End Try
'    End Sub
'    Public Function Getdatareader(ByVal sqlquery As String, Optional ByVal SqlConnection As SqlConnection = Nothing) As SqlDataReader
'        Dim glb_dr As SqlDataReader
'        Dim glb_com As New SqlClient.SqlCommand()
'        Try

'            glb_com.CommandText = sqlquery
'            glb_com.CommandType = CommandType.Text
'            glb_com.Connection = SqlConnection
'            glb_dr = glb_com.ExecuteReader()

'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Getdatareader:" & ex.Message)

'        End Try
'        Return glb_dr
'    End Function
'    Public Function Bind_DDL_Month(ByVal oGridView As Object, ByVal sqlquery As String) As DataTable
'        Dim dttable As New DataTable
'        Try
'            dttable = Getdatatable(sqlquery)
'            oGridView.DataSource = dttable
'            oGridView.datamember = "table"
'            oGridView.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Bind_DDL_Month:" & ex.Message)
'        End Try
'        Return dttable
'    End Function
'    Public Sub BindRepeater(ByVal rp As Repeater, ByVal sdr As SqlDataReader)
'        Try
'            rp.DataSource = sdr
'            rp.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:BindRepeater:" & ex.Message)
'        End Try
'    End Sub
'    Public Sub BindDatalist(ByVal ds As DataList, ByVal sdr As SqlDataReader)
'        Try
'            ds.DataSource = sdr
'            ds.DataBind()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:BindDatalist:" & ex.Message)
'        End Try
'    End Sub
'    Public Function Getdatatable(ByVal Sqlquery As String) As DataTable
'        Dim glb_dt As New DataTable()
'        Dim glb_com As New SqlClient.SqlCommand()
'        Dim glb_adp As New SqlClient.SqlDataAdapter()
'        Try
'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                glb_com.CommandTimeout = 0
'                glb_com.CommandText = Sqlquery
'                glb_com.CommandType = CommandType.Text
'                glb_com.Connection = connection
'                glb_adp.SelectCommand = glb_com
'                glb_dt.Clear()
'                glb_adp.Fill(glb_dt)
'            End Using
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Getdatatable:" & ex.Message)
'        Finally

'            glb_com = Nothing
'            glb_adp = Nothing
'        End Try
'        Return glb_dt
'    End Function
'    Public Function GetDataview(ByVal Sqlquery As String) As DataView
'        Dim Glb_ds As New DataSet
'        Dim glb_dt As New DataView()
'        Try
'            Dim glb_com As New SqlClient.SqlCommand()
'            Dim glb_adp As New SqlClient.SqlDataAdapter()

'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                glb_com.CommandText = Sqlquery
'                glb_com.CommandType = CommandType.Text
'                glb_com.Connection = connection
'                glb_adp.SelectCommand = glb_com
'                glb_adp.Fill(Glb_ds)
'                glb_dt = Glb_ds.Tables(0).DefaultView
'            End Using
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:GetDataview:" & ex.Message)
'        Finally

'        End Try
'        Return glb_dt
'    End Function
'    Public Function Getdatatable_param(ByVal Sqlquery As String, ByVal Param As SqlClient.SqlParameter) As DataTable
'        Dim glb_dt As New DataTable()
'        Dim glb_com As New SqlClient.SqlCommand()
'        Dim glb_adp As New SqlClient.SqlDataAdapter()
'        Try
'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                glb_com.CommandText = Sqlquery
'                glb_com.Parameters.Add(Param)
'                glb_com.CommandType = CommandType.Text
'                glb_com.Connection = connection
'                glb_adp.SelectCommand = glb_com
'                glb_dt.Clear()
'                glb_adp.Fill(glb_dt)
'            End Using
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Getdatatable_param:" & ex.Message)
'        Finally

'            glb_com = Nothing
'            glb_adp = Nothing
'        End Try
'        Return glb_dt
'    End Function
'    Public Function Getdataset(ByVal Sqlquery As String, ByVal TblAlias As String) As DataSet
'        Dim glb_dst As New DataSet()
'        Dim glb_com As New SqlClient.SqlCommand()
'        Dim glb_adp As New SqlClient.SqlDataAdapter()
'        Try

'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                glb_com.CommandText = Sqlquery
'                glb_com.CommandType = CommandType.Text
'                glb_com.Connection = connection
'                glb_adp.SelectCommand = glb_com
'                glb_dst.Clear()
'                glb_adp.Fill(glb_dst, TblAlias)
'            End Using
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:Getdataset:" & ex.Message)
'        Finally

'            glb_adp = Nothing
'            glb_com = Nothing
'        End Try
'        Return glb_dst
'    End Function
'    Public Function GetdataList(ByVal Sqlquery As String, ByVal glb_dst As DataList) As DataList
'        Try
'            Dim glb_com As New SqlClient.SqlCommand()

'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                glb_com.CommandText = Sqlquery
'                glb_com.CommandType = CommandType.Text
'                glb_com.Connection = connection
'                glb_dst.DataSource = glb_com.ExecuteReader()
'                glb_dst.DataBind()
'            End Using
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:GetdataList:" & ex.Message)
'        Finally

'        End Try
'    End Function
'    Public Function GetdatasetSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, ByVal TblAlias As String) As Data.DataSet
'        Dim sdr As DataSet = Nothing
'        Dim com As New SqlCommand
'        Dim SpAdepter As New SqlDataAdapter
'        Try
'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                com.CommandText = SPName
'                com.CommandType = CommandType.StoredProcedure
'                com.Connection = connection
'                com.CommandTimeout = 600
'                com.Parameters.Clear()

'                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
'                    Dim parameter As New SqlClient.SqlParameter
'                    parameter.ParameterName = oClsSqlParameter.ParaName
'                    parameter.SqlDbType = oClsSqlParameter.ParaType
'                    parameter.Value = oClsSqlParameter.ParaValue
'                    parameter.Direction = oClsSqlParameter.ParaDirection
'                    com.Parameters.Add(parameter)
'                Next
'                SpAdepter = New SqlDataAdapter(com)
'                sdr = New Data.DataSet
'                SpAdepter.Fill(sdr)
'                Return sdr
'            End Using
'        Catch ex As Exception : Throw ex
'            LogHelper.Error("ClsDataccess:GetdatasetSp:" & ex.Message)
'        Finally
'            com.Parameters.Clear()
'            com = Nothing
'            SpAdepter = Nothing
'        End Try
'        Return sdr
'    End Function
'    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataTable
'        Dim sdr As DataTable = Nothing
'        Dim com As New SqlCommand
'        Dim SpAdepter As New SqlDataAdapter
'        Try
'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                com.CommandText = SPName
'                com.CommandType = CommandType.StoredProcedure
'                com.Connection = connection
'                com.Parameters.Clear()
'                com.CommandTimeout = 0

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
'            LogHelper.Error("ClsDataccess:GetdataTableSp:" & ex.Message)
'        Finally
'            com.Parameters.Clear()
'            com = Nothing
'            SpAdepter = Nothing
'        End Try
'        Return sdr
'    End Function
'    Public Function GetdataView(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataView
'        Dim sdr As DataTable = Nothing
'        Dim sdr1 As DataView = Nothing
'        Dim com As New SqlCommand
'        Dim SpAdepter As New SqlDataAdapter
'        Try
'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                com.CommandText = SPName
'                com.CommandType = CommandType.StoredProcedure
'                com.Connection = connection
'                com.Parameters.Clear()
'                com.CommandTimeout = 0
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
'                sdr1 = sdr.DefaultView
'                If strTableName <> "" Then sdr.TableName = strTableName
'            End Using
'            Return sdr1
'        Catch ex As Exception : Throw ex
'            LogHelper.Error("ClsDataccess:GetdataView:" & ex.Message)
'        Finally
'            com.Parameters.Clear()

'        End Try
'        Return sdr1
'    End Function
'    Public Function getparentcategory(ByVal categoryid As Integer) As String
'        Try
'            Dim paramarr As New ArrayList
'            paramarr.Add(New Object() {"@category_string", SqlDbType.VarChar, "", ParameterDirection.InputOutput})
'            paramarr.Add(New Object() {"@category_id", SqlDbType.Int, categoryid, ParameterDirection.Input})
'            Return getstring_fromprocedure("Sp_Get_Parent_Category", paramarr)
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:getparentcategory:" & ex.Message)
'        End Try
'    End Function
'    Public Function getstring_fromprocedure(ByVal SPName As String, ByVal SPParameter As ArrayList) As String
'        Dim output_value As String = String.Empty
'        Dim glb_com As New SqlClient.SqlCommand()
'        Try

'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()
'                glb_com.CommandText = SPName
'                glb_com.CommandType = CommandType.StoredProcedure
'                glb_com.Connection = connection
'                glb_com.Parameters.Clear()
'                Dim param As Object
'                For Each param In SPParameter
'                    Dim parameter As New SqlClient.SqlParameter()
'                    parameter.ParameterName = CType(param(0), String)
'                    parameter.SqlDbType = CType(param(1), SqlDbType)
'                    parameter.Value = param(2)
'                    parameter.Direction = param(3)
'                    If parameter.SqlDbType = SqlDbType.VarChar And parameter.Direction = ParameterDirection.InputOutput Then
'                        parameter.Size = 150
'                    End If
'                    glb_com.Parameters.Add(parameter)
'                Next
'                glb_com.ExecuteScalar()
'            End Using
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:getstring_fromprocedure:" & ex.Message)
'        Finally

'        End Try
'        Return glb_com.Parameters(0).Value
'    End Function
'    Public Function gettable_fromprocedure(ByVal SPName As String, Optional ByVal SPParameter As ArrayList = Nothing) As DataTable
'        Dim output_value As String = String.Empty
'        Dim glb_dt As New DataTable
'        Dim glb_com As New SqlClient.SqlCommand()
'        Try
'            Using connection As New SqlConnection(DBManager.get_first_ConnString)
'                connection.Open()

'                glb_com.CommandText = SPName
'                glb_com.CommandType = CommandType.StoredProcedure
'                glb_com.Connection = connection
'                glb_com.Parameters.Clear()
'                Dim param As Object
'                If Not SPParameter Is Nothing Then
'                    For Each param In SPParameter
'                        Dim parameter As New SqlClient.SqlParameter()
'                        parameter.ParameterName = CType(param(0), String)
'                        parameter.SqlDbType = CType(param(1), SqlDbType)
'                        parameter.Value = param(2)
'                        parameter.Direction = param(3)
'                        If parameter.SqlDbType = SqlDbType.VarChar And parameter.Direction = ParameterDirection.InputOutput Then
'                            parameter.Size = 150
'                        End If
'                        glb_com.Parameters.Add(parameter)
'                    Next
'                End If
'                glb_dt.Load(glb_com.ExecuteReader())
'            End Using
'            Return glb_dt
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:gettable_fromprocedure:" & ex.Message)
'        Finally

'        End Try
'        Return glb_dt
'    End Function

'    Public Function GetdatareaderSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal sqlconn As SqlConnection = Nothing) As SqlDataReader
'        Dim sdr As SqlDataReader = Nothing
'        Dim com As New SqlCommand()
'        Try
'            com.CommandText = SPName
'            com.CommandType = CommandType.StoredProcedure
'            If Not sqlconn Is Nothing Then
'                com.Connection = sqlconn
'            Else
'            End If
'            com.Parameters.Clear()
'            For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
'                Dim parameter As New SqlClient.SqlParameter()
'                parameter.ParameterName = oClsSqlParameter.ParaName
'                parameter.SqlDbType = oClsSqlParameter.ParaType
'                parameter.Value = oClsSqlParameter.ParaValue
'                parameter.Direction = oClsSqlParameter.ParaDirection
'                com.Parameters.Add(parameter)
'            Next
'            sdr = com.ExecuteReader()
'        Catch ex As Exception
'            LogHelper.Error("ClsDataccess:GetdatareaderSp:" & ex.Message)
'        Finally
'            com.Parameters.Clear()
'        End Try
'        Return sdr

'    End Function

'End Class


Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI
Imports System.IO
Imports System
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Configuration

Public Class Common
    Dim conn As DBConnection = New DBConnection()

    Public Function GetBookingSettings(ByVal StoreId As Integer) As DataRow
        Dim strQvuery As String = "SELECT BS.* FROM bookingsettings BS WHERE StoreID = " & StoreId.ToString()
        Return conn.SingleRow(strQvuery)
    End Function

    Public Function CreateBooking(ByVal BookingType As Integer, ByVal covers As Integer, ByVal comment As String, ByVal Roomid As Integer, ByVal arrivaldate As DateTime, ByVal departuredate As DateTime, ByVal checkedin As Integer, ByVal bookingref As String, ByVal bookingtotal As Integer, ByVal deposit As Decimal, ByVal accountid As Integer, ByVal period As Integer, ByVal bookingtime As TimeSpan, ByVal BookingSettingsid As Integer, ByVal IsCanceled As Boolean, ByVal CreatedBy As Integer, ByVal IPAddress As String, ByVal GrandTotal As Decimal, ByVal BookingScheduleId As System.Nullable(Of Integer), ByVal BookingScheduleDateId As System.Nullable(Of Integer), ByVal IsOnline As Boolean, ByVal OurStoreId As Int32) As String
        '1 - Room & 2 - Table
        Dim strQvuery As String = ""
        strQvuery &= " "
        If BookingType = 1 Then
            strQvuery &= " DECLARE @Arrival DATETIME"
            strQvuery &= " DECLARE @Departure DATETIME"
            strQvuery &= " DECLARE @Date DATETIME"

            strQvuery &= " SET @Arrival = '" & arrivaldate.ToString("s") & "'"
            strQvuery &= " SET @Departure = '" & departuredate.ToString("s") & "'"

            strQvuery &= " DECLARE @BookingRef NVARCHAR(MAX)"
            strQvuery &= " SET @BookingRef = '" & bookingref & "'"

            strQvuery &= " DECLARE @i INT"
            strQvuery &= " SET @i = 0"
            strQvuery &= " DECLARE @NoOfNights INT"
            strQvuery &= " SET @NoOfNights = DATEDIFF(DAY,@Arrival,@Departure)"

            strQvuery &= " WHILE(@NoOfNights > 0) "
            strQvuery &= " BEGIN "
            strQvuery &= " SET @Date = DATEADD(DAY,@i,@Arrival) "

            strQvuery &= " INSERT INTO [bookings]"
            strQvuery &= " ([covers]"
            strQvuery &= " ,[date]"
            strQvuery &= " ,[comment]"
            strQvuery &= " ,[Roomid]"
            strQvuery &= " ,[arrivaldate]"
            strQvuery &= " ,[departuredate]"
            strQvuery &= " ,[checkedin]"
            strQvuery &= " ,[bookingref]"
            strQvuery &= " ,[bookingtotal]"
            strQvuery &= " ,[deposit]"
            strQvuery &= " ,[accountid]"
            strQvuery &= " ,[period]"
            strQvuery &= " ,[bookingtime]"
            strQvuery &= " ,[BookingSettingsid]"
            strQvuery &= " ,[IsCanceled]"
            strQvuery &= " ,[CreatedDate]"
            strQvuery &= " ,[CreatedBy]"
            strQvuery &= " ,[IPAddress]"
            strQvuery &= " ,[GrandTotal]"
            strQvuery &= " ,[BookingScheduleID]"
            strQvuery &= " ,[IsOnline]"
            strQvuery &= " ,[OurStoreId])"

            strQvuery &= " VALUES"
            strQvuery &= " ('" & covers & "'"
            strQvuery &= " ,@Date"
            strQvuery &= " ,'" & comment & "'"
            strQvuery &= " ,'" & Roomid & "'"
            strQvuery &= " ,@Arrival"
            strQvuery &= " ,@Departure"
            strQvuery &= " ,'" & checkedin & "'"
            strQvuery &= " ,@bookingref"
            strQvuery &= " ,'" & bookingtotal & "'"
            strQvuery &= " ,'" & deposit & "'"
            strQvuery &= " ,'" & accountid & "'"
            strQvuery &= " ,NULL"
            strQvuery &= " ,'" & bookingtime.ToString() & "'"
            strQvuery &= " ,'" & BookingSettingsid & "'"
            strQvuery &= " ,'" & IsCanceled & "'"
            strQvuery &= " ,GETDATE()"
            strQvuery &= " ,'" & CreatedBy & "'"
            strQvuery &= " ,'" & IPAddress & "'"
            strQvuery &= " ," & GrandTotal & ""
            strQvuery &= " ," & BookingScheduleId & ""
            strQvuery &= " ,'" & IsOnline & "'"
            strQvuery &= " ," & OurStoreId & ")"
            strQvuery &= " SET @i = @i + 1"
            strQvuery &= " SET @NoOfNights = @NoOfNights - 1"
            strQvuery &= " END"
            strQvuery &= " SELECT @BookingRef"
        Else
            strQvuery &= " INSERT INTO [bookings]"
            strQvuery &= " ([covers]"
            strQvuery &= " ,[date]"
            strQvuery &= " ,[comment]"
            strQvuery &= " ,[Roomid]"
            strQvuery &= " ,[arrivaldate]"
            strQvuery &= " ,[departuredate]"
            strQvuery &= " ,[checkedin]"
            strQvuery &= " ,[bookingref]"
            strQvuery &= " ,[bookingtotal]"
            strQvuery &= " ,[deposit]"
            strQvuery &= " ,[accountid]"
            strQvuery &= " ,[period]"
            strQvuery &= " ,[bookingtime]"
            strQvuery &= " ,[BookingSettingsid]"
            strQvuery &= " ,[IsCanceled]"
            strQvuery &= " ,[CreatedDate]"
            strQvuery &= " ,[CreatedBy]"
            strQvuery &= " ,[IPAddress]"
            strQvuery &= " ,[GrandTotal]"
            strQvuery &= " ,[BookingScheduleID]"
            strQvuery &= " ,[BookingScheduleDateId]"
            strQvuery &= " ,[IsOnline]"
            strQvuery &= " ,[OurStoreId])"

            strQvuery &= " VALUES"
            strQvuery &= " (" & covers
            strQvuery &= " ,'" & arrivaldate.ToString("s") & "'"
            strQvuery &= " ,'" & comment & "'"
            strQvuery &= " ," & Roomid
            strQvuery &= " ,NULL"
            strQvuery &= " ,NULL"
            strQvuery &= " ," & checkedin
            strQvuery &= " ,'" & bookingref & "'"
            strQvuery &= " ," & bookingtotal
            strQvuery &= " ," & deposit
            strQvuery &= " ," & accountid
            strQvuery &= " ,1"
            strQvuery &= " ,'" & bookingtime.ToString() & "'"
            strQvuery &= " ," & BookingSettingsid
            strQvuery &= " ,'" & IsCanceled & "'"
            strQvuery &= " ,GETDATE()"
            strQvuery &= " ," & CreatedBy
            strQvuery &= " ,'" & IPAddress & "'"
            strQvuery &= " ," & GrandTotal & ""
            strQvuery &= " ," & BookingScheduleId & ""
            strQvuery &= " ," & BookingScheduleDateId
            strQvuery &= " ,'" & IsOnline & "'"
            strQvuery &= " ," & OurStoreId & ")"

            strQvuery &= " SELECT '" & bookingref & "'"
        End If

        Return conn.SingleCell(strQvuery)

    End Function

    Public Function CreateBookingInitiate(ByVal BookingType As Integer, ByVal covers As Integer, ByVal comment As String, ByVal Roomid As Integer, ByVal arrivaldate As DateTime, ByVal departuredate As DateTime, ByVal checkedin As Integer, ByVal bookingref As String, ByVal bookingtotal As Integer, ByVal deposit As Decimal, ByVal accountid As Integer, ByVal period As Integer, ByVal bookingtime As TimeSpan, ByVal BookingSettingsid As Integer, ByVal IsCanceled As Boolean, ByVal CreatedBy As Integer, ByVal IPAddress As String, ByVal GrandTotal As Decimal, ByVal BookingScheduleId As System.Nullable(Of Integer), ByVal BookingScheduleDateId As System.Nullable(Of Integer), ByVal IsOnline As Boolean, ByVal OurStoreId As Int32) As String
        '1 - Room & 2 - Table
        Dim strQvuery As String = ""
        strQvuery &= " "
        If BookingType = 1 Then
            strQvuery &= " DECLARE @Arrival DATETIME"
            strQvuery &= " DECLARE @Departure DATETIME"
            strQvuery &= " DECLARE @Date DATETIME"

            strQvuery &= " SET @Arrival = '" & arrivaldate.ToString("s") & "'"
            strQvuery &= " SET @Departure = '" & departuredate.ToString("s") & "'"

            strQvuery &= " DECLARE @BookingRef NVARCHAR(MAX)"
            strQvuery &= " SET @BookingRef = '" & bookingref & "'"

            strQvuery &= " DECLARE @i INT"
            strQvuery &= " SET @i = 0"
            strQvuery &= " DECLARE @NoOfNights INT"
            strQvuery &= " SET @NoOfNights = DATEDIFF(DAY,@Arrival,@Departure)"

            strQvuery &= " WHILE(@NoOfNights > 0) "
            strQvuery &= " BEGIN "
            strQvuery &= " SET @Date = DATEADD(DAY,@i,@Arrival) "

            strQvuery &= " INSERT INTO [Booking_Initiate]"
            strQvuery &= " ([covers]"
            strQvuery &= " ,[date]"
            strQvuery &= " ,[comment]"
            strQvuery &= " ,[Roomid]"
            strQvuery &= " ,[arrivaldate]"
            strQvuery &= " ,[departuredate]"
            strQvuery &= " ,[checkedin]"
            strQvuery &= " ,[bookingref]"
            strQvuery &= " ,[bookingtotal]"
            strQvuery &= " ,[deposit]"
            strQvuery &= " ,[accountid]"
            strQvuery &= " ,[period]"
            strQvuery &= " ,[bookingtime]"
            strQvuery &= " ,[BookingSettingsid]"
            strQvuery &= " ,[IsCanceled]"
            strQvuery &= " ,[CreatedDate]"
            strQvuery &= " ,[CreatedBy]"
            strQvuery &= " ,[IPAddress]"
            strQvuery &= " ,[GrandTotal]"
            strQvuery &= " ,[BookingScheduleID]"
            'strQvuery &= " ,[BookingScheduleDateId]"
            strQvuery &= " ,[IsOnline]"
            strQvuery &= " ,[OurStoreId])"

            strQvuery &= " VALUES"
            strQvuery &= " ('" & covers & "'"
            strQvuery &= " ,@Date"
            strQvuery &= " ,'" & comment & "'"
            strQvuery &= " ,'" & Roomid & "'"
            strQvuery &= " ,@Arrival"
            strQvuery &= " ,@Departure"
            strQvuery &= " ,'" & checkedin & "'"
            strQvuery &= " ,@bookingref"
            strQvuery &= " ,'" & bookingtotal & "'"
            strQvuery &= " ,'" & deposit & "'"
            strQvuery &= " ,'" & accountid & "'"
            strQvuery &= " ,NULL"
            strQvuery &= " ,'" & bookingtime.ToString() & "'"
            strQvuery &= " ,'" & BookingSettingsid & "'"
            strQvuery &= " ,'" & IsCanceled & "'"
            strQvuery &= " ,GETDATE()"
            strQvuery &= " ,'" & CreatedBy & "'"
            strQvuery &= " ,'" & IPAddress & "'"
            strQvuery &= " ," & GrandTotal & ""
            strQvuery &= " ," & BookingScheduleId & ""
            'strQvuery &= " ," & BookingScheduleDateId
            strQvuery &= " ,'" & IsOnline & "'"
            strQvuery &= " ," & OurStoreId & ")"
            strQvuery &= " SET @i = @i + 1"
            strQvuery &= " SET @NoOfNights = @NoOfNights - 1"
            strQvuery &= " END"
            strQvuery &= " SELECT @BookingRef"
        Else
            strQvuery &= " INSERT INTO [Booking_Initiate]"
            strQvuery &= " ([covers]"
            strQvuery &= " ,[date]"
            strQvuery &= " ,[comment]"
            strQvuery &= " ,[Roomid]"
            strQvuery &= " ,[arrivaldate]"
            strQvuery &= " ,[departuredate]"
            strQvuery &= " ,[checkedin]"
            strQvuery &= " ,[bookingref]"
            strQvuery &= " ,[bookingtotal]"
            strQvuery &= " ,[deposit]"
            strQvuery &= " ,[accountid]"
            strQvuery &= " ,[period]"
            strQvuery &= " ,[bookingtime]"
            strQvuery &= " ,[BookingSettingsid]"
            strQvuery &= " ,[IsCanceled]"
            strQvuery &= " ,[CreatedDate]"
            strQvuery &= " ,[CreatedBy]"
            strQvuery &= " ,[IPAddress]"
            strQvuery &= " ,[GrandTotal]"
            strQvuery &= " ,[BookingScheduleID]"
            strQvuery &= " ,[BookingScheduleDateId]"
            strQvuery &= " ,[IsOnline]"
            strQvuery &= " ,[OurStoreId])"

            strQvuery &= " VALUES"
            strQvuery &= " (" & covers
            strQvuery &= " ,'" & arrivaldate.ToString("s") & "'"
            strQvuery &= " ,'" & comment & "'"
            strQvuery &= " ," & Roomid
            strQvuery &= " ,NULL"
            strQvuery &= " ,NULL"
            strQvuery &= " ," & checkedin
            strQvuery &= " ,'" & bookingref & "'"
            strQvuery &= " ," & bookingtotal
            strQvuery &= " ," & deposit
            strQvuery &= " ," & accountid
            strQvuery &= " ,1"
            strQvuery &= " ,'" & bookingtime.ToString() & "'"
            strQvuery &= " ," & BookingSettingsid
            strQvuery &= " ,'" & IsCanceled & "'"
            strQvuery &= " ,GETDATE()"
            strQvuery &= " ," & CreatedBy
            strQvuery &= " ,'" & IPAddress & "'"
            strQvuery &= " ," & GrandTotal & ""
            strQvuery &= " ," & BookingScheduleId & ""
            strQvuery &= " ," & BookingScheduleDateId
            strQvuery &= " ,'" & IsOnline & "'"
            strQvuery &= " ," & OurStoreId & ")"

            strQvuery &= " SELECT '" & bookingref & "'"
        End If

        Return conn.SingleCell(strQvuery)

    End Function


    Public Shared Sub SaveBookingServices(ByVal dt As DataTable)
        Dim connStr As String
        connStr = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")

        connStr = connStr & ";" & "Min pool size=" & ConfigurationManager.AppSettings("Min_Pool_Size")


        connStr = connStr & ";" & "Max pool size=" & ConfigurationManager.AppSettings("Max_Pool_Size")


        connStr = connStr & ";" & "Connect Timeout=" & ConfigurationManager.AppSettings("Connect_Timeout")

        Using bulkCopy As New SqlBulkCopy(connStr, SqlBulkCopyOptions.TableLock)
            bulkCopy.DestinationTableName = "BookingServices"
            bulkCopy.NotifyAfter = 1000
            bulkCopy.WriteToServer(dt)
        End Using

    End Sub

    Public Function SaveAccount(ByVal accountId As Integer, ByVal Parentid As Integer, ByVal FirstName As String, ByVal Lastname As String, ByVal mobile As String, ByVal phonework As String, ByVal email1st As String, ByVal street_1 As String, ByVal street_2 As String, ByVal Street_3 As String, ByVal city As String, ByVal Pcode As String, ByVal insertfield As String, ByVal insertval As String, ByVal updateVal As String) As Integer

        Dim strQvuery As String = ""

        'First Add/Update Address Details 
        If (accountId = 0) Then
            strQvuery &= " if not exists (select 1 from address where email1st = '" + email1st + "' )  Begin"
            strQvuery &= " INSERT INTO address ("
            strQvuery &= " [dateupdated]"
            strQvuery &= " ,[mobile]"
            strQvuery &= " ,[phonework]"
            strQvuery &= " ,[email1st]"
            strQvuery &= " ,[street_1]"
            strQvuery &= " ,[street_2]"
            strQvuery &= " ,[Street_3]"
            strQvuery &= " ,[city]"
            strQvuery &= " ,[Pcode])"
            strQvuery &= " values ("
            strQvuery &= " Getdate()"
            strQvuery &= " ,'" & mobile & "'"
            strQvuery &= " ,'" & phonework & "'"
            strQvuery &= " ,'" & email1st & "'"
            strQvuery &= " ,'" & street_1 & "'"
            strQvuery &= " ,'" & street_2 & "'"
            strQvuery &= " ,'" & Street_3 & "'"
            strQvuery &= " ,'" & city & "'"
            strQvuery &= " ,'" & Pcode & "')"
            strQvuery &= " SELECT CAST(SCOPE_IDENTITY() AS INT)"
            strQvuery &= " End Else Begin   "
            strQvuery &= " Update address Set "
            strQvuery &= " [dateupdated] = Getdate()"
            strQvuery &= " ,[mobile] = '" & mobile & "'"
            strQvuery &= " ,[phonework] = '" & phonework & "'"
            strQvuery &= " ,[email1st] = '" & email1st & "'"
            strQvuery &= " ,[street_1] = '" & street_1 & "'"
            strQvuery &= " ,[street_2] = '" & street_2 & "'"
            strQvuery &= " ,[Street_3] = '" & Street_3 & "'"
            strQvuery &= " ,[city] = '" & city & "'"
            strQvuery &= " ,[Pcode] = '" & Pcode & "'"
            strQvuery &= " WHERE AddressID = (SELECT AddressID From Account where AccountId = " & accountId.ToString & ")"
            strQvuery &= " SELECT (SELECT AddressID From Account where AccountId = " & accountId.ToString & ")"
            strQvuery &= " end "
        Else
            strQvuery &= " Update address Set "
            strQvuery &= " [dateupdated] = Getdate()"
            strQvuery &= " ,[mobile] = '" & mobile & "'"
            strQvuery &= " ,[phonework] = '" & phonework & "'"
            strQvuery &= " ,[email1st] = '" & email1st & "'"
            strQvuery &= " ,[street_1] = '" & street_1 & "'"
            strQvuery &= " ,[street_2] = '" & street_2 & "'"
            strQvuery &= " ,[Street_3] = '" & Street_3 & "'"
            strQvuery &= " ,[city] = '" & city & "'"
            strQvuery &= " ,[Pcode] = '" & Pcode & "'"
            strQvuery &= " WHERE AddressID = (SELECT AddressID From Account where AccountId = " & accountId.ToString & ")"
            strQvuery &= " SELECT (SELECT AddressID From Account where AccountId = " & accountId.ToString & ")"
        End If

        Dim retAddressId As Integer = Convert.ToInt32(conn.ExecuteScalar(strQvuery))

        'Then Add/Update Account information
        If (accountId = 0) Then
            strQvuery = " Insert into Account ("
            strQvuery &= " [ParentID]"
            strQvuery &= " ,[FirstName]"
            strQvuery &= " ,[Lastname]"
            strQvuery &= " ,[AddressID]"
            strQvuery &= " ,[cmp_id]"
            strQvuery &= "" & insertfield & ""
            strQvuery &= ")"
            strQvuery &= " Values ("
            strQvuery &= " '" & Parentid.ToString() & "'"
            strQvuery &= " ,'" & FirstName & "'"
            strQvuery &= " ,'" & Lastname & "'"
            strQvuery &= " ,'" & retAddressId.ToString() & "','" & Val(HttpContext.Current.Session("cmp_id")) & "'" & insertval & ") SELECT CAST(SCOPE_IDENTITY() AS INT)"
        Else
            strQvuery = " Update Account SET"
            strQvuery &= " [FirstName] = '" & FirstName & "'"
            strQvuery &= " ,[Lastname] = '" & Lastname & "'"
            strQvuery &= " ,[cmp_id] = '" & Val(HttpContext.Current.Session("cmp_id")) & "'"
            strQvuery &= "" & updateVal & ""
            strQvuery &= " Where AccountId = " & accountId
            strQvuery &= " SELECT " & accountId
        End If

        Dim retAccountId As Integer = Convert.ToInt32(conn.ExecuteScalar(strQvuery))


        'Then Add CUSTOMER DETAIL information
        'If (accountId = 0 And retAccountId <> 0) Then
        '    strQvuery = " Insert into M_Customer ("
        '    strQvuery &= " [first_name]"
        '    strQvuery &= " ,[last_name]"
        '    strQvuery &= " ,[email]"
        '    strQvuery &= " ,[contact_no]"
        '    strQvuery &= ",[address]"
        '    strQvuery &= ",[postal_code]"
        '    strQvuery &= ",[created_date]"
        '    strQvuery &= ",[modify_date]"
        '    strQvuery &= ",[is_active]"
        '    strQvuery &= ",[ip_address]"
        '    strQvuery &= ",[AccountID]"
        '    strQvuery &= ")"
        '    strQvuery &= " Values ("
        '    strQvuery &= " '" & FirstName & "'"
        '    strQvuery &= " ,'" & Lastname & "'"
        '    strQvuery &= " ,'" & email1st & "'"
        '    strQvuery &= " ,'" & mobile & "'"
        '    strQvuery &= " ,'" & street_1 + street_2 + Street_3 & "'"
        '    strQvuery &= " ,'" & Pcode & "'"
        '    strQvuery &= " ,Getdate()"
        '    strQvuery &= " ,Getdate()"
        '    strQvuery &= ",1"
        '    strQvuery &= " ,'" & HttpContext.Current.Request.UserHostAddress.ToString() & "'"
        '    strQvuery &= " ,'" & retAccountId.ToString() & "' ) SELECT CAST(SCOPE_IDENTITY() AS INT)"
        'Else
        '    strQvuery = " Update M_Customer SET"
        '    strQvuery &= " [first_name] = '" & FirstName & "'"
        '    strQvuery &= " ,[last_name] = '" & Lastname & "'"
        '    'strQvuery &= "" & updateVal & ""
        '    strQvuery &= " Where customer_id = " & accountId
        '    strQvuery &= " SELECT " & accountId

        'End If

        'Dim customer_id As Integer = Convert.ToInt32(conn.ExecuteScalar(strQvuery))

        Return retAccountId

    End Function

    Public Function CheckDuplicateEmail(ByVal email As String, ByVal accountId As Integer) As Boolean
        Dim strQvuery As String = ""
        strQvuery &= "SELECT * FROM ACCOUNT ACC "
        strQvuery &= "INNER JOIN Address AD ON AD.AddressID = ACC.AddressID "
        strQvuery &= "WHERE AD.email1st = '" & email & "' AND status = 0 AND AccountId <> " & accountId.ToString()
        Return conn.CheckData(strQvuery)
    End Function

    Public Function CheckDuplicate(ByVal email As String, ByVal Mobile As String, ByVal SearchBy As Integer, ByVal accountId As Integer) As Boolean
        Try
            Dim SearchOption As String = ""
            If SearchBy = 0 Then
                SearchOption = "(email1st like '" & email & "')"
            ElseIf SearchBy = 1 Then
                SearchOption = "(Mobile like '" & Mobile & "')"
            ElseIf SearchBy = 2 Then
                SearchOption = "((email1st like '" & email & "') or (Mobile like '" & Mobile & "'))"
            End If
            Dim conn As DBConnection = New DBConnection()
            Dim que As String = ""
            que &= " SELECT firstname, lastname, mobile, email1st, accountid, account.addressid, pcode, street_1, Street_2, Street_3, City, phonework "
            que &= " FROM ACCOUNT , Address where account.addressid = Address.AddressID "
            que &= " and " + SearchOption.ToString + " AND status = 0  AND AccountId <> " & accountId.ToString()
            Return conn.CheckData(que)
        Catch ex As Exception

        End Try
        'Dim strQvuery As String = ""
        'strQvuery &= "SELECT * FROM ACCOUNT ACC "
        'strQvuery &= "INNER JOIN Address AD ON AD.AddressID = ACC.AddressID "
        'strQvuery &= "WHERE AD.email1st = '" & email & "' AND status = 0 AND AccountId <> " & accountId.ToString()
    End Function

    Public Function GetAccountById(ByVal accountId As Integer) As DataRow
        Dim que As String = ""
        que &= " SELECT *"
        que &= " FROM ACCOUNT , Address where account.addressid = Address.AddressID AND account.accountid = '" & accountId.ToString() & "'"
        Return conn.SingleRow(que)
    End Function

    Public Function GetAccountByIdDt(ByVal accountId As Integer) As DataTable
        Dim que As String = ""
        que &= " SELECT firstname, lastname, mobile, email1st, accountid, account.addressid, pcode, street_1, Street_2, Street_3, City, phonework "
        que &= " FROM ACCOUNT , Address where account.addressid = Address.AddressID AND account.accountid = '" & accountId.ToString() & "'"
        Return conn.SelectData(que).Tables(0)
    End Function

    Public Shared Sub BindVenue(ByRef ddl As RadDropDownList)
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT DISTINCT V.*"
        que &= " FROM Venue V INNER JOIN Store S ON S.VenueID = V.VenueID"
        que &= " INNER JOIN bookingsettings BS ON BS.StoreID = S.StoreID"
        que &= " WHERE BS.IsActive = 1"
        Dim ds As DataSet = conn.SelectData(que)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "VenueID"
        ddl.DataTextField = "Name"
        ddl.DataBind()
        ddl.ClearSelection()
    End Sub

    Public Shared Function SearchCustomer(ByVal searchParam As String) As DataSet

        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT firstname, lastname, mobile, email1st, accountid, account.addressid, pcode, street_1, Street_2, Street_3, City, phonework "
        que &= " FROM ACCOUNT , Address where account.addressid = Address.AddressID "
        que &= " and ((email1st like '%" & searchParam & "%') or (Mobile like '%" & searchParam & "%') or (firstname like '%" & searchParam & "%') or (lastname like '%" & searchParam & "%') or (firstname + ' ' + lastname like '%" & searchParam & "%')) AND status = 0 " '' Comment to search only by email

        '' que &= " and (Firstname like '" & searchParam & "' OR lastname like '" & searchParam & "' OR email1st like '" & searchParam & "' OR pcode like '" & searchParam & "' OR Firstname +' '+ lastname like '" & searchParam & "') AND status = 0 " '' Comment to search only by email
        'If (accountId = 0) Then
        '    que &= " and (Firstname like '" + txtSearchClient.Text.Trim() + "' OR lastname like '" + txtSearchClient.Text.Trim() + "' OR email1st like '" + txtSearchClient.Text.Trim() + "' OR pcode like '" + txtSearchClient.Text.Trim() + "') AND status = 0 "
        'Else
        '    que &= " and account.accountId = " + accountId.ToString()
        'End If

        Dim ds As DataSet = conn.SelectData(que)
        Return ds
    End Function

    Public Shared Function SearchCustomer(ByVal searchParam As String, ByVal SearchBy As Integer) As DataSet
        Try
            Dim SearchOption As String = ""
            If SearchBy = 0 Then
                SearchOption = "(email1st like '" & searchParam & "')"
            ElseIf SearchBy = 1 Then
                SearchOption = "(Mobile like '" & searchParam & "')"
            ElseIf SearchBy = 2 Then
                SearchOption = "((email1st like '" & searchParam & "') or (Mobile like '" & searchParam & "'))"
            End If
            Dim conn As DBConnection = New DBConnection()
            Dim que As String = ""
            que &= " SELECT firstname, lastname, mobile, email1st, accountid, account.addressid, pcode, street_1, Street_2, Street_3, City, phonework "
            que &= " FROM ACCOUNT , Address where account.addressid = Address.AddressID "
            que &= " and " + SearchOption.ToString + " AND status = 0 "
            Dim ds As DataSet = conn.SelectData(que)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetRoomNo(ByVal StoreId As Integer, ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal roomTypeId As Integer) As String

        Dim strRoomId As String = String.Empty
        Dim strQvuery As String

        strQvuery = " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='StoreRooms')DROP TABLE StoreRooms"
        strQvuery &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='bookedrooms')DROP TABLE bookedrooms"

        strQvuery &= " CREATE TABLE bookedrooms (roomid int null, bookingid int null, date smalldatetime null,storeid int null)"
        strQvuery &= " INSERT INTO bookedrooms (roomid, bookingid, date,storeid)"
        strQvuery &= " SELECT B.RoomID,B.bookingid,B.date,S.OurStoreId"
        strQvuery &= " FROM Bookings B"
        strQvuery &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID"
        strQvuery &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
        strQvuery &= " WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        strQvuery &= " AND (date BETWEEN '" & startDate.ToString("s") & "' AND '" & endDate.ToString("s") & "')"
        strQvuery &= " AND IsCanceled = 0 "

        strQvuery &= " CREATE TABLE StoreRooms(roomid int)"
        strQvuery &= " INSERT INTO StoreRooms(roomid)"
        strQvuery &= " SELECT DISTINCT P.ProductId"
        strQvuery &= " FROM ProdStore PS INNER JOIN Product P ON PS.ProductID = P.ProductID AND P.ParentID = '" & roomTypeId.ToString() & "' AND P.INACTIVE = 0"
        strQvuery &= " INNER JOIN Store S ON PS.StoreID = S.StoreID"
        strQvuery &= " left outer join StoreMaster SM ON SM.OtherStoreId = S.StoreID"
        strQvuery &= " INNER JOIN BookingSettings BS ON BS.StoreID = SM.OurStoreId"
        strQvuery &= " WHERE SM.OurStoreId = '" & StoreId.ToString() & "' AND P.ProdSort = BS.Sort AND BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        strQvuery &= " AND NOT EXISTS (SELECT 1 FROM bookedrooms BR WHERE BR.roomid = PS.ProductID AND BR.storeid = SM.OurStoreId)"

        strQvuery &= " SELECT DISTINCT roomid"
        strQvuery &= " FROM StoreRooms"

        strQvuery &= " DROP TABLE StoreRooms"
        strQvuery &= " drop table bookedrooms"

        Dim ds As DataSet = conn.SelectData(strQvuery)

        If ds.Tables.Count > 0 And ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows.Count > 1 Then
                Dim rowNo As Integer
                Dim rnd As Random = New Random()
                rowNo = rnd.Next(0, ds.Tables(0).Rows.Count - 1)
                strRoomId = ds.Tables(0).Rows(rowNo)("roomid").ToString()
            Else
                strRoomId = ds.Tables(0).Rows(0)("roomid").ToString()
            End If

        Else
            strRoomId = String.Empty
        End If
        Return strRoomId

    End Function

    Public Shared Function GetCustomerByEmail(ByVal email As String) As DataSet
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT firstname, lastname, mobile, email1st, accountid, account.addressid, pcode, street_1, Street_2, Street_3, City, phonework "
        que &= " FROM ACCOUNT , Address where account.addressid = Address.AddressID "
        que &= " and email1st = '" & email & "' AND status = 0 "
        Dim ds As DataSet = conn.SelectData(que)
        Return ds
    End Function

    Public Shared Function GetCustomerBySearchBy(ByVal email As String, ByVal Mobile As String, ByVal searchBy As Integer) As DataSet

        Dim SearchOption As String = ""
        If searchBy = 0 Then
            SearchOption = "(email1st like '" & email & "')"
        ElseIf searchBy = 1 Then
            SearchOption = "(Mobile like '" & Mobile & "')"
        ElseIf searchBy = 2 Then
            SearchOption = "((email1st like '" & email & "') or (Mobile like '" & Mobile & "'))"
        End If

        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT firstname, lastname, mobile, email1st, accountid, account.addressid, pcode, street_1, Street_2, Street_3, City, phonework "
        que &= " FROM ACCOUNT , Address where account.addressid = Address.AddressID "
        que &= " and " + SearchOption.ToString + " AND status = 0 "
        Dim ds As DataSet = conn.SelectData(que)
        Return ds
    End Function

    Public Function GetBookingDetailByRef(ByVal BookingRef As String) As DataRow
        Dim strQuery As String = String.Empty
        strQuery = " SELECT TOP 1 B.*,S.StoreName AS Name,A.FirstName,A.LastName,AD.Mobile,AD.Email1st,S.OurStoreId AS StoreID,A.AccountID,A.AccNumber,"
        strQuery &= " (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table "
        strQuery &= " where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables1"
        strQuery &= " FROM Bookings B"
        strQuery &= " INNER JOIN BookingSettings BS ON B.BookingSettingsid = BS.BookingSettingsid"
        strQuery &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
        strQuery &= " INNER JOIN Account A ON B.AccountID = A.AccountID"
        strQuery &= " INNER JOIN [Address] AD ON A.AddressId = AD.AddressID"
        strQuery &= " WHERE BookingRef = '" & BookingRef & "'"
        Return conn.SingleRow(strQuery)
    End Function

    Public Function GetBookingDetailByRefInitiate(ByVal BookingRef As String) As DataRow
        Dim strQuery As String = String.Empty
        strQuery = " SELECT TOP 1 B.*,S.StoreName AS Name,A.FirstName,A.LastName,AD.Mobile,AD.Email1st,S.OurStoreId AS StoreID,A.AccountID"
        strQuery &= " FROM Booking_Initiate B"
        strQuery &= " INNER JOIN BookingSettings BS ON B.BookingSettingsid = BS.BookingSettingsid"
        strQuery &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
        strQuery &= " INNER JOIN Account A ON B.AccountID = A.AccountID"
        strQuery &= " INNER JOIN [Address] AD ON A.AddressId = AD.AddressID"
        strQuery &= " WHERE BookingRef = '" & BookingRef & "'"
        Return conn.SingleRow(strQuery)
    End Function

    Public Function GetBookingServicesByRef(ByVal BookingRef As String) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT BS.*,P.Name"
        strQuery &= " FROM BookingServices BS "
        strQuery &= " INNER JOIN Product P ON BS.ProductID = P.ProductID"
        strQuery &= " WHERE BookingRef = '" & BookingRef & "'"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetRoomTypeName(ByVal RoomId As String) As String
        Dim strQuery As String = String.Empty
        strQuery = " Select Name"
        strQuery &= " FROM Product WHERE ProductId = (SELECT ParentID FROM Product WHERE ProductId = '" & RoomId & "')"
        Return conn.SingleCell(strQuery)
    End Function

    Public Shared Function SearchTableGetStore(ByVal bookingDate As String, ByVal bookingTime As String, ByVal venueId As String, ByVal BookingTypeID As String) As DataTable
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='RestaurantList')DROP TABLE RestaurantList"
        que &= " DECLARE @BookingDate DATETIME"
        que &= " SET @BookingDate = '" & bookingDate & "'"
        que &= " DECLARE @BookingTime TIME"
        que &= " SET @BookingTime = '" & bookingTime & "'"

        que &= " DECLARE @BookingScheduleID INT"
        que &= " SET @BookingScheduleID = '" & BookingTypeID & "'"

        que &= " CREATE TABLE RestaurantList(RestaurantID INT,TimeSlotId INT,StartTime Time,EndTime Time,SettingID INT,Schedule Nvarchar(50))"

        que &= " INSERT INTO RestaurantList (RestaurantID,TimeSlotId,StartTime,EndTime,SettingID,Schedule)"
        que &= " SELECT S.OurStoreId,1,BSD.StartTime,BSD.EndTime,BS.BookingSettingsid,BSC.Name"
        que &= " FROM BookingSettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
        que &= " INNER JOIN BookingSchedule BSC ON BSC.BookingSettingsID = BS.BookingSettingsid"
        que &= " INNER JOIN BookingScheduleDate BSD ON BSD.BookingScheduleId = BSC.BookingScheduleID"
        que &= " WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        que &= " AND (BSC.StartTime IS NOT NULL AND BSC.StartTime != '00:00:00.0000000' AND BSC.EndTime IS NOT NULL AND BSC.EndTime != '00:00:00.0000000')"
        que &= " AND BSC.BookingScheduleID = @BookingScheduleID AND BS.BookingSettingsid = '" & venueId & "'"
        que &= " AND BSD.ScheduleDate = @BookingDate"

        que &= " SELECT S.OurStoreId as StoreID,S.StoreName as Name,RL.TimeSlotId,RL.StartTime,RL.EndTime,RL.SettingID,RL.Schedule"
        que &= " FROM RestaurantList RL left outer join StoreMaster S ON RL.RestaurantID = S.OurStoreId"

        que &= " DROP TABLE RestaurantList"

        Dim ds As DataSet = conn.SelectData(que)
        If (ds.Tables.Count > 0) Then
            Return ds.Tables(0)
        End If
        Return Nothing

    End Function

    Public Shared Function SearchTableGetTimeSlots(ByVal bookingDate As String, ByVal settingsId As String, ByVal currentCover As String, ByVal BookingScheduleID As Int32, ByVal CurrBookingRefNo As String, ByVal IsOnline As Boolean) As DataTable
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='TimeList')DROP TABLE TimeList"
        que &= " DECLARE @BookingDate DATETIME"
        que &= " SET @BookingDate = '" & bookingDate & "'"
        que &= " DECLARE @SettingsID INT"
        que &= " SET @SettingsID = '" & settingsId & "'"
        que &= " DECLARE @CurrentCover INT"
        que &= " SET @CurrentCover = '" & currentCover & "'"
        que &= " DECLARE @BookingScheduleID INT"
        que &= " SET @BookingScheduleID = '" & BookingScheduleID & "'"

        que &= " DECLARE @BookingScheduleDateID INT"
        que &= " SET @BookingScheduleDateID = (SELECT BookingScheduleDateId FROM BookingScheduleDate WHERE BookingScheduleId = @BookingScheduleID AND ScheduleDate = @BookingDate)"

        que &= " CREATE TABLE TimeList(SlotTime Time,IsAvailable BIT,BookingScheduleID INT,BookingScheduleDateID INT)"

        que &= " DECLARE @SlotStartTime TIME"
        que &= " DECLARE @SlotEndTime TIME"
        que &= " DECLARE @TimeSpan INT"
        que &= " DECLARE @NumberOfCovers INT"
        que &= " DECLARE @ServiceDuration INT"
        que &= " DECLARE @FutureDuration INT"
        que &= " DECLARE @IsOnline BIT"
        que &= " SET @IsOnline = '" & IsOnline & "'"

        '---------
        que &= " declare @minCovers numeric(18,0)"
        que &= " set @minCovers = 0"
        que &= " declare @oneBookatAtime tinyint"
        que &= " set @oneBookatAtime = 0"
        '---------

        que &= " SELECT @SlotStartTime = BSD.StartTime,@SlotEndTime = BSD.EndTime,@TimeSpan = BSD.TimeSpan,@NumberOfCovers = BSD.NumberOfCover,@ServiceDuration = BSD.ServiceDuration,@FutureDuration = BS.FutureReservationTime ,@minCovers = MinCovers, @oneBookatAtime = One_booking_at_a_time"
        que &= " FROM BookingSchedule BS INNER JOIN BookingScheduleDate BSD ON BS.BookingScheduleID = BSD.BookingScheduleId"
        que &= " WHERE BS.BookingScheduleID = @BookingScheduleID AND BSD.ScheduleDate = @BookingDate"

        que &= " IF @IsOnline = 'False'"
        que &= " BEGIN"
        que &= " SET @FutureDuration = 0"
        que &= " End"

        que &= " SET @ServiceDuration = ISNULL(@ServiceDuration,0)"

        que &= " DECLARE @StartDateTime DATETIME"
        que &= " DECLARE @EndDateTime DATETIME"

        que &= " SET @StartDateTime = CONVERT(DATETIME, CONVERT(CHAR(8), @BookingDate, 112)   + ' ' + CONVERT(CHAR(8), @SlotStartTime, 108))"
        que &= " SET @EndDateTime = CONVERT(DATETIME, CONVERT(CHAR(8), @BookingDate, 112)  + ' ' + CONVERT(CHAR(8), @SlotEndTime, 108))"
        'Real
        'que &= " SET @StartDateTime = CAST(@BookingDate AS DATE)+CAST(@SlotStartTime AS DATETIME)"
        'que &= " SET @EndDateTime = CAST(@BookingDate AS DATE)+CAST(@SlotEndTime AS DATETIME)"
        'Change by zankar
        'que &= " SET @StartDateTime = CONVERT(VARCHAR(11),@BookingDate ,103)+CAST(@SlotStartTime AS DATETIME)"
        'que &= " SET @EndDateTime = CONVERT(VARCHAR(11),@BookingDate ,103)+CAST(@SlotEndTime AS DATETIME)"

        que &= " WHILE(@StartDateTime <= @EndDateTime)"
        que &= " BEGIN"
        que &= " DECLARE @SlotBookingsCover INT"

        que &= " SET @SlotBookingsCover = (SELECT ISNULL(SUM(covers),0) FROM bookings WHERE IsCanceled = 0 AND period = 1 AND  BookingSettingsid = @SettingsID AND bookingref != '" & CurrBookingRefNo & "'"
        que &= " AND (CONVERT(DATETIME, CONVERT(CHAR(8), @SlotStartTime, 108)) BETWEEN DATEADD(MINUTE,-(@ServiceDuration-1),(CONVERT(DATETIME, CONVERT(CHAR(8), [date], 112) + ' ' + CONVERT(CHAR(8), bookingtime, 108)))) AND DATEADD(MINUTE,@ServiceDuration-1,(CONVERT(DATETIME, CONVERT(CHAR(8), [date], 112) + ' ' + CONVERT(CHAR(8), bookingtime, 108))))))"
        'Real
        'que &= " SET @SlotBookingsCover = (SELECT ISNULL(SUM(covers),0) FROM bookings WHERE IsCanceled = 0 AND period = 1 AND  BookingSettingsid = @SettingsID AND bookingref != '" & CurrBookingRefNo & "' AND (@StartDateTime BETWEEN DATEADD(MINUTE,-(@ServiceDuration-1),(CAST([date] AS DATE)+CAST(bookingtime AS DATETIME))) AND DATEADD(MINUTE,@ServiceDuration-1,(CAST([date] AS DATE)+CAST(bookingtime AS DATETIME)))))"
        'Change by zankar
        'que &= " SET @SlotBookingsCover = (SELECT ISNULL(SUM(covers),0) FROM bookings WHERE IsCanceled = 0 AND period = 1 AND  BookingSettingsid = @SettingsID AND bookingref != '" & CurrBookingRefNo & "' AND (@StartDateTime BETWEEN DATEADD(MINUTE,-(@ServiceDuration-1),(CONVERT(DATETIME, CONVERT(CHAR(8), date, 112) + ' ' + CONVERT(CHAR(8), bookingtime, 108)))) AND DATEADD(MINUTE,@ServiceDuration-1,(CONVERT(DATETIME, CONVERT(CHAR(8), date, 112) + ' ' + CONVERT(CHAR(8), bookingtime, 108))))))"

        que &= " DECLARE @IsAvail BIT"
        que &= " SET @IsAvail = 1"
        que &= " IF((@SlotBookingsCover + @CurrentCover > @NumberOfCovers) OR (@StartDateTime <= DATEADD(MINUTE,@FutureDuration,GETDATE())) OR (@oneBookatAtime = 1 and @SlotBookingsCover > 0))"
        que &= " BEGIN"
        que &= " SET @IsAvail = 0"
        que &= " END"
        que &= " INSERT INTO TimeList (SlotTime,IsAvailable,BookingScheduleID,BookingScheduleDateID)"
        que &= " VALUES (CAST(@StartDateTime AS TIME), @IsAvail,@BookingScheduleID,@BookingScheduleDateID)"
        que &= " SET @StartDateTime = (SELECT DATEADD(MINUTE,@TimeSpan,@StartDateTime))"
        que &= " END"

        que &= " SELECT * FROM TimeList "
        que &= " DROP TABLE TimeList"

        Dim ds As DataSet = conn.SelectData(que)
        If (ds.Tables.Count > 0) Then
            Return ds.Tables(0)
        End If
        Return Nothing
    End Function

    Public Function GetBookingDetailByRefAndEmail(ByVal BookingRef As String, ByVal Email As String) As DataRow
        Dim strQuery As String = String.Empty
        strQuery = " SELECT TOP 1 B.*,S.Name,A.FirstName,A.LastName,AD.Mobile,AD.Email1st,S.StoreID,A.AccountID"
        strQuery &= " FROM Bookings B"
        strQuery &= " INNER JOIN BookingSettings BS ON B.BookingSettingsid = BS.BookingSettingsid"
        strQuery &= " INNER JOIN Store S ON BS.StoreID = S.StoreID"
        strQuery &= " INNER JOIN Account A ON B.AccountID = A.AccountID"
        strQuery &= " INNER JOIN [Address] AD ON A.AddressId = AD.AddressID"
        strQuery &= " WHERE BookingRef = '" & BookingRef & "' AND AD.Email1st = '" & Email & "'"
        Return conn.SingleRow(strQuery)
    End Function

    Public Function GetTableType(ByVal settingId As Int32, ByVal bookingDate As DateTime) As DataSet

        Dim strQuery As String = String.Empty
        'strQuery = " SELECT BookingScheduleID AS Id,Name FROM BookingSchedule WHERE BookingSettingsID = " & settingId
        strQuery &= " DECLARE @BookingSettingsID INT"
        strQuery &= " SET @BookingSettingsID = " & settingId
        strQuery &= " DECLARE @BookingDate DATETIME"
        strQuery &= " SET @BookingDate = '" & bookingDate.ToString("yyyy-MM-dd") & "'"
        strQuery &= " SELECT DISTINCT BS.BookingScheduleID AS Id,BS.Name FROM BookingSchedule BS"
        strQuery &= " INNER JOIN BookingScheduleDate BSD ON BS.BookingScheduleID = BSD.BookingScheduleId"
        strQuery &= " WHERE BS.BookingSettingsID = @BookingSettingsID AND (@BookingDate BETWEEN BS.StartDate AND BS.EndDate)"
        strQuery &= " AND BSD.ScheduleDate = @BookingDate AND BSD.IsAvailable = 1"
        Return conn.SelectData(strQuery)

    End Function

    Public Function GetTableTypeOnlyBySettingId(ByVal settingId As Int32) As DataSet
        Dim strQuery As String = String.Empty
        'strQuery = " SELECT BookingScheduleID AS Id,Name FROM BookingSchedule WHERE BookingSettingsID = " & settingId
        strQuery &= " DECLARE @BookingSettingsID INT"
        strQuery &= " SET @BookingSettingsID = " & settingId
        strQuery &= " SELECT DISTINCT BS.BookingScheduleID AS Id,BS.Name FROM BookingSchedule BS"
        strQuery &= " INNER JOIN BookingScheduleDate BSD ON BS.BookingScheduleID = BSD.BookingScheduleId"
        strQuery &= " WHERE BS.BookingSettingsID = @BookingSettingsID AND BS.EndDate >= convert(nvarchar(10),getdate(),120) "
        strQuery &= " AND BSD.IsAvailable = 1"

        Return conn.SelectData(strQuery)
    End Function

    Public Shared Function GetXmlData() As DataSet
        Dim ds As DataSet = New DataSet()

        ds.ReadXml(HttpContext.Current.Server.MapPath("~/XML/Settings.xml"))

        Return ds
    End Function

    Public Shared Sub SetXmlData(ByVal ds As DataSet)
        ds.WriteXml(HttpContext.Current.Server.MapPath("~/XML/Settings.xml"), XmlWriteMode.WriteSchema)
    End Sub

    Public Shared Sub BindRoomStoreDropdown(ByRef ddl As RadDropDownList, ByVal TabID As Int32)
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "') AND BS.HotelTabID = " & TabID
        Dim ds As DataSet = conn.SelectData(que)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "BookingSettingsid"
        ddl.DataTextField = "Name"
        ddl.DataBind()
        ddl.ClearSelection()
    End Sub

    Public Shared Sub BindRoomStoreDropdownForWidget(ByRef ddl As DropDownList)
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND BS.IsShowOnWidget = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "') "
        Dim ds As DataSet = conn.SelectData(que)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "BookingSettingsid"
        ddl.DataTextField = "Name"
        ddl.DataBind()
        ddl.ClearSelection()
    End Sub

    Public Shared Sub BindRoomStoreDropdownForWidgetRad(ByRef ddl As RadDropDownList)
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND BS.IsShowOnWidget = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "') "
        Dim ds As DataSet = conn.SelectData(que)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "BookingSettingsid"
        ddl.DataTextField = "Name"
        ddl.DataBind()
        ddl.ClearSelection()
    End Sub

    Public Shared Sub BindTableStoreDropdown(ByRef ddl As RadDropDownList, ByVal TabId As Int32, ByVal IsOnline As Boolean)
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim que As String = ""


            If IsOnline Then
                que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer JOIN StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND BS.IsShowOnWidget = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
            Else
                que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer JOIN StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "') AND BS.RestTabID = " & TabId
            End If

            Dim ds As DataSet = conn.SelectData(que)
            ddl.DataSource = ds.Tables(0)
            ddl.DataValueField = "BookingSettingsid"
            ddl.DataTextField = "Name"
            ddl.DataBind()
            ddl.ClearSelection()
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub BindTableStoreDropdownAll(ByRef ddl As RadDropDownList, ByVal TabId As Int32, ByVal IsOnline As Boolean)
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""


        If IsOnline Then
            que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND BS.IsShowOnWidget = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        Else
            que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "') AND BS.RestTabID = " & TabId
        End If

        Dim ds As DataSet = conn.SelectData(que)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "BookingSettingsid"
        ddl.DataTextField = "Name"
        ddl.DataBind()

        Dim li As Telerik.Web.UI.DropDownListItem = New Telerik.Web.UI.DropDownListItem("ALL", "0")
        ddl.Items.Insert(0, li)
        ddl.ClearSelection()
        If ds.Tables(0).Rows.Count > 0 Then
            ddl.SelectedIndex = 1
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub
    'vighnesh(22-04-2016)
    Public Shared Sub BindTableStoreDropdownAllNoTab(ByRef ddl As RadDropDownList, ByVal IsOnline As Boolean)
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""


        If IsOnline Then
            que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND BS.IsShowOnWidget = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        Else
            que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        End If

        Dim ds As DataSet = conn.SelectData(que)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "BookingSettingsid"
        ddl.DataTextField = "Name"
        ddl.DataBind()

        Dim li As Telerik.Web.UI.DropDownListItem = New Telerik.Web.UI.DropDownListItem("ALL", "0")
        ddl.Items.Insert(0, li)
        ddl.ClearSelection()
        If ds.Tables(0).Rows.Count > 0 Then
            ddl.SelectedIndex = 1
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub

    Public Shared Sub BindTableStoreDropdown(ByRef ddl As DropDownList)
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND BS.IsShowOnWidget = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        Dim ds As DataSet = conn.SelectData(que)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "BookingSettingsid"
        ddl.DataTextField = "Name"
        ddl.DataBind()
        ddl.ClearSelection()
    End Sub

    Public Shared Sub BindRoomStoreDropdown(ByRef ddl As DropDownList)
        Dim conn As DBConnection = New DBConnection()
        Dim que As String = ""
        que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        Dim ds As DataSet = conn.SelectData(que)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "BookingSettingsid"
        ddl.DataTextField = "Name"
        ddl.DataBind()
        ddl.ClearSelection()
    End Sub

    Public Shared Function GetBookingSettingTypeCount(ByVal bookingType As Int32) As Int32
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM BookingSettings WHERE IsActive = 1 AND bookingtype = '" & Utils.Encrypt(bookingType.ToString) & "'"
        Dim objConn As DBConnection = New DBConnection()
        Dim ds As DataSet = objConn.SelectData(strQuery)
        Dim rtnValue As Int32 = 0

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0) IsNot Nothing Then
            rtnValue = ds.Tables(0).Rows.Count
        End If

        Return rtnValue
    End Function

    Public Function GetProducts(ByVal StoreId As Int32) As DataSet
        Dim str As String = ""

        str = String.Empty
        str &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='ProductList')DROP TABLE ProductList"
        str &= " CREATE TABLE ProductList(ProductID INT,ProductName VARCHAR(30),ParentID INT,ParentName VARCHAR(30),Price DECIMAL(10,2))"
        str &= " INSERT INTO ProductList(ProductID,ProductName,ParentID,ParentName,Price)"
        str &= " SELECT P.ProductID,P.Name,P1.ProductID,P1.Name,CAST(PS.Price1Eachsize_1*.01 as Decimal (10,2))"
        str &= " FROM ProdStore PS INNER JOIN Product P ON PS.ProductID = P.ProductID"
        str &= " INNER JOIN Product P1 ON P.ParentID = P1.ProductID"
        str &= " left outer join StoreMaster SM ON PS.StoreID = SM.OtherStoreId"
        str &= " WHERE P.ExportCode_4 = 'BOOKING' AND P.Inactive = 0 AND SM.OurStoreId = '" & StoreId.ToString() & "'"
        str &= " ORDER BY P1.ProductID"
        str &= " SELECT * FROM ProductList"
        str &= " DROP TABLE ProductList"

        Dim ds As DataSet = conn.SelectData(str)
        Return ds
    End Function

    'Public Shared Sub TodayOpenTables()
    '    Dim str As String = ""

    '    str = String.Empty
    '    str &= " Insert into tables (tableNumber, venueid, name, tablegroup, printed, training, Datetimeopened, covers, linkedaccountID, tablestate)"
    '    'str &= " Select bookingref, (SELECT VenueID FROM Store WHERE StoreID = (SELECT StoreID FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid)), Firstname + ' ' + Lastname as name,(SELECT TableGroup FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid),0,0,getdate(), covers, B.Accountid,1"
    '    str &= " Select bookingref, (select v.OtherVenueId from StoreMaster s left outer join VenueMaster v on v.BookingVenueId = s.BookingVenueId where OurStoreId = (SELECT StoreID FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid)), Firstname + ' ' + Lastname as name,(SELECT TableGroup FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid),0,0,getdate(), covers, B.Accountid,1" 'SELECT VenueID FROM Store WHERE StoreID = 
    '    str &= " from bookings B, account"
    '    str &= " where B.accountid = account.accountid and CAST(B.date AS DATE) = CONVERT(date, getdate()) and Checkedin <> 1 AND ISNULL(period,0) = 1 AND B.IsCanceled = 0 "
    '    str &= " Update Bookings Set Checkedin = 1 where CAST(date AS DATE) = CONVERT(date, getdate()) AND ISNULL(period,0) = 1 AND checkedin <> 1"

    '    Dim objConnection As DBConnection = New DBConnection()
    '    objConnection.Ins_Upd_Del(str)

    'End Sub

    'Public Shared Sub TodayOpenTablesByBookingScheduleID(ByVal BId As Int32)
    '    Dim str As String = ""

    '    str = String.Empty
    '    str &= " Insert into tables (tableNumber, venueid, name, tablegroup, printed, training, Datetimeopened, covers, linkedaccountID, tablestate)"
    '    str &= " Select bookingref, (select v.OtherVenueId from StoreMaster s left outer join VenueMaster v on v.BookingVenueId = s.BookingVenueId where OurStoreId = (SELECT StoreID FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid)), Firstname + ' ' + Lastname as name,(SELECT TableGroup FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid),0,0,getdate(), covers, B.Accountid,1" 'SELECT VenueID FROM Store WHERE StoreID = 
    '    str &= " from bookings B, account"
    '    str &= " where B.accountid = account.accountid and CAST(B.date AS DATE) = CONVERT(date, getdate()) and Checkedin <> 1 AND ISNULL(period,0) = 1 AND B.IsCanceled = 0 AND B.BookingScheduleID = " + BId.ToString() + ""
    '    str &= " Update Bookings Set Checkedin = 1 where CAST(date AS DATE) = CONVERT(date, getdate()) AND ISNULL(period,0) = 1 AND BookingScheduleID = " + BId.ToString() + " AND checkedin <> 1"

    '    Dim objConnection As DBConnection = New DBConnection()
    '    objConnection.Ins_Upd_Del(str)

    'End Sub

    'Public Shared Sub TodayOpenTablesByBookingSettingsID(ByVal BId As Int32)
    '    Dim str As String = ""

    '    str = String.Empty
    '    str &= " Insert into tables (tableNumber, venueid, name, tablegroup, printed, training, Datetimeopened, covers, linkedaccountID, tablestate)"
    '    str &= " Select bookingref, (select v.OtherVenueId from StoreMaster s left outer join VenueMaster v on v.BookingVenueId = s.BookingVenueId where OurStoreId = (SELECT StoreID FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid)), Firstname + ' ' + Lastname as name,(SELECT TableGroup FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid),0,0,getdate(), covers, B.Accountid,1" 'SELECT VenueID FROM Store WHERE StoreID = 
    '    str &= " from bookings B, account"
    '    str &= " where B.accountid = account.accountid and CAST(B.date AS DATE) = CONVERT(date, getdate()) and Checkedin <> 1 AND ISNULL(period,0) = 1 AND B.IsCanceled = 0 AND B.BookingScheduleID in (Select BookingScheduleID from BookingSchedule A where A.BookingSettingsID = " + BId.ToString() + ")"
    '    str &= " Update Bookings Set Checkedin = 1 where CAST(date AS DATE) = CONVERT(date, getdate()) AND ISNULL(period,0) = 1 AND BookingScheduleID in (Select BookingScheduleID from BookingSchedule A where A.BookingSettingsID = " + BId.ToString() + ") AND checkedin <> 1"

    '    Dim objConnection As DBConnection = New DBConnection()
    '    objConnection.Ins_Upd_Del(str)

    'End Sub

    'Public Shared Sub TodayOpenTablesByBookingID(ByVal BookindId As Int32)
    '    Dim str As String = ""

    '    str = String.Empty
    '    str &= " Insert into tables (tableNumber, venueid, name, tablegroup, printed, training, Datetimeopened, covers, linkedaccountID, tablestate)"
    '    str &= " Select bookingref, (select v.OtherVenueId from StoreMaster s left outer join VenueMaster v on v.BookingVenueId = s.BookingVenueId where OurStoreId = (SELECT StoreID FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid)), Firstname + ' ' + Lastname as name,(SELECT TableGroup FROM bookingsettings BS WHERE BS.BookingSettingsid = B.BookingSettingsid),0,0,getdate(), covers, B.Accountid,1" 'SELECT VenueID FROM Store WHERE StoreID = 
    '    str &= " from bookings B, account"
    '    str &= " where B.accountid = account.accountid and CAST(B.date AS DATE) = CONVERT(date, getdate()) and Checkedin <> 1 AND ISNULL(period,0) = 1 AND B.bookingid = " + BookindId.ToString() + ""
    '    str &= " IF EXISTS (SELECT 1 FROM tables WHERE TAbleNumber = (select bookingref from bookings where bookingid = " + BookindId.ToString() + ")) BEGIN Update Bookings Set Checkedin = 1 where bookingid = " + BookindId.ToString() + " END"

    '    Dim objConnection As DBConnection = New DBConnection()
    '    objConnection.Ins_Upd_Del(str)

    'End Sub

    Public Shared Sub GenerateXMLwithDefaultData()

        If Not Directory.Exists(HttpContext.Current.Server.MapPath("~/XML")) Then
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/XML"))
        End If

        If Not File.Exists(HttpContext.Current.Server.MapPath("~/XML/Settings.xml")) Then
            File.Create(HttpContext.Current.Server.MapPath("~/XML/Settings.xml")).Dispose()

            Dim ds As DataSet = New DataSet()

            Dim dtTabName As DataTable = New DataTable("TabName")
            dtTabName.Columns.Add("Tabs", GetType(String))

            Dim drTabName As DataRow = dtTabName.NewRow()
            drTabName("Tabs") = "Search"
            dtTabName.Rows.Add(drTabName)

            drTabName = dtTabName.NewRow()
            drTabName("Tabs") = "Hotel"
            dtTabName.Rows.Add(drTabName)

            drTabName = dtTabName.NewRow()
            drTabName("Tabs") = "Restaurent"
            dtTabName.Rows.Add(drTabName)

            ds.Tables.Add(dtTabName)

            Dim dtCurrency As DataTable = New DataTable("Currency")
            dtCurrency.Columns.Add("Symbol", GetType(String))
            Dim drCurrency As DataRow = dtCurrency.NewRow()
            drCurrency("Symbol") = "£"
            dtCurrency.Rows.Add(drCurrency)

            ds.Tables.Add(dtCurrency)

            ds.WriteXml(HttpContext.Current.Server.MapPath("~/XML/Settings.xml"), XmlWriteMode.WriteSchema)
        End If

    End Sub

    Public Sub InsertUpdateBookingSchedule(ByVal BookingScheduleID As Int32, ByVal BookingSettingsID As Int32, ByVal Name As String, ByVal StartTime As TimeSpan, ByVal EndTime As TimeSpan, ByVal NumberOfCover As Int32, ByVal ServiceDuration As Int32, ByVal TimeSpan As Int32, ByVal PaymentType As Int32, ByVal DepositPercentage As Int32, ByVal DepositAmount As Int32, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal FutureTime As Int32, ByVal OnlineMaxCovers As Int32, ByVal One_booking_at_a_time As Int32, ByVal MinCovers As Int32, ByVal GroupID As Int32, ByVal is_selectProduct As Int32, ByVal MCPTimeSpan As Int32)
        Dim strQvuery As String = ""
        strQvuery &= " IF(" & BookingScheduleID & " = 0)"
        strQvuery &= " BEGIN"
        strQvuery &= " INSERT INTO BookingSchedule"
        strQvuery &= " ("
        strQvuery &= " BookingSettingsID,"
        strQvuery &= " Name,"
        strQvuery &= " StartTime,"
        strQvuery &= " EndTime,"
        strQvuery &= " NumberOfCover,"
        strQvuery &= " ServiceDuration,"
        strQvuery &= " TimeSpan,"
        strQvuery &= " PaymentType,"
        strQvuery &= " DepositPercentage,"
        strQvuery &= " DepositAmount,"
        strQvuery &= " StartDate,"
        strQvuery &= " EndDate,"
        strQvuery &= " FutureReservationTime,"
        strQvuery &= " OnlineMaxCovers,"
        strQvuery &= " One_booking_at_a_time,"
        strQvuery &= " MinCovers,"
        strQvuery &= " GroupID,"
        strQvuery &= " is_selectProduct,"
        strQvuery &= " MCPTimeSpan"
        strQvuery &= " )"
        strQvuery &= " VALUES"
        strQvuery &= " ("
        strQvuery &= BookingSettingsID & ","
        strQvuery &= "'" & Name & "',"
        strQvuery &= "'" & StartTime.ToString() & "',"
        strQvuery &= "'" & EndTime.ToString() & "',"
        strQvuery &= NumberOfCover & ","
        strQvuery &= ServiceDuration & ","
        strQvuery &= TimeSpan & ","
        strQvuery &= PaymentType & ","
        strQvuery &= DepositPercentage & ","
        strQvuery &= DepositAmount & ","
        strQvuery &= " CAST ('" & StartDate.ToString("s") & "' AS DATE),"
        strQvuery &= " CAST ('" & EndDate.ToString("s") & "' AS DATE),"
        strQvuery &= FutureTime & ","
        strQvuery &= OnlineMaxCovers & ","
        strQvuery &= One_booking_at_a_time & ","
        strQvuery &= MinCovers & ","
        strQvuery &= GroupID & ","
        strQvuery &= is_selectProduct & ","
        strQvuery &= MCPTimeSpan
        strQvuery &= " )"
        strQvuery &= " SELECT CAST(SCOPE_IDENTITY() AS INT)"
        strQvuery &= " END"
        strQvuery &= " ELSE"
        strQvuery &= " BEGIN"
        strQvuery &= " UPDATE BookingSchedule"
        strQvuery &= " SET BookingSettingsID = " & BookingSettingsID
        strQvuery &= " ,Name = '" & Name & "'"
        strQvuery &= " ,StartTime = '" & StartTime.ToString() & "'"
        strQvuery &= " ,EndTime = '" & EndTime.ToString() & "'"
        strQvuery &= " ,NumberOfCover = " & NumberOfCover
        strQvuery &= " ,ServiceDuration = " & ServiceDuration
        strQvuery &= " ,TimeSpan =  " & TimeSpan
        strQvuery &= " ,PaymentType =  " & PaymentType
        strQvuery &= " ,DepositPercentage = " & DepositPercentage
        strQvuery &= " ,DepositAmount = " & DepositAmount
        strQvuery &= " ,StartDate = " & " CAST ('" & StartDate.ToString("s") & "' AS DATE)"
        strQvuery &= " ,EndDate = " & " CAST ('" & EndDate.ToString("s") & "' AS DATE)"
        strQvuery &= " ,FutureReservationTime = " & FutureTime
        strQvuery &= " ,OnlineMaxCovers = " & OnlineMaxCovers
        strQvuery &= " ,One_booking_at_a_time = " & One_booking_at_a_time
        strQvuery &= " ,MinCovers = " & MinCovers
        strQvuery &= " ,GroupID = " & GroupID
        strQvuery &= " ,is_selectProduct = " & is_selectProduct
        strQvuery &= " ,MCPTimeSpan = " & MCPTimeSpan
        strQvuery &= " WHERE BookingScheduleID = " & BookingScheduleID
        strQvuery &= " END"
        strQvuery &= " SELECT " & BookingScheduleID

        Dim NewBookingScheduleID As Int32
        NewBookingScheduleID = Convert.ToInt32(conn.ExecuteScalar(strQvuery))

        SaveBookingSchedulesDates(StartDate, EndDate, NewBookingScheduleID)
    End Sub

    Public Sub DeleteBookingSchedule(ByVal BookingScheduleID As Int32)
        Dim strQuery As String
        strQuery = " DELETE FROM BookingSchedule WHERE BookingScheduleID = '" & BookingScheduleID & "'"
        conn.Ins_Upd_Del(strQuery)
    End Sub

    Public Function GetBookingScheduleBySettingID(ByVal BookingSettingID As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery &= " SELECT BS.BookingScheduleID,BS.BookingSettingsID,BS.Name,BS.StartTime,BS.EndTime,BS.NumberOfCover,BS.ServiceDuration,BS.TimeSpan,BS.PaymentType,BS.DepositPercentage,BS.DepositAmount,ISNULL(BS.StartDate,GETDATE()) AS StartDate,ISNULL(BS.EndDate,DATEADD(DAY,1,GETDATE())) AS EndDate,ISNULL(BS.FutureReservationTime,0) AS FutureReservationTime,ISNULL(BS.OnlineMaxCovers,1) AS OnlineMaxCovers,BS.One_booking_at_a_time,BS.MinCovers,BS.GroupID,(Select group_name from M_Table_Group WHERE GroupID = BS.GroupID) as GroupName,is_selectProduct,MCPTimeSpan"
        strQuery &= " FROM BookingSchedule BS WHERE BS.BookingSettingsID = '" & BookingSettingID.ToString() & "'"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetBookingScheduleByID(ByVal BookingScheduleID As Int32) As DataRow
        Dim strQuery As String
        strQuery = " SELECT * FROM BookingSchedule WHERE BookingScheduleID = '" & BookingScheduleID & "'"
        Return conn.SingleRow(strQuery)
    End Function

    Public Sub SaveBookingSchedulesDates(ByVal StartDate As Date, ByVal EndDate As Date, ByVal BookingScheduleId As Int32)

        Dim ds As New DataSet()
        Dim oClsDataccess As New ClsDataccess
        Dim oColSqlPar As ColSqlparram = New ColSqlparram()
        oColSqlPar.Add("@StartDate", StartDate, SqlDbType.DateTime)
        oColSqlPar.Add("@EndDate", EndDate, SqlDbType.DateTime)
        oColSqlPar.Add("@BBookingScheduleId", BookingScheduleId, SqlDbType.Int)
        ds = oClsDataccess.GetdatasetSp("SaveBookingSchedulesDates", oColSqlPar, "SBSD")

        'Dim strQuery As String = " "

        'strQuery &= " DECLARE @ScheStartDate DATE"
        'strQuery &= " DECLARE @ScheEndDate DATE"
        'strQuery &= " DECLARE @BookingScheduleId INT"
        'strQuery &= " DECLARE @CurrentDate DATE"
        'strQuery &= " SET @ScheStartDate = '" & StartDate.ToString("s") & "'"
        'strQuery &= " SET @ScheEndDate = '" & EndDate.ToString("s") & "'"
        'strQuery &= " SET @BookingScheduleId =" & BookingScheduleId

        'strQuery &= " DECLARE @StartTime TIME(7)"
        'strQuery &= " DECLARE @EndTime TIME(7)"
        'strQuery &= " DECLARE @NumberOfCover INT"
        'strQuery &= " DECLARE @ServiceDuration INT"
        'strQuery &= " DECLARE @TimeSpan INT"
        'strQuery &= " DECLARE @PaymentType INT"
        'strQuery &= " DECLARE @DepositPercentage DECIMAL"
        'strQuery &= " DECLARE @DepositAmount DECIMAL"
        'strQuery &= " DECLARE @BookingSettingsID INT"

        'strQuery &= " SELECT @BookingSettingsID = BookingSettingsID,@StartTime = StartTime,@EndTime = EndTime,"
        'strQuery &= " @NumberOfCover = NumberOfCover,@ServiceDuration = ServiceDuration,@TimeSpan = TimeSpan,"
        'strQuery &= " @PaymentType = PaymentType,@DepositPercentage = DepositPercentage,@DepositAmount = DepositAmount"
        'strQuery &= " FROM BookingSchedule WHERE BookingScheduleID = @BookingScheduleId"

        'strQuery &= " WHILE @ScheStartDate <= @ScheEndDate "
        'strQuery &= " BEGIN"
        'strQuery &= " SET @CurrentDate = @ScheStartDate"
        'strQuery &= " IF NOT EXISTS (SELECT * FROM BookingScheduleDate"
        'strQuery &= " WHERE BookingScheduleId = @BookingScheduleId AND  ScheduleDate = @CurrentDate)"
        'strQuery &= " BEGIN"
        'strQuery &= " INSERT INTO BookingScheduleDate "
        'strQuery &= " ( "
        'strQuery &= " BookingScheduleId,"
        'strQuery &= " BookingSettingsid,"
        'strQuery &= " ScheduleDate,"
        'strQuery &= " IsAvailable,"
        'strQuery &= " StartTime,"
        'strQuery &= " EndTime,"
        'strQuery &= " NumberOfCover,"
        'strQuery &= " ServiceDuration,"
        'strQuery &= " TimeSpan,"
        'strQuery &= " PaymentType,"
        'strQuery &= " DepositPercentage,"
        'strQuery &= " DepositAmount"
        'strQuery &= " ) "
        'strQuery &= " VALUES"
        'strQuery &= " ( "
        'strQuery &= " @BookingScheduleId, "
        'strQuery &= " @BookingSettingsID, "
        'strQuery &= " @CurrentDate, "
        'strQuery &= " 1,"
        'strQuery &= " @StartTime,"
        'strQuery &= " @EndTime,"
        'strQuery &= " @NumberOfCover,"
        'strQuery &= " @ServiceDuration,"
        'strQuery &= " @TimeSpan,"
        'strQuery &= " @PaymentType,"
        'strQuery &= " @DepositPercentage,"
        'strQuery &= " @DepositAmount"
        'strQuery &= " )"
        'strQuery &= " END"
        'strQuery &= " ELSE"
        'strQuery &= " BEGIN"
        'strQuery &= " UPDATE BookingScheduleDate"
        'strQuery &= " SET StartTime = @StartTime,"
        'strQuery &= " EndTime = @EndTime,"
        'strQuery &= " NumberOfCover = @NumberOfCover,"
        'strQuery &= " ServiceDuration = @ServiceDuration,"
        'strQuery &= " TimeSpan = @TimeSpan,"
        'strQuery &= " PaymentType = @PaymentType,"
        'strQuery &= " DepositPercentage = @DepositPercentage,"
        'strQuery &= " DepositAmount = @DepositAmount"
        'strQuery &= " WHERE BookingScheduleId = @BookingScheduleId AND  ScheduleDate = @CurrentDate"
        'strQuery &= " END"
        'strQuery &= " SET @ScheStartDate = DATEADD(DAY,1,@ScheStartDate) "
        'strQuery &= " END"
        'strQuery &= " Select 1"

        'Dim objConnection As DBConnection = New DBConnection()
        'objConnection.Ins_Upd_Del(strQuery)
        'conn.ExecuteScalar(strQuery)
    End Sub

    Public Function GetScheduleDateData(ByVal BookingScheduleId As Int32, ByVal StartDate As Date, ByVal EndDate As Date) As DataTable
        Dim dt As DataTable = New DataTable
        Dim strQuery As String = String.Empty
        strQuery &= " SELECT * FROM BookingScheduleDate BSD"
        strQuery &= " WHERE BSD.BookingScheduleId = " & BookingScheduleId & " AND (ScheduleDate BETWEEN '" & StartDate.ToString("s") & "' AND '" & EndDate.ToString("s") & "')"

        dt = conn.SelectData(strQuery).Tables(0)

        Return dt
    End Function

    Public Shared Function GetBookingScheduleDateId(ByVal bookingDate As Date, ByVal bookingScheduleId As Int32) As String
        Dim strQuery As String = String.Empty
        strQuery = " SELECT BookingScheduleDateId FROM BookingScheduleDate WHERE BookingScheduleId = " & bookingScheduleId & " AND ScheduleDate = '" & bookingDate.ToString("yyyy-MM-dd") & "'"
        Dim objConn As DBConnection = New DBConnection()
        Return objConn.SingleCell(strQuery)
    End Function

    Public Shared Function GetOnlineMaxCover(ByVal bookingScheduleId As Int32) As DataRow
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM BookingSchedule WHERE BookingScheduleID = " & bookingScheduleId
        Dim objConn As DBConnection = New DBConnection()
        Return objConn.SingleRow(strQuery)
    End Function

    Public Shared Function GetVenueAddressDetail(ByVal bookingSettingId As Int32) As DataRow
        Dim strQuery As String = String.Empty
        strQuery &= " SELECT A.* "
        strQuery &= " FROM bookingsettings BS INNER JOIN Store S ON BS.StoreID = S.StoreID"
        strQuery &= " INNER JOIN Venue V ON S.VenueID = V.VenueID"
        strQuery &= " INNER JOIN Address A ON V.AddressID = A.AddressID"
        strQuery &= " WHERE BS.BookingSettingsid = " & bookingSettingId
        Dim objConn As DBConnection = New DBConnection()
        Return objConn.SingleRow(strQuery)
    End Function

    Public Shared Sub UpdateRecurringSchedule(ByVal RecurringType As Int32, ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal noOfOccurance As Int32, ByVal IsOccurance As Boolean, ByVal BookingScheduleDateId As Int32, ByVal Days As List(Of Int32), ByVal IsAvailable As Integer)
        Dim strQuery As String = String.Empty
        Dim objConn As DBConnection = New DBConnection()
        LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 1")
        If RecurringType = 2 Then
            LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 2")
            If Days.Count > 0 Then
                LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 3")
                Dim dayOfWeek As Int32
                If IsOccurance Then
                    LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 3.1")
                    Dim i As Int32 = 1
                    While i <= noOfOccurance
                        dayOfWeek = CInt(startDate.DayOfWeek)
                        If Days.Contains(dayOfWeek) Then
                            strQuery = String.Empty
                            strQuery &= " DECLARE @BookingScheduleDateId INT"
                            strQuery &= " SET @BookingScheduleDateId = " & BookingScheduleDateId

                            strQuery &= " DECLARE @BookingScheduleId INT"
                            strQuery &= " SET @BookingScheduleId = (SELECT BookingScheduleId FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId)"

                            strQuery &= " DECLARE @CurrentDate DATETIME"
                            strQuery &= " SET @CurrentDate = '" & startDate.ToString("s") & "'"

                            strQuery &= " IF EXISTS (SELECT * FROM BookingScheduleDate WHERE BookingScheduleId = @BookingScheduleId AND ScheduleDate = CAST(@CurrentDate AS DATE))"
                            strQuery &= " BEGIN"
                            strQuery &= " UPDATE BookingScheduleDate"
                            strQuery &= " SET StartTime = (SELECT StartTime FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " EndTime = (SELECT EndTime FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " NumberOfCover = (SELECT NumberOfCover FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " ServiceDuration = (SELECT ServiceDuration FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " TimeSpan = (SELECT TimeSpan FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " PaymentType = (SELECT PaymentType FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " DepositPercentage = (SELECT DepositPercentage FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " DepositAmount = (SELECT DepositAmount FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId)"
                            strQuery &= " WHERE BookingScheduleId = @BookingScheduleId AND ScheduleDate = CAST(@CurrentDate AS DATE)"

                            strQuery &= " End"
                            LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule" + strQuery)
                            objConn.Ins_Upd_Del(strQuery)

                            i = i + 1
                        End If
                        startDate = startDate.AddDays(1)
                    End While

                Else
                    LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 3.2")
                    LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 3.2.1" + BookingScheduleDateId.ToString())
                    LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 3.2.1" + startDate.ToString("s"))
                    LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 3.2.1" + endDate.ToString("s"))

                    While startDate <= endDate
                        dayOfWeek = CInt(startDate.DayOfWeek)
                        If Days.Contains(dayOfWeek) Then
                            strQuery = String.Empty
                            strQuery &= " DECLARE @BookingScheduleDateId INT"
                            strQuery &= " SET @BookingScheduleDateId = " & BookingScheduleDateId.ToString()

                            strQuery &= " DECLARE @BookingScheduleId INT"
                            strQuery &= " SET @BookingScheduleId = (SELECT BookingScheduleId FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId)"

                            strQuery &= " DECLARE @CurrentDate DATETIME"
                            strQuery &= " SET @CurrentDate = '" & startDate.ToString("s") & "'"

                            strQuery &= " IF EXISTS (SELECT * FROM BookingScheduleDate WHERE BookingScheduleId = @BookingScheduleId AND ScheduleDate = CAST(@CurrentDate AS DATE))"
                            strQuery &= " BEGIN"
                            strQuery &= " UPDATE BookingScheduleDate"
                            strQuery &= " SET StartTime = (SELECT StartTime FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " EndTime = (SELECT EndTime FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " NumberOfCover = (SELECT NumberOfCover FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " IsAvailable = (SELECT IsAvailable FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            'If IsAvailable = 0 Then
                            '    strQuery &= " IsAvailable = 0,"
                            'ElseIf IsAvailable = 1 Then
                            '    strQuery &= " IsAvailable = 1,"
                            'End If
                            strQuery &= " ServiceDuration = (SELECT ServiceDuration FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " TimeSpan = (SELECT TimeSpan FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " PaymentType = (SELECT PaymentType FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " DepositPercentage = (SELECT DepositPercentage FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                            strQuery &= " DepositAmount = (SELECT DepositAmount FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId)"
                            strQuery &= " WHERE BookingScheduleId = @BookingScheduleId AND ScheduleDate = CAST(@CurrentDate AS DATE)"

                            strQuery &= " End"
                            LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule" + strQuery)
                            objConn.Ins_Upd_Del(strQuery)
                        End If

                        startDate = startDate.AddDays(1)
                    End While

                End If
            End If
        Else
            LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule 4")
            If IsOccurance Then
                strQuery &= " DECLARE @StartDate DATETIME"
                strQuery &= " SET @StartDate = '" & startDate.ToString("s") & "'"
                strQuery &= " DECLARE @BookingScheduleDateId INT"
                strQuery &= " SET @BookingScheduleDateId = " & BookingScheduleDateId
                strQuery &= " DECLARE @Occurance INT"
                strQuery &= " SET @Occurance = " & noOfOccurance
                strQuery &= " DECLARE @DurationType INT"
                strQuery &= " SET @DurationType = " & RecurringType
                strQuery &= " DECLARE @Count INT"
                strQuery &= " SET @Count = 0"
                strQuery &= " DECLARE @CurrentDate DATETIME"
                strQuery &= " DECLARE @BookingScheduleId INT"
                strQuery &= " SET @BookingScheduleId = (SELECT BookingScheduleId FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId)"

                strQuery &= " WHILE (@Count < @Occurance)"
                strQuery &= " BEGIN"
                strQuery &= " IF @DurationType = 1"
                strQuery &= " BEGIN"
                strQuery &= " SET @CurrentDate = DATEADD(DAY,@Count,@StartDate)"
                strQuery &= " End"
                strQuery &= " ELSE IF @DurationType = 2"
                strQuery &= " BEGIN"
                strQuery &= " SET @CurrentDate = DATEADD(WEEK,@Count,@StartDate)"
                strQuery &= " End"
                strQuery &= " ELSE IF @DurationType = 3"
                strQuery &= " BEGIN"
                strQuery &= " SET @CurrentDate = DATEADD(MONTH,@Count,@StartDate)"
                strQuery &= " End"
                strQuery &= " ELSE IF @DurationType = 4"
                strQuery &= " BEGIN"
                strQuery &= " SET @CurrentDate = DATEADD(YEAR,@Count,@StartDate)"
                strQuery &= " End"
                strQuery &= " IF EXISTS (SELECT * FROM BookingScheduleDate WHERE BookingScheduleId = @BookingScheduleId AND ScheduleDate = CAST(@CurrentDate AS DATE))"
                strQuery &= " BEGIN"
                strQuery &= " UPDATE BookingScheduleDate"
                strQuery &= " SET StartTime = (SELECT StartTime FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " EndTime = (SELECT EndTime FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " NumberOfCover = (SELECT NumberOfCover FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " ServiceDuration = (SELECT ServiceDuration FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " TimeSpan = (SELECT TimeSpan FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " PaymentType = (SELECT PaymentType FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " DepositPercentage = (SELECT DepositPercentage FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " DepositAmount = (SELECT DepositAmount FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId)"
                strQuery &= " WHERE BookingScheduleId = @BookingScheduleId AND ScheduleDate = CAST(@CurrentDate AS DATE)"

                strQuery &= " End"
                strQuery &= " SET @Count = @Count + 1"
                strQuery &= " End"
            Else
                strQuery &= " DECLARE @StartDate DATETIME"
                strQuery &= " SET @StartDate = '" & startDate.ToString("s") & "'"
                strQuery &= " DECLARE @EndDate DATETIME"
                strQuery &= " SET @EndDate = '" & endDate.ToString("s") & "'"
                strQuery &= " DECLARE @BookingScheduleDateId INT"
                strQuery &= " SET @BookingScheduleDateId = " & BookingScheduleDateId
                strQuery &= " DECLARE @DurationType INT"
                strQuery &= " SET @DurationType = " & RecurringType
                strQuery &= " DECLARE @Count INT"
                strQuery &= " SET @Count = 0"
                strQuery &= " DECLARE @CurrentDate DATETIME"
                strQuery &= " SET @CurrentDate = @StartDate"
                strQuery &= " DECLARE @BookingScheduleId INT"
                strQuery &= " SET @BookingScheduleId = (SELECT BookingScheduleId FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId)"

                strQuery &= " WHILE (@CurrentDate < @EndDate)"
                strQuery &= " BEGIN"
                strQuery &= " IF @DurationType = 1"
                strQuery &= " BEGIN"
                strQuery &= " SET @CurrentDate = DATEADD(DAY,@Count,@StartDate)"
                strQuery &= " End"
                strQuery &= " ELSE IF @DurationType = 2"
                strQuery &= " BEGIN"
                strQuery &= " SET @CurrentDate = DATEADD(WEEK,@Count,@StartDate)"
                strQuery &= " End"
                strQuery &= " ELSE IF @DurationType = 3"
                strQuery &= " BEGIN"
                strQuery &= " SET @CurrentDate = DATEADD(MONTH,@Count,@StartDate)"
                strQuery &= " End"
                strQuery &= " ELSE IF @DurationType = 4"
                strQuery &= " BEGIN"
                strQuery &= " SET @CurrentDate = DATEADD(YEAR,@Count,@StartDate)"
                strQuery &= " End"
                strQuery &= " IF EXISTS (SELECT * FROM BookingScheduleDate WHERE BookingScheduleId = @BookingScheduleId AND ScheduleDate = CAST(@CurrentDate AS DATE))"
                strQuery &= " BEGIN"
                strQuery &= " UPDATE BookingScheduleDate"
                strQuery &= " SET StartTime = (SELECT StartTime FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " EndTime = (SELECT EndTime FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " NumberOfCover = (SELECT NumberOfCover FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " ServiceDuration = (SELECT ServiceDuration FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " TimeSpan = (SELECT TimeSpan FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " PaymentType = (SELECT PaymentType FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " DepositPercentage = (SELECT DepositPercentage FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId),"
                strQuery &= " DepositAmount = (SELECT DepositAmount FROM BookingScheduleDate WHERE BookingScheduleDateId = @BookingScheduleDateId)"
                strQuery &= " WHERE BookingScheduleId = @BookingScheduleId AND ScheduleDate = CAST(@CurrentDate AS DATE)"
                strQuery &= " End"
                strQuery &= " SET @Count = @Count + 1"
                strQuery &= " End"


            End If
            objConn.Ins_Upd_Del(strQuery)
            LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule" + strQuery)
        End If
        LogHelper.Error("RecurringSchedule: UpdateRecurringSchedule" + strQuery)
    End Sub

    Public Function GetBookingScheduleDateByID(ByVal BookingScheduleDateId As Int32) As DataRow
        Dim strQuery As String
        strQuery = " SELECT * FROM BookingScheduleDate WHERE BookingScheduleDateId = '" & BookingScheduleDateId & "'"
        Return conn.SingleRow(strQuery)
    End Function

    Public Function GetAllVenueForMaster() As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM Venue"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetVenueById(ByVal VenueId As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM Venue WHERE VenueID = " & VenueId
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetAllVenueMasterData() As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT VM.*,V.Name FROM VenueMaster VM left outer JOIN Venue V ON VM.OtherVenueId = V.VenueId"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetVenueMasterById(ByVal BookingVenueId As Int32) As DataRow
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM VenueMaster WHERE BookingVenueId = " & BookingVenueId
        Return conn.SingleRow(strQuery)
    End Function

    Public Sub VenueMasterSave(ByVal BookingVenueId As Int32, ByVal VenueName As String, ByVal OtherVenueId As Int32)
        Dim strQuery As String = String.Empty
        strQuery &= " IF " & BookingVenueId & " = 0"
        strQuery &= " BEGIN"
        strQuery &= " INSERT INTO VenueMaster"
        strQuery &= " ("
        strQuery &= " VenueName,"
        strQuery &= " OtherVenueId"
        strQuery &= " )"
        strQuery &= " VALUES"
        strQuery &= " ("
        strQuery &= "'" & VenueName & "',"
        strQuery &= "'" & OtherVenueId & "'"
        strQuery &= " )"
        strQuery &= " End"
        strQuery &= " Else"
        strQuery &= " BEGIN"
        strQuery &= " UPDATE VenueMaster"
        strQuery &= " SET VenueName = '" & VenueName & "',"
        strQuery &= " OtherVenueId = '" & OtherVenueId & "'"
        strQuery &= " WHERE BookingVenueId = " & BookingVenueId
        strQuery &= " End"
        conn.Ins_Upd_Del(strQuery)
    End Sub
    Public Sub DeleteVenueMasterById(ByVal BookingVenueId As Int32)
        Dim strQuery As String = String.Empty
        strQuery = " DELETE FROM VenueMaster WHERE BookingVenueId = " & BookingVenueId
        conn.Ins_Upd_Del(strQuery)
    End Sub

    Public Function GetOtherStoreForStoreMaster(ByVal BookingVenueId As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM Store S"
        Return conn.SelectData(strQuery)
    End Function

    Public Sub StoreMasterSave(ByVal OurStoreId As Int32, ByVal StoreName As String, ByVal BookingVenueId As Int32, ByVal OtherStoreId As Int32)
        Dim strQuery As String = String.Empty

        strQuery &= " IF " & OurStoreId & " = 0"
        strQuery &= " BEGIN"
        strQuery &= " INSERT INTO StoreMaster"
        strQuery &= " ("
        strQuery &= " StoreName,"
        strQuery &= " BookingVenueId,"
        strQuery &= " OtherStoreId"
        strQuery &= " )"
        strQuery &= " VALUES"
        strQuery &= " ("
        strQuery &= " '" & StoreName & "',"
        strQuery &= BookingVenueId & ","
        strQuery &= OtherStoreId
        strQuery &= " )"
        strQuery &= " End"
        strQuery &= " Else"
        strQuery &= " BEGIN"
        strQuery &= " UPDATE StoreMaster"
        strQuery &= " SET StoreName = '" & StoreName & "',"
        strQuery &= " BookingVenueId = " & BookingVenueId & ","
        strQuery &= " OtherStoreId = " & OtherStoreId
        strQuery &= " WHERE OurStoreId = " & OurStoreId
        strQuery &= " End"

        conn.Ins_Upd_Del(strQuery)
    End Sub

    Public Function GetAllTabNames() As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT *,(CASE TabType WHEN 1 THEN 'Hotel' ELSE 'Sehedule' END) AS TabTypeName  FROM TabMaster "
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetDefaultField() As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " select * from M_Venue_Settings "
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetEmpByBookingRef(ByVal bookingRef As String) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " select addr.* from bookings b Inner Join Account a On a.AccountID = b.accountid Inner join Address addr on addr.AddressID = a.AddressID where b.bookingref = '" + bookingRef.ToString + "' "
        'Return conn.SelectData(strQuery)
        Dim ds As DataSet = conn.SelectData(strQuery)
        Return ds
    End Function

    Public Function GetAllTabNamesByType(ByVal TypeID As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM TabMaster WHERE TabType = " & TypeID
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetOtherStoreById(ByVal StoreID As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM Store S WHERE S.StoreID  = " & StoreID
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetAllStoreMasterData() As DataSet
        Dim strQuery As String = String.Empty
        strQuery &= " SELECT SM.*,VM.VenueName,S.Name "
        strQuery &= " FROM StoreMaster SM left outer join VenueMaster VM ON SM.BookingVenueId = VM.BookingVenueId"
        strQuery &= " left outer JOIN Store S ON SM.OtherStoreId = S.StoreID"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetStoreMasterById(ByVal OurStoreId As Int32) As DataRow
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM StoreMaster WHERE OurStoreId = " & OurStoreId
        Return conn.SingleRow(strQuery)
    End Function

    Public Sub DeleteStoreMasterById(ByVal OurStoreId As Int32)
        Dim strQuery As String = String.Empty
        strQuery = " DELETE FROM StoreMaster WHERE OurStoreId = " & OurStoreId
        conn.Ins_Upd_Del(strQuery)
    End Sub

    Public Function GetStoreMasterByBookingVenueId(ByVal BookingVenueId As Int32, ByVal BookingSettingsId As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery &= " SELECT * FROM StoreMaster WHERE BookingVenueId = " & BookingVenueId & " AND OurStoreId NOT IN (SELECT StoreId FROM BookingSettings WHERE BookingSettingsid <> " & BookingSettingsId & ")"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetRedirectToLocation(ByVal BookingSettingId As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT BS.*,SM.StoreName FROM bookingsettings BS left outer join StoreMaster SM ON SM.OurStoreId = BS.StoreID WHERE BS.BookingSettingsid  <> " & BookingSettingId
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetBookingSettingsByRedirectTo(ByVal RedirectToId As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM bookingsettings WHERE  RedirectScheduleTo = " & RedirectToId
        Return conn.SelectData(strQuery)
    End Function

    Public Sub CopyRedirectToData(ByVal BookingSettingID As Int32, ByVal RedirectToID As Int32)
        Dim strQuery As String = String.Empty

        strQuery &= " DECLARE @BookingSettingID INT"
        strQuery &= " SET @BookingSettingID = " & BookingSettingID
        strQuery &= " DECLARE @RedirectToID INT"
        strQuery &= " SET @RedirectToID = " & RedirectToID

        strQuery &= " DELETE FROM BookingScheduleDate WHERE BookingSettingsid = @BookingSettingID"

        strQuery &= " DELETE FROM BookingSchedule WHERE BookingSettingsid = @BookingSettingID"

        strQuery &= " DECLARE @BookingScheduleID INT"

        strQuery &= " DECLARE CurGetAllScheduleOfRedirectTo CURSOR FOR"
        strQuery &= " SELECT BookingScheduleID FROM BookingSchedule WHERE BookingSettingsID = @RedirectToID"

        strQuery &= " OPEN CurGetAllScheduleOfRedirectTo"

        strQuery &= " FETCH NEXT"
        strQuery &= " FROM CurGetAllScheduleOfRedirectTo INTO @BookingScheduleID"
        strQuery &= " WHILE @@FETCH_STATUS = 0"
        strQuery &= " BEGIN"

        strQuery &= " INSERT INTO BookingSchedule("
        strQuery &= " BookingSettingsID,Name,StartTime,EndTime,NumberOfCover,ServiceDuration,TimeSpan,PaymentType,"
        strQuery &= " DepositPercentage,DepositAmount,StartDate,EndDate,FutureReservationTime,OnlineMaxCovers,MinCovers,One_booking_at_a_time,GroupID,is_selectProduct,MCPTimeSpan)"
        strQuery &= " SELECT @BookingSettingID,Name,StartTime,EndTime,NumberOfCover,ServiceDuration,TimeSpan,PaymentType,"
        strQuery &= " DepositPercentage, DepositAmount, StartDate, EndDate, FutureReservationTime, OnlineMaxCovers,MinCovers,One_booking_at_a_time,GroupID,is_selectProduct,MCPTimeSpan"
        strQuery &= " FROM BookingSchedule WHERE BookingScheduleID = @BookingScheduleID"


        strQuery &= " DECLARE @NewBookingScheduleID INT = CAST(SCOPE_IDENTITY() AS INT)"


        strQuery &= " DECLARE @ScheStartDate DATE"
        strQuery &= " DECLARE @ScheEndDate DATE"

        strQuery &= " DECLARE @CurrentDate DATE"
        strQuery &= " SET @ScheStartDate = (SELECT StartDate FROM BookingSchedule WHERE BookingScheduleID = @BookingScheduleID)"
        strQuery &= " SET @ScheEndDate = (SELECT EndDate FROM BookingSchedule WHERE BookingScheduleID = @BookingScheduleID)"


        strQuery &= " DECLARE @StartTime TIME(7)"
        strQuery &= " DECLARE @EndTime TIME(7)"
        strQuery &= " DECLARE @NumberOfCover INT"
        strQuery &= " DECLARE @ServiceDuration INT"
        strQuery &= " DECLARE @TimeSpan INT"
        strQuery &= " DECLARE @PaymentType INT"
        strQuery &= " DECLARE @DepositPercentage DECIMAL"
        strQuery &= " DECLARE @DepositAmount DECIMAL"
        strQuery &= " DECLARE @BookingSettingsID INT"

        strQuery &= " SELECT @BookingSettingsID = BookingSettingsID,@StartTime = StartTime,@EndTime = EndTime,"
        strQuery &= " @NumberOfCover = NumberOfCover,@ServiceDuration = ServiceDuration,@TimeSpan = TimeSpan,"
        strQuery &= " @PaymentType = PaymentType,@DepositPercentage = DepositPercentage,@DepositAmount = DepositAmount"
        strQuery &= " FROM BookingSchedule WHERE BookingScheduleID = @NewBookingScheduleID"

        strQuery &= " WHILE @ScheStartDate <= @ScheEndDate "
        strQuery &= " BEGIN"
        strQuery &= " SET @CurrentDate = @ScheStartDate"
        strQuery &= " IF NOT EXISTS (SELECT * FROM BookingScheduleDate"
        strQuery &= " WHERE BookingScheduleId = @NewBookingScheduleID AND  ScheduleDate = @CurrentDate)"
        strQuery &= " BEGIN"
        strQuery &= " INSERT INTO BookingScheduleDate"
        strQuery &= " ( "
        strQuery &= " BookingScheduleId,"
        strQuery &= " BookingSettingsid,"
        strQuery &= " ScheduleDate,"
        strQuery &= " IsAvailable,"
        strQuery &= " StartTime,"
        strQuery &= " EndTime,"
        strQuery &= " NumberOfCover,"
        strQuery &= " ServiceDuration,"
        strQuery &= " TimeSpan,"
        strQuery &= " PaymentType,"
        strQuery &= " DepositPercentage,"
        strQuery &= " DepositAmount"
        strQuery &= " ) "
        strQuery &= " VALUES"
        strQuery &= " ( "
        strQuery &= " @NewBookingScheduleID,"
        strQuery &= " @BookingSettingsID,"
        strQuery &= " @CurrentDate, "
        strQuery &= " 1,"
        strQuery &= " @StartTime,"
        strQuery &= " @EndTime,"
        strQuery &= " @NumberOfCover,"
        strQuery &= " @ServiceDuration,"
        strQuery &= " @TimeSpan,"
        strQuery &= " @PaymentType,"
        strQuery &= " @DepositPercentage,"
        strQuery &= " @DepositAmount"
        strQuery &= " )"
        strQuery &= " End"
        strQuery &= " Else"
        strQuery &= " BEGIN"
        strQuery &= " UPDATE BookingScheduleDate"
        strQuery &= " SET StartTime = @StartTime,"
        strQuery &= " EndTime = @EndTime,"
        strQuery &= " NumberOfCover = @NumberOfCover,"
        strQuery &= " ServiceDuration = @ServiceDuration,"
        strQuery &= " TimeSpan = @TimeSpan,"
        strQuery &= " PaymentType = @PaymentType,"
        strQuery &= " DepositPercentage = @DepositPercentage,"
        strQuery &= " DepositAmount = @DepositAmount"
        strQuery &= " WHERE BookingScheduleId = @NewBookingScheduleID AND  ScheduleDate = @CurrentDate"
        strQuery &= " End"
        strQuery &= " SET @ScheStartDate = DATEADD(DAY,1,@ScheStartDate)"
        strQuery &= " End"


        strQuery &= " FETCH NEXT"
        strQuery &= " FROM CurGetAllScheduleOfRedirectTo INTO @BookingScheduleID"
        strQuery &= " End"

        strQuery &= " CLOSE CurGetAllScheduleOfRedirectTo"
        strQuery &= " DEALLOCATE CurGetAllScheduleOfRedirectTo"

        conn.Ins_Upd_Del(strQuery)
    End Sub

    Public Function GetStoreIDForOnlineBooking(ByVal RedirectTo As Int32, ByVal ScheduleDate As DateTime, ByVal BookingScheID As Int32) As Int32
        Dim strQuery As String = String.Empty

        strQuery &= " DECLARE @RedirectToId INT "
        strQuery &= " SET @RedirectToId = " & RedirectTo
        strQuery &= " DECLARE @Date DATETIME"
        strQuery &= " SET @Date = '" & ScheduleDate.ToString("s") & "'"
        strQuery &= " DECLARE @BookingScheduleID INT"
        strQuery &= " SET @BookingScheduleID = " & BookingScheID

        strQuery &= " DECLARE @TotalBookings INT"
        strQuery &= " SET @TotalBookings = (SELECT NumberOfCover FROM BookingScheduleDate WHERE BookingSettingsid = @RedirectToId AND ScheduleDate = @Date AND BookingScheduleId = @BookingScheduleId)"
        strQuery &= " DECLARE @StoreID INT "
        strQuery &= " SET @StoreID = 0"

        strQuery &= " DECLARE @BookingSettingsid INT"
        strQuery &= " DECLARE @TotalStoreBooking INT"
        strQuery &= " SET @TotalStoreBooking = (SELECT COUNT(*) FROM bookings WHERE [date] = @Date AND BookingSettingsid = @RedirectToId AND BookingScheduleID = @BookingScheduleID AND period = 1)"
        strQuery &= " IF @TotalStoreBooking < @TotalBookings"
        strQuery &= " BEGIN"
        strQuery &= " SET @StoreID = (SELECT StoreID FROM bookingsettings WHERE BookingSettingsid = @RedirectToId)"
        strQuery &= " End"

        strQuery &= " IF @StoreID = 0"
        strQuery &= " BEGIN"
        strQuery &= " DECLARE CurRelatedRecords CURSOR FOR"
        strQuery &= " SELECT BookingSettingsid FROM bookingsettings WHERE RedirectScheduleTo = @RedirectToId"

        strQuery &= " OPEN CurRelatedRecords"
        strQuery &= " FETCH NEXT"
        strQuery &= " FROM CurRelatedRecords INTO @BookingSettingsid"

        strQuery &= " WHILE @@FETCH_STATUS = 0"
        strQuery &= " BEGIN"
        strQuery &= " SET @TotalBookings = (SELECT NumberOfCover FROM BookingScheduleDate WHERE BookingSettingsid = @RedirectToId AND ScheduleDate = @Date)"
        strQuery &= " SET @TotalStoreBooking = (SELECT COUNT(*) FROM bookings WHERE [date] = @Date AND BookingSettingsid = @BookingSettingsid AND BookingScheduleID = @BookingScheduleID  AND period = 1)"

        strQuery &= " IF @TotalStoreBooking < @TotalBookings"
        strQuery &= " BEGIN"
        strQuery &= " SET @StoreID = (SELECT StoreID FROM bookingsettings WHERE BookingSettingsid = @BookingSettingsid)"
        strQuery &= " BREAK"
        strQuery &= " End"

        strQuery &= " FETCH NEXT"
        strQuery &= " FROM CurRelatedRecords INTO @BookingSettingsid"
        strQuery &= " End"

        strQuery &= " CLOSE CurRelatedRecords"
        strQuery &= " DEALLOCATE CurRelatedRecords"
        strQuery &= " End"

        strQuery &= " SELECT @StoreID"

        Return Utils.ParseInt(conn.SingleCell(strQuery))
    End Function

    Public Function GetStaticFieldData() As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT Static_Field FROM M_Custom_Field WHERE Static_Field <> 'Custom' UNION ALL SELECT DISTINCT Static_Field FROM M_Custom_Field WHERE Static_Field = 'Custom'"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetSelectedValue(ByRef ddl As RadComboBox) As String
        Try
            Dim sb As New StringBuilder()
            Dim collection As IList(Of RadComboBoxItem) = ddl.CheckedItems

            If (collection.Count <> 0) Then

                sb.Append("")

                For Each item As RadComboBoxItem In collection
                    If sb.ToString = "" Then
                        sb.Append(item.Value)
                    Else
                        sb.Append("#" + item.Value)
                    End If

                Next
            End If
            Return sb.ToString

        Catch ex As Exception
            Return ""

        End Try
    End Function
    Public Function GetAllValue(ByRef ddl As RadComboBox) As String
        Try
            Dim sb As New StringBuilder()
            Dim collection As IList(Of RadComboBoxItem) = ddl.Items

            If (collection.Count <> 0) Then

                sb.Append("")

                For Each item As RadComboBoxItem In collection
                    If sb.ToString = "" Then
                        sb.Append(item.Value)
                    Else
                        sb.Append("#" + item.Value)
                    End If

                Next
            End If
            Return sb.ToString

        Catch ex As Exception
            Return ""

        End Try
    End Function

    Public Function GetTableNames() As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT b.*,Stuff((SELECT ', ' + a.Table_name FROM M_table a where cast(a.Table_id as nvarchar(100)) in (select items from dbo.split((b.AllowedJoin),'#')) FOR XML PATH('')), 1, 2, '') AS TableNames FROM M_Table b  "
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetTableNamesWithoutId(ByVal Table_id As Int32) As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM M_Table where Table_id <> " + Table_id.ToString() + " "
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetGroupNames() As DataSet
        Dim strQuery As String = String.Empty
        strQuery = " SELECT *,Stuff((SELECT ', ' + Table_name FROM M_table where cast(Table_id as nvarchar(100)) in (select items from dbo.split((table_set),'#')) FOR XML PATH('')), 1, 2, '') AS TableNames FROM M_Table_Group"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetTableNamesByGroupID(ByVal GroupID As Int32, ByVal bookingref As String) As DataSet
        Dim strQuery As String = String.Empty
        ''strQuery = " SELECT * FROM M_Table b where Table_id in (select items from dbo.Split((select table_set from M_Table_Group where GroupID = " + GroupID.ToString() + "),('#'))) "
        'strQuery += "SELECT Table_name + ' - Max:' + convert(nvarchar(50),MaxCover) as table_name ,table_id FROM M_Table b where Table_id in (select items from dbo.Split((select table_set from M_Table_Group where GroupID = " + GroupID.ToString() + "),('#')))"
        'strQuery += " AND Table_id not in (select items from dbo.Split(((select Stuff((SELECT '# ' + b.Allotted_Tables "
        'strQuery += " from bookings b INNER JOIN Tables t ON t.TableNumber = b.bookingref where b.BookingScheduleDateId = (select BookingScheduleDateId from bookings WHERE bookingref = '" + bookingref.ToString() + "')"
        'strQuery += " AND b.bookingtime = (select bookingtime from bookings WHERE bookingref = '" + bookingref.ToString() + "')"
        'strQuery += " AND b.bookingref <> '" + bookingref.ToString() + "' AND t.Closed = 1  FOR XML PATH('')), 1, 2, ''))),('#')))"

        strQuery += "SELECT Table_name + ' - Max:' + convert(nvarchar(50),MaxCover) as table_name ,table_id FROM M_Table b where cast(Table_id as nvarchar(100)) in (select items from dbo.Split((select table_set from M_Table_Group where GroupID = " + GroupID.ToString() + "),('#')))"
        strQuery += "AND  cast(Table_id as nvarchar(100))  not in (select isnull(items,'')  from dbo.Split(((select Stuff((SELECT '# ' + b.Allotted_Tables from bookings b "
        strQuery += "where	b.BookingScheduleDateId = (select BookingScheduleDateId from bookings WHERE bookingref = '" + bookingref.ToString() + "') "
        strQuery += "AND Convert(varchar(5), b.bookingtime, 108) BETWEEN (SELECT Convert(varchar(5), DATEADD(minute,(bsh.ServiceDuration-((bsh.ServiceDuration*2)-1)),bks.bookingtime), 108) AS Mservice from bookings bks INNER JOIN BookingSchedule bsh on bsh.BookingScheduleID = bks.BookingScheduleID WHERE bks.bookingref = '" + bookingref.ToString() + "')"
        strQuery += "AND (SELECT Convert(varchar(5), DATEADD(minute,(bsh.ServiceDuration-1),bks.bookingtime), 108) AS Pservice from bookings bks INNER JOIN BookingSchedule bsh on bsh.BookingScheduleID = bks.BookingScheduleID WHERE bks.bookingref = '" + bookingref.ToString() + "') FOR XML PATH('')), 1, 2, ''))),('#')))"

        'strQuery += "DECLARE @ServiceDuration int set @ServiceDuration = (select ServiceDuration from BookingSchedule where BookingScheduleID = (select BookingScheduleID from bookings where bookingref = '" + bookingref.ToString() + "'))"
        'strQuery += "SELECT Table_name + ' - Max:' + convert(nvarchar(50),MaxCover) as table_name ,table_id FROM M_Table b where Table_id in (select items from dbo.Split((select table_set from M_Table_Group where GroupID = " + GroupID.ToString() + "),('#')))"
        'strQuery += "AND Table_id not in (select items from dbo.Split(((select Stuff((SELECT '# ' + b.Allotted_Tables from bookings b "
        'strQuery += "where	b.BookingScheduleDateId = (select BookingScheduleDateId from bookings WHERE bookingref = '" + bookingref.ToString() + "') "

        'strQuery += " AND BookingRef NOT IN (ISNULL((select BookingRef from bookings INNER JOIN tables t ON t.tablenumber = bookingref where BookingScheduleDateId = (select BookingScheduleDateId from bookings where BookingRef = '" + bookingref.ToString() + "') AND Allotted_Tables IS NOT NULL"
        'strQuery += " AND CAST(Bookingtime as time) BETWEEN CAST((select dbo.sec_hours(dbo.Hours_Sec(Convert(varchar(5), DATEADD(minute,-(@ServiceDuration-1),bookingtime), 108))) as Bookingtime from bookings where BookingRef = '" + bookingref.ToString() + "') as time)"
        'strQuery += " AND CAST((select dbo.sec_hours(dbo.Hours_Sec(Convert(varchar(5), DATEADD(minute,@ServiceDuration-1,bookingtime), 108))) as Bookingtime from bookings where BookingRef = '" + bookingref.ToString() + "') as time) and IsCanceled <> 1 and t.closed = 0 and t.datetimeclosed is not NULL),''))"

        'strQuery += "AND Convert(varchar(5), b.bookingtime, 108) BETWEEN (SELECT Convert(varchar(5), DATEADD(minute,(bsh.ServiceDuration-((bsh.ServiceDuration*2)-1)),bks.bookingtime), 108) AS Mservice from bookings bks INNER JOIN BookingSchedule bsh on bsh.BookingScheduleID = bks.BookingScheduleID WHERE bks.bookingref = '" + bookingref.ToString() + "')"
        'strQuery += "AND (SELECT Convert(varchar(5), DATEADD(minute,(bsh.ServiceDuration-1),bks.bookingtime), 108) AS Pservice from bookings bks INNER JOIN BookingSchedule bsh on bsh.BookingScheduleID = bks.BookingScheduleID WHERE bks.bookingref = '" + bookingref.ToString() + "') FOR XML PATH('')), 1, 2, ''))),('#')))"
        Return conn.SelectData(strQuery)
    End Function

    Public Function GetTableNamesByBookingScheduleDateId(ByVal BookingScheduleDateId As Int32, ByVal BookingScheduleID As Int32, ByVal bookingtime As String) As DataSet
        Dim strQuery As String = String.Empty

        strQuery += "SELECT Table_name + ' - Max:' + convert(nvarchar(50),MaxCover) as table_name ,table_id FROM M_Table b where  cast(Table_id as nvarchar(100))  in (select items from dbo.Split((select table_set from M_Table_Group where GroupID =(select GroupID from BookingSchedule where BookingScheduleID=" + BookingScheduleID.ToString() + ")),('#')))"
        strQuery += "AND  cast(Table_id as nvarchar(100))  not in (select items from dbo.Split(((select Stuff((SELECT '# ' + b.Allotted_Tables from bookings b "
        strQuery += "where	b.BookingScheduleDateId =" + BookingScheduleDateId.ToString() + ""
        strQuery += "AND Convert(varchar(5), b.bookingtime, 108) BETWEEN (SELECT Convert(varchar(5), DATEADD(minute,(bsh.ServiceDuration-((bsh.ServiceDuration*2)-1)),'" + bookingtime.ToString() + "'), 108) AS Mservice from BookingSchedule bsh where bsh.BookingScheduleID =(select BookingScheduleID from BookingScheduleDate where BookingScheduleDateId= " + BookingScheduleDateId.ToString() + "))"
        strQuery += "AND (SELECT Convert(varchar(5), DATEADD(minute,(bsh.ServiceDuration-1),'" + bookingtime.ToString() + "'), 108) AS Pservice from BookingSchedule bsh where bsh.BookingScheduleID = (select BookingScheduleID from BookingScheduleDate where BookingScheduleDateId=" + BookingScheduleDateId.ToString() + ")) FOR XML PATH('')), 1, 2, ''))),('#')))"


        Return conn.SelectData(strQuery)
    End Function

End Class

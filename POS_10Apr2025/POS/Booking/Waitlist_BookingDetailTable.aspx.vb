Imports System.Data
Imports Telerik.Web.UI

Partial Class BookingEasy_Waitlist_BookingDetailTable
    Inherits System.Web.UI.Page



    Public ReadOnly Property StoreId() As Integer
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("storeid")) Then
                Try
                    Return Utils.getInteger(Utils.Decrypt(Request.QueryString("storeid").ToString()))
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
    End Property
    Public ReadOnly Property SettingId() As Integer
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("settingid")) Then
                Try
                    Return Utils.getInteger(Utils.Decrypt(Request.QueryString("settingid").ToString()))
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
    End Property
    Public ReadOnly Property BookingDateTime() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("datetime")) Then
                Try
                    Return Utils.Decrypt(Request.QueryString("datetime").ToString())
                Catch ex As Exception
                    Return String.Empty
                End Try
            Else
                Return String.Empty
            End If
        End Get
    End Property
    Public ReadOnly Property NoOfCovers() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("covers")) Then
                Try
                    Return Utils.Decrypt(Request.QueryString("covers").ToString())
                Catch ex As Exception
                    Return String.Empty
                End Try
            Else
                Return String.Empty
            End If
        End Get
    End Property
    Public Property BookingService() As DataTable
        Get
            Dim conn As DBConnection = New DBConnection()
            If (ViewState("BookingService") IsNot Nothing) Then
                Return DirectCast(ViewState("BookingService"), DataTable)
            End If
            Dim dt As DataTable = conn.SelectData("select * from BookingServices where BookingServiceID = -1").Tables(0)
            dt.Columns("BookingServiceID").AutoIncrement = True
            'dt.Columns.Add("ProductName")
            ViewState("BookingService") = dt
            Return dt
        End Get
        Set(ByVal value As DataTable)
            ViewState("BookingService") = value
        End Set
    End Property
    Public ReadOnly Property CurrencySymbol() As String
        Get
            Dim ds As DataSet = Common.GetXmlData()
            If Not ds.Tables("Currency") Is Nothing Then
                Return DirectCast(ds.Tables("Currency").Rows(0)("Symbol").ToString(), String)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property BookingScheduleID() As Integer
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("bookingScheduleId")) Then
                Try
                    Return Utils.getInteger(Utils.Decrypt(Request.QueryString("bookingScheduleId").ToString()))
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property BookingScheduleDateId() As Integer
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("bookingScheduleDateId")) Then
                Try
                    Return Utils.getInteger(Utils.Decrypt(Request.QueryString("bookingScheduleDateId").ToString()))
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property IsOnline() As Integer
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("isonline")) Then
                Try
                    Return Utils.getInteger(Request.QueryString("isonline").ToString())
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
    End Property

    Public Sub FillSoreDeteils()
        Dim conn As DBConnection = New DBConnection()
        Dim str As String = "SELECT StoreName AS Name FROM StoreMaster WHERE OurStoreId = '" + StoreId.ToString() + "'"
        Dim ds As DataSet = conn.SelectData(str)
        If (ds.Tables(0).Rows.Count > 0) Then
            lblStoreName.Text = ds.Tables(0).Rows(0)("Name").ToString()
        End If


        lblDate.Text = BookingDateTime.Replace(" ", " at ")
        lblNoOfCovers.Text = NoOfCovers
    End Sub
    Private Sub SearchClient()
        Try
            Dim SearchOption As Integer = 0
            Dim objCommon As Common = New Common()
            Dim dsField As DataSet = objCommon.GetDefaultField()
            If dsField.Tables(0).Rows.Count > 0 Then
                SearchOption = dsField.Tables(0).Rows(0)("DefaultField")
            Else
                SearchOption = 0
            End If

            Dim ds As DataSet = Common.SearchCustomer(txtSearchClient.Text.Trim(), SearchOption)

            If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
                gvSearchClient.DataSource = ds.Tables(0)
                gvSearchClient.DataBind()
                divSearchResult.Visible = True
                lblSearchMessage.Text = String.Empty
                If txtSearchClient.Text.Trim() = "" Then
                    gvSearchClient.Visible = False
                    divSearchResult.Visible = False
                End If
                If txtSearchClient.Text.Trim() <> "" Then
                    gvSearchClient.Visible = True
                    divSearchResult.Visible = True
                End If
            Else
                divSearchResult.Visible = False
                lblSearchMessage.Text = "No Records Found."

                If lblSearchOption.Text = "Email" Then
                    CType(ucCustomer1.FindControl("df_Email1st"), TextBox).Text = txtSearchClient.Text.Trim()
                End If
                If lblSearchOption.Text = "Mobile" Then
                    CType(ucCustomer1.FindControl("df_Mobile"), TextBox).Text = txtSearchClient.Text.Trim()
                End If

                If lblSearchOption.Text = "Email or Mobile" Then
                    Dim reg As New Regex("^\d$")

                    If IsNumeric(txtSearchClient.Text) Then
                        CType(ucCustomer1.FindControl("df_Mobile"), TextBox).Text = txtSearchClient.Text.Trim()
                        CType(ucCustomer1.FindControl("df_Email1st"), TextBox).Text = ""

                    Else
                        CType(ucCustomer1.FindControl("df_Email1st"), TextBox).Text = txtSearchClient.Text.Trim()
                        CType(ucCustomer1.FindControl("df_Mobile"), TextBox).Text = ""
                    End If

                End If

            End If
            'divMessageBox.Visible = False
            divMessageBox.Attributes.Add("class", "")
            lbExistingCust.Text = String.Empty
            'gvExistingCustomer.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetAccountDetail(ByVal accountId As Integer)
        Dim com As Common = New Common()
        Dim dr As DataRow = com.GetAccountById(accountId)
        If (dr IsNot Nothing) Then
            If CType(ucCustomer1.FindControl("df_FirstName"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_FirstName"), TextBox).Text = dr("firstname").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_LastName"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_LastName"), TextBox).Text = dr("lastname").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_Mobile"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_Mobile"), TextBox).Text = dr("mobile").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_PhoneWork"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_PhoneWork"), TextBox).Text = dr("phonework").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_Email1st"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_Email1st"), TextBox).Text = dr("email1st").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_Street_1"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_Street_1"), TextBox).Text = dr("street_1").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_Street_2"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_Street_2"), TextBox).Text = dr("Street_2").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_Street_3"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_Street_3"), TextBox).Text = dr("Street_3").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_city"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_city"), TextBox).Text = dr("City").ToString().Trim
            End If
            If CType(ucCustomer1.FindControl("df_Pcode"), TextBox) IsNot Nothing Then
                CType(ucCustomer1.FindControl("df_Pcode"), TextBox).Text = dr("pcode").ToString().Trim
            End If
            'ucCustomer1.FirstName = dr("firstname").ToString()
            'ucCustomer1.LastName = dr("lastname").ToString()
            'ucCustomer1.Mobile = dr("mobile").ToString()
            'ucCustomer1.WorkNumber = dr("phonework").ToString()
            'ucCustomer1.Email = dr("email1st").ToString()
            'ucCustomer1.Address1 = dr("street_1").ToString()
            'ucCustomer1.Address2 = dr("Street_2").ToString()
            'ucCustomer1.Address3 = dr("Street_3").ToString()
            'ucCustomer1.City = dr("City").ToString()
            'ucCustomer1.PostCode = dr("pcode").ToString()
            Dim oDbConnection As New DBConnection()
            Dim sql As String = "SELECT * FROM M_Custom_Field WHERE Enable_Val = 1 AND Static_Field = 'Custom' ORDER BY CAST(Sorting_no AS INT) ASC"
            Dim sort As DataTable = oDbConnection.SelectData(sql).Tables(0)
            Dim val As String = sort.Rows.Count
            If val > 0 Then
                For i As Integer = 0 To (val - 1)
                    Dim drr As DataRow = sort.Rows(i)
                    Dim query As DataSet = oDbConnection.SelectData("SELECT * FROM M_Custom_Field WHERE FieldId = '" + drr("FieldID").ToString() + "' ")
                    If query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomText") Then

                        If query.Tables(0).Rows(0)("FieldName").ToString = "Textbox" Then

                            CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), TextBox).Text = dr(query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString).ToString()

                        ElseIf query.Tables(0).Rows(0)("FieldName").ToString = "Checkbox" Then

                            Dim chkval As String = dr(query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString).ToString
                            If Not chkval = "" Then
                                Dim aryTextFile() As String = chkval.Split("#")

                                'Dim querychk As DataSet = oDbConnection.SelectData("select " + query.Tables(0).Rows(0)("Mapping_Field").ToString + " from account where AccountId = " + accountId.ToString + "")
                                'Dim a As String = query.Tables(0).Rows(0)("Mapping_Field").ToString
                                'Dim b As String = querychk.ToString
                                'Dim c() As String = b.Split("#")
                                'Dim ind As String = Array.Find(aryTextFile, Function(s) s = c.ToString)

                                For p = 0 To UBound(aryTextFile)
                                    If aryTextFile(p).ToString = "0" Then
                                        CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBoxList).Items(p).Selected = False
                                    Else
                                        CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBoxList).Items(p).Selected = True
                                    End If

                                Next (p)
                            End If

                        Else
                            Dim querydropdown As DataSet = oDbConnection.SelectData("select " + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + " from account where AccountId = " + accountId.ToString + "")
                            Dim a As String = query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString
                            If Not querydropdown.Tables(0).Rows(0)(a) = "" Then
                                CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), RadComboBox).SelectedValue = dr(query.Tables(0).Rows(0)("Mapping_Field_Alies"))
                            End If
                        End If

                    ElseIf query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomNum") Then

                        Dim querydropdown As DataSet = oDbConnection.SelectData("select " + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + " from account where AccountId = " + accountId.ToString + "")
                        Dim a As String = query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString
                        If Not querydropdown.Tables(0).Rows(0)(a) = Nothing Then
                            CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), TextBox).Text = dr(query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString).ToString()
                        Else
                            CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), TextBox).Text = ""
                        End If

                    ElseIf query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomFlag") Then
                        Dim chkval As String = dr(query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString).ToString
                        If chkval = "0" Then
                            CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBox).Checked = False
                        ElseIf chkval = False Then
                            CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBox).Checked = False
                        Else
                            CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBox).Checked = True
                        End If
                    Else 'date
                        Dim querydropdown As DataSet = oDbConnection.SelectData("select " + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + " from account where AccountId = " + accountId.ToString + "")
                        Dim a As String = query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString
                        Dim b As String = querydropdown.Tables(0).Rows(0)(a).ToString
                        If Not b = "" Then
                            CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), RadDatePicker).SelectedDate = dr(query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString).ToString()
                        End If

                    End If

                Next

            End If

            hdnAccountID.Value = accountId.ToString()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("AllowSetting") = Nothing
        If Not IsPostBack Then
            If (StoreId > 0) Then
                'BindProduct()
                FillSoreDeteils()
                Dim a = BookingScheduleID()
                Dim b = BookingScheduleDateId()
                'If Sessions.UserID <> 0 Then
                '    pnlSearchClient.Visible = True
                'Else
                '    pnlSearchClient.Visible = False
                'End If
                BindSearch()
            End If
        End If
    End Sub

    Public Sub BindSearch()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetDefaultField()
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("DefaultField") = 0 Then
                    lblSearchOption.Text = "Email"
                ElseIf ds.Tables(0).Rows(0)("DefaultField") = 1 Then
                    lblSearchOption.Text = "Mobile"
                ElseIf ds.Tables(0).Rows(0)("DefaultField") = 2 Then
                    lblSearchOption.Text = "Email or Mobile"
                End If
            Else
                lblSearchOption.Text = "Email"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearchClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchClient.Click
        SearchClient()
    End Sub
    Protected Sub gvSearchClient_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvSearchClient.ItemCommand
        If (e.CommandName = "SelectClient") Then
            SearchClient()
        ElseIf (e.CommandName = "RowClick") Then
            Dim accountid As String = TryCast(e.Item, GridDataItem).GetDataKeyValue("accountid").ToString()
            GetAccountDetail(Convert.ToInt32(accountid))
        End If
    End Sub
    Protected Sub gvExistingCustomer_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvExistingCustomer.ItemCommand
        If (e.CommandName = "RowClick") Then
            Dim accountid As String = TryCast(e.Item, GridDataItem).GetDataKeyValue("accountid").ToString()
            GetAccountDetail(Convert.ToInt32(accountid))
        End If
    End Sub
    Protected Sub gvSearchClient_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles gvSearchClient.PageIndexChanged
        gvSearchClient.CurrentPageIndex = e.NewPageIndex
        SearchClient()
    End Sub
    Protected Sub btnConfitmBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfitmBooking.Click      
            Insert()
    End Sub

    Public Sub Insert()
        Dim conn As DBConnection = New DBConnection()
        Dim insertval As String = ""
        Dim insertfield As String = ""
        Dim updateVal As String = ""
        Dim text_val As String = ""
        Dim date_val As New Date
        Dim d_val As String = ""
        Dim oDbConnection As New DBConnection()
        Dim sql As String = "SELECT * FROM M_Custom_Field WHERE Enable_Val = 1 AND Static_Field = 'Custom' ORDER BY CAST(Sorting_no AS INT) ASC"
        Dim sort As DataTable = oDbConnection.SelectData(sql).Tables(0)
        Dim val As String = sort.Rows.Count
        If val > 0 Then
            For i As Integer = 0 To (val - 1)
                Dim dr As DataRow = sort.Rows(i)
                Dim query As DataSet = oDbConnection.SelectData("SELECT * FROM M_Custom_Field WHERE FieldId = '" + dr("FieldID").ToString() + "' ")
                If query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomText") Then

                    If query.Tables(0).Rows(0)("FieldName").ToString = "Textbox" Then

                        text_val = CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), TextBox).Text

                    ElseIf query.Tables(0).Rows(0)("FieldName").ToString = "Checkbox" Then

                        Dim chkcount As String = CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBoxList).Items.Count
                        Dim chkval As String
                        text_val = Nothing
                        Dim chkval1 As String = Nothing
                        For j As Integer = 0 To (chkcount - 1)
                            If CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBoxList).Items(j).Selected Then
                                chkval = CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBoxList).Items(j).ToString
                                If chkval1 = Nothing Then
                                    text_val = chkval
                                    chkval1 = "1"
                                Else
                                    text_val += "#" + chkval
                                End If
                            Else
                                If text_val = Nothing Then
                                    text_val = "0"
                                    chkval1 = "1"
                                Else
                                    text_val += "#" + "0"
                                End If
                            End If
                        Next
                    Else 'Dropdown
                        text_val = CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), RadComboBox).SelectedValue
                    End If
                ElseIf query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomNum") Then

                    text_val = CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), TextBox).Text

                ElseIf query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomFlag") Then

                    Dim flag_val As String = CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), CheckBox).Checked
                    If flag_val = "True" Then
                        text_val = "1"
                    Else
                        text_val = "0"
                    End If

                Else 'date
                    If Not CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), RadDatePicker).SelectedDate Is Nothing Then
                        d_val = "Yes"
                        date_val = CType(ucCustomer1.FindControl("df_" + query.Tables(0).Rows(0)("Mapping_Field").ToString), RadDatePicker).SelectedDate
                    Else
                        d_val = "No"
                    End If
                End If

                If query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomText") Then

                    insertfield += " ,[" + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + "]"
                    insertval += " ," + "'" + text_val + "'"
                    updateVal += " ,[" + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + "] = '" + text_val + "'"

                ElseIf query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomNum") Then

                    insertfield += " ,[" + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + "]"
                    insertval += " ," + text_val
                    updateVal += " ,[" + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + "] = " + text_val

                ElseIf query.Tables(0).Rows(0)("Mapping_Field").ToString.Contains("CustomFlag") Then

                    insertfield += " ,[" + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + "]"
                    insertval += " ," + text_val
                    updateVal += " ,[" + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + "] = " + text_val

                Else
                    If Not d_val = "No" Then
                        Dim date1 As String = date_val.ToString("MM/dd/yyyy")
                        insertfield += " ,[" + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + "]"
                        insertval += " ," + "'" + date1 + "'"
                        updateVal += " ,[" + query.Tables(0).Rows(0)("Mapping_Field_Alies").ToString + "] = '" + date1 + "'"
                    End If

                End If

            Next

        End If

        Dim searchVal As Integer
        Dim objCommon As Common = New Common()
        Dim dsField As DataSet = objCommon.GetDefaultField()
        If dsField.Tables(0).Rows.Count > 0 Then
            searchVal = dsField.Tables(0).Rows(0)("DefaultField")
        Else
            searchVal = 0
        End If

        Dim t0 As TextBox
        t0 = CType(ucCustomer1.FindControl("df_Email1st"), TextBox)

        Dim t1 As TextBox
        t1 = CType(ucCustomer1.FindControl("df_Mobile"), TextBox)

        Dim t2 As TextBox
        t2 = CType(ucCustomer1.FindControl("df_Both"), TextBox)

        If searchVal = 0 And t0 Is Nothing Then
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            lbExistingCust.Text = "Email field required in customer details."

        ElseIf searchVal = 1 And t1 Is Nothing Then
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            lbExistingCust.Text = "Mobile number field required in customer details."

        ElseIf (searchVal = 2) And (t0 Is Nothing And t1 Is Nothing) Then
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            lbExistingCust.Text = "Email or mobile number field required in customer details."
        Else
            divMessageBox.Visible = False
            Dim em1 As String = ""
            Dim mo1 As String = ""

            If Not t0 Is Nothing Then
                em1 = t0.Text.ToString()
            End If
            If Not t1 Is Nothing Then
                mo1 = t1.Text.ToString()
            End If

            If objCommon.CheckDuplicate(em1, mo1, searchVal, Utils.ParseInt(hdnAccountID.Value)) Then
                ' Message for duplicate Email Exists & Bind Grid For Comfirmation
                Dim ds As DataSet = Common.GetCustomerBySearchBy(em1, mo1, searchVal)
                gvExistingCustomer.DataSource = ds.Tables(0)
                gvExistingCustomer.DataBind()
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-danger")
                lbExistingCust.Text = "We have a client with the same " + lblSearchOption.Text + ", please confirm this is you or give us a new " + lblSearchOption.Text + ""
                gvExistingCustomer.Visible = True
            Else
                divMessageBox.Visible = False
                divMessageBox.Attributes.Add("class", "")
                lbExistingCust.Text = String.Empty
                gvExistingCustomer.Visible = False

                Dim drSetting As DataRow = objCommon.GetBookingSettings(StoreId)
                Dim drStore As DataRow = conn.SingleRow("Select * from Store where StoreID = " & StoreId.ToString())
                ' Insert/Update Customer Detail and fetch its account id

                Dim FirstName As String = ""
                Dim LastName As String = ""
                Dim Mobile As String = ""
                Dim WorkNumber As String = ""
                Dim Email As String = ""
                Dim Address1 As String = ""
                Dim Address2 As String = ""
                Dim Address3 As String = ""
                Dim City As String = ""
                Dim PostCode As String = ""

                If CType(ucCustomer1.FindControl("df_FirstName"), TextBox) IsNot Nothing Then
                    FirstName = CType(ucCustomer1.FindControl("df_FirstName"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_LastName"), TextBox) IsNot Nothing Then
                    LastName = CType(ucCustomer1.FindControl("df_LastName"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_Mobile"), TextBox) IsNot Nothing Then
                    Mobile = CType(ucCustomer1.FindControl("df_Mobile"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_PhoneWork"), TextBox) IsNot Nothing Then
                    WorkNumber = CType(ucCustomer1.FindControl("df_PhoneWork"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_Email1st"), TextBox) IsNot Nothing Then
                    Email = CType(ucCustomer1.FindControl("df_Email1st"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_Street_1"), TextBox) IsNot Nothing Then
                    Address1 = CType(ucCustomer1.FindControl("df_Street_1"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_Street_2"), TextBox) IsNot Nothing Then
                    Address2 = CType(ucCustomer1.FindControl("df_Street_2"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_Street_3"), TextBox) IsNot Nothing Then
                    Address3 = CType(ucCustomer1.FindControl("df_Street_3"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_city"), TextBox) IsNot Nothing Then
                    City = CType(ucCustomer1.FindControl("df_city"), TextBox).Text
                End If
                If CType(ucCustomer1.FindControl("df_Pcode"), TextBox) IsNot Nothing Then
                    PostCode = CType(ucCustomer1.FindControl("df_Pcode"), TextBox).Text
                End If

                Dim acctId As Integer = objCommon.SaveAccount(Utils.ParseInt(hdnAccountID.Value), Utils.ParseInt(drSetting("Guestaccount")), FirstName,
                                                              LastName, Mobile, WorkNumber, Email, Address1,
                                                              Address2, Address3, City, PostCode, insertfield, insertval, updateVal)

                'Dim ds1 As New DataSet()

                Dim oClsDataccess As New ClsDataccess
                Dim oColSqlPar As ColSqlparram = New ColSqlparram()

                oColSqlPar.Add("@WaitingListDate", Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd"), SqlDbType.DateTime)
                oColSqlPar.Add("@Cover", NoOfCovers, SqlDbType.Int)
                oColSqlPar.Add("@Comment", txtComment.Text)
                oColSqlPar.Add("@BookingScheduleID", 0, SqlDbType.Int)
                oColSqlPar.Add("@BookingScheduleDateId", 0, SqlDbType.Int)
                oColSqlPar.Add("@BookingSettingsid", SettingId, SqlDbType.Int)
                oColSqlPar.Add("@accountid", Utils.ParseInt(acctId), SqlDbType.Int)
                oColSqlPar.Add("@waiting_no", 0, SqlDbType.Int)
                oColSqlPar.Add("@status", 0, SqlDbType.Int)
                oColSqlPar.Add("@IPAddress", Request.UserHostAddress)
                oColSqlPar.Add("@CreatedBy", Sessions.UserID, SqlDbType.Int)
                oClsDataccess.GetdatasetSp("P_M_WaitList", oColSqlPar, "SearchTableGetTimeSlots")

                Session("wl") = "1"

                If Not Session("BookingDetails_waiting") = Nothing Then
                    If Convert.ToInt32(Session("BookingDetails_waiting").ToString()) = 1 Then
                        Response.Redirect("Dashboard.aspx?TabId=" + Utils.Encrypt(Session("mytab").ToString()) + "")
                        Session("BookingDetails_waiting") = Nothing
                    Else
                        Response.Redirect("BookingDetails.aspx")
                        Session("BookingDetails_waiting") = Nothing
                    End If
                Else

                End If


            End If
        End If

    End Sub

    'Public Sub BindProduct()
    '    Dim objCommon As Common = New Common()
    '    Dim ds As DataSet = objCommon.GetProducts(StoreId)
    '    gvServices.DataSource = ds.Tables(0)
    '    gvServices.DataBind()

    'End Sub


    Protected Sub gvExistingCustomer_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvExistingCustomer.NeedDataSource
        Try
            RebindgvExistingCustomer()
        Catch ex As Exception

        End Try

    End Sub

    Public Sub RebindgvExistingCustomer()
        Try
            Dim searchVal As Integer
            Dim objCommon As Common = New Common()
            Dim dsField As DataSet = objCommon.GetDefaultField()
            If dsField.Tables(0).Rows.Count > 0 Then
                searchVal = dsField.Tables(0).Rows(0)("DefaultField")
            Else
                searchVal = 0
            End If

            Dim ds As DataSet = Common.GetCustomerBySearchBy(CType(ucCustomer1.FindControl("df_Email1st"), TextBox).Text, CType(ucCustomer1.FindControl("df_Mobile"), TextBox).Text, searchVal)
            gvExistingCustomer.DataSource = ds.Tables(0)
            gvExistingCustomer.DataBind()
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            lbExistingCust.Text = "We have a client with the same " + lblSearchOption.Text + ", please confirm this is you or give us a new " + lblSearchOption.Text + ""
            gvExistingCustomer.Visible = True
        Catch ex As Exception

        End Try


    End Sub
End Class

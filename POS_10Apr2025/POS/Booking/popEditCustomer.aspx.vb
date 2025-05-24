Imports System.Data
Imports Telerik.Web.UI

Partial Class BookingEasy_popEditCustomer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Private Sub GetAccountDetail(ByVal accountId As Integer)
        'Dim com As Common = New Common()
        'Dim dr As DataRow = com.GetAccountById(accountId)
        'If (dr IsNot Nothing) Then
        '    'ucCustomer1.FirstName = dr("firstname").ToString()
        '    'ucCustomer1.LastName = dr("lastname").ToString()
        '    'ucCustomer1.Mobile = dr("mobile").ToString()
        '    'ucCustomer1.WorkNumber = dr("phonework").ToString()
        '    'ucCustomer1.Email = dr("email1st").ToString()
        '    'ucCustomer1.Address1 = dr("street_1").ToString()
        '    'ucCustomer1.Address2 = dr("Street_2").ToString()
        '    'ucCustomer1.Address3 = dr("Street_3").ToString()
        '    'ucCustomer1.City = dr("City").ToString()
        '    'ucCustomer1.PostCode = dr("pcode").ToString()
        '    If CType(ucCustomer1.FindControl("df_FirstName"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_FirstName"), TextBox).Text = dr("firstname").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_LastName"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_LastName"), TextBox).Text = dr("lastname").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_Mobile"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_Mobile"), TextBox).Text = dr("mobile").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_PhoneWork"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_PhoneWork"), TextBox).Text = dr("phonework").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_Email1st"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_Email1st"), TextBox).Text = dr("email1st").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_Street_1"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_Street_1"), TextBox).Text = dr("street_1").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_Street_2"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_Street_2"), TextBox).Text = dr("Street_2").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_Street_3"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_Street_3"), TextBox).Text = dr("Street_3").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_city"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_city"), TextBox).Text = dr("City").ToString()
        '    End If
        '    If CType(ucCustomer1.FindControl("df_Pcode"), TextBox) IsNot Nothing Then
        '        CType(ucCustomer1.FindControl("df_Pcode"), TextBox).Text = dr("pcode").ToString()
        '    End If
        'End If
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

        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Dim accId As Integer = Utils.getInteger(Request.QueryString("accId"))
        'If (accId > 0) Then

        '    Dim conn As DBConnection = New DBConnection()

        '    Dim que As String = "Update ACCOUNT Set" & vbNewLine
        '    que &= " FirstName='" & CType(ucCustomer1.FindControl("df_FirstName"), TextBox).Text & "'," & vbNewLine
        '    que &= " LastName='" & CType(ucCustomer1.FindControl("df_FirstName"), TextBox).Text & "'" & vbNewLine
        '    que &= " Where AccountID ='" & Utils.getInteger(Request.QueryString("accId")) & "'"
        '    que &= " Select AddressID from account"
        '    que &= " Where AccountID ='" & Utils.getInteger(Request.QueryString("accId")) & "'"

        '    Dim addId As Integer = Utils.getInteger(conn.ExecuteScalar(que))

        '    If (addId > 0) Then
        '        que = "Update Address Set" & vbNewLine
        '        que &= " Mobile='" & CType(ucCustomer1.FindControl("df_Mobile"), TextBox).Text & "'," & vbNewLine
        '        que &= " PhoneWork='" & CType(ucCustomer1.FindControl("df_PhoneWork"), TextBox).Text & "'," & vbNewLine
        '        que &= " Email1st='" & CType(ucCustomer1.FindControl("df_Email1st"), TextBox).Text & "'," & vbNewLine
        '        que &= " Street_1='" & CType(ucCustomer1.FindControl("df_Street_1"), TextBox).Text & "'," & vbNewLine
        '        que &= " Street_2='" & CType(ucCustomer1.FindControl("df_Street_2"), TextBox).Text & "'," & vbNewLine
        '        que &= " Street_3='" & CType(ucCustomer1.FindControl("df_Street_3"), TextBox).Text & "'," & vbNewLine
        '        que &= " City='" & CType(ucCustomer1.FindControl("df_city"), TextBox).Text & "'," & vbNewLine
        '        que &= " PCode='" & CType(ucCustomer1.FindControl("df_Pcode"), TextBox).Text & "'" & vbNewLine
        '        que &= " Where AddressID ='" & addId & "'"
        '        Utils.getInteger(conn.ExecuteNonQuery(que))
        '    End If

        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
        'End If
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
        Dim objCommon As Common = New Common()
        Dim acctId As Integer = objCommon.SaveAccount(Utils.getInteger(Request.QueryString("accId")), 0, FirstName,
                                                      LastName, Mobile, WorkNumber, Email, Address1,
                                                      Address2, Address3, City, PostCode, insertfield, insertval, updateVal)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            If (Request.QueryString("accId") IsNot Nothing) Then
                GetAccountDetail(Utils.getInteger(Request.QueryString("accId")))
            End If
        End If
    End Sub
End Class

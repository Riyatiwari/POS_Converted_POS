Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Data
Imports System.Data.SqlClient
'Imports System.Windows.Forms
Imports System.Web

Partial Class Bookingeasy_UserControl_Customer
    Inherits System.Web.UI.UserControl
    Public Property Category_type() As String
        Get
            Try
                Return hfCategory.Value
            Catch
                Return ""
            End Try
        End Get
        Set(ByVal value As String)
            hfCategory.Value = value
        End Set
    End Property

    'Public Property LastName() As String
    '    Get
    '        Try
    '            Return txtLname.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtLname.Text = value
    '    End Set
    'End Property

    'Public Property Mobile() As String
    '    Get
    '        Try
    '            Return txtMobile.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtMobile.Text = value
    '    End Set
    'End Property

    'Public Property WorkNumber() As String
    '    Get
    '        Try
    '            Return txtWorkNumber.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtWorkNumber.Text = value
    '    End Set
    'End Property

    'Public Property Email() As String
    '    Get
    '        Try
    '            Return txtEmail.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtEmail.Text = value
    '    End Set
    'End Property

    'Public Property Address1() As String
    '    Get
    '        Try
    '            Return txtAddress1.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtAddress1.Text = value
    '    End Set
    'End Property

    'Public Property Address2() As String
    '    Get
    '        Try
    '            Return txtAddress2.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtAddress2.Text = value
    '    End Set
    'End Property

    'Public Property Address3() As String
    '    Get
    '        Try
    '            Return txtAddress3.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtAddress3.Text = value
    '    End Set
    'End Property

    'Public Property City() As String
    '    Get
    '        Try
    '            Return txtCity.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtCity.Text = value
    '    End Set
    'End Property

    'Public Property PostCode() As String
    '    Get
    '        Try
    '            Return txtPostCode.Text()
    '        Catch
    '            Return ""
    '        End Try
    '    End Get
    '    Set(ByVal value As String)
    '        txtPostCode.Text = value
    '    End Set
    'End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            getfield()
    End Sub

    Protected Sub getfield()
            Dim oDbConnection As New DBConnection()
        Dim sql As String = "SELECT * FROM M_Custom_Field WHERE Enable_Val = 1   and  isnull(categoty_type,'B') = '" + hfCategory.Value.ToString() + "'   ORDER BY CAST(Sorting_no AS INT) ASC"
        Dim sort As DataTable = oDbConnection.SelectData(sql).Tables(0)
            Dim td As HtmlGenericControl
            Dim val As String = sort.Rows.Count
            If val > 0 Then

                For i As Integer = 0 To (val - 1)
                    Dim dr As DataRow = sort.Rows(i)
                    Dim query As DataSet = oDbConnection.SelectData("SELECT * FROM M_Custom_Field WHERE FieldId = '" + dr("FieldID").ToString() + "' ")

                    Dim lbl1 As New Label
                    lbl1.ID = "lbl" & i.ToString()
                    lbl1.CssClass = "control-label"
                    lbl1.Text = query.Tables(0).Rows(0)("FieldLable").ToString
                    td = GetTdLabel(i)
                    td.Visible = True
                    td.Controls.Add(lbl1)

                    If query.Tables(0).Rows(0)("FieldName").ToString = "Textbox" Then

                        Dim textbox1 As New TextBox
                        Dim validation As String = "0"
                        textbox1.CssClass = "form-control"
                        Dim id As String = "df_" & query.Tables(0).Rows(0)("Mapping_Field").ToString
                        textbox1.ID = id
                        textbox1.ValidationGroup = "SearchClient"
                        If query.Tables(0).Rows(0)("FieldType").ToString = 1 Then
                            textbox1.TextMode = TextBoxMode.SingleLine
                        ElseIf query.Tables(0).Rows(0)("FieldType").ToString = 2 Then
                            textbox1.TextMode = TextBoxMode.MultiLine
                        Else
                            textbox1.TextMode = TextBoxMode.Password
                        End If
                        textbox1.MaxLength = query.Tables(0).Rows(0)("MaxVal").ToString
                        td = GetTdControl(i)
                    td.Visible = True
                        td.Controls.Add(textbox1)
                        If query.Tables(0).Rows(0)("Required_Val").ToString = 1 Then
                            validation = "0"
                            RequiredFun(i, id, validation)
                        End If
                        If query.Tables(0).Rows(0)("Validation_Val").ToString = 2 Then
                            validation = "1"
                            RequiredFun(i, id, validation)
                        ElseIf query.Tables(0).Rows(0)("Validation_Val").ToString = 3 Then
                            validation = "2"
                            RequiredFun(i, id, validation)
                        End If

                    ElseIf query.Tables(0).Rows(0)("FieldName").ToString = "Checkbox" Then

                    If Not query.Tables(0).Rows(0)("Choice").ToString = "" Then
                        Dim item As String = query.Tables(0).Rows(0)("Choice").ToString
                        Dim aryTextFile() As String = item.Split("#")
                        Dim checkbox1 As New CheckBoxList
                        checkbox1.DataSource = aryTextFile
                        checkbox1.DataBind()
                        Dim id As String = "df_" & query.Tables(0).Rows(0)("Mapping_Field").ToString
                        checkbox1.ID = id
                        'checkbox1.CellSpacing = "5"
                        checkbox1.CellPadding = "6"
                        checkbox1.RepeatDirection = RepeatDirection.Horizontal
                        checkbox1.CssClass = "chkmrng"
                        checkbox1.AutoPostBack = True
                        td = GetTdControl(i)
                        td.Visible = True
                        td.Style("padding-top") = "6px"
                        td.Controls.Add(checkbox1)

                    Else

                        Dim checkbox1 As New CheckBox
                        Dim id As String = "df_" & query.Tables(0).Rows(0)("Mapping_Field").ToString
                        checkbox1.ID = id
                        checkbox1.Checked = False
                        td = GetTdControl(i)
                        td.Visible = True
                        td.Style("padding-top") = "6px"
                        td.Controls.Add(checkbox1)

                    End If

                ElseIf query.Tables(0).Rows(0)("FieldName").ToString = "Dropdown" Then

                    Dim combo As New RadComboBox
                    Dim validation As String = ""
                    Dim id As String = "df_" & query.Tables(0).Rows(0)("Mapping_Field").ToString
                    combo.ID = id
                    combo.Skin = "MetroTouch"
                    combo.ValidationGroup = "SearchClient"
                    Dim item As String = query.Tables(0).Rows(0)("Choice").ToString
                    Dim aryTextFile() As String = item.Split("#")
                    combo.DataSource = aryTextFile
                    combo.DataBind()

                    'Dim p As Integer
                    'For p = 0 To UBound(aryTextFile)
                    '    Dim item1 As New RadComboBoxItem(aryTextFile(p))
                    '    combo.Items.Add(item1)
                    'Next (p)
                    'combo.SelectedIndex = -1
                    ' combo.SelectedItem.Text = ""
                    'Dim item1 As New RadComboBoxItem()
                    'combo.Items.Add(item1)
                    td = GetTdControl(i)
                    td.Visible = True
                    td.Controls.Add(combo)
                    If query.Tables(0).Rows(0)("Required_Val").ToString = 1 Then
                        validation = "4"
                        RequiredFun(i, id, validation)
                    End If

                Else

                    Dim datepicker As New RadDatePicker
                    Dim validation As String = "0"
                    Dim id As String = "df_" & query.Tables(0).Rows(0)("Mapping_Field").ToString
                    datepicker.ID = id
                    'datepicker.DbSelectedDate = Date.Now
                    datepicker.EnableTyping = True
                    datepicker.DateInput.DateFormat = "dd/MM/yyyy"
                    datepicker.Skin = "MetroTouch"
                    td = GetTdControl(i)
                    td.Visible = True
                    td.Controls.Add(datepicker)
                    If query.Tables(0).Rows(0)("Required_Val").ToString = 1 Then
                        validation = "0"
                        RequiredFun(i, id, validation)
                    End If
                End If
            Next
            End If
    End Sub

    Private Function RequiredFun(ByVal i As Integer, ByVal id As String, ByVal validation As String) As String
        Try
            If validation = "0" Then
                Dim requ As New RequiredFieldValidator
                requ.ID = "Required" & id.ToString() & validation.ToString()
                requ.ControlToValidate = id
                requ.Text = "*"
                requ.Display = ValidatorDisplay.Dynamic
                requ.ForeColor = Drawing.Color.Red
                requ.CssClass = "rfv"
                Dim reqtd As HtmlGenericControl
                reqtd = GetTdControl(i)
                reqtd.Controls.Add(requ)
                Return "Yes"
            ElseIf validation = "4" Then
                Dim requ As New RequiredFieldValidator
                requ.ID = "Required" & id.ToString() & validation.ToString()
                requ.ControlToValidate = id
                requ.Text = "*"
                requ.Display = ValidatorDisplay.Dynamic
                requ.ForeColor = Drawing.Color.Red
                requ.CssClass = "rfv"
                requ.InitialValue = "Select"
                Dim reqtd As HtmlGenericControl
                reqtd = GetTdControl(i)
                reqtd.Controls.Add(requ)
                Return "Yes"
            ElseIf validation = "1" Then
                Dim requ As New RegularExpressionValidator
                requ.ID = "Regular" & id.ToString() & validation.ToString()
                requ.ControlToValidate = id
                requ.Display = ValidatorDisplay.Dynamic
                requ.ValidationExpression = "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                requ.Text = "Enter Email In Proper Format"
                requ.ForeColor = Drawing.Color.Red
                requ.CssClass = "rfv"
                Dim reqtd As HtmlGenericControl
                reqtd = GetTdControl(i)
                reqtd.Controls.Add(requ)
                Return "Yes"
            ElseIf validation = "2" Then
                Dim requ As New RegularExpressionValidator
                requ.ID = "Regular" & id.ToString() & validation.ToString()
                requ.ControlToValidate = id
                requ.EnableClientScript = True
                requ.Display = ValidatorDisplay.Dynamic
                requ.ValidationExpression = "^\d+$"
                requ.Text = "Please Enter Only Numbers"
                requ.ForeColor = Drawing.Color.Red
                requ.CssClass = "rfv"
                Dim reqtd As HtmlGenericControl
                reqtd = GetTdControl(i)
                reqtd.Controls.Add(requ)
                Return "Yes"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function GetTdLabel(ByVal tdInd As Integer) As HtmlGenericControl
        Try
            Dim maindiv As HtmlGenericControl = CType(Me.FindControl("frm" + tdInd.ToString + "1"), HtmlGenericControl)
            maindiv.Visible = True
            Return CType(Me.FindControl("Tdd" + tdInd.ToString + "1"), HtmlGenericControl)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function GetTdControl(ByVal tdInd As Integer) As HtmlGenericControl
        Try
            Return CType(Me.FindControl("Tdd" + tdInd.ToString + "2"), HtmlGenericControl)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

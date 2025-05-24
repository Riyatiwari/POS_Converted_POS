Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.IO

Partial Class BookingEasy_DynamicFieldMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            BindFieldGrid()
            If Not IsPostBack Then
                ViewState("ChoiceData") = Nothing
                'BindFieldDDL()
                btnAdd.Visible = True
                ddlField.Enabled = True
                'BindHiddenField()
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub

    'Protected Sub BindFieldDDL()
    '    Dim objCommon As Common = New Common()
    '    Dim ds As DataSet = objCommon.GetStaticFieldData()
    '    ddlStaticField.DataSource = ds.Tables(0)
    '    ddlStaticField.DataValueField = "Static_Field"
    '    ddlStaticField.DataTextField = "Static_Field"
    '    ddlStaticField.DataBind()
    '    ddlStaticField.ClearSelection()

    'End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        If ddlField.SelectedValue = 1 Then
            DivTextBoxField.Visible = True
            DivCheckboxField.Visible = False
            DivDropdownField.Visible = False
            DivDateTime.Visible = False
            ddlField.Enabled = False
            btnAdd.Visible = False
            reqValidation.Enabled = True
            ddlType.Enabled = True
            RangeValidator1.MaximumValue = 40
            RangeValidator1.ErrorMessage = "Max 40"
            RangeValidator1.Enabled = True
            lblTxtMax.Visible = True
            lblTxtMax.InnerText = "( Maximum 40 )"
            clrtext()
        ElseIf ddlField.SelectedValue = 2 Then
            DivTextBoxField.Visible = False
            DivCheckboxField.Visible = True
            DivDropdownField.Visible = False
            DivDateTime.Visible = False
            ddlField.Enabled = False
            btnAdd.Visible = False
            reqValidation.Enabled = True
            ddlType.Enabled = True
            clrcheck()
        ElseIf ddlField.SelectedValue = 3 Then
            DivTextBoxField.Visible = False
            DivCheckboxField.Visible = False
            DivDropdownField.Visible = True
            DivDateTime.Visible = False
            ddlField.Enabled = False
            btnAdd.Visible = False
            reqValidation.Enabled = True
            ddlType.Enabled = True
            clrdrop()
        Else
            DivTextBoxField.Visible = False
            DivCheckboxField.Visible = False
            DivDropdownField.Visible = False
            DivDateTime.Visible = True
            ddlField.Enabled = False
            btnAdd.Visible = False
            reqValidation.Enabled = True
            ddlType.Enabled = True
            clrdate()
        End If
        ViewState("ChoiceData") = Nothing
    End Sub

#Region "Clear Functions"

    Protected Sub clrtext()
        hdnUpdate.Value = "0"
        txtTextboxLable.Text = ""
        ddlType.SelectedValue = "1"
        chkSelect.Checked = False
        reqValidation.SelectedValue = "1"
        txtTextboxMaximum.Text = ""
        txtTextboxSortingNo.Text = ""
        ddlTextboxStatus.SelectedValue = "1"
        btnCancelTabSetting.Visible = True
        reqValidation.Enabled = True
        ddlType.Enabled = True
    End Sub

    Protected Sub clrcheck()
        hdnUpdate.Value = "0"
        txtCheckboxLbl.Text = ""
        ViewState("ChoiceData") = Nothing
        txtCheckBoxSorting.Text = ""
        ddlCheckBoxStatus.SelectedValue = "1"
        btnCancelTabSetting.Visible = True
    End Sub

    Protected Sub clrdrop()
        hdnUpdate.Value = "0"
        txtLableDropdown.Text = ""
        chkDropdownReq.Checked = False
        txtDropDownSorting.Text = ""
        txtDropDownStatus.SelectedValue = ""
        btnCancelTabSetting.Visible = True
    End Sub

    Protected Sub clrdate()
        hdnUpdate.Value = "0"
        txtDTLbl.Text = ""
        chkDateReq.Checked = False
        txtDatetimeSorting.Text = ""
        ddlDateStatus.SelectedValue = "1"
        btnCancelTabSetting.Visible = True
    End Sub

    Protected Sub clr()
        hdnUpdate.Value = "0"
        DivTextBoxField.Visible = False
        DivCheckboxField.Visible = False
        DivDropdownField.Visible = False
        DivDateTime.Visible = False
        btnAdd.Visible = True
        ddlField.Enabled = True
        ddlField.SelectedValue = "1"
        ViewState("ChoiceData") = Nothing
        btnCancelTabSetting.Visible = False
        ddlField.Enabled = True
        btnAdd.Visible = True
        reqValidation.Enabled = True
        ddlType.Enabled = True
    End Sub

#End Region

#Region "Submit Button"

    Protected Sub btntxt_Click(sender As Object, e As System.EventArgs) Handles btntxt.Click
        Dim conn As DBConnection = New DBConnection()
        Dim strQuery As String
        Dim strQuery2 As String
        Dim strQuery3 As String
        Dim req As String
        If chkSelect.Checked Then
            req = "1"
        Else
            req = "0"
        End If
        Dim lbl1 As String = Replace(txtTextboxLable.Text, ":", "")
        Dim lbl As String = lbl1 + " :"
        If hdnUpdate.Value = "0" Then

            Dim mapqry As String = ""
            If reqValidation.SelectedValue = "3" Then
                mapqry &= "Begin tran "
                mapqry &= "Select top 1 Tracking_Name FROM T_Custome_FieldTracking "
                mapqry &= "Where Tracking_Name LIKE '%' + (select distinct Field_MapName from M_FieldType where Field_Type = 'Number') + '%' "
                mapqry &= "AND Tracking_Enable = '0' "
                mapqry &= "ORDER BY Sorting_No rollback tran"
            Else
                mapqry &= "Begin tran "
                mapqry &= "Select top 1 Tracking_Name FROM T_Custome_FieldTracking "
                mapqry &= "Where Tracking_Name LIKE '%' + (select distinct Field_MapName from M_FieldType where Field_Type = 'Text') + '%' "
                mapqry &= "AND Tracking_Enable = '0' "
                mapqry &= "ORDER BY Sorting_No rollback tran"
            End If
            Dim sort As DataTable = conn.SelectData(mapqry).Tables(0)
            Dim val As String = sort.Rows.Count
            If val > 0 Then
                Dim maxnum As String = ""
                If txtTextboxMaximum.Text = "" Then
                    maxnum = "40"
                Else
                    maxnum = txtTextboxMaximum.Text
                End If
                Dim dr As DataRow = conn.SingleRow(mapqry)
                Dim string_1 As String = lbl1.ToString.Replace(" ", "_")
                Dim static_field As String = "Custom"
                strQuery = " INSERT INTO M_Custom_Field(FieldName,FieldLable,FieldType,Required_Val,MaxVal,Validation_Val,Sorting_no,Enable_Val,Static_Field,Mapping_Field,Mapping_Field_Alies,categoty_type ) VALUES ('Textbox','" + lbl + "','" + ddlType.SelectedValue.ToString + "','" + req + "','" + txtTextboxMaximum.Text + "','" + reqValidation.SelectedValue.ToString + "','" + txtTextboxSortingNo.Text + "','" + ddlTextboxStatus.SelectedValue.ToString + "','" + static_field.ToString + "','" + dr("Tracking_Name") + "','" + string_1.ToString + "','" + RadDropdowntype.SelectedValue.ToString + "')"
                conn.Ins_Upd_Del(strQuery)
                strQuery2 = " UPDATE T_Custome_FieldTracking set Tracking_Enable = '1' where Tracking_Name = '" + dr("Tracking_Name") + "' "
                conn.Ins_Upd_Del(strQuery2)
                strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Tracking_Name") + "', @column_change = '" + string_1 + "'"
                conn.Ins_Upd_Del(strQuery3)

            Else
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-danger")
                lblMessageBox.Text = "Limit Exist"
            End If
        Else 'update
            Dim lbl11 As String = Replace(txtTextboxLable.Text, ":", "").Trim
            Dim string_11 As String = lbl11.ToString.Replace(" ", "_")
            
            Dim mapqry1 As String = "Select * from M_Custom_Field where FieldID='" + hdnUpdate.Value + "'"
            Dim dr As DataRow = conn.SingleRow(mapqry1)
            If dr("Static_Field") = "Custom" Then
                strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "',FieldType='" + ddlType.SelectedValue.ToString + "',Required_Val='" + req + "', MaxVal='" + txtTextboxMaximum.Text + "', Validation_Val='" + reqValidation.SelectedValue.ToString + "', Sorting_no='" + txtTextboxSortingNo.Text + "', Enable_Val='" + ddlTextboxStatus.SelectedValue.ToString + "', Mapping_Field_Alies = '" + string_11.ToString + "' ,categoty_type ='" + RadDropdowntype.SelectedValue.ToString + "' WHERE FieldID='" + hdnUpdate.Value + "'"
                conn.Ins_Upd_Del(strQuery)
                strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Mapping_Field_Alies") + "', @column_change = '" + string_11 + "'"
                conn.Ins_Upd_Del(strQuery3)
            Else
                strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "',FieldType='" + ddlType.SelectedValue.ToString + "',Required_Val='" + req + "', MaxVal='" + txtTextboxMaximum.Text + "', Validation_Val='" + reqValidation.SelectedValue.ToString + "', Sorting_no='" + txtTextboxSortingNo.Text + "', Enable_Val='" + ddlTextboxStatus.SelectedValue.ToString + "',categoty_type ='" + RadDropdowntype.SelectedValue.ToString + "' WHERE FieldID='" + hdnUpdate.Value + "'"
                conn.Ins_Upd_Del(strQuery)
            End If
            
        End If
        clrtext()
        clr()
        BindFieldGrid2()
        hdnUpdate.Value = "0"
        btnAddField.Visible = True
        btnCancelField.Visible = False
        ddlStaticField.Enabled = True
        ddlStaticField.ClearSelection()
        divFieldType.Visible = False
    End Sub

    Protected Sub btnCheckBox_Click(sender As Object, e As System.EventArgs) Handles btnCheckBox.Click
        Dim conn As DBConnection = New DBConnection()
        Dim strQuery As String
        Dim strQuery2 As String
        Dim strQuery3 As String
        Dim items As String
        Dim mapqry As String = ""
        If ViewState("ChoiceData") = Nothing Then
            items = ""
            mapqry &= "Begin tran "
            mapqry &= "Select top 1 Tracking_Name FROM T_Custome_FieldTracking "
            mapqry &= "Where Tracking_Name LIKE '%' + (select distinct Field_MapName from M_FieldType where Field_Type = 'Flag') + '%' "
            mapqry &= "AND Tracking_Enable = '0' "
            mapqry &= "ORDER BY Sorting_No rollback tran"
        Else
            items = ViewState("ChoiceData").ToString
            mapqry &= "Begin tran "
            mapqry &= "Select top 1 Tracking_Name FROM T_Custome_FieldTracking "
            mapqry &= "Where Tracking_Name LIKE '%' + (select distinct Field_MapName from M_FieldType where Field_Type = 'Text') + '%' "
            mapqry &= "AND Tracking_Enable = '0' "
            mapqry &= "ORDER BY Sorting_No rollback tran"
        End If
        Dim lbl1 As String = Replace(txtCheckboxLbl.Text, ":", "")
        Dim lbl As String = lbl1 + " :"
        If hdnUpdate.Value = "0" Then

            Dim sort As DataTable = conn.SelectData(mapqry).Tables(0)
            Dim val As String = sort.Rows.Count
            If val > 0 Then
                Dim dr As DataRow = conn.SingleRow(mapqry)
                Dim string_1 As String = lbl1.ToString.Replace(" ", "_")
                Dim static_field As String = "Custom"
                strQuery = " INSERT INTO M_Custom_Field(FieldName,FieldLable,Choice,Sorting_no,Enable_Val,Static_Field,Mapping_Field,Mapping_Field_Alies,categoty_type ) VALUES ('Checkbox','" + lbl + "','" + items + "','" + txtCheckBoxSorting.Text + "','" + ddlCheckBoxStatus.SelectedValue.ToString + "','" + static_field.ToString + "','" + dr("Tracking_Name") + "','" + string_1.ToString + "','" + RadDropdowntype.SelectedValue.ToString + "')"
                conn.Ins_Upd_Del(strQuery)
                strQuery2 = " UPDATE T_Custome_FieldTracking set Tracking_Enable = '1' where Tracking_Name = '" + dr("Tracking_Name") + "' "
                conn.Ins_Upd_Del(strQuery2)
                strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Tracking_Name") + "', @column_change = '" + string_1 + "'"
                conn.Ins_Upd_Del(strQuery3)
            Else
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-danger")
                lblMessageBox.Text = "Limit Exist"
            End If
        Else 'update
            Dim lbl11 As String = Replace(txtCheckboxLbl.Text, ":", "").Trim
            Dim string_11 As String = lbl11.ToString.Replace(" ", "_")

            Dim mapqry1 As String = "Select * from M_Custom_Field where FieldID='" + hdnUpdate.Value + "'"
            Dim dr As DataRow = conn.SingleRow(mapqry1)
            If dr("Static_Field") = "Custom" Then
                strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "', Choice='" + items + "', Sorting_no='" + txtCheckBoxSorting.Text + "', Enable_Val='" + ddlCheckBoxStatus.SelectedValue.ToString + "', Mapping_Field_Alies = '" + string_11.ToString + "' ,categoty_type ='" + RadDropdowntype.SelectedValue.ToString + "'  WHERE FieldID='" + hdnUpdate.Value + "'"
                conn.Ins_Upd_Del(strQuery)
                strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Mapping_Field_Alies") + "', @column_change = '" + string_11 + "'"
                conn.Ins_Upd_Del(strQuery3)
            Else
                strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "', Choice='" + items + "', Sorting_no='" + txtCheckBoxSorting.Text + "', Enable_Val='" + ddlCheckBoxStatus.SelectedValue.ToString + "' ,categoty_type ='" + RadDropdowntype.SelectedValue.ToString + "'  WHERE FieldID='" + hdnUpdate.Value + "'"
                conn.Ins_Upd_Del(strQuery)
            End If

        End If
        clrcheck()
        clr()
        BindFieldGrid2()
        hdnUpdate.Value = "0"
        btnAddField.Visible = True
        btnCancelField.Visible = False
        ddlStaticField.Enabled = True
        ddlStaticField.ClearSelection()
        divFieldType.Visible = False

    End Sub

    Protected Sub btnDropDown_Click(sender As Object, e As System.EventArgs) Handles btnDropDown.Click
        Dim items As String
        If ViewState("ChoiceData") = Nothing Then
            lblError.Visible = True
        Else
            Dim conn As DBConnection = New DBConnection()
            Dim strQuery As String
            Dim strQuery2 As String
            Dim strQuery3 As String
            Dim req As String

            If chkDropdownReq.Checked Then
                req = "1"
            Else
                req = "0"
            End If
            Dim lbl1 As String = Replace(txtLableDropdown.Text, ":", "")
            Dim lbl As String = lbl1 + " :"
            items = ViewState("ChoiceData").ToString
            If hdnUpdate.Value = "0" Then

                Dim mapqry As String = ""
                mapqry &= "Begin tran "
                mapqry &= "Select top 1 Tracking_Name FROM T_Custome_FieldTracking "
                mapqry &= "Where Tracking_Name LIKE '%' + (select distinct Field_MapName from M_FieldType where Field_Type = 'Text') + '%' "
                mapqry &= "AND Tracking_Enable = '0' "
                mapqry &= "ORDER BY Sorting_No rollback tran"

                Dim sort As DataTable = conn.SelectData(mapqry).Tables(0)
                Dim val As String = sort.Rows.Count
                If val > 0 Then
                    Dim dr As DataRow = conn.SingleRow(mapqry)
                    Dim string_1 As String = lbl1.ToString.Replace(" ", "_")
                    Dim static_field As String = "Custom"
                    strQuery = " INSERT INTO M_Custom_Field(FieldName,FieldLable,Required_Val,Choice,Sorting_no,Enable_Val,Static_Field,Mapping_Field,Mapping_Field_Alies,categoty_type) VALUES ('Dropdown','" + lbl + "','" + req + "','" + items + "','" + txtDropDownSorting.Text + "','" + txtDropDownStatus.SelectedValue.ToString + "','" + static_field.ToString + "','" + dr("Tracking_Name") + "','" + string_1.ToString + "','" + RadDropdowntype.SelectedValue.ToString + "')"
                    conn.Ins_Upd_Del(strQuery)
                    strQuery2 = " UPDATE T_Custome_FieldTracking set Tracking_Enable = '1' where Tracking_Name = '" + dr("Tracking_Name") + "' "
                    conn.Ins_Upd_Del(strQuery2)
                    strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Tracking_Name") + "', @column_change = '" + string_1 + "'"
                    conn.Ins_Upd_Del(strQuery3)
                Else
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "alert alert-danger")
                    lblMessageBox.Text = "Limit Exist"
                End If
            Else 'update
                Dim lbl11 As String = Replace(txtLableDropdown.Text, ":", "").Trim
                Dim string_11 As String = lbl11.ToString.Replace(" ", "_")

                Dim mapqry1 As String = "Select * from M_Custom_Field where FieldID='" + hdnUpdate.Value + "'"
                Dim dr As DataRow = conn.SingleRow(mapqry1)
                If dr("Static_Field") = "Custom" Then
                    strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "',Required_Val='" + req + "',Choice='" + items + "',Sorting_no='" + txtDropDownSorting.Text + "',Enable_Val='" + txtDropDownStatus.SelectedValue.ToString + "', Mapping_Field_Alies = '" + string_11.ToString + "',categoty_type ='" + RadDropdowntype.SelectedValue.ToString + "' WHERE FieldID='" + hdnUpdate.Value + "'"
                    conn.Ins_Upd_Del(strQuery)
                    strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Mapping_Field_Alies") + "', @column_change = '" + string_11 + "'"
                    conn.Ins_Upd_Del(strQuery3)
                Else
                    strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "',Required_Val='" + req + "',Choice='" + items + "',Sorting_no='" + txtDropDownSorting.Text + "',Enable_Val='" + txtDropDownStatus.SelectedValue.ToString + "' ,categoty_type ='" + RadDropdowntype.SelectedValue.ToString + "' WHERE FieldID='" + hdnUpdate.Value + "'"
                    conn.Ins_Upd_Del(strQuery)
                End If

                'strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "',Required_Val='" + req + "',Choice='" + items + "',Sorting_no='" + txtDropDownSorting.Text + "',Enable_Val='" + txtDropDownStatus.SelectedValue.ToString + "', Mapping_Field_Alies = '" + string_11.ToString + "' WHERE FieldID='" + hdnUpdate.Value + "'"
                'conn.Ins_Upd_Del(strQuery)
                'Dim mapqry1 As String = "Select * from M_Custom_Field where FieldID='" + hdnUpdate.Value + "'"
                'Dim dr As DataRow = conn.SingleRow(mapqry1)
                'If dr("Static_Field") = "Custom" Then
                '    strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Mapping_Field_Alies") + "', @column_change = '" + string_11 + "'"
                '    conn.Ins_Upd_Del(strQuery3)
                'End If
            End If
            clrdrop()
            clr()
            BindFieldGrid2()
            hdnUpdate.Value = "0"
            btnAddField.Visible = True
            btnCancelField.Visible = False
            ddlStaticField.Enabled = True
            ddlStaticField.ClearSelection()
            divFieldType.Visible = False
        End If
    End Sub

    Protected Sub btnDate_Click(sender As Object, e As System.EventArgs) Handles btnDate.Click
        Dim conn As DBConnection = New DBConnection()
        Dim strQuery As String
        Dim strQuery2 As String
        Dim strQuery3 As String
        Dim req As String
        If chkDateReq.Checked Then
            req = "1"
        Else
            req = "0"
        End If
        Dim lbl1 As String = Replace(txtDTLbl.Text, ":", "")
        Dim lbl As String = lbl1 + " :"
        If hdnUpdate.Value = "0" Then

            Dim mapqry As String = ""
            mapqry &= "Begin tran "
            mapqry &= "Select top 1 Tracking_Name FROM T_Custome_FieldTracking "
            mapqry &= "Where Tracking_Name LIKE '%' + (select distinct Field_MapName from M_FieldType where Field_Type = 'Date') + '%' "
            mapqry &= "AND Tracking_Enable = '0' "
            mapqry &= "ORDER BY Sorting_No rollback tran"

            Dim sort As DataTable = conn.SelectData(mapqry).Tables(0)
            Dim val As String = sort.Rows.Count
            If val > 0 Then
                Dim dr As DataRow = conn.SingleRow(mapqry)
                Dim string_1 As String = lbl1.ToString.Replace(" ", "_")
                Dim static_field As String = "Custom"
                strQuery = " INSERT INTO M_Custom_Field(FieldName,FieldLable,Required_Val,Sorting_no,Enable_Val,Static_Field,Mapping_Field,Mapping_Field_Alies,categoty_type ) VALUES ('Date','" + lbl + "','" + req + "','" + txtDatetimeSorting.Text + "','" + ddlDateStatus.SelectedValue.ToString + "','" + static_field.ToString + "','" + dr("Tracking_Name") + "','" + string_1.ToString + "','" + RadDropdowntype.SelectedValue.ToString + "')"
                conn.Ins_Upd_Del(strQuery)
                strQuery2 = " UPDATE T_Custome_FieldTracking set Tracking_Enable = '1' where Tracking_Name = '" + dr("Tracking_Name") + "' "
                conn.Ins_Upd_Del(strQuery2)
                strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Tracking_Name") + "', @column_change = '" + string_1 + "'"
                conn.Ins_Upd_Del(strQuery3)
            Else
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-danger")
                lblMessageBox.Text = "Limit Exist"
            End If
        Else ' update
            Dim lbl11 As String = Replace(txtDTLbl.Text, ":", "").Trim
            Dim string_11 As String = lbl11.ToString.Replace(" ", "_")

            Dim mapqry1 As String = "Select * from M_Custom_Field where FieldID='" + hdnUpdate.Value + "'"
            Dim dr As DataRow = conn.SingleRow(mapqry1)
            If dr("Static_Field") = "Custom" Then
                strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "',Required_Val='" + req + "',Sorting_no='" + txtDatetimeSorting.Text + "',Enable_Val='" + ddlDateStatus.SelectedValue.ToString + "', Mapping_Field_Alies = '" + string_11.ToString + "' ,categoty_type ='" + RadDropdowntype.SelectedValue.ToString + "'  WHERE FieldID='" + hdnUpdate.Value + "'"
                conn.Ins_Upd_Del(strQuery)
                strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Mapping_Field_Alies") + "', @column_change = '" + string_11 + "'"
                conn.Ins_Upd_Del(strQuery3)
            Else
                strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "',Required_Val='" + req + "',Sorting_no='" + txtDatetimeSorting.Text + "',Enable_Val='" + ddlDateStatus.SelectedValue.ToString + "' ,categoty_type ='" + RadDropdowntype.SelectedValue.ToString + "' WHERE FieldID='" + hdnUpdate.Value + "'"
                conn.Ins_Upd_Del(strQuery)
            End If

            'strQuery = " UPDATE M_Custom_Field SET FieldLable='" + lbl + "',Required_Val='" + req + "',Sorting_no='" + txtDatetimeSorting.Text + "',Enable_Val='" + ddlDateStatus.SelectedValue.ToString + "', Mapping_Field_Alies = '" + string_11.ToString + "'  WHERE FieldID='" + hdnUpdate.Value + "'"
            'conn.Ins_Upd_Del(strQuery)
            'Dim mapqry1 As String = "Select * from M_Custom_Field where FieldID='" + hdnUpdate.Value + "'"
            'Dim dr As DataRow = conn.SingleRow(mapqry1)
            'If dr("Static_Field") = "Custom" Then
            '    strQuery3 = "EXEC [proc_column_rename] @column_name = '" + dr("Mapping_Field_Alies") + "', @column_change = '" + string_11 + "'"
            '    conn.Ins_Upd_Del(strQuery3)
            'End If
            End If
            clrdate()
            clr()
            BindFieldGrid2()
            hdnUpdate.Value = "0"
            btnAddField.Visible = True
            btnCancelField.Visible = False
            ddlStaticField.Enabled = True
            ddlStaticField.ClearSelection()
            divFieldType.Visible = False
    End Sub

#End Region

#Region "Add Items Button"

    Protected Sub addChkItems_Click(sender As Object, e As System.EventArgs) Handles addChkItems.Click
        Try
            If ViewState("ChoiceData") = Nothing Then
                ViewState("ChoiceData") = txtCheckboxChoiceItems.Text
            Else
                ViewState("ChoiceData") += "#" + txtCheckboxChoiceItems.Text
            End If
            bindChoiceData()
            txtCheckboxChoiceItems.Text = ""
            txtCheckboxChoiceItems.Focus()
            clrChkItems.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub addDropdownItems_Click(sender As Object, e As System.EventArgs) Handles addDropdownItems.Click
        Try
            If ViewState("ChoiceData") = Nothing Then
                ViewState("ChoiceData") = "Select" + "#" + txtItemsDropdown.Text
            Else
                ViewState("ChoiceData") += "#" + txtItemsDropdown.Text
            End If
            bindDropdownData()
            txtItemsDropdown.Text = ""
            txtItemsDropdown.Focus()
            clrDropdownItems.Visible = True
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Bind Item Data"

    Protected Sub bindChoiceData()
        Try
            Dim ChoiceVal As String = ViewState("ChoiceData")
            gvCheckBox.DataSource = ChoiceVal.ToString.Split("#")
            gvCheckBox.DataBind()
            gvCheckBox.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub bindDropdownData()
        Try
            Dim ChoiceVal As String = ViewState("ChoiceData")
            gvDropDown.DataSource = ChoiceVal.ToString.Split("#")
            gvDropDown.DataBind()
            gvDropDown.Visible = True
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Grid Bind"

    Private Sub BindFieldGrid()
        Dim conn As DBConnection = New DBConnection()
        Dim strQvuery As String = ""
        'strQvuery &= "SELECT Static_Field,FieldID, FieldName, FieldLable, Sorting_no,case categoty_type when 'W' THEN 'WebServices' when  'B' THEN 'Booking' end As categoty_type , case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field WHERE Static_Field <> 'Custom' AND Enable_Val = 1 UNION SELECT Static_Field,FieldID, FieldName, FieldLable, Sorting_no,case categoty_type when 'W' THEN 'WebServices'when  'B' THEN 'Booking' end As categoty_type , case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field ORDER BY Sorting_no ASC" ' WHERE Static_Field = 'Custom'
        strQvuery &= "SELECT Static_Field,FieldID, FieldName, FieldLable, Sorting_no,case categoty_type when 'W' THEN 'WebServices' when  'B' THEN 'Booking' end As categoty_type , case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field WHERE Static_Field <> 'Custom' AND Enable_Val = 1   and  isnull(categoty_type,'B') = '" + RadDropdowntype.SelectedValue.ToString() + "' UNION SELECT Static_Field,FieldID, FieldName, FieldLable, Sorting_no,case categoty_type when 'W' THEN 'WebServices'when  'B' THEN 'Booking' end As categoty_type , case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field where isnull(categoty_type,'B') = '" + RadDropdowntype.SelectedValue.ToString() + "' ORDER BY Sorting_no ASC "
        Dim dss As DataSet = conn.SelectData(strQvuery)
        If dss.Tables(0).Rows.Count > 0 Then
            btnDefaultValue.Visible = False
        Else
            btnDefaultValue.Visible = True
        End If
        gvField.DataSource = dss

    End Sub
    Private Sub BindFieldGrid2()
        Dim conn As DBConnection = New DBConnection()
        Dim strQvuery As String = ""
        'strQvuery &= "SELECT Static_Field,FieldID, FieldName, FieldLable, Sorting_no, case categoty_type when 'W' THEN 'WebServices' when  'B' THEN 'Booking' end As categoty_type ,case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field WHERE Static_Field <> 'Custom' AND Enable_Val = 1 UNION SELECT Static_Field,FieldID, FieldName, FieldLable, Sorting_no,case categoty_type when 'W' THEN 'WebServices' when  'B' THEN 'Booking' end As categoty_type , case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field   ORDER BY Sorting_no ASC"
        strQvuery &= "SELECT Static_Field,FieldID, FieldName, FieldLable, Sorting_no, case categoty_type when 'W' THEN 'WebServices' when  'B' THEN 'Booking' end As categoty_type ,case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field WHERE Static_Field <> 'Custom' AND Enable_Val = 1   and  isnull(categoty_type,'B') = '" + RadDropdowntype.SelectedValue.ToString() + "'  UNION SELECT Static_Field,FieldID, FieldName, FieldLable, Sorting_no,case categoty_type when 'W' THEN 'WebServices' when  'B' THEN 'Booking' end As categoty_type , case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field   where isnull(categoty_type,'B') = '" + RadDropdowntype.SelectedValue.ToString() + "'  ORDER BY Sorting_no ASC"
        'strQvuery &= "SELECT FieldID, FieldName, FieldLable, Sorting_no, case when Enable_Val = 1 then 'YES' when Enable_Val = 0 then 'NO' end AS Enable_Val FROM M_Custom_Field ORDER BY CAST(Sorting_no AS INT) ASC"
        Dim dss As DataSet = conn.SelectData(strQvuery)
        gvField.DataSource = dss
        gvField.DataBind()
    End Sub

#End Region

#Region "Clear Button"

    Protected Sub btnCancelTabSetting_Click(sender As Object, e As System.EventArgs) Handles btnCancelTabSetting.Click
        clrtext()
        clrcheck()
        clrdrop()
        clrdate()
        clr()
        btnAddField.Visible = False
        reqValidation.Enabled = True
        ddlType.Enabled = True
    End Sub

    Protected Sub clrDropdownItems_Click(sender As Object, e As System.EventArgs) Handles clrDropdownItems.Click
        ViewState("ChoiceData") = Nothing
        txtItemsDropdown.Text = ""
        txtItemsDropdown.Focus()
        gvDropDown.Visible = False
        clrDropdownItems.Visible = False
    End Sub

    Protected Sub clrChkItems_Click(sender As Object, e As System.EventArgs) Handles clrChkItems.Click
        ViewState("ChoiceData") = Nothing
        txtCheckboxChoiceItems.Text = ""
        txtCheckboxChoiceItems.Focus()
        gvCheckBox.Visible = False
        clrChkItems.Visible = False
    End Sub

#End Region

#Region "Grid Command & DataBound"

    Protected Sub gvField_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvField.ItemCommand
        If (e.CommandName = "EditField") Then
            Dim idval As String = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("FieldID").ToString())
            Dim strQuery As String = " SELECT * FROM M_Custom_Field WHERE FieldID = " & idval
            Dim conn As DBConnection = New DBConnection()
            Dim dr As DataRow = conn.SingleRow(strQuery)
            If dr("categoty_type").ToString() = "" Then
                RadDropdowntype.SelectedValue = "B"
            Else
                RadDropdowntype.SelectedValue = dr("categoty_type").ToString()
            End If

            If dr("FieldName") = "Textbox" Then
                DivTextBoxField.Visible = True
                DivCheckboxField.Visible = False
                DivDropdownField.Visible = False
                DivDateTime.Visible = False

                ddlStaticField.SelectedValue = Utils.NullToString(dr("Static_Field"))
                ddlStaticField.Enabled = False
                divFieldType.Visible = True
                btnAdd.Visible = False
                btnAddField.Visible = False
                If dr("Static_Field") = "Custom" Then
                    btnCancelTabSetting.Visible = True
                    btnCancelField.Visible = True
                    txtStatus.Visible = True
                    reqValidation.Enabled = True
                    ddlType.Enabled = True
                Else
                    btnCancelTabSetting.Visible = False
                    btnCancelField.Visible = True
                    txtStatus.Visible = True
                    reqValidation.Enabled = False
                    ddlType.Enabled = False
                End If
                If dr("Mapping_Field") = "PhoneWork" Then

                    RangeValidator1.MaximumValue = 30
                    RangeValidator1.ErrorMessage = "Max 30"
                    lblTxtMax.Visible = True
                    lblTxtMax.InnerText = "( Maximum 30 )"

                ElseIf dr("Mapping_Field") = "Email1st" Then

                    RangeValidator1.MaximumValue = 50
                    RangeValidator1.ErrorMessage = "Max 50"
                    lblTxtMax.Visible = True
                    lblTxtMax.InnerText = "( Maximum 50 )"

                ElseIf dr("Mapping_Field") = "Pcode" Then
                    RangeValidator1.MaximumValue = 20
                    RangeValidator1.ErrorMessage = "Max 20"
                    lblTxtMax.Visible = True
                    lblTxtMax.InnerText = "( Maximum 20 )"

                ElseIf dr("Mapping_Field") = "Mobile" Then
                    RangeValidator1.MaximumValue = 30
                    RangeValidator1.ErrorMessage = "Max 30"
                    lblTxtMax.Visible = True
                    lblTxtMax.InnerText = "( Maximum 30 )"

                Else
                    RangeValidator1.MaximumValue = 40
                    RangeValidator1.ErrorMessage = "Max 40"
                    lblTxtMax.Visible = True
                    lblTxtMax.InnerText = "( Maximum 40 )"

                End If

                ddlField.SelectedText = Utils.NullToString(dr("FieldName"))
                ddlField.Enabled = False
                txtTextboxLable.Text = Utils.NullToString(dr("FieldLable").ToString().Replace(":", "").Trim)
                ddlType.SelectedValue = Utils.NullToString(dr("FieldType"))
                If dr("Required_Val") = "1" Then
                    chkSelect.Checked = True
                Else
                    chkSelect.Checked = False
                End If
                reqValidation.SelectedValue = Utils.NullToString(dr("Validation_Val"))
                txtTextboxMaximum.Text = Utils.NullToString(dr("MaxVal"))
                txtTextboxSortingNo.Text = Utils.NullToString(dr("Sorting_no"))
                ddlTextboxStatus.SelectedValue = Utils.NullToString(dr("Enable_Val"))
                hdnUpdate.Value = idval

            ElseIf dr("FieldName") = "Checkbox" Then
                DivTextBoxField.Visible = False
                DivCheckboxField.Visible = True
                DivDropdownField.Visible = False
                DivDateTime.Visible = False

                ddlStaticField.SelectedValue = Utils.NullToString(dr("Static_Field"))
                ddlStaticField.Enabled = False
                divFieldType.Visible = True
                btnAdd.Visible = False
                btnAddField.Visible = False
                If dr("Static_Field") = "Custom" Then
                    btnCancelTabSetting.Visible = True
                    btnCancelField.Visible = True
                    CheckBoxStatus.Visible = True
                Else
                    btnCancelTabSetting.Visible = False
                    btnCancelField.Visible = True
                    CheckBoxStatus.Visible = False
                End If

                ddlField.SelectedText = Utils.NullToString(dr("FieldName"))
                ddlField.Enabled = False
                txtCheckboxLbl.Text = Utils.NullToString(dr("FieldLable").ToString().Replace(":", "").Trim)
                If Not dr("Choice") = Nothing Then
                    ViewState("ChoiceData") = Utils.NullToString(dr("Choice"))
                    bindChoiceData()
                End If
                txtCheckBoxSorting.Text = Utils.NullToString(dr("Sorting_no"))
                ddlCheckBoxStatus.SelectedValue = Utils.NullToString(dr("Enable_Val"))
                hdnUpdate.Value = idval
            ElseIf dr("FieldName") = "Dropdown" Then
                DivTextBoxField.Visible = False
                DivCheckboxField.Visible = False
                DivDropdownField.Visible = True
                DivDateTime.Visible = False

                ddlStaticField.SelectedValue = Utils.NullToString(dr("Static_Field"))
                ddlStaticField.Enabled = False
                divFieldType.Visible = True
                btnAdd.Visible = False
                btnAddField.Visible = False
                If dr("Static_Field") = "Custom" Then
                    btnCancelTabSetting.Visible = True
                    btnCancelField.Visible = True
                    DropDownStatus.Visible = True
                Else
                    btnCancelTabSetting.Visible = False
                    btnCancelField.Visible = True
                    DropDownStatus.Visible = False
                End If

                ddlField.SelectedText = Utils.NullToString(dr("FieldName"))
                ddlField.Enabled = False
                txtLableDropdown.Text = Utils.NullToString(dr("FieldLable").ToString().Replace(":", "").Trim)
                If dr("Required_Val") = "1" Then
                    chkDropdownReq.Checked = True
                Else
                    chkDropdownReq.Checked = False
                End If
                ViewState("ChoiceData") = Utils.NullToString(dr("Choice"))
                clrDropdownItems.Visible = True
                bindDropdownData()
                txtDropDownSorting.Text = Utils.NullToString(dr("Sorting_no"))
                txtDropDownStatus.SelectedValue = Utils.NullToString(dr("Enable_Val"))
                hdnUpdate.Value = idval
            Else 'Date
                DivTextBoxField.Visible = False
                DivCheckboxField.Visible = False
                DivDropdownField.Visible = False
                DivDateTime.Visible = True

                ddlStaticField.SelectedValue = Utils.NullToString(dr("Static_Field"))
                ddlStaticField.Enabled = False
                divFieldType.Visible = True
                btnAdd.Visible = False
                btnAddField.Visible = False
                If dr("Static_Field") = "Custom" Then
                    btnCancelTabSetting.Visible = True
                    btnCancelField.Visible = True
                    DateStatus.Visible = True
                Else
                    btnCancelTabSetting.Visible = False
                    btnCancelField.Visible = True
                    DateStatus.Visible = False
                End If

                ddlField.SelectedText = Utils.NullToString(dr("FieldName"))
                ddlField.Enabled = False
                txtDTLbl.Text = Utils.NullToString(dr("FieldLable").ToString().Replace(":", "").Trim)
                If dr("Required_Val") = "1" Then
                    chkDateReq.Checked = True
                Else
                    chkDateReq.Checked = False
                End If
                txtDatetimeSorting.Text = Utils.NullToString(dr("Sorting_no"))
                ddlDateStatus.SelectedValue = Utils.NullToString(dr("Enable_Val"))
                hdnUpdate.Value = idval
            End If
        ElseIf (e.CommandName = "DeleteField") Then
            Dim conn As DBConnection = New DBConnection()
            Dim tabID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("FieldID").ToString())
            Dim sql_staticfield As String = "SELECT Mapping_Field,Static_Field,Mapping_Field_Alies From M_Custom_Field WHERE FieldID = " & tabID
            Dim dr_staticfield As DataRow = conn.SingleRow(sql_staticfield)
            If dr_staticfield("Static_Field") = "Custom" Then
                'Dim sql1 As String = "SELECT Mapping_Field FROM M_Custom_Field WHERE FieldID = " & tabID
                'Dim dr1 As DataRow = conn.SingleRow(sql1)
                Dim mapping As String = dr_staticfield("Mapping_Field")
                Dim strQuery1 As String = "UPDATE T_Custome_FieldTracking set Tracking_Enable = '0' where Tracking_Name = '" + mapping.ToString + "' "
                conn.Ins_Upd_Del(strQuery1)
                Try


                    Dim strQuery3 As String = "EXEC [proc_column_rename] @column_name = '" + dr_staticfield("Mapping_Field_Alies") + "', @column_change = '" + mapping.ToString + "'"
                    conn.Ins_Upd_Del(strQuery3)
                Catch ex As Exception

                End Try
                Dim strQuery As String = " DELETE FROM M_Custom_Field WHERE FieldID = " & tabID
                conn.Ins_Upd_Del(strQuery)
            Else
                Dim mapping As String = dr_staticfield("Mapping_Field")
                Dim strQuery1 As String = "UPDATE M_Custom_Field set Enable_Val = '0' where Mapping_Field = '" + mapping.ToString + "' "
                conn.Ins_Upd_Del(strQuery1)
            End If
            divMessageBox.Visible = False
            BindFieldGrid2()
            BindFieldGrid()
            clrtext()
            clrcheck()
            clrdrop()
            clrdate()
            clr()
            divMessageBox.Visible = False
            divFieldType.Visible = False
            btnAddField.Visible = True
            ddlStaticField.Enabled = True
            divFieldType.Visible = False
            btnCancelTabSetting.Visible = False
            btnCancelField.Visible = False
            ddlStaticField.ClearSelection()
        End If
    End Sub

    Protected Sub gvField_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvField.ItemCreated
        'e.Item.BackColor = Drawing.Color.Red
        
    End Sub

    Protected Sub gvField_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvField.ItemDataBound

        'If TypeOf e.Item Is GridDataItem Then
        '    Dim items As GridDataItem = DirectCast(e.Item, GridDataItem)
        '    Dim cell As TableCell = DirectCast(items("No"), TableCell)
        '    cell.BackColor = System.Drawing.Color.Red
        'End If
       
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)

            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If

            If Not CType(e.Item.FindControl("HiddenField0"), HiddenField).Value = "Custom" Then
                e.Item.BackColor = Drawing.Color.SkyBlue
            End If
        End If
    End Sub

#End Region

    Protected Sub reqValidation_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles reqValidation.SelectedIndexChanged
        txtTextboxMaximum.Text = ""
        disable()
    End Sub

    Protected Sub disable()
        RangeValidator1.Enabled = False
        If reqValidation.SelectedValue = "1" Or reqValidation.SelectedValue = "2" Then
            RangeValidator1.MaximumValue = 40
            RangeValidator1.ErrorMessage = "Max 40"
            RangeValidator1.Enabled = True
            lblTxtMax.Visible = True
            lblTxtMax.InnerText = "( Maximum 40 )"
        Else
            RangeValidator1.MaximumValue = 10
            RangeValidator1.ErrorMessage = "Max 10"
            RangeValidator1.Enabled = True
            lblTxtMax.Visible = True
            lblTxtMax.InnerText = "( Maximum 10 )"
        End If
    End Sub

    Protected Sub btnCancelField_Click(sender As Object, e As System.EventArgs) Handles btnCancelField.Click
        clrtext()
        clrcheck()
        clrdrop()
        clrdate()
        clr()
        divMessageBox.Visible = False
        divFieldType.Visible = False
        btnAddField.Visible = True
        ddlStaticField.Enabled = True
        divFieldType.Visible = False
        btnCancelTabSetting.Visible = False
        btnCancelField.Visible = False
        ddlStaticField.ClearSelection()
    End Sub

    Protected Sub btnAddField_Click(sender As Object, e As System.EventArgs) Handles btnAddField.Click
        Dim conn As DBConnection = New DBConnection()
        Dim sqlqry As String = "SELECT * From M_Custom_Field WHERE Static_Field = '" + ddlStaticField.SelectedValue.ToString + "' "
        Dim dr As DataRow = conn.SingleRow(sqlqry)
        If ddlStaticField.SelectedValue = "Custom" Then
            ddlStaticField.Enabled = False
            btnAddField.Visible = False
            divFieldType.Visible = True
            divMessageBox.Visible = False
            txtStatus.Visible = True
            CheckBoxStatus.Visible = True
            DropDownStatus.Visible = True
            DateStatus.Visible = True
            btnCancelField.Visible = True
        Else
            If dr("Enable_Val") = 1 Then
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-danger")
                lblMessageBox.Text = "Field Already Added"
                btnAddField.Visible = True
                btnCancelField.Visible = False
            Else
                divMessageBox.Visible = False
                divFieldType.Visible = False
                If dr("FieldName") = "Textbox" Then
                    If dr("Mapping_Field") = "PhoneWork" Then

                        RangeValidator1.MaximumValue = 30
                        RangeValidator1.ErrorMessage = "Max 30"
                        lblTxtMax.Visible = True
                        lblTxtMax.InnerText = "( Maximum 30 )"

                    ElseIf dr("Mapping_Field") = "Email1st" Then

                        RangeValidator1.MaximumValue = 50
                        RangeValidator1.ErrorMessage = "Max 50"
                        lblTxtMax.Visible = True
                        lblTxtMax.InnerText = "( Maximum 50 )"

                    ElseIf dr("Mapping_Field") = "Pcode" Then
                        RangeValidator1.MaximumValue = 20
                        RangeValidator1.ErrorMessage = "Max 20"
                        lblTxtMax.Visible = True
                        lblTxtMax.InnerText = "( Maximum 20 )"

                    ElseIf dr("Mapping_Field") = "Mobile" Then
                        RangeValidator1.MaximumValue = 30
                        RangeValidator1.ErrorMessage = "Max 30"
                        lblTxtMax.Visible = True
                        lblTxtMax.InnerText = "( Maximum 30 )"

                    Else
                        RangeValidator1.MaximumValue = 40
                        RangeValidator1.ErrorMessage = "Max 40"
                        lblTxtMax.Visible = True
                        lblTxtMax.InnerText = "( Maximum 40 )"

                    End If
                    DivTextBoxField.Visible = True
                    DivCheckboxField.Visible = False
                    DivDropdownField.Visible = False
                    DivDateTime.Visible = False
                    txtStatus.Visible = True

                    ddlStaticField.SelectedText = Utils.NullToString(dr("FieldName"))
                    ddlStaticField.Enabled = False
                    Dim lblname As String = Utils.NullToString(dr("FieldLable").ToString().Replace(":", ""))
                    txtTextboxLable.Text = lblname.Trim
                    ddlType.SelectedValue = Utils.NullToString(dr("FieldType"))
                    If dr("Required_Val") = "1" Then
                        chkSelect.Checked = True
                    Else
                        chkSelect.Checked = False
                    End If
                    reqValidation.SelectedValue = Utils.NullToString(dr("Validation_Val"))
                    txtTextboxMaximum.Text = Utils.NullToString(dr("MaxVal"))
                    txtTextboxSortingNo.Text = Utils.NullToString(dr("Sorting_no"))
                    ddlTextboxStatus.SelectedValue = "1"
                    hdnUpdate.Value = dr("FieldID")
                    txtStatus.Visible = True
                    reqValidation.Enabled = False
                    ddlType.Enabled = False
                ElseIf dr("FieldName") = "Checkbox" Then
                    DivTextBoxField.Visible = False
                    DivCheckboxField.Visible = True
                    DivDropdownField.Visible = False
                    DivDateTime.Visible = False

                    ddlStaticField.SelectedText = Utils.NullToString(dr("FieldName"))
                    ddlStaticField.Enabled = False
                    txtCheckboxLbl.Text = Utils.NullToString(dr("FieldLable").ToString().Replace(":", "").Trim)
                    If Not dr("Choice") = Nothing Then
                        ViewState("ChoiceData") = Utils.NullToString(dr("Choice"))
                        bindChoiceData()
                    End If
                    txtCheckBoxSorting.Text = Utils.NullToString(dr("Sorting_no"))
                    ddlCheckBoxStatus.SelectedValue = "1"
                    hdnUpdate.Value = dr("FieldID")
                    CheckBoxStatus.Visible = False
                ElseIf dr("FieldName") = "Dropdown" Then
                    DivTextBoxField.Visible = False
                    DivCheckboxField.Visible = False
                    DivDropdownField.Visible = True
                    DivDateTime.Visible = False

                    ddlStaticField.SelectedText = Utils.NullToString(dr("FieldName"))
                    ddlStaticField.Enabled = False
                    txtLableDropdown.Text = Utils.NullToString(dr("FieldLable").ToString().Replace(":", "").Trim)
                    If dr("Required_Val") = "1" Then
                        chkDropdownReq.Checked = True
                    Else
                        chkDropdownReq.Checked = False
                    End If
                    ViewState("ChoiceData") = Utils.NullToString(dr("Choice"))
                    clrDropdownItems.Visible = True
                    bindDropdownData()
                    txtDropDownSorting.Text = Utils.NullToString(dr("Sorting_no"))
                    txtDropDownStatus.SelectedValue = "1"
                    hdnUpdate.Value = dr("FieldID")
                    DropDownStatus.Visible = False
                Else 'Date
                    DivTextBoxField.Visible = False
                    DivCheckboxField.Visible = False
                    DivDropdownField.Visible = True
                    DivDateTime.Visible = False

                    ddlStaticField.SelectedText = Utils.NullToString(dr("FieldName"))
                    ddlStaticField.Enabled = False
                    txtDTLbl.Text = Utils.NullToString(dr("FieldLable").ToString().Replace(":", "").Trim)
                    If dr("Required_Val") = "1" Then
                        chkDateReq.Checked = True
                    Else
                        chkDateReq.Checked = False
                    End If
                    txtDatetimeSorting.Text = Utils.NullToString(dr("Sorting_no"))
                    ddlDateStatus.SelectedValue = Utils.NullToString(dr("Enable_Val"))
                    hdnUpdate.Value = dr("FieldID")
                    DateStatus.Visible = False
                End If
                btnAddField.Visible = False
                btnCancelField.Visible = True
            End If
        End If
    End Sub

    Protected Sub RadDropdowntype_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs)
        Try
            ifrmaCustomer.Src = "DynamicPage.aspx?cat=" + RadDropdowntype.SelectedValue.ToString()
            BindFieldGrid()
            BindFieldGrid2()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnDefaultValue_Click(sender As Object, e As EventArgs)
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim static_field As String = "Custom"
            Dim strQuery As String
            strQuery = "INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'First Name :', 1, 0, 40, N'1', NULL, 1, 1, N'FirstName', N'First Name', N'First Name','" + RadDropdowntype.SelectedValue.ToString() + "') " +
                                               "INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'Last Name :', 1, 0, 40, N'1', NULL, 2, 1, N'LastName', N'Last Name', N'Last Name','" + RadDropdowntype.SelectedValue.ToString() + "')" +
                                               " INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'Mobile :', 1, 0, 30, N'3', NULL, 3, 1, N'Mobile', N'Mobile', N'Mobile','" + RadDropdowntype.SelectedValue.ToString() + "')" +
                                               " INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'Work Number :', 1, 1, 30, N'3', NULL, 3, 1, N'PhoneWork', N'Work Number', N'PhoneWork','" + RadDropdowntype.SelectedValue.ToString() + "')" +
                                               " INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'Email :', 1, 0, 50, N'2', NULL, 5, 1, N'Email1st', N'Email ID', N'Email1st','" + RadDropdowntype.SelectedValue.ToString() + "')" +
                                               " INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'Address 1 :', 1, 0, 40, N'1', NULL, 6, 1, N'Street_1', N'Address 1', N'Street_1','" + RadDropdowntype.SelectedValue.ToString() + "')" +
                                               " INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'Address :', 1, 0, 40, N'1', NULL, 7, 1, N'Street_2', N'Address 2', N'Street_2','" + RadDropdowntype.SelectedValue.ToString() + "')" +
                                               " INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'Address :', 1, 0, 40, N'1', NULL, 8, 1, N'Street_3', N'Address 3', N'Street_3','" + RadDropdowntype.SelectedValue.ToString() + "')" +
                                                " INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'City :', 1, 1, 40, N'1', NULL, 9, 1, N'city', N'City', N'city','" + RadDropdowntype.SelectedValue.ToString() + "')" +
                                               " INSERT [dbo].[M_Custom_Field] ( [FieldName], [FieldLable], [FieldType], [Required_Val], [MaxVal], [Validation_Val], [Choice], [Sorting_no], [Enable_Val], [Mapping_Field], [Static_Field], [Mapping_Field_Alies],categoty_type) VALUES ( N'Textbox', N'Post Code :', 1, 1, 20, N'1', NULL, 10, 1, N'Pcode', N'Post Code', N'Pcode','" + RadDropdowntype.SelectedValue.ToString() + "')"

            conn.Ins_Upd_Del(strQuery)
            BindFieldGrid()
            BindFieldGrid2()
        Catch ex As Exception

        End Try

    End Sub
End Class

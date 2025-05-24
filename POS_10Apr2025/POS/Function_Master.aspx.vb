Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Drawing

Partial Class Function_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsFunction As New Controller_clsFunction()
    Dim dt As DataTable = New DataTable
    Dim oclsTax As New Controller_clsTax()
    Dim oclsProfile As New Controller_clsProfileMaster()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                oclsBind.BindDepartment(ddldeaprtmentid, Val(Session("cmp_id")))
                oclsBind.BindCourse(ddlcourseid, Val(Session("cmp_id")))
                oclsBind.BindTax(ddltax, Val(Session("cmp_id")))
                oclsBind.BindProfile(rdProfileType, Val(Session("cmp_id")))
                oclsBind.BindPrices_By_Cmp(rdPriceLevel, Val(Session("cmp_id")))
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
                oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(0))
                oclsTax.cmp_id = Val(Session("cmp_id"))
                oclsTax.Value = 5
                Dim dttax As DataTable = oclsTax.SelectByValue()

                If dttax.Rows.Count > 0 Then
                    ddltax.SelectedValue = dttax.Rows(0)("tax_id").ToString()
                End If
                If Not Session("mainfunction_id") = Nothing Then
                    BindFunction()
                    oclsBind.BindParentFunction(ddParent, Val(Session("cmp_id")))
                    oclsBind.BindEditPanel(Radeditpanel, Val(Session("cmp_id")), Val(Session("mainfunction_id")))
                    'txtCode.ReadOnly = True
                Else
                    bindSortingNo()
                    oclsBind.BindMachineFunction(RadMachine, Val(Session("cmp_id")), 0)
                    oclsBind.BindParentFunction(ddParent, Val(Session("cmp_id")))
                    'oclsBind.BindFunctionCode1(ddCode, Val(Session("cmp_id")), Val(Session("function_id")), 0, 0)
                    'radBackcolorpicker.SelectedColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
                    BindFunctionGrid()
                    'oclsBind.BindEditPanel(Radeditpanel, Val(Session("cmp_id")))
                End If

                oclsBind.BindFunctionType(ddCaption, Val(Session("cmp_id")))
                oclsBind.BindCardPaymentType(ddlPayType, Val(Session("cmp_id")))
                'oclsBind.BindFunctionCode(ddCode, Val(Session("cmp_id")), Val(Session("function_id")))
                oclsBind.bindPayment(ddPayment, Val(Session("cmp_id")))
                'oclsBind.BindFunctionCode1(ddCode, Val(Session("cmp_id")), Val(Session("function_id")), 0)
                'oclsBind.BindPanel(ddPanel, Val(Session("cmp_id")))
                chkActive.Checked = True
                radBackcolorpicker.SelectedColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            End If
        Catch ex As Exception
            LogHelper.Error("Function_Master:Page_Load" + ex.Message)
        End Try
    End Sub
    Private Sub bindSortingNo()
        Try
            oclsFunction.cmp_id = Val(Session("cmp_id"))
            oclsFunction.form_name = "Function"
            Dim dtTable As DataTable = oclsFunction.Select1()
            If dtTable.Rows.Count > 0 Then
                txtSortinigNo.Text = Val(dtTable.Rows(0)("SNO"))
            End If
        Catch ex As Exception

            LogHelper.Error("Function_Master:bindSortingNo" + ex.Message)
        End Try
    End Sub

    Private Sub BindFunction()
        Try
            oclsFunction.cmp_id = Val(Session("cmp_id"))
            oclsFunction.mainfunction_id = Val(Session("mainfunction_id"))
            Dim dtMainFunction As DataTable = oclsFunction.SelectMainFunction()
            Dim Edit_dt As DataTable = oclsFunction.SelectFunction_Details()

            If dtMainFunction.Rows.Count > 0 Then
                txtMainFun.Text = dtMainFunction.Rows(0)("name").ToString
                RadMachine.ClearSelection()
                oclsBind.BindMachineFunction(RadMachine, Val(Session("cmp_id")), Val(dtMainFunction.Rows(0)("machine_id").ToString))
                RadMachine.SelectedValue = dtMainFunction.Rows(0)("machine_id").ToString
                oclsBind.BindSwapMachineFunction(RadSwapMachine, Val(Session("cmp_id")), Val(dtMainFunction.Rows(0)("machine_id").ToString))
                If dtMainFunction.Rows(0)("is_active").ToString = "Yes" Then
                    ChkmainActive.Checked = True
                Else
                    ChkmainActive.Checked = False
                End If

            End If

            dt.Columns.Add("id", GetType(Integer))
            dt.Columns.Add("cmp_id", GetType(Integer))
            dt.Columns.Add("name", GetType(String))
            dt.Columns.Add("code", GetType(String))
            dt.Columns.Add("caption_type", GetType(String))
            dt.Columns.Add("is_active", GetType(Integer))
            dt.Columns.Add("ip_address", GetType(String))
            dt.Columns.Add("login_id", GetType(Integer))
            dt.Columns.Add("item", GetType(Decimal))
            dt.Columns.Add("back_color", GetType(String))
            dt.Columns.Add("for_color", GetType(String))
            dt.Columns.Add("machine_id", GetType(Integer))
            dt.Columns.Add("big_button", GetType(Integer))
            dt.Columns.Add("Payment_id", GetType(Integer))
            dt.Columns.Add("pay_type", GetType(Integer))
            dt.Columns.Add("pay_sub_type", GetType(String))
            dt.Columns.Add("is_groupby_dept", GetType(Integer))
            dt.Columns.Add("is_groupby_course", GetType(Integer))
            dt.Columns.Add("dept_id", GetType(String))
            dt.Columns.Add("course_id", GetType(String))
            dt.Columns.Add("Panel_Id", GetType(Integer))
            dt.Columns.Add("Parent_id", GetType(Integer))
            dt.Columns.Add("Is_Parent", GetType(Integer))
            dt.Columns.Add("Function_Id", GetType(Integer))
            dt.Columns.Add("platformAdd", GetType(String))
            dt.Columns.Add("clientToken", GetType(String))
            dt.Columns.Add("accessToken", GetType(String))
            dt.Columns.Add("serviceid", GetType(String))
            dt.Columns.Add("EOHOAmount", GetType(Decimal))
            dt.Columns.Add("tax_id", GetType(Integer))
            dt.Columns.Add("Total_Value", GetType(Decimal))
            dt.Columns.Add("Sales_Handling_Fee", GetType(Decimal))
            dt.Columns.Add("Payment_Handling_Fee", GetType(Decimal))
            dt.Columns.Add("Tax_Amount", GetType(Decimal))
            dt.Columns.Add("profile_id", GetType(Integer))
            dt.Columns.Add("DefaultDateTime", GetType(String))
            dt.Columns.Add("ZR_VenueID", GetType(Integer))
            dt.Columns.Add("ZR_LocationID", GetType(Integer))
            dt.Columns.Add("ZR_TillID", GetType(String))
            dt.Columns.Add("CardPayType", GetType(Integer))

            Dim val1 As String = "F"

            For i = 1 To 30
                Dim row As DataRow = dt.NewRow()
                row("id") = i
                row("cmp_id") = 0
                row("name") = ""
                row("code") = val1 + i.ToString()
                row("caption_type") = ""
                row("is_active") = 1
                row("ip_address") = ""
                row("login_id") = 0
                row("item") = 0.00
                row("back_color") = "#1B7BBD"
                row("for_color") = "#FFFFFF"
                row("machine_id") = 0
                row("big_button") = 0
                row("Payment_id") = 0
                row("pay_type") = 0
                row("pay_sub_type") = ""
                row("is_groupby_dept") = 0
                row("is_groupby_course") = 0
                row("dept_id") = 0
                row("course_id") = 0
                row("Panel_Id") = 0
                row("Parent_id") = 0
                row("Is_Parent") = 0
                row("Function_Id") = 0
                row("platformAdd") = ""
                row("clientToken") = ""
                row("accessToken") = ""
                row("serviceid") = ""
                row("EOHOAmount") = 0
                row("tax_id") = 0
                row("Total_Value") = 0
                row("Sales_Handling_Fee") = 0
                row("Payment_Handling_Fee") = 0
                row("Tax_Amount") = 0
                row("profile_id") = 0
                row("DefaultDateTime") = ""
                row("ZR_VenueID") = 0
                row("ZR_LocationID") = 0
                row("ZR_TillID") = ""
                row("CardPayType") = 0


                dt.Rows.Add(row)
            Next
            ViewState("Data_dt") = dt

            For Each row As DataRow In Edit_dt.Rows
                For Each row1 As DataRow In dt.Rows
                    If row1("code").ToString() = row("code").ToString() Then
                        row1("cmp_id") = row("cmp_id")
                        row1("name") = row("name")
                        row1("code") = row("code")
                        row1("caption_type") = row("caption_type")
                        row1("is_active") = row("is_active")
                        row1("ip_address") = row("ip_address")
                        row1("login_id") = row("login_id")
                        row1("item") = row("item")
                        row1("back_color") = row("back_color")
                        row1("for_color") = row("for_color")
                        row1("machine_id") = row("machine_id")
                        row1("big_button") = row("big_button")
                        row1("Payment_id") = row("Payment_id")
                        row1("pay_type") = row("pay_type")
                        row1("pay_sub_type") = row("pay_sub_type")
                        row1("is_groupby_dept") = row("is_groupby_dept")
                        row1("is_groupby_course") = row("is_groupby_course")
                        row1("dept_id") = row("dept_id")
                        row1("course_id") = row("course_id")
                        row1("Panel_Id") = row("Panel_Id")
                        row1("Parent_id") = row("Parent_id")
                        row1("Is_Parent") = row("Parent_id")
                        row1("Function_Id") = row("Function_Id")
                        row1("platformAdd") = row("platformAdd").ToString()
                        row1("clientToken") = row("clientToken").ToString()
                        row1("accessToken") = row("accessToken").ToString()
                        row1("serviceid") = row("serviceid").ToString()
                        row1("EOHOAmount") = row("EOHOAmount")
                        row1("tax_id") = row("tax_id")
                        row1("Total_Value") = row("Total_Value")
                        row1("Sales_Handling_Fee") = row("Sales_Handling_Fee")
                        row1("Payment_Handling_Fee") = row("Payment_Handling_Fee")
                        row1("Tax_Amount") = row("Tax_Amount")
                        row1("profile_id") = row("profile_id")
                        row1("DefaultDateTime") = row("DefaultDateTime").ToString()
                        row1("ZR_VenueID") = Val(row("ZR_VenueID"))
                        row1("ZR_LocationID") = Val(row("ZR_LocationID"))
                        row1("ZR_TillID") = row("ZR_TillID").ToString()
                        row1("CardPayType") = Val(row("CardPayType"))


                    End If
                Next
            Next

            ViewState("Data_dt") = dt

            For Each row1 As DataRow In dt.Rows
                Dim Code As Integer = Val(row1("code").ToString().Replace("F", "")) + 1

                If row1("big_button") = "1" Then
                    If Code = 2 Then
                        btn_7by7_2.Visible = False
                        btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 3 Then
                        btn_7by7_3.Visible = False
                        btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 4 Then
                        btn_7by7_4.Visible = False
                        btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 5 Then
                        btn_7by7_5.Visible = False
                        btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 6 Then
                        btn_7by7_6.Visible = False
                        btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 7 Then
                        btn_7by7_7.Visible = False
                        btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 8 Then
                        btn_7by7_8.Visible = False
                        btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 9 Then
                        btn_7by7_9.Visible = False
                        btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 10 Then
                        btn_7by7_10.Visible = False
                        btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"

                    ElseIf Code = 11 Then
                        btn_7by7_11.Visible = False
                        btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"


                    ElseIf Code = 12 Then
                        btn_7by7_12.Visible = False
                        btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 13 Then
                        btn_7by7_13.Visible = False
                        btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 14 Then
                        btn_7by7_14.Visible = False
                        btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"

                        'ElseIf Code = 15 Then
                        '    btn_7by7_15.Visible = False
                        '    btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 16 Then
                        btn_7by7_16.Visible = False
                        btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 17 Then
                        btn_7by7_17.Visible = False
                        btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 18 Then
                        btn_7by7_18.Visible = False
                        btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 19 Then
                        btn_7by7_19.Visible = False
                        btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 20 Then
                        btn_7by7_20.Visible = False
                        btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 21 Then
                        btn_7by7_21.Visible = False
                        btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"

                    ElseIf Code = 22 Then
                        btn_7by7_22.Visible = False
                        btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 23 Then
                        btn_7by7_23.Visible = False
                        btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                    ElseIf Code = 24 Then
                        btn_7by7_24.Visible = False
                        btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 25 Then
                        btn_7by7_25.Visible = False
                        btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 26 Then
                        btn_7by7_26.Visible = False
                        btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    ElseIf Code = 27 Then
                        btn_7by7_27.Visible = False
                        btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                    ElseIf Code = 28 Then
                        btn_7by7_28.Visible = False
                        btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        'ElseIf Code = 29 Then
                        '    btn_7by7_29.Visible = False
                        '    btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    ElseIf Code = 30 Then
                        btn_7by7_30.Visible = False
                        btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                End If


                If row1("name") = "" Then
                Else
                    If row1("code") = "F1" Then
                        SetName(btn_7by7_1, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F2" Then
                        SetName(btn_7by7_2, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F3" Then
                        SetName(btn_7by7_3, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F4" Then
                        SetName(btn_7by7_4, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F5" Then
                        SetName(btn_7by7_5, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F6" Then
                        SetName(btn_7by7_6, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F7" Then
                        SetName(btn_7by7_7, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F8" Then
                        SetName(btn_7by7_8, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F9" Then
                        SetName(btn_7by7_9, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F10" Then
                        SetName(btn_7by7_10, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F11" Then
                        SetName(btn_7by7_11, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F12" Then
                        SetName(btn_7by7_12, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F13" Then
                        SetName(btn_7by7_13, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F14" Then
                        SetName(btn_7by7_14, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F15" Then
                        SetName(btn_7by7_15, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F16" Then
                        SetName(btn_7by7_16, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F17" Then
                        SetName(btn_7by7_17, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F18" Then
                        SetName(btn_7by7_18, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F19" Then
                        SetName(btn_7by7_19, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F20" Then
                        SetName(btn_7by7_20, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F21" Then
                        SetName(btn_7by7_21, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F22" Then
                        SetName(btn_7by7_22, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F23" Then
                        SetName(btn_7by7_23, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F24" Then
                        SetName(btn_7by7_24, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F25" Then
                        SetName(btn_7by7_25, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F26" Then
                        SetName(btn_7by7_26, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F27" Then
                        SetName(btn_7by7_27, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F28" Then
                        SetName(btn_7by7_28, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F29" Then
                        SetName(btn_7by7_29, row1("name"), row1("back_color"), row1("for_color"))
                    ElseIf row1("code") = "F30" Then
                        SetName(btn_7by7_30, row1("name"), row1("back_color"), row1("for_color"))
                    End If
                End If

            Next

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Function_Master:BindFunction" + ex.Message)
        End Try
    End Sub


    Private Sub BindFunctionGrid()
        dt.Columns.Add("id", GetType(Integer))
        dt.Columns.Add("cmp_id", GetType(Integer))
        dt.Columns.Add("name", GetType(String))
        dt.Columns.Add("code", GetType(String))
        dt.Columns.Add("caption_type", GetType(String))
        dt.Columns.Add("is_active", GetType(Integer))
        dt.Columns.Add("ip_address", GetType(String))
        dt.Columns.Add("login_id", GetType(Integer))
        dt.Columns.Add("item", GetType(Decimal))
        dt.Columns.Add("back_color", GetType(String))
        dt.Columns.Add("for_color", GetType(String))
        dt.Columns.Add("machine_id", GetType(Integer))
        dt.Columns.Add("big_button", GetType(Integer))
        dt.Columns.Add("Payment_id", GetType(Integer))
        dt.Columns.Add("pay_type", GetType(Integer))
        dt.Columns.Add("pay_sub_type", GetType(String))
        dt.Columns.Add("is_groupby_dept", GetType(Integer))
        dt.Columns.Add("is_groupby_course", GetType(Integer))
        dt.Columns.Add("dept_id", GetType(String))
        dt.Columns.Add("course_id", GetType(String))
        dt.Columns.Add("Panel_Id", GetType(Integer))
        dt.Columns.Add("Parent_id", GetType(Integer))
        dt.Columns.Add("Is_Parent", GetType(Integer))
        dt.Columns.Add("Function_Id", GetType(Integer))
        dt.Columns.Add("platformAdd", GetType(String))
        dt.Columns.Add("clientToken", GetType(String))
        dt.Columns.Add("accessToken", GetType(String))
        dt.Columns.Add("serviceid", GetType(String))
        dt.Columns.Add("Total_Value", GetType(Decimal))
        dt.Columns.Add("Sales_Handling_Fee", GetType(Decimal))
        dt.Columns.Add("Payment_Handling_Fee", GetType(Decimal))
        dt.Columns.Add("Tax_Amount", GetType(Decimal))
        dt.Columns.Add("profile_id", GetType(Integer))
        dt.Columns.Add("DefaultDateTime", GetType(String))
        dt.Columns.Add("ZR_VenueID", GetType(Integer))
        dt.Columns.Add("ZR_LocationID", GetType(Integer))
        dt.Columns.Add("ZR_TillID", GetType(String))
        dt.Columns.Add("CardPayType", GetType(Integer))

        Dim val1 As String = "F"

        For i = 1 To 30
            Dim row As DataRow = dt.NewRow()
            row("id") = i
            row("cmp_id") = 0
            row("name") = ""
            row("code") = val1 + i.ToString()
            row("caption_type") = ""
            row("is_active") = 1
            row("ip_address") = ""
            row("login_id") = 0
            row("item") = 0.00
            row("back_color") = "#1B7BBD"
            row("for_color") = "#FFFFFF"
            row("machine_id") = 0
            row("big_button") = 0
            row("Payment_id") = 0
            row("pay_type") = 0
            row("pay_sub_type") = ""
            row("is_groupby_dept") = 0
            row("is_groupby_course") = 0
            row("dept_id") = 0
            row("course_id") = 0
            row("Panel_Id") = 0
            row("Parent_id") = 0
            row("Is_Parent") = 0
            row("Function_Id") = 0
            row("platformAdd") = ""
            row("clientToken") = ""
            row("accessToken") = ""
            row("serviceid") = ""
            row("Total_Value") = 0
            row("Sales_Handling_Fee") = 0
            row("Payment_Handling_Fee") = 0
            row("Tax_Amount") = 0
            row("profile_id") = 0
            row("DefaultDateTime") = ""
            row("ZR_VenueID") = 0
            row("ZR_LocationID") = 0
            row("ZR_TillID") = ""
            row("CardPayType") = 0


            dt.Rows.Add(row)
        Next
        ViewState("Data_dt") = dt
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("function_id") = Nothing Then
                Clear()
            Else
                BindFunction()
            End If
        Catch ex As Exception
            LogHelper.Error("Function_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Try
            Response.Redirect("Function_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Function_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtFName.Text = ""
            'ddCode.SelectedIndex = 0
            'txtShortinigNo.Text = ""
            ddCaption.SelectedIndex = 0
            divPayType.Visible = False
            ddlPayType.ClearSelection()
            chkActive.Checked = True
            txtItem.Text = ""
            txt_TotalValue.Text = ""
            txt_SalesHandlingFee.Text = ""
            txt_PaymentHandlingFee.Text = ""
            txt_TaxAmount.Text = ""
            txtAmount.Text = ""
            radForcolorpicker.SelectedColor = Nothing
            radBackcolorpicker.SelectedColor = Nothing
            chkbigbutton.Checked = True
            divPayment.Visible = False
            ddPayment.ClearSelection()
            divCardSale.Visible = False
            divCardSub.Visible = False
            ddlCardType.SelectedValue = "0"
            ddlCardSubType.SelectedValue = "EVO"
            RadMachine.ClearSelection()
            rdExpTime.SelectedValue = "DD"
            rdProfileType.SelectedIndex = 0
            rdPriceLevel.SelectedIndex = 0
            txtDays.Text = ""
            radVenue.ClearSelection()
            radLocation.ClearSelection()
            radTill.ClearSelection()
            divZReport.Visible = False

        Catch ex As Exception
            LogHelper.Error("Function_Master:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            oclsFunction.cmp_id = Val(Session("cmp_id"))
            oclsFunction.name = txtMainFun.Text.Trim()

            oclsFunction.machine_id = Val(RadMachine.SelectedValue)
            oclsFunction.is_active = IIf(ChkmainActive.Checked = True, 1, 0)
            oclsFunction.ip_address = Request.UserHostAddress
            oclsFunction.login_id = Val(Session("login_id"))

            If Session("mainfunction_id") = Nothing Then
                oclsFunction.mainfunction_id = 0
                Dim id As Integer = oclsFunction.InsertMainFunction()
                Insert_FunctionDetails(id)
                Session("Success") = "Record inserted successfully"
            Else
                oclsFunction.mainfunction_id = Val(Session("mainfunction_id"))
                oclsFunction.UpdateMainFunction()
                Insert_FunctionDetails(Val(Session("mainfunction_id")))
                Session("Success") = "Record updated successfully"
            End If


            Session("mainfunction_id") = Nothing
            Response.Redirect("Function_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Function_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub


    Private Sub Insert_FunctionDetails(ByVal id As Integer)
        Try


            Dim Data_dt As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

            For Each row As DataRow In Data_dt.Rows
                Try

                    If Val(row("Function_Id")) > 0 And row("name") IsNot "" Then
                        oclsFunction.mainfunction_id = id
                        oclsFunction.function_id = Val(row("Function_Id"))
                        oclsFunction.cmp_id = Val(Session("cmp_id"))
                        oclsFunction.name = row("name")
                        oclsFunction.code = row("code")
                        oclsFunction.caption_type = row("caption_type")
                        oclsFunction.is_active = row("is_active")
                        oclsFunction.shorting_no = txtSortinigNo.Text
                        oclsFunction.ip_address = row("ip_address")
                        oclsFunction.login_id = Val(row("login_id"))
                        oclsFunction.for_color = row("for_color")
                        oclsFunction.back_color = row("back_color")
                        oclsFunction.machine_id = Val(row("machine_id"))
                        oclsFunction.Panel_id = Val(row("Panel_id"))
                        oclsFunction.Parent_id = Val(row("Parent_id"))
                        oclsFunction.big_button = row("big_button")
                        oclsFunction.item = Val(row("item"))
                        oclsFunction.isgroupbydept = row("is_groupby_dept")
                        oclsFunction.isgroupbycourse = row("is_groupby_course")
                        oclsFunction.dept_id = row("dept_id")
                        oclsFunction.course_id = row("course_id")
                        oclsFunction.Payment_id = Val(row("Payment_id"))
                        oclsFunction.pay_type = Val(row("pay_type"))
                        oclsFunction.pay_sub_type = row("pay_sub_type")
                        oclsFunction.platformAdd = row("platformAdd").ToString()
                        oclsFunction.clientToken = row("clientToken").ToString()
                        oclsFunction.accessToken = row("accessToken").ToString()
                        oclsFunction.serviceid = row("serviceid").ToString()
                        If ddltax.Items.Count > 0 Then
                            oclsFunction.tax_id = Val(row("tax_id").ToString())
                        Else
                            oclsFunction.tax_id = 0
                        End If
                        oclsFunction.EOHelpOut_max_amount_each = Val(row("EOHOAmount").ToString())
                        oclsFunction.Total_Value = Val(row("Total_Value").ToString())
                        oclsFunction.Tax_Amount = Val(row("Tax_Amount").ToString())
                        oclsFunction.Sales_Handling_Fee = Val(row("Sales_Handling_Fee").ToString())
                        oclsFunction.Payment_Handling_Fee = Val(row("Payment_Handling_Fee").ToString())
                        oclsFunction.profile_id = Val(row("profile_id"))
                        oclsFunction.DefaultDateTime = row("DefaultDateTime")
                        oclsFunction.ZR_VenueID = Val(row("ZR_VenueID"))
                        oclsFunction.ZR_LocationID = Val(row("ZR_LocationID"))
                        oclsFunction.ZR_TillID = row("ZR_TillID")
                        oclsFunction.CardPayType = Val(row("CardPayType"))

                        oclsFunction.Update()
                    Else
                        If row("name") = "" Then

                        Else
                            oclsFunction.mainfunction_id = id
                            oclsFunction.cmp_id = Val(Session("cmp_id"))
                            oclsFunction.name = row("name")
                            oclsFunction.code = row("code")
                            oclsFunction.caption_type = row("caption_type")
                            oclsFunction.is_active = row("is_active")
                            oclsFunction.shorting_no = txtSortinigNo.Text
                            oclsFunction.ip_address = row("ip_address")
                            oclsFunction.login_id = Val(row("login_id"))
                            oclsFunction.for_color = row("for_color")
                            oclsFunction.back_color = row("back_color")
                            oclsFunction.machine_id = Val(row("machine_id"))
                            oclsFunction.Panel_id = Val(row("Is_Parent"))
                            oclsFunction.Parent_id = Val(row("Parent_id"))
                            oclsFunction.big_button = row("big_button")
                            oclsFunction.item = Val(row("item"))
                            oclsFunction.isgroupbydept = row("is_groupby_dept")
                            oclsFunction.isgroupbycourse = row("is_groupby_course")
                            oclsFunction.dept_id = row("dept_id")
                            oclsFunction.course_id = row("course_id")
                            oclsFunction.Payment_id = Val(row("Payment_id"))
                            oclsFunction.pay_type = Val(row("pay_type"))
                            oclsFunction.pay_sub_type = row("pay_sub_type")
                            oclsFunction.platformAdd = row("platformAdd").ToString()
                            oclsFunction.clientToken = row("clientToken").ToString()
                            oclsFunction.accessToken = row("accessToken").ToString()
                            oclsFunction.serviceid = row("serviceid").ToString()
                            If ddltax.Items.Count > 0 Then
                                oclsFunction.tax_id = ddltax.SelectedValue
                            Else
                                oclsFunction.tax_id = 0
                            End If
                            oclsFunction.EOHelpOut_max_amount_each = Val(txtEOHOAmount.Text)
                            oclsFunction.Total_Value = Val(row("Total_Value").ToString())
                            oclsFunction.Tax_Amount = Val(row("Tax_Amount").ToString())
                            oclsFunction.Sales_Handling_Fee = Val(row("Sales_Handling_Fee").ToString())
                            oclsFunction.Payment_Handling_Fee = Val(row("Payment_Handling_Fee").ToString())
                            oclsFunction.profile_id = Val(row("profile_id"))
                            oclsFunction.DefaultDateTime = row("DefaultDateTime")
                            oclsFunction.ZR_VenueID = Val(row("ZR_VenueID"))
                            oclsFunction.ZR_LocationID = Val(row("ZR_LocationID"))
                            oclsFunction.ZR_TillID = row("ZR_TillID")
                            oclsFunction.CardPayType = Val(row("CardPayType"))


                            oclsFunction.Insert()
                        End If

                        If Not Session("mainfunction_id") = Nothing Then
                            For Each row1 As DataRow In Data_dt.Rows
                                If Val(row1("Is_Parent")) > 0 And row1("name") IsNot "" Then
                                    oclsFunction.mainfunction_id = id
                                    oclsFunction.Panel_id = Val(row1("Is_Parent"))
                                    oclsFunction.Update_Function_Parent()
                                End If
                            Next
                        Else
                            For Each row1 As DataRow In Data_dt.Rows
                                If Val(row1("Is_Parent")) > 0 And row1("name") IsNot "" Then
                                    oclsFunction.mainfunction_id = id
                                    oclsFunction.Panel_id = Val(row1("Is_Parent"))
                                    oclsFunction.Update_Function_Parent()
                                End If
                            Next
                        End If
                    End If

                Catch ex As Exception
                    LogHelper.Error("Function_Master:btnSave_Click:Loop:" + ex.Message)
                End Try
            Next

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Function_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddCaption_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddCaption.SelectedIndexChanged
        Try

            If ddCaption.SelectedValue = "Discount On Item" Or ddCaption.SelectedValue = "Discount On Percentage" Or ddCaption.SelectedValue = "Discount Amount" Or ddCaption.SelectedValue = "Discount Amount In Item" Then
                divItem.Visible = True
                divLauncher.Visible = False
                divAmount.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                'ddlcourseid.Visible = True
                'ddldeaprtmentid.Visible = True
                chkgroupbycourse.Visible = True
                chkgroupbydept.Visible = True
                'lbldept.Visible = True
                'lblcourse.Visible = True
                lblgroupbycourse.Visible = True
                lblgroupbydept.Visible = True
                'ddlcourseid.Visible = True
                'ddldeaprtmentid.Visible = True
                divSurchargeAmount.Visible = False
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedItem.Value = "Surcharge" Then
                divSurchargeAmount.Visible = True
                divItem.Visible = False
                divLauncher.Visible = False
                divAmount.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                chkgroupbycourse.Visible = False
                chkgroupbydept.Visible = False
                lblgroupbycourse.Visible = True
                lblgroupbydept.Visible = True
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedItem.Value = "Eat out to help out" Then
                divEOHO.Visible = True
                divSurchargeAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divAmount.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                chkgroupbycourse.Visible = False
                chkgroupbydept.Visible = False
                lblgroupbycourse.Visible = True
                lblgroupbydept.Visible = True

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedItem.Value = "Cash Sale" Then
                divAmount.Visible = True
                divItem.Visible = False
                divLauncher.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                divSurchargeAmount.Visible = False
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedItem.Value = "Integrated Pay" Then
                divPayment.Visible = True
                divPayType.Visible = True
                ddlPayType.ClearSelection()
                divAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divCardSale.Visible = False
                divCardSub.Visible = False
                divSurchargeAmount.Visible = False
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedItem.Value = "Card Sale" Then

                divAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = True
                divCardSub.Visible = False
                ddlCardType.SelectedValue = "0"
                divSurchargeAmount.Visible = False
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False
                divPayType.Visible = True


            ElseIf ddCaption.SelectedItem.Value = "Dance Voucher" Then
                divAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                ddlCardType.SelectedValue = "0"
                divSurchargeAmount.Visible = False
                divEOHO.Visible = False

                divTaxAmount.Visible = True
                divSalesHandlingFee.Visible = True
                divTotalValue.Visible = True
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedValue = "Dance Voucher Redeem" Then
                divAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                ddlCardType.SelectedValue = "0"
                divSurchargeAmount.Visible = False
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = True
                divPaymentHandlingFee.Visible = True
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedValue = "Account Search" Then
                divAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                ddlCardType.SelectedValue = "0"
                divSurchargeAmount.Visible = False
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = True
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedItem.Value = "Change Price Level" Or ddCaption.SelectedItem.Value = "Price Level Transaction" Then
                divPriceLevel.Visible = True
                divSurchargeAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divAmount.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                chkgroupbycourse.Visible = False
                chkgroupbydept.Visible = False
                lblgroupbycourse.Visible = True
                lblgroupbydept.Visible = True
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedItem.Value = "Z-Report BO" Then

                divZReport.Visible = True
                divEOHO.Visible = False
                divAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                chkgroupbycourse.Visible = False
                chkgroupbydept.Visible = False
                lbldept.Visible = False
                lblcourse.Visible = False
                lblgroupbycourse.Visible = False
                lblgroupbydept.Visible = False
                ddlcourseid.Visible = False
                ddldeaprtmentid.Visible = False
                divSurchargeAmount.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
            ElseIf ddCaption.SelectedValue = "Launcher" Then
                divItem.Visible = False
                divLauncher.Visible = True
                divAmount.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                'ddlcourseid.Visible = True
                'ddldeaprtmentid.Visible = True
                chkgroupbycourse.Visible = True
                chkgroupbydept.Visible = True
                'lbldept.Visible = True
                'lblcourse.Visible = True
                lblgroupbycourse.Visible = True
                lblgroupbydept.Visible = True
                'ddlcourseid.Visible = True
                'ddldeaprtmentid.Visible = True
                divSurchargeAmount.Visible = False
                divEOHO.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                divZReport.Visible = False

            ElseIf ddCaption.SelectedValue = "Stripe Payment" Then
                divZReport.Visible = False
                divEOHO.Visible = False
                divAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = True
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                chkgroupbycourse.Visible = False
                chkgroupbydept.Visible = False
                lbldept.Visible = False
                lblcourse.Visible = False
                lblgroupbycourse.Visible = False
                lblgroupbydept.Visible = False
                ddlcourseid.Visible = False
                ddldeaprtmentid.Visible = False
                divSurchargeAmount.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False
                txtAccountID.Visible = True
                lblAccountID.Visible = True
                txtLauncher.Attributes("placeholder") = "Secret Key"

                lbllauncher.Text = "Secret Key"
                txtAccountID.Attributes("clientToken") = "Account ID"

                lblAccountID.Text = "Account ID"
            Else
                divZReport.Visible = False
                divEOHO.Visible = False
                divAmount.Visible = False
                divItem.Visible = False
                divLauncher.Visible = False
                divPayment.Visible = False
                divPayType.Visible = False
                ddlPayType.ClearSelection()
                divCardSale.Visible = False
                divCardSub.Visible = False
                chkgroupbycourse.Visible = False
                chkgroupbydept.Visible = False
                lbldept.Visible = False
                lblcourse.Visible = False
                lblgroupbycourse.Visible = False
                lblgroupbydept.Visible = False
                ddlcourseid.Visible = False
                ddldeaprtmentid.Visible = False
                divSurchargeAmount.Visible = False

                divTaxAmount.Visible = False
                divSalesHandlingFee.Visible = False
                divTotalValue.Visible = False
                divPaymentHandlingFee.Visible = False
                divAccountCreate.Visible = False
                divPriceLevel.Visible = False

            End If

        Catch ex As Exception
            LogHelper.Error("Function_Master:ddCaption_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub ddlCardType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlCardType.SelectedIndexChanged
        Try
            If ddlCardType.SelectedItem.Value = "0" Then
                divCardSub.Visible = False
            Else
                divCardSub.Visible = True
                ddlCardSubType.SelectedValue = "EVO"
            End If
        Catch ex As Exception
            LogHelper.Error("Function_Master:ddlCardType_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub chkgroupbydept_CheckedChanged(sender As Object, e As EventArgs) Handles chkgroupbydept.CheckedChanged
        Try
            If chkgroupbydept.Checked = True Then
                ddldeaprtmentid.Visible = True
                lbldept.Visible = True
            Else
                ddldeaprtmentid.Visible = False
                lbldept.Visible = False
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:chkIsActive_CheckedChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub chkgroupbycourse_CheckedChanged(sender As Object, e As EventArgs) Handles chkgroupbycourse.CheckedChanged
        Try
            If chkgroupbycourse.Checked = True Then
                ddlcourseid.Visible = True
                lblcourse.Visible = True
            Else
                ddlcourseid.Visible = False
                lblcourse.Visible = False
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:chkIsActive_CheckedChanged:" + ex.Message)
        End Try
    End Sub

    Private Sub ddParent_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddParent.SelectedIndexChanged
        Try

            If ddParent.SelectedIndex = 0 Then
                oclsBind.BindFunctionType(ddCaption, Val(Session("cmp_id")))
            Else
                ddCaption.ClearSelection()
                oclsBind.BindFunctionType(ddCaption, Val(Session("cmp_id")))
                ddCaption.SelectedItem.Text = ddParent.SelectedItem.Text
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadMachine_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RadMachine.SelectedIndexChanged
        Try

        Catch ex As Exception

        End Try
    End Sub


    Private Sub btn_7by7_1_Click(sender As Object, e As EventArgs) Handles btn_7by7_1.Click
        Try
            Session("btn_7by7") = "btn_7by7_1"
            Session("Row_Id") = "1"
            Session("FCode") = "F1"


            btn_7by7_1.BorderColor = Color.Maroon
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_1.Text = "F1" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_2_Click(sender As Object, e As EventArgs) Handles btn_7by7_2.Click
        Try
            Session("btn_7by7") = "btn_7by7_2"
            Session("Row_Id") = "2"
            Session("FCode") = "F2"

            btn_7by7_2.BorderColor = Color.Maroon
            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_2.Text = "F2" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_3_Click(sender As Object, e As EventArgs) Handles btn_7by7_3.Click
        Try
            Session("btn_7by7") = "btn_7by7_3"
            Session("Row_Id") = "3"
            Session("FCode") = "F3"

            btn_7by7_3.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_3.Text = "F3" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_4_Click(sender As Object, e As EventArgs) Handles btn_7by7_4.Click
        Try
            Session("btn_7by7") = "btn_7by7_4"
            Session("Row_Id") = "4"
            Session("FCode") = "F4"

            btn_7by7_4.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_4.Text = "F4" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_5_Click(sender As Object, e As EventArgs) Handles btn_7by7_5.Click
        Try
            Session("btn_7by7") = "btn_7by7_5"
            Session("Row_Id") = "5"
            Session("FCode") = "F5"

            btn_7by7_5.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_5.Text = "F5" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_6_Click(sender As Object, e As EventArgs) Handles btn_7by7_6.Click
        Try
            Session("btn_7by7") = "btn_7by7_6"
            Session("Row_Id") = "6"
            Session("FCode") = "F6"

            btn_7by7_6.BorderColor = Color.Maroon
            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_6.Text = "F6" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_7_Click(sender As Object, e As EventArgs) Handles btn_7by7_7.Click
        Try
            Session("btn_7by7") = "btn_7by7_7"
            Session("Row_Id") = "7"
            Session("FCode") = "F7"

            btn_7by7_7.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_7.Text = "F7" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_8_Click(sender As Object, e As EventArgs) Handles btn_7by7_8.Click
        Try
            Session("btn_7by7") = "btn_7by7_8"
            Session("Row_Id") = "8"
            Session("FCode") = "F8"

            btn_7by7_8.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_8.Text = "F8" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_9_Click(sender As Object, e As EventArgs) Handles btn_7by7_9.Click
        Try
            Session("btn_7by7") = "btn_7by7_9"
            Session("Row_Id") = "9"
            Session("FCode") = "F9"

            btn_7by7_9.BorderColor = Color.Maroon
            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_9.Text = "F9" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_10_Click(sender As Object, e As EventArgs) Handles btn_7by7_10.Click
        Try
            Session("btn_7by7") = "btn_7by7_10"
            Session("Row_Id") = "10"
            Session("FCode") = "F10"

            btn_7by7_10.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_10.Text = "F10" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_11_Click(sender As Object, e As EventArgs) Handles btn_7by7_11.Click
        Try
            Session("btn_7by7") = "btn_7by7_11"
            Session("Row_Id") = "11"
            Session("FCode") = "F11"

            btn_7by7_11.BorderColor = Color.Maroon
            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_11.Text = "F11" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_12_Click(sender As Object, e As EventArgs) Handles btn_7by7_12.Click
        Try
            Session("btn_7by7") = "btn_7by7_12"
            Session("Row_Id") = "12"
            Session("FCode") = "F12"

            btn_7by7_12.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_12.Text = "F12" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_13_Click(sender As Object, e As EventArgs) Handles btn_7by7_13.Click
        Try
            Session("btn_7by7") = "btn_7by7_13"
            Session("Row_Id") = "13"
            Session("FCode") = "F13"

            btn_7by7_13.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_13.Text = "F13" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_14_Click(sender As Object, e As EventArgs) Handles btn_7by7_14.Click
        Try
            Session("btn_7by7") = "btn_7by7_14"
            Session("Row_Id") = "14"
            Session("FCode") = "F14"

            btn_7by7_14.BorderColor = Color.Maroon
            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_14.Text = "F14" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_15_Click(sender As Object, e As EventArgs) Handles btn_7by7_15.Click
        Try
            Session("btn_7by7") = "btn_7by7_15"
            Session("Row_Id") = "15"
            Session("FCode") = "F15"

            btn_7by7_15.BorderColor = Color.Maroon
            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_15.Text = "F15" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_16_Click(sender As Object, e As EventArgs) Handles btn_7by7_16.Click
        Try
            Session("btn_7by7") = "btn_7by7_16"
            Session("Row_Id") = "16"
            Session("FCode") = "F16"

            btn_7by7_16.BorderColor = Color.Maroon
            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_16.Text = "F16" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_17_Click(sender As Object, e As EventArgs) Handles btn_7by7_17.Click
        Try
            Session("btn_7by7") = "btn_7by7_17"
            Session("Row_Id") = "17"
            Session("FCode") = "F17"

            btn_7by7_17.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")





            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_17.Text = "F17" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_18_Click(sender As Object, e As EventArgs) Handles btn_7by7_18.Click
        Try
            Session("btn_7by7") = "btn_7by7_18"
            Session("Row_Id") = "18"
            Session("FCode") = "F18"
            btn_7by7_18.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")





            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_18.Text = "F18" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_19_Click(sender As Object, e As EventArgs) Handles btn_7by7_19.Click
        Try
            Session("btn_7by7") = "btn_7by7_19"
            Session("Row_Id") = "19"
            Session("FCode") = "F19"

            btn_7by7_19.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_19.Text = "F19" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_20_Click(sender As Object, e As EventArgs) Handles btn_7by7_20.Click
        Try
            Session("btn_7by7") = "btn_7by7_20"
            Session("Row_Id") = "20"
            Session("FCode") = "F20"

            btn_7by7_20.BorderColor = Color.Maroon
            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_20.Text = "F20" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_21_Click(sender As Object, e As EventArgs) Handles btn_7by7_21.Click
        Try
            Session("btn_7by7") = "btn_7by7_21"
            Session("Row_Id") = "21"
            Session("FCode") = "F21"

            btn_7by7_21.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_21.Text = "F21" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_22_Click(sender As Object, e As EventArgs) Handles btn_7by7_22.Click
        Try
            Session("btn_7by7") = "btn_7by7_22"
            Session("Row_Id") = "22"
            Session("FCode") = "F22"

            btn_7by7_22.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")





            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_22.Text = "F22" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_23_Click(sender As Object, e As EventArgs) Handles btn_7by7_23.Click
        Try
            Session("btn_7by7") = "btn_7by7_23"
            Session("Row_Id") = "23"
            Session("FCode") = "F23"

            btn_7by7_23.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_23.Text = "F23" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_24_Click(sender As Object, e As EventArgs) Handles btn_7by7_24.Click
        Try
            Session("btn_7by7") = "btn_7by7_24"
            Session("Row_Id") = "24"
            Session("FCode") = "F24"

            btn_7by7_24.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_24.Text = "F24" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_25_Click(sender As Object, e As EventArgs) Handles btn_7by7_25.Click
        Try
            Session("btn_7by7") = "btn_7by7_25"
            Session("Row_Id") = "25"
            Session("FCode") = "F25"

            btn_7by7_25.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")





            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_25.Text = "F25" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_26_Click(sender As Object, e As EventArgs) Handles btn_7by7_26.Click
        Try
            Session("btn_7by7") = "btn_7by7_26"
            Session("Row_Id") = "26"
            Session("FCode") = "F26"

            btn_7by7_26.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_26.Text = "F26" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_27_Click(sender As Object, e As EventArgs) Handles btn_7by7_27.Click
        Try
            Session("btn_7by7") = "btn_7by7_27"
            Session("Row_Id") = "27"
            Session("FCode") = "F27"

            btn_7by7_27.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")





            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_27.Text = "F27" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_28_Click(sender As Object, e As EventArgs) Handles btn_7by7_28.Click
        Try
            Session("btn_7by7") = "btn_7by7_28"
            Session("Row_Id") = "28"
            Session("FCode") = "F28"

            btn_7by7_28.BorderColor = Color.Maroon




            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_28.Text = "F28" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_29_Click(sender As Object, e As EventArgs) Handles btn_7by7_29.Click
        Try
            Session("btn_7by7") = "btn_7by7_29"
            Session("Row_Id") = "29"
            Session("FCode") = "F29"
            btn_7by7_29.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")




            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_29.Text = "F29" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_7by7_30_Click(sender As Object, e As EventArgs) Handles btn_7by7_30.Click
        Try
            Session("btn_7by7") = "btn_7by7_30"
            Session("Row_Id") = "30"
            Session("FCode") = "F30"

            btn_7by7_30.BorderColor = Color.Maroon

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            If btn_7by7_30.Text = "F30" Then

            Else
                BindData(Session("Row_Id"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Try
            If Session("Row_Id").ToString() = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Kindly select function code');", True)
                Return
            End If


            Dim Code As Integer = Val(Session("FCode").Replace("F", "")) + 1
            If chkbigbutton.Checked = True Then
                If Code = 2 Then
                    If btn_7by7_2.Text = "F2" Then
                        btn_7by7_2.Visible = False
                        btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 3 Then
                    If btn_7by7_3.Text = "F3" Then
                        btn_7by7_3.Visible = False
                        btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 4 Then
                    If btn_7by7_4.Text = "F4" Then
                        btn_7by7_4.Visible = False
                        btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 5 Then
                    If btn_7by7_5.Text = "F5" Then
                        btn_7by7_5.Visible = False
                        btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 6 Then
                    If btn_7by7_6.Text = "F6" Then
                        btn_7by7_6.Visible = False
                        btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 7 Then
                    If btn_7by7_7.Text = "F7" Then
                        btn_7by7_7.Visible = False
                        btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 8 Then
                    If btn_7by7_8.Text = "F8" Then
                        btn_7by7_8.Visible = False
                        btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 9 Then
                    If btn_7by7_9.Text = "F9" Then
                        btn_7by7_9.Visible = False
                        btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 10 Then
                    If btn_7by7_10.Text = "F10" Then
                        btn_7by7_10.Visible = False
                        btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 11 Then
                    If btn_7by7_11.Text = "F11" Then
                        btn_7by7_11.Visible = False
                        btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If


                ElseIf Code = 12 Then
                    If btn_7by7_12.Text = "F12" Then
                        btn_7by7_12.Visible = False
                        btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 13 Then
                    If btn_7by7_13.Text = "F13" Then
                        btn_7by7_13.Visible = False
                        btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 14 Then
                    If btn_7by7_14.Text = "F14" Then
                        btn_7by7_14.Visible = False
                        btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                    'ElseIf Code = 15 Then
                    '    If btn_7by7_15.Text = "F15" Then
                    '        btn_7by7_15.Visible = False
                    '        btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    '    End If
                ElseIf Code = 16 Then
                    If btn_7by7_16.Text = "F16" Then
                        btn_7by7_16.Visible = False
                        btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 17 Then
                    If btn_7by7_17.Text = "F17" Then
                        btn_7by7_17.Visible = False
                        btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 18 Then
                    If btn_7by7_18.Text = "F18" Then
                        btn_7by7_18.Visible = False
                        btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 19 Then
                    If btn_7by7_19.Text = "F19" Then
                        btn_7by7_19.Visible = False
                        btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 20 Then
                    If btn_7by7_20.Text = "F20" Then
                        btn_7by7_20.Visible = False
                        btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 21 Then
                    If btn_7by7_21.Text = "F21" Then
                        btn_7by7_21.Visible = False
                        btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 22 Then
                    If btn_7by7_22.Text = "F22" Then
                        btn_7by7_22.Visible = False
                        btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 23 Then
                    If btn_7by7_23.Text = "F23" Then
                        btn_7by7_23.Visible = False
                        btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 24 Then
                    If btn_7by7_24.Text = "F24" Then
                        btn_7by7_24.Visible = False
                        btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 25 Then
                    If btn_7by7_25.Text = "F25" Then
                        btn_7by7_25.Visible = False
                        btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 26 Then
                    If btn_7by7_26.Text = "F26" Then
                        btn_7by7_26.Visible = False
                        btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 27 Then
                    If btn_7by7_27.Text = "F27" Then
                        btn_7by7_27.Visible = False
                        btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 28 Then
                    If btn_7by7_28.Text = "F28" Then
                        btn_7by7_28.Visible = False
                        btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                    'ElseIf Code = 29 Then
                    '    If btn_7by7_29.Text = "F29" Then
                    '        btn_7by7_29.Visible = False
                    '        btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    '    End If
                ElseIf Code = 30 Then
                    If btn_7by7_30.Text = "F30" Then
                        btn_7by7_30.Visible = False
                        btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                End If

            Else
                If Code = 2 Then
                    If btn_7by7_2.Text = "F2" Then
                        btn_7by7_2.Visible = True
                        btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 3 Then
                    If btn_7by7_3.Text = "F3" Then
                        btn_7by7_3.Visible = True
                        btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 4 Then
                    If btn_7by7_4.Text = "F4" Then
                        btn_7by7_4.Visible = True
                        btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 5 Then
                    If btn_7by7_5.Text = "F5" Then
                        btn_7by7_5.Visible = True
                        btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 6 Then
                    If btn_7by7_6.Text = "F6" Then
                        btn_7by7_6.Visible = True
                        btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 7 Then
                    If btn_7by7_7.Text = "F7" Then
                        btn_7by7_7.Visible = True
                        btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 8 Then
                    If btn_7by7_8.Text = "F8" Then
                        btn_7by7_8.Visible = True
                        btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 9 Then
                    If btn_7by7_9.Text = "F9" Then
                        btn_7by7_9.Visible = True
                        btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 10 Then
                    If btn_7by7_10.Text = "F10" Then
                        btn_7by7_10.Visible = True
                        btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If

                ElseIf Code = 11 Then
                    If btn_7by7_11.Text = "F11" Then
                        btn_7by7_11.Visible = True
                        btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If

                ElseIf Code = 12 Then
                    If btn_7by7_12.Text = "F12" Then
                        btn_7by7_12.Visible = True
                        btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 13 Then
                    If btn_7by7_13.Text = "F13" Then
                        btn_7by7_13.Visible = True
                        btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                ElseIf Code = 14 Then
                    If btn_7by7_14.Text = "F14" Then
                        btn_7by7_14.Visible = True
                        btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                    'ElseIf Code = 15 Then
                    '    If btn_7by7_15.Text = "F15" Then
                    '        btn_7by7_15.Visible = True
                    '        btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    '    End If
                ElseIf Code = 16 Then
                    If btn_7by7_16.Text = "F16" Then
                        btn_7by7_16.Visible = True
                        btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 17 Then
                    If btn_7by7_17.Text = "F17" Then
                        btn_7by7_17.Visible = True
                        btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 18 Then
                    If btn_7by7_18.Text = "F18" Then
                        btn_7by7_18.Visible = True
                        btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 19 Then
                    If btn_7by7_19.Text = "F19" Then
                        btn_7by7_19.Visible = True
                        btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 20 Then
                    If btn_7by7_20.Text = "F20" Then
                        btn_7by7_20.Visible = True
                        btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If

                ElseIf Code = 21 Then
                    If btn_7by7_21.Text = "F21" Then
                        btn_7by7_21.Visible = True
                        btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 22 Then
                    If btn_7by7_22.Text = "F22" Then
                        btn_7by7_22.Visible = True
                        btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If

                ElseIf Code = 23 Then
                    If btn_7by7_23.Text = "F23" Then
                        btn_7by7_23.Visible = True
                        btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 24 Then
                    If btn_7by7_24.Text = "F24" Then
                        btn_7by7_24.Visible = True
                        btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 25 Then
                    If btn_7by7_25.Text = "F25" Then
                        btn_7by7_25.Visible = True
                        btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 26 Then
                    If btn_7by7_26.Text = "F26" Then
                        btn_7by7_26.Visible = True
                        btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 27 Then
                    If btn_7by7_27.Text = "F27" Then
                        btn_7by7_27.Visible = True
                        btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                ElseIf Code = 28 Then
                    If btn_7by7_28.Text = "F28" Then
                        btn_7by7_28.Visible = True
                        btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                    End If
                    'ElseIf Code = 29 Then
                    '    If btn_7by7_29.Text = "F29" Then
                    '        btn_7by7_29.Visible = True
                    '        btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    '    End If
                ElseIf Code = 30 Then
                    If btn_7by7_30.Text = "F30" Then
                        btn_7by7_30.Visible = True
                        btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                    End If
                End If
            End If

            'Dim Code As Integer = Val(Session("FCode").Replace("F", "")) + 1

            If (Session("FCode") = "F14" Or Session("FCode") = "F28") And chkbigbutton.Checked = True Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button');", True)
                Exit Sub
            End If

            If chkbigbutton.Checked = True Then
                If Code = 2 Then
                    If btn_7by7_2.Text = "F2" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 3 Then
                    If btn_7by7_3.Text = "F3" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 4 Then
                    If btn_7by7_4.Text = "F4" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 5 Then
                    If btn_7by7_5.Text = "F5" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 6 Then
                    If btn_7by7_6.Text = "F6" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 7 Then
                    If btn_7by7_7.Text = "F7" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 8 Then
                    If btn_7by7_8.Text = "F8" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 9 Then
                    If btn_7by7_9.Text = "F9" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 10 Then
                    If btn_7by7_10.Text = "F10" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 11 Then
                    If btn_7by7_11.Text = "F11" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 12 Then
                    If btn_7by7_12.Text = "F12" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 13 Then
                    If btn_7by7_13.Text = "F13" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 14 Then
                    If btn_7by7_14.Text = "F14" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                    'ElseIf Code = 15 Then
                    '    If btn_7by7_15.Text = "F15" Then
                    '    Else
                    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                    '        Exit Sub
                    '    End If
                ElseIf Code = 16 Then
                    If btn_7by7_16.Text = "F16" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 17 Then
                    If btn_7by7_17.Text = "F17" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 18 Then
                    If btn_7by7_18.Text = "F18" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 19 Then
                    If btn_7by7_19.Text = "F19" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 20 Then
                    If btn_7by7_20.Text = "F20" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If

                ElseIf Code = 21 Then
                    If btn_7by7_21.Text = "F21" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If

                ElseIf Code = 22 Then
                    If btn_7by7_22.Text = "F22" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 23 Then
                    If btn_7by7_23.Text = "F23" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 24 Then
                    If btn_7by7_24.Text = "F24" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 25 Then
                    If btn_7by7_25.Text = "F25" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 26 Then
                    If btn_7by7_26.Text = "F26" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 27 Then
                    If btn_7by7_27.Text = "F27" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 28 Then
                    If btn_7by7_28.Text = "F28" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 29 Then
                    If btn_7by7_29.Text = "F29" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                ElseIf Code = 30 Then
                    If btn_7by7_30.Text = "F30" Then
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not create big button,it is already assigned');", True)
                        Exit Sub
                    End If
                End If

            Else

            End If

            Dim Fcolor As String

            If radForcolorpicker.SelectedColor.Name = "0" Then
                Fcolor = "#FFFFFF"
            Else
                Fcolor = ColorTranslator.ToHtml(radForcolorpicker.SelectedColor)
            End If

            radForcolorpicker.SelectedColor = ColorTranslator.FromHtml(Fcolor)

            Dim dt1 As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

            For Each row As DataRow In dt1.Rows
                If Session("Row_Id").ToString() = row("id").ToString() And IIf(Radeditpanel.SelectedValue.ToString() = "0" Or Radeditpanel.SelectedValue.ToString() = "", "0", Val(Radeditpanel.SelectedValue.ToString())) = row("Parent_Id") Then
                    row("cmp_id") = Val(Session("cmp_id"))
                    row("name") = txtFName.Text
                    row("code") = Session("FCode")
                    If ddCaption.SelectedItem.Text = "" Then
                        row("caption_type") = ""
                    Else
                        row("caption_type") = ddCaption.SelectedItem.Text
                    End If

                    row("is_active") = IIf(chkActive.Checked = True, 1, 0)
                    row("ip_address") = Request.UserHostAddress
                    row("login_id") = Val(Session("login_id"))
                    If (ddCaption.SelectedValue = "Discount On Item" Or ddCaption.SelectedValue = "Discount On Percentage" Or ddCaption.SelectedValue = "Discount Amount" Or ddCaption.SelectedValue = "Discount Amount In Item") Then
                        row("item") = Val(txtItem.Text)
                    ElseIf (ddCaption.SelectedItem.Value = "Surcharge") Then
                        row("item") = Val(txtItem1.Text)
                    ElseIf (ddCaption.SelectedItem.Value = "Launcher") Then
                        row("item") = Val(0)
                        row("platformAdd") = txtLauncher.Text.Trim()
                    ElseIf (ddCaption.SelectedItem.Value = "Stripe Payment") Then
                        row("item") = Val(0)
                        row("platformAdd") = txtLauncher.Text.Trim()
                        row("clientToken") = txtAccountID.Text.Trim()
                    ElseIf (ddCaption.SelectedItem.Value = "Eat out to help out") Then
                        row("item") = Val(itemEOHO.Text)
                        If ddltax.Items.Count > 0 Then
                            row("tax_id") = Val(ddltax.SelectedValue)
                        End If
                        row("EOHOAmount") = Val(txtEOHOAmount.Text)
                    ElseIf (ddCaption.SelectedItem.Value = "Cash Sale") Then
                        row("item") = Val(txtAmount.Text)
                    ElseIf (ddCaption.SelectedItem.Value = "Dance Voucher") Then
                        row("Total_Value") = Val(txt_TotalValue.Text)
                        row("Tax_Amount") = Val(txt_TaxAmount.Text)
                        row("Sales_Handling_Fee") = Val(txt_SalesHandlingFee.Text)
                        row("Payment_Handling_Fee") = 0
                    ElseIf ddCaption.SelectedItem.Value = "Dance Voucher Redeem" Then
                        row("Total_Value") = Val(txt_TotalValue.Text)
                        row("Tax_Amount") = 0
                        row("Sales_Handling_Fee") = 0
                        row("Payment_Handling_Fee") = Val(txt_PaymentHandlingFee.Text)
                    ElseIf (ddCaption.SelectedItem.Value = "Account Search") Then
                        row("profile_id") = Val(rdProfileType.SelectedValue)
                        If txtDays.Text <> "" Then
                            row("DefaultDateTime") = txtDays.Text + "_" + rdExpTime.SelectedValue
                        Else
                            row("DefaultDateTime") = ""
                        End If
                    ElseIf (ddCaption.SelectedItem.Value = "Change Price Level" Or ddCaption.SelectedItem.Value = "Price Level Transaction") Then
                        row("item") = Val(rdPriceLevel.SelectedValue)

                    ElseIf (ddCaption.SelectedItem.Value = "Z-Report BO") Then

                        row("ZR_VenueID") = Val(radVenue.SelectedValue)
                        row("ZR_LocationID") = Val(radLocation.SelectedValue)
                        row("ZR_TillID") = GetSelectedValue(radTill) 'radTill.SelectedItem.Value

                    Else
                        row("item") = 0
                    End If
                    row("back_color") = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                    row("for_color") = ColorTranslator.ToHtml(radForcolorpicker.SelectedColor)
                    row("machine_id") = Val(RadMachine.SelectedValue)
                    row("big_button") = IIf(chkbigbutton.Checked = True, 1, 0)
                    row("Payment_id") = Val(ddPayment.SelectedItem.Value)
                    row("CardPayType") = Val(ddlPayType.SelectedItem.Value)
                    row("pay_type") = Val(ddlCardType.SelectedItem.Value)
                    If Val(ddlCardType.SelectedItem.Value) = 1 Then
                        row("pay_sub_type") = ddlCardSubType.SelectedValue.ToString()
                    Else
                        row("pay_sub_type") = ""
                    End If
                    row("is_groupby_dept") = IIf(chkgroupbydept.Checked = True, 1, 0)
                    row("is_groupby_course") = IIf(chkgroupbycourse.Checked = True, 1, 0)
                    If chkgroupbydept.Checked = True Then
                        row("dept_id") = GetSelectedValue(ddldeaprtmentid)
                    Else
                        row("dept_id") = 0
                    End If
                    If chkgroupbycourse.Checked = True Then
                        row("course_id") = GetSelectedValue(ddlcourseid)
                    Else
                        row("course_id") = 0
                    End If
                    row("Panel_Id") = Val(ddParent.SelectedValue.ToString())
                    row("Parent_id") = Val(IIf(Radeditpanel.SelectedValue.ToString() = "0" Or Radeditpanel.SelectedValue.ToString() = "", "0", Val(Radeditpanel.SelectedValue.ToString()))) 'Val(ddParent.SelectedValue.ToString())
                    row("Is_Parent") = Val(ddParent.SelectedValue.ToString())

                    If (ddCaption.SelectedItem.Value <> "Launcher") Then
                        row("platformAdd") = txtRoomPlatform.Text.Trim()
                    End If

                    row("clientToken") = txtRoomCToken.Text.Trim()
                    row("accessToken") = txtRoomAToken.Text.Trim()
                    row("serviceid") = txtserviceid.Text.Trim()
                    If (ddCaption.SelectedItem.Value = "Stripe Payment") Then
                        row("platformAdd") = txtLauncher.Text.Trim()
                        row("clientToken") = txtAccountID.Text.Trim()
                    End If
                End If
            Next

            ViewState("Data_dt") = dt1


            If Not Session("mainfunction_id") = Nothing Then
                Dim panel_dt As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

                If ddParent.SelectedValue.ToString() > 0 Then

                    Dim dr1 As DataRow() = panel_dt.Select("Panel_Id <>'" & Val(0) & "'")

                    ViewState("panel_dt") = dr1.CopyToDataTable

                    Dim panel_dt1 As DataTable = DirectCast(ViewState("panel_dt"), DataTable)

                    ViewState("Bind_Panel") = panel_dt1

                    Dim new_Dt As DataTable = New DataTable()

                    new_Dt.Columns.Add("id", GetType(Integer))
                    new_Dt.Columns.Add("name", GetType(String))

                    For Each rowpanel As DataRow In panel_dt1.Rows
                        If Val(rowpanel("Function_Id").ToString()) > 0 Then
                            If panel_dt1.Rows.Count > 0 Then

                                Dim row As DataRow = new_Dt.NewRow()
                                row("id") = Val(rowpanel("Function_Id").ToString())
                                row("name") = rowpanel("name").ToString()
                                new_Dt.Rows.Add(row)
                            End If
                        ElseIf Val(rowpanel("Is_Parent").ToString()) > 0 Then
                            If panel_dt1.Rows.Count > 0 Then

                                Dim row As DataRow = new_Dt.NewRow()
                                row("id") = Val(rowpanel("Is_Parent").ToString())
                                row("name") = rowpanel("name").ToString()
                                new_Dt.Rows.Add(row)
                            End If
                        End If
                        If new_Dt.Rows.Count > 0 Then
                            Radeditpanel.DataSource = new_Dt
                            Radeditpanel.DataTextField = "name"
                            Radeditpanel.DataValueField = "id"
                            Radeditpanel.DataBind()
                            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
                            Radeditpanel.Items.Insert(0, li)
                        End If
                    Next
                End If
            Else
                Dim panel_dt As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

                If ddParent.SelectedValue.ToString() > 0 Then

                    Dim dr1 As DataRow() = panel_dt.Select("Is_Parent <>'" & Val(0) & "'")

                    ViewState("panel_dt") = dr1.CopyToDataTable

                    Dim panel_dt1 As DataTable = DirectCast(ViewState("panel_dt"), DataTable)

                    ViewState("Bind_Panel") = panel_dt1


                    If panel_dt1.Rows.Count > 0 Then
                        Radeditpanel.DataSource = panel_dt1
                        Radeditpanel.DataTextField = "name"
                        Radeditpanel.DataValueField = "Is_Parent"
                        Radeditpanel.DataBind()
                        Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
                        Radeditpanel.Items.Insert(0, li)
                    End If
                End If
            End If



            Dim str As String
            Dim FCode As String

            If txtFName.Text.Length > 15 Then
                str = txtFName.Text.Substring(0, 15) + "...."
            Else
                str = txtFName.Text
            End If
            'str = txtFName.Text

            If Session("btn_7by7") = "btn_7by7_1" Then
                btn_7by7_1.Text = str.ToString()
                btn_7by7_1.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_1.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_1.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F1"
            End If
            If Session("btn_7by7") = "btn_7by7_2" Then
                btn_7by7_2.Text = str.ToString()
                btn_7by7_2.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_2.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_2.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)

                FCode = "F2"
            End If
            If Session("btn_7by7") = "btn_7by7_3" Then
                btn_7by7_3.Text = str.ToString()
                btn_7by7_3.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_3.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_3.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)

                FCode = "F3"
            End If
            If Session("btn_7by7") = "btn_7by7_4" Then
                btn_7by7_4.Text = str.ToString()
                btn_7by7_4.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_4.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_4.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F4"
            End If
            If Session("btn_7by7") = "btn_7by7_5" Then
                btn_7by7_5.Text = str.ToString()
                btn_7by7_5.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_5.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_5.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F5"
            End If
            If Session("btn_7by7") = "btn_7by7_6" Then
                btn_7by7_6.Text = str.ToString()
                btn_7by7_6.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_6.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_6.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F6"
            End If
            If Session("btn_7by7") = "btn_7by7_7" Then
                btn_7by7_7.Text = str.ToString()
                btn_7by7_7.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_7.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_7.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F7"
            End If
            If Session("btn_7by7") = "btn_7by7_8" Then
                btn_7by7_8.Text = str.ToString()
                btn_7by7_8.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_8.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_8.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F8"
            End If
            If Session("btn_7by7") = "btn_7by7_9" Then
                btn_7by7_9.Text = str.ToString()
                btn_7by7_9.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_9.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_9.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F9"
            End If
            If Session("btn_7by7") = "btn_7by7_10" Then
                btn_7by7_10.Text = str.ToString()
                btn_7by7_10.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_10.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_10.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F10"
            End If
            If Session("btn_7by7") = "btn_7by7_11" Then
                btn_7by7_11.Text = str.ToString()
                btn_7by7_11.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_11.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_11.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F11"
            End If
            If Session("btn_7by7") = "btn_7by7_12" Then
                btn_7by7_12.Text = str.ToString()
                btn_7by7_12.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_12.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_12.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F12"
            End If
            If Session("btn_7by7") = "btn_7by7_13" Then
                btn_7by7_13.Text = str.ToString()
                btn_7by7_13.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_13.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_13.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F13"
            End If
            If Session("btn_7by7") = "btn_7by7_14" Then
                btn_7by7_14.Text = str.ToString()
                btn_7by7_14.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_14.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_14.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F14"
            End If
            If Session("btn_7by7") = "btn_7by7_15" Then
                btn_7by7_15.Text = str.ToString()
                btn_7by7_15.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_15.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_15.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F15"
            End If
            If Session("btn_7by7") = "btn_7by7_16" Then
                btn_7by7_16.Text = str.ToString()
                btn_7by7_16.ToolTip = str.ToString()
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_16.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_16.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F16"
            End If
            If Session("btn_7by7") = "btn_7by7_17" Then
                btn_7by7_17.Text = str.ToString()
                btn_7by7_17.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_17.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_17.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F17"
            End If
            If Session("btn_7by7") = "btn_7by7_18" Then
                btn_7by7_18.Text = txtFName.Text
                btn_7by7_18.ToolTip = str.ToString()
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_18.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_18.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F18"
            End If
            If Session("btn_7by7") = "btn_7by7_19" Then
                btn_7by7_19.Text = str.ToString()
                btn_7by7_19.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_19.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_19.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F19"
            End If
            If Session("btn_7by7") = "btn_7by7_20" Then
                btn_7by7_20.Text = str.ToString()
                btn_7by7_20.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_20.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_20.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F20"
            End If
            If Session("btn_7by7") = "btn_7by7_21" Then
                btn_7by7_21.Text = str.ToString()
                btn_7by7_21.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_21.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_21.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F21"
            End If
            If Session("btn_7by7") = "btn_7by7_22" Then
                btn_7by7_22.Text = str.ToString()
                btn_7by7_22.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_22.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_22.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F22"
            End If
            If Session("btn_7by7") = "btn_7by7_23" Then
                btn_7by7_23.Text = str.ToString()
                btn_7by7_23.ToolTip = txtFName.Text

                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_23.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_23.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F23"
            End If
            If Session("btn_7by7") = "btn_7by7_24" Then
                btn_7by7_24.Text = str.ToString()
                btn_7by7_24.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_24.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_24.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F24"
            End If
            If Session("btn_7by7") = "btn_7by7_25" Then
                btn_7by7_25.Text = str.ToString()
                btn_7by7_25.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_25.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_25.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F25"
            End If
            If Session("btn_7by7") = "btn_7by7_26" Then
                btn_7by7_26.Text = str.ToString()
                btn_7by7_26.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_26.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_26.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F26"
            End If
            If Session("btn_7by7") = "btn_7by7_27" Then
                btn_7by7_27.Text = str.ToString()
                btn_7by7_27.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_27.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_27.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F27"
            End If
            If Session("btn_7by7") = "btn_7by7_28" Then
                btn_7by7_28.Text = str.ToString()
                btn_7by7_28.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_28.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_28.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F28"
            End If
            If Session("btn_7by7") = "btn_7by7_29" Then
                btn_7by7_29.Text = str.ToString()
                btn_7by7_29.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_29.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_29.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F29"
            End If
            If Session("btn_7by7") = "btn_7by7_30" Then
                btn_7by7_30.Text = str.ToString()
                btn_7by7_30.ToolTip = txtFName.Text
                Dim color As String = ColorTranslator.ToHtml(radBackcolorpicker.SelectedColor)
                btn_7by7_30.BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                btn_7by7_30.ForeColor = System.Drawing.ColorTranslator.FromHtml(Fcolor)
                FCode = "F30"
            End If

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")



            'Radeditpanel.ClearSelection()
            txtFName.Text = ""
            ddParent.ClearSelection()
            ddCaption.ClearSelection()
            ddlPayType.ClearSelection()
            oclsBind.BindFunctionType(ddCaption, Val(Session("cmp_id")))
            'ddCaption.Items(0).Selected = True
            txtItem.Text = ""
            txtLauncher.Text = ""
            txtAccountID.Text = ""
            txtAmount.Text = ""
            txt_PaymentHandlingFee.Text = ""
            txt_SalesHandlingFee.Text = ""
            txt_TaxAmount.Text = ""
            txt_TotalValue.Text = ""
            divTaxAmount.Visible = False
            divSalesHandlingFee.Visible = False
            divTotalValue.Visible = False
            divPaymentHandlingFee.Visible = False
            ddPayment.ClearSelection()
            ddlCardType.ClearSelection()
            ddlCardSubType.ClearSelection()
            chkActive.Checked = True
            radBackcolorpicker.SelectedColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            radForcolorpicker.SelectedColor = ColorTranslator.FromHtml("#FFFFFF")
            chkbigbutton.Checked = False
            chkgroupbydept.Checked = False
            chkgroupbycourse.Checked = False
            ddldeaprtmentid.ClearSelection()
            ddlcourseid.ClearSelection()
            txtRoomAToken.Text = ""
            txtRoomCToken.Text = ""
            txtRoomPlatform.Text = ""
            txtserviceid.Text = ""
            'oclsBind.BindEditPanel(Radeditpanel, Val(Session("cmp_id")))
            Session("Row_Id") = ""
            Session("btn_7by7") = ""
            Fcolor = ""
            divPriceLevel.Visible = False
            rdPriceLevel.ClearSelection()
            oclsBind.BindPrices_By_Cmp(rdPriceLevel, Val(Session("cmp_id")))
            radVenue.ClearSelection()
            radLocation.ClearSelection()
            radTill.ClearSelection()
            divZReport.Visible = False
            divPayType.Visible = False
            divPayment.Visible = False
            divCardSale.Visible = False

        Catch ex As Exception
            Dim st As String = ex.ToString()
        End Try
    End Sub

    Private Sub SetName(b1 As Button, str As String, Bg As String, fcolor As String)

        If str.Length > 15 Then
            b1.Text = str.ToString().Substring(0, 15) + "...."
        Else
            b1.Text = str.ToString()
        End If
        b1.ToolTip = str.ToString()
        'b1.Text = str.ToString()
        b1.ForeColor = System.Drawing.ColorTranslator.FromHtml(fcolor)
        b1.BackColor = System.Drawing.ColorTranslator.FromHtml(Bg)
    End Sub

    Private Sub BindData(ByVal id As String)
        Try
            Dim Data_dt As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

            For Each row As DataRow In Data_dt.Rows
                If row("id").ToString() = id.ToString() And row("Parent_Id") = Val(Radeditpanel.SelectedValue) And row("name") IsNot "" Then

                    txtFName.Text = row("name")

                    ddParent.ClearSelection()
                    oclsBind.BindParentFunction(ddParent, Val(Session("cmp_id")))
                    ddParent.SelectedValue = row("Panel_Id").ToString

                    ddCaption.ClearSelection()
                    ddCaption.SelectedValue = row("caption_type")

                    ddlPayType.ClearSelection()
                    ddlPayType.SelectedValue = row("CardPayType")


                    If ddCaption.SelectedValue = "Discount On Item" Or ddCaption.SelectedValue = "Discount On Percentage" Or ddCaption.SelectedValue = "Discount Amount" Or ddCaption.SelectedValue = "Discount Amount In Item" Then
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()
                        divZReport.Visible = False
                        divPriceLevel.Visible = False
                        divAccountCreate.Visible = False
                        divItem.Visible = True
                        divLauncher.Visible = False
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        txtItem.Text = row("item")
                        txtAmount.Text = ""
                        txt_PaymentHandlingFee.Text = ""
                        txt_SalesHandlingFee.Text = ""
                        txt_TaxAmount.Text = ""
                        txt_TotalValue.Text = ""
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        chkgroupbycourse.Visible = True
                        chkgroupbydept.Visible = True
                        'ddldeaprtmentid.Visible = True
                        'ddlcourseid.Visible = True
                        'lblcourse.Visible = True
                        'lbldept.Visible = True
                        lblgroupbycourse.Visible = True
                        lblgroupbydept.Visible = True
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        If Val(row("is_groupby_dept") = 1) Then
                            chkgroupbydept.Checked = True
                        Else
                            chkgroupbydept.Checked = False
                        End If
                        If Val(row("is_groupby_course") = 1) Then
                            chkgroupbycourse.Checked = True
                        Else
                            chkgroupbycourse.Checked = False
                        End If
                        If chkgroupbydept.Checked = True Then

                            ddldeaprtmentid.Visible = True
                            lbldept.Visible = True
                            Dim collection As IList(Of RadComboBoxItem) = ddldeaprtmentid.Items
                            Dim stArr As String() = row("dept_id").ToString().Split("#")

                            For i As Integer = 0 To stArr.Length - 1
                                For Each item As RadComboBoxItem In collection
                                    If stArr(i).ToString = item.Value Then
                                        item.Checked = True
                                        Exit For
                                    End If
                                Next
                            Next
                        End If
                        If chkgroupbycourse.Checked = True Then
                            ddlcourseid.Visible = True
                            lblcourse.Visible = True
                            Dim stArr1 As String() = row("course_id").ToString().Split("#")

                            Dim collection1 As IList(Of RadComboBoxItem) = ddlcourseid.Items
                            For i As Integer = 0 To stArr1.Length - 1
                                For Each item1 As RadComboBoxItem In collection1
                                    If stArr1(i).ToString = item1.Value Then
                                        item1.Checked = True
                                        Exit For
                                    End If
                                Next
                            Next
                        End If

                    ElseIf ddCaption.SelectedValue = "Cash Sale" Then
                        divAmount.Visible = True

                        divItem.Visible = False
                        divLauncher.Visible = False
                        txtAmount.Text = Val(row("item"))
                        txtItem.Text = ""

                        txt_PaymentHandlingFee.Text = ""
                        txt_SalesHandlingFee.Text = ""
                        txt_TaxAmount.Text = ""
                        txt_TotalValue.Text = ""
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False

                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divZReport.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()

                    ElseIf ddCaption.SelectedValue = "Dance Voucher" Then
                        divAmount.Visible = False
                        divTaxAmount.Visible = True
                        divSalesHandlingFee.Visible = True
                        divTotalValue.Visible = True
                        divPaymentHandlingFee.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        txtAmount.Text = ""
                        txtItem.Text = ""
                        txt_PaymentHandlingFee.Text = ""
                        txt_SalesHandlingFee.Text = Val(row("Sales_Handling_Fee"))
                        txt_TaxAmount.Text = Val(row("Tax_Amount"))
                        txt_TotalValue.Text = Val(row("Total_Value"))
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divZReport.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()

                    ElseIf ddCaption.SelectedValue = "Dance Voucher Redeem" Then
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = True
                        divPaymentHandlingFee.Visible = True
                        divItem.Visible = False
                        divLauncher.Visible = False
                        txtAmount.Text = ""
                        txtItem.Text = ""
                        txt_PaymentHandlingFee.Text = Val(row("Payment_Handling_Fee"))
                        txt_SalesHandlingFee.Text = ""
                        txt_TaxAmount.Text = ""
                        txt_TotalValue.Text = Val(row("Total_Value"))
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divZReport.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()

                    ElseIf ddCaption.SelectedItem.Value = "Surcharge" Then
                        txtItem1.Text = row("item")
                        divSurchargeAmount.Visible = True
                        divItem.Visible = False
                        divLauncher.Visible = False
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divPayment.Visible = False
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        chkgroupbycourse.Visible = False
                        chkgroupbydept.Visible = False
                        lblgroupbycourse.Visible = True
                        lblgroupbydept.Visible = True
                        divEOHO.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divZReport.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()

                    ElseIf ddCaption.SelectedItem.Value = "Eat out to help out" Then
                        divEOHO.Visible = True
                        itemEOHO.Text = row("item")
                        divSurchargeAmount.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divPayment.Visible = False
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        chkgroupbycourse.Visible = False
                        chkgroupbydept.Visible = False
                        lblgroupbycourse.Visible = True
                        lblgroupbydept.Visible = True
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divZReport.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()

                    ElseIf ddCaption.SelectedValue = "Integrated Pay" Then
                        divPayment.Visible = True
                        divPayType.Visible = True
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        ddPayment.SelectedValue = Val(row("Payment_id"))
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divZReport.Visible = False

                    ElseIf ddCaption.SelectedValue = "Card Sale" Then
                        divPayType.Visible = True
                        divCardSale.Visible = True
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        ddlCardType.SelectedValue = row("pay_type").ToString()
                        If ddlCardType.SelectedValue = "1" Then
                            divCardSub.Visible = True
                            If row("pay_sub_type").ToString() <> "" Then
                                ddlCardSubType.SelectedValue = row("pay_sub_type").ToString()
                            End If
                        End If
                        divPayment.Visible = False
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divZReport.Visible = False

                    ElseIf ddCaption.SelectedValue = "Account Search" Then
                        divZReport.Visible = False
                        divPayment.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        divPriceLevel.Visible = False

                        divAccountCreate.Visible = True
                        rdProfileType.SelectedValue = Val(row("profile_id"))
                        If row("DefaultDateTime").ToString() IsNot "" Then
                            Dim dft As String() = row("DefaultDateTime").ToString().Split("_")
                            txtDays.Text = dft(0).ToString()
                            rdExpTime.SelectedValue = dft(1).ToString()
                        End If

                    ElseIf ddCaption.SelectedItem.Value = "Change Price Level" Or ddCaption.SelectedItem.Value = "Price Level Transaction" Then

                        divPriceLevel.Visible = True
                        rdPriceLevel.SelectedValue = Val(row("item"))
                        divSurchargeAmount.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divPayment.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        chkgroupbycourse.Visible = False
                        chkgroupbydept.Visible = False
                        lblgroupbycourse.Visible = True
                        lblgroupbydept.Visible = True
                        divEOHO.Visible = False
                        divAccountCreate.Visible = False
                        divZReport.Visible = False

                    ElseIf ddCaption.SelectedItem.Value = "Z-Report BO" Then
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divZReport.Visible = True
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()

                        radVenue.SelectedValue = Val(row("ZR_VenueID"))
                        oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))
                        radLocation.SelectedValue = Val(row("ZR_LocationID"))
                        'radTill.SelectedValue = Val(row("ZR_TillID"))
                        oclsBind.BindMachineByLocation(radTill, Val(Session("cmp_id")), Val(radLocation.SelectedValue))

                        Dim collection As IList(Of RadComboBoxItem) = radTill.Items
                        Dim stArr As String() = row("ZR_TillID").ToString().Split("#")

                        For i As Integer = 0 To stArr.Length - 1
                            For Each item As RadComboBoxItem In collection
                                If stArr(i).ToString = item.Value Then
                                    item.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    ElseIf ddCaption.SelectedValue = "Launcher" Then
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()
                        divZReport.Visible = False
                        divPriceLevel.Visible = False
                        divAccountCreate.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        txtLauncher.Text = row("platformAdd")
                        txtAccountID.Text = row("clientToken")
                        divLauncher.Visible = True
                        'txtItem.Text = row("item")
                        txtAmount.Text = ""
                        txt_PaymentHandlingFee.Text = ""
                        txt_SalesHandlingFee.Text = ""
                        txt_TaxAmount.Text = ""
                        txt_TotalValue.Text = ""
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        chkgroupbycourse.Visible = True
                        chkgroupbydept.Visible = True
                        'ddldeaprtmentid.Visible = True
                        'ddlcourseid.Visible = True
                        'lblcourse.Visible = True
                        'lbldept.Visible = True
                        lblgroupbycourse.Visible = True
                        lblgroupbydept.Visible = True
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        If Val(row("is_groupby_dept") = 1) Then
                            chkgroupbydept.Checked = True
                        Else
                            chkgroupbydept.Checked = False
                        End If
                        If Val(row("is_groupby_course") = 1) Then
                            chkgroupbycourse.Checked = True
                        Else
                            chkgroupbycourse.Checked = False
                        End If
                        If chkgroupbydept.Checked = True Then

                            ddldeaprtmentid.Visible = True
                            lbldept.Visible = True
                            Dim collection As IList(Of RadComboBoxItem) = ddldeaprtmentid.Items
                            Dim stArr As String() = row("dept_id").ToString().Split("#")

                            For i As Integer = 0 To stArr.Length - 1
                                For Each item As RadComboBoxItem In collection
                                    If stArr(i).ToString = item.Value Then
                                        item.Checked = True
                                        Exit For
                                    End If
                                Next
                            Next
                        End If
                        If chkgroupbycourse.Checked = True Then
                            ddlcourseid.Visible = True
                            lblcourse.Visible = True
                            Dim stArr1 As String() = row("course_id").ToString().Split("#")

                            Dim collection1 As IList(Of RadComboBoxItem) = ddlcourseid.Items
                            For i As Integer = 0 To stArr1.Length - 1
                                For Each item1 As RadComboBoxItem In collection1
                                    If stArr1(i).ToString = item1.Value Then
                                        item1.Checked = True
                                        Exit For
                                    End If
                                Next
                            Next
                        End If
                    ElseIf ddCaption.SelectedValue = "Stripe Payment" Then
                        divLauncher.Visible = False
                        lblAccountID.Visible = True
                        txtAccountID.Visible = True

                        txtLauncher.Text = row("platformAdd")
                        txtAccountID.Text = row("clientToken")
                        divLauncher.Visible = True
                        divZReport.Visible = False
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        divItem.Visible = False
                        txtLauncher.Attributes("placeholder") = "Secret Key"
                        lbllauncher.Text = "Secret Key"

                        txtAccountID.Attributes("clientToken") = "Account ID"

                        lblAccountID.Text = "Account ID"
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()
                    Else
                        divZReport.Visible = False
                        divEOHO.Visible = False
                        divSurchargeAmount.Visible = False
                        divItem.Visible = False
                        divLauncher.Visible = False
                        divAmount.Visible = False
                        divTaxAmount.Visible = False
                        divSalesHandlingFee.Visible = False
                        divTotalValue.Visible = False
                        divPaymentHandlingFee.Visible = False
                        divCardSale.Visible = False
                        divCardSub.Visible = False
                        divAccountCreate.Visible = False
                        divPriceLevel.Visible = False
                        divPayType.Visible = False
                        ddlPayType.ClearSelection()
                    End If

                    radBackcolorpicker.SelectedColor = ColorTranslator.FromHtml(row("back_color"))
                    radForcolorpicker.SelectedColor = ColorTranslator.FromHtml(row("for_color"))
                    If row("is_active").ToString = "1" Then
                        chkActive.Checked = True
                    Else
                        chkActive.Checked = False
                    End If
                    If Val(row("big_button")) = 1 Then
                        chkbigbutton.Checked = True
                    Else
                        chkbigbutton.Checked = False
                    End If
                    Try
                        txtRoomAToken.Text = row("accessToken").ToString()
                        txtRoomCToken.Text = row("clientToken").ToString()
                        txtRoomPlatform.Text = row("platformAdd").ToString()
                        txtserviceid.Text = row("serviceid").ToString()
                    Catch ex As Exception

                    End Try
                    If ddlCardSubType.SelectedValue = "Room Payment" Then
                        divRoomDetails.Visible = True
                    Else
                        divRoomDetails.Visible = False
                    End If
                End If
            Next

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Key_Map_Master:BindData:" + ex.Message)
        End Try
    End Sub

    Private Sub Radeditpanel_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles Radeditpanel.SelectedIndexChanged
        Try

            SetName(btn_7by7_1, "F1", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_2, "F2", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_3, "F3", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_4, "F4", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_5, "F5", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_6, "F6", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_7, "F7", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_8, "F8", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_9, "F9", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_10, "F10", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_11, "F11", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_12, "F12", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_13, "F13", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_14, "F14", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_15, "F15", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_16, "F16", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_17, "F17", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_18, "F18", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_19, "F19", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_20, "F20", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_21, "F21", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_22, "F22", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_23, "F23", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_24, "F24", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_25, "F25", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_26, "F26", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_27, "F27", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_28, "F28", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_29, "F29", "#1B7BBD", "#FFFFFF")
            SetName(btn_7by7_30, "F30", "#1B7BBD", "#FFFFFF")



            If Not Session("mainfunction_id") = Nothing Then
                oclsFunction.cmp_id = Val(Session("cmp_id"))
                oclsFunction.Panel_id = Val(Radeditpanel.SelectedValue)
                oclsFunction.mainfunction_id = Val(Session("mainfunction_id"))
                Dim Edit_dt As DataTable = oclsFunction.SelectPanelFunction()

                Dim dt1 As DataTable = New DataTable
                dt1.Columns.Add("id", GetType(Integer))
                dt1.Columns.Add("cmp_id", GetType(Integer))
                dt1.Columns.Add("name", GetType(String))
                dt1.Columns.Add("code", GetType(String))
                dt1.Columns.Add("caption_type", GetType(String))
                dt1.Columns.Add("is_active", GetType(Integer))
                dt1.Columns.Add("ip_address", GetType(String))
                dt1.Columns.Add("login_id", GetType(Integer))
                dt1.Columns.Add("item", GetType(Decimal))
                dt1.Columns.Add("back_color", GetType(String))
                dt1.Columns.Add("for_color", GetType(String))
                dt1.Columns.Add("machine_id", GetType(Integer))
                dt1.Columns.Add("big_button", GetType(Integer))
                dt1.Columns.Add("Payment_id", GetType(Integer))
                dt1.Columns.Add("pay_type", GetType(Integer))
                dt1.Columns.Add("pay_sub_type", GetType(String))
                dt1.Columns.Add("is_groupby_dept", GetType(Integer))
                dt1.Columns.Add("is_groupby_course", GetType(Integer))
                dt1.Columns.Add("dept_id", GetType(String))
                dt1.Columns.Add("course_id", GetType(String))
                dt1.Columns.Add("Panel_Id", GetType(Integer))
                dt1.Columns.Add("Parent_id", GetType(Integer))
                dt1.Columns.Add("Is_Parent", GetType(Integer))
                dt1.Columns.Add("Function_Id", GetType(Integer))
                dt1.Columns.Add("platformAdd", GetType(String))
                dt1.Columns.Add("clientToken", GetType(String))
                dt1.Columns.Add("accessToken", GetType(String))
                dt1.Columns.Add("serviceid", GetType(String))
                dt1.Columns.Add("EOHOAmount", GetType(Decimal))
                dt1.Columns.Add("tax_id", GetType(Integer))
                dt1.Columns.Add("Total_Value", GetType(Decimal))
                dt1.Columns.Add("Sales_Handling_Fee", GetType(Decimal))
                dt1.Columns.Add("Payment_Handling_Fee", GetType(Decimal))
                dt1.Columns.Add("Tax_Amount", GetType(Decimal))
                dt1.Columns.Add("profile_id", GetType(Integer))
                dt1.Columns.Add("DefaultDateTime", GetType(String))
                dt1.Columns.Add("ZR_VenueID", GetType(Integer))
                dt1.Columns.Add("ZR_LocationID", GetType(Integer))
                dt1.Columns.Add("ZR_TillID", GetType(String))
                dt1.Columns.Add("CardPayType", GetType(Integer))

                Dim val2 As String = "F"

                For i = 1 To 30
                    Dim row As DataRow = dt1.NewRow()
                    row("id") = i
                    row("cmp_id") = 0
                    row("name") = ""
                    row("code") = val2 + i.ToString()
                    row("caption_type") = ""
                    row("is_active") = 1
                    row("ip_address") = ""
                    row("login_id") = 0
                    row("item") = 0.00
                    row("back_color") = "#1B7BBD"
                    row("for_color") = "#FFFFFF"
                    row("machine_id") = 0
                    row("big_button") = 0
                    row("Payment_id") = 0
                    row("pay_type") = 0
                    row("pay_sub_type") = ""
                    row("is_groupby_dept") = 0
                    row("is_groupby_course") = 0
                    row("dept_id") = 0
                    row("course_id") = 0
                    row("Panel_Id") = 0
                    row("Parent_id") = Val(Radeditpanel.SelectedValue)
                    row("Is_Parent") = 0
                    row("Function_Id") = 0
                    row("platformAdd") = ""
                    row("clientToken") = ""
                    row("accessToken") = ""
                    row("serviceid") = ""
                    row("EOHOAmount") = 0
                    row("tax_id") = 0
                    row("Total_Value") = 0
                    row("Sales_Handling_Fee") = 0
                    row("Payment_Handling_Fee") = 0
                    row("Tax_Amount") = 0
                    row("profile_id") = 0
                    row("DefaultDateTime") = ""
                    row("ZR_VenueID") = 0
                    row("ZR_LocationID") = 0
                    row("ZR_TillID") = ""
                    row("CardPayType") = 0


                    dt1.Rows.Add(row)
                Next
                Dim PreData1 As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

                For Each row As DataRow In Edit_dt.Rows
                    For Each row1 As DataRow In dt1.Rows
                        If row1("code").ToString() = row("code").ToString() And row1("Parent_id") = Val(Radeditpanel.SelectedValue) Then
                            row1("cmp_id") = row("cmp_id")
                            row1("name") = row("name")
                            row1("code") = row("code")
                            row1("caption_type") = row("caption_type")
                            row1("is_active") = row("is_active")
                            row1("ip_address") = row("ip_address")
                            row1("login_id") = row("login_id")
                            row1("item") = row("item")
                            row1("back_color") = row("back_color")
                            row1("for_color") = row("for_color")
                            row1("machine_id") = row("machine_id")
                            row1("big_button") = row("big_button")
                            row1("Payment_id") = row("Payment_id")
                            row1("pay_type") = row("pay_type")
                            row1("pay_sub_type") = row("pay_sub_type")
                            row1("is_groupby_dept") = row("is_groupby_dept")
                            row1("is_groupby_course") = row("is_groupby_course")
                            row1("dept_id") = row("dept_id")
                            row1("course_id") = row("course_id")
                            row1("Panel_Id") = row("Panel_Id")
                            row1("Parent_id") = row("Parent_id")
                            row1("Is_Parent") = row("Parent_id")
                            row1("Function_Id") = row("Function_Id")
                            row1("platformAdd") = row("platformAdd").ToString()
                            row1("clientToken") = row("clientToken").ToString()
                            row1("accessToken") = row("accessToken").ToString()
                            row1("serviceid") = row("serviceid").ToString()
                            row1("EOHOAmount") = row("EOHOAmount").ToString()
                            row1("tax_id") = row("tax_id").ToString()
                            row1("Total_Value") = Val(row("Total_Value").ToString())
                            row1("Sales_Handling_Fee") = Val(row("Sales_Handling_Fee").ToString())
                            row1("Payment_Handling_Fee") = Val(row("Payment_Handling_Fee").ToString())
                            row1("Tax_Amount") = Val(row("Tax_Amount").ToString())
                            row1("profile_id") = Val(row("profile_id").ToString())
                            row1("DefaultDateTime") = row("DefaultDateTime").ToString()
                            row1("ZR_VenueID") = Val(row("ZR_VenueID").ToString())
                            row1("ZR_LocationID") = Val(row("ZR_LocationID").ToString())
                            row1("ZR_TillID") = row("ZR_TillID").ToString()
                            row1("CardPayType") = Val(row("CardPayType").ToString())


                        End If
                    Next
                Next

                PreData1.Merge(dt1)
                ViewState("Data_dt") = PreData1


                For Each row1 As DataRow In dt1.Rows
                    Dim Code As Integer = Val(row1("code").ToString().Replace("F", "")) + 1

                    If row1("big_button") = "1" Then
                        If Code = 2 Then
                            btn_7by7_2.Visible = False
                            btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 3 Then
                            btn_7by7_3.Visible = False
                            btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 4 Then
                            btn_7by7_4.Visible = False
                            btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 5 Then
                            btn_7by7_5.Visible = False
                            btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 6 Then
                            btn_7by7_6.Visible = False
                            btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 7 Then
                            btn_7by7_7.Visible = False
                            btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 8 Then
                            btn_7by7_8.Visible = False
                            btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 9 Then
                            btn_7by7_9.Visible = False
                            btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 10 Then
                            btn_7by7_10.Visible = False
                            btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"

                        ElseIf Code = 11 Then
                            btn_7by7_11.Visible = False
                            btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"


                        ElseIf Code = 12 Then
                            btn_7by7_12.Visible = False
                            btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left:1%;"
                        ElseIf Code = 13 Then
                            btn_7by7_13.Visible = False
                            btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                        ElseIf Code = 14 Then
                            btn_7by7_14.Visible = False
                            btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;"
                            'ElseIf Code = 15 Then
                            '    btn_7by7_15.Visible = False
                            '    btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 16 Then
                            btn_7by7_16.Visible = False
                            btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;  margin-bottom: 10px;"
                        ElseIf Code = 17 Then
                            btn_7by7_17.Visible = False
                            btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;  margin-bottom: 10px;"
                        ElseIf Code = 18 Then
                            btn_7by7_18.Visible = False
                            btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;  margin-bottom: 10px;"
                        ElseIf Code = 19 Then
                            btn_7by7_19.Visible = False
                            btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;  margin-bottom: 10px;"
                        ElseIf Code = 20 Then
                            btn_7by7_20.Visible = False
                            btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;  margin-bottom: 10px;"
                        ElseIf Code = 21 Then
                            btn_7by7_21.Visible = False
                            btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;  margin-bottom: 10px;"
                        ElseIf Code = 22 Then
                            btn_7by7_22.Visible = False
                            btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;  margin-bottom: 10px;"
                        ElseIf Code = 23 Then
                            btn_7by7_23.Visible = False
                            btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%; margin-bottom: 10px;"
                        ElseIf Code = 24 Then
                            btn_7by7_24.Visible = False
                            btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%; margin-bottom: 10px;"
                        ElseIf Code = 25 Then
                            btn_7by7_25.Visible = False
                            btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%; margin-bottom: 10px;"
                        ElseIf Code = 26 Then
                            btn_7by7_26.Visible = False
                            btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%; margin-bottom: 10px;"
                        ElseIf Code = 27 Then
                            btn_7by7_27.Visible = False
                            btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%; margin-bottom: 10px;"
                        ElseIf Code = 28 Then
                            btn_7by7_28.Visible = False
                            btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%; margin-bottom: 10px;"
                            'ElseIf Code = 29 Then
                            '    btn_7by7_29.Visible = False
                            '    btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 30 Then
                            btn_7by7_30.Visible = False
                            btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        End If
                    Else
                        If Code = 2 Then
                            If btn_7by7_2.Text = "F2" Then
                                btn_7by7_2.Visible = True
                                btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 3 Then
                            If btn_7by7_3.Text = "F3" Then
                                btn_7by7_3.Visible = True
                                btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 4 Then
                            If btn_7by7_4.Text = "F4" Then
                                btn_7by7_4.Visible = True
                                btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 5 Then
                            If btn_7by7_5.Text = "F5" Then
                                btn_7by7_5.Visible = True
                                btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 6 Then
                            If btn_7by7_6.Text = "F6" Then
                                btn_7by7_6.Visible = True
                                btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 7 Then
                            If btn_7by7_7.Text = "F7" Then
                                btn_7by7_7.Visible = True
                                btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 8 Then
                            If btn_7by7_8.Text = "F8" Then
                                btn_7by7_8.Visible = True
                                btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 9 Then
                            If btn_7by7_9.Text = "F9" Then
                                btn_7by7_9.Visible = True
                                btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 10 Then
                            If btn_7by7_10.Text = "F10" Then
                                btn_7by7_10.Visible = True
                                btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If

                        ElseIf Code = 11 Then
                            If btn_7by7_11.Text = "F11" Then
                                btn_7by7_11.Visible = True
                                btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If

                        ElseIf Code = 12 Then
                            If btn_7by7_12.Text = "F12" Then
                                btn_7by7_12.Visible = True
                                btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 13 Then
                            If btn_7by7_13.Text = "F13" Then
                                btn_7by7_13.Visible = True
                                btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 14 Then
                            If btn_7by7_14.Text = "F14" Then
                                btn_7by7_14.Visible = True
                                btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                            'ElseIf Code = 15 Then
                            '    If btn_7by7_15.Text = "F15" Then
                            '        btn_7by7_15.Visible = True
                            '        btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            '    End If
                        ElseIf Code = 16 Then
                            If btn_7by7_16.Text = "F16" Then
                                btn_7by7_16.Visible = True
                                btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 17 Then
                            If btn_7by7_17.Text = "F17" Then
                                btn_7by7_17.Visible = True
                                btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3% ; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 18 Then
                            If btn_7by7_18.Text = "F18" Then
                                btn_7by7_18.Visible = True
                                btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 19 Then
                            If btn_7by7_19.Text = "F19" Then
                                btn_7by7_19.Visible = True
                                btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 20 Then
                            If btn_7by7_20.Text = "F20" Then
                                btn_7by7_20.Visible = True
                                btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 21 Then
                            If btn_7by7_21.Text = "F21" Then
                                btn_7by7_21.Visible = True
                                btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 22 Then
                            If btn_7by7_22.Text = "F22" Then
                                btn_7by7_22.Visible = True
                                btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px; "
                            End If
                        ElseIf Code = 23 Then
                            If btn_7by7_23.Text = "F23" Then
                                btn_7by7_23.Visible = True
                                btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 24 Then
                            If btn_7by7_24.Text = "F24" Then
                                btn_7by7_24.Visible = True
                                btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 25 Then
                            If btn_7by7_25.Text = "F25" Then
                                btn_7by7_25.Visible = True
                                btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 26 Then
                            If btn_7by7_26.Text = "F26" Then
                                btn_7by7_26.Visible = True
                                btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 27 Then
                            If btn_7by7_27.Text = "F27" Then
                                btn_7by7_27.Visible = True
                                btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 28 Then
                            If btn_7by7_28.Text = "F28" Then
                                btn_7by7_28.Visible = True
                                btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                            'ElseIf Code = 29 Then
                            '    If btn_7by7_29.Text = "F29" Then
                            '        btn_7by7_29.Visible = True
                            '        btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            '    End If
                        ElseIf Code = 30 Then
                            If btn_7by7_30.Text = "F30" Then
                                btn_7by7_30.Visible = True
                                btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        End If
                    End If


                    If row1("name") = "" Then
                    Else
                        If row1("code") = "F1" Then
                            SetName(btn_7by7_1, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F2" Then
                            SetName(btn_7by7_2, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F3" Then
                            SetName(btn_7by7_3, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F4" Then
                            SetName(btn_7by7_4, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F5" Then
                            SetName(btn_7by7_5, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F6" Then
                            SetName(btn_7by7_6, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F7" Then
                            SetName(btn_7by7_7, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F8" Then
                            SetName(btn_7by7_8, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F9" Then
                            SetName(btn_7by7_9, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F10" Then
                            SetName(btn_7by7_10, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F11" Then
                            SetName(btn_7by7_11, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F12" Then
                            SetName(btn_7by7_12, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F13" Then
                            SetName(btn_7by7_13, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F14" Then
                            SetName(btn_7by7_14, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F15" Then
                            SetName(btn_7by7_15, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F16" Then
                            SetName(btn_7by7_16, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F17" Then
                            SetName(btn_7by7_17, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F18" Then
                            SetName(btn_7by7_18, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F19" Then
                            SetName(btn_7by7_19, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F20" Then
                            SetName(btn_7by7_20, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F21" Then
                            SetName(btn_7by7_21, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F22" Then
                            SetName(btn_7by7_22, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F23" Then
                            SetName(btn_7by7_23, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F24" Then
                            SetName(btn_7by7_24, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F25" Then
                            SetName(btn_7by7_25, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F26" Then
                            SetName(btn_7by7_26, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F27" Then
                            SetName(btn_7by7_27, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F28" Then
                            SetName(btn_7by7_28, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F29" Then
                            SetName(btn_7by7_29, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F30" Then
                            SetName(btn_7by7_30, row1("name"), row1("back_color"), row1("for_color"))
                        End If
                    End If

                Next
            Else
                Dim dt As DataTable = New DataTable
                dt.Columns.Add("id", GetType(Integer))
                dt.Columns.Add("cmp_id", GetType(Integer))
                dt.Columns.Add("name", GetType(String))
                dt.Columns.Add("code", GetType(String))
                dt.Columns.Add("caption_type", GetType(String))
                dt.Columns.Add("is_active", GetType(Integer))
                dt.Columns.Add("ip_address", GetType(String))
                dt.Columns.Add("login_id", GetType(Integer))
                dt.Columns.Add("item", GetType(Decimal))
                dt.Columns.Add("back_color", GetType(String))
                dt.Columns.Add("for_color", GetType(String))
                dt.Columns.Add("machine_id", GetType(Integer))
                dt.Columns.Add("big_button", GetType(Integer))
                dt.Columns.Add("Payment_id", GetType(Integer))
                dt.Columns.Add("pay_type", GetType(Integer))
                dt.Columns.Add("pay_sub_type", GetType(String))
                dt.Columns.Add("is_groupby_dept", GetType(Integer))
                dt.Columns.Add("is_groupby_course", GetType(Integer))
                dt.Columns.Add("dept_id", GetType(String))
                dt.Columns.Add("course_id", GetType(String))
                dt.Columns.Add("Panel_Id", GetType(Integer))
                dt.Columns.Add("Parent_id", GetType(Integer))
                dt.Columns.Add("Is_Parent", GetType(Integer))
                dt.Columns.Add("Function_Id", GetType(Integer))
                dt.Columns.Add("platformAdd", GetType(String))
                dt.Columns.Add("clientToken", GetType(String))
                dt.Columns.Add("accessToken", GetType(String))
                dt.Columns.Add("serviceid", GetType(String))
                dt.Columns.Add("EOHOAmount", GetType(Decimal))
                dt.Columns.Add("tax_id", GetType(Integer))
                dt.Columns.Add("Total_Value", GetType(Decimal))
                dt.Columns.Add("Sales_Handling_Fee", GetType(Decimal))
                dt.Columns.Add("Payment_Handling_Fee", GetType(Decimal))
                dt.Columns.Add("Tax_Amount", GetType(Decimal))
                dt.Columns.Add("profile_id", GetType(Integer))
                dt.Columns.Add("DefaultDateTime", GetType(String))
                dt.Columns.Add("ZR_VenueID", GetType(Integer))
                dt.Columns.Add("ZR_LocationID", GetType(Integer))
                dt.Columns.Add("ZR_TillID", GetType(String))
                dt.Columns.Add("CardPayType", GetType(Integer))

                Dim val1 As String = "F"
                For i = 1 To 30
                    Dim row As DataRow = dt.NewRow()
                    row("id") = i
                    row("cmp_id") = 0
                    row("name") = ""
                    row("code") = val1 + i.ToString()
                    row("caption_type") = ""
                    row("is_active") = 1
                    row("ip_address") = ""
                    row("login_id") = 0
                    row("item") = 0.00
                    row("back_color") = "#1B7BBD"
                    row("for_color") = "#FFFFFF"
                    row("machine_id") = 0
                    row("big_button") = 0
                    row("Payment_id") = 0
                    row("pay_type") = 0
                    row("pay_sub_type") = ""
                    row("is_groupby_dept") = 0
                    row("is_groupby_course") = 0
                    row("dept_id") = 0
                    row("course_id") = 0
                    row("Panel_Id") = 0
                    row("Parent_id") = Val(Radeditpanel.SelectedValue)
                    row("Is_Parent") = 0
                    row("Function_Id") = 0
                    row("platformAdd") = ""
                    row("clientToken") = ""
                    row("accessToken") = ""
                    row("serviceid") = ""
                    row("EOHOAmount") = 0
                    row("tax_id") = 0
                    row("Total_Value") = 0
                    row("Sales_Handling_Fee") = 0
                    row("Payment_Handling_Fee") = 0
                    row("Tax_Amount") = 0
                    row("profile_id") = 0
                    row("DefaultDateTime") = ""
                    row("ZR_VenueID") = 0
                    row("ZR_LocationID") = 0
                    row("ZR_TillID") = ""
                    row("CardPayType") = 0


                    dt.Rows.Add(row)
                Next

                Dim PreData As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

                Dim dr1 As DataRow() = PreData.Select("Parent_Id='" & Val(Radeditpanel.SelectedValue) & "'")
                If dr1.Length > 1 Then
                    'PreData.Merge(dt)
                    'ViewState("Data_dt") = PreData
                Else
                    PreData.Merge(dt)
                    ViewState("Data_dt") = PreData
                End If

                Dim PreData2 As DataTable = DirectCast(ViewState("Data_dt"), DataTable)
                Dim dr As DataRow() = PreData2.Select("Parent_Id='" & Val(Radeditpanel.SelectedValue) & "'")

                ViewState("Data_dt1") = dr.CopyToDataTable()

                Dim dt1 As DataTable = DirectCast(ViewState("Data_dt1"), DataTable)

                For Each row1 As DataRow In dt1.Rows
                    Dim Code As Integer = Val(row1("code").ToString().Replace("F", "")) + 1

                    'If row1("name") = "" Then

                    'Else
                    If row1("big_button") = "1" Then
                        If Code = 2 Then
                            btn_7by7_2.Visible = False
                            btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 3 Then
                            btn_7by7_3.Visible = False
                            btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 4 Then
                            btn_7by7_4.Visible = False
                            btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 5 Then
                            btn_7by7_5.Visible = False
                            btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 6 Then
                            btn_7by7_6.Visible = False
                            btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 7 Then
                            btn_7by7_7.Visible = False
                            btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 8 Then
                            btn_7by7_8.Visible = False
                            btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 9 Then
                            btn_7by7_9.Visible = False
                            btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 10 Then
                            btn_7by7_10.Visible = False
                            btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 11 Then
                            btn_7by7_11.Visible = False
                            btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"



                        ElseIf Code = 12 Then
                            btn_7by7_12.Visible = False
                            btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 13 Then
                            btn_7by7_13.Visible = False
                            btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 14 Then
                            btn_7by7_14.Visible = False
                            btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            'ElseIf Code = 15 Then
                            '    btn_7by7_15.Visible = False
                            '    btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 16 Then
                            btn_7by7_16.Visible = False
                            btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 17 Then
                            btn_7by7_17.Visible = False
                            btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px; "
                        ElseIf Code = 18 Then
                            btn_7by7_18.Visible = False
                            btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                        ElseIf Code = 19 Then
                            btn_7by7_19.Visible = False
                            btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                        ElseIf Code = 20 Then
                            btn_7by7_20.Visible = False
                            btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 21 Then
                            btn_7by7_21.Visible = False
                            btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 22 Then
                            btn_7by7_22.Visible = False
                            btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 23 Then
                            btn_7by7_23.Visible = False
                            btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 24 Then
                            btn_7by7_24.Visible = False
                            btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 25 Then
                            btn_7by7_25.Visible = False
                            btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 26 Then
                            btn_7by7_26.Visible = False
                            btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 27 Then
                            btn_7by7_27.Visible = False
                            btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                        ElseIf Code = 28 Then
                            btn_7by7_28.Visible = False
                            btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            'ElseIf Code = 29 Then
                            '    btn_7by7_29.Visible = False
                            '    btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        ElseIf Code = 30 Then
                            btn_7by7_30.Visible = False
                            btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                        End If
                    Else
                        If Code = 2 Then
                            If btn_7by7_2.Text = "F2" Then
                                btn_7by7_2.Visible = True
                                btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 3 Then
                            If btn_7by7_3.Text = "F3" Then
                                btn_7by7_3.Visible = True
                                btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 4 Then
                            If btn_7by7_4.Text = "F4" Then
                                btn_7by7_4.Visible = True
                                btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 5 Then
                            If btn_7by7_5.Text = "F5" Then
                                btn_7by7_5.Visible = True
                                btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 6 Then
                            If btn_7by7_6.Text = "F6" Then
                                btn_7by7_6.Visible = True
                                btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 7 Then
                            If btn_7by7_7.Text = "F7" Then
                                btn_7by7_7.Visible = True
                                btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 8 Then
                            If btn_7by7_8.Text = "F8" Then
                                btn_7by7_8.Visible = True
                                btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 9 Then
                            If btn_7by7_9.Text = "F9" Then
                                btn_7by7_9.Visible = True
                                btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 10 Then
                            If btn_7by7_10.Text = "F10" Then
                                btn_7by7_10.Visible = True
                                btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If

                        ElseIf Code = 11 Then
                            If btn_7by7_11.Text = "F11" Then
                                btn_7by7_11.Visible = True
                                btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If

                        ElseIf Code = 12 Then
                            If btn_7by7_12.Text = "F12" Then
                                btn_7by7_12.Visible = True
                                btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 13 Then
                            If btn_7by7_13.Text = "F13" Then
                                btn_7by7_13.Visible = True
                                btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        ElseIf Code = 14 Then
                            If btn_7by7_14.Text = "F14" Then
                                btn_7by7_14.Visible = True
                                btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                            'ElseIf Code = 15 Then
                            '    If btn_7by7_15.Text = "F15" Then
                            '        btn_7by7_15.Visible = True
                            '        btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            '    End If
                        ElseIf Code = 16 Then
                            If btn_7by7_16.Text = "F16" Then
                                btn_7by7_16.Visible = True
                                btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 17 Then
                            If btn_7by7_17.Text = "F17" Then
                                btn_7by7_17.Visible = True
                                btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 18 Then
                            If btn_7by7_18.Text = "F18" Then
                                btn_7by7_18.Visible = True
                                btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px; "
                            End If
                        ElseIf Code = 19 Then
                            If btn_7by7_19.Text = "F19" Then
                                btn_7by7_19.Visible = True
                                btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            End If
                        ElseIf Code = 20 Then
                            If btn_7by7_20.Text = "F20" Then
                                btn_7by7_20.Visible = True
                                btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 21 Then
                            If btn_7by7_21.Text = "F21" Then
                                btn_7by7_21.Visible = True
                                btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If

                        ElseIf Code = 22 Then
                            If btn_7by7_22.Text = "F22" Then
                                btn_7by7_22.Visible = True
                                btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 23 Then
                            If btn_7by7_23.Text = "F23" Then
                                btn_7by7_23.Visible = True
                                btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 24 Then
                            If btn_7by7_24.Text = "F24" Then
                                btn_7by7_24.Visible = True
                                btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 25 Then
                            If btn_7by7_25.Text = "F25" Then
                                btn_7by7_25.Visible = True
                                btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 26 Then
                            If btn_7by7_26.Text = "F26" Then
                                btn_7by7_26.Visible = True
                                btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 27 Then
                            If btn_7by7_27.Text = "F27" Then
                                btn_7by7_27.Visible = True
                                btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                        ElseIf Code = 28 Then
                            If btn_7by7_28.Text = "F28" Then
                                btn_7by7_28.Visible = True
                                btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                            End If
                            'ElseIf Code = 29 Then
                            '    If btn_7by7_29.Text = "F29" Then
                            '        btn_7by7_29.Visible = True
                            '        btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            '    End If
                        ElseIf Code = 30 Then
                            If btn_7by7_30.Text = "F30" Then
                                btn_7by7_30.Visible = True
                                btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        End If
                    End If

                    'End If

                    If row1("name") = "" Then
                    Else
                        If row1("code") = "F1" Then
                            SetName(btn_7by7_1, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F2" Then
                            SetName(btn_7by7_2, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F3" Then
                            SetName(btn_7by7_3, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F4" Then
                            SetName(btn_7by7_4, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F5" Then
                            SetName(btn_7by7_5, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F6" Then
                            SetName(btn_7by7_6, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F7" Then
                            SetName(btn_7by7_7, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F8" Then
                            SetName(btn_7by7_8, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F9" Then
                            SetName(btn_7by7_9, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F10" Then
                            SetName(btn_7by7_10, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F11" Then
                            SetName(btn_7by7_11, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F12" Then
                            SetName(btn_7by7_12, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F13" Then
                            SetName(btn_7by7_13, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F14" Then
                            SetName(btn_7by7_14, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F15" Then
                            SetName(btn_7by7_15, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F16" Then
                            SetName(btn_7by7_16, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F17" Then
                            SetName(btn_7by7_17, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F18" Then
                            SetName(btn_7by7_18, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F19" Then
                            SetName(btn_7by7_19, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F20" Then
                            SetName(btn_7by7_20, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F21" Then
                            SetName(btn_7by7_21, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F22" Then
                            SetName(btn_7by7_22, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F23" Then
                            SetName(btn_7by7_23, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F24" Then
                            SetName(btn_7by7_24, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F25" Then
                            SetName(btn_7by7_25, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F26" Then
                            SetName(btn_7by7_26, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F27" Then
                            SetName(btn_7by7_27, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F28" Then
                            SetName(btn_7by7_28, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F29" Then
                            SetName(btn_7by7_29, row1("name"), row1("back_color"), row1("for_color"))
                        ElseIf row1("code") = "F30" Then
                            SetName(btn_7by7_30, row1("name"), row1("back_color"), row1("for_color"))
                        End If
                    End If

                Next

            End If

            If Radeditpanel.SelectedValue > 0 Then
                divParentFunction.Visible = False
            Else
                divParentFunction.Visible = True
            End If

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")

            txtFName.Text = ""
            ddParent.ClearSelection()
            ddCaption.ClearSelection()
            ddlPayType.ClearSelection()
            oclsBind.BindFunctionType(ddCaption, Val(Session("cmp_id")))
            'ddCaption.Items(0).Selected = True
            txtItem.Text = ""
            txtLauncher.Text = ""
            txtAccountID.Text = ""
            txtAmount.Text = ""
            ddPayment.ClearSelection()
            ddlCardType.ClearSelection()
            ddlCardSubType.ClearSelection()
            chkActive.Checked = True
            radBackcolorpicker.SelectedColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            radForcolorpicker.SelectedColor = ColorTranslator.FromHtml("#FFFFFF")
            chkbigbutton.Checked = False
            chkgroupbydept.Checked = False
            chkgroupbycourse.Checked = False
            ddldeaprtmentid.ClearSelection()
            ddlcourseid.ClearSelection()
            txt_PaymentHandlingFee.Text = ""
            txt_SalesHandlingFee.Text = ""
            txt_TaxAmount.Text = ""
            txt_TotalValue.Text = ""
            divTaxAmount.Visible = False
            divSalesHandlingFee.Visible = False
            divTotalValue.Visible = False
            divPaymentHandlingFee.Visible = False
            rdExpTime.SelectedValue = "DD"
            rdProfileType.SelectedIndex = 0
            txtDays.Text = ""

        Catch ex As Exception
            LogHelper.Error("Function_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub btn_clear_Click(sender As Object, e As EventArgs) Handles btn_clear.Click
        Try

            If Session("Row_Id") = "1" Then
                btn_7by7_1.Text = "F1"
                btn_7by7_1.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "2" Then
                btn_7by7_2.Text = "F2"
                btn_7by7_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "3" Then
                btn_7by7_3.Text = "F3"
                btn_7by7_3.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "4" Then
                btn_7by7_4.Text = "F4"
                btn_7by7_4.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "5" Then
                btn_7by7_5.Text = "F5"
                btn_7by7_5.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "6" Then
                btn_7by7_6.Text = "F6"
                btn_7by7_6.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "7" Then
                btn_7by7_7.Text = "F7"
                btn_7by7_7.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "8" Then
                btn_7by7_8.Text = "F8"
                btn_7by7_8.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "9" Then
                btn_7by7_9.Text = "F9"
                btn_7by7_9.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "10" Then
                btn_7by7_10.Text = "F10"
                btn_7by7_10.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "11" Then
                btn_7by7_11.Text = "F11"
                btn_7by7_11.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "12" Then
                btn_7by7_12.Text = "F12"
                btn_7by7_12.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "13" Then
                btn_7by7_13.Text = "F13"
                btn_7by7_13.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "14" Then
                btn_7by7_14.Text = "F14"
                btn_7by7_14.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "15" Then
                btn_7by7_15.Text = "F15"
                btn_7by7_15.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "16" Then
                btn_7by7_16.Text = "F16"
                btn_7by7_16.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "17" Then
                btn_7by7_17.Text = "F17"
                btn_7by7_17.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "18" Then
                btn_7by7_18.Text = "F18"
                btn_7by7_18.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "19" Then
                btn_7by7_19.Text = "F19"
                btn_7by7_19.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "20" Then
                btn_7by7_20.Text = "F20"
                btn_7by7_20.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "21" Then
                btn_7by7_21.Text = "F21"
                btn_7by7_21.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "22" Then
                btn_7by7_22.Text = "F22"
                btn_7by7_22.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "23" Then
                btn_7by7_23.Text = "F23"
                btn_7by7_23.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "24" Then
                btn_7by7_24.Text = "F24"
                btn_7by7_24.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "25" Then
                btn_7by7_25.Text = "F25"
                btn_7by7_25.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "26" Then
                btn_7by7_26.Text = "F26"
                btn_7by7_26.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "27" Then
                btn_7by7_27.Text = "F27"
                btn_7by7_27.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "28" Then
                btn_7by7_28.Text = "F28"
                btn_7by7_28.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "29" Then
                btn_7by7_29.Text = "F29"
                btn_7by7_29.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            ElseIf Session("Row_Id") = "30" Then
                btn_7by7_30.Text = "F30"
                btn_7by7_30.BackColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            End If

            If Not Session("mainfunction_id") = Nothing Then
                Dim dt1 As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

                For Each row As DataRow In dt1.Rows
                    If Session("Row_Id").ToString() = row("id").ToString() And IIf(Radeditpanel.SelectedValue.ToString() = "0" Or Radeditpanel.SelectedValue.ToString() = "", "0", Val(Radeditpanel.SelectedValue.ToString())) = row("Parent_Id") Then

                        oclsFunction.function_id = Val(row("Function_Id"))
                        oclsFunction.cmp_id = Val(Session("cmp_id"))
                        oclsFunction.name = ""
                        oclsFunction.code = ""
                        oclsFunction.caption_type = ""
                        oclsFunction.is_active = 1
                        oclsFunction.shorting_no = ""
                        oclsFunction.ip_address = ""
                        oclsFunction.login_id = 0
                        oclsFunction.item = 0
                        oclsFunction.back_color = ""
                        oclsFunction.for_color = ""
                        oclsFunction.machine_id = 0
                        oclsFunction.big_button = 0
                        oclsFunction.Payment_id = 0
                        oclsFunction.pay_type = 0
                        oclsFunction.pay_sub_type = ""
                        oclsFunction.isgroupbydept = 0
                        oclsFunction.isgroupbycourse = 0
                        oclsFunction.dept_id = ""
                        oclsFunction.course_id = ""
                        oclsFunction.Panel_id = 0
                        oclsFunction.Parent_id = 0
                        oclsFunction.mainfunction_id = 0
                        oclsFunction.Tran_Type = "D"
                        oclsFunction.Delete()


                        Dim Code As Integer = Val(row("code").ToString().Replace("F", "")) + 1
                        row("cmp_id") = 0
                        row("name") = ""
                        row("code") = ""
                        row("caption_type") = ""
                        row("is_active") = 0
                        row("ip_address") = ""
                        row("login_id") = 0
                        row("item") = 0
                        row("back_color") = ""
                        row("for_color") = ""
                        row("machine_id") = 0
                        row("big_button") = 0
                        row("Payment_id") = 0
                        row("pay_type") = 0
                        row("pay_sub_type") = ""
                        row("is_groupby_dept") = 0
                        row("is_groupby_course") = 0
                        row("dept_id") = 0
                        row("course_id") = 0
                        row("Panel_Id") = 0
                        row("Parent_id") = 0
                        row("Is_Parent") = 0
                        row("Function_Id") = 0

                        If row("big_button") = "1" Then

                        Else
                            If Code = 2 Then
                                If btn_7by7_2.Text = "F2" Then
                                    btn_7by7_2.Visible = True
                                    btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 3 Then
                                If btn_7by7_3.Text = "F3" Then
                                    btn_7by7_3.Visible = True
                                    btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 4 Then
                                If btn_7by7_4.Text = "F4" Then
                                    btn_7by7_4.Visible = True
                                    btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 5 Then
                                If btn_7by7_5.Text = "F5" Then
                                    btn_7by7_5.Visible = True
                                    btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 6 Then
                                If btn_7by7_6.Text = "F6" Then
                                    btn_7by7_6.Visible = True
                                    btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 7 Then
                                If btn_7by7_7.Text = "F7" Then
                                    btn_7by7_7.Visible = True
                                    btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 8 Then
                                If btn_7by7_8.Text = "F8" Then
                                    btn_7by7_8.Visible = True
                                    btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 9 Then
                                If btn_7by7_9.Text = "F9" Then
                                    btn_7by7_9.Visible = True
                                    btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 10 Then
                                If btn_7by7_10.Text = "F10" Then
                                    btn_7by7_10.Visible = True
                                    btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If

                            ElseIf Code = 11 Then
                                If btn_7by7_11.Text = "F11" Then
                                    btn_7by7_11.Visible = True
                                    btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If


                            ElseIf Code = 12 Then
                                If btn_7by7_12.Text = "F12" Then
                                    btn_7by7_12.Visible = True
                                    btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 13 Then
                                If btn_7by7_13.Text = "F13" Then
                                    btn_7by7_13.Visible = True
                                    btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 14 Then
                                If btn_7by7_14.Text = "F14" Then
                                    btn_7by7_14.Visible = True
                                    btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                                'ElseIf Code = 15 Then
                                '    If btn_7by7_15.Text = "F15" Then
                                '        btn_7by7_15.Visible = True
                                '        btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                '    End If
                            ElseIf Code = 16 Then
                                If btn_7by7_16.Text = "F16" Then
                                    btn_7by7_16.Visible = True
                                    btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                                End If
                            ElseIf Code = 17 Then
                                If btn_7by7_17.Text = "F17" Then
                                    btn_7by7_17.Visible = True
                                    btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                                End If
                            ElseIf Code = 18 Then
                                If btn_7by7_18.Text = "F18" Then
                                    btn_7by7_18.Visible = True
                                    btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                                End If
                            ElseIf Code = 19 Then
                                If btn_7by7_19.Text = "F19" Then
                                    btn_7by7_19.Visible = True
                                    btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;  margin-bottom: 10px;"
                                End If
                            ElseIf Code = 20 Then
                                If btn_7by7_20.Text = "F20" Then
                                    btn_7by7_20.Visible = True
                                    btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;margin-bottom: 10px; "
                                End If
                            ElseIf Code = 21 Then
                                If btn_7by7_21.Text = "F21" Then
                                    btn_7by7_21.Visible = True
                                    btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 22 Then
                                If btn_7by7_22.Text = "F22" Then
                                    btn_7by7_22.Visible = True
                                    btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 23 Then
                                If btn_7by7_23.Text = "F23" Then
                                    btn_7by7_23.Visible = True
                                    btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 24 Then
                                If btn_7by7_24.Text = "F24" Then
                                    btn_7by7_24.Visible = True
                                    btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 25 Then
                                If btn_7by7_25.Text = "F25" Then
                                    btn_7by7_25.Visible = True
                                    btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 26 Then
                                If btn_7by7_26.Text = "F26" Then
                                    btn_7by7_26.Visible = True
                                    btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 27 Then
                                If btn_7by7_27.Text = "F27" Then
                                    btn_7by7_27.Visible = True
                                    btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 28 Then
                                If btn_7by7_28.Text = "F28" Then
                                    btn_7by7_28.Visible = True
                                    btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                                'ElseIf Code = 29 Then
                                '    If btn_7by7_29.Text = "F29" Then
                                '        btn_7by7_29.Visible = True
                                '        btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                '    End If
                            ElseIf Code = 30 Then
                                If btn_7by7_30.Text = "F30" Then
                                    btn_7by7_30.Visible = True
                                    btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            End If
                        End If
                    End If
                Next

                ViewState("Data_dt") = dt1
            Else
                Dim dt1 As DataTable = DirectCast(ViewState("Data_dt"), DataTable)

                For Each row As DataRow In dt1.Rows
                    If Session("Row_Id").ToString() = row("id").ToString() And IIf(Radeditpanel.SelectedValue.ToString() = "0" Or Radeditpanel.SelectedValue.ToString() = "", "0", Val(Radeditpanel.SelectedValue.ToString())) = row("Parent_Id") Then
                        Dim Code As Integer = Val(row("code").ToString().Replace("F", "")) + 1
                        row("cmp_id") = 0
                        row("name") = ""
                        row("code") = ""
                        row("caption_type") = ""
                        row("is_active") = 0
                        row("ip_address") = ""
                        row("login_id") = 0
                        row("item") = 0
                        row("back_color") = ""
                        row("for_color") = ""
                        row("machine_id") = 0
                        row("big_button") = 0
                        row("Payment_id") = 0
                        row("pay_type") = 0
                        row("pay_sub_type") = ""
                        row("is_groupby_dept") = 0
                        row("is_groupby_course") = 0
                        row("dept_id") = 0
                        row("course_id") = 0
                        row("Panel_Id") = 0
                        row("Parent_id") = 0
                        row("Is_Parent") = 0


                        If row("big_button") = "1" Then

                        Else
                            If Code = 2 Then
                                If btn_7by7_2.Text = "F2" Then
                                    btn_7by7_2.Visible = True
                                    btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 3 Then
                                If btn_7by7_3.Text = "F3" Then
                                    btn_7by7_3.Visible = True
                                    btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 4 Then
                                If btn_7by7_4.Text = "F4" Then
                                    btn_7by7_4.Visible = True
                                    btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 5 Then
                                If btn_7by7_5.Text = "F5" Then
                                    btn_7by7_5.Visible = True
                                    btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 6 Then
                                If btn_7by7_6.Text = "F6" Then
                                    btn_7by7_6.Visible = True
                                    btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 7 Then
                                If btn_7by7_7.Text = "F7" Then
                                    btn_7by7_7.Visible = True
                                    btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 8 Then
                                If btn_7by7_8.Text = "F8" Then
                                    btn_7by7_8.Visible = True
                                    btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 9 Then
                                If btn_7by7_9.Text = "F9" Then
                                    btn_7by7_9.Visible = True
                                    btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 10 Then
                                If btn_7by7_10.Text = "F10" Then
                                    btn_7by7_10.Visible = True
                                    btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If

                            ElseIf Code = 11 Then
                                If btn_7by7_11.Text = "F11" Then
                                    btn_7by7_11.Visible = True
                                    btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If

                            ElseIf Code = 12 Then
                                If btn_7by7_12.Text = "F12" Then
                                    btn_7by7_12.Visible = True
                                    btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 13 Then
                                If btn_7by7_13.Text = "F13" Then
                                    btn_7by7_13.Visible = True
                                    btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            ElseIf Code = 14 Then
                                If btn_7by7_14.Text = "F14" Then
                                    btn_7by7_14.Visible = True
                                    btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                                'ElseIf Code = 15 Then
                                '    If btn_7by7_15.Text = "F15" Then
                                '        btn_7by7_15.Visible = True
                                '        btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                '    End If
                            ElseIf Code = 16 Then
                                If btn_7by7_16.Text = "F16" Then
                                    btn_7by7_16.Visible = True
                                    btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 17 Then
                                If btn_7by7_17.Text = "F17" Then
                                    btn_7by7_17.Visible = True
                                    btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 18 Then
                                If btn_7by7_18.Text = "F18" Then
                                    btn_7by7_18.Visible = True
                                    btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 19 Then
                                If btn_7by7_19.Text = "F19" Then
                                    btn_7by7_19.Visible = True
                                    btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;margin-bottom: 10px;"
                                End If
                            ElseIf Code = 20 Then
                                If btn_7by7_20.Text = "F20" Then
                                    btn_7by7_20.Visible = True
                                    btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 21 Then
                                If btn_7by7_21.Text = "F21" Then
                                    btn_7by7_21.Visible = True
                                    btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 22 Then
                                If btn_7by7_22.Text = "F22" Then
                                    btn_7by7_22.Visible = True
                                    btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 23 Then
                                If btn_7by7_23.Text = "F23" Then
                                    btn_7by7_23.Visible = True
                                    btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 24 Then
                                If btn_7by7_24.Text = "F24" Then
                                    btn_7by7_24.Visible = True
                                    btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 25 Then
                                If btn_7by7_25.Text = "F25" Then
                                    btn_7by7_25.Visible = True
                                    btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 26 Then
                                If btn_7by7_26.Text = "F26" Then
                                    btn_7by7_26.Visible = True
                                    btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 27 Then
                                If btn_7by7_27.Text = "F27" Then
                                    btn_7by7_27.Visible = True
                                    btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                            ElseIf Code = 28 Then
                                If btn_7by7_28.Text = "F28" Then
                                    btn_7by7_28.Visible = True
                                    btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                End If
                                'ElseIf Code = 29 Then
                                '    If btn_7by7_29.Text = "F29" Then
                                '        btn_7by7_29.Visible = True
                                '        btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                '    End If
                            ElseIf Code = 30 Then
                                If btn_7by7_30.Text = "F30" Then
                                    btn_7by7_30.Visible = True
                                    btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                End If
                            End If
                        End If
                    End If
                Next

                ViewState("Data_dt") = dt1
            End If

            btn_7by7_1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_4.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_5.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_7.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_9.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_11.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_12.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_13.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_14.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_15.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_16.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_17.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_18.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_19.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_20.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_21.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_22.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_23.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_24.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_25.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_26.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_27.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_28.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_29.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            btn_7by7_30.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")


            txtFName.Text = ""
            ddParent.ClearSelection()
            ddCaption.ClearSelection()
            ddlPayType.ClearSelection()
            txtItem.Text = ""
            txtLauncher.Text = ""
            txtLauncher.Text = ""
            txtAccountID.Text = ""
            txt_PaymentHandlingFee.Text = ""
            txt_SalesHandlingFee.Text = ""
            txt_TaxAmount.Text = ""
            txt_TotalValue.Text = ""
            divTaxAmount.Visible = False
            divSalesHandlingFee.Visible = False
            divTotalValue.Visible = False
            divPaymentHandlingFee.Visible = False
            ddPayment.ClearSelection()
            ddlCardType.ClearSelection()
            ddlCardSubType.ClearSelection()
            chkActive.Checked = True
            radBackcolorpicker.SelectedColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
            radForcolorpicker.SelectedColor = ColorTranslator.FromHtml("#FFFFFF")
            chkbigbutton.Checked = False
            chkgroupbydept.Checked = False
            chkgroupbycourse.Checked = False
            ddldeaprtmentid.ClearSelection()
            ddlcourseid.ClearSelection()
            Session("Row_Id") = ""
            rdExpTime.SelectedValue = "DD"
            rdProfileType.SelectedIndex = 0
            txtDays.Text = ""

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadSwapMachine_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RadSwapMachine.SelectedIndexChanged
        Try
            If RadSwapMachine.SelectedIndex = 0 Then
                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('No swap '" + RadSwapMachine.SelectedItem.Text + "' function map');", True)
                'Return
            Else
                'Dim str As String = "Are you sure you want to swap till " + RadSwapMachine.SelectedItem.Text + " and save function map ?"
                Dim str As String = "Are you sure you want to save and swap till " + RadSwapMachine.SelectedItem.Text + " ?"
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "test", "DeleteItem('" + str + "');", True)
                Return

            End If
            'BindFunction_Swap()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub BindFunction_Swap()
        Try

            If hf_swapstatus.Text.ToString() = "1" Then
                oclsFunction.cmp_id = Val(Session("cmp_id"))
                oclsFunction.machine_id = Val(RadSwapMachine.SelectedValue)
                Dim dtMainFunction_id As DataTable = oclsFunction.SelectMainFunction_Swap()
                If dtMainFunction_id.Rows.Count > 0 Then
                    oclsFunction.cmp_id = Val(Session("cmp_id"))
                    oclsFunction.mainfunction_id = Val(dtMainFunction_id.Rows(0)("mainfunction_id").ToString)
                    Dim dtMainFunction As DataTable = oclsFunction.SelectMainFunction()
                    Dim Edit_dt As DataTable = oclsFunction.SelectFunction_Details()

                    If dtMainFunction.Rows.Count > 0 Then
                        txtMainFun.Text = dtMainFunction.Rows(0)("name").ToString
                        RadMachine.ClearSelection()
                        oclsBind.BindMachineFunction(RadMachine, Val(Session("cmp_id")), Val(dtMainFunction.Rows(0)("machine_id").ToString))
                        RadMachine.SelectedValue = dtMainFunction.Rows(0)("machine_id").ToString
                        'oclsBind.BindSwapMachineFunction(RadSwapMachine, Val(Session("cmp_id")), Val(dtMainFunction.Rows(0)("machine_id").ToString))
                        Radeditpanel.ClearSelection()
                        oclsBind.BindEditPanel(Radeditpanel, Val(Session("cmp_id")), Val(dtMainFunction_id.Rows(0)("mainfunction_id").ToString))
                        If dtMainFunction.Rows(0)("is_active").ToString = "Yes" Then
                            ChkmainActive.Checked = True
                        Else
                            ChkmainActive.Checked = False
                        End If

                    End If

                    dt.Columns.Add("id", GetType(Integer))
                    dt.Columns.Add("cmp_id", GetType(Integer))
                    dt.Columns.Add("name", GetType(String))
                    dt.Columns.Add("code", GetType(String))
                    dt.Columns.Add("caption_type", GetType(String))
                    dt.Columns.Add("is_active", GetType(Integer))
                    dt.Columns.Add("ip_address", GetType(String))
                    dt.Columns.Add("login_id", GetType(Integer))
                    dt.Columns.Add("item", GetType(Decimal))
                    dt.Columns.Add("back_color", GetType(String))
                    dt.Columns.Add("for_color", GetType(String))
                    dt.Columns.Add("machine_id", GetType(Integer))
                    dt.Columns.Add("big_button", GetType(Integer))
                    dt.Columns.Add("Payment_id", GetType(Integer))
                    dt.Columns.Add("pay_type", GetType(Integer))
                    dt.Columns.Add("pay_sub_type", GetType(String))
                    dt.Columns.Add("is_groupby_dept", GetType(Integer))
                    dt.Columns.Add("is_groupby_course", GetType(Integer))
                    dt.Columns.Add("dept_id", GetType(String))
                    dt.Columns.Add("course_id", GetType(String))
                    dt.Columns.Add("Panel_Id", GetType(Integer))
                    dt.Columns.Add("Parent_id", GetType(Integer))
                    dt.Columns.Add("Is_Parent", GetType(Integer))
                    dt.Columns.Add("Function_Id", GetType(Integer))

                    Dim val1 As String = "F"

                    For i = 1 To 30
                        Dim row As DataRow = dt.NewRow()
                        row("id") = i
                        row("cmp_id") = 0
                        row("name") = ""
                        row("code") = val1 + i.ToString()
                        row("caption_type") = ""
                        row("is_active") = 1
                        row("ip_address") = ""
                        row("login_id") = 0
                        row("item") = 0.00
                        row("back_color") = "#1B7BBD"
                        row("for_color") = "#FFFFFF"
                        row("machine_id") = 0
                        row("big_button") = 0
                        row("Payment_id") = 0
                        row("pay_type") = 0
                        row("pay_sub_type") = ""
                        row("is_groupby_dept") = 0
                        row("is_groupby_course") = 0
                        row("dept_id") = 0
                        row("course_id") = 0
                        row("Panel_Id") = 0
                        row("Parent_id") = 0
                        row("Is_Parent") = 0
                        row("Function_Id") = 0
                        dt.Rows.Add(row)
                    Next
                    ViewState("Data_dt") = dt

                    For Each row As DataRow In Edit_dt.Rows
                        For Each row1 As DataRow In dt.Rows
                            If row1("code").ToString() = row("code").ToString() Then
                                row1("cmp_id") = row("cmp_id")
                                row1("name") = row("name")
                                row1("code") = row("code")
                                row1("caption_type") = row("caption_type")
                                row1("is_active") = row("is_active")
                                row1("ip_address") = row("ip_address")
                                row1("login_id") = row("login_id")
                                row1("item") = row("item")
                                row1("back_color") = row("back_color")
                                row1("for_color") = row("for_color")
                                row1("machine_id") = row("machine_id")
                                row1("big_button") = row("big_button")
                                row1("Payment_id") = row("Payment_id")
                                row1("pay_type") = row("pay_type")
                                row1("pay_sub_type") = row("pay_sub_type")
                                row1("is_groupby_dept") = row("is_groupby_dept")
                                row1("is_groupby_course") = row("is_groupby_course")
                                row1("dept_id") = row("dept_id")
                                row1("course_id") = row("course_id")
                                row1("Panel_Id") = row("Panel_Id")
                                row1("Parent_id") = row("Parent_id")
                                row1("Is_Parent") = row("Parent_id")
                                row1("Function_Id") = row("Function_Id")
                            End If
                        Next
                    Next

                    ViewState("Data_dt") = dt

                    For Each row1 As DataRow In dt.Rows
                        Dim Code As Integer = Val(row1("code").ToString().Replace("F", "")) + 1

                        If row1("big_button") = "1" Then
                            If Code = 2 Then
                                btn_7by7_2.Visible = False
                                btn_7by7_1.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 3 Then
                                btn_7by7_3.Visible = False
                                btn_7by7_2.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 4 Then
                                btn_7by7_4.Visible = False
                                btn_7by7_3.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 5 Then
                                btn_7by7_5.Visible = False
                                btn_7by7_4.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 6 Then
                                btn_7by7_6.Visible = False
                                btn_7by7_5.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 7 Then
                                btn_7by7_7.Visible = False
                                btn_7by7_6.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 8 Then
                                btn_7by7_8.Visible = False
                                btn_7by7_7.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 9 Then
                                btn_7by7_9.Visible = False
                                btn_7by7_8.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 10 Then
                                btn_7by7_10.Visible = False
                                btn_7by7_9.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"


                            ElseIf Code = 11 Then
                                btn_7by7_11.Visible = False
                                btn_7by7_10.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"

                            ElseIf Code = 12 Then
                                btn_7by7_12.Visible = False
                                btn_7by7_11.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 13 Then
                                btn_7by7_13.Visible = False
                                btn_7by7_12.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 14 Then
                                btn_7by7_14.Visible = False
                                btn_7by7_13.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                                'ElseIf Code = 15 Then
                                '    btn_7by7_15.Visible = False
                                '    btn_7by7_14.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 16 Then
                                btn_7by7_16.Visible = False
                                btn_7by7_15.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 17 Then
                                btn_7by7_17.Visible = False
                                btn_7by7_16.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 18 Then
                                btn_7by7_18.Visible = False
                                btn_7by7_17.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 19 Then
                                btn_7by7_19.Visible = False
                                btn_7by7_18.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 20 Then
                                btn_7by7_20.Visible = False
                                btn_7by7_19.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 21 Then
                                btn_7by7_21.Visible = False
                                btn_7by7_20.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 22 Then
                                btn_7by7_22.Visible = False
                                btn_7by7_21.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 23 Then
                                btn_7by7_23.Visible = False
                                btn_7by7_22.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 24 Then
                                btn_7by7_24.Visible = False
                                btn_7by7_23.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 25 Then
                                btn_7by7_25.Visible = False
                                btn_7by7_24.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 26 Then
                                btn_7by7_26.Visible = False
                                btn_7by7_25.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 27 Then
                                btn_7by7_27.Visible = False
                                btn_7by7_26.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                            ElseIf Code = 28 Then
                                btn_7by7_28.Visible = False
                                btn_7by7_27.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%; margin-bottom: 10px;"
                                'ElseIf Code = 29 Then
                                '    btn_7by7_29.Visible = False
                                '    btn_7by7_28.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            ElseIf Code = 30 Then
                                btn_7by7_30.Visible = False
                                btn_7by7_29.Attributes("style") = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;"
                            End If
                        End If


                        If row1("name") = "" Then
                        Else
                            If row1("code") = "F1" Then
                                SetName(btn_7by7_1, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F2" Then
                                SetName(btn_7by7_2, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F3" Then
                                SetName(btn_7by7_3, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F4" Then
                                SetName(btn_7by7_4, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F5" Then
                                SetName(btn_7by7_5, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F6" Then
                                SetName(btn_7by7_6, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F7" Then
                                SetName(btn_7by7_7, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F8" Then
                                SetName(btn_7by7_8, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F9" Then
                                SetName(btn_7by7_9, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F10" Then
                                SetName(btn_7by7_10, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F11" Then
                                SetName(btn_7by7_11, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F12" Then
                                SetName(btn_7by7_12, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F13" Then
                                SetName(btn_7by7_13, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F14" Then
                                SetName(btn_7by7_14, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F15" Then
                                SetName(btn_7by7_15, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F16" Then
                                SetName(btn_7by7_16, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F17" Then
                                SetName(btn_7by7_17, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F18" Then
                                SetName(btn_7by7_18, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F19" Then
                                SetName(btn_7by7_19, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F20" Then
                                SetName(btn_7by7_20, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F21" Then
                                SetName(btn_7by7_21, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F22" Then
                                SetName(btn_7by7_22, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F23" Then
                                SetName(btn_7by7_23, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F24" Then
                                SetName(btn_7by7_24, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F25" Then
                                SetName(btn_7by7_25, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F26" Then
                                SetName(btn_7by7_26, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F27" Then
                                SetName(btn_7by7_27, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F28" Then
                                SetName(btn_7by7_28, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F29" Then
                                SetName(btn_7by7_29, row1("name"), row1("back_color"), row1("for_color"))
                            ElseIf row1("code") = "F30" Then
                                SetName(btn_7by7_30, row1("name"), row1("back_color"), row1("for_color"))
                            End If
                        End If

                    Next

                    If chkbigbutton.Checked = True Then

                    End If
                End If

            ElseIf hf_swapstatus.Text.ToString() = "0" Then

            Else

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Function_Master:BindFunction" + ex.Message)
        End Try
    End Sub

    Private Sub ddlCardSubType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlCardSubType.SelectedIndexChanged
        Try
            'If ddlCardSubType.SelectedValue = "Room Payment" Then
            divRoomDetails.Visible = True
            'Else
            '    divRoomDetails.Visible = False
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try

            oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub radLocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try

            oclsBind.BindMachineByLocation(radTill, Val(Session("cmp_id")), Val(radLocation.SelectedValue))

        Catch ex As Exception

        End Try
    End Sub
End Class
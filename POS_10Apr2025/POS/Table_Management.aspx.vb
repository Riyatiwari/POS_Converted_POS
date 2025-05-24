Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing
Imports System.Data.SqlClient

Partial Class Table_Management
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oClsDataccess As New ClsDataccess
    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
    Dim oclsTable As Controller_clsTable = New Controller_clsTable()

    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)

    Public Property Script_text() As String
        Get
            If (Not ViewState("Script_text_final") Is Nothing) Then
                Return ViewState("Script_text_final")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("Script_text_final") = value
        End Set
    End Property

    Public Property Script_text_O() As String
        Get
            If (Not ViewState("Script_text_final_O") Is Nothing) Then
                Return ViewState("Script_text_final_O")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("Script_text_final_O") = value
        End Set
    End Property

    Public Property Script_text_Click() As String
        Get
            If (Not ViewState("Script_text_Click") Is Nothing) Then
                Return ViewState("Script_text_Click")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("Script_text_Click") = value
        End Set
    End Property

    Public Property Script_offset() As String
        Get
            If (Not ViewState("Script_offset") Is Nothing) Then
                Return ViewState("Script_offset")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("Script_offset") = value
        End Set
    End Property

    Public Property Script_offset_O() As String
        Get
            If (Not ViewState("Script_offset_O") Is Nothing) Then
                Return ViewState("Script_offset_O")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("Script_offset_O") = value
        End Set
    End Property

    'Public ReadOnly Property CurrencySymbol() As String
    '    Get
    '        Dim ds As DataSet = Common.GetXmlData()
    '        If Not ds.Tables("Currency") Is Nothing Then
    '            Return DirectCast(ds.Tables("Currency").Rows(0)("Symbol").ToString(), String)
    '        Else
    '            Return String.Empty
    '        End If
    '    End Get
    'End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        RadScriptManager1.RegisterPostBackControl(lnkExport)

        If Not IsPostBack Then
            Try

                If Not Request.QueryString("store") = Nothing Then
                    CheckStore()
                End If
                oclsBind.BindLocationProduct(ddLocation, Val(Session("cmp_id")))
                binddiv()
                Set_BackgroundColor()
                ' BindShape()
                bindcolor()

            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub bindcolor()
        Try
            oClsDataccess = New ClsDataccess
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@BookingSettingsID", Val(Session("drpTableStoreId")), SqlDbType.Int)
            Dim dtLive As DataTable = oClsDataccess.GetdataTableSp("Get_Color_BY_BookingSettingsid", oColSqlparram)
            If dtLive.Rows.Count > 0 Then
                rptLegend.DataSource = dtLive
                rptLegend.DataBind()
            End If
        Catch ex As Exception
            Dim st As String = ex.Message.ToString()
        End Try
    End Sub

    Private Sub binddiv()
        Try
            'Dim oDbConnection As New DBConnection()
            'Dim objCommon As Common = New Common()
            'oColSqlPar.Add("@BookingScheduleID", Val(Session("rddlTypeId")))
            'oColSqlPar.Add("@Date", DateTime.Today, SqlDbType.DateTime)
            'oColSqlPar.Add("@Cur_Sym", CurrencySymbol.ToString())
            'Dim ds As DataSet = oClsDataccess.GetdatasetSp("Get_Table_Div", oColSqlPar, "Get_Table_Div")
            oclsTable.BookingScheduleID = 335
            oclsTable.Datebooking = System.DateTime.Now()
            oclsTable.curr = "$"
            oclsTable.Location_id = ddLocation.SelectedValue
            Dim ds As DataSet = oclsTable.BindDIv()
            If ds.Tables(0).Rows.Count > 0 Then
                rptDIV.DataSource = ds.Tables(0)
                rptDIV.DataBind()
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                rptOtherDIV.DataSource = ds.Tables(1)
                rptOtherDIV.DataBind()
            End If
        Catch ex As Exception
            LogHelper.Error("Table_managment:divBind:" + ex.Message)
        End Try
    End Sub

    Protected Sub Set_BackgroundColor()
        Try
            oclsTable.BookingScheduleID = 1

            Dim dsTables As DataSet = oclsTable.GetBackgroundColour()
            If dsTables.Tables(0).Rows.Count > 0 Then
                Dim color As String = dsTables.Tables(0).Rows(0)("Color").ToString()
                If color <> "" Then
                    RadColorPicker2.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dsTables.Tables(0).Rows(0)("Color").ToString())
                    containmentwrapper.Style.Add("background-color", System.Drawing.ColorTranslator.ToHtml(RadColorPicker2.SelectedColor).ToString())
                Else
                    RadColorPicker2.SelectedColor = System.Drawing.ColorTranslator.FromHtml("#efefef")
                    containmentwrapper.Style.Add("background-color", System.Drawing.ColorTranslator.ToHtml(RadColorPicker2.SelectedColor).ToString())
                End If
            Else
                RadColorPicker2.SelectedColor = System.Drawing.ColorTranslator.FromHtml("#efefef")
                containmentwrapper.Style.Add("background-color", System.Drawing.ColorTranslator.ToHtml(RadColorPicker2.SelectedColor).ToString())
            End If
        Catch ex As Exception
            LogHelper.Error("Table_managment:divBind:" + ex.Message)
        End Try
    End Sub

    'Private Sub BindShape()
    '    Try


    '        'Dim conn As DBConnection = New DBConnection()
    '        'Dim allStores As String
    '        'allStores = "select shape_name,shape_number,shape_sorting from M_Shape order by shape_sorting"
    '        'Dim dsTables As New DataSet
    '        'dsTables = conn.SelectData(allStores)
    '        Dim dsTables As DataSet = oclsTable.GetShapes()
    '        radShape.DataSource = dsTables
    '        radShape.DataTextField = "shape_name"
    '        radShape.DataValueField = "shape_number"
    '        radShape.DataBind()
    '        If Val(Session("radShapeVal1")) <> 0 Then
    '            radShape.SelectedValue = Session("radShapeVal1").ToString()
    '        Else
    '            radShape.SelectedIndex = 0
    '        End If

    '        oclsTable.shape_id = 1
    '        Dim dsTable As DataSet = oclsTable.GetShapeDetails()
    '        'allStores = "select Tran_id,div_sample,name,name  + '#' + Shape as divId  from M_Table_Set where shape_number = " + radShape.SelectedValue.ToString + " order by sorting"
    '        'Dim dsTable As New DataSet
    '        'dsTable = conn.SelectData(allStores)
    '        rpShapeVal.DataSource = dsTable
    '        rpShapeVal.DataBind()
    '    Catch ex As Exception
    '        LogHelper.Error("Table_managment:divBind:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub rptDIV_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDIV.ItemDataBound
        Try
            Dim item As RepeaterItem = TryCast(e.Item, RepeaterItem)
            If item IsNot Nothing Then
                Dim hdnTbl_id As HiddenField = TryCast(item.FindControl("hdnTbl_id"), HiddenField)
                Dim hdnAllowTable As HiddenField = TryCast(item.FindControl("hdnAllowTable"), HiddenField)
                Dim hdnpageX As HiddenField = TryCast(item.FindControl("hdnpageX"), HiddenField)
                Dim hdnpageY As HiddenField = TryCast(item.FindControl("hdnpageY"), HiddenField)
                If (hdnTbl_id IsNot Nothing) And (hdnAllowTable IsNot Nothing) Then
                    If hdnAllowTable.Value <> Nothing Then
                        Dim result As Array = hdnAllowTable.Value.Split("#")
                        Dim count1 As Integer = 0
                        ViewState("Script_text") = "$(""#" + hdnTbl_id.Value + """).draggable({ snap: false } ##);"
                        For Each fern As String In result
                            If count1 = 0 Then
                                ViewState("Script_text") = ViewState("Script_text").ToString.Replace("{ snap: false }", "").Replace("##", "{ snap: '#" + fern.ToString + "##")
                                count1 = 1
                            Else
                                ViewState("Script_text") = ViewState("Script_text").ToString.Replace("##", ",#" + fern.ToString + " ##")
                            End If
                        Next
                        ViewState("Script_text_Click") += " $('#" + hdnTbl_id.Value + "').click(function (e) { alert(e.pageX + ' , ' + e.pageY);});"
                        ViewState("Script_text") = ViewState("Script_text").ToString.Replace("##", "' ,containment: ""#containmentwrapper"" }").Replace(" ", "")
                        ViewState("Script_text_final") += " " + ViewState("Script_text")
                        If hdnpageX.Value <> Nothing Then
                            ViewState("Script_offset") += " $('#" + hdnTbl_id.Value + "').offset({ top: " + hdnpageY.Value + ", left: " + hdnpageX.Value + " });"
                        End If
                    Else
                        ViewState("Script_text_Click") += " $('#" + hdnTbl_id.Value + "').click(function (e) { alert(e.pageX + ' , ' + e.pageY);});"
                        ViewState("Script_text") = "$(""#" + hdnTbl_id.Value + """).draggable({ grid: [ 5, 5 ] ,containment: ""#containmentwrapper"" });"
                        ViewState("Script_text_final") += " " + ViewState("Script_text")
                        If hdnpageX.Value <> Nothing Then
                            ViewState("Script_offset") += " $('#" + hdnTbl_id.Value + "').offset({ top: " + hdnpageY.Value + ", left: " + hdnpageX.Value + " });"
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rptOtherDIV_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptOtherDIV.ItemDataBound
        Try
            Dim item As RepeaterItem = TryCast(e.Item, RepeaterItem)
            If item IsNot Nothing Then
                Dim OTran_id As HiddenField = TryCast(item.FindControl("OTran_id"), HiddenField)
                Dim OpageX As HiddenField = TryCast(item.FindControl("OpageX"), HiddenField)
                Dim OpageY As HiddenField = TryCast(item.FindControl("OpageY"), HiddenField)
                Dim Tran_id As HiddenField = TryCast(item.FindControl("Tran_id"), HiddenField)
                Dim RadBinaryImage2 As RadBinaryImage = TryCast(item.FindControl("RadBinaryImage2"), RadBinaryImage)
                If (OTran_id IsNot Nothing) Then
                    ViewState("Script_text_O") = "$(""#" + OTran_id.Value + """).draggable({ grid: [ 5, 5 ] ,containment: ""#containmentwrapper"" });"
                    ViewState("Script_text_final_O") += " " + ViewState("Script_text_O")
                    If OpageX.Value <> Nothing Then
                        ViewState("Script_offset_O") += " $('#" + OTran_id.Value + "').offset({ top: " + OpageY.Value + ", left: " + OpageX.Value + " });"
                    End If
                End If
                If Tran_id IsNot Nothing Then
                    Dim conn As DBConnection = New DBConnection()
                    Dim allStores As String
                    allStores = "select Shape_Img from M_Table_Details where Tran_id = " + Tran_id.Value + ""
                    Dim dsTables As New DataSet
                    dsTables = conn.SelectData(allStores)

                    RadBinaryImage2.DataValue = dsTables.Tables(0).Rows(0)("Shape_Img")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub radShape_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radShape.SelectedIndexChanged
    '    Dim conn As DBConnection = New DBConnection()
    '    Dim allStores As String
    '    allStores = "select Tran_id,div_sample,name,name  + '#' + Shape as divId from M_Table_Set where shape_number = " + radShape.SelectedValue.ToString + " order by sorting"
    '    Dim dsTable As New DataSet
    '    dsTable = conn.SelectData(allStores)
    '    rpShapeVal.DataSource = dsTable
    '    rpShapeVal.DataBind()
    '    Session("radShapeVal1") = radShape.SelectedValue
    'End Sub

    Protected Sub RadContextMenu1_ItemClick(sender As Object, e As Telerik.Web.UI.RadMenuEventArgs) Handles RadContextMenu1.ItemClick
        'Try
        '    If e.Item.Text = "Add" Then
        '        Session("titleWindow") = "Add " + hdnNameShape.Value.ToString.Split("#")(0)
        '        Session("Structure") = hdnNameShape.Value.ToString.Split("#")(1)
        '        ddlSize.SelectedIndex = -1
        '        RadColorPicker1.SelectedColor = System.Drawing.ColorTranslator.FromHtml("#999999")
        '        Session("colourChange") = System.Drawing.ColorTranslator.ToHtml(RadColorPicker1.SelectedColor)
        '        Session("popupRadWindow") = 1
        '        Session("EditShape") = Nothing
        '        If hdnNameShape.Value.ToString.Split("#")(0) = "Label" Then
        '            Session("Caption") = "Label"
        '        Else
        '            Session("Caption") = Nothing
        '        End If
        '        txtCaption.Text = ""
        '        Session("DivSize") = "50"
        '        lblErrCaption.Visible = False
        '        lblErrSize.Visible = False
        '    End If
        'Catch

        'End Try
    End Sub

    Protected Sub RadContextMenu2_ItemClick(sender As Object, e As Telerik.Web.UI.RadMenuEventArgs) Handles RadContextMenu2.ItemClick
        Try
            If e.Item.Text = "Edit" Then
                Dim conn As DBConnection = New DBConnection()
                Dim allStores As String
                '  allStores = "SELECT td.Caption,td.color,ts.Shape,ts.name,td.Size FROM M_Table_Details td inner join M_Table_Set ts on ts.Shape = td.Shape where td.Tran_id = " + hdnShapeId.Value.ToString().Split("#")(0).Replace("Slb", "") + ""
                Dim dsTables As New DataSet
                dsTables = conn.SelectData(allStores)
                Session("titleWindow") = "Edit " + dsTables.Tables(0).Rows(0)("name")
                Session("Structure") = dsTables.Tables(0).Rows(0)("Shape")
                RadColorPicker1.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dsTables.Tables(0).Rows(0)("color"))
                Session("colourChange") = dsTables.Tables(0).Rows(0)("color")
                Session("EditShape") = 1
                Session("popupRadWindow") = 1
                Session("Caption") = dsTables.Tables(0).Rows(0)("Caption")
                Session("DivSize") = dsTables.Tables(0).Rows(0)("Size")
                lblErrCaption.Visible = False
                lblErrSize.Visible = False
                If dsTables.Tables(0).Rows(0)("Shape") = "Lable" Then
                    txtCaption.Text = dsTables.Tables(0).Rows(0)("Caption").ToString
                End If
                'ElseIf e.Item.Text = "Delete" Then
                '    Dim conn As DBConnection = New DBConnection()
                '    Dim id As String = hdnShapeId.Value.ToString().Split("#")(0).Replace("Slb", "")
                '    conn.ExecuteNonQuery("DELETE FROM M_Table_Details WHERE Tran_id=" + id.ToString() + "")
                '    binddiv()
                'ElseIf e.Item.Text = "Send to back" Then
                '    Dim conn As DBConnection = New DBConnection()
                '    Dim id As String = hdnShapeId.Value.ToString().Split("#")(0).Replace("Slb", "")
                '    conn.ExecuteNonQuery("update M_Table_Details set zindex = zindex - 1 where Tran_id = " + id.ToString() + "")
                '    binddiv()
                'ElseIf e.Item.Text = "Bring to front" Then
                '    Dim conn As DBConnection = New DBConnection()
                '    Dim id As String = hdnShapeId.Value.ToString().Split("#")(0).Replace("Slb", "")
                '    conn.ExecuteNonQuery("update M_Table_Details set zindex = zindex + 1 where Tran_id = " + id.ToString() + "")
                '    binddiv()
            End If
        Catch

        End Try
    End Sub

    Protected Sub lnkSaveShape_Click(sender As Object, e As EventArgs)
        Try
            oClsDataccess = New ClsDataccess

            If ddlSize.SelectedIndex <> -1 Then

                Dim a As Integer = 0
                If Not String.IsNullOrEmpty(TryCast(Session("Caption"), String)) Then
                    If txtCaption.Text = "" Then
                        lblErrCaption.Visible = True
                        Dim script1 As String = "function f(){$find(""" + RadWindow_ContentTemplate.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script1, True)
                        a = 1
                    End If
                End If

                If a = 0 Then

                    Dim bytes As Byte() = Nothing
                    RadBinaryImage1.DataValue = Nothing
                    If hfImageData.UniqueID.ToString <> "" Then
                        Dim base64 As String = Request.Form(hfImageData.UniqueID).Split(",")(1)
                        bytes = Convert.FromBase64String(base64)
                        RadBinaryImage1.DataValue = bytes
                    End If

                    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                    oColSqlparram.Add("@BookingScheduleID", Val(Session("rddlTypeId")), SqlDbType.Int)
                    oColSqlparram.Add("@Table_id", 0, SqlDbType.Int)
                    oColSqlparram.Add("@Shape", Session("Structure").ToString())
                    oColSqlparram.Add("@Size", ddlSize.SelectedValue)
                    oColSqlparram.Add("@Direction", "")
                    If Val(Session("EditShape")) <> 0 Then
                        oColSqlparram.Add("@Tran_id", 0, SqlDbType.Int) 'hdnShapeId.Value.ToString().Split("#")(0).Replace("Slb", "")
                        oColSqlparram.Add("@Tran_type", "E")
                    Else
                        oColSqlparram.Add("@Tran_id", 0, SqlDbType.Int)
                        oColSqlparram.Add("@Tran_type", "I")
                    End If
                    oColSqlparram.Add("@pageX", "")
                    oColSqlparram.Add("@pageY", "")
                    oColSqlparram.Add("@Caption", txtCaption.Text.Trim)
                    If Not bytes Is Nothing Then
                        oColSqlparram.Add("@Shape_Img", RadBinaryImage1.DataValue, SqlDbType.Image)
                    End If
                    oColSqlparram.Add("@color", System.Drawing.ColorTranslator.ToHtml(RadColorPicker1.SelectedColor))
                    oClsDataccess.ExecStoredProcedure("P_M_Table_Details", oColSqlparram)
                    ddlSize.SelectedIndex = -1
                    txtCaption.Text = Nothing
                    binddiv()
                    Session("popupRadWindow") = Nothing
                    Session("colourChange") = Nothing
                    RadBinaryImage1.DataValue = Nothing
                    Session("EditShape") = Nothing
                    lblErrSize.Visible = False
                    lblErrCaption.Visible = False
                    Dim script As String = "function f(){$find(""" + RadWindow_ContentTemplate.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                End If

            Else
                lblErrSize.Visible = True
                Dim script As String = "function f(){$find(""" + RadWindow_ContentTemplate.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            End If

        Catch ex As Exception
            Dim str As String = ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlSize_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlSize.SelectedIndexChanged
        Try
            Session("DivSize") = ddlSize.SelectedValue
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Session("popupRadWindow") = Nothing
            Session("colourChange") = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkCancel2_Click(sender As Object, e As EventArgs)
        Try
            Session("openPopup2") = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Set_div(sender As Object, e As System.EventArgs)
        Try
            Dim eventArgument As String = (If(Me.Request("__EVENTARGUMENT") Is Nothing, String.Empty, Me.Request("__EVENTARGUMENT")))
            Dim oclsParm As New ColSqlparram
            oclsParm.Add("@ID", 0, SqlDbType.Int)
            oclsParm.Add("@Tran_type", "BookingRefSeat")
            oclsParm.Add("@BookingRef", eventArgument.ToString)
            oClsDataccess.ExecStoredProcedure("P_M_OpenTable", oclsParm)
            binddiv()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message + "');", True)
        End Try
    End Sub

    Protected Sub drag_div(sender As Object, e As System.EventArgs)
        Try
            Try
                Dim eventArgument As String = (If(Me.Request("__EVENTARGUMENT") Is Nothing, String.Empty, Me.Request("__EVENTARGUMENT")))
                Dim conn As DBConnection = New DBConnection()
                Dim ary As Array = eventArgument.ToString().Split("#")
                Dim id As String = ary(0).ToString()
                Dim YTop As String = ary(1).ToString()
                Dim XLeft As String = ary(2).ToString()
                If id.ToString().Contains("Slb") Then
                    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                    oColSqlparram.Add("@Tran_id", id.ToString().Replace("Slb", ""), SqlDbType.Int)
                    oColSqlparram.Add("@BookingScheduleID", Val(Session("rddlTypeId")), SqlDbType.Int)
                    oColSqlparram.Add("@Table_id", 0, SqlDbType.Int)
                    oColSqlparram.Add("@Shape", "")
                    oColSqlparram.Add("@Size", ddlSize.SelectedValue)
                    oColSqlparram.Add("@Direction", "")
                    oColSqlparram.Add("@Tran_type", "P")
                    oColSqlparram.Add("@pageX", YTop)
                    oColSqlparram.Add("@pageY", XLeft)
                    oColSqlparram.Add("@Caption", "")
                    oClsDataccess.ExecStoredProcedure("P_M_Table_Details", oColSqlparram)
                Else
                    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                    oColSqlparram.Add("@Tran_id", 0, SqlDbType.Int)
                    oColSqlparram.Add("@BookingScheduleID", Val(Session("rddlTypeId")), SqlDbType.Int)
                    oColSqlparram.Add("@Table_id", id, SqlDbType.Int)
                    oColSqlparram.Add("@Shape", "")
                    oColSqlparram.Add("@Size", ddlSize.SelectedValue)
                    oColSqlparram.Add("@Direction", "")
                    oColSqlparram.Add("@Tran_type", "U")
                    oColSqlparram.Add("@pageX", YTop)
                    oColSqlparram.Add("@pageY", XLeft)
                    oColSqlparram.Add("@Caption", "")
                    oClsDataccess.ExecStoredProcedure("P_M_Table_Details", oColSqlparram)
                End If
            Catch ex As Exception

            End Try
            binddiv()
            'Session("openPopup3") = 2
            'Session("drag2") = 1
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            If Val(Session("popupRadWindow")) <> 0 Then
                If Not Session("colourChange") Is Nothing Then
                    RadColorPicker1.SelectedColor = System.Drawing.ColorTranslator.FromHtml(Session("colourChange").ToString())
                    ddlSize.SelectedValue = Session("DivSize").ToString()

                    Dim hexcolor = Session("colourChange")
                    Dim color = System.Drawing.ColorTranslator.FromHtml(hexcolor)
                    Dim LightColor1 As Color = Lighten(color, 0.2)
                    Dim Color1 = System.Drawing.ColorTranslator.ToHtml(LightColor1)
                    Dim LightColor2 As Color = Lighten(color, 0.3)
                    Dim Color2 = System.Drawing.ColorTranslator.ToHtml(LightColor2)

                    Dim conn As DBConnection = New DBConnection()
                    Dim allStores As String
                    If Not String.IsNullOrEmpty(TryCast(Session("Caption"), String)) Then
                        allStores = "select REPLACE(REPLACE(REPLACE(div_html, '#0001', '" + Session("colourChange").ToString() + "'),'#0002', '" + Color2.ToString() + "'),'##lable##', '" + Session("Caption").ToString() + "') as div_display from M_Table_Set where Shape = '" + Session("Structure").ToString + "'"
                    Else
                        allStores = "select REPLACE(REPLACE(div_html, '#0001', '" + Session("colourChange").ToString() + "'),'#0002', '" + Color2.ToString() + "') as div_display from M_Table_Set where Shape = '" + Session("Structure").ToString + "'"
                    End If
                    'allStores = "select REPLACE(REPLACE(REPLACE(div_html, '#0001', '" + Session("colourChange").ToString() + "'),'#0002', '" + Color2.ToString() + "'),'##lable##', '" + txtCaption.Text + "') as div_display from M_Table_Set where Shape = '" + Session("Structure").ToString + "'"
                    Dim dsTable As New DataSet
                    dsTable = conn.SelectData(allStores)
                    rptDemo.DataSource = dsTable
                    rptDemo.DataBind()
                Else
                    Dim conn As DBConnection = New DBConnection()
                    Dim allStores As String
                    allStores = "select div_html,div_display from M_Table_Set where Shape = '" + Session("Structure").ToString + "'"
                    Dim dsTable As New DataSet
                    dsTable = conn.SelectData(allStores)
                    rptDemo.DataSource = dsTable
                    rptDemo.DataBind()
                End If
                If Session("Structure").ToString = "Lable" Then
                    txtCaption.Display = True
                Else
                    txtCaption.Display = False
                End If
                RadWindow_ContentTemplate.Title = Session("titleWindow").ToString()
                Dim script As String = "function f(){$find(""" + RadWindow_ContentTemplate.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            End If
            If Val(Session("openPopup2")) = 1 Then
                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                oColSqlPar.Add("@Bookingref", Session("BookingrefPopup").ToString())
                Dim ds As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail_BY_Bookingref", oColSqlPar, "BookingDetail")
                If ds.Tables(0).Rows.Count > 0 Then
                    lblRefNo.Text = Session("BookingrefPopup").ToString()
                    lblTableNo.Text = ds.Tables(0).Rows(0)("TableNo")
                    lblName.Text = ds.Tables(0).Rows(0)("Name")
                    lblBalance.Text = ds.Tables(0).Rows(0)("Balance").ToString() + " $"
                    lblStartTime.Text = ds.Tables(0).Rows(0)("StartTime").ToString()
                    lblGuest.Text = ds.Tables(0).Rows(0)("Guest").ToString()
                    lblCourse1Sold.Text = ds.Tables(0).Rows(0)("Course1Sold").ToString()
                    lblCourse2Sold.Text = ds.Tables(0).Rows(0)("Course2Sold").ToString()
                    lblCourse3Sold.Text = ds.Tables(0).Rows(0)("Course3Sold").ToString()
                    lblCourse4Sold.Text = ds.Tables(0).Rows(0)("Course4Sold").ToString()
                    lblCourse1Served.Text = ds.Tables(0).Rows(0)("Course1Served").ToString()
                    lblCourse2Served.Text = ds.Tables(0).Rows(0)("Course2Served").ToString()
                    lblCourse3Served.Text = ds.Tables(0).Rows(0)("Course3Served").ToString()
                    lblCourse4Served.Text = ds.Tables(0).Rows(0)("Course4Served").ToString()
                    lblCourse1Away.Text = ds.Tables(0).Rows(0)("Course1Away").ToString()
                    lblCourse2Away.Text = ds.Tables(0).Rows(0)("Course2Away").ToString()
                    lblCourse3Away.Text = ds.Tables(0).Rows(0)("Course3Away").ToString()
                    lblCourse4Away.Text = ds.Tables(0).Rows(0)("Course4Away").ToString()
                    lblETC.Text = ds.Tables(0).Rows(0)("ETC").ToString()
                End If
                Dim script As String = "function f(){$find(""" + rwInfo.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            Else
                Dim script As String = "function f(){$find(""" + rwInfo.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                Session("openPopup2") = 0
                Session("BookingrefPopup") = Nothing
                'binddiv()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RadColorPicker1_ColorChanged(sender As Object, e As System.EventArgs) Handles RadColorPicker1.ColorChanged
        Session("colourChange") = System.Drawing.ColorTranslator.ToHtml(RadColorPicker1.SelectedColor)
    End Sub

    Public Shared Function Lighten(inColor As Color, inAmount As Double) As Color
        Return Color.FromArgb(inColor.A, CInt(Math.Min(255, inColor.R + 255 * inAmount)), CInt(Math.Min(255, inColor.G + 255 * inAmount)), CInt(Math.Min(255, inColor.B + 255 * inAmount)))
    End Function

    Protected Sub lnkExport_Click(sender As Object, e As System.EventArgs)
        Dim base64 As String = Request.Form(hdnExport.UniqueID).Split(",")(1)
        Dim bytes As Byte() = Convert.FromBase64String(base64)
        Response.Clear()
        Response.ContentType = "image/png"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Session("rddlTypeText").ToString() + ".png")
        Response.Buffer = True
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.BinaryWrite(bytes)
        Response.End()
    End Sub

    Protected Sub lnkSaveBackground_Click(sender As Object, e As System.EventArgs)
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim color As String = System.Drawing.ColorTranslator.ToHtml(RadColorPicker2.SelectedColor).ToString()
            Dim str As String = "update BookingSettings set color = '" + color.ToString() + "' where BookingSettingsid = " + Val(Session("drpTableStoreId")).ToString() + " "
            conn.ExecuteNonQuery(str.ToString())
            containmentwrapper.Style.Add("background-color", System.Drawing.ColorTranslator.ToHtml(RadColorPicker2.SelectedColor).ToString())
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtCaption_TextChanged(sender As Object, e As System.EventArgs) Handles txtCaption.TextChanged
        Session("Caption") = txtCaption.Text
    End Sub

    Protected Sub table_Click(sender As Object, e As System.EventArgs)
        Try
            If Val(Session("openPopup2")) <> 1 Then
                Dim eventArgument As String = (If(Me.Request("__EVENTARGUMENT") Is Nothing, String.Empty, Me.Request("__EVENTARGUMENT")))
                If eventArgument.ToString() <> "" And eventArgument.ToString() <> "undefined" Then
                    Session("BookingrefPopup") = eventArgument.ToString()
                    Session("openPopup2") = 1
                Else
                    Session("openPopup2") = 0
                End If
            Else
                Session("openPopup2") = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub imgrefreshTableView_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgrefreshTableView.Click
    '    Try
    '        binddiv()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub CheckStore()
        Try

            strcon.Open()
            Dim n As String = Request.QueryString("store").ToString()

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("db_server") = dt.Rows(0)("db_server")
                Session("user_name") = dt.Rows(0)("db_username")
                Session("password") = Decode_data(dt.Rows(0)("db_password"))
                Session("db_name") = dt.Rows(0)("db_name")
                Session("ConnectionString") = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Session("password") + ";"
            Else


            End If
            strcon.Close()
        Catch ex As Exception
            strcon.Close()
            LogHelper.Error("Till:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub

    Public Function Encode_data(ByVal str As String) As String
        Try
            Return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Protected Sub ddLocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            binddiv()
            Set_BackgroundColor()
            ' BindShape()
            bindcolor()

        Catch ex As Exception

        End Try
    End Sub
End Class

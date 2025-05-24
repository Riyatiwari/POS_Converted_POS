Imports System.Data
Imports Telerik.Web.UI

Partial Class BookingEasy_Settings2
    Inherits System.Web.UI.Page

#Region "Properties"
    Private Property BookingSettingsid() As Integer
        Get
            Try
                Return Utils.getInteger(ViewState("BookingSettingsid"))
            Catch
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            ViewState("BookingSettingsid") = value
        End Set
    End Property
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            Dim conn As DBConnection = New DBConnection()
            Dim str As String = conn.SingleCell("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bookings')SELECT 1 AS res ELSE SELECT 0 AS res;")
            If (str <> "0") Then
                BindgvTableAndDDl()
                BindgvGroupAndDDl()
                BindTableForGroup()

                ViewState("edit") = Nothing
                ViewState("save") = Nothing
                ViewState("edit_Redirect") = Nothing
            End If
        End If
    End Sub
#End Region

    Private Function CheckNameExists(ByVal scheduleId As Int32, ByVal scheduleName As String) As Boolean
        Dim strQuery As String = String.Empty
        strQuery &= " SELECT * FROM BookingSchedule"
        strQuery &= " WHERE Name = '" & scheduleName & "' AND BookingScheduleID != " & scheduleId & " AND BookingSettingsID = " & BookingSettingsid

        Dim objConn As DBConnection = New DBConnection()
        Return objConn.CheckData(strQuery)
    End Function

    Protected Sub btnSaveTable_Click(sender As Object, e As System.EventArgs) Handles btnSaveTable.Click
        Try
            Dim strQuery As String
            Dim conn As DBConnection = New DBConnection()
            Dim objCommon As Common = New Common()
            Dim que = ""
            LogHelper.Error("TableManagement:btnSaveTable_Click 1")
            If hdn_Table_id.Value <> 0 Then
                que = "Select 1 from M_Table Where TableNo = " & txtTableNo.Text & " and table_id <> " + hdn_Table_id.Value + " "
            Else
                que = "Select 1 from M_Table Where TableNo = " & txtTableNo.Text
            End If
            LogHelper.Error("TableManagement:btnSaveTable_Click 2" + que)
            Dim ds As DataSet = conn.SelectData(que)
            If (ds.Tables(0).Rows.Count > 0) Then
                'no
                divMsgErr.Visible = True
                divMsgErr.Attributes.Add("class", "alert alert-danger")
                lblMsg.Text = "Table No already exist"
            Else
                LogHelper.Error("TableManagement:btnSaveTable_Click 3")
                Dim tablejoin As String = objCommon.GetSelectedValue(ddlTableJoin)
                If tablejoin = "" Or tablejoin = Nothing Then
                    tablejoin = ""
                End If
                If hdn_Table_id.Value <> 0 Then
                    strQuery = "UPDATE M_Table SET Table_name = '" + txtTableName.Text.ToString() + "', MinCover =" + txtMinCovers.Text.ToString() + ", MaxCover =" + txtMaxCovers.Text.ToString() + ", AllowedJoin ='" + tablejoin.ToString() + "', TableNo = " + txtTableNo.Text + ", ModifyDate =GETDATE(), CreatedBy =" + Sessions.UserID.ToString() + ", IPAddress ='" + Request.UserHostAddress.ToString() + "' "
                    strQuery += " where Table_id = " + hdn_Table_id.Value + " "
                Else
                    strQuery = "INSERT INTO M_Table (Table_name, MinCover, MaxCover, AllowedJoin, TableNo, CreatedDate, ModifyDate, CreatedBy, IPAddress)"
                    strQuery += " VALUES ('" + txtTableName.Text.ToString() + "'," + txtMinCovers.Text.ToString() + "," + txtMaxCovers.Text.ToString() + ",'" + tablejoin.ToString() + "'," + txtTableNo.Text.ToString() + ",GETDATE(),GETDATE()," + Sessions.UserID.ToString() + ",'" + Request.UserHostAddress.ToString() + "')"
                End If
                LogHelper.Error("TableManagement:btnSaveTable_Click 4" + strQuery)
                conn.Ins_Upd_Del(strQuery)
                divMsgErr.Visible = True
                divMsgErr.Attributes.Add("class", "alert alert-success")
                If hdn_Table_id.Value <> 0 Then
                    lblMsg.Text = "Table Updated Successfully "
                Else
                    lblMsg.Text = "Table Save Successfully"
                End If
                ClearTableField()
                BindgvTableAndDDl()
                BindTableForGroup()
                hdn_Table_id.Value = 0
                btnSaveTable.Text = "Save"
            End If
        Catch ex As Exception
            LogHelper.Error("TableManagement:btnSaveTable_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelTable_Click(sender As Object, e As System.EventArgs) Handles btnCancelTable.Click
        ClearTableField()
        BindgvTableAndDDl()
        BindTableForGroup()
        hdn_Table_id.Value = 0
        btnSaveTable.Text = "Save"
        divMsgErr.Visible = False
    End Sub

    Public Sub BindgvTableAndDDl()
        Try
            gvTable.DataSource = Nothing
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetTableNames()
            gvTable.DataSource = ds
            gvTable.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearTableField()
        txtTableName.Text = ""
        txtMinCovers.Text = ""
        txtMaxCovers.Text = ""
        txtTableNo.Text = ""
        ddlTableJoin.ClearCheckedItems()
    End Sub

    Public Sub GetTableNamesWithoutId()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetTableNamesWithoutId(hdn_Table_id.Value)
        If ds.Tables.Count > 0 Then
            ddlTableJoin.DataSource = ds
            ddlTableJoin.DataTextField = "Table_name"
            ddlTableJoin.DataValueField = "Table_id"
            ddlTableJoin.DataBind()
        End If
    End Sub

    Protected Sub gvTable_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvTable.ItemDataBound
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)

            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If
        End If
    End Sub

    Protected Sub gvTable_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvTable.ItemCommand

        If (e.CommandName = "EditTableName") Then
            Try
                hdn_Table_id.Value = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("Table_id").ToString())
                Dim strQuery As String = " SELECT * FROM M_Table WHERE Table_id = " & hdn_Table_id.Value
                Dim conn As DBConnection = New DBConnection()
                Dim dr As DataRow = conn.SingleRow(strQuery)
                txtTableName.Text = Utils.NullToString(dr("Table_name"))
                txtMinCovers.Text = Utils.NullToString(dr("MinCover"))
                txtMaxCovers.Text = Utils.NullToString(dr("MaxCover"))
                txtTableNo.Text = Utils.NullToString(dr("TableNo"))
                GetTableNamesWithoutId()
                Dim collection As IList(Of RadComboBoxItem) = ddlTableJoin.Items
                Dim stArr As String() = Utils.NullToString(dr("AllowedJoin")).ToString().Split("#")
                For i As Integer = 0 To stArr.Length - 1
                    For Each item As RadComboBoxItem In collection
                        If stArr(i).ToString = item.Value Then
                            item.Checked = True
                            Exit For
                        End If

                    Next
                Next
                btnSaveTable.Text = "Update"
                divMsgErr.Visible = False
            Catch ex As Exception

            End Try

        ElseIf (e.CommandName = "DeleteTableName") Then
            Try
                Dim tabID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("Table_id").ToString())
                Dim strQuery As String = ""
                Dim conn As DBConnection = New DBConnection()
                Dim objCommon As Common = New Common()
                Dim que = "Select 1 from M_Table Where AllowedJoin LIKE '%" + tabID.ToString() + "%'"
                Dim ds As DataSet = conn.SelectData(que)
                If (ds.Tables(0).Rows.Count = 0) Then
                    Dim que1 = "Select 1 from M_Table_Group Where table_set LIKE '%" + tabID.ToString() + "%'"
                    Dim ds1 As DataSet = conn.SelectData(que1)
                    If (ds1.Tables(0).Rows.Count = 0) Then
                        Dim strQuery1 As String = " DELETE FROM M_Table WHERE Table_id = " & tabID
                        Dim conn1 As DBConnection = New DBConnection()
                        conn1.Ins_Upd_Del(strQuery1)
                        BindgvTableAndDDl()
                        BindTableForGroup()
                        divMsgErr.Visible = True
                        divMsgErr.Attributes.Add("class", "alert alert-success")
                        lblMsg.Text = "Table Deleted Successfully "
                    Else
                        divMsgErr.Visible = True
                        divMsgErr.Attributes.Add("class", "alert alert-danger")
                        lblMsg.Text = "Table assigned in group"
                    End If
                Else
                    divMsgErr.Visible = True
                    divMsgErr.Attributes.Add("class", "alert alert-danger")
                    lblMsg.Text = "Table Join with auther table"
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub

    Protected Sub btnSaveGroup_Click(sender As Object, e As System.EventArgs) Handles btnSaveGroup.Click
        Try
            Dim strQuery As String
            Dim conn As DBConnection = New DBConnection()
            Dim objCommon As Common = New Common()
            Dim que = ""
            If hdn_group_id.Value <> 0 Then
                que = "Select 1 from M_Table_Group Where UPPER(group_name) = UPPER('" & txtGroupName.Text & "') and GroupID <> " + hdn_group_id.Value + " "
            Else
                que = "Select 1 from M_Table_Group Where UPPER(group_name) = UPPER('" & txtGroupName.Text & "')"
            End If
            Dim ds As DataSet = conn.SelectData(que)
            If (ds.Tables(0).Rows.Count > 0) Then
                'no
                divGMsgerr.Visible = True
                divGMsgerr.Attributes.Add("class", "alert alert-danger")
                lblGmsg.Text = "Group Name already exist"
            Else

                Dim tableSetjoin As String = objCommon.GetSelectedValue(ddlTableSet)
                If tableSetjoin <> "" Or tableSetjoin <> Nothing Then
                    If hdn_group_id.Value <> 0 Then
                        strQuery = "UPDATE M_Table_Group SET group_name = '" + txtGroupName.Text.ToString() + "', table_set ='" + tableSetjoin.ToString() + "', ModifyDate = GETDATE(), CreatedBy =" + Sessions.UserID.ToString() + ", IPAddress ='" + Request.UserHostAddress.ToString() + "' "
                        strQuery += " where GroupID = " + hdn_group_id.Value + " "
                    Else
                        strQuery = "INSERT INTO M_Table_Group (group_name, table_set, CreatedDate, ModifyDate, CreatedBy, IPAddress)"
                        strQuery += " VALUES ('" + txtGroupName.Text.ToString() + "','" + tableSetjoin.ToString() + "',GETDATE(),GETDATE()," + Sessions.UserID.ToString() + ",'" + Request.UserHostAddress.ToString() + "')"
                    End If
                    conn.Ins_Upd_Del(strQuery)
                    divGMsgerr.Visible = True
                    divGMsgerr.Attributes.Add("class", "alert alert-success")
                    If hdn_group_id.Value <> 0 Then
                        lblGmsg.Text = "Group Updated Successfully "
                    Else
                        lblGmsg.Text = "Group Saved Successfully"
                    End If
                    ClearGroupField()
                    BindgvGroupAndDDl()
                    hdn_group_id.Value = 0
                    btnSaveGroup.Text = "Save"
                Else
                    divGMsgerr.Visible = True
                    divGMsgerr.Attributes.Add("class", "alert alert-danger")
                    lblGmsg.Text = "Please select Proper Table to join with group"
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnCancelGroup_Click(sender As Object, e As System.EventArgs) Handles btnCancelGroup.Click
        ClearGroupField()
        BindgvGroupAndDDl()
        hdn_group_id.Value = 0
        btnSaveGroup.Text = "Save"
        divGMsgerr.Visible = False
    End Sub

    Public Sub BindTableForGroup()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetTableNames()
            If ds.Tables.Count > 0 Then
                ddlTableJoin.DataSource = ds
                ddlTableJoin.DataTextField = "Table_name"
                ddlTableJoin.DataValueField = "Table_id"
                ddlTableJoin.DataBind()
                ddlTableSet.DataSource = ds
                ddlTableSet.DataTextField = "table_name"
                ddlTableSet.DataValueField = "table_id"
                ddlTableSet.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindgvGroupAndDDl()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetGroupNames()
            gvGroup.DataSource = ds
            gvGroup.DataBind()
            ViewState("gvSchedule") = ds
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearGroupField()
        txtGroupName.Text = ""
        ddlTableSet.ClearCheckedItems()
    End Sub

    Protected Sub gvGroup_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvGroup.ItemDataBound
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)

            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If
        End If
    End Sub

    Protected Sub gvGroup_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvGroup.ItemCommand

        If (e.CommandName = "EditGroup") Then
            hdn_group_id.Value = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("GroupID").ToString())
            Dim strQuery As String = " SELECT * FROM M_Table_Group WHERE GroupID = " & hdn_group_id.Value
            Dim conn As DBConnection = New DBConnection()
            Dim dr As DataRow = conn.SingleRow(strQuery)
            txtGroupName.Text = Utils.NullToString(dr("group_name"))
            Dim collection As IList(Of RadComboBoxItem) = ddlTableSet.Items
            Dim stArr As String() = Utils.NullToString(dr("table_set")).ToString().Split("#")
            For i As Integer = 0 To stArr.Length - 1
                For Each item As RadComboBoxItem In collection
                    If stArr(i).ToString = item.Value Then
                        item.Checked = True
                        Exit For
                    End If
                Next
            Next
            btnSaveGroup.Text = "Update"
            divGMsgerr.Visible = False
        ElseIf (e.CommandName = "DeleteGroup") Then
            Dim grpID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("GroupID").ToString())
            Dim conn As DBConnection = New DBConnection()
            Dim objCommon As Common = New Common()

            'Dim strQuery As String = " DELETE FROM M_Table_Group WHERE GroupID = " & grpID.ToString()
            'conn.Ins_Upd_Del(strQuery)
            'BindgvGroupAndDDl()
            'divGMsgerr.Visible = True
            'divGMsgerr.Attributes.Add("class", "alert alert-success")
            'lblGmsg.Text = "Group Deleted Successfully "
            Dim que = "select 1 from BookingSchedule where GroupID = " + grpID.ToString() + ""
            Dim ds As DataSet = conn.SelectData(que)
            If (ds.Tables(0).Rows.Count = 0) Then
                Dim strQuery1 As String = " DELETE FROM M_Table_Group WHERE GroupID = " & grpID.ToString()
                Dim conn1 As DBConnection = New DBConnection()
                conn1.Ins_Upd_Del(strQuery1)
                BindgvGroupAndDDl()
                divGMsgerr.Visible = True
                divGMsgerr.Attributes.Add("class", "alert alert-success")
                lblGmsg.Text = "Group Deleted Successfully "
            Else
                divGMsgerr.Visible = True
                divGMsgerr.Attributes.Add("class", "alert alert-danger")
                lblGmsg.Text = "Can not delete Group. Reference existing in schedule"
            End If

        End If
    End Sub

    Protected Sub gvTable_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvTable.NeedDataSource
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetTableNames()
        gvTable.DataSource = ds
    End Sub

    Protected Sub gvGroup_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvGroup.NeedDataSource
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetGroupNames()
        gvGroup.DataSource = ds
    End Sub
End Class

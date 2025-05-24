Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder

Partial Class BookingEasy_MultiVanue
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            'If (Not String.IsNullOrEmpty(BookingRef)) Then
            '    hdnMultiVenue.Value = "0"
            '    BindgvMultiVanue()
            '    BindVenue()
            'Else
            '    Response.Redirect("~\Login.aspx")
            'End If
            hdnMultiVenue.Value = "0"
            BindgvMultiVanue()
            BindVenue()
        End If
    End Sub

    Public ReadOnly Property BookingRef() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("BookingRef")) Then
                Return Request.QueryString("BookingRef").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Private Sub BindVenue()
        Dim conn As DBConnection = New DBConnection()
        Dim strQvuery As String = ""
        strQvuery &= "select * from VenueMaster"
        Dim dss As DataSet = conn.SelectData(strQvuery)
        ddlVenue.DataSource = dss.Tables(0)
        ddlVenue.DataValueField = "BookingVenueId"
        ddlVenue.DataTextField = "VenueName"
        ddlVenue.Items.Insert(0, New RadComboBoxItem("--SELECT--", "0"))
        ddlVenue.SelectedIndex = -1
        ddlVenue.DataBind()
    End Sub

    Private Sub BindgvMultiVanue()
        Dim conn As DBConnection = New DBConnection()
        Dim strQvuery As String = ""
        strQvuery &= "SELECT MultiVanueID ,VanueID, UserName, ServerName, DataBase_Name, CAST(Sync_Time AS time) as Sync_Time, "
        strQvuery &= " (CASE when Is_Active = 0 "
        strQvuery &= "THEN 'Inactive' "
        strQvuery &= "Else 'Active' "
        strQvuery &= "END) as Is_Active, v.VenueName "
        strQvuery &= "FROM MultiVanue left outer join VenueMaster v on v.BookingVenueId = VanueID"
        Dim dss As DataSet = conn.SelectData(strQvuery)
        gvMultiVanue.DataSource = dss
        gvMultiVanue.DataBind()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        divMessageBox.Visible = False
        divAddbtn.Visible = False
        AddDBDetails.Visible = True
        divGridField.Visible = False
        ClearField()
    End Sub

    Private Sub ClearField()
        ddlVenue.ClearSelection()
        txtDatabaseName.Text = String.Empty
        txtUserName.Text = String.Empty
        txtPassword.Text = String.Empty
        txtday.Text = String.Empty
        'TimeOfCourse.SelectedTime = Nothing
        chkEnable.Checked = False
        txtServerName.Text = String.Empty
    End Sub

    Protected Sub btnSaveSetting_Click(sender As Object, e As System.EventArgs) Handles btnSaveSetting.Click
        Try
            Dim strQuery As String
            Dim conn As DBConnection = New DBConnection()
            If hdnMultiVenue.Value = "0" Then
                Dim que = "Select * from MultiVanue Where VanueID = '" & ddlVenue.SelectedValue & "'"
                Dim ds As DataSet = conn.SelectData(que)
                If (ds.Tables(0).Rows.Count = 1) Then
                    'no
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "alert alert-danger")
                    lblMessageBox.Text = "Venue Name already exist"
                Else
                    'yes
                    Dim a As Integer
                    If chkEnable.Checked = True Then
                        a = 1
                    Else
                        a = 0
                    End If

                    strQuery = "INSERT INTO MultiVanue(VanueID, UserName, ServerName, Password, DataBase_Name, Sync_Time, Is_Active, Created_Date, Modify_Date,sync_day)"
                    strQuery += " VALUES (" + ddlVenue.SelectedValue + ",'" + txtUserName.Text + "','" + txtServerName.Text + "','" + txtPassword.Text + "','" + txtDatabaseName.Text + "',"
                    strQuery += "'21:00:00',"
                    strQuery += "" + a.ToString() + ",getdate(),getdate()," + txtday.Text + ")"
                    conn.Ins_Upd_Del(strQuery)
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "alert alert-success")
                    lblMessageBox.Text = "Record Inserted"
                    BindgvMultiVanue()
                End If
            Else
                Dim strUpdate As String
                strUpdate = "UPDATE MultiVanue SET VanueID = " + ddlVenue.SelectedValue + ", UserName = '" + txtUserName.Text + "', ServerName = '" + txtServerName.Text + "', Password = '" + txtPassword.Text + "', DataBase_Name = '" + txtDatabaseName.Text + "', Sync_Time = '21:00:00', Is_Active = " + IIf(chkEnable.Checked, 1, 0).ToString() + ", Modify_Date = GETDATE(), sync_day = " + txtday.Text + " WHERE MultiVanueID = " & hdnMultiVenue.Value
                conn.Ins_Upd_Del(strUpdate)
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-success")
                lblMessageBox.Text = "Record Updated"
                BindgvMultiVanue()
                hdnMultiVenue.Value = "0"
            End If
            divAddbtn.Visible = True
            AddDBDetails.Visible = False
            divGridField.Visible = True
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancelSetting_Click(sender As Object, e As System.EventArgs) Handles btnCancelSetting.Click
        divMessageBox.Visible = False
        divAddbtn.Visible = True
        AddDBDetails.Visible = False
        divGridField.Visible = True
    End Sub


    Protected Sub gvMultiVanue_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvMultiVanue.ItemCommand
        ClearField()
        If (e.CommandName = "EditTabName") Then
            hdnMultiVenue.Value = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("MultiVanueID").ToString())
            Dim strQuery As String = "Select * from MultiVanue Where MultiVanueID = " & hdnMultiVenue.Value
            Dim conn As DBConnection = New DBConnection()
            Dim dr As DataRow = conn.SingleRow(strQuery)
            ddlVenue.Items.FindItemByValue(dr("VanueID").ToString).Selected = True
            'ddlVenue.SelectedValue = Utils.NullToString(dr("VanueID"))
            txtDatabaseName.Text = Utils.NullToString(dr("DataBase_Name"))
            txtUserName.Text = Utils.NullToString(dr("UserName"))
            txtServerName.Text = Utils.NullToString(dr("ServerName"))
            txtPassword.Text = Utils.NullToString(dr("Password"))
            txtday.Text = Utils.NullToString(dr("sync_day"))
            Dim requestddate As DateTime = dr("Sync_Time").ToString()
            'Dim rtime As TimeSpan = requestddate.TimeOfDay
            'TimeOfCourse.SelectedTime = rtime
            chkEnable.Checked = IIf(dr("Is_Active") = "1", True, False)
            divMessageBox.Visible = False
            divAddbtn.Visible = False
            AddDBDetails.Visible = True
            divGridField.Visible = False
        ElseIf (e.CommandName = "DeleteTabName") Then
            Dim tabID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("MultiVanueID").ToString())
            Dim strQuery As String = " DELETE FROM MultiVanue WHERE MultiVanueID = " & tabID
            Dim conn As DBConnection = New DBConnection()
            conn.Ins_Upd_Del(strQuery)
            BindgvMultiVanue()
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-success")
            lblMessageBox.Text = "Record Deleted"
            BindgvMultiVanue()
        ElseIf (e.CommandName = "btnChange") Then
            Dim tabID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("MultiVanueID").ToString())
            Dim str As Integer
            If e.CommandArgument = "Active" Then
                str = 0
            Else
                str = 1
            End If
            'Dim strQvuery As String = ""
            'strQvuery &= "SELECT Is_Active FROM MultiVanue WHERE MultiVanueID = " & e.CommandArgument
            'Dim dr As DataRow = conn.SingleRow(strQvuery)
            'If dr("Is_Active") = "0" Then
            '    str = 1
            'Else
            '    str = 0
            'End If
            Dim strUpdate As String
            Dim conn As DBConnection = New DBConnection()
            strUpdate = "UPDATE MultiVanue SET Is_Active = " + str.ToString() + " WHERE MultiVanueID = " & tabID.ToString()
            conn.Ins_Upd_Del(strUpdate)
            BindgvMultiVanue()
        End If
    End Sub

    Protected Sub gvMultiVanue_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvMultiVanue.ItemDataBound

        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)

            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If

        End If
    End Sub

End Class

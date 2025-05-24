Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class TillShifts_Master
    Inherits BaseClass
    Dim objclsTillShiftMaster As New Controller_clsTillShifts()

    Dim oclsBind As New clsBinding
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Not Session("success") = Nothing Then
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                oclsBind.BindMachine(Radtill, Val(Session("cmp_id")))
                binddll()
                chk_Active.Checked = True
                If Not Session("tillshift_id") = Nothing Then
                    Bindtillshift()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("TillShifts_Master:Page_Load" + ex.Message)
        End Try
    End Sub
    Private Sub Bindtillshift()
        Try
            Dim sender As Object

            objclsTillShiftMaster.cmp_id = Val(Session("cmp_id"))
            objclsTillShiftMaster.tillshift_id = Val(Session("tillshift_id"))
            Dim dt As DataTable = objclsTillShiftMaster.Select()
            If dt.Rows.Count > 0 Then
                txtshiftname.Text = dt.Rows(0)("shift_name").ToString
                txtshiftno.Text = dt.Rows(0)("shift_no").ToString
                If dt.Rows.Count > 0 Then
                    Radtill.ClearSelection()
                    Radtill.SelectedValue = Val(dt.Rows(0)("machine_id"))

                    RadVenue.ClearSelection()
                    RadVenue.SelectedValue = Val(dt.Rows(0)("venue_id"))

                    If dt.Rows(0)("active").ToString = "1" Then
                        chk_Active.Checked = True
                    Else
                        chk_Active.Checked = False
                    End If

                End If



            End If




        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("TillShifts_Master:Bindtillshifts:" + ex.Message)
        End Try

    End Sub
   

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            objclsTillShiftMaster.cmp_id = Session("cmp_id")

            'If Radtill.SelectedValue = "0" Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please select Till');", True)
            '    Return
            'Else
            '    objclsTillShiftMaster.machine_id = Radtill.SelectedValue
            'End If

            If RadVenue.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please select Venue');", True)
                Return
            Else
                objclsTillShiftMaster.venue_id = RadVenue.SelectedValue
            End If

            Radtill.SelectedValue = 0
            objclsTillShiftMaster.machine_id = Radtill.SelectedValue
            objclsTillShiftMaster.cmp_id = Session("cmp_id")
            objclsTillShiftMaster.shift_no = txtshiftno.Text



            objclsTillShiftMaster.shift_name = txtshiftname.Text

            objclsTillShiftMaster.shift_no = txtshiftno.Text

            objclsTillShiftMaster.active = chk_Active.Checked


   

            If chk_Active.Checked = True Then
                objclsTillShiftMaster.active = 1
            Else
                objclsTillShiftMaster.active = 0
            End If



            If Session("tillshift_id") = Nothing Then
                Dim dtShiftVal As DataTable = objclsTillShiftMaster.GetShiftVal()
                If (dtShiftVal.Rows.Count > 0) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Shift number is already exist');", True)
                    Return
                End If
                objclsTillShiftMaster.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                objclsTillShiftMaster.tillshift_id = Val(Session("tillshift_id"))

                'Dim dtShiftupVal As DataTable = objclsTillShiftMaster.GetShiftUpdateVal()

                'If (dtShiftupVal.Rows.Count > 0) Then
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Shift number is already exist');", True)
                '    Return
                'End If

                objclsTillShiftMaster.Update()
                Session("Success") = "Record updated successfully"
            End If

            Session("tillshift_id") = Nothing
            Response.Redirect("TillShifts_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("TillShifts_List:Bindtillshifts:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Try
            If Session("tillshift_id") = Nothing Then
                Clear()
            Else
                Bindtillshift()
            End If
        Catch ex As Exception
            LogHelper.Error("TillShifts_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txtshiftname.Text = ""
            Radtill.SelectedIndex = 0
            chk_Active.Checked = False

        Catch ex As Exception
            LogHelper.Error("TillShifts_Master:Clear" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try

            Response.Redirect("TillShifts_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("TillShifts_Master:lnkNew_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub Radtill_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles Radtill.SelectedIndexChanged
        Try
            If Radtill.SelectedIndex > 0 Then
                objclsTillShiftMaster.machine_id = Radtill.SelectedValue
                objclsTillShiftMaster.cmp_id = Session("cmp_id")
                Dim dtShorting As DataTable = objclsTillShiftMaster.GetShiftShortingNo()

                If (dtShorting.Rows.Count > 0) Then
                    txtshiftno.Text = dtShorting.Rows(0)(0).ToString()
                End If
            Else

            End If
        Catch ex As Exception
            LogHelper.Error("TillShifts_Master:Radtill_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub RadVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RadVenue.SelectedIndexChanged
        Try
            If RadVenue.SelectedIndex > 0 Then
                objclsTillShiftMaster.venue_id = RadVenue.SelectedValue
                objclsTillShiftMaster.cmp_id = Session("cmp_id")
                Dim dtShorting As DataTable = objclsTillShiftMaster.GetShiftShortingNo()

                If (dtShorting.Rows.Count > 0) Then
                    txtshiftno.Text = dtShorting.Rows(0)(0).ToString()
                End If
            Else

            End If
        Catch ex As Exception
            LogHelper.Error("TillShifts_Master:Radtill_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Public Sub binddll()
        Try
            If RadVenue.SelectedIndex = -1 Then
                oclsBind.BindVenue(RadVenue, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("ZReport:binddll:" + ex.Message)
        End Try
    End Sub


End Class

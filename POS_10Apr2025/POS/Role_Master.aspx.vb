Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class Role_Master
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess
    'Dim oClsRole As New clsRole
    Dim oClsRole As New Controller_clsRole
  
    Protected Sub chkcheck_CheckedChanged(sender As Object, e As EventArgs)
        Try

            Dim target As CheckBox = DirectCast(sender, CheckBox)

            For Each radTreeNode As RadTreeNode In rtlForm.Nodes

                Dim node As RadTreeNode = DirectCast(target.Parent, RadTreeNode)
                Dim vi As CheckBox = DirectCast(node.FindControl("View"), CheckBox)
                Dim ad As CheckBox = DirectCast(node.FindControl("Add"), CheckBox)
                Dim ed1 As CheckBox = DirectCast(node.FindControl("Edit"), CheckBox)
                Dim de As CheckBox = DirectCast(node.FindControl("Delete"), CheckBox)

                For Each temp In rtlForm.Nodes

                    Dim v As HiddenField = CType(temp.FindControl("hv"), HiddenField)
                    Dim a As HiddenField = CType(temp.FindControl("ha"), HiddenField)
                    Dim ed As HiddenField = CType(temp.FindControl("he"), HiddenField)
                    Dim d As HiddenField = CType(temp.FindControl("hd"), HiddenField)
                    Dim f As Integer = 0

                    If target.Checked = True Then
                        v.Value = 1
                        a.Value = 1
                        ed.Value = 1
                        d.Value = 1
                    Else
                        v.Value = 0
                        a.Value = 0
                        ed.Value = 0
                        d.Value = 0
                    End If
                    If vi.Checked = True Then
                        v.Value = 1
                    Else
                        v.Value = 0
                    End If

                    If ad.Checked = True Then
                        a.Value = 1
                    Else
                        a.Value = 0
                    End If
                    If ed1.Checked = True Then
                        ed.Value = 1
                    Else
                        ed.Value = 0
                    End If
                    If de.Checked = True Then
                        d.Value = 1
                    Else
                        d.Value = 0
                    End If

                    If v.Value = 1 Or a.Value = 1 Or ed.Value = 1 Or d.Value = 1 Then
                        node.Checked = True
                    ElseIf v.Value = 0 And a.Value = 0 And ed.Value = 0 And d.Value = 0 Then
                        node.Checked = False
                    Else
                        node.Checked = False
                    End If

                Next temp
            Next
        Catch ex As Exception
            LogHelper.Error("Role_Master:chkcheck_CheckedChanged:" + ex.Message)
        End Try
    End Sub
 
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                txtRname.Focus()
                BindFormGrid()
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                If Not Session("RoleEdit") = Nothing Then
                    EditRole()
                End If
            End If

        Catch ex As Exception
            LogHelper.Error("Role_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Public Sub BindFormGrid()
        Try
            Dim dtFomrs As DataTable = oClsRole.SelectAllForm()
            ViewState("dt") = dtFomrs
            rtlForm.DataSource = dtFomrs
            rtlForm.DataBind()
            rtlForm.ExpandAllNodes()

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Role_Master:BindFormGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub EditRole()
        Try
            oClsRole.cmp_id = Val(Session("cmp_id"))
            oClsRole.Role_Id = Val(Session("RoleEdit"))
            Dim dtRole As DataTable = oClsRole.SelectRole()

            If dtRole.Rows.Count > 0 Then

                If dtRole.Rows(0)("type").ToString() = 0 Or dtRole.Rows(0)("type").ToString() = 1 Then

                    hiRole_ID.Value = Val(dtRole.Rows(0)("Role_id"))
                    txtRname.Text = dtRole.Rows(0)("Role_name").ToString
                    If dtRole.Rows(0)("Active").ToString() = "Yes" Then
                        chkActive.Checked = True
                    Else
                        chkActive.Checked = False
                    End If
                    BindFormGrid()
                    oClsRole.cmp_id = Val(Session("cmp_id"))
                    oClsRole.Role_Id = Val(Session("RoleEdit"))
                    Dim dtRoleDetail As DataTable = oClsRole.SelectDRole()

                    For Each roleRow As DataRow In dtRoleDetail.Rows
                        For Each item As RadTreeNode In rtlForm.GetAllNodes
                            If CType(item.FindControl("hfformID"), HiddenField).Value = roleRow("form_id") Then
                                'CType(item.FindControl("maiin"), CheckBox).Checked = True
                                If CType(item.FindControl("hfParentID"), HiddenField).Value <> "" Then
                                    item.Checked = True
                                    CType(item.FindControl("View"), CheckBox).Checked = IIf(roleRow("is_view") = "1", True, False)
                                    CType(item.FindControl("Add"), CheckBox).Checked = IIf(roleRow("is_Add") = "1", True, False)
                                    CType(item.FindControl("Edit"), CheckBox).Checked = IIf(roleRow("is_Edit") = "1", True, False)
                                    CType(item.FindControl("Delete"), CheckBox).Checked = IIf(roleRow("is_Delete") = "1", True, False)
                                End If
                                Exit For
                            End If

                        Next
                    Next


                End If
            End If

        Catch ex As Exception
            LogHelper.Error("Role_Master:EditRole:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSignIn_Click(sender As Object, e As System.EventArgs) Handles btnSignIn.Click
        Try

            Dim tran As String

            If hiRole_ID.Value = "0" Then
                tran = "Ins"
            Else
                tran = "Upd"
            End If

            Dim sFormId As String = ""
            For Each item As RadTreeNode In rtlForm.CheckedNodes

                sFormId += CType(item.FindControl("hfformid"), HiddenField).Value + "#"
                sFormId += IIf(CType(item.FindControl("View"), CheckBox).Checked, "1", "0") + "#"
                sFormId += IIf(CType(item.FindControl("Add"), CheckBox).Checked, "1", "0") + "#"
                sFormId += IIf(CType(item.FindControl("Edit"), CheckBox).Checked, "1", "0") + "#"
                sFormId += IIf(CType(item.FindControl("Delete"), CheckBox).Checked, "1", "0") + "$"

            Next

            oClsRole.Role_Id = Val(hiRole_ID.Value)
            oClsRole.cmp_id = Val(Session("cmp_id"))
            oClsRole.Role_Name = txtRname.Text.Trim
            oClsRole.Type = 0
            oClsRole.Role_Detail = sFormId
            'oClsRole.is_active = 1
            oClsRole.is_active = IIf(chkActive.Checked = True, 1, 0)
            oClsRole.Ip_address = Request.UserHostAddress
            oClsRole.login_id = Val(Session("login_id"))
            oClsRole.Tran_Type = tran
            If hiRole_ID.Value = "0" Then
                oClsRole.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oClsRole.Update()
                Session("Success") = "Record updated successfully"
            End If
            txtRname.Text = ""
            Response.Redirect("Role_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Role_Master:btnSignIn_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub rtlForm_NodeCheck(sender As Object, e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles rtlForm.NodeCheck
        Try
            If e.Node.Checked = True Then
                CType(e.Node.FindControl("view"), CheckBox).Checked = True
                CType(e.Node.FindControl("add"), CheckBox).Checked = True
                CType(e.Node.FindControl("edit"), CheckBox).Checked = True
                CType(e.Node.FindControl("delete"), CheckBox).Checked = True

                GetAllNodes(e.Node)
            Else
                CType(e.Node.FindControl("view"), CheckBox).Checked = False
                CType(e.Node.FindControl("add"), CheckBox).Checked = False
                CType(e.Node.FindControl("edit"), CheckBox).Checked = False
                CType(e.Node.FindControl("delete"), CheckBox).Checked = False
                GetAllNodes(e.Node)
            End If
        Catch ex As Exception
            LogHelper.Error("Role_Master:rtlForm_NodeCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub rtlForm_NodeClick(sender As Object, e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles rtlForm.NodeClick
        Try
            If e.Node.Checked = True Then
                CType(e.Node.FindControl("view"), CheckBox).Checked = True
                CType(e.Node.FindControl("add"), CheckBox).Checked = True
                CType(e.Node.FindControl("edit"), CheckBox).Checked = True
                CType(e.Node.FindControl("delete"), CheckBox).Checked = True
            Else
                CType(e.Node.FindControl("view"), CheckBox).Checked = False
                CType(e.Node.FindControl("add"), CheckBox).Checked = False
                CType(e.Node.FindControl("edit"), CheckBox).Checked = False
                CType(e.Node.FindControl("delete"), CheckBox).Checked = False
            End If


        Catch ex As Exception
            LogHelper.Error("Role_Master:rtlForm_NodeClick:" + ex.Message)
        End Try
    End Sub

    Private Sub GetAllNodes(ByVal param As RadTreeNode)
        Try
            If param.Nodes.Count = 0 Then
                Return
            End If
            Dim temp As RadTreeNode
            For Each temp In param.Nodes

                If temp.Checked = True Then
                    CType(temp.FindControl("View"), CheckBox).Checked = True
                    CType(temp.FindControl("Add"), CheckBox).Checked = True
                    CType(temp.FindControl("Edit"), CheckBox).Checked = True
                    CType(temp.FindControl("Delete"), CheckBox).Checked = True
                Else
                    CType(temp.FindControl("View"), CheckBox).Checked = False
                    CType(temp.FindControl("Add"), CheckBox).Checked = False
                    CType(temp.FindControl("Edit"), CheckBox).Checked = False
                    CType(temp.FindControl("Delete"), CheckBox).Checked = False

                End If
                GetAllNodes(temp)
            Next temp
        Catch ex As Exception
            LogHelper.Error("Role_Master:GetAllNodes:" + ex.Message)
        End Try

    End Sub 'GetAllNodes

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("RoleEdit") = Nothing Then
                txtRname.Text = ""
                BindFormGrid()
            Else
                EditRole()
            End If
        Catch ex As Exception
            LogHelper.Error("Role_Master:btnReset_Click:" + ex.Message)
        End Try

    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Session("RoleEdit") = Nothing
            Response.Redirect("Role_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Role_Master:btnCancel_Click:" + ex.Message)
        End Try

    End Sub

End Class
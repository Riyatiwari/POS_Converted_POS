Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder

Partial Class BookingEasy_PaymentGateway
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            hdnGatewayName.Value = "0"
            AreaHide()
            BindGatewayGrid()
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        divMessageBox.Visible = False
        GridField.Visible = False
        AreaShow()
        AreaClear()
        txtGatewayName.Focus()
    End Sub

    Private Sub AreaClear()
        txtGatewayName.Text = ""
        ddlGateway.SelectedValue = "Authorized"
        txtLoginId.Text = ""
        txtTransactionKey.Text = ""
        txtPassword.Text = ""
        txtURL.Text = ""
        txtReturnURL.Text = ""
        txtCancelURL.Text = ""
        ddlStatus.SelectedValue = "1"
        txtOrderNo.Text = ""
        txtTransactionMode.Text = ""
        txtCurrency.Text = ""
    End Sub

    Private Sub AreaShow()
        btn.Visible = False
        AddField.Visible = True
    End Sub

    Private Sub AreaHide()
        btn.Visible = True
        AddField.Visible = False
    End Sub

    Private Sub BindGatewayGrid()
        Dim conn As DBConnection = New DBConnection()
        Dim strQvuery As String = ""
        strQvuery &= "SELECT PaymentID,Name,Gateway,LoginID,TransactionKey,Password,URL,ReturnURL,CancelURL,OrderNo,TransactionMode,Currency,"
        strQvuery &= " (CASE when Status = 0 "
        strQvuery &= "THEN 'Disable' "
        strQvuery &= "Else 'Enable' "
        strQvuery &= "END) as Status "
        strQvuery &= "FROM M_Payment_Gateway"
        Dim dss As DataSet = conn.SelectData(strQvuery)
        gvGateway.DataSource = dss
        gvGateway.DataBind()
    End Sub


    Protected Sub btnSaveGatewaySetting_Click(sender As Object, e As System.EventArgs) Handles btnSaveGatewaySetting.Click

        Dim conn As DBConnection = New DBConnection()
        Dim strQuery As String

        If hdnGatewayName.Value = "0" Then
            Dim que = "Select Gateway from M_Payment_Gateway Where Gateway = '" & ddlGateway.SelectedText & "'"
            Dim ds As DataSet = conn.SelectData(que)
            If (ds.Tables(0).Rows.Count = 1) Then
                'no
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-danger")
                lblMessageBox.Text = "Gateway Name already exist"
                txtGatewayName.Text = ""
                txtGatewayName.Focus()
            Else
                'yes
                If txtOrderNo.Text = "" Then
                    txtOrderNo.Text = "0"
                End If
                strQuery = " INSERT INTO M_Payment_Gateway(Name,Gateway,LoginID,TransactionKey,Password,URL,ReturnURL,CancelURL,Status,OrderNo,TransactionMode,Currency,system_date) VALUES ('" & txtGatewayName.Text & "','" & ddlGateway.SelectedValue & "','" & txtLoginId.Text & "','" & txtTransactionKey.Text & "','" & txtPassword.Text & "','" & txtURL.Text & "','" & txtReturnURL.Text & "','" & txtCancelURL.Text & "','" & Convert.ToInt32(ddlStatus.SelectedValue) & "','" & txtOrderNo.Text & "','" & txtTransactionMode.Text & "','" & txtCurrency.Text & "',getdate())"
                conn.Ins_Upd_Del(strQuery)
                AreaHide()
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-success")
                lblMessageBox.Text = "Success"
                BindGatewayGrid()
            End If
        Else
            If txtOrderNo.Text = "" Then
                txtOrderNo.Text = "0"
            End If
            Dim strUpdate As String
            strUpdate = " UPDATE M_Payment_Gateway SET Name = '" & txtGatewayName.Text & "',Gateway = '" & ddlGateway.SelectedValue & "',system_date = getdate(),LoginID = '" & txtLoginId.Text & "',TransactionKey = '" & txtTransactionKey.Text & "',Password = '" & txtPassword.Text & "',URL = '" & txtURL.Text & "',ReturnURL = '" & txtReturnURL.Text & "',CancelURL = '" & txtCancelURL.Text & "',Status = '" & Convert.ToInt32(ddlStatus.SelectedValue) & "',OrderNo = '" & txtOrderNo.Text & "',TransactionMode = '" & txtTransactionMode.Text & "',Currency = '" & txtCurrency.Text & "' WHERE PaymentID = " & hdnGatewayName.Value
            conn.Ins_Upd_Del(strUpdate)
            AreaHide()
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-success")
            lblMessageBox.Text = "Database Updated"
            BindGatewayGrid()
        End If
        GridField.Visible = True
        hdnGatewayName.Value = "0"
    End Sub

    Protected Sub btnCancelGatewaySetting_Click(sender As Object, e As System.EventArgs) Handles btnCancelGatewaySetting.Click
        divMessageBox.Visible = False
        AreaHide()
        GridField.Visible = True
    End Sub

    Protected Sub gvGateway_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvGateway.ItemCommand
        If (e.CommandName = "EditTabName") Then
            divMessageBox.Visible = False
            AreaShow()
            GridField.Visible = False
            hdnGatewayName.Value = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("PaymentID").ToString())
            Dim strQuery As String = " SELECT * FROM M_Payment_Gateway WHERE PaymentID = " & hdnGatewayName.Value
            Dim conn As DBConnection = New DBConnection()
            Dim dr As DataRow = conn.SingleRow(strQuery)
            txtGatewayName.Text = Utils.NullToString(dr("Name"))
            ddlGateway.SelectedValue = Utils.NullToString(dr("Gateway"))
            txtLoginId.Text = Utils.NullToString(dr("LoginID"))
            txtTransactionKey.Text = Utils.NullToString(dr("TransactionKey"))
            txtPassword.Text = Utils.NullToString(dr("Password"))
            txtURL.Text = Utils.NullToString(dr("URL"))
            txtReturnURL.Text = Utils.NullToString(dr("ReturnURL"))
            txtCancelURL.Text = Utils.NullToString(dr("CancelURL"))
            If Utils.NullToString(dr("Status")) = "1" Then
                ddlStatus.SelectedValue = "1"
            Else
                ddlStatus.SelectedValue = "0"
            End If
            txtOrderNo.Text = Utils.NullToString(dr("OrderNo"))
            txtTransactionMode.Text = Utils.NullToString(dr("TransactionMode"))
            txtCurrency.Text = Utils.NullToString(dr("Currency"))

        ElseIf (e.CommandName = "DeleteTabName") Then
            Dim tabID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("PaymentID").ToString())
            Dim strQuery As String = " DELETE FROM M_Payment_Gateway WHERE PaymentID = " & tabID
            Dim conn As DBConnection = New DBConnection()
            conn.Ins_Upd_Del(strQuery)
            BindGatewayGrid()
        End If
    End Sub

    Protected Sub gvGateway_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvGateway.ItemDataBound

        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)

            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If
        End If
    End Sub
End Class

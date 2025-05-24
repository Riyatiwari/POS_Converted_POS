Imports System.Data
Imports Telerik.Web.UI

Partial Class BookingEasy_SMS_Settings
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            BindSettings()
        End If

    End Sub

    Protected Sub btnSaveSetting_Click(sender As Object, e As System.EventArgs) Handles btnSaveSetting.Click
        Try

            Dim oClsDataccess As New ClsDataccess
            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            oColSqlPar.Add("@URL", txt_SMS_URL.Text)
            oColSqlPar.Add("@Username", txt_Username.Text)
            oColSqlPar.Add("@Password", txt_Password.Text)
            oColSqlPar.Add("@Port", txt_Senderid.Text)
            oColSqlPar.Add("@IPAddress", Request.UserHostAddress)
            oColSqlPar.Add("@CreatedBy", Sessions.UserID)
            oColSqlPar.Add("@Status", 1, SqlDbType.Int)
            oColSqlPar.Add("@Tran_type", "I")
            oClsDataccess.ExecStoredProcedure("P_M_SMS_Settings", oColSqlPar)

            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-success")
            lbExistingCust.Text = "Record Saved Successfully.!"

            'lblSaveField.Visible = True
            BindSettings()
        Catch ex As Exception

        End Try
      
    End Sub
    Private Sub BindSettings()
        Try
            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            Dim oClsDataccess As New ClsDataccess
            oColSqlPar.Add("@status", 1, SqlDbType.Int)
            Dim ds As DataSet = oClsDataccess.GetdatasetSp("Get_SMSSettings", oColSqlPar, "Waiting Table List")
            gvSMSSetting.DataSource = ds.Tables(0)
            gvSMSSetting.DataBind()

            If ds.Tables(0).Rows.Count > 0 Then
                txt_SMS_URL.Text = ds.Tables(0).Rows(0)("MailServer").ToString()
                txt_Username.Text = ds.Tables(0).Rows(0)("MailServer_Username").ToString()
                txt_Password.Text = ds.Tables(0).Rows(0)("MailServer_Password").ToString()
                txt_Senderid.Text = ds.Tables(0).Rows(0)("MailServer_Port").ToString()
            End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnResetCancel_Click(sender As Object, e As System.EventArgs) Handles btnResetCancel.Click
        Try
            Response.Redirect("Dashboard.aspx?TabId=" + Utils.Encrypt(Sessions.TabID))
        Catch ex As Exception

        End Try
    End Sub
End Class

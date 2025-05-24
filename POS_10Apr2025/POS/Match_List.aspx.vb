Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports Telerik.Web.UI
 Imports Telerik.Reporting
Imports Telerik.Reporting.TextBox
Imports System.Web.UI.WebControls.TextBox
Partial Class Match_List
    Inherits BaseClass
    Dim oclsRegister As New Controller_clsRegister()
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole
    Dim oClsDataccess As New ClsDataccess



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                BindRepeater()

            End If

        Catch ex As Exception
            LogHelper.Error("Product_List:Page_Load:" + ex.Message)
        End Try
    End Sub
    Private Sub BindRepeater()

        Dim dt As DataTable = GetDataFromDataSource()
        rdProduct.DataSource = dt
        rdProduct.DataBind()
    End Sub
    Protected Sub rdProduct_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rdProduct.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim txtDepartment As Telerik.Web.UI.RadTextBox = DirectCast(e.Item.FindControl("txtDepartment"), Telerik.Web.UI.RadTextBox)
            ' Dim chkUpdate As CheckBox = TryCast(e.Item.FindControl("chkUpdate"), CheckBox)
            Dim hidCatID As HiddenField = DirectCast(e.Item.FindControl("hidCatID"), HiddenField)
            Dim tillUUIDLabel As Label = DirectCast(e.Item.FindControl("lblTillUUID"), Label)
            Dim tillUUID As String = tillUUIDLabel.Text
            'If chkUpdate IsNot Nothing AndAlso TypeOf chkUpdate Is CheckBox Then
            '    chkUpdate.Attributes.Add("class", "updateCheckbox")
            '    AddHandler chkUpdate.CheckedChanged, AddressOf ChkUpdate_CheckedChanged
            'End If

            Dim drv As DataRowView = DirectCast(e.Item.DataItem, DataRowView)
            txtDepartment.Text = If(drv("department_name") IsNot DBNull.Value, drv("department_name").ToString(), String.Empty)
            hidCatID.Value = drv("dep_ID").ToString()
        End If
    End Sub


    Private Function GetDataFromDataSource() As DataTable
        oclsRegister.Company_id = Val(Session("cmp_id"))
        Dim dt As DataTable = oclsRegister.selectLite()
        LogHelper.Error(String.Format("Number of rows retrieved: {0}", dt.Rows.Count))
        Return dt
    End Function



    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        For Each item As RepeaterItem In rdProduct.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                'Dim chkUpdate As CheckBox = DirectCast(item.FindControl("chkUpdate"), CheckBox)
                ' Dim hidCatID As HiddenField = DirectCast(item.FindControl("hidCatID"), HiddenField)


                ' Dim isChecked As Boolean = chkUpdate.Checked
                'Dim catID As String = hidCatID.Value


            End If
        Next

        BindRepeater()
    End Sub
End Class

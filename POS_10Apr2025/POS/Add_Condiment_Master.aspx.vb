Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.UI.WebControls
Partial Class Add_Condiment_Master

    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsCondiment As New Controller_clsCondiment()
    Dim clsproduct As New Controller_clsProduct()
    Dim clscategory As New Controller_clsCategory()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Try
                If Not IsPostBack Then
                    If Session("cmp_id") = Nothing Then
                        Response.Redirect("SignIn.aspx", False)
                    End If


                End If
            Catch ex As Exception
                LogHelper.Error("Condiment_Master:Page_Load" + ex.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub


    Private Sub BindCondiment()
        Try


            clsproduct.cmp_id = Val(Session("cmp_id"))
            Dim dtt As DataTable = clsproduct.[SelectProductCondiment]()
            ViewState("ProductCondiment") = dtt

            clscategory.cmp_id = Val(Session("cmp_id"))
            Dim dt1t As DataTable = clscategory.SelectCategoryByCmp()
            ViewState("Progroup") = dt1t



            'Dim sender As Object
            'Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs

            'oclsCondiment.cmp_id = Val(Session("cmp_id"))
            'oclsCondiment.Condiment_Id = 0

            ''Dim dt As DataTable = oclsCondiment.Select()
            ''dt.DefaultView.RowFilter = "Condiment_Id = 0"

            ''Dim dt As DataTable = oclsCondiment.SelectDefault()


            ''Dim dt1 As DataTable
            ''Dim dr As DataRow = dt.NewRow()
            ''dt1.Rows.Add(dr)


            ''ViewState("dt1") = dt1
            'If ViewState("getGridInfo") Is Nothing Then
            '    '    Dim dtview As DataTable = ViewState("getGridInfo")
            '    '    dt1.Merge(dtview)
            '    'Else

            'End If


            'rdcondiment.DataSource = dt
            'rdcondiment.DataBind()

            Dim dt As DataTable = ViewState("getGridInfo")

            rdcondiment.DataSource = dt
            rdcondiment.DataBind()

            fillgridviewdata(dt)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Condiment_Master:BindFunctionType" + ex.Message)
        End Try
    End Sub
    Protected Sub fillgridviewdata(dt1 As DataTable)

        For i As Integer = 0 To dt1.Rows.Count - 1
            If dt1.Rows.Count > 0 Then

                Dim txtcondimnt As RadTextBox = CType(rdcondiment.Items(i).FindControl("txtcondimnt"), RadTextBox)
                Dim ddlprogroup As RadComboBox = CType(rdcondiment.Items(i).FindControl("ddlprogroup"), RadComboBox)
                Dim ddlpro As RadComboBox = CType(rdcondiment.Items(i).FindControl("ddlpro"), RadComboBox)

                If Not dt1.Rows(i)("name").ToString() = "" Then
                    txtcondimnt.Text = dt1.Rows(i)("name")
                End If
                If Not dt1.Rows(i)("category_id").ToString() = "" Then
                    ddlprogroup.SelectedValue = dt1.Rows(i)("category_id").ToString()


                End If
                If Not dt1.Rows(i)("product_id").ToString() = "" Then

                    ddlpro.SelectedValue = dt1.Rows(i)("product_id").ToString()
                End If


            End If
        Next





    End Sub



    Protected Sub rdcondiment_ItemDataBound2(sender As Object, e As GridItemEventArgs)
        Try


            Dim item As GridDataItem = CType(e.Item, GridDataItem)

            Dim ddlpro As RadComboBox = CType(e.Item.FindControl("ddlpro"), RadComboBox)
            ddlpro.DataSource = ViewState("ProductCondiment")
            ddlpro.DataTextField = "name"
            ddlpro.DataValueField = "Product_Id"
            ddlpro.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddlpro.Items.Insert(0, li)

            Dim ddlprogroup As RadComboBox = CType(e.Item.FindControl("ddlprogroup"), RadComboBox)
            ddlprogroup.DataSource = ViewState("Progroup")
            ddlprogroup.DataTextField = "name"
            ddlprogroup.DataValueField = "category_id"
            ddlprogroup.DataBind()

            Dim li2 As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddlprogroup.Items.Insert(0, li2)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try


            For Each item As GridDataItem In rdcondiment.Items
                Try


                    oclsCondiment.cmp_id = Session("cmp_id")
                    oclsCondiment.Condiment = CType(item.FindControl("txtcondimnt"), RadTextBox).Text
                    oclsCondiment.product_id = Val(CType(item.FindControl("ddlpro"), RadComboBox).SelectedValue)
                    '  oclsCondiment.product_group = Val(CType(item.FindControl("ddlpro"), RadComboBox).SelectedValue)

                    oclsCondiment.is_active = 1
                    oclsCondiment.ip_address = ""
                    oclsCondiment.is_add_substract = 0
                    oclsCondiment.Quantity = 1
                    oclsCondiment.Unit = 0
                    oclsCondiment.choices = 0

                    ' If Session("Condiment_Id") = Nothing Then
                    oclsCondiment.Condiment_Id = 0
                    oclsCondiment.Insert()
                    Session("Success") = "Record inserted successfully"
                    'Else
                    'oclsCondiment.Condiment_Id = Val(Session("Condiment_Id"))
                    '    oclsCondiment.Update()
                    '    Session("Success") = "Record updated successfully"
                    'End If
                Catch ex As Exception
                    LogHelper.Error("Profile_Master:btnSave_Click" + ex.Message)
                End Try
            Next
            Response.Redirect("Condiment_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Profile_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub


    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Try

            getGridInfo()
            BindCondiment()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub getGridInfo()
        'Dim dt As DataTable = New DataTable()
        Dim dr As DataRow

        'dt.Columns.Add(New System.Data.DataColumn("Condiment", GetType(String)))
        'dt.Columns.Add(New System.Data.DataColumn("product", GetType(String)))
        'dt.Columns.Add(New System.Data.DataColumn("product group", GetType(String)))
        Dim dt As DataTable = oclsCondiment.SelectDefault()





        For Each row As GridDataItem In rdcondiment.Items
            Dim condiment As RadTextBox = CType(row.FindControl("txtcondimnt"), RadTextBox)
            Dim productgroup As RadComboBox = CType(row.FindControl("ddlprogroup"), RadComboBox)
            Dim product As RadComboBox = CType(row.FindControl("ddlpro"), RadComboBox)

            dr = dt.NewRow()
            dr(0) = condiment.Text
            dr(1) = productgroup.SelectedValue
            dr(2) = product.SelectedValue
            dt.Rows.Add(dr)
        Next

        'dr = dt.NewRow()
        'dr(0) = ""
        'dr(1) = 0
        'dr(2) = 0
        'dt.Rows.Add(dr)

        ViewState("getGridInfo") = dt

    End Sub

    Protected Sub ddlprogroup_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)


        Dim cmbLocId As RadComboBox = CType(sender, RadComboBox)
        Dim item As GridDataItem = CType(cmbLocId.NamingContainer, GridDataItem)
        Dim strLocID As String = (TryCast(sender, RadComboBox)).SelectedValue


        Dim dt As DataTable = ViewState("ProductCondiment")
        dt.DefaultView.RowFilter = "category_id = " + strLocID + ""

        Dim ddlpro As RadComboBox = CType(item.FindControl("ddlpro"), RadComboBox)

        ddlpro.DataSource = dt
        ddlpro.DataTextField = "name"
        ddlpro.DataValueField = "Product_Id"
        ddlpro.DataBind()


    End Sub

End Class


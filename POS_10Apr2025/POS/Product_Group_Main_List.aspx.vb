Imports System.Data
Imports Telerik.Web.UI
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ComponentModel

'Imports Telerik.Reporting

Partial Class Product_Group_Main_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProductGroup As New Controller_clsCategoryMain
    Dim ID As String
    Dim table As New DataTable()

    Dim oclsProduct As ClsDataccess = New ClsDataccess()
    Dim selectedRowIndex As Integer
    'Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
    'Dim con As SqlConnection = New SqlConnection(connectionString)
    Public Sub bindGrid()
        Try
            oclsProductGroup.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProductGroup.SelectAll()

            'rdCategory.AllowPaging = True

            If dt.Rows.Count > 0 Then
                rdCategory.DataSource = dt
                rdCategory.DataBind()
            Else
                rdCategory.DataSource = String.Empty
                rdCategory.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Group_Main_List:bindGrid:" + ex.Message)
        End Try


    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hdsearchvalueAfterEdit.Value = Session("JquerySearchFilter")

                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("add") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("add") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Or Val(ViewState("delete")) = 1 Then
                    divPGroup.Visible = True
                Else
                    divPGroup.Visible = False
                End If
                If ViewState("add") = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If

                bindGrid()
            Else
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Group_Main_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Group Category"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_add = 1 Then
                ViewState("add") = 1
            Else
                ViewState("add") = 0
            End If
            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If
            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Group_Main_List:RoleCheck:" + ex.Message)
        End Try
    End Sub



    Protected Sub deleteProdct_Group(ByVal id As Integer)
        Try
            oclsProductGroup.Category_id = Val(id)
            oclsProductGroup.cmp_id = Val(Session("cmp_id"))
            oclsProductGroup.key_map_id = 0
            oclsProductGroup.name = ""
            oclsProductGroup.description = ""
            oclsProductGroup.Is_active = 0
            oclsProductGroup.Ip_address = ""
            oclsProductGroup.login_id = 0
            oclsProductGroup.machine_id = 0
            oclsProductGroup.sorting_no = 0
            oclsProductGroup.Tran_Type = "D"
            oclsProductGroup.Is_web_available = 0
            oclsProductGroup.Strlocation_id = ""
            oclsProductGroup.click_and_collect = 0
            oclsProductGroup.deliver = 0
            oclsProductGroup.Order_at_table = 0
            oclsProductGroup.Description_sales = ""
            'oclsProductGroup.Aimage =
            oclsProductGroup.Delete()

            'delete img
            Dim idStr As String
            idStr = Val(id).ToString

            Dim pth As String
            pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & id & ".jpg"

            If System.IO.File.Exists(pth) Then
                System.IO.File.Delete(pth)
            End If

            pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & id & ".png"
            If System.IO.File.Exists(pth) Then
                System.IO.File.Delete(pth)
            End If

            pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & id & ".jpeg"
            If System.IO.File.Exists(pth) Then
                System.IO.File.Delete(pth)
            End If
            'delete img end


            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Group_Main_List:deleteProdct_Group:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("category_id") = Nothing
            Response.Redirect("Product_Group_Main_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_Group_Main_List:lnkNew_Click:" + ex.Message)
        End Try
    End Sub
    Private Shared Function GetOrder(ByVal ordersToSearchIn As IEnumerable(Of Order), ByVal Category_id As Decimal) As Order
        For Each order As Order In ordersToSearchIn
            If order.Category_id = Category_id Then
                Return order
            End If
        Next
        Return Nothing
    End Function


    Public Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable

        'Dim table As New DataTable()

        'table.Columns.Add("Category_id")

        'For Each item In list
        '    table.Columns.Add(item)
        'Next
        'Return table

        Dim entityType As Type = GetType(T)
        'Dim table As New DataTable()
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)
        For Each prop As PropertyDescriptor In properties
            table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = If(prop.GetValue(item), DBNull.Value)
            Next
            table.Rows.Add(row)
        Next
        Return table



        'Dim table As New DataTable()
        'Dim fields() As FieldInfo = GetType(T).GetFields()
        'For Each field As FieldInfo In fields
        '    table.Columns.Add(field.Name, field.FieldType)
        'Next
        'For Each item As T In list
        '    Dim row As DataRow = table.NewRow()
        '    For Each field As FieldInfo In fields
        '        row(field.Name) = field.GetValue(item)
        '    Next
        '    table.Rows.Add(row)
        'Next
        'Return table
    End Function

    Protected Sub Upd_Sorting_no()
        ' rdCategory.AllowPaging = False
        ' rdCategory.Rebind()
        bindGrid()
        Dim dt As New DataTable()

        'dt.Columns.AddRange(New DataColumn(0) {New DataColumn("Category_id", GetType(Integer))})
        'For Each item As GridItem In rdCategory.MasterTableView.Items
        '    Dim dataitem As GridDataItem = CType(item, GridDataItem)
        '    Dim Category_id As Integer = Integer.Parse(item.Cells(2).Text)
        '    dt.Rows.Add(Category_id)
        'Next

        dt = table.Copy()
        dt.Columns.Remove("name")
        dt.Columns.Remove("is_active")
        dt.Columns.Remove("Machine_Name")

        If dt.Rows.Count > 0 Then
            Dim consString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Using con As New SqlConnection(consString)
                Using cmd As New SqlCommand("P_M_Cat_Sort")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@tblcatsort", dt)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
            'rdCategory.AllowPaging = True
            'rdCategory.Rebind()
            bindGrid()
            'oclsProductGroup.dt = dt
            'oclsProductGroup.Update_sorting_no_dt()
        End If
    End Sub

#Region "Nested Type: Order"
    Protected Class Order
        Private _name As String
        Private _is_active As String
        Private _Category_id As Decimal
        Private _Machine_Name As String


        Public Sub New(ByVal name As String, ByVal is_active As String, ByVal Machine_Name As String, ByVal Category_id As Decimal)
            _name = name
            _is_active = is_active
            _Category_id = Category_id
            _Machine_Name = Machine_Name

        End Sub

        Public ReadOnly Property name() As String
            Get
                Return _name
            End Get
        End Property

        Public ReadOnly Property Is_active() As String
            Get
                Return _is_active
            End Get
        End Property

        Public ReadOnly Property Category_id() As Decimal
            Get
                Return _Category_id
            End Get
        End Property

        Public ReadOnly Property Machine_Name() As String
            Get
                Return _Machine_Name
            End Get
        End Property

    End Class
#End Region


    Protected Sub rdCategory_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("category_id") = Convert.ToInt32(e.CommandArgument)
                Session("JquerySearchFilter") = hdnsearchvalue.Value
                Response.Redirect("Product_Group_Main_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteProdct_Group(Convert.ToInt32(e.CommandArgument))



                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Group_Main_List:rdcategory_ItemCommand:" + ex.Message)
        End Try
    End Sub

End Class


Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Web.UI.WebControls

Public Class clsBinding
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Dim oclsCommon As Controller_clsCommon = New Controller_clsCommon()
    Dim oclsStaff As Controller_clsStaff = New Controller_clsStaff()
    Dim oclsRole As Controller_clsRole = New Controller_clsRole()
    Dim oclsCategory As Controller_clsCategory = New Controller_clsCategory()
    Dim oclsPrinter As Controller_clsPrinter = New Controller_clsPrinter()
    Dim oclsFunction As Controller_clsFunction = New Controller_clsFunction()
    Dim oclsKeyMap As Controller_clsKey_Map = New Controller_clsKey_Map()
    Dim oclsMachine As Controller_clsMachine = New Controller_clsMachine()
    Dim oclsTax As Controller_clsTax = New Controller_clsTax()
    Dim oclsVenue As Controller_clsVenue = New Controller_clsVenue()
    Dim oclsProduct As Controller_clsProduct = New Controller_clsProduct()
    Dim oclsLocation As Controller_clsLocation = New Controller_clsLocation()
    Dim oclsDevice As Controller_clsDevice = New Controller_clsDevice()
    Dim oclsDevice_Type As Controller_clsDevice_Type = New Controller_clsDevice_Type()
    Dim oclsSize As Controller_clsSize = New Controller_clsSize()
    Dim oclsUnit As Controller_clsUnit = New Controller_clsUnit()
    Dim oclsPayment As Controller_clsPayment = New Controller_clsPayment()
    Dim oclsDepartment As Controller_clsDepartment = New Controller_clsDepartment()
    Dim oclsCourse As Controller_clsCourse = New Controller_clsCourse()
    Dim oclsStock As Controller_clsStock = New Controller_clsStock()
    Dim oclsSale As Controller_clsSales = New Controller_clsSales()
    Dim oclsPanel As Controller_clsPanel = New Controller_clsPanel()
    Dim oclsExpenseMaster As Controller_clsExpenseMaster = New Controller_clsExpenseMaster()
    Dim oclsCategoryMain As Controller_clsCategoryMain = New Controller_clsCategoryMain()
    Dim objclsCustomer As Controller_clsCustomer = New Controller_clsCustomer()
    Dim objclsTable_Map As Controller_clsTable_Map = New Controller_clsTable_Map()
    Dim objclsProfile As Controller_clsProfileMaster = New Controller_clsProfileMaster()


    Public Sub New()
    End Sub

    Public Sub BindCountry(ByRef ddl As RadComboBox)
        Try
            Dim dt As DataTable = oclsCommon.SelectCountry()
            ddl.DataSource = dt
            ddl.DataTextField = "CountryName"
            ddl.DataValueField = "Country_id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindCountry(ByRef ddl As DropDownList)
        Try
            Dim dt As DataTable = oclsCommon.SelectCountry()
            ddl.DataSource = dt
            ddl.DataTextField = "CountryName"
            ddl.DataValueField = "Country_id"
            ddl.DataBind()
            'Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddl.Items.Insert(0, "--SELECT--")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindState(ByRef ddl As RadComboBox, ByVal Country_id As Integer)
        Try
            oclsCommon.country_id = Country_id
            Dim dt As DataTable = oclsCommon.SelectState()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "StateName"
                ddl.DataValueField = "State_Id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception
        End Try

    End Sub

    Public Sub BindCity(ByRef ddl As RadComboBox, ByVal State_id As Integer)
        Try
            oclsCommon.state_id = State_id
            Dim dt As DataTable = oclsCommon.SelectCity()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "CityName"
                ddl.DataValueField = "City_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub


    Public Sub BindState(ByRef ddl As DropDownList, ByVal Country_id As Integer)
        Try
            oclsCommon.country_id = Country_id
            Dim dt As DataTable = oclsCommon.SelectState()

            ddl.DataSource = dt
            ddl.DataTextField = "StateName"
            ddl.DataValueField = "State_Id"
            ddl.DataBind()

            'Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddl.Items.Insert(0, "--SELECT--")

        Catch ex As Exception
        End Try

    End Sub

    Public Sub BindCity(ByRef ddl As DropDownList, ByVal State_id As Integer)
        Try
            oclsCommon.state_id = State_id
            Dim dt As DataTable = oclsCommon.SelectCity()

            ddl.DataSource = dt
            ddl.DataTextField = "CityName"
            ddl.DataValueField = "City_id"
            ddl.DataBind()

            'Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddl.Items.Insert(0, "--SELECT--")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindStaff_new(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsStaff.cmp_id = cmp_id
            Dim dt As DataTable = oclsStaff.SelectStaff_new()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "staff_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindStaff(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsStaff.cmp_id = cmp_id
            Dim dt As DataTable = oclsStaff.SelectStaff()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "staff_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindRole(ByRef ddl As Object, ByVal cmp_id As Integer)
        Try
            oclsRole.cmp_id = cmp_id
            Dim dt As DataTable = oclsRole.SelectRoleByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Role_Name"
                ddl.DataValueField = "Role_Id"
                ddl.DataBind()
            End If
            ' Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindStaffBindStaffLoginId(ByRef ddl As Object, ByVal cmp_id As Integer, ByVal staff_id As Integer)
        Try
            oclsStaff.cmp_id = cmp_id
            oclsStaff.staff_id = staff_id
            Dim dt As DataTable = oclsStaff.SelectStaffLoginId()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "login_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
            ddl.SelectedValue = 0
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindStaffBindStaffLoginIdForDiscount(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal staff_id As Integer)
        Try
            oclsStaff.cmp_id = cmp_id
            oclsStaff.staff_id = staff_id
            Dim dt As DataTable = oclsStaff.SelectStaffLoginIdForDiscount()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "login_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
            ddl.SelectedValue = 0
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindStaffBindStaffLoginIdForDiscount(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal staff_id As Integer)
        Try
            oclsStaff.cmp_id = cmp_id
            oclsStaff.staff_id = staff_id
            Dim dt As DataTable = oclsStaff.SelectStaffLoginIdForDiscount()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "login_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
            ddl.SelectedValue = 0
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindTableName(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            objclsTable_Map.cmp_id = cmp_id
            Dim dt As DataTable = objclsTable_Map.Select_tableName()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Table_name"
                ddl.DataValueField = "table_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductGroupByMainCategory(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal MainCategory_id As Integer)
        Try
            oclsCategoryMain.cmp_id = cmp_id
            oclsCategoryMain.Category_id = MainCategory_id
            Dim dt As DataTable = oclsCategoryMain.SelectProductGroupByCategory()

            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "category_id"
            ddl.DataBind()

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductGroupByMainCategory(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal MainCategory_id As Integer)
        Try
            oclsCategoryMain.cmp_id = cmp_id
            oclsCategoryMain.Category_id = MainCategory_id
            Dim dt As DataTable = oclsCategoryMain.SelectProductGroupByCategory()

            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "category_id"
            ddl.DataBind()

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, li)
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductGroup(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsCategory.cmp_id = cmp_id
            Dim dt As DataTable = oclsCategory.SelectCategoryByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "category_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductGroup(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsCategory.cmp_id = cmp_id
            Dim dt As DataTable = oclsCategory.SelectCategoryByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "category_id"
                ddl.DataBind()
            End If

            Dim li As ListItem = New ListItem("SELECT", 0)
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductGroupMain(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsCategoryMain.cmp_id = cmp_id
            Dim dt As DataTable = oclsCategoryMain.SelectCategoryByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "category_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductGroupMain(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsCategoryMain.cmp_id = cmp_id
            Dim dt As DataTable = oclsCategoryMain.SelectCategoryByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "category_id"
                ddl.DataBind()
            End If

            Dim li As ListItem = New ListItem("SELECT", 0)
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindProductGroup1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsCategory.cmp_id = cmp_id
            Dim dt As DataTable = oclsCategory.SelectCategoryByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "name"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindProductByProductGroup(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal category_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            oclsProduct.Category_id = category_id
            Dim dt As DataTable = oclsProduct.SelectProductByGroup()

            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "product_id"
            ddl.DataBind()

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductByProductGroup(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal category_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            oclsProduct.Category_id = category_id
            Dim dt As DataTable = oclsProduct.SelectProductByGroup()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "product_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductByProductGroupLocation(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal category_id As Integer, ByVal Location_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            oclsProduct.Category_id = category_id
            oclsProduct.Location_id = Location_id
            Dim dt As DataTable = oclsProduct.SelectProductByGroupLocation()

            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "product_id"
            ddl.DataBind()

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductByProductGroupLocation(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal category_id As Integer, ByVal Location_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            oclsProduct.Category_id = category_id
            oclsProduct.Location_id = Location_id
            Dim dt As DataTable = oclsProduct.SelectProductByGroupLocation()

            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "product_id"
            ddl.DataBind()

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub


    Public Sub BindDepartment(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try

            oclsDepartment.Cmp_id = cmp_id

            Dim dt As DataTable = oclsDepartment.Select_By_Cmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Name"
                ddl.DataValueField = "Department_id"
                ddl.DataBind()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindDepartment(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsDepartment.Cmp_id = cmp_id

            Dim dt As DataTable = oclsDepartment.Select_By_Cmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Name"
                ddl.DataValueField = "Department_id"
                ddl.DataBind()
            End If


        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindDepartmentSelect(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsDepartment.Cmp_id = cmp_id

            Dim dt As DataTable = oclsDepartment.Select_By_Cmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Name"
                ddl.DataValueField = "Department_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindDepartment1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsDepartment.Cmp_id = cmp_id

            Dim dt As DataTable = oclsDepartment.Select_By_Cmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Name"
                ddl.DataValueField = "Name"
                ddl.DataBind()
            End If


        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindDepartmentALL(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsDepartment.Cmp_id = cmp_id

            Dim dt As DataTable = oclsDepartment.Select_By_Cmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Name"
                ddl.DataValueField = "Department_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("ALL", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindCourse(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsCourse.Cmp_id = cmp_id

            Dim dt As DataTable = oclsCourse.Select_By_Cmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Name"
                ddl.DataValueField = "Course_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindCourse(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try

            oclsCourse.Cmp_id = cmp_id

            Dim dt As DataTable = oclsCourse.Select_By_Cmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Name"
                ddl.DataValueField = "Course_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindCourse1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsCourse.Cmp_id = cmp_id

            Dim dt As DataTable = oclsCourse.Select_By_Cmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Name"
                ddl.DataValueField = "Name"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindPrinter(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsPrinter.cmp_id = cmp_id
            Dim dt As DataTable = oclsPrinter.SelectPrinterByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "printer_id"
                ddl.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindPrinter(ByRef ddl As ListBox, ByVal cmp_id As Integer)
        Try
            oclsPrinter.cmp_id = cmp_id
            Dim dt As DataTable = oclsPrinter.SelectPrinterByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "printer_id"
                ddl.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindPrinter1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsPrinter.cmp_id = cmp_id
            Dim dt As DataTable = oclsPrinter.SelectPrinterByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "name"
                ddl.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindFunctionType(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsFunction.cmp_id = cmp_id
            Dim dt As DataTable = oclsFunction.SelectFunctionByCmp()
            If Session("show_chips") = "0" Then
                Dim dv As DataView = dt.DefaultView
                dv.RowFilter = "name not like '%Dance%'"
                dt = dv.ToTable()
            End If

            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "name"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindCardPaymentType(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsFunction.cmp_id = cmp_id
            Dim dt As DataTable = oclsFunction.SelectCardPaymentType()

            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "CardName"
                ddl.DataValueField = "CardPayType_Id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindParentFunction(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsFunction.cmp_id = cmp_id
            Dim dt As DataTable = oclsFunction.SelectParentFunctionByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "function_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindEditPanel(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal mainFunction_id As Integer)
        Try
            oclsFunction.cmp_id = cmp_id
            oclsFunction.mainfunction_id = mainFunction_id
            Dim dt As DataTable = oclsFunction.SelectEditPanelByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "Parent_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindParentFunction1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsFunction.cmp_id = cmp_id
            Dim dt As DataTable = oclsFunction.SelectParentFunctionByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "name"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindPanel(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsPanel.cmp_id = cmp_id
            Dim dt As DataTable = oclsPanel.Select_Panel()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Panel"
                ddl.DataValueField = "Panel_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindPanel1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsPanel.cmp_id = cmp_id
            Dim dt As DataTable = oclsPanel.Select_Panel()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Panel"
                ddl.DataValueField = "Panel"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindLocation_ddl(ByRef ddl As Object, ByVal cmp_id As Integer)
        Try

            oclsLocation.cmp_id = cmp_id
            Dim dt As DataTable = oclsLocation.SelectLocationByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "location_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindLocation(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsLocation.cmp_id = cmp_id
            Dim dt As DataTable = oclsLocation.SelectLocationByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "location_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindLocation_new(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsLocation.cmp_id = cmp_id
            Dim dt As DataTable = oclsLocation.SelectLocationByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "location_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub bindTemplate(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsStock.cmp_id = cmp_id
            Dim dt As DataTable = oclsStock.SelectTemplateByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "templete_name"
                ddl.DataValueField = "Templete_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindLocation(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try

            oclsLocation.cmp_id = cmp_id
            Dim dt As DataTable = oclsLocation.SelectLocationByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "location_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindLocationProduct(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try

            oclsLocation.cmp_id = cmp_id
            Dim dt As DataTable = oclsLocation.SelectLocationByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "location_id"
                ddl.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindLocationProduct(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try

            oclsLocation.cmp_id = cmp_id
            Dim dt As DataTable = oclsLocation.SelectLocationByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "location_id"
                ddl.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindKeyMap(ByRef ddl As Object, ByVal cmp_id As Integer)
        Try
            oclsKeyMap.cmp_id = cmp_id
            Dim dt As DataTable = oclsKeyMap.SelectKeyMapByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "key_map_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindMachine(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            Dim dt As DataTable = oclsMachine.SelectMachineByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "machine_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindMachine(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            Dim dt As DataTable = oclsMachine.SelectMachineByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "machine_id"
                ddl.DataBind()
            End If

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindMachine1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            Dim dt As DataTable = oclsMachine.SelectMachineByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "name"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindMachineFunction(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal machine_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            oclsMachine.machine_id = machine_id
            Dim dt As DataTable = oclsMachine.SelectMachineByFunction()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "machine_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindSwapMachineFunction(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal machine_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            oclsMachine.machine_id = machine_id
            Dim dt As DataTable = oclsMachine.SelectSwapMachineByFunction()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "machine_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindTax(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsTax.cmp_id = cmp_id
            Dim dt As DataTable = oclsTax.SelectTaxByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "Tax_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindTax(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsTax.cmp_id = cmp_id
            Dim dt As DataTable = oclsTax.SelectTaxByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "Tax_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, li)
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try

    End Sub
    Public Sub BindProfile(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            objclsProfile.cmp_id = cmp_id
            Dim dt As DataTable = objclsProfile.SelectProfile()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "profile_name"
                ddl.DataValueField = "profile_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindTax1(ByRef ddl As Object, ByVal cmp_id As Integer)
        Try
            oclsTax.cmp_id = cmp_id
            Dim dt As DataTable = oclsTax.SelectTaxByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "Tax_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindTaxDirect(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsTax.cmp_id = cmp_id
            Dim dt As DataTable = oclsTax.SelectTaxByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "Tax_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindTaxDirect(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsTax.cmp_id = cmp_id
            Dim dt As DataTable = oclsTax.SelectTaxByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "Tax_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, li)
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    'Public Sub BindVenue(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
    '    Try
    '        oclsVenue.cmp_id = cmp_id
    '        Dim dt As DataTable = oclsVenue.SelectVenueByCmp()
    '        If dt.Rows.Count > 0 Then
    '            ddl.DataSource = dt
    '            ddl.DataTextField = "venue_name"
    '            ddl.DataValueField = "venue_id"
    '            ddl.DataBind()
    '        End If
    '        Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
    '        ddl.Items.Insert(0, li)
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub BindVenueByRoleVenue(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal v_id As String)
        Try
            oclsVenue.cmp_id = cmp_id
            oclsVenue.RoleVenue = v_id
            Dim dt As DataTable = oclsVenue.SelectVenueByRoleVenue()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "venue_name"
                ddl.DataValueField = "venue_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindVenue(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsVenue.cmp_id = cmp_id
            Dim dt As DataTable = oclsVenue.SelectVenueByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "venue_name"
                ddl.DataValueField = "venue_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindVenue(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsVenue.cmp_id = cmp_id
            Dim dt As DataTable = oclsVenue.SelectVenueByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "venue_name"
                ddl.DataValueField = "venue_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindSMaster(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsStaff.cmp_id = cmp_id
            Dim dt As DataTable = oclsStaff.SelectSMaster()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "m_staff_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProduct(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            Dim dt As DataTable = oclsProduct.SelectProductByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "product_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProduct(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            Dim dt As DataTable = oclsProduct.SelectProductByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "product_id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindLocationByVenue(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal Venue_id As Integer)
        Try
            oclsLocation.cmp_id = cmp_id
            oclsLocation.venue_id = Venue_id
            Dim dt As DataTable = oclsLocation.SelectLocationByVenue()

            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "location_id"
            ddl.DataBind()

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindLocationByVenue(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal Venue_id As Integer)
        Try
            oclsLocation.cmp_id = cmp_id
            oclsLocation.venue_id = Venue_id
            Dim dt As DataTable = oclsLocation.SelectLocationByVenue()

            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "location_id"
            ddl.DataBind()

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindMachineByLocation(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal Location_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            oclsMachine.location_id = Location_id
            Dim dt As DataTable = oclsMachine.SelectMachineByLocation()
            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "machine_id"
            ddl.DataBind()

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindMachineByLocation_new(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal Location_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            oclsMachine.location_id = Location_id
            Dim dt As DataTable = oclsMachine.SelectMachineByLocation()
            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "machine_id"
            ddl.DataBind()

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindMachineByLocation(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal Location_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            oclsMachine.location_id = Location_id
            Dim dt As DataTable = oclsMachine.SelectMachineByLocation()
            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "machine_id"
            ddl.DataBind()

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindDevice(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsDevice.cmp_id = cmp_id
            Dim dt As DataTable = oclsDevice.SelectDeviceByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "device_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindDeviceByMachine(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal machine_id As Integer)
        Try
            oclsDevice.cmp_id = cmp_id
            oclsDevice.machine_id = machine_id
            Dim dt As DataTable = oclsDevice.SelectDeviceByMachine()
            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "device_id"
            ddl.DataBind()

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindFunctionCode(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal function_id As Integer)
        Try
            oclsFunction.cmp_id = cmp_id
            oclsFunction.function_id = function_id
            Dim dt As DataTable = oclsFunction.SelectFunctionCode()
            ddl.DataSource = dt
            ddl.DataTextField = "Code"
            ddl.DataValueField = "Code"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindFunctionCode1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal function_id As Integer, ByVal Parent_id As Integer, ByVal Till_id As Integer)
        Try
            oclsFunction.cmp_id = cmp_id
            oclsFunction.function_id = function_id
            oclsFunction.Parent_id = Parent_id
            oclsFunction.machine_id = Till_id
            Dim dt As DataTable = oclsFunction.SelectFunctionCode_New()
            ddl.DataSource = dt
            ddl.DataTextField = "Code"
            ddl.DataValueField = "Code"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindDeviceType(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsDevice_Type.cmp_id = cmp_id
            Dim dt As DataTable = oclsDevice_Type.SelectDeviceType()
            ddl.DataSource = dt
            ddl.DataTextField = "Device_Type"
            ddl.DataValueField = "Device_Type_id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindDeviceType(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal cat_id As Integer)
        Try
            oclsDevice_Type.cmp_id = cmp_id
            oclsDevice_Type.cat_id = cat_id
            Dim dt As DataTable = oclsDevice_Type.SelectDeviceTypecat()
            ddl.DataSource = dt
            ddl.DataTextField = "Device_Type"
            ddl.DataValueField = "Device_Type_id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindDeviceCategory(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsDevice_Type.cmp_id = cmp_id
            Dim dt As DataTable = oclsDevice_Type.SelectDeviceCategory()
            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "device_category_id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindDeviceType(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsDevice_Type.cmp_id = cmp_id
            Dim dt As DataTable = oclsDevice_Type.SelectDeviceType()
            ddl.DataSource = dt
            ddl.DataTextField = "Device_Type"
            ddl.DataValueField = "Device_Type_id"
            ddl.DataBind()

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindUnit(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsUnit.cmp_id = cmp_id
            Dim dt As DataTable = oclsUnit.Select_Unit()
            ddl.DataSource = dt
            ddl.DataTextField = "Unit"
            ddl.DataValueField = "Unit_Id"
            ddl.DataBind()
            ' Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindUnit(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsUnit.cmp_id = cmp_id
            Dim dt As DataTable = oclsUnit.Select_Unit()
            ddl.DataSource = dt
            ddl.DataTextField = "Unit"
            ddl.DataValueField = "Unit_Id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    'added by hardik
    Public Sub BindPrices_By_Cmp(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            Dim dt As DataTable = oclsProduct.SelectPricesByCmp()
            ddl.DataSource = dt
            ddl.DataTextField = "PPrice"
            ddl.DataValueField = "Product_Price_Id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindPrices_By_Cmp(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            Dim dt As DataTable = oclsProduct.SelectPricesByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "PPrice"
                ddl.DataValueField = "Product_Price_Id"
                ddl.DataBind()
            End If
            ' Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    'end

    Public Sub BindUnitFor_Gm(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsUnit.cmp_id = cmp_id
            Dim dt As DataTable = oclsUnit.Select_Unit_For_Gm()
            ddl.DataSource = dt
            ddl.DataTextField = "Unit"
            ddl.DataValueField = "Unit_Id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindUnitFor_Gm(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsUnit.cmp_id = cmp_id
            Dim dt As DataTable = oclsUnit.Select_Unit_For_Gm()
            ddl.DataSource = dt
            ddl.DataTextField = "Unit"
            ddl.DataValueField = "Unit_Id"
            ddl.DataBind()
            ' Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindUnitFor_Ml(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsUnit.cmp_id = cmp_id
            Dim dt As DataTable = oclsUnit.Select_Unit_For_Ml()
            ddl.DataSource = dt
            ddl.DataTextField = "Unit"
            ddl.DataValueField = "Unit_Id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindUnitFor_Ml(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsUnit.cmp_id = cmp_id
            Dim dt As DataTable = oclsUnit.Select_Unit_For_Ml()
            ddl.DataSource = dt
            ddl.DataTextField = "Unit"
            ddl.DataValueField = "Unit_Id"
            ddl.DataBind()
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindUnit1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsUnit.cmp_id = cmp_id
            Dim dt As DataTable = oclsUnit.Select_Unit()
            ddl.DataSource = dt
            ddl.DataTextField = "Unit"
            ddl.DataValueField = "Unit"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindCurrency(ByRef ddl As RadComboBox)
        Try
            Dim dt As DataTable = oclsCommon.SelectCurrency()
            ddl.DataSource = dt
            ddl.DataTextField = "Currency"
            ddl.DataValueField = "Currency_Id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    'Public Sub BindBusinessDone(ByRef ddl As RadComboBox)
    '    Try
    '        Dim dt As DataTable = oclsCommon.SelectBusinessDone()
    '        ddl.DataSource = dt
    '        ddl.DataTextField = "Name"
    '        ddl.DataValueField = "Department_id"
    '        ddl.DataBind()
    '        Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
    '        ddl.Items.Insert(0, li)

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub BindProductBySize(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal product_id As Integer)
        Try
            oclsSize.cmp_id = cmp_id
            oclsSize.product_id = product_id
            Dim dt As DataTable = oclsSize.SelectProductBySize()
            ddl.DataSource = dt
            ddl.DataTextField = "Size_Unit"
            ddl.DataValueField = "Size_Id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductBySize(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal product_id As Integer)
        Try
            oclsSize.cmp_id = cmp_id
            oclsSize.product_id = product_id
            Dim dt As DataTable = oclsSize.SelectProductBySize()
            ddl.DataSource = dt
            ddl.DataTextField = "Size_Unit"
            ddl.DataValueField = "Size_Id"
            ddl.DataBind()
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductCondiment(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsProduct.cmp_id = cmp_id
            Dim dt As DataTable = oclsProduct.SelectProductCondiment()
            ddl.DataSource = dt
            ddl.DataTextField = "name"
            ddl.DataValueField = "Product_Id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindVenue1(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsVenue.cmp_id = cmp_id
            Dim dt As DataTable = oclsVenue.SelectVenue()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "venue_name"
                ddl.DataValueField = "venue_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub bindPayment(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsPayment.cmp_id = cmp_id
            Dim dt As DataTable = oclsPayment.SelectPayment()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "Payment_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindProductGroupALL(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsCategory.cmp_id = cmp_id
            Dim dt As DataTable = oclsCategory.SelectCategoryByCmpALL()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "category_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindProductGroupALL(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsCategory.cmp_id = cmp_id
            Dim dt As DataTable = oclsCategory.SelectCategoryByCmpALL()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "category_id"
                ddl.DataBind()
            End If

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindRoleAll(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsRole.cmp_id = cmp_id
            Dim dt As DataTable = oclsRole.SelectRoleAll()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Role_Name"
                ddl.DataValueField = "Role_Name"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindFunctionTypeAll(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsFunction.cmp_id = cmp_id
            Dim dt As DataTable = oclsFunction.SelectFunctionAll()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "name"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindUnitByProduct(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal product_id As Integer)
        Try
            oclsStock.cmp_id = cmp_id
            oclsStock.product_id = product_id
            Dim dt As DataTable = oclsStock.select_tax()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Unit"
                ddl.DataValueField = "Unit_Id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindUnitByProduct(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal product_id As Integer)
        Try
            oclsStock.cmp_id = cmp_id
            oclsStock.product_id = product_id
            Dim dt As DataTable = oclsStock.select_tax()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Unit"
                ddl.DataValueField = "Unit_Id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            'ddl.Items.Insert(0, li)
            ddl.Items.Insert(0, "SELECT")
        Catch ex As Exception

        End Try
    End Sub


    Public Sub BindSizeByProduct(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal product_id As Integer)
        Try
            oclsSize.cmp_id = cmp_id
            oclsSize.product_id = product_id
            Dim dt As DataTable = oclsSize.SelectProductBySize()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Size"
                ddl.DataValueField = "Size_Id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindSizeUnitByProduct(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal product_id As Integer)
        Try
            oclsSize.cmp_id = cmp_id
            oclsSize.product_id = product_id
            Dim dt As DataTable = oclsSize.SelectSizeUnitByProduct()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Size_Unit"
                ddl.DataValueField = "Size_Id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindSizeUnitByProduct_1(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal product_id As Integer)
        Try
            oclsSize.cmp_id = cmp_id
            oclsSize.product_id = product_id
            Dim dt As DataTable = oclsSize.SelectSizeUnitByProduct()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Size_Unit"
                ddl.DataValueField = "Size_Id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub Bindtill(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal machine_id As Integer)
        Try

            oclsLocation.cmp_id = cmp_id
            oclsMachine.machine_id = machine_id
            Dim dt As DataTable = oclsMachine.Gettill()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindSizeUnitByProduct(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal product_id As Integer)
        Try
            oclsSize.cmp_id = cmp_id
            oclsSize.product_id = product_id
            Dim dt As DataTable = oclsSize.SelectSizeUnitByProduct()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Size_Unit"
                ddl.DataValueField = "Size_Id"
                ddl.DataBind()
            End If
            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindSizeUnitByProductandLocation(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal product_id As Integer, ByVal Location_id As Integer)
        Try
            oclsSize.cmp_id = cmp_id
            oclsSize.product_id = product_id
            oclsSize.Location_Id = Location_id
            Dim dt As DataTable = oclsSize.[SelectSizeUnitByProductandLocation]()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Size_Unit"
                ddl.DataValueField = "Size_Id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindSizeUnitByProductandLocation(ByRef ddl As DropDownList, ByVal cmp_id As Integer, ByVal product_id As Integer, ByVal Location_id As Integer)
        Try
            oclsSize.cmp_id = cmp_id
            oclsSize.product_id = product_id
            oclsSize.Location_Id = Location_id
            Dim dt As DataTable = oclsSize.[SelectSizeUnitByProductandLocation]()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Size_Unit"
                ddl.DataValueField = "Size_Id"
                ddl.DataBind()
            End If

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindDiscount(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsSale.cmp_id = cmp_id
            Dim dt As DataTable = oclsSale.SelectDiscountByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Tsale_discount_name"
                ddl.DataValueField = "Tsale_discount_name"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindDiscount(ByRef ddl As DropDownList, ByVal cmp_id As Integer)
        Try
            oclsSale.cmp_id = cmp_id
            Dim dt As DataTable = oclsSale.SelectDiscountByCmp()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "Tsale_discount_name"
                ddl.DataValueField = "Tsale_discount_name"
                ddl.DataBind()
            End If
            ' Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindExpense(ByRef ddl As RadComboBox)
        Try
            Dim dt As DataTable = oclsExpenseMaster.Select_Expense()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "Exp_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindMerchant(ByRef ddl As RadComboBox)
        Try
            Dim dt As DataTable = oclsLocation.Select_Merchant()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "location_id"
                ddl.DataBind()
            End If

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Bindpro(ByRef ddl As Object, ByVal cmp_id As Integer)
        Try
            objclsCustomer.cmp_id = cmp_id
            Dim dt As DataTable = objclsCustomer.[SelectProfile]()
            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "profile_name"
                ddl.DataValueField = "profile_id"
                ddl.DataBind()
            End If

            'Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, "SELECT")

        Catch ex As Exception

        End Try
    End Sub

    'add by hardik

    Public Sub BindShiftByDuration(ByRef ddl As RadComboBox, ByVal cmp_id As Integer, ByVal date1 As DateTime, ByVal date2 As DateTime, ByVal duration As String)
        Try
            oclsMachine.cmp_id = cmp_id
            oclsMachine.date1 = date1
            oclsMachine.date2 = date2
            oclsMachine.duration = duration
            Dim dt As DataTable = oclsMachine.SelectShiftByDuration()
            ddl.DataSource = dt
            ddl.DataTextField = "shift_name"
            ddl.DataValueField = "shift_name"
            ddl.DataBind()

            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)
        Catch ex As Exception
            Dim s As String = ex.Message.ToString()
        End Try
    End Sub

    Public Sub BindShiftNoByTill(ByRef txtTill As RadTextBox, ByVal cmp_id As Integer, ByVal machine_id As Integer)
        Try
            oclsMachine.cmp_id = cmp_id
            oclsMachine.machine_id = machine_id
            Dim dt As DataTable = oclsMachine.SelectShiftNoByTill
            'txtTill.DataSource = dt.Rows[0]["Name"];
            'txtTill.DataTextField = "shift_name"
            'txtTill.DataBind()
            'txtTill.Text = dt.Rows


        Catch ex As Exception

        End Try
    End Sub

End Class

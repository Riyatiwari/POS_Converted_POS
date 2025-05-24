Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.Services.Protocols
Imports System.Runtime.Serialization.Json
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Web.Script.Serialization

Partial Class POS_WS
    Inherits BaseClass
    'Dim oClsDal As ClsDataccess = New ClsDataccess()
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Response.Clear()
            CallMethod()
        Catch ex As Exception
            LogHelper.Error("POS_WS:Page_Load:" + ex.Message)
        End Try
    End Sub

    Public Sub CallMethod()
        Try
            Dim str As String
            Dim objwebservices As New ServiceReference1.POS_WebServiceSoapClient

            If Request.QueryString("Function").ToString() = "Login" Then
                str = objwebservices.Login(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("loginU").ToString(), Request.QueryString("LoginP").ToString(),
                                           Request.QueryString("IP").ToString(), Request.QueryString("mac_id").ToString(), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Customer_List" Then
                str = objwebservices.Customer_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "KioskSettiong_List" Then
                str = objwebservices.KioskSettiong_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                   Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("TillID")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "AutoSync_List" Then
                str = objwebservices.AutoSync_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                   Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("TillID")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "AutoSync_Update" Then
                str = objwebservices.AutoSync_Update(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                   Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("AutoSync_Id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Graphic_Table" Then
                str = objwebservices.Graphic_Table(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Request.QueryString("location_id").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Schedule_List" Then
                str = objwebservices.Schedule_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Request.QueryString("location_id").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Customer_Master" Then
                str = objwebservices.Customer_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("login")), Val(Request.QueryString("customer_id")), Request.QueryString("first_name").ToString(),
                                                     Request.QueryString("last_name").ToString(), Request.QueryString("email").ToString(), Request.QueryString("contact").ToString(),
                                                     Request.QueryString("address").ToString(), Val(Request.QueryString("country")), Val(Request.QueryString("state")),
                                                     Val(Request.QueryString("city")), Request.QueryString("postal_code").ToString(), Val(Request.QueryString("is_active")),
                                                     Request.QueryString("ip").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(),
                                                     Val(Request.QueryString("venue_id")), Request.QueryString("other_id").ToString(), Request.QueryString("store_name").ToString(),
                                                     Val(Request.QueryString("machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Customer_Master_withCardNumber" Then
                str = objwebservices.Customer_Master_withCardNumber(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("login")), Val(Request.QueryString("customer_id")), Request.QueryString("first_name").ToString(),
                                                     Request.QueryString("last_name").ToString(), Request.QueryString("email").ToString(), Request.QueryString("contact").ToString(),
                                                     Request.QueryString("address").ToString(), Val(Request.QueryString("country")), Val(Request.QueryString("state")),
                                                     Val(Request.QueryString("city")), Request.QueryString("postal_code").ToString(), Val(Request.QueryString("is_active")),
                                                     Request.QueryString("ip").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(),
                                                     Val(Request.QueryString("venue_id")), Request.QueryString("other_id").ToString(), Request.QueryString("store_name").ToString(),
                                                     Val(Request.QueryString("machine_id")), Val(Request.QueryString("Is_credit")), Request.QueryString("CardNumber"),
                                                     Convert.ToDateTime(Request.QueryString("DateTimeExpiry")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Customer_Details" Then
                str = objwebservices.Customer_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                      Val(Request.QueryString("customer_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "User_List" Then
                str = objwebservices.User_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "User_Details" Then
                str = objwebservices.User_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                  Val(Request.QueryString("customer_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "User_Master" Then
                str = objwebservices.User_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                 Val(Request.QueryString("login")), Val(Request.QueryString("staff_id").ToString()), Request.QueryString("code").ToString(),
                                                 Request.QueryString("name").ToString(), Convert.ToDateTime(Request.QueryString("joining")), Request.QueryString("email").ToString(),
                                                 Request.QueryString("contact").ToString(), Request.QueryString("address").ToString(), Val(Request.QueryString("country")),
                                                 Val(Request.QueryString("state")), Val(Request.QueryString("city").ToString()), Request.QueryString("postal_code").ToString(),
                                                 Val(Request.QueryString("is_active")), Val(Request.QueryString("role_id")), Request.QueryString("ip").ToString(),
                                                 Val(Request.QueryString("till_code")), Val(Request.QueryString("till_active")), Request.QueryString("photo").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("venue_id").ToString()), Request.QueryString("other_id").ToString(), Request.QueryString("store_name").ToString(), Val(Request.QueryString("machine_id")), Request.QueryString("Authentication_Code").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Group_List" Then
                str = objwebservices.Product_Group_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Till_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Group_category_List" Then
                str = objwebservices.Group_category_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Group_List_Till" Then
                str = objwebservices.Product_Group_List_Till(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Group_Details" Then
                str = objwebservices.Product_Group_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                           Val(Request.QueryString("group_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Group_Master" Then
                str = objwebservices.Product_Group_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                          Val(Request.QueryString("login")), Val(Request.QueryString("group_id")), Val(Request.QueryString("key_map_id")), Request.QueryString("name").ToString(),
                                                          Request.QueryString("Description").ToString(), Val(Request.QueryString("is_active").ToString()), Request.QueryString("ip").ToString(),
                                                          Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("sorting_no")), Val(Request.QueryString("machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Printer_List" Then
                str = objwebservices.Printer_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Promotion_List" Then
                str = objwebservices.Promotion_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Location_id")), Val(Request.QueryString("Venue_id")), Val(Request.QueryString("Till_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Promotion_Type_List" Then
                str = objwebservices.Promotion_Type_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Location_id")), Val(Request.QueryString("Venue_id")), Val(Request.QueryString("Till_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Printer_Details" Then
                str = objwebservices.Printer_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("printer_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Printer_Master" Then
                str = objwebservices.Printer_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                    Val(Request.QueryString("login")), Val(Request.QueryString("printer_id")), Request.QueryString("name").ToString(),
                                                    Request.QueryString("palias").ToString(), Val(Request.QueryString("is_active")), Request.QueryString("ip").ToString(),
                                                    Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("venue_id")),
                                                    Request.QueryString("store_name").ToString(), Val(Request.QueryString("machine_id")), Val(Request.QueryString("is_product_small_large")),
                                                    Val(Request.QueryString("is_condiment_small_large")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_List" Then
                str = objwebservices.Product_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Product_Image" Then
                str = objwebservices.Product_Image(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Product_Group_Image" Then
                str = objwebservices.Product_Group_Image(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Group_Category_Image" Then
                str = objwebservices.Group_Category_Image(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Product_List_ForKiosk" Then
                str = objwebservices.Product_List_ForKiosk(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                           Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                           Val(Request.QueryString("location_id")), Val(Request.QueryString("machine_id")),
                                                           Val(Request.QueryString("IsKiosk")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Product_Details" Then
                str = objwebservices.Product_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("product_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Department_List" Then
                str = objwebservices.Department_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Course_List" Then
                str = objwebservices.Course_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Master" Then
                str = objwebservices.Product_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                    Val(Request.QueryString("login")), Val(Request.QueryString("product_id")), Val(Request.QueryString("group_id")),
                                                    Request.QueryString("name").ToString(), Request.QueryString("code").ToString(), Convert.ToDecimal(Request.QueryString("price").ToString()),
                                                    Request.QueryString("barcode").ToString(), Request.QueryString("Description").ToString(), Val(Request.QueryString("is_active")),
                                                    Request.QueryString("ip").ToString(), Val(Request.QueryString("department_id").ToString()), Val(Request.QueryString("course_id")),
                                                    Request.QueryString("printer_id").ToString(), Val(Request.QueryString("key_map_id")), Request.QueryString("List").ToString(),
                                                    Convert.ToDecimal(Request.QueryString("Actual_Price").ToString()), Val(Request.QueryString("Tax_id").ToString()),
                                                    Convert.ToDecimal(Request.QueryString("Tax").ToString()), Request.QueryString("Tran_Type").ToString(),
                                                    Request.QueryString("mac_id").ToString(), Val(Request.QueryString("venue_id").ToString()),
                                                    Request.QueryString("other_id").ToString(), Request.QueryString("other_size").ToString(),
                                                    Request.QueryString("store_name").ToString(), Val(Request.QueryString("Is_Ingredient")),
                                                    Val(Request.QueryString("Is_Condiment")), Request.QueryString("Base").ToString(),
                                                    Val(Request.QueryString("Unit_id")), Val(Request.QueryString("size_zero")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_IsOutOfStock" Then
                str = objwebservices.Product_IsOutOfStock(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("product_id")), Val(Request.QueryString("IsOutofStock")),
                                                    Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Master_import" Then
                str = objwebservices.Product_Master_import(Request.QueryString("authU").ToString(),
                                                           Request.QueryString("authP").ToString(),
                                                           Val(Request.QueryString("Cmp_id")),
                                                    Val(Request.QueryString("login")),
                                                    Request.QueryString("store_name").ToString(),
                                                    Val(Request.QueryString("product_id")),
                                                    Val(Request.QueryString("location_ID")),
                                                    Val(Request.QueryString("department_ID")),
                                                    Val(Request.QueryString("Category_Id")),
                                                    Val(Request.QueryString("printer_id1")),
                                                    Val(Request.QueryString("printer_id2")),
                                                    Val(Request.QueryString("printer_id3")),
                                                    Val(Request.QueryString("Price_level")),
                                                    Request.QueryString("Product_name").ToString(),
                                                    Convert.ToDecimal(Request.QueryString("BaseSize").ToString()),
                                                    Val(Request.QueryString("BaseUnit")),
                                                    Val(Request.QueryString("Size_id1")),
                                                    Request.QueryString("size1name").ToString(),
                                                    Val(Request.QueryString("size1unit")),
                                                    Convert.ToDecimal(Request.QueryString("size1Qty").ToString()),
                                                    Convert.ToDecimal(Request.QueryString("size1price").ToString()),
                                                    Val(Request.QueryString("Price_id1")),
                                                    Val(Request.QueryString("size1Taxid_1")),
                                                    Val(Request.QueryString("size1Taxid_2")),
                                                    Val(Request.QueryString("Size_id2")),
                                                    Request.QueryString("size2name").ToString(),
                                                    Val(Request.QueryString("size2unit")),
                                                    Convert.ToDecimal(Request.QueryString("size2Qty").ToString()),
                                                    Convert.ToDecimal(Request.QueryString("size2price").ToString()),
                                                    Val(Request.QueryString("Price_id2")),
                                                    Val(Request.QueryString("size2Taxid_1")),
                                                    Val(Request.QueryString("size2Taxid_2")),
                                                    Val(Request.QueryString("Size_id3")),
                                                    Request.QueryString("size3name").ToString(),
                                                    Val(Request.QueryString("size3unit")),
                                                    Convert.ToDecimal(Request.QueryString("size3Qty").ToString()),
                                                    Convert.ToDecimal(Request.QueryString("size3price").ToString()),
                                                    Val(Request.QueryString("Price_id3")),
                                                    Val(Request.QueryString("size3Taxid_1")),
                                                    Val(Request.QueryString("size3Taxid_2")),
                                                    Val(Request.QueryString("Size_id4")),
                                                    Request.QueryString("size4name").ToString(),
                                                    Val(Request.QueryString("size4unit")),
                                                    Convert.ToDecimal(Request.QueryString("size4Qty").ToString()),
                                                    Convert.ToDecimal(Request.QueryString("size4price").ToString()),
                                                    Val(Request.QueryString("Price_id4")),
                                                    Val(Request.QueryString("size4Taxid_1")),
                                                    Val(Request.QueryString("size4Taxid_2")),
                                                    Request.QueryString("ip").ToString(),
                                                    Request.QueryString("Tran_Type").ToString(),
                                                    Val(Request.QueryString("Is_OutOfStock")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Product_Master_ins_update" Then
                str = objwebservices.Product_Master_ins_update(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("login")),
                                                    Request.QueryString("store_name").ToString(), Val(Request.QueryString("product_id")), Request.QueryString("name").ToString(), Val(Request.QueryString("price_id")),
                                                    Convert.ToDecimal(Request.QueryString("price").ToString()), Val(Request.QueryString("size_id")),
                                                    Val(Request.QueryString("size").ToString()), Request.QueryString("ip").ToString(), Request.QueryString("Tran_Type").ToString(),
                                                    Request.QueryString("Unit").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Change_Password" Then
                str = objwebservices.Change_Password(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("login")), Request.QueryString("old_pass").ToString(), Request.QueryString("new_pass").ToString(), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Role_List" Then
                str = objwebservices.Role_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Countries_List" Then
                str = objwebservices.Countries_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "States_List" Then
                str = objwebservices.States_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Country_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Cities_List" Then
                str = objwebservices.Cities_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("State_Id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_By_CatId" Then
                str = objwebservices.Product_By_CatId(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("category_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Function_List" Then
                str = objwebservices.Function_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Till_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_List" Then
                str = objwebservices.Sales_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "TSales_List" Then
                str = objwebservices.TSales_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("sales_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Master" Then
                str = objwebservices.Sales_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")), Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "TSales_Master" Then
                str = objwebservices.TSales_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                Val(Request.QueryString("tsales_id")), Val(Request.QueryString("sales_id")), Val(Request.QueryString("login")), Val(Request.QueryString("sales_total_amount")),
                                                Val(Request.QueryString("product_id")), Val(Request.QueryString("sales_discount")), Val(Request.QueryString("quntity")),
                                                Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("venue_id")), Val(Request.QueryString("price")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("sales_net_amount")), Convert.ToDecimal(Request.QueryString("sales_tax_amount")), Convert.ToDecimal(Request.QueryString("sales_actual_price")), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Table_List" Then
                str = objwebservices.Table_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str.ToArray())
                Else
                    Response.Write("Invalid User Details ...!")
                End If


            ElseIf Request.QueryString("Function").ToString() = "Table_Master" Then
                str = objwebservices.Table_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                  Val(Request.QueryString("table_id").ToString()), Val(Request.QueryString("login").ToString()),
                                                  Request.QueryString("name").ToString(), Val(Request.QueryString("min_cover").ToString()),
                                                  Val(Request.QueryString("max_cover").ToString()), Val(Request.QueryString("is_active").ToString()),
                                                  Val(Request.QueryString("is_open").ToString()), Request.QueryString("ip").ToString(),
                                                  Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(),
                                                  Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(),
                                                  Val(Request.QueryString("machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Key_Map_List" Then
                str = objwebservices.Key_Map_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Location_id")), Val(Request.QueryString("Venue_id")), Val(Request.QueryString("Till_id")))  'Val(Request.QueryString("Venue_id")), Val(Request.QueryString("Till_id"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Group_By_KeyMapId" Then
                str = objwebservices.Product_Group_By_KeyMapId(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("key_map_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_By_KeyMapId" Then
                str = objwebservices.Product_By_KeyMapId(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("key_map_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Location_List" Then
                str = objwebservices.Location_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Venue_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Location_Master" Then
                str = objwebservices.Location_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("location_id").ToString()), Val(Request.QueryString("login").ToString()), Request.QueryString("name").ToString(), Request.QueryString("address").ToString(), Request.QueryString("code").ToString(), Val(Request.QueryString("city_id").ToString()), Val(Request.QueryString("state_id").ToString()), Val(Request.QueryString("country_id").ToString()), Request.QueryString("ip").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Request.QueryString("venue_id"), Request.QueryString("store_name").ToString(), Val(Request.QueryString("till_auto_log_off")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("is_active")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Machine_List" Then
                str = objwebservices.Machine_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Machine_Master" Then
                str = objwebservices.Machine_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("machine_id").ToString()), Val(Request.QueryString("login").ToString()), Request.QueryString("name").ToString(), Val(Request.QueryString("serial_no").ToString()), Request.QueryString("code").ToString(), Request.QueryString("mac_address").ToString(), Request.QueryString("model").ToString(), Request.QueryString("ip").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")), Val(Request.QueryString("is_assign")), Val(Request.QueryString("is_minipos")), Val(Request.QueryString("is_active")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Device_List" Then
                str = objwebservices.Device_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Device_Master" Then
                str = objwebservices.Device_Master(Request.QueryString("authU").ToString(),
                                                   Request.QueryString("authP").ToString(),
                                                   Val(Request.QueryString("cmp")),
                                                   Val(Request.QueryString("device_id").ToString()),
                                                   Val(Request.QueryString("machine_id").ToString()),
                                                   Val(Request.QueryString("login").ToString()),
                                                   Request.QueryString("name").ToString(),
                                                   Request.QueryString("serial_no").ToString(),
                                                   Request.QueryString("code").ToString(),
                                                   Request.QueryString("ip").ToString(),
                                                   Request.QueryString("Tran_Type").ToString(),
                                                   Request.QueryString("mac_id").ToString(),
                                                   Val(Request.QueryString("venue_id")),
                                                   Request.QueryString("store_name").ToString(),
                                                   Val(Request.QueryString("Device_Type_id")),
                                                   Val(Request.QueryString("is_active")),
                                                   Request.QueryString("printer_ip_address"),
                                                    Val(Request.QueryString("port")),
                                                    Request.QueryString("network_type").ToString(),
                                                    Val(Request.QueryString("vender_id")),
                                                    Request.QueryString("budrate").ToString(),
                                                    Request.QueryString("device_name").ToString(),
                                                    Val(Request.QueryString("width")),
                                                    Val(Request.QueryString("Printer_ID")),
                                                    Val(Request.QueryString("Detail_device_ID")),
                                                    Val(Request.QueryString("Detail_machine_ID")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_ProductGroup_keymapId" Then
                str = objwebservices.Product_ProductGroup_keymapId(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("key_map_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "SignIn" Then
                str = objwebservices.SignIn(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Till_code")),
                                           Request.QueryString("IP").ToString(), Request.QueryString("store_name").ToString(), Request.QueryString("Authentication_Code").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Upd_StaffImg" Then
                str = objwebservices.Upd_StaffImg(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Cmp")), Val(Request.QueryString("Staff_Id")), Request.QueryString("photo").ToString(), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Key_Map_Master" Then
                str = objwebservices.Key_Map_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("key_map_id").ToString()), Val(Request.QueryString("login").ToString()), Request.QueryString("name").ToString(), Request.QueryString("description").ToString(), Request.QueryString("ip").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("machine_id")), Val(Request.QueryString("is_active")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Key_Map_button_detail_update" Then
                str = objwebservices.Key_Map_button_detail_update(Request.QueryString("authU").ToString(),
                                                                  Request.QueryString("authP").ToString(),
                                                                  Val(Request.QueryString("cmp").ToString()),
                                                                  Val(Request.QueryString("key_map_detail_id").ToString()),
                                                                  Val(Request.QueryString("key_map_id").ToString()),
                                                                  Val(Request.QueryString("login").ToString()),
                                                                  Request.QueryString("ip").ToString(),
                                                                  Request.QueryString("Tran_Type").ToString(),
                                                                  Request.QueryString("store_name").ToString(),
                                                                  Val(Request.QueryString("is_active")),
                                                                  Val(Request.QueryString("Product_Group_id")),
                                                                  Val(Request.QueryString("Product_id")),
                                                                  Val(Request.QueryString("Size_id")),
                                                                  Request.QueryString("BG_Color").ToString(),
                                                                  Request.QueryString("FG_Color").ToString(), Request.QueryString("matrix").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then

                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_By_Date" Then
                str = objwebservices.Sales_By_Date(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Convert.ToDateTime(Request.QueryString("date1")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("venue_id")), Val(Request.QueryString("location_id")), Val(Request.QueryString("machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

                '                                       
            ElseIf Request.QueryString("Function").ToString() = "Tax_Master" Then
                str = objwebservices.Tax_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("Tax_id")), Val(Request.QueryString("login")), Request.QueryString("Name").ToString(), Request.QueryString("Mode").ToString(), Convert.ToDecimal(Request.QueryString("Value").ToString()), Convert.ToDateTime(Request.QueryString("Effective_Date")), Val(Request.QueryString("Is_active").ToString()), Request.QueryString("ip").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Tax_List" Then
                str = objwebservices.Tax_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Z_Report" Then
                str = objwebservices.Sales_Z_Report(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Convert.ToDateTime(Request.QueryString("date1")), Convert.ToDateTime(Request.QueryString("date2")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("venue_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Company_Details" Then
                str = objwebservices.Company_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Company_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "TSales_By_Date" Then
                str = objwebservices.TSales_By_Date(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Convert.ToDateTime(Request.QueryString("date1")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("venue_id")), Val(Request.QueryString("location_id")), Val(Request.QueryString("machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Company_List" Then
                str = objwebservices.Company_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If


            ElseIf Request.QueryString("Function").ToString() = "TSales_Master1" Then
                str = objwebservices.TSales_Master1(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("values"), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Table_Details" Then
                str = objwebservices.Table_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("table_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Machine_Details" Then
                str = objwebservices.Machine_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("machine_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Location_Details" Then
                str = objwebservices.Location_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("location_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "KeyMap_Details" Then
                str = objwebservices.KeyMap_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("keymap_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "KeyMap_Details_New" Then
                str = objwebservices.KeyMap_Details_New(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Location_id")), Val(Request.QueryString("Venue_id")), Val(Request.QueryString("Till_id")))  'Val(Request.QueryString("Venue_id")), Val(Request.QueryString("Till_id"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Device_Details" Then
                str = objwebservices.Device_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("device_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Settings" Then
                str = objwebservices.Settings(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Role_Wise_Menus" Then
                str = objwebservices.Role_Wise_Menus(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Role_Id")), Val(Request.QueryString("Type")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Role_Wise_Form" Then
                str = objwebservices.Role_Wise_Form(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Role_id")), IIf(Request.QueryString("Form_Name") = "", "NULL", Request.QueryString("Form_Name")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Venue_List" Then
                str = objwebservices.Venue_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Venue_Details" Then
                str = objwebservices.Venue_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                     Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Venue_Master" Then
                str = objwebservices.Venue_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                  Val(Request.QueryString("venue_id").ToString()), Request.QueryString("venue_name").ToString(),
                                                  Request.QueryString("description").ToString(), Request.QueryString("mac_id").ToString(),
                                                  Request.QueryString("Tran_Type").ToString(), Request.QueryString("store_name").ToString(),
                                                  Val(Request.QueryString("print_receipt")), Val(Request.QueryString("print_duplicate_receipt")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("is_active")), Val(Request.QueryString("login_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Type_List" Then
                str = objwebservices.Sales_Type_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Machine_Assign_Details" Then
                str = objwebservices.Machine_Assign_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                      Val(Request.QueryString("machine_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("is_assign")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Location_By_Venue" Then
                str = objwebservices.Location_By_Venue(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                      Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Machine_By_Location" Then
                str = objwebservices.Machine_By_Location(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                                      Val(Request.QueryString("location_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Prefix_Master" Then
                str = objwebservices.Prefix_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Prefix_id")), Request.QueryString("Prefix").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("store_name").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("login")), Request.QueryString("ip").ToString(), Val(Request.QueryString("is_active")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Prefix_List" Then
                str = objwebservices.Prefix_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Val(Request.QueryString("cmp")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Prefix_Details" Then
                str = objwebservices.Prefix_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Prefix_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("cmp")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Device_Type_List" Then
                str = objwebservices.Device_Type_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Val(Request.QueryString("cmp")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Device_Type_Details" Then
                str = objwebservices.Device_Type_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("Device_Type_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Device_Type_Master" Then
                str = objwebservices.Device_Type_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("Device_Type_id")), Request.QueryString("Device_Type").ToString(), Val(Request.QueryString("login_id")), Request.QueryString("ip_address").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("store_name").ToString(), Val(Request.QueryString("is_active")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Barcode_List" Then
                str = objwebservices.Barcode_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Val(Request.QueryString("cmp")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Barcode_Details" Then
                str = objwebservices.Barcode_Details(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("Barcode_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Barcode_Master" Then
                str = objwebservices.Barcode_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("Barcode_id")), Request.QueryString("Barcode").ToString(), Val(Request.QueryString("product_id")), Val(Request.QueryString("login_id")), Request.QueryString("ip_address").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Size_Id")), Val(Request.QueryString("is_active")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Barcode_By_Product" Then
                str = objwebservices.Barcode_By_Product(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("product_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Price_By_Size" Then
                str = objwebservices.Price_By_Size(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("Size_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Ingredient_List" Then
                str = objwebservices.Ingredient_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("product_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Condiment_List" Then
                str = objwebservices.Condiment_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Condiment_List" Then
                str = objwebservices.Product_Condiment_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("product_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Size_List" Then
                str = objwebservices.Size_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("product_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Payment_List" Then
                str = objwebservices.Payment_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("payment_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Printer_Detail_By_Product" Then
                str = objwebservices.Printer_Detail_By_Product(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("product_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Printer_Detail_By_Machine" Then
                str = objwebservices.Printer_Detail_By_Machine(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("machine_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Device_List_With_Payment" Then
                str = objwebservices.Device_List_With_Payment(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Till_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "T_payment_with_card" Then
                str = objwebservices.T_payment_with_card(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), (Request.QueryString("sale_id").ToString()), (Request.QueryString("card_holdername").ToString()),
                                                   (Request.QueryString("expiration_date")), Request.QueryString("pan").ToString(), Request.QueryString("cardtype").ToString(), Request.QueryString("amount").ToString(), Request.QueryString("type_ISO_currency_code").ToString(), Request.QueryString("date1").ToString(), Request.QueryString("status_message").ToString(),
                                                   (Request.QueryString("transaction_id").ToString()), (Request.QueryString("payment_account_data_token").ToString()), (Request.QueryString("retrieval_reference_number").ToString()), Request.QueryString("merchant_id").ToString(),
                                                    (Request.QueryString("terminal_id").ToString()))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full" Then
                str = objwebservices.Sales_Master_Full(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("values"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_payment_with_card" Then
                str = objwebservices.Sales_Master_Full_payment_with_card(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("values"), Request.QueryString("card_holdername"),
                                 Request.QueryString("expiration_date"), Request.QueryString("pan"), Request.QueryString("cardtype"), Request.QueryString("amount"), Request.QueryString("type_ISO_currency_code"), Request.QueryString("date1").ToString(), Request.QueryString("status_message"), Request.QueryString("transaction_id"),
                                 Request.QueryString("payment_account_data_token"), Request.QueryString("retrieval_reference_number"), Request.QueryString("merchant_id"), Request.QueryString("terminal_id"),
                                 Request.QueryString("card_method_type"), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_payment_with_card_Condiment" Then
                str = objwebservices.Sales_Master_Full_payment_with_card_Condiment(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("values"), Request.QueryString("card_holdername"),
                                 Request.QueryString("expiration_date"), Request.QueryString("pan"), Request.QueryString("cardtype"), Request.QueryString("amount"), Request.QueryString("type_ISO_currency_code"), Request.QueryString("date1").ToString(), Request.QueryString("status_message"), Request.QueryString("transaction_id"),
                                 Request.QueryString("payment_account_data_token"), Request.QueryString("retrieval_reference_number"), Request.QueryString("merchant_id"), Request.QueryString("terminal_id"),
                                 Request.QueryString("card_method_type"), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Reason_List" Then
                str = objwebservices.Reason_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Device_chg_password" Then
                str = objwebservices.Device_chg_password(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Request.QueryString("service_key").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "ResDiary_List" Then
                str = objwebservices.ResDiary_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "P_M_Store_version" Then
                str = objwebservices.P_M_Store_version(Request.QueryString("store_name").ToString(), Request.QueryString("version_no").ToString(), Request.QueryString("device_space").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "P_M_Sync_table" Then
                str = objwebservices.P_M_Sync_table(Request.QueryString("sync_id").ToString(), Request.QueryString("sales_id").ToString(), Request.QueryString("for_date").ToString(), Request.QueryString("cmp_id").ToString(),
                                                     Request.QueryString("store_name").ToString(), Request.QueryString("till_name").ToString(), Request.QueryString("venue_id").ToString(), Request.QueryString("Tran_Type").ToString(), Request.QueryString("machine_id").ToString(),
                                                     Request.QueryString("app_version").ToString(), Request.QueryString("db_version").ToString(), Request.QueryString("login_id").ToString(),
                                                     Request.QueryString("ip_address").ToString(), Request.QueryString("type").ToString(), Request.QueryString("description").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "P_M_App_info" Then
                str = objwebservices.P_M_App_info(Request.QueryString("App_info_id").ToString(), Request.QueryString("type").ToString(), Request.QueryString("for_date").ToString(), Request.QueryString("cmp_id").ToString(),
                                                     Request.QueryString("store_name").ToString(), Request.QueryString("till_name").ToString(), Request.QueryString("venue_id").ToString(), Request.QueryString("machine_id").ToString(),
                                                     Request.QueryString("app_version").ToString(), Request.QueryString("db_version").ToString(), Request.QueryString("login_id").ToString(),
                                                     Request.QueryString("ip_address").ToString(), Request.QueryString("Tran_Type").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "P_M_Store_expire_list" Then
                str = objwebservices.P_M_Store_expire_list(Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_Discount" Then
                str = objwebservices.Sales_Master_Full_Discount(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"), Request.QueryString("values"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_Discount_New" Then
                str = objwebservices.Sales_Master_Full_Discount_New(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"), Request.QueryString("values"), Val(Request.QueryString("table_created_machine_id")), Val(Request.QueryString("table_close_machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_Discount_Condiment" Then
                str = objwebservices.Sales_Master_Full_Discount_Condiment(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"), Request.QueryString("values"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_Discount_Condiment_RoomPayment" Then
                str = objwebservices.Sales_Master_Full_Discount_Condiment_RoomPayment(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"), Val(Request.QueryString("Room_Payment_Number")), Request.QueryString("Room_Payment_Name"), Request.QueryString("values"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_Discount_New_Condiment" Then
                str = objwebservices.Sales_Master_Full_Discount_New_Condiment(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"), Request.QueryString("values"), Val(Request.QueryString("table_created_machine_id")), Val(Request.QueryString("table_close_machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_Discount_New_Condiment_RoomPayment" Then
                str = objwebservices.Sales_Master_Full_Discount_New_Condiment_RoomPayment(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"), Request.QueryString("values"), Val(Request.QueryString("table_created_machine_id")), Val(Request.QueryString("table_close_machine_id")), Val(Request.QueryString("Room_Payment_Number")), Request.QueryString("Room_Payment_Name"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Full_payment_with_card_discount" Then
                str = objwebservices.Sales_Master_Full_payment_with_card_discount(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("values"), Request.QueryString("card_holdername"),
                                 Request.QueryString("expiration_date"), Request.QueryString("pan"), Request.QueryString("cardtype"), Request.QueryString("amount"), Request.QueryString("type_ISO_currency_code"), Request.QueryString("date1").ToString(), Request.QueryString("status_message"), Request.QueryString("transaction_id"),
                                 Request.QueryString("payment_account_data_token"), Request.QueryString("retrieval_reference_number"), Request.QueryString("merchant_id"), Request.QueryString("terminal_id"),
                                 Request.QueryString("card_method_type"), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Query_DATA_List" Then
                str = objwebservices.Query_DATA_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Till_id")), Request.QueryString("query_val").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Price_Master" Then
                str = objwebservices.Price_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Company_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Price_Id")),
                                                          Val(Request.QueryString("Price")), Val(Request.QueryString("Location_Id")), Val(Request.QueryString("Size_Id")), Val(Request.QueryString("Actual_Price")), Val(Request.QueryString("Tax")), Val(Request.QueryString("Product_id")), Request.QueryString("Ip_Address").ToString(), Val(Request.QueryString("Login_id")), Request.QueryString("Tran_Type").ToString(), Val(Request.QueryString("Tax_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Size_Master" Then
                str = objwebservices.Size_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Company_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Product_id")), Val(Request.QueryString("Size_Id")), Request.QueryString("Size_Unit").ToString(), Val(Request.QueryString("Size")), Request.QueryString("Name").ToString(), Request.QueryString("Unit").ToString(), Request.QueryString("Ip_Address").ToString(),
                                                           Val(Request.QueryString("Login_id")), Request.QueryString("Tran_Type").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Department_Master" Then
                str = objwebservices.Department_Master(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("Company_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("department_id")), Request.QueryString("name").ToString(), Request.QueryString("Ip_Address").ToString(), Val(Request.QueryString("Login_id")), Request.QueryString("Tran_Type").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Cash_Declaration" Then
                str = objwebservices.cash_Declaration(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("machine_id")), Convert.ToDateTime(Request.QueryString("Fordate").ToString()), Request.QueryString("Ip_Address").ToString(), Val(Request.QueryString("Login_id")), Val(Request.QueryString("amount").ToString()), Val(Request.QueryString("type").ToString()))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Cash_Declaration_Delete" Then
                str = objwebservices.cash_Declaration_delete(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("machine_id")), Convert.ToDateTime(Request.QueryString("Fordate").ToString()), Request.QueryString("Ip_Address").ToString(), Val(Request.QueryString("Login_id")), Val(Request.QueryString("amount").ToString()), Val(Request.QueryString("type").ToString()))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Self_Sales_List" Then
                str = objwebservices.Self_Sales_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Self_Sales_inactive" Then
                str = objwebservices.Self_Sales_inactive(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Sales_id")), (Request.QueryString("uuid")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Voucher_Range" Then
                str = objwebservices.Voucher_Range(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Email_Setting" Then
                str = objwebservices.Email_Setting(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Customer_Add" Then

                str = objwebservices.Customer_Add(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                  Request.QueryString("name").ToString(), Request.QueryString("mobile").ToString(), Request.QueryString("email").ToString(), Val(Request.QueryString("cust_id").ToString()),
                                                  Val(Request.QueryString("Is_credit")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Customer_Add_WithCardNumber" Then

                str = objwebservices.Customer_Add_WithCardNumber(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                  Request.QueryString("name").ToString(), Request.QueryString("mobile").ToString(), Request.QueryString("email").ToString(), Val(Request.QueryString("cust_id").ToString()),
                                                  Val(Request.QueryString("Is_credit")), Request.QueryString("CardNumber"),
                                                     Convert.ToDateTime(Request.QueryString("DateTimeExpiry")), Val(Request.QueryString("profile_id")), Val(Request.QueryString("price_level_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Account" Then
                str = objwebservices.Sales_Master_Account(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                 Val(Request.QueryString("sales_id").ToString()), Val(Request.QueryString("login")), Val(Request.QueryString("total_amount").ToString()),
                                 Val(Request.QueryString("total_discount").ToString()), Val(Request.QueryString("net_amount")), Request.QueryString("ip").ToString(), Convert.ToDecimal(Request.QueryString("change").ToString()), Convert.ToDecimal(Request.QueryString("tax").ToString()),
                                 Request.QueryString("Tran_Type").ToString(), Request.QueryString("mac_id").ToString(), Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(), Convert.ToDecimal(Request.QueryString("actual_total_amount")),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Val(Request.QueryString("is_return").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("mode").ToString()),
                                  Convert.ToDecimal(Request.QueryString("value")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")), Convert.ToDecimal(Request.QueryString("input_amount")), Val(Request.QueryString("sale_type")), Val(Request.QueryString("is_table")),
                                  Convert.ToDateTime(Request.QueryString("Payment_Date")), Convert.ToDecimal(Request.QueryString("Payment_Amount")), Request.QueryString("Table_name"), Val(Request.QueryString("is_close")), Request.QueryString("Discount_mode"), Request.QueryString("discount_name"), Val(Request.QueryString("Room_Payment_Number")), Request.QueryString("Room_Payment_Name"), Request.QueryString("values"), Val(Request.QueryString("cust_id")), Request.QueryString("cust_name"), Request.QueryString("cust_contact"), Request.QueryString("cust_email"), Convert.ToDecimal(Request.QueryString("used_bonus_Point")), Convert.ToDecimal(Request.QueryString("Used_bonus_amount")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Sales_Master_Account_Update" Then
                str = objwebservices.Sales_Master_Account_Update(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")),
                                   Val(Request.QueryString("ref_id")), Val(Request.QueryString("venue_id")), Request.QueryString("store_name").ToString(),
                                  Val(Request.QueryString("temp_sale_id").ToString()), Convert.ToDateTime(Request.QueryString("created_date")), Convert.ToDateTime(Request.QueryString("modify_date")), Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")),
                                     Val(Request.QueryString("cust_id")), Request.QueryString("cust_name"), Request.QueryString("cust_contact"), Request.QueryString("cust_email"), Convert.ToDecimal(Request.QueryString("used_bonus_Point")), Convert.ToDecimal(Request.QueryString("Used_bonus_amount")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_45" Then
                str = objwebservices.WS_P_M_Sales_ver_45(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_59" Then
                str = objwebservices.WS_P_M_Sales_ver_59(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_60" Then
                str = objwebservices.WS_P_M_Sales_ver_60(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Voucher_tran_add" Then
                str = objwebservices.Voucher_tran_add(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                  Val(Request.QueryString("sales_id")), Val(Request.QueryString("customer_id")),
                                                  Val(Request.QueryString("Amount")), Request.QueryString("VoucherRef_no").ToString(),
                                                  Convert.ToDateTime(Request.QueryString("tran_date")), Request.QueryString("ip_address").ToString(),
                                                  Request.QueryString("Tran_Type").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Get_Voucher_tran" Then
                str = objwebservices.Get_Voucher_tran(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                   Val(Request.QueryString("customer_id")), Request.QueryString("VoucherRef_no").ToString(), Request.QueryString("VoucherRef_TYPE").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Get_All_CustomerWithVoucher_Balance" Then
                str = objwebservices.Get_All_CustomerWithVoucher_Balance(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                      Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_63" Then
                str = objwebservices.WS_P_M_Sales_ver_63(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")))
                LogHelper.Error("POS_WS:CallMethod63():" + Val(Request.QueryString("ref_id")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Get_Sync_Request" Then
                str = objwebservices.Get_Sync_Request(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                   Val(Request.QueryString("Machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Shift_List" Then
                str = objwebservices.Shift_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                   Val(Request.QueryString("Machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Prices_List" Then
                str = objwebservices.Prices_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                   Val(Request.QueryString("Product_Price_Id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_64" Then
                str = objwebservices.WS_P_M_Sales_ver_64(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name"))
                )
                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("ref_id")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_65" Then

                str = objwebservices.WS_P_M_Sales_ver_65(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name"))
                )
                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("ref_id")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_651" Then

                str = objwebservices.WS_P_M_Sales_ver_651(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id"))
                )
                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_652" Then

                str = objwebservices.WS_P_M_Sales_ver_652(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID"))
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_653" Then

                str = objwebservices.WS_P_M_Sales_ver_653(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")))

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_67" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_67(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "credit_account_pay" Then
                str = objwebservices.credit_account_pay(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                   Val(Request.QueryString("customer_web_id")), Request.QueryString("customer_mobile_no").ToString(),
                                                   Val(Request.QueryString("Amount")), Convert.ToDateTime(Request.QueryString("credit_date")),
                                                   Val(Request.QueryString("machine_id")), Val(Request.QueryString("location_id")),
                                                   Request.QueryString("pay_uuid").ToString(), Request.QueryString("Tran_Type").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Customer_Credit_Balance" Then
                str = objwebservices.Customer_Credit_Balance(Request.QueryString("authU").ToString(),
                                                             Request.QueryString("authP").ToString(),
                                                             Request.QueryString("store_name").ToString(),
                                                             Val(Request.QueryString("cust_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "TableMaster_by_Location" Then
                str = objwebservices.TableMaster_by_Location(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "TableSalesMaster_by_Location" Then
                str = objwebservices.TableSalesMaster_by_Location(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")),
                                        Request.QueryString("table_uuid").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "TableTransaction_by_Location" Then
                str = objwebservices.TableTransaction_by_Location(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")),
                                        Request.QueryString("table_uuid").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "TableSalesTransaction_by_Location" Then
                str = objwebservices.TableSalesTransaction_by_Location(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")),
                                        Request.QueryString("table_uuid").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If


            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_68" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_68(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_69" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_69(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If


            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_691" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_691(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                         Request.QueryString("Original_table_UUID"),
                                          Request.QueryString("Transfered_Table_UUID")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_692" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_692(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                         Request.QueryString("Original_table_UUID"),
                                          Request.QueryString("Transfered_Table_UUID")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_693" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_693(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                         Request.QueryString("Original_table_UUID"),
                                          Request.QueryString("Transfered_Table_UUID"),
                                           Request.QueryString("Integrated_Terminal_ID"),
                                            Request.QueryString("Integrated_Merchant_ID"),
                                             Request.QueryString("Integrated_SaleType"),
                                              Request.QueryString("Integrated_Entry_Method")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_694" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_694(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                         Request.QueryString("Original_table_UUID"),
                                          Request.QueryString("Transfered_Table_UUID"),
                                           Request.QueryString("Integrated_Terminal_ID"),
                                            Request.QueryString("Integrated_Merchant_ID"),
                                             Request.QueryString("Integrated_SaleType"),
                                              Request.QueryString("Integrated_Entry_Method")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_695" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_695(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                    Val(Request.QueryString("VoucherBalance")),
                                    Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                         Request.QueryString("Original_table_UUID"),
                                          Request.QueryString("Transfered_Table_UUID"),
                                           Request.QueryString("Integrated_Terminal_ID"),
                                            Request.QueryString("Integrated_Merchant_ID"),
                                             Request.QueryString("Integrated_SaleType"),
                                              Request.QueryString("Integrated_Entry_Method"),
                                               Request.QueryString("Elina_Room_No")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If


            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_696" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_696(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_697" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_697(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_698" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_698(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No"),
                                         Val(Request.QueryString("TIP_AMOUNT"))
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_699" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_699(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No"),
                                         Val(Request.QueryString("TIP_AMOUNT")),
                                          Val(Request.QueryString("CardPayType"))
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_700" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_700(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No"),
                                         Val(Request.QueryString("TIP_AMOUNT")),
                                          Val(Request.QueryString("CardPayType")),
                                          Request.QueryString("KINETIC_REF_NO")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_701" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_701(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No"),
                                         Val(Request.QueryString("TIP_AMOUNT")),
                                          Val(Request.QueryString("CardPayType")),
                                          Request.QueryString("KINETIC_REF_NO")
                                        )

                LogHelper.Error("POS_WS:CallMethod64():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If




            ElseIf Request.QueryString("Function").ToString() = "ZReport_OperatorWise" Then
                str = objwebservices.ZReport_OperatorWise(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                   Convert.ToDateTime(Request.QueryString("F_date")),
                                                   Request.QueryString("login_id").ToString(), Val(Request.QueryString("location_id")),
                                                    Val(Request.QueryString("machine_id")), Val(Request.QueryString("venue_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "ZReport_BO_combine" Then
                str = objwebservices.ZReport_BO_combine(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Request.QueryString("store_name").ToString(),
                                                   Convert.ToDateTime(Request.QueryString("F_date")),
                                                   Request.QueryString("login_id").ToString(), Val(Request.QueryString("location_id")),
                                                    Val(Request.QueryString("machine_id")), Val(Request.QueryString("venue_id")), Request.QueryString("machine_list").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Customer_Profile" Then
                str = objwebservices.Customer_Profile(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Voucher_List" Then
                str = objwebservices.Voucher_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "IssueVoucher_Add" Then
                str = objwebservices.IssueVoucher_Add(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString(),
                                                      Request.QueryString("cust_id").ToString(), Request.QueryString("Voucher_id").ToString(), Val(Request.QueryString("deposit_amount")),
                                                      Request.QueryString("ref_no").ToString(), Request.QueryString("Voucher_duration").ToString(),
                                                      Convert.ToDateTime(Request.QueryString("start_date")), Convert.ToDateTime(Request.QueryString("end_date")), Request.QueryString("ip_address").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Unit_List" Then
                str = objwebservices.Unit_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "staff_rules_master_List" Then
                str = objwebservices.staff_rules_master_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "staff_rules_detail_List" Then
                str = objwebservices.staff_rules_detail_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Booking_seated_List" Then
                str = objwebservices.Booking_seated_List(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Booking_seated_Insert" Then
                str = objwebservices.Booking_seated_Insert(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString(), Request.QueryString("BookingRef_no").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "InOut_Insert" Then
                str = objwebservices.InOut_Insert(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("staff_id").ToString()), Val(Request.QueryString("login_id").ToString()),
                                                    Convert.ToDateTime(Request.QueryString("InOut_Datetime")), Val(Request.QueryString("IsInOut").ToString()), Request.QueryString("Type").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Buying_Size" Then
                str = objwebservices.Buying_Size(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Val(Request.QueryString("cmp")), Val(Request.QueryString("Product_id")), Request.QueryString("store_name").ToString(), Val(Request.QueryString("Location_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Stock_Take" Then
                str = objwebservices.Stock_Take(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(),
                Request.QueryString("ID").ToString(),
                Request.QueryString("Producte_id").ToString(),
                Request.QueryString("product_name").ToString(),
                Request.QueryString("Qty").ToString(),
                Request.QueryString("Receipt_no").ToString(),
                Request.QueryString("purchase_date").ToString(),
                Request.QueryString("location_id").ToString(),
                Request.QueryString("machine_id").ToString(),
                Request.QueryString("is_purchase").ToString(),
                Request.QueryString("buying_size_id").ToString(),
                Request.QueryString("buying_size").ToString(),
                Request.QueryString("buying_size_unit").ToString(),
                Request.QueryString("cost").ToString(),
                Request.QueryString("buying_size_unit_id").ToString(),
                Request.QueryString("selling_size").ToString(),
                Request.QueryString("selling_size_unit").ToString(),
                Request.QueryString("selling_size_unit_id").ToString(),
                Request.QueryString("base_size").ToString(),
                Request.QueryString("base_size_id").ToString(),
                Request.QueryString("base_size_unit_id").ToString(),
                Request.QueryString("cretaed_date").ToString(),
                Request.QueryString("modify_date").ToString(),
                Request.QueryString("user_id").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Add_Product_CRM" Then
                str = objwebservices.Add_Product_CRM(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Request.QueryString("GrpCat").ToString(), Request.QueryString("GrpName").ToString(), Request.QueryString("prdName").ToString(), Request.QueryString("Unit").ToString(), Request.QueryString("Base").ToString(), Request.QueryString("SizeN1").ToString(), Request.QueryString("SizeU1").ToString(), Request.QueryString("SizeQ1").ToString(), Request.QueryString("Price1").ToString(), Request.QueryString("SizeN2").ToString(), Request.QueryString("SizeU2").ToString(), Request.QueryString("SizeQ2").ToString(), Request.QueryString("Price2").ToString(), Request.QueryString("SizeN3").ToString(), Request.QueryString("SizeU3").ToString(), Request.QueryString("SizeQ3").ToString(), Request.QueryString("Price3").ToString(), Request.QueryString("SizeN4").ToString(), Request.QueryString("SizeU4").ToString(), Request.QueryString("SizeQ4").ToString(), Request.QueryString("Price4").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Add_User_CRM" Then
                str = objwebservices.Add_User_CRM(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Request.QueryString("Username").ToString(), Request.QueryString("password").ToString(), Request.QueryString("email").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Voucher_Validate" Then
                str = objwebservices.Voucher_Validate(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Request.QueryString("VoucherCode").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Voucher_Used" Then
                str = objwebservices.Voucher_Used(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Request.QueryString("VoucherCode").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "PaybyLink_Payment" Then
                str = objwebservices.PaybyLink_Payment(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Val(Request.QueryString("machine_id").ToString()))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "PaybyLink_Payment_update" Then
                str = objwebservices.PaybyLink_Payment_update(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(), Request.QueryString("store_name").ToString(), Request.QueryString("payUUID").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_702" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_702(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No"),
                                         Val(Request.QueryString("TIP_AMOUNT")),
                                          Val(Request.QueryString("CardPayType")),
                                          Request.QueryString("KINETIC_REF_NO"),
                                          Request.QueryString("pay_uuid")
                                        )

                LogHelper.Error("POS_WS:CallMethod65():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_703" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                str = objwebservices.WS_P_M_Sales_ver_703(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(Request.QueryString("created_date")),
                                          Convert.ToDateTime(Request.QueryString("modify_date")),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No"),
                                         Val(Request.QueryString("TIP_AMOUNT")),
                                          Val(Request.QueryString("CardPayType")),
                                          Request.QueryString("KINETIC_REF_NO"),
                                          Request.QueryString("pay_uuid"),
                                          Request.QueryString("No_OfCover")
                                        )

                LogHelper.Error("POS_WS:CallMethod65():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If



            ElseIf Request.QueryString("Function").ToString() = "WS_P_M_Sales_ver_704" Then
                Dim deliverydate As DateTime
                If Not Request.QueryString("Cust_delivery_date") = "NULL" Then
                    deliverydate = Request.QueryString("Cust_delivery_date")
                End If
                Dim createdDate As DateTime
                Dim modifyDate As DateTime
                Dim paymentDate As DateTime = Convert.ToDateTime(Request.QueryString("Payment_Date"))

                ' Check and assign the dates
                If Request.QueryString("created_date") = "-1" Then
                    createdDate = paymentDate
                Else
                    createdDate = Convert.ToDateTime(Request.QueryString("created_date"))
                End If

                If Request.QueryString("modify_date") = "-1" Then
                    modifyDate = paymentDate
                Else
                    modifyDate = Convert.ToDateTime(Request.QueryString("modify_date"))
                End If

                str = objwebservices.WS_P_M_Sales_ver_704(Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Val(Request.QueryString("cmp")),
                                         Val(Request.QueryString("sales_id").ToString()),
                                         Val(Request.QueryString("login")),
                                         Val(Request.QueryString("total_amount").ToString()),
                                         Val(Request.QueryString("total_discount").ToString()),
                                         Val(Request.QueryString("net_amount")),
                                         Request.QueryString("ip").ToString(),
                                         Val(Request.QueryString("change").ToString()),
                                         Val(Request.QueryString("tax").ToString()),
                                         Request.QueryString("Tran_Type").ToString(),
                                         Request.QueryString("mac_id").ToString(),
                                         Val(Request.QueryString("ref_id")),
                                         Val(Request.QueryString("venue_id")),
                                         Request.QueryString("store_name").ToString(),
                                         Val(Request.QueryString("actual_total_amount")),
                                          Val(Request.QueryString("temp_sale_id").ToString()),
                                          Val(Request.QueryString("is_return").ToString()),
                                          Convert.ToDateTime(createdDate),
                                          Convert.ToDateTime(modifyDate),
                                          Val(Request.QueryString("mode").ToString()),
                                          Val(Request.QueryString("value")),
                                          Val(Request.QueryString("machine_id")),
                                          Val(Request.QueryString("location_id")),
                                          Val(Request.QueryString("input_amount")),
                                          Val(Request.QueryString("sale_type")),
                                          Val(Request.QueryString("is_table")),
                                          Convert.ToDateTime(Request.QueryString("Payment_Date")),
                                          Val(Request.QueryString("Payment_Amount")),
                                          Request.QueryString("Table_name"),
                                          Val(Request.QueryString("is_close")),
                                          Request.QueryString("Discount_mode"),
                                          Request.QueryString("discount_name"),
                                          Val(Request.QueryString("Room_Payment_Number")),
                                          Request.QueryString("Room_Payment_Name"),
                                          Request.QueryString("values"),
                                          Val(Request.QueryString("cust_id")),
                                          Request.QueryString("cust_name"),
                                          Request.QueryString("cust_contact"),
                                          Request.QueryString("cust_email"),
                                          Val(Request.QueryString("used_bonus_Point")),
                                          Val(Request.QueryString("Used_bonus_amount")),
                                          Val(Request.QueryString("table_created_machine_id")),
                                          Val(Request.QueryString("table_close_machine_id")),
                                          Request.QueryString("card_holdername"),
                                         Request.QueryString("expiration_date"),
                                         Request.QueryString("pan"),
                                         Request.QueryString("cardtype"),
                                         Request.QueryString("amount"),
                                         Request.QueryString("type_ISO_currency_code"),
                                         Request.QueryString("date1").ToString(),
                                         Request.QueryString("status_message"),
                                         Request.QueryString("transaction_id"),
                                         Request.QueryString("payment_account_data_token"),
                                         Request.QueryString("retrieval_reference_number"),
                                         Request.QueryString("merchant_id"),
                                         Request.QueryString("terminal_id"),
                                         Request.QueryString("card_method_type"),
                                         Val(Request.QueryString("surcharge_amount")),
                                         Val(Request.QueryString("surcharge_value")),
                                         Request.QueryString("surcharge_name"),
                                         Val(Request.QueryString("GrandTotal")),
                                         Val(Request.QueryString("EatOutAmount")),
                                        Val(Request.QueryString("TotalCOver")),
                                        Val(Request.QueryString("VoucherId")),
                                        Request.QueryString("VocherCode"),
                                        Val(Request.QueryString("VoucherBalance")),
                                        Val(Request.QueryString("transaction_count")),
                                        (Request.QueryString("shift_name")),
                                        Val(Request.QueryString("SALE_NEWTABLE")),
                                        Val(Request.QueryString("Table_id")),
                                        (Request.QueryString("TRANS_UUID")),
                                        (Request.QueryString("TABLE_UUID")),
                                        Request.QueryString("IS_Delivery"),
                                         Convert.ToDateTime(Request.QueryString("Cust_delivery_date")),
                                        Request.QueryString("Cust_delivery_time"),
                                        Request.QueryString("Cust_delivery_Addr"),
                                        Request.QueryString("sales_sub_type"),
                                        Request.QueryString("Is_Email"),
                                        Request.QueryString("Original_table_UUID"),
                                        Request.QueryString("Transfered_Table_UUID"),
                                        Request.QueryString("Integrated_Terminal_ID"),
                                        Request.QueryString("Integrated_Merchant_ID"),
                                        Request.QueryString("Integrated_SaleType"),
                                        Request.QueryString("Integrated_Entry_Method"),
                                        Request.QueryString("Elina_Room_No"),
                                         Val(Request.QueryString("TIP_AMOUNT")),
                                          Val(Request.QueryString("CardPayType")),
                                          Request.QueryString("KINETIC_REF_NO"),
                                          Request.QueryString("pay_uuid"),
                                          Request.QueryString("No_OfCover"),
                                          Request.QueryString("NON_TABLE_PART_PAYMENT")
                                        )

                LogHelper.Error("POS_WS:CallMethod65():" + Val(Request.QueryString("SALE_NEWTABLE")).ToString() + " :: " + str.ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If



            ElseIf Request.QueryString("Function").ToString() = "GetCommunicator_Request" Then
                str = objwebservices.GetCommunicator_Request(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                   Val(Request.QueryString("location_id")), Request.QueryString("store_name").ToString(), Request.QueryString("TillID"))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "GetCommunicator_Request_Confirmation" Then
                str = objwebservices.GetCommunicator_Request_Confirmation(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                   Val(Request.QueryString("location_id")), Request.QueryString("store_name").ToString(), Request.QueryString("pay_uuid"), Val(Request.QueryString("status")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If


            ElseIf Request.QueryString("Function").ToString() = "Communicator_Response" Then
                str = objwebservices.Communicator_Response(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                   Val(Request.QueryString("location_id")), Request.QueryString("store_name").ToString(), Request.QueryString("paymentRef").ToString(), Request.QueryString("pay_uuid"),
                                                    Request.QueryString("status").ToString(), Request.QueryString("order_ref").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

                'ByVal authUsername As String, ByVal authPassword As String, ByVal location_id As Integer,
                '          ByVal store_name As String, ByVal paymentRef As String, ByVal pay_uuid As String, ByVal status As Integer, ByVal order_ref As String



                '--------------------------------------------------------------------------------------------------------------------------------------------
            ElseIf Request.QueryString("Function").ToString() = "AddDPT_Lite" Then

                str = objwebservices.AddDPT_Lite(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                   Request.QueryString("store_name").ToString(), Request.QueryString("department_name").ToString(), Val(Request.QueryString("department_id")), Val(Request.QueryString("is_product")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Communicator_transactionData" Then
                str = objwebservices.Communicator_transactionData(Request.QueryString("authU").ToString(), Request.QueryString("authP").ToString(),
                                                  Request.QueryString("store_name").ToString(), Val(Request.QueryString("location_id")), Val(Request.QueryString("id")), Request.QueryString("pay_uuid").ToString,
                                                    Request.QueryString("TOTAL_AMOUNT").ToString(), Request.QueryString("LANGUAGE").ToString(), Val(Request.QueryString("TABLE_MASTER_REF_ID")), Request.QueryString("TRAN_UUID").ToString(), Request.QueryString("TABLE_UUID").ToString(),
                                                    Val(Request.QueryString("CREATED_USER_ID")), Val(Request.QueryString("MACHINE_ID")), Val(Request.QueryString("IS_SYNC")), Val(Request.QueryString("IS_REPLAY")), Convert.ToDateTime(Request.QueryString("CREATED_DATE")),
                                                    DateTime.Parse(Request.QueryString("MODIFIED_DATE")), Val(Request.QueryString("PAX_POS_ID")), Request.QueryString("IP_ADDRESS").ToString(), Val(Request.QueryString("IS_COMPLETE_TASK")), Request.QueryString("STATUS").ToString(),
                                                    Val(Request.QueryString("IS_PROCESSING")), Request.QueryString("PAX_UUID").ToString(), Val(Request.QueryString("IS_WEBOR_POS")), Request.QueryString("Payment_Method").ToString(), Request.QueryString("transactionType").ToString(), Val(Request.QueryString("is_lastdata")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Company_Master" Then
                str = objwebservices.Company_Master(Request.QueryString("authU").ToString(),
                                                    Request.QueryString("authP").ToString(),
                                                    Val(Request.QueryString("Company_id")),
                                                    Request.QueryString("store_name").ToString(),
                                                    Request.QueryString("Name").ToString(),
                                                    Request.QueryString("Domain").ToString(),
                                                    Request.QueryString("Description").ToString(),
                                                    Request.QueryString("Receipt_Header").ToString(),
                                                    Request.QueryString("Receipt_Footer").ToString(),
                                                    Request.QueryString("Venue_Name").ToString(),
                                                    Convert.ToDecimal(Request.QueryString("log_off_time")),
                                                    Convert.ToDecimal(Request.QueryString("par_sale_par_operator")),
                                                    Request.QueryString("Website").ToString(),
                                                    Request.QueryString("Email").ToString(),
                                                    Request.QueryString("Contact").ToString(),
                                                    Request.QueryString("Vat_No").ToString(),
                                                    Request.QueryString("Address").ToString(),
                                                    Convert.ToDateTime(Request.QueryString("Starting_Date")),
                                                    Request.QueryString("IP_Address").ToString(),
                                                    Val(Request.QueryString("Country")),
                                                    Val(Request.QueryString("State")),
                                                    Val(Request.QueryString("City")),
                                                    Request.QueryString("Code").ToString(),
                                                    Request.QueryString("Postal").ToString(),
                                                    Val(Request.QueryString("Registration_no")),
                                                    Val(Request.QueryString("GST_VAT")),
                                                    Val(Request.QueryString("CST_VAT")),
                                                    Val(Request.QueryString("Service_tax_no")),
                                                    Val(Request.QueryString("Pan_no")),
                                                    Request.QueryString("Branch_Name").ToString(),
                                                    Request.QueryString("Synchronization").ToString(),
                                                    Val(Request.QueryString("Currency_id")),
                                                    Request.QueryString("Store_UUID").ToString(),
                                                    Convert.ToDateTime(Request.QueryString("Created_date")),
                                                    Convert.ToDateTime(Request.QueryString("Modify_Date")),
                                                    Val(Request.QueryString("IsAddTax2")),
                                                    Val(Request.QueryString("IsExclusiveTax")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Venue_List_set" Then
                str = objwebservices.Venue_List_set(Request.QueryString("authU").ToString(),
                                                    Request.QueryString("authP").ToString(),
                                                    Request.QueryString("store_name").ToString(),
                                                    Val(Request.QueryString("venue_id")),
                                                    Request.QueryString("venue_name").ToString(),
                                                    Request.QueryString("description").ToString(),
                                                    Val(Request.QueryString("sorting_no")),
                                                    Val(Request.QueryString("cmp_id")),
                                                    Val(Request.QueryString("mac_id")),
                                                    Convert.ToDateTime(Request.QueryString("Created_date")),
                                                    Convert.ToDateTime(Request.QueryString("Modify_Date")),
                                                    Val(Request.QueryString("print_receipt")),
                                                    Val(Request.QueryString("print_duplicate_receipt")),
                                                    Val(Request.QueryString("machine_id")),
                                                    Val(Request.QueryString("is_active")),
                                                    Val(Request.QueryString("login_id")),
                                                    Val(Request.QueryString("group_by")),
                                                    Val(Request.QueryString("group_by_with")),
                                                    Val(Request.QueryString("consile_date")),
                                                    Convert.ToDateTime(Request.QueryString("date_time")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Function_List_Set" Then
                str = objwebservices.Function_List_Set(Request.QueryString("authU").ToString(),
                                                    Request.QueryString("authP").ToString(),
                                                    Request.QueryString("store_name").ToString(),
                                                   Val(Request.QueryString("cmp_id")),
                                                    Val(Request.QueryString("function_id")),
                                                    Request.QueryString("name").ToString(),
                                                    Request.QueryString("code").ToString(),
                                                    Request.QueryString("caption_type").ToString(),
                                                    Val(Request.QueryString("is_active")),
                                                    Val(Request.QueryString("shorting_no")),
                                                    Request.QueryString("ip_address").ToString(),
                                                    Val(Request.QueryString("login_id")),
                                                    Val(Request.QueryString("item")),
                                                    Request.QueryString("back_color").ToString(),
                                                    Request.QueryString("for_color").ToString(),
                                                    Val(Request.QueryString("machine_id")),
                                                    Val(Request.QueryString("big_button")),
                                                    Val(Request.QueryString("payment_id")),
                                                    Val(Request.QueryString("is_groupby_course")),
                                                    Val(Request.QueryString("is_groupby_dept")),
                                                    Val(Request.QueryString("dept_id")),
                                                    Val(Request.QueryString("course_id")),
                                                    Val(Request.QueryString("Parent_Id")),
                                                    Request.QueryString("platformAdd").ToString(),
                                                    Request.QueryString("clientToken").ToString(),
                                                    Request.QueryString("accessToken").ToString(),
                                                    Request.QueryString("EOHelpOut_max_amount_each").ToString(),
                                                    Val(Request.QueryString("serviceid")),
                                                    Val(Request.QueryString("Total_Value")),
                                                    Val(Request.QueryString("Tax_Amount")),
                                                    Val(Request.QueryString("Sales_Handling_Fee")),
                                                    Val(Request.QueryString("Payment_Handling_Fee")),
                                                    Val(Request.QueryString("Def_Profile_Id")),
                                                    Request.QueryString("Default_Exp_date").ToString(),
                                                    Val(Request.QueryString("ZR_VenueID")),
                                                    Val(Request.QueryString("ZR_LocationID")),
                                                    Val(Request.QueryString("ZR_TillID")),
                                                    Val(Request.QueryString("CardPayType")),
                                                    Convert.ToDateTime(Request.QueryString("Created_date")),
                                                    Convert.ToDateTime(Request.QueryString("Modify_Date")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Machine_List_Set" Then
                str = objwebservices.Machine_List_Set(
                                                    Request.QueryString("authU").ToString(),
                                                    Request.QueryString("authP").ToString(),
                                                    Request.QueryString("store_name").ToString(),
                                                    Val(Request.QueryString("cmp_id")),
                                                    Request.QueryString("name"),
                                                    Val(Request.QueryString("machine_id")),
                                                    Request.QueryString("serial_no").ToString(),
                                                    Request.QueryString("mac_address").ToString(),
                                                    Request.QueryString("model").ToString(),
                                                    Request.QueryString("code").ToString(),
                                                    Convert.ToDateTime(Request.QueryString("created_date")),
                                                    Convert.ToDateTime(Request.QueryString("modify_date")),
                                                    Request.QueryString("ip_address").ToString(),
                                                    Val(Request.QueryString("login_id")),
                                                    Val(Request.QueryString("is_assign")),
                                                    Val(Request.QueryString("location_id")),
                                                    Val(Request.QueryString("is_minipos")),
                                                    Val(Request.QueryString("Active")),
                                                    Request.QueryString("Receipt_Header").ToString(),
                                                    Request.QueryString("Receipt_Footer").ToString(),
                                                    Val(Request.QueryString("is_master")),
                                                    Request.QueryString("tillip_address").ToString(),
                                                    Request.QueryString("till_server").ToString(),
                                                    Val(Request.QueryString("table_sharing")),
                                                    Val(Request.QueryString("printer_sharing")),
                                                    Request.QueryString("sync_time"),
                                                    Val(Request.QueryString("key_back_to_main")),
                                                    Convert.ToDecimal(Request.QueryString("ExtraSurcharge")),
                                                    Val(Request.QueryString("Only_table_trans")),
                                                    Val(Request.QueryString("AutoSurcharge")),
                                                    Val(Request.QueryString("AutoSurchargeTables")),
                                                    Val(Request.QueryString("AutoSurchargeNonTables")),
                                                    Val(Request.QueryString("AutoSurchargeCloakroom")),
                                                    Val(Request.QueryString("AutoSurchargeChips")),
                                                    Val(Request.QueryString("Sync_Request")),
                                                    Val(Request.QueryString("Service_Controller_Start")),
                                                    Val(Request.QueryString("Service_Websale_print")),
                                                    Val(Request.QueryString("Service_Print_Share")),
                                                    Val(Request.QueryString("Service_print_Share_Other_Till")),
                                                    Val(Request.QueryString("Is_NoLogout")),
                                                    Val(Request.QueryString("Is_PrintServer")),
                                                    Val(Request.QueryString("Is_ServiceBooking")),
                                                    Val(Request.QueryString("Is_OnlineZreport")),
                                                    Val(Request.QueryString("NoCashDrawer")),
                                                    Val(Request.QueryString("IsKiosk")),
                                                    Val(Request.QueryString("TblTranLimit")),
                                                    Val(Request.QueryString("QuickTran")),
                                                    Val(Request.QueryString("kitchenPrint")),
                                                    Val(Request.QueryString("ReceiptPrint")),
                                                    Val(Request.QueryString("ElinaTran")),
                                                    Val(Request.QueryString("is_POSLite"))
                                                )
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_List_Set" Then
                str = objwebservices.Product_List_Set(
                                                    Request.QueryString("authU").ToString(),
                                                    Request.QueryString("authP").ToString(),
                                                    Request.QueryString("store_name").ToString(),
                                                    Val(Request.QueryString("cmp_id")),
                                                    Val(Request.QueryString("product_id")),
                                                    Val(Request.QueryString("category_id")),
                                                    Request.QueryString("code").ToString(),
                                                    Request.QueryString("name").ToString(),
                                                    Val(Request.QueryString("price")),
                                                    Request.QueryString("barcode").ToString(),
                                                    Request.QueryString("description").ToString(),
                                                    Convert.ToDateTime(Request.QueryString("created_date")),
                                                    Convert.ToDateTime(Request.QueryString("modify_date")),
                                                    Val(Request.QueryString("is_active")),
                                                    Request.QueryString("ip_address").ToString(),
                                                    Val(Request.QueryString("login_id")),
                                                    Val(Request.QueryString("department_id")),
                                                    Val(Request.QueryString("course_id")),
                                                    Val(Request.QueryString("list")),
                                                    Val(Request.QueryString("printer_id")),
                                                    Val(Request.QueryString("key_map_id")),
                                                    Val(Request.QueryString("Actual_Price")),
                                                    Val(Request.QueryString("Tax")),
                                                    Val(Request.QueryString("Tax_id")),
                                                    Val(Request.QueryString("other_id")),
                                                    Val(Request.QueryString("other_size")),
                                                    Val(Request.QueryString("Base")),
                                                    Val(Request.QueryString("Unit_id")),
                                                    Val(Request.QueryString("size_zero")),
                                                    Val(Request.QueryString("Is_Condiment")),
                                                    Val(Request.QueryString("Cloak_Room")),
                                                    Val(Request.QueryString("Is_DanceVoucher")),
                                                    Val(Request.QueryString("By_Weight")),
                                                    Val(Request.QueryString("IsHouse")),
                                                    Val(Request.QueryString("SortingNo")),
                                                    Val(Request.QueryString("IsPkgProduct")),
                                                    Val(Request.QueryString("ForKiosk")),
                                                    Val(Request.QueryString("Is_OutOfStock")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "ProductGroup_List_Set" Then
                str = objwebservices.ProductGroup_List_Set(
                                                    Request.QueryString("authU").ToString(),
                                                    Request.QueryString("authP").ToString(),
                                                    Request.QueryString("store_name").ToString(),
                                                    Val(Request.QueryString("cmp_id")),
                                                    Val(Request.QueryString("group_id")),
                                                    Request.QueryString("name").ToString(),
                                                    Request.QueryString("description").ToString(),
                                                    Convert.ToDateTime(Request.QueryString("created_date")),
                                                    Convert.ToDateTime(Request.QueryString("modify_date")),
                                                    Val(Request.QueryString("is_active")),
                                                    Request.QueryString("ip_address").ToString(),
                                                    Val(Request.QueryString("login_id")),
                                                    Val(Request.QueryString("key_map_id")),
                                                    Val(Request.QueryString("sorting_no")),
                                                    Val(Request.QueryString("maincategory_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Price_By_Size_Set" Then
                str = objwebservices.Price_By_Size_Set(
                                                    Request.QueryString("authU").ToString(),
                                                    Request.QueryString("authP").ToString(),
                                                    Request.QueryString("store_name").ToString(),
                                                    Val(Request.QueryString("cmp_id")),
                                                    Val(Request.QueryString("Product_id")),
                                                    Val(Request.QueryString("Price")),
                                                    Val(Request.QueryString("Price_Id")),
                                                    Val(Request.QueryString("Actual_Price")),
                                                    Val(Request.QueryString("Tax")),
                                                    Val(Request.QueryString("Location_Id")),
                                                    Val(Request.QueryString("Size_Id")),
                                                    Val(Request.QueryString("tax_id")),
                                                    Val(Request.QueryString("Product_Price_Id")),
                                                    Val(Request.QueryString("Tax2")),
                                                    Val(Request.QueryString("Tax_id2")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Size_List_Set" Then
                str = objwebservices.Size_List_Set(
                                                        Request.QueryString("authU").ToString(),
                                                        Request.QueryString("authP").ToString(),
                                                        Request.QueryString("store_name").ToString(),
                                                        Request.QueryString("Name").ToString(),
                                                        Val(Request.QueryString("product_id")),
                                                        Val(Request.QueryString("Size_Id")),
                                                        Val(Request.QueryString("Size")),
                                                        Request.QueryString("Unit").ToString(),
                                                        Val(Request.QueryString("active")),
                                                        Val(Request.QueryString("sorting_no")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Key_Map_List_Set" Then
                str = objwebservices.Key_Map_List_Set(Request.QueryString("authU").ToString(),
                                                        Request.QueryString("authP").ToString(),
                                                        Request.QueryString("store_name").ToString(),
                                                        Val(Request.QueryString("cmp_id")),
                                                         Val(Request.QueryString("key_map_id")),
                                                        Val(Request.QueryString("shorting_no")),
                                                        Request.QueryString("description").ToString(),
                                                        Val(Request.QueryString("is_active")),
                                                        Request.QueryString("Font_Color").ToString(),
                                                        Request.QueryString("BG_Color").ToString(),
                                                        Request.QueryString("Display_Name").ToString(),
                                                        Request.QueryString("ButtonStyle").ToString(),
                                                        Request.QueryString("ImageStyle").ToString(),
                                                        Request.QueryString("ImageOption").ToString(),
                                                        Val(Request.QueryString("login_id")),
                                                        Val(Request.QueryString("machine_id")),
                                                        Val(Request.QueryString("venue_id")),
                                                        Val(Request.QueryString("Location_id")),
                                                        Request.QueryString("Keymap_size").ToString(),
                                                        Convert.ToDateTime(Request.QueryString("created_date")),
                                                    Convert.ToDateTime(Request.QueryString("modify_date")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Printer_List_Set" Then
                str = objwebservices.Printer_List_Set(
                                                        Request.QueryString("authU").ToString(),
                                                        Request.QueryString("authP").ToString(),
                                                        Request.QueryString("store_name").ToString(),
                                                        Val(Request.QueryString("cmp_id")),
                                                          Val(Request.QueryString("printer_id")),
                                                        Request.QueryString("name").ToString(),
                                                        Request.QueryString("Alias").ToString(),
                                                        Val(Request.QueryString("is_active")),
                                                        Request.QueryString("ip_address").ToString(),
                                                        Val(Request.QueryString("login_id")),
                                                        Val(Request.QueryString("machine_id")),
                                                        Request.QueryString("printer_ip_address").ToString(),
                                                        Val(Request.QueryString("is_Default")),
                                                        Request.QueryString("network_type").ToString(),
                                                        Request.QueryString("budrate").ToString(),
                                                        Val(Request.QueryString("vender_id")),
                                                        Request.QueryString("device_name").ToString(),
                                                        Val(Request.QueryString("is_product_small_large")),
                                                        Val(Request.QueryString("group_by")),
                                                        Val(Request.QueryString("group_by_with")),
                                                        Val(Request.QueryString("consile_date")),
                                                        Val(Request.QueryString("port")),
                                                        Val(Request.QueryString("OrderFlag")),
                                                        Convert.ToDateTime(Request.QueryString("created_date")),
                                                        Convert.ToDateTime(Request.QueryString("modify_date")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "GroupCategory_List_Set" Then
                str = objwebservices.GroupCategory_List_Set(
                                                        Request.QueryString("authU").ToString(),
                                                        Request.QueryString("authP").ToString(),
                                                        Request.QueryString("store_name").ToString(),
                                                         Val(Request.QueryString("maincategory_id")),
                                                        Request.QueryString("name").ToString(),
                                                        Val(Request.QueryString("sorting_no")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Location_List_Set" Then
                str = objwebservices.Location_List_Set(
                                                        Request.QueryString("authU").ToString(),
                                                        Request.QueryString("authP").ToString(),
                                                        Request.QueryString("store_name").ToString(),
                                                        Val(Request.QueryString("location_id")),
                                                        Val(Request.QueryString("Country_id")),
                                                        Val(Request.QueryString("City_ID")),
                                                        Val(Request.QueryString("State_Id")),
                                                        Request.QueryString("name").ToString(),
                                                        Request.QueryString("address").ToString(),
                                                        Request.QueryString("code").ToString(),
                                                        Request.QueryString("City").ToString(),
                                                        Request.QueryString("State").ToString(),
                                                        Request.QueryString("Country").ToString(),
                                                        Request.QueryString("ip_address").ToString(),
                                                        Val(Request.QueryString("cmp_id")),
                                                        Convert.ToDateTime(Request.QueryString("created_date")),
                                                        Convert.ToDateTime(Request.QueryString("modify_date")),
                                                        Val(Request.QueryString("login_id")),
                                                        Request.QueryString("venue_name").ToString(),
                                                        Val(Request.QueryString("venue_id")),
                                                        Val(Request.QueryString("till_auto_log_off")),
                                                        Val(Request.QueryString("Active")),
                                                        Val(Request.QueryString("is_email")),
                                                        Val(Request.QueryString("betweenSlot")),
                                                        Val(Request.QueryString("Is_Email_APK")),
                                                        Request.QueryString("Table_As_Box").ToString())

                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "User_List_Set" Then
                str = objwebservices.User_List_Set(Request.QueryString("authU").ToString(),
                                                    Request.QueryString("authP").ToString(),
                                                    Request.QueryString("store_name").ToString(),
                                                    Val(Request.QueryString("Cmp_id")),
                                                    Val(Request.QueryString("staff_id")),
                                                    Request.QueryString("staff_code").ToString(),
                                                    Request.QueryString("name").ToString(),
                                                    Convert.ToDateTime(Request.QueryString("joining_date")),
                                                    Request.QueryString("address").ToString(),
                                                    Val(Request.QueryString("country_id")),
                                                    Val(Request.QueryString("state_id")),
                                                    Val(Request.QueryString("city_id")),
                                                    Request.QueryString("national_id").ToString(),
                                                    Request.QueryString("contact_no").ToString(),
                                                    Request.QueryString("email").ToString(),
                                                    Convert.ToDateTime(Request.QueryString("last_working_date")),
                                                    Val(Request.QueryString("role_id")),
                                                    Convert.ToDateTime(Request.QueryString("created_date")),
                                                    Convert.ToDateTime(Request.QueryString("modify_date")),
                                                    Val(Request.QueryString("is_active")),
                                                    Val(Request.QueryString("User_Login")),
                                                    Request.QueryString("ip_address").ToString(),
                                                    Request.QueryString("till_code").ToString(),
                                                    Val(Request.QueryString("till_active")),
                                                    Request.QueryString("other_id").ToString(),
                                                    Request.QueryString("Username").ToString(),
                                                    Request.QueryString("Password").ToString(),
                                                    Request.QueryString("PAlias").ToString(),
                                                    Val(Request.QueryString("machine_id")),
                                                    Request.QueryString("Authentication_Code").ToString(),
                                                    Val(Request.QueryString("is_trainee")),
                                                    Val(Request.QueryString("m_staff_id"))
)

                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Prices_List_set" Then
                str = objwebservices.Prices_List_set(
                                                         Request.QueryString("authU").ToString(),
                                                Request.QueryString("authP").ToString(),
                                                Request.QueryString("store_name").ToString(),
                                                Val(Request.QueryString("Cmp_id")),
                                                Val(Request.QueryString("Product_Price_Id")),
                                                Request.QueryString("Name").ToString(),
                                                Val(Request.QueryString("Is_Active")),
                                                Val(Request.QueryString("Is_Default")),
                                                Val(Request.QueryString("Login_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Condiment_List_Set" Then
                str = objwebservices.Condiment_List_Set(
                                                         Request.QueryString("authU").ToString(),
                                            Request.QueryString("authP").ToString(),
                                            Val(Request.QueryString("Cmp_id")),
                                            Request.QueryString("store_name").ToString(),
                                            Val(Request.QueryString("Condiment_Id")),
                                            Request.QueryString("Condiment").ToString(),
                                            Val(Request.QueryString("product_id")),
                                            Val(Request.QueryString("is_active")),
                                            Request.QueryString("ip_address").ToString(),
                                            Val(Request.QueryString("is_add_substract")),
                                            Val(Request.QueryString("choices")),
                                            Val(Request.QueryString("IsBySize")),
                                            Val(Request.QueryString("sizeID")),
                                            Val(Request.QueryString("UseProductCondi")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If

            ElseIf Request.QueryString("Function").ToString() = "Department_List_Set" Then
                str = objwebservices.Department_List_Set(
                                                          Request.QueryString("authU").ToString(),
                                                        Request.QueryString("authP").ToString(),
                                                        Request.QueryString("store_name").ToString(),
                                                        Val(Request.QueryString("department_id")),
                                                        Request.QueryString("name").ToString(),
                                                        Val(Request.QueryString("department_category_id")),
                                                        Request.QueryString("department_category_name").ToString())
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "KeyMap_Details_Set" Then
                str = objwebservices.KeyMap_Details_Set(
                                                        Request.QueryString("authU").ToString(),
                                                         Request.QueryString("authP").ToString(),
                                                         Request.QueryString("store_name").ToString(),
                                                         Val(Request.QueryString("key_map_detail_id")),
                                                         Val(Request.QueryString("key_map_id")),
                                                         Val(Request.QueryString("Product_Group_id")),
                                                         Val(Request.QueryString("Product_id")),
                                                         Val(Request.QueryString("Size_id")),
                                                         Val(Request.QueryString("cmp_id")),
                                                         Val(Request.QueryString("login_id")),
                                                         Request.QueryString("ip_address").ToString(),
                                                         Convert.ToDateTime(Request.QueryString("created_date")),
                                                         Convert.ToDateTime(Request.QueryString("modify_date")),
                                                         Val(Request.QueryString("is_active")),
                                                         Request.QueryString("BG_Color").ToString(),
                                                         Request.QueryString("FG_Color").ToString(),
                                                         Request.QueryString("matrix").ToString(),
                                                         Val(Request.QueryString("maincategory_id"))
                                                      )
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Product_Condiment_List_Set" Then
                str = objwebservices.Product_Condiment_List_Set(
                                                         Request.QueryString("authU").ToString(),
                                                 Request.QueryString("authP").ToString(),
                                                 Request.QueryString("store_name").ToString(),
                                                 Val(Request.QueryString("Condiment_Id")),
                                                 Request.QueryString("Condiment").ToString(),
                                                 Val(Request.QueryString("is_add_substract")),
                                                 Val(Request.QueryString("Product_id")),
                                                 Val(Request.QueryString("Tran_id")),
                                                 Val(Request.QueryString("Price")),
                                                 Val(Request.QueryString("choices")),
                                                 Val(Request.QueryString("min_select")),
                                                 Val(Request.QueryString("max_select")),
                                                 Val(Request.QueryString("UseProductCondi"))
                                                      )
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Device_List_Set" Then
                str = objwebservices.Device_List_Set(
                                                         Request.QueryString("authU").ToString(),
                                                 Request.QueryString("authP").ToString(),
                                                 Request.QueryString("store_name").ToString(),
                                                 Val(Request.QueryString("device_id")),
                                                 Val(Request.QueryString("machine_id")),
                                                 Val(Request.QueryString("cmp_id")),
                                                 Request.QueryString("name").ToString(),
                                                 Request.QueryString("serial_no").ToString(),
                                                 Request.QueryString("code").ToString(),
                                                 Convert.ToDateTime(Request.QueryString("created_date")),
                                                 Convert.ToDateTime(Request.QueryString("modify_date")),
                                                 Request.QueryString("ip_address").ToString(),
                                                 Val(Request.QueryString("login_id")),
                                                 Val(Request.QueryString("device_type_id")),
                                                 Val(Request.QueryString("is_active")),
                                                 Request.QueryString("printer_ip_address").ToString(),
                                                 Request.QueryString("port").ToString(),
                                                 Request.QueryString("network_type").ToString(),
                                                 Val(Request.QueryString("vender_id")),
                                                 Val(Request.QueryString("budrate")),
                                                 Request.QueryString("device_name").ToString(),
                                                 Val(Request.QueryString("width")),
                                                 Request.QueryString("user_name").ToString(),
                                                 Request.QueryString("password").ToString(),
                                                 Val(Request.QueryString("application_profile_id")),
                                                 Request.QueryString("service_key").ToString(),
                                                 Request.QueryString("bluetooth_name").ToString(),
                                                 Val(Request.QueryString("device_subtype"))
                                                      )
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Tax_List_Set" Then
                str = objwebservices.Tax_List_Set(
                                             Request.QueryString("authU").ToString(),
                                             Request.QueryString("authP").ToString(),
                                             Val(Request.QueryString("cmp_id")),
                                             Request.QueryString("store_name").ToString(),
                                             Val(Request.QueryString("Tax_id")),
                                             Request.QueryString("name").ToString(),
                                             Request.QueryString("Mode").ToString(),
                                             Val(Request.QueryString("Value")),
                                             Val(Request.QueryString("Is_active")),
                                             Convert.ToDateTime(Request.QueryString("Effective_Date")),
                                             Convert.ToDateTime(Request.QueryString("created_date")),
                                             Convert.ToDateTime(Request.QueryString("Modified_date")),
                                             Val(Request.QueryString("Login_id")),
                                             Request.QueryString("Ip_address").ToString(),
                                             Val(Request.QueryString("machine_id")))
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Printer_Detail_By_Machine_Set" Then
                str = objwebservices.Printer_Detail_By_Machine_Set(
                                             Request.QueryString("authU").ToString(),
                                             Request.QueryString("authP").ToString(),
                                             Val(Request.QueryString("cmp_id")),
                                             Val(Request.QueryString("machine_id")),
                                             Request.QueryString("store_name").ToString(),
                                             Val(Request.QueryString("Printer_id")),
                                             Val(Request.QueryString("Device_id")),
                                             Val(Request.QueryString("login_id")),
                                             Request.QueryString("ip_address").ToString(),
                                             Convert.ToDateTime(Request.QueryString("created_date")),
                                             Convert.ToDateTime(Request.QueryString("modify_date"))
)
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            ElseIf Request.QueryString("Function").ToString() = "Unit_List_Set" Then
                str = objwebservices.Unit_List_Set(
                                              Request.QueryString("authU").ToString(),
                                             Request.QueryString("authP").ToString(),
                                             Val(Request.QueryString("cmp_id")),
                                             Request.QueryString("store_name").ToString(),
                                             Val(Request.QueryString("Unit_Id")),
                                             Request.QueryString("Unit").ToString(),
                                             Val(Request.QueryString("login_id"))
)
                If str <> "[]" And str <> "No User Found." And str <> "Invalid Webservice User Access...!" Then
                    Response.Write(str)
                Else
                    Response.Write("Invalid User Details ...!")
                End If
            End If

        Catch ex As Exception
            Response.Write("Invalid Request " + ex.ToString)
            LogHelper.Error("POS_WS:CallMethod():" + ex.Message)
        End Try

        'End Try
    End Sub

End Class

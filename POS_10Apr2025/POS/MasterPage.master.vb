Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    ' Inherits BaseClass
    Dim oclsRegister As New Controller_clsRegister()
    Dim oclsProduct As New Controller_clsProduct()
    Dim oClsDataccess As New ClsDataccess

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try

            If Session("cmp_id") Is Nothing Or Session("login_id") Is Nothing Then
                Response.Redirect("SignIn.aspx")
                'Response.Redirect("Login.aspx")
            Else
                If Session("staff_role_id") Is Nothing Then
                    Response.Redirect("SignIn.aspx")
                    'Response.Redirect("Login.aspx")
                End If
            End If

            If Not IsPostBack Then
                lblcmpNameMaster.Text = Session("cmp_name").ToString()
                If Session("staff_name") Is Nothing Then
                    lblUserNameMaster.Text = "Administrator"
                    imgphoto.ImageUrl = "~/images/avatar/user1.png"
                Else
                    lblUserNameMaster.Text = Session("staff_name").ToString()
                    Dim s As String = Session("Photo").ToString()
                    If Session("Photo").ToString() = "" Then
                        imgphoto.ImageUrl = "~/images/avatar/user1.png"
                    Else
                        imgphoto.ImageUrl = "~/Files/UserProfile/" + Session("Photo").ToString()
                    End If

                End If

                'bindCmpGrp()
                'oclsRegister.Company_id = Session("cmp_id").ToString()
                'Dim dtCompany As DataTable = oclsRegister.Select()
                'If Session("staff_id_login") Is Nothing Then
                '    lblUserNameMaster.Text = "Administration"
                '    If dtCompany.Rows(0)("Logo") IsNot DBNull.Value Then
                '        spnimg.Visible = True
                '        Dim bytes As Byte() = DirectCast(dtCompany.Rows(0)("C_Logo"), Byte())
                '        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                '        imgLogo.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                '        ViewState("Image") = 1
                '        radBImageMaster.DataValue = dtCompany.Rows(0)("C_Logo")
                '    Else
                '        lblcmpName.Text = dtCompany.Rows(0)("cmp_name").ToString()
                '        spnimg.Visible = False
                '    End If
                'Else
                '    lblUserNameMaster.Text = Session("staff_name").ToString.ToUpper
                '    'lblcmpName.Text = Session("cmp_name").ToString().ToUpper
                '    If dtCompany.Rows(0)("C_Logo") IsNot DBNull.Value Then
                '        spnimg.Visible = True
                '        Dim bytes As Byte() = DirectCast(dtCompany.Rows(0)("C_Logo"), Byte())
                '        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                '        imgLogo.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                '        ViewState("Image") = 1
                '        radBImageMaster.DataValue = dtCompany.Rows(0)("C_Logo")
                '    Else
                '        lblcmpName.Text = dtCompany.Rows(0)("cmp_name").ToString()
                '        spnimg.Visible = False
                '    End If
                'End If


            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("MasterPage:Page_Init:" + ex.Message)
        End Try
    End Sub

    Protected Sub editbtn_Click(sender As Object, e As EventArgs)
        Try
            Session("product_id") = Session("Last_product_id")
            Response.Redirect("Product_Master.aspx")
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindProduct:" + ex.Message)
        End Try

    End Sub

    'Private Sub MasterPage_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Try
    '        If Val(Session("IsPopup")) = 1 Then
    '            'dvMain.Visible = False
    '            'dvmenu.Visible = False
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Master.bodyNode.Attributes.add("class", "right-side-collapsed")
    'End Sub

    Protected Sub Getccess_Click(sender As Object, e As EventArgs)
        Session("product_type") = 1
        Session("fullaccess") = 1

        Response.Redirect("dashboard.aspx")
    End Sub
End Class


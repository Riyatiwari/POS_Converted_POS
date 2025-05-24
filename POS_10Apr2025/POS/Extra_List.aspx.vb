Imports System.Data
Imports System.Drawing.Printing
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports Telerik.Web.UI
Partial Class Extra_List
    Inherits System.Web.UI.Page
    Dim oclsExtra As New Controller_clsExtra

    Public Sub bindGrid()
        Try
            'oclsExtra.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsExtra.SelectAll()

            If dt.Rows.Count > 0 Then
                rdExtra.DataSource = dt
                rdExtra.DataBind()
            Else
                rdExtra.DataSource = String.Empty
                rdExtra.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Extra_List:bindGrid" + ex.Message)
        End Try

    End Sub

    Protected Sub rdStock_DetailTableDataBind(source As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs)

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then

                clear()

            End If

            bindGrid()
        Catch ex As Exception
            LogHelper.Error("Extra_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            'Response.Redirect("Extra.aspx", False)
            Dim dat As String
            If (rdNarration.SelectedValue = "Select") Then
                oclsExtra.narration = "0"
            Else
                oclsExtra.narration = rdNarration.SelectedItem.Text
            End If

            If (txtAmount.Text = "") Then
                oclsExtra.amount = 0
            Else
                oclsExtra.amount = txtAmount.Text
            End If

            If txtDate.SelectedDate.ToString() Is "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('For Date is require');", True)
                Return
            Else
                dat = IIf(txtDate.SelectedDate.ToString() = "", txtDate.SelectedDate = DateTime.Now, txtDate.SelectedDate)
                oclsExtra.for_Date = dat
            End If

            If (Val(Session("Extra_id")) <> 0) Then
                oclsExtra.extra_id = (Val(Session("Extra_id")))
                oclsExtra.Update()
                bindGrid()
                Session("Extra_id") = Nothing
                clear()
            Else
                oclsExtra.extra_id = 0
                oclsExtra.Insert()
                bindGrid()
                clear()

            End If



        Catch ex As Exception
            LogHelper.Error("Extra_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    Public Sub clear()
        rdNarration.ClearSelection()
        txtAmount.Text = ""
        'txtDate.SelectedDate = ""
    End Sub
    Protected Sub rdtill_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdExtra.ItemCommand
        Try
            If e.CommandName = "Edit" Then

                Session("Extra_id") = Val(e.CommandArgument)
                oclsExtra.extra_id = Val(e.CommandArgument)

                Dim dt As DataTable = oclsExtra.Select()

                'txtAmount.Text = dt.Rows(0).Item(0)

                If (dt.Rows(0)("narration") IsNot "") Then
                    rdNarration.SelectedValue = dt.Rows(0)("narration").ToString
                Else

                End If

                If (dt.Rows(0)("amount") IsNot "") Then
                    txtAmount.Text = dt.Rows(0)("amount").ToString
                Else
                    txtAmount.Text = 0
                End If



                If (dt.Rows(0)("For_Date") IsNot "") Then
                    'Dim dte As String
                    'dte = (dt.Rows(0)("For_Date").ToString())
                    'String.Format("{0:d}", dte)

                    txtDate.SelectedDate = (dt.Rows(0)("For_Date").ToString())
                Else

                End If


            ElseIf e.CommandName = "Delete" Then
                    'deleteTax(Val(e.CommandArgument))

                    oclsExtra.extra_id = (Val(e.CommandArgument))

                oclsExtra.for_Date = DateTime.Now

                oclsExtra.Delete()

                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Tax_List:rdTax_ItemCommand:" + ex.Message)
        End Try
    End Sub



End Class

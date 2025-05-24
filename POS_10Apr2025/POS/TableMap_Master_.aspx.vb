Imports System.Data
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.IO
Imports System.Drawing

Partial Class TableMap_Master_
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsTableMap As New Controller_clsTable_Map()

    Dim btn_val As Integer = 1
    Dim i As Integer = 0
    Dim dt As DataTable = New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("AllowSetting") = 1
        If Not IsPostBack Then
            Try

                oclsTableMap.Table_map_id = Val(Session("Table_map_id"))
                Dim Edit_dt As DataTable = oclsTableMap.SelectTableMap_Details()
                dt.Columns.Add("id", GetType(Integer))
                dt.Columns.Add("matrix", GetType(String))

                Dim j As Integer = 0
                Dim k As Integer = 0
                Dim l As Integer = 0
                Dim m As Integer = 0
                Dim n As Integer = 0
                Dim o As Integer = 0
                Dim p As Integer = 0
                Dim q As Integer = 0
                Dim r As Integer = 0

                Dim val1 As String = "0"
                Dim val2 As String = "1"
                Dim val3 As String = "2"
                Dim val4 As String = "3"
                Dim val5 As String = "4"
                Dim val6 As String = "5"
                Dim val7 As String = "6"
                Dim val8 As String = "7"
                Dim val9 As String = "8"
                Dim val10 As String = "9"

                For i = 0 To 99
                    Dim row As DataRow = dt.NewRow()
                    If (i < Edit_dt.Rows.Count) Then
                        dt.ImportRow(Edit_dt.Rows(i))
                    Else

                        row("id") = i + 1

                        If (i + 1) < 11 Then
                            row("matrix") = val1 + i.ToString()
                        ElseIf (i + 1) < 21 Then
                            'Dim j As Integer = 0
                            row("matrix") = val2 + j.ToString()
                            j = j + 1
                        ElseIf (i + 1) < 31 Then
                            row("matrix") = val3 + k.ToString()
                            k = k + 1
                        ElseIf (i + 1) < 41 Then
                            row("matrix") = val4 + l.ToString()
                            l = l + 1
                        ElseIf (i + 1) < 51 Then
                            row("matrix") = val5 + m.ToString()
                            m = m + 1
                        ElseIf (i + 1) < 61 Then
                            row("matrix") = val6 + n.ToString()
                            n = n + 1
                        ElseIf (i + 1) < 71 Then
                            row("matrix") = val7 + o.ToString()
                            o = o + 1
                        ElseIf (i + 1) < 81 Then
                            row("matrix") = val8 + p.ToString()
                            p = p + 1
                        ElseIf (i + 1) < 91 Then
                            row("matrix") = val9 + q.ToString()
                            q = q + 1
                        ElseIf (i + 1) < 101 Then
                            row("matrix") = val10 + r.ToString()
                            r = r + 1
                        End If
                        dt.Rows.Add(row)
                    End If

                Next

                'ViewState("Data_dt") = dt

                dlButton.DataSource = dt
                dlButton.DataBind()

            Catch ex As Exception
                Dim dt As String = ex.Message.ToString()
            End Try
        End If

    End Sub


    Private Sub Item_Bound(ByVal sender As [Object], ByVal e As DataListItemEventArgs) Handles dlButton.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim btn_10by10 As Button = DirectCast(e.Item.FindControl("btn_10by10"), Button)
                'Dim hd_10by10 As HiddenField = DirectCast(e.Item.FindControl("hd_10by10"), HiddenField)


                btn_10by10.CommandName = "btn_10by10_" + btn_val.ToString()

                Dim a As String = CType(e.Item.FindControl("hd_10by10"), HiddenField).Value

                If CType(e.Item.FindControl("hd_10by10"), HiddenField).Value = btn_val.ToString() Then
                    CType(e.Item.FindControl("btn_10by10"), Button).Text = "1"
                End If

                btn_val = btn_val + 1

            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub dlButton_ItemCommand(source As Object, e As DataListCommandEventArgs)
        Try

            Dim btn_10by10 As Button = DirectCast(e.Item.FindControl("btn_10by10"), Button)


            For index = 1 To 100

                If btn_10by10.CommandName = ("btn_10by10_" + index.ToString()) Then
                    Session("div_10by10") = ("btn_10by10_" + index.ToString())
                    Session("Row_Id") = index.ToString()

                    btn_10by10.BorderColor = Color.Maroon
                Else

                    'btn_10by10.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B7BBD")
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btn_save_Click(sender As Object, e As EventArgs)
        Try

            Dim item As DataList = TryCast(dlButton.Parent, DataList)
            Dim button As Button = TryCast(item.FindControl("btn_10by10"), Button)

            For index = 1 To 100

                If button.CommandName = Session("div_10by10") Then

                Else

                End If

            Next
        Catch ex As Exception

        End Try
    End Sub
End Class

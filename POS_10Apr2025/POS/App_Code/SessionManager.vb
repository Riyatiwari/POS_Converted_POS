Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Globalization

Imports System.Web.Services
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web

Public Class SessionManager

    Public oClsDataccess As New ClsDataccess

    Dim DefStr As String = "dt_"

    Public Function CreateSessionDT(ByVal page_name As String, ByVal FilteringItemName As String) As String
        Try

            If HttpContext.Current.Session(DefStr + page_name) Is Nothing Then

                Dim array As Array = FilteringItemName.ToString.Split("#")

                Dim Table1 As DataTable
                Table1 = New DataTable("CreateSession")

                Dim column1 As DataColumn = New DataColumn("FIN")
                column1.DataType = System.Type.GetType("System.String")
                Dim column2 As DataColumn = New DataColumn("VAL")
                column2.DataType = System.Type.GetType("System.String")

                Table1.Columns.Add(column1)
                Table1.Columns.Add(column2)

                For Each row As String In array
                    If row <> "" Then
                        Table1.Rows.Add(row, "")
                    End If
                Next

                HttpContext.Current.Session(DefStr + page_name) = Table1

            End If
            Return HttpContext.Current.Session(DefStr + page_name)
        Catch ex As Exception
            LogHelper.Error("ClsSessionManager:CreateSessionDT:" + ex.Message)
        End Try
        Return ""
    End Function

    Public Function AddValInSessionDT(ByVal page_name As String, ByVal FilterName As String, ByVal FilterVal As String) As String
        Try

            If Not HttpContext.Current.Session(DefStr + page_name) Is Nothing Then

                For Each row As DataRow In HttpContext.Current.Session(DefStr + page_name).Rows
                    If row("FIN").ToString() = FilterName.ToString Then
                        row.SetField("VAL", FilterVal.ToString)
                        Exit For
                    End If
                Next

            End If

        Catch ex As Exception
            LogHelper.Error("ClsSessionManager:CreateSessionDT:" + ex.Message)
        End Try
        Return ""
    End Function

    Public Function ResetSessionDT(ByVal page_name As String) As String
        Try

            If Not HttpContext.Current.Session(DefStr + page_name) Is Nothing Then

                For Each row As DataRow In HttpContext.Current.Session(DefStr + page_name).Rows
                    row.SetField("VAL", "")
                Next

            End If

        Catch ex As Exception
            LogHelper.Error("ClsSessionManager:CreateSessionDT:" + ex.Message)
        End Try
        Return ""
    End Function

End Class

Imports System.Data

Partial Class Tab_Chart
    Inherits System.Web.UI.Page

    Public Property jscript() As String
        Get
            If (Not ViewState("jscript") Is Nothing) Then
                Return ViewState("jscript")
            End If
            Return 0
        End Get
        Set(ByVal value As String)
            ViewState("jscript") = value
        End Set
    End Property

    Public Property drpTableStoreId() As Int32
        Get
            If (Not Session("drpTableStoreId") Is Nothing) Then
                Return Session("drpTableStoreId")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            Session("drpTableStoreId") = value
        End Set
    End Property

    Public Property rddlTypeId() As Int32
        Get
            If (Not Session("rddlTypeId") Is Nothing) Then
                Return Session("rddlTypeId")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            Session("rddlTypeId") = value
        End Set
    End Property

    Public Property rdpDate() As DateTime
        Get
            If (Not Session("rdpDate") Is Nothing) Then
                Return Session("rdpDate")
            End If
            Return System.DateTime.Now
        End Get
        Set(ByVal value As DateTime)
            Session("rdpDate") = value
        End Set
    End Property

    Private Sub Bindchart()
        Try
            Dim ds As New DataSet()
            Dim oClsDataccess As New ClsDataccess
            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            oColSqlPar.Add("@BBookingDate", Convert.ToDateTime(rdpDate.ToString()).ToString("yyyy-MM-dd"))
            oColSqlPar.Add("@BSettingsID", drpTableStoreId())
            oColSqlPar.Add("@BBookingScheduleID", rddlTypeId())
            ds = oClsDataccess.GetdatasetSp("Get_Chart_Script", oColSqlPar, "Get_Live_Schedule")
            ViewState("jscript") = ""
            Dim count As Integer = 0
            If ds.Tables.Count > 0 Then
                For Each row In ds.Tables(0).Rows
                    If count = 1 Then
                        ViewState("jscript") = ViewState("jscript") + row("Script")
                    Else
                        ViewState("jscript") = row("Script")
                        count = 1
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Bindchart()
        Catch ex As Exception

        End Try
    End Sub
End Class

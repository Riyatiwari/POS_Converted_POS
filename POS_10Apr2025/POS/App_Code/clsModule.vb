Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading



Public Class Controller_clsModule
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private objclsModule As clsModule
#End Region

#Region "Public Property"


#End Region

#Region "Function"
    Public Function [booking_Default]() As DataTable
        Dim dt As DataTable
        Try
            objclsModule = New clsModule()

            dt = objclsModule.booking_Default()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [default_image_web_sales]() As DataTable
        Dim dt As DataTable
        Try
            objclsModule = New clsModule()

            dt = objclsModule.default_image_web_sales()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region
End Class

Public Class clsModule
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private objclsModule As clsModule
#End Region

#Region "Public Property"

#End Region

#Region "functions"
    Public Function [booking_Default]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_booking_default_script", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [default_image_web_sales]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Default_image_web_sales", oColSqlparram)

        Return dtlogin
    End Function
#End Region
End Class

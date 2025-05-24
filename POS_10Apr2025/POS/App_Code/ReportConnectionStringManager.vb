
Imports System
Imports Telerik.Reporting

Public Class ReportConnectionStringManager
    ReadOnly connectionString As String

    Public Sub New(connectionString As String)
        Me.connectionString = connectionString
    End Sub

    Public Function UpdateReportSource(sourceReportSource As ReportSource) As ReportSource
        If TypeOf sourceReportSource Is UriReportSource Then
            Dim uriReportSource = DirectCast(sourceReportSource, UriReportSource)
            Dim reportInstance = DeserializeReport(uriReportSource)

            ValidateReportSource(uriReportSource.Uri)
            Me.SetConnectionString(reportInstance)
            Return CreateInstanceReportSource(reportInstance, uriReportSource)
        End If

        If TypeOf sourceReportSource Is XmlReportSource Then
            Dim xml = DirectCast(sourceReportSource, XmlReportSource)
            ValidateReportSource(xml.Xml)
            Dim reportInstance = Me.DeserializeReport(xml)
            Me.SetConnectionString(reportInstance)
            Return CreateInstanceReportSource(reportInstance, xml)
        End If

        If TypeOf sourceReportSource Is InstanceReportSource Then
            Dim instanceReportSource = DirectCast(sourceReportSource, InstanceReportSource)
            Me.SetConnectionString(DirectCast(instanceReportSource.ReportDocument, ReportItemBase))
            Return instanceReportSource
        End If

        If TypeOf sourceReportSource Is TypeReportSource Then
            Dim typeReportSource = DirectCast(sourceReportSource, TypeReportSource)
            Dim typeName = typeReportSource.TypeName
            ValidateReportSource(typeName)
            Dim reportType = Type.[GetType](typeName)
            Dim reportInstance = DirectCast(Activator.CreateInstance(reportType), Report)
            Me.SetConnectionString(DirectCast(reportInstance, ReportItemBase))
            Return CreateInstanceReportSource(reportInstance, typeReportSource)
        End If

        Throw New NotImplementedException("Handler for the used ReportSource type is not implemented.")
    End Function

    Private Function CreateInstanceReportSource(report As IReportDocument, originalReportSource As ReportSource) As ReportSource
        Dim instanceReportSource = New InstanceReportSource() With { _
             .ReportDocument = report _
        }
        instanceReportSource.Parameters.AddRange(originalReportSource.Parameters)
        Return instanceReportSource
    End Function

    Public Sub ValidateReportSource(value As String)
        If value.Trim().StartsWith("=") Then
            Throw New InvalidOperationException("Expressions for ReportSource are not supported when changing the connection string dynamically")
        End If
    End Sub


    Public Function DeserializeReport(uriReportSource As UriReportSource) As Report
        Dim settings = New System.Xml.XmlReaderSettings()
        settings.IgnoreWhitespace = True
        Using xmlReader = System.Xml.XmlReader.Create(uriReportSource.Uri, settings)
            Dim xmlSerializer = New Telerik.Reporting.XmlSerialization.ReportXmlSerializer()
            Dim report = DirectCast(xmlSerializer.Deserialize(xmlReader), Telerik.Reporting.Report)
            Return report
        End Using
    End Function

    Public Function DeserializeReport(xmlReportSource As XmlReportSource) As Report
        Dim settings = New System.Xml.XmlReaderSettings()
        settings.IgnoreWhitespace = True
        Dim textReader = New System.IO.StringReader(xmlReportSource.Xml)
        Using xmlReader = System.Xml.XmlReader.Create(textReader, settings)
            Dim xmlSerializer = New Telerik.Reporting.XmlSerialization.ReportXmlSerializer()
            Dim report = DirectCast(xmlSerializer.Deserialize(xmlReader), Telerik.Reporting.Report)
            Return report
        End Using
    End Function

    Public Sub SetConnectionString(reportItemBase As ReportItemBase)
        If reportItemBase.Items.Count < 1 Then
            Return
        End If

        If TypeOf reportItemBase Is Report Then
            Dim report = DirectCast(reportItemBase, Report)

            If TypeOf report.DataSource Is SqlDataSource Then
                Dim sqlDataSource = DirectCast(report.DataSource, SqlDataSource)
                sqlDataSource.ConnectionString = connectionString
                ' sqlDataSource.ConnectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password") & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
                Dim c As String = sqlDataSource.ConnectionString
            End If
            For Each parameter As Telerik.Reporting.ReportParameter In report.ReportParameters
                If TypeOf parameter.AvailableValues.DataSource Is SqlDataSource Then
                    Dim sqlDataSource = DirectCast(parameter.AvailableValues.DataSource, SqlDataSource)
                    sqlDataSource.ConnectionString = connectionString
                End If
            Next
        End If

        For Each item As Telerik.Reporting.ReportItemBase In reportItemBase.Items
            'recursively set the connection string to the items from the Items collection
            SetConnectionString(item)

            'set the drillthrough report connection strings
            Dim drillThroughAction = TryCast(item.Action, NavigateToReportAction)
            If drillThroughAction IsNot Nothing Then
                Dim updatedReportInstance = Me.UpdateReportSource(drillThroughAction.ReportSource)
                drillThroughAction.ReportSource = updatedReportInstance
            End If

            If TypeOf item Is SubReport Then
                Dim subReport = DirectCast(item, SubReport)
                subReport.ReportSource = Me.UpdateReportSource(subReport.ReportSource)
                Continue For
            End If

            'Covers all data items(Crosstab, Table, List, Graph, Map and Chart)
            If TypeOf item Is DataItem Then
                Dim dataItem = DirectCast(item, DataItem)
                If TypeOf dataItem.DataSource Is SqlDataSource Then
                    Dim sqlDataSource = DirectCast(dataItem.DataSource, SqlDataSource)
                    sqlDataSource.ConnectionString = connectionString
                    Continue For
                End If

            End If
        Next
    End Sub
End Class

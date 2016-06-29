Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Data
Public Class clsCSVWriter

    Public Shared Sub WriteDataTable(ByVal sourceTable As DataTable, ByVal FileName As String, ByVal writer As TextWriter, ByVal includeHeaders As Boolean)

        Dim context As HttpContext = HttpContext.Current
        Dim memoryStream As New MemoryStream
        writer = New StreamWriter(memoryStream, Encoding.UTF8)
        If (includeHeaders) Then
            Dim headerValues As List(Of String) = New List(Of String)()
            For Each column As DataColumn In sourceTable.Columns
                headerValues.Add(QuoteValue(column.ColumnName))
            Next
            writer.WriteLine(String.Join(",", headerValues.ToArray))
        End If
        Dim items() As String = Nothing
        For Each row As DataRow In sourceTable.Rows
            items = row.ItemArray.Select(Function(obj) QuoteValue(obj.ToString())).ToArray()
            writer.WriteLine(String.Join(",", items))
        Next

        writer.Flush()
        Dim bytesInStream As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        context.Response.Clear()
        context.Response.Charset = String.Empty
        context.Response.ContentType = "text/csv"
        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".csv")
        context.Response.BinaryWrite(bytesInStream)
        context.Response.End()
    End Sub




    Private Shared Function QuoteValue(ByVal value As String) As String
        Return String.Concat("""", value.Replace("""", """"""), """")
    End Function
End Class



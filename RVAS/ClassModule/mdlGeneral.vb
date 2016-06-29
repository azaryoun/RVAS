Imports System.Data.OleDb


Public Module mdlGeneral
    Dim rndVar As New Random

    Public Const cnst_RowsCountInPage As Integer = 25


    Public Function GeneratePassword(rndVar As Random, Length As Integer) As String



        Dim strPassword As String = ""

        For i As Integer = 0 To Length - 1
A:          Randomize()
            Dim intRndNumber As Integer = Math.Ceiling(rndVar.Next(48, 122))
            If intRndNumber > 57 AndAlso intRndNumber < 65 Then GoTo A
            If intRndNumber > 90 AndAlso intRndNumber < 97 Then GoTo A
            strPassword &= Chr(intRndNumber)
        Next

        Return strPassword

    End Function

    Public Function GetGregorianDate(ByVal strPersianDate As String) As Date

        Dim persCal As New Globalization.PersianCalendar
        Return persCal.ToDateTime(strPersianDate.Substring(0, 4), strPersianDate.Substring(5, 2), strPersianDate.Substring(8, 2), 0, 0, 0, 0)


    End Function


    Public Function GetPersianDate(ByVal dteGregorain As Date) As String

        Dim persCal As New Globalization.PersianCalendar
        Return persCal.GetYear(dteGregorain).ToString("0000") & "/" & persCal.GetMonth(dteGregorain).ToString("00") & "/" & persCal.GetDayOfMonth(dteGregorain).ToString("00")


    End Function

    Public Function GetPersianDateTime(ByVal dteGregorain As Date) As String

        Dim strPerianDate As String = GetPersianDate(dteGregorain)

        Return strPerianDate & " " & dteGregorain.ToString("HH:mm:ss")

    End Function

    Public Function GetPersianMonthLastDay(ByVal intYear As Integer, ByVal intMonth As Integer) As Integer

        Select Case intMonth
            Case 1 To 6
                Return 31
            Case 7 To 11
                Return 30
            Case 12
                Dim clsPersian As New Globalization.PersianCalendar
                If clsPersian.IsLeapYear(intYear) = True Then
                    Return 30
                Else
                    Return 29
                End If
            Case Else
                Return 31
        End Select


    End Function


    Public Function GetPersianGreetingMessage() As String

        Dim intHour As Integer = Date.Now.Hour

        Select Case intHour

            Case 0 To 3
                Return "شب بخير"
            Case 4 To 10
                Return "صبح بخير"

            Case 11 To 15
                Return "ظهر بخير"

            Case 16 To 18
                Return "عصر بخير"
            Case Else
                Return "شب بخير"

        End Select


    End Function

    Public Function GetGreetingMessage() As String

        Dim intHour As Integer = Date.Now.Hour

        Select Case intHour

            Case 0 To 3
                Return "Good Night"
            Case 4 To 10
                Return "Good Morning"

            Case 11 To 15
                Return "Good Evening"

            Case 16 To 18
                Return "Good Afternoon"
            Case Else
                Return "Good Night"

        End Select


    End Function

    Public Function GetPersianToday() As String

        Dim dteGregorain As Date = Date.Now

        Dim arrMonths As String() = {"فروردین", "اردیبهشت", "خرداد", "تیر", "امرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"}
        Dim arrWeeks As String() = {"یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه"}


        Dim persCal As New Globalization.PersianCalendar
        'Return persCal.GetYear(dteGregorain).ToString("0000") & "/" & persCal.GetMonth(dteGregorain).ToString("00") & "/" & persCal.GetDayOfMonth(dteGregorain).ToString("00")


        Return arrWeeks(dteGregorain.DayOfWeek) & " " & persCal.GetDayOfMonth(dteGregorain).ToString() & " " & arrMonths(persCal.GetMonth(dteGregorain) - 1) & " ماه " & persCal.GetYear(dteGregorain).ToString("0000")





    End Function
    Public Function GetPersianMonthYear(theDate As Date) As String


        Dim arrMonths As String() = {"فروردین", "اردیبهشت", "خرداد", "تیر", "امرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"}


        Dim persCal As New Globalization.PersianCalendar


        Return arrMonths(persCal.GetMonth(theDate) - 1) & " ماه " & persCal.GetYear(theDate).ToString("0000")





    End Function

    Public Function ReadExcelFile2007(ByVal strFilePath As String) As DataTable

        Dim strConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" & strFilePath & "; Extended Properties=""Excel 8.0; HDR=Yes; IMEX=1;"""
        Dim objConn As New OleDbConnection(strConnectionString)

        objConn.Open()

        Dim sdt As DataTable = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

        Dim strCommand As String = "SELECT * FROM [" & sdt.Rows(0)("TABLE_NAME") & "]"
        Dim objCmdSelect As New OleDbCommand(strCommand, objConn)
        Dim oOleDbDataAdapter As New OleDbDataAdapter
        oOleDbDataAdapter.SelectCommand = objCmdSelect

        Dim dtbl As New System.Data.DataTable
        oOleDbDataAdapter.Fill(dtbl)


        objConn.Close()
        dtbl.AcceptChanges()

        Return dtbl

    End Function
    Public Function ReadExcel2007(ByVal strPath As String) As DataTable

        Dim sConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" & strPath & "; Extended Properties=""Excel 12.0; HDR=No; """
        Dim objConn As New OleDbConnection(sConnectionString)
        objConn.Open()
        Dim sdt As DataTable = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

        Dim str As String = String.Format("SELECT * FROM  [SHEET1$]") '"SELECT * FROM [" & sdt.Rows(0)("TABLE_NAME") & "]"
        Dim objCmdSelect As New OleDbCommand(str, objConn)
        Dim objAdapter1 As New OleDbDataAdapter
        objAdapter1.SelectCommand = objCmdSelect
        Dim dt As New DataTable
        objAdapter1.Fill(dt)

        'For Each row As DataRow In dt.Rows
        '    If row("CNTR#NO") Is DBNull.Value Then
        '        row.Delete()
        '    End If
        'Next

        objConn.Close()
        dt.AcceptChanges()
        Return dt
    End Function


    Public Function ReadExcel2003(ByVal strFileName As String, ByVal strSheetName As String) As DataTable
        Try
            If IO.File.Exists(strFileName) = False Then
                Return Nothing
            End If

            Dim strCommandText As String
            Dim strCommandTextTemplate As String = Nothing

            Dim strConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                                                "OLE DB Services=-4;" & _
                                                "Data Source=" & strFileName & ";" & _
                                                "Extended Properties=Excel 8.0;"

            Dim dbConnection As New OleDbConnection(strConnectionString)

            Dim dbCommand As New OleDbCommand()
            Dim bCloseExcelWriterConnPeriodically As Boolean = False

            dbConnection.Open()

            dbCommand.Connection = dbConnection

            strCommandText = "select * from [" + strSheetName + "$]"

            dbCommand.CommandText = strCommandText


            Dim dtrTemp As OleDb.OleDbDataReader = dbCommand.ExecuteReader



            Dim dtblRes As New DataTable

            For i As Integer = 0 To dtrTemp.FieldCount - 1
                Dim dcl As New DataColumn
                dcl.DataType = GetType(String)

                dtblRes.Columns.Add(dcl)

            Next

            Do While dtrTemp.Read

                Dim drw As DataRow = dtblRes.NewRow

                For i As Integer = 0 To dtrTemp.FieldCount - 1

                    drw.Item(i) = dtrTemp.Item(i)
                Next
                dtblRes.Rows.Add(drw)
            Loop

            dbConnection.Close()

            Return dtblRes

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function GenerateRandomColor() As String


        Dim intRed As Integer = Math.Ceiling(rndVar.Next(0, 256))
        Dim intGreen As Integer = Math.Ceiling(rndVar.Next(0, 256))
        Dim intBlue As Integer = Math.Ceiling(rndVar.Next(0, 256))

        Return "#" & intRed.ToString("X2") & intGreen.ToString("X2") & intBlue.ToString("X2")



    End Function









End Module


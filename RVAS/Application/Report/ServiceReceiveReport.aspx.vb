
Imports System.IO
Public Class ServiceReceiveReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Panel.ShowDisplay = True
        Bootstrap_Panel.ShowExcel = False
        Bootstrap_Panel.Enable_Display_Client_Validate = True
        Bootstrap_Panel.ClearMessage()


        If Page.IsPostBack = False Then

            tblReport_Detail.Visible = False
            tblReport_Summary.Visible = False


            Dim strURL As String = Request.ServerVariables("HTTP_REFERER")
            Dim intPos As Integer = strURL.IndexOf("?")
            If intPos <> -1 Then
                strURL = strURL.Substring(0, intPos)
            End If

            ViewState("BackPage") = strURL

            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
            Dim odbVAS As New BusinessObject.Context.dbVASEntities
            Dim lnqVASService = odbVAS.tbl_VASService.Where(Function(x) x.FK_OwnerUserID = osctUserInfo.ID)
            Dim lnqVASServiceGroupList = lnqVASService.GroupBy(Function(x) New With {x.FK_HeadNumberID, x.theWholeNumber}).OrderBy(Function(x) x.Key.theWholeNumber).ToList


            For Each lnqVASServiceGroupListItem In lnqVASServiceGroupList

                cmbHeadNumbers.Items.Add(New ListItem(lnqVASServiceGroupListItem.Key.theWholeNumber, lnqVASServiceGroupListItem.Key.FK_HeadNumberID))

            Next lnqVASServiceGroupListItem


            cmbHeadNumbers.Items.Insert(0, "(تمام سرشماره ها)")
            cmbHeadNumbers.SelectedIndex = 0

            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.Date
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

        End If

    End Sub



    Private Sub Bootstrap_Panel_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Display_Click

        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim odbVAS As New BusinessObject.Context.dbVASEntities

        tblReport_Summary.Visible = False
        tblReport_Detail.Visible = False
        Session("strCSVFile") = Nothing
        Bootstrap_Panel.ShowExcel = False
        lblSummarySum.Text = "0.0"


        Dim dteFrom As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteTo As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime


        Dim intHeadNumberID? As Integer = Nothing

        If cmbHeadNumbers.SelectedIndex <> 0 Then
            intHeadNumberID = cmbHeadNumbers.SelectedValue
        End If


        Dim blnAscendingSort As Boolean = rdoIsAscendingSortYes.Checked


        Dim lnqReceiveLog = odbVAS.tbl_ReceiveLog.Where(Function(x) x.tbl_HeadNumber.tbl_VASService.Any(Function(y) y.FK_OwnerUserID = osctUserInfo.ID))
        lnqReceiveLog = lnqReceiveLog.Where(Function(x) x.STime >= dteFrom AndAlso x.STime <= dteTo)


        If intHeadNumberID IsNot Nothing Then
            lnqReceiveLog = lnqReceiveLog.Where(Function(x) x.FK_HeadNumberID = intHeadNumberID)
        End If


        If rdoShowSummaryYes.Checked = True Then
            'Summary

            tblReport_Summary.Visible = True


            Dim lnqSendLogGroupList = lnqReceiveLog.GroupBy(Function(x) New With {x.tbl_Date.DateP, .HeadNumber = x.theWholeNumber}).OrderBy(Function(x) New With {x.Key.DateP, x.Key.HeadNumber}).ToList


            If lnqSendLogGroupList.Count = 0 Then


                Dim tbRow As New HtmlTableRow
                Dim tbCell As New HtmlTableCell
                tbRow.Attributes.Add("class", "odd")
                tbRow.VAlign = "top"
                tbCell.InnerHtml = "رکوردی یافت نشد"
                tbCell.ColSpan = tblReport_Summary.Rows(0).Cells.Count
                tbCell.Align = "center"
                tbRow.Cells.Add(tbCell)
                tblReport_Summary.Rows.Insert(1, tbRow)


            Else


                Dim i As Integer = 1


                For Each lnqSendLogGroupListItem In lnqSendLogGroupList
                    Dim tbRow As New HtmlTableRow
                    Dim tbCell As HtmlTableCell = Nothing
                    If i Mod 2 = 0 Then
                        tbRow.Attributes.Add("class", "even")
                    Else
                        tbRow.Attributes.Add("class", "odd")
                    End If

                    tbRow.Attributes.Add("role", "row")


                    tbCell = New HtmlTableCell
                    tbCell.InnerHtml = i.ToString
                    tbRow.Cells.Add(tbCell)


                    tbCell = New HtmlTableCell
                    tbCell.InnerHtml = lnqSendLogGroupListItem.Key.DateP
                    tbRow.Cells.Add(tbCell)


                    tbCell = New HtmlTableCell
                    tbCell.InnerHtml = lnqSendLogGroupListItem.Key.HeadNumber
                    tbRow.Cells.Add(tbCell)


                    tbCell = New HtmlTableCell
                    Dim intCount As Integer = lnqSendLogGroupListItem.Count
                    tbCell.InnerHtml = intCount.ToString("n0")
                    tbRow.Cells.Add(tbCell)

                    tblReport_Summary.Rows.Insert(tblReport_Summary.Rows.Count - 1, tbRow)
                    i += 1

                Next lnqSendLogGroupListItem

                lblSummarySum.Text = lnqSendLogGroupList.Sum(Function(x) x.Count).ToString("n0")



            End If



        Else 'Detail

            tblReport_Detail.Visible = True


            If blnAscendingSort = True Then
                lnqReceiveLog = lnqReceiveLog.OrderBy(Function(x) x.ID)
            Else
                lnqReceiveLog = lnqReceiveLog.OrderByDescending(Function(x) x.ID)
            End If



            Dim lnqReceiveLogList = lnqReceiveLog.ToList()

            If lnqReceiveLogList.Count = 0 Then


                Dim tbRow As New HtmlTableRow
                Dim tbCell As New HtmlTableCell
                tbRow.Attributes.Add("class", "odd")
                tbRow.VAlign = "top"
                tbCell.InnerHtml = "رکوردی یافت نشد"
                tbCell.ColSpan = tblReport_Detail.Rows(0).Cells.Count
                tbCell.Align = "center"
                tbRow.Cells.Add(tbCell)
                tblReport_Detail.Rows.Insert(1, tbRow)

            Else
                Dim strCSVFile As String = ""

                For k As Integer = 0 To tblReport_Detail.Rows(0).Cells.Count - 1
                    strCSVFile &= "," & tblReport_Detail.Rows(0).Cells(k).InnerHtml
                Next
                Dim i As Integer = 1

                strCSVFile &= ControlChars.NewLine

                For Each lnqReceiveLogListItem In lnqReceiveLogList
                    Dim tbRow As New HtmlTableRow
                    Dim tbCell As HtmlTableCell = Nothing
                    If i Mod 2 = 0 Then
                        tbRow.Attributes.Add("class", "even")
                    Else
                        tbRow.Attributes.Add("class", "odd")
                    End If

                    tbRow.Attributes.Add("role", "row")


                    tbCell = New HtmlTableCell
                    tbCell.InnerHtml = i.ToString
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    tbCell.InnerHtml = mdlGeneral.GetPersianDateTime(lnqReceiveLogListItem.STime)
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    tbCell.InnerHtml = lnqReceiveLogListItem.theWholeNumber
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    Dim strMobileNo As String = lnqReceiveLogListItem.SenderMobile
                    If osctUserInfo.IsDataAdmin = False Then
                        strMobileNo = strMobileNo.Substring(0, 3) & "***" & strMobileNo.Substring(7)
                    End If
                    tbCell.InnerHtml = strMobileNo
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    Dim strtheText As String = lnqReceiveLogListItem.theText
                    tbCell.NoWrap = False
                    tbCell.InnerHtml = strtheText
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml



                    tblReport_Detail.Rows.Insert(tblReport_Detail.Rows.Count - 1, tbRow)
                    strCSVFile &= ControlChars.NewLine
                    i += 1

                Next lnqReceiveLogListItem

                Bootstrap_Panel.ShowExcel = True
                strCSVFile = strCSVFile.Substring(1).Replace("<br />", ControlChars.NewLine)
                Session("strCSVFile") = CObj(strCSVFile)



            End If




        End If








    End Sub

    Private Sub Bootstrap_Panel_Panel_Excel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Excel_Click

        Dim strCSVFile As String = CStr(Session("strCSVFile"))
        Dim memstrmObj As New MemoryStream
        Dim writer As TextWriter = New StreamWriter(memstrmObj, Encoding.UTF8)
        writer.Write(strCSVFile)

        writer.Flush()
        Dim bytesInStream As Byte() = memstrmObj.ToArray()
        memstrmObj.Close()
        Context.Response.Clear()
        Context.Response.Charset = String.Empty
        Context.Response.ContentType = "text/csv"
        Context.Response.AddHeader("Content-Disposition", "attachment; filename=" & "VASReport_Subscription" & ".csv")
        Context.Response.BinaryWrite(bytesInStream)
        Context.Response.End()
        writer.Close()


    End Sub

End Class
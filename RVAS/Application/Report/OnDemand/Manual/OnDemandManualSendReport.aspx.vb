
Imports System.IO
Public Class OnDemandManualSendReport
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

            Dim lnqVASServiceOnDemand = odbVAS.tbl_VASServiceOnDemand.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
            lnqVASServiceOnDemand = lnqVASServiceOnDemand.Where(Function(x) x.IsSystematic = False).OrderBy(Function(x) x.tbl_VASService.ServiceName)

            For Each lnqVASServiceOnDemandItem In lnqVASServiceOnDemand

                cmbVASServices.Items.Add(New ListItem(lnqVASServiceOnDemandItem.tbl_VASService.ServiceName & " (" & lnqVASServiceOnDemandItem.tbl_VASService.AggergatorServiceID & ")", lnqVASServiceOnDemandItem.FK_VASServiceID))

            Next lnqVASServiceOnDemandItem
            cmbVASServices.Items.Insert(0, "(تمام سرویس های دستی)")
            cmbVASServices.SelectedIndex = 0

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

        Dim intVASServiceID? As Integer = Nothing

        If cmbVASServices.SelectedIndex <> 0 Then
            intVASServiceID = cmbVASServices.SelectedValue
        End If


        Dim blnAscendingSort As Boolean = rdoIsAscendingSortYes.Checked


        Dim lnqSendLog = odbVAS.tbl_SendLog.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
        lnqSendLog = lnqSendLog.Where(Function(x) x.tbl_VASService.IsOnDemand = True AndAlso x.tbl_VASService.tbl_VASServiceOnDemand.IsSystematic = False)

        lnqSendLog = lnqSendLog.Where(Function(x) x.SendDateTime >= dteFrom AndAlso x.SendDateTime <= dteTo)


        If intVASServiceID IsNot Nothing Then
            lnqSendLog = lnqSendLog.Where(Function(x) x.FK_VASServiceID = intVASServiceID)
        End If


        If rdoShowSummaryYes.Checked = True Then
            'Summary

            tblReport_Summary.Visible = True


            Dim lnqSendLogGroupList = lnqSendLog.GroupBy(Function(x) New With {x.tbl_Date.DateP, .ServiceName = x.tbl_VASService.ServiceName & " (" & x.tbl_VASService.AggergatorServiceID & ")"}).OrderBy(Function(x) New With {x.Key.DateP, x.Key.ServiceName}).ToList


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
                    tbCell.InnerHtml = lnqSendLogGroupListItem.Key.ServiceName
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
                lnqSendLog = lnqSendLog.OrderBy(Function(x) x.ID)
            Else
                lnqSendLog = lnqSendLog.OrderByDescending(Function(x) x.ID)
            End If



            Dim lnqSendLogList = lnqSendLog.ToList()

            If lnqSendLogList.Count = 0 Then


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

                For Each lnqSendLogListItem In lnqSendLogList
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
                    tbCell.InnerHtml = mdlGeneral.GetPersianDateTime(lnqSendLogListItem.SendDateTime)
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    Dim strService As String = lnqSendLogListItem.tbl_VASService.ServiceName & " (" & lnqSendLogListItem.tbl_VASService.AggergatorServiceID & ")"
                    strService &= "<br />" & lnqSendLogListItem.tbl_VASService.theWholeNumber
                    tbCell.InnerHtml = strService
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    Dim strMobileNo As String = lnqSendLogListItem.ReceiverMobile
                    If osctUserInfo.IsDataAdmin = False Then
                        strMobileNo = strMobileNo.Substring(0, 3) & "***" & strMobileNo.Substring(7)
                    End If
                    tbCell.InnerHtml = strMobileNo
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    Dim strtheText As String = lnqSendLogListItem.theText
                    tbCell.NoWrap = False
                    tbCell.InnerHtml = strtheText
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml



                    tblReport_Detail.Rows.Insert(tblReport_Detail.Rows.Count - 1, tbRow)
                    strCSVFile &= ControlChars.NewLine
                    i += 1

                Next lnqSendLogListItem

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
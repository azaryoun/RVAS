
Imports System.IO
Public Class MembershipSerialMembersReport
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

            Dim lnqVASServiceMembershipNews = odbVAS.tbl_VASServiceMembership.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
            lnqVASServiceMembershipNews = lnqVASServiceMembershipNews.Where(Function(x) x.IsNewsContent = False).OrderBy(Function(x) x.tbl_VASService.ServiceName)

            For Each lnqVASServiceMembershipNewsItem In lnqVASServiceMembershipNews

                cmbVASServices.Items.Add(New ListItem(lnqVASServiceMembershipNewsItem.tbl_VASService.ServiceName & " (" & lnqVASServiceMembershipNewsItem.tbl_VASService.AggergatorServiceID & ")", lnqVASServiceMembershipNewsItem.FK_VASServiceID))

            Next lnqVASServiceMembershipNewsItem
            cmbVASServices.Items.Insert(0, "(تمام سرویس های سریالی)")
            cmbVASServices.SelectedIndex = 0


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


        Dim intVASServiceID? As Integer = Nothing

        If cmbVASServices.SelectedIndex <> 0 Then
            intVASServiceID = cmbVASServices.SelectedValue
        End If

        Dim intSubscriptionType As Integer = cmbSubscriptionType.SelectedValue

        Dim blnAscendingSort As Boolean = rdoIsAscendingSortYes.Checked


        Dim lnqVASServiceMembershipSubscriber = odbVAS.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)

        lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.IsNewsContent = False)

        If intVASServiceID IsNot Nothing Then
            lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.FK_VASServiceID = intVASServiceID)
        End If


        If intSubscriptionType = 2 Then
            lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.UnsubscriptionDate Is Nothing)
        ElseIf intSubscriptionType = 3
            lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.UnsubscriptionDate IsNot Nothing)
        End If


        If rdoShowSummaryYes.Checked = True Then
            'Summary

            tblReport_Summary.Visible = True


            Dim lnqVASServiceMembershipSubscriberGroupList = lnqVASServiceMembershipSubscriber.GroupBy(Function(x) x.tbl_VASServiceMembership.tbl_VASService.ServiceName & " (" & x.tbl_VASServiceMembership.tbl_VASService.AggergatorServiceID & ")").OrderBy(Function(x) x.Key).ToList



            If lnqVASServiceMembershipSubscriberGroupList.Count = 0 Then


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



                For Each lnqVASServiceMembershipSubscriberGroupListItem In lnqVASServiceMembershipSubscriberGroupList
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
                    tbCell.InnerHtml = lnqVASServiceMembershipSubscriberGroupListItem.Key
                    tbRow.Cells.Add(tbCell)


                    tbCell = New HtmlTableCell
                    Dim intCount As Integer = lnqVASServiceMembershipSubscriberGroupListItem.Count
                    tbCell.InnerHtml = intCount.ToString("n0")
                    tbRow.Cells.Add(tbCell)

                    tblReport_Summary.Rows.Insert(tblReport_Summary.Rows.Count - 1, tbRow)
                    i += 1

                Next lnqVASServiceMembershipSubscriberGroupListItem

                lblSummarySum.Text = lnqVASServiceMembershipSubscriberGroupList.Sum(Function(x) x.Count).ToString("n0")



            End If











        Else 'Detail

            tblReport_Detail.Visible = True


            If blnAscendingSort = True Then
                lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.OrderBy(Function(x) x.ID)
            Else
                lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.OrderByDescending(Function(x) x.ID)
            End If



            Dim lnqVASServiceMembershipSubscriberList = lnqVASServiceMembershipSubscriber.ToList()

            If lnqVASServiceMembershipSubscriberList.Count = 0 Then


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

                For Each lnqVASServiceMembershipSubscriberListItem In lnqVASServiceMembershipSubscriberList
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
                    tbCell.InnerHtml = lnqVASServiceMembershipSubscriberListItem.tbl_VASServiceMembership.tbl_VASService.ServiceName & " (" & lnqVASServiceMembershipSubscriberListItem.tbl_VASServiceMembership.tbl_VASService.AggergatorServiceID & ")"
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    Dim strMobileNo As String = lnqVASServiceMembershipSubscriberListItem.SubscriberMobileNo
                    If osctUserInfo.IsDataAdmin = False Then
                        strMobileNo = strMobileNo.Substring(0, 3) & "***" & strMobileNo.Substring(7)
                    End If
                    tbCell.InnerHtml = strMobileNo
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    tbCell.InnerHtml = mdlGeneral.GetPersianDateTime(lnqVASServiceMembershipSubscriberListItem.SubscrptionDate)
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tbCell = New HtmlTableCell
                    Dim strUnsubscriptionDate As String = ""
                    If lnqVASServiceMembershipSubscriberListItem.UnsubscriptionDate IsNot Nothing Then
                        strUnsubscriptionDate = mdlGeneral.GetPersianDateTime(lnqVASServiceMembershipSubscriberListItem.UnsubscriptionDate)
                    Else
                        strUnsubscriptionDate = "(مشترک هنوز عضو است)"
                    End If

                    tbCell.InnerHtml = strUnsubscriptionDate
                    tbRow.Cells.Add(tbCell)
                    strCSVFile &= "," & tbCell.InnerHtml


                    tblReport_Detail.Rows.Insert(tblReport_Detail.Rows.Count - 1, tbRow)
                    strCSVFile &= ControlChars.NewLine
                    i += 1

                Next lnqVASServiceMembershipSubscriberListItem

                Bootstrap_Panel.ShowExcel = True
                strCSVFile = strCSVFile.Substring(1)
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
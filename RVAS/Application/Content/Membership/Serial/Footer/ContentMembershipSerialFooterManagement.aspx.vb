Public Class ContentMembershipSerialFooterManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Panel.ShowDelete = True
        Bootstrap_Panel.ShowNew = True
        Bootstrap_Panel.ShowUp = True
        Bootstrap_Panel.Enable_Delete_Client_Validate = True
        Bootstrap_Panel.ClearMessage()



        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel.ShowMessage("قسمت محتوای سریالی با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel.ShowMessage("قسمت محتوای سریالی با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel.ShowMessage("در فرآیند ذخیره قسمت محتوای سریالی خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel.ShowMessage("در فرآیند ویرایش قسمت محتوای سریالی خطا رخ داده است", True)
            End If

            Dim strURL As String = Request.ServerVariables("HTTP_REFERER")
            If strURL Is Nothing Then
                Response.Redirect("~/Login.aspx")
                Return
            End If
            Dim intPos As Integer = strURL.IndexOf("?")
            If intPos <> -1 Then
                strURL = strURL.Substring(0, intPos)
            End If

            ViewState("BackPage") = strURL

            If MyBase.Session("inttheParentKey") Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))

            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
            Dim OdbVas As New BusinessObject.Context.dbVASEntities
            Dim lnqVASServiceMembershipSerialContentHeader = OdbVas.tbl_VASServiceMembershipSerialContentHeader.Where(Function(x) x.FK_VASServiceID = inttheParentKey).FirstOrDefault

            If lnqVASServiceMembershipSerialContentHeader Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Bootstrap_Panel.ShowPath(lnqVASServiceMembershipSerialContentHeader.theName)


            Dim intRecordCount As Integer = GetRecordCount(inttheParentKey)

            Session("intRecordCount") = CObj(intRecordCount)
            If intRecordCount = 0 Then
                Session("intCurrentPageNo") = "0"
            Else
                Session("intCurrentPageNo") = "1"
            End If
            lblTableRecordCount.Text = intRecordCount.ToString("n0")
            Session("strFilter") = ""
            Call FillTable(inttheParentKey)


        Else

            If hdnAction.Value.StartsWith("E") = True Then
                Dim inttheKey As Integer = CInt(hdnAction.Value.Split(";")(1))
                MyBase.Session("inttheKey") = inttheKey
                Response.Redirect("ContentMembershipSerialFooterEdit.aspx")
            End If


        End If


    End Sub



#Region "Bootstrap_Panel Events"
    Private Sub Bootstrap_Panel_Panel_Up_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Up_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub
    Private Sub Bootstrap_Panel_Panel_New_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_New_Click
        Response.Redirect("ContentMembershipSerialFooterNew.aspx")

    End Sub
    Private Sub Bootstrap_Panel_Panel_Delete_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Delete_Click
        Dim arrstrTheKeys As String() = hdnAction.Value.Split(";")
        Dim odbVAS As New BusinessObject.Context.dbVASEntities
        Try
            For i As Integer = 0 To arrstrTheKeys.Length - 2
                Dim inttheKey As Integer = CInt(arrstrTheKeys(i).Substring(4))
                odbVAS.spr_VASServiceMembershipSerialContentFooter_Delete(inttheKey)
            Next i
            Bootstrap_Panel.ShowMessage("رکورد یا رکوردهای انتخاب شده با موفقیت حذف شدند", False)
        Catch ex As Exception
            Bootstrap_Panel.ShowMessage("خطا در عملیات حذف " & ex.Message, True)
        End Try

        txtTableSearch.Text = CStr(Session("strFilter"))
        Call btnSearch_New_Click(sender, e)

    End Sub



#End Region

#Region "Management Table Buttons (No Update Required)"


    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))


        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim strFilter = CStr(Session("strFilter"))

        If intCurrentPageNo > 0 Then
            intCurrentPageNo = 1
            Session("intCurrentPageNo") = "1"
        End If

        If strFilter = "" Then
            Call FillTable(inttheParentKey)
        Else
            Call FillTable(inttheParentKey, strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString


    End Sub

    Private Sub btnGoPage_Click(sender As Object, e As EventArgs) Handles btnGoPage.Click
        Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))

        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim intRecordCount As Integer = CInt(Session("intRecordCount"))

        Dim strFilter = CStr(Session("strFilter"))

        If intCurrentPageNo > 0 Then
            Dim intRequestPageNo As Integer = Val(txtTablePageNo.Value)
            Dim intLastPageNo As Integer = Math.Ceiling(intRecordCount / mdlGeneral.cnst_RowsCountInPage)


            If intRequestPageNo < 1 Then
                intRequestPageNo = 1
            ElseIf intRequestPageNo > intLastPageNo
                intRequestPageNo = intLastPageNo
            End If
            intCurrentPageNo = intRequestPageNo
            Session("intCurrentPageNo") = CObj(intCurrentPageNo)

        End If

        If strFilter = "" Then
            Call FillTable(inttheParentKey)
        Else
            Call FillTable(inttheParentKey, strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString

    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))

        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim intRecordCount As Integer = CInt(Session("intRecordCount"))
        Dim strFilter = CStr(Session("strFilter"))

        If intCurrentPageNo > 0 Then
            Dim intLastPageNo As Integer = Math.Ceiling(intRecordCount / mdlGeneral.cnst_RowsCountInPage)
            intCurrentPageNo = intLastPageNo
            Session("intCurrentPageNo") = CObj(intCurrentPageNo)
        End If

        If strFilter = "" Then
            Call FillTable(inttheParentKey)
        Else
            Call FillTable(inttheParentKey, strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString




    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))

        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim intRecordCount As Integer = CInt(Session("intRecordCount"))

        Dim strFilter = CStr(Session("strFilter"))
        If intCurrentPageNo > 0 Then
            Dim intLastPageNo As Integer = Math.Ceiling(intRecordCount / mdlGeneral.cnst_RowsCountInPage)

            intCurrentPageNo += 1
            If intCurrentPageNo > intLastPageNo Then
                intCurrentPageNo = intLastPageNo
            End If
            Session("intCurrentPageNo") = CObj(intCurrentPageNo)
        End If

        If strFilter = "" Then
            Call FillTable(inttheParentKey)
        Else
            Call FillTable(inttheParentKey, strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString

    End Sub

    Private Sub btnPreviousPage_Click(sender As Object, e As EventArgs) Handles btnPreviousPage.Click
        Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))

        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim strFilter = CStr(Session("strFilter"))

        If intCurrentPageNo > 0 Then
            intCurrentPageNo -= 1
            If intCurrentPageNo < 1 Then
                intCurrentPageNo = 1
            End If
            Session("intCurrentPageNo") = CObj(intCurrentPageNo)
        End If

        If strFilter = "" Then
            Call FillTable(inttheParentKey)
        Else
            Call FillTable(inttheParentKey, strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString

    End Sub

    Private Sub btnSearch_New_Click(sender As Object, e As EventArgs) Handles btnSearch_New.Click
        Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))

        Dim strFilter As String = txtTableSearch.Text.Trim
        Dim intRecordCount As Integer = 0
        Session("strFilter") = strFilter

        If strFilter = "" Then
            intRecordCount = GetRecordCount(inttheParentKey)

            If intRecordCount = 0 Then
                Session("intCurrentPageNo") = "0"
            Else
                Session("intCurrentPageNo") = "1"

            End If

            Call FillTable(inttheParentKey)
        Else

            intRecordCount = GetRecordCount(inttheParentKey, strFilter)
            If intRecordCount = 0 Then
                Session("intCurrentPageNo") = "0"
            Else
                Session("intCurrentPageNo") = "1"
            End If

            Call FillTable(inttheParentKey, strFilter)

        End If
        lblTableRecordCount.Text = intRecordCount.ToString("n0")

        Session("intRecordCount") = CObj(intRecordCount)

    End Sub

#End Region

#Region "Table Functions"

    Private Function GetRecordCount(inttheHeaderID As Integer) As Integer

        Dim OdbVAS As New BusinessObject.Context.dbVASEntities
        Dim intRecordCount As Integer = 0

        Dim lnqVASServiceMembershipSerialContentFooter = OdbVAS.tbl_VASServiceMembershipSerialContentFooter.Where(Function(x) x.FK_VASServiceID = inttheHeaderID)

        intRecordCount = lnqVASServiceMembershipSerialContentFooter.Count

        Return intRecordCount



    End Function
    Public Function GetRecordCount(inttheHeaderID As Integer, strFilterText As String) As Integer

        Dim OdbVAS As New BusinessObject.Context.dbVASEntities
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim intRecordCount As Integer = 0

        Dim lnqVASServiceMembershipSerialContentFooter = OdbVAS.tbl_VASServiceMembershipSerialContentFooter.Where(Function(x) x.FK_VASServiceID = inttheHeaderID)
        lnqVASServiceMembershipSerialContentFooter = lnqVASServiceMembershipSerialContentFooter.Where(Function(x) x.theText.Contains(strFilterText) OrElse x.IntervalDay.ToString.Contains(strFilterText) OrElse x.SendTime.ToString.Contains(strFilterText) OrElse x.theOrder.ToString.Contains(strFilterText))

        intRecordCount = lnqVASServiceMembershipSerialContentFooter.Count

        Return intRecordCount



    End Function
    Private Sub FillTable(inttheHeaderID As Integer)
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim intStartRecord As Integer = 0
        Dim intEndRecord As Integer = 0

        If intCurrentPageNo > 0 Then

            Dim intSkip As Integer = (intCurrentPageNo - 1) * mdlGeneral.cnst_RowsCountInPage
            Dim OdbVAS As New BusinessObject.Context.dbVASEntities


            Dim lnqVASServiceMembershipSerialContentFooter = OdbVAS.tbl_VASServiceMembershipSerialContentFooter.Where(Function(x) x.FK_VASServiceID = inttheHeaderID)
            Dim lnqVASServiceMembershipSerialContentFooterList = lnqVASServiceMembershipSerialContentFooter.OrderBy(Function(x) x.theOrder).Skip(intSkip).Take(mdlGeneral.cnst_RowsCountInPage).ToList

            intStartRecord = (intCurrentPageNo - 1) * mdlGeneral.cnst_RowsCountInPage + 1
            intEndRecord = intStartRecord + lnqVASServiceMembershipSerialContentFooterList.Count - 1

            Call BuildTable(intStartRecord, lnqVASServiceMembershipSerialContentFooterList)


        Else

            Dim tbRow As New HtmlTableRow
            Dim tbCell As New HtmlTableCell
            tbRow.Attributes.Add("class", "odd")
            tbRow.VAlign = "top"
            tbCell.InnerHtml = "رکوردی یافت نشد"
            tbCell.ColSpan = tblManagement.Rows(0).Cells.Count
            tbCell.Align = "center"
            tbRow.Cells.Add(tbCell)
            tblManagement.Rows.Insert(1, tbRow)



        End If


        lblTableRowFrom.Text = intStartRecord.ToString("n0")
        lblTableRowTo.Text = intEndRecord.ToString("n0")
        txtTablePageNo.Value = intCurrentPageNo.ToString("n0")

    End Sub

    Private Sub FillTable(inttheHeaderID As Integer, strFilterText As String)
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)


        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))

        Dim intStartRecord As Integer = 0
        Dim intEndRecord As Integer = 0

        If intCurrentPageNo > 0 Then
            Dim intSkip As Integer = (intCurrentPageNo - 1) * mdlGeneral.cnst_RowsCountInPage


            Dim OdbVAS As New BusinessObject.Context.dbVASEntities

            Dim lnqVASServiceMembershipSerialContentFooter = OdbVAS.tbl_VASServiceMembershipSerialContentFooter.Where(Function(x) x.FK_VASServiceID = inttheHeaderID)
            lnqVASServiceMembershipSerialContentFooter = lnqVASServiceMembershipSerialContentFooter.Where(Function(x) x.theText.Contains(strFilterText) OrElse x.IntervalDay.ToString.Contains(strFilterText) OrElse x.SendTime.ToString.Contains(strFilterText) OrElse x.theOrder.ToString.Contains(strFilterText))
            Dim lnqVASServiceMembershipSerialContentFooterList = lnqVASServiceMembershipSerialContentFooter.OrderBy(Function(x) x.theOrder).Skip(intSkip).Take(mdlGeneral.cnst_RowsCountInPage).ToList



            intStartRecord = (intCurrentPageNo - 1) * mdlGeneral.cnst_RowsCountInPage + 1
            intEndRecord = intStartRecord + lnqVASServiceMembershipSerialContentFooterList.Count - 1

            Call BuildTable(intStartRecord, lnqVASServiceMembershipSerialContentFooterList)



        Else
            Dim tbRow As New HtmlTableRow
            Dim tbCell As New HtmlTableCell
            tbRow.Attributes.Add("class", "odd")
            tbRow.VAlign = "top"
            tbCell.InnerHtml = "رکوردی یافت نشد"
            tbCell.ColSpan = tblManagement.Rows(0).Cells.Count
            tbCell.Align = "center"
            tbRow.Cells.Add(tbCell)
            tblManagement.Rows.Insert(1, tbRow)

        End If


        lblTableRowFrom.Text = intStartRecord.ToString("n0")
        lblTableRowTo.Text = intEndRecord.ToString("n0")
        txtTablePageNo.Value = intCurrentPageNo.ToString("n0")


    End Sub

    Private Sub BuildTable(intStartRecord As Integer, lnqVASServiceMembershipSerialContentFooterList As IEnumerable(Of BusinessObject.Context.tbl_VASServiceMembershipSerialContentFooter))


        Dim i As Integer = intStartRecord
        For Each lnqVASServiceMembershipSerialContentFooterListItem In lnqVASServiceMembershipSerialContentFooterList
            Dim tbRow As New HtmlTableRow
            Dim tbCell As HtmlTableCell = Nothing
            If i Mod 2 = 0 Then
                tbRow.Attributes.Add("class", "even")
            Else
                tbRow.Attributes.Add("class", "odd")
            End If

            tbRow.Attributes.Add("role", "row")


            tbCell = New HtmlTableCell
            tbCell.InnerHtml = "<input type = ""checkbox"" value="""" id=""chkk" & lnqVASServiceMembershipSerialContentFooterListItem.ID & """   />"
            tbRow.Cells.Add(tbCell)

            tbCell = New HtmlTableCell
            tbCell.InnerHtml = i.ToString
            tbRow.Cells.Add(tbCell)


            tbCell = New HtmlTableCell
            tbCell.InnerHtml = "<a href=""#"" onclick=""btnEdit_ClientClick(" & lnqVASServiceMembershipSerialContentFooterListItem.ID & ")"">&nbsp;" & If(lnqVASServiceMembershipSerialContentFooterListItem.theText.Length > 25, lnqVASServiceMembershipSerialContentFooterListItem.theText.Substring(0, 23) & " ...", lnqVASServiceMembershipSerialContentFooterListItem.theText) & "&nbsp;</a>"
            tbCell.NoWrap = False
            tbCell.Attributes.Add("title", "برای ویرایش روی لینک، کلیک نمایید")
            tbRow.Cells.Add(tbCell)


            Dim intIntervalDay As Integer = lnqVASServiceMembershipSerialContentFooterListItem.IntervalDay
            tbCell = New HtmlTableCell
            tbCell.InnerHtml = intIntervalDay.ToString("n0")
            tbRow.Cells.Add(tbCell)


            Dim tspnSendTime As TimeSpan = lnqVASServiceMembershipSerialContentFooterListItem.SendTime
            tbCell = New HtmlTableCell
            tbCell.InnerHtml = tspnSendTime.Hours.ToString("00") & ":" & tspnSendTime.Minutes.ToString("00")
            tbRow.Cells.Add(tbCell)

            Dim inttheOrder As Integer = lnqVASServiceMembershipSerialContentFooterListItem.theOrder
            tbCell = New HtmlTableCell
            tbCell.InnerHtml = inttheOrder.ToString("n0")
            tbRow.Cells.Add(tbCell)


            tblManagement.Rows.Insert(tblManagement.Rows.Count - 1, tbRow)
            i += 1
        Next lnqVASServiceMembershipSerialContentFooterListItem



    End Sub


#End Region



End Class
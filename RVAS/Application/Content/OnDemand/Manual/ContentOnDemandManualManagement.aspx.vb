Public Class ContentOnDemandManualManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Panel.ShowDelete = True
        Bootstrap_Panel.ShowNew = True
        Bootstrap_Panel.Enable_Delete_Client_Validate = True
        Bootstrap_Panel.ClearMessage()

        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel.ShowMessage("محتوای دستی با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel.ShowMessage("محتوای دستی با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel.ShowMessage("در فرآیند ذخیره محتوای دستی خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel.ShowMessage("در فرآیند ویرایش محتوای دستی خطا رخ داده است", True)
            End If


            Dim intRecordCount As Integer = GetRecordCount()

            Session("intRecordCount") = CObj(intRecordCount)
            If intRecordCount = 0 Then
                Session("intCurrentPageNo") = "0"
            Else
                Session("intCurrentPageNo") = "1"
            End If
            lblTableRecordCount.Text = intRecordCount.ToString("n0")
            Session("strFilter") = ""
            Call FillTable()


        Else

            If hdnAction.Value.StartsWith("E") = True Then
                Dim inttheKey As Integer = CInt(hdnAction.Value.Split(";")(1))
                MyBase.Session("inttheKey") = inttheKey
                Response.Redirect("ContentOnDemandManualEdit.aspx")
            End If


        End If


    End Sub



#Region "Bootstrap_Panel Events"

    Private Sub Bootstrap_Panel_Panel_New_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_New_Click
        Response.Redirect("ContentOnDemandManualNew.aspx")

    End Sub
    Private Sub Bootstrap_Panel_Panel_Delete_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Delete_Click
        Dim arrstrTheKeys As String() = hdnAction.Value.Split(";")
        Try
            For i As Integer = 0 To arrstrTheKeys.Length - 2
                Dim inttheKey As Integer = CInt(arrstrTheKeys(i).Substring(4))
                Dim odbVAS As New BusinessObject.Context.dbVASEntities
                odbVAS.spr_VASServiceOnDemandManualContent_Delete(inttheKey)
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
        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim strFilter = CStr(Session("strFilter"))

        If intCurrentPageNo > 0 Then
            intCurrentPageNo = 1
            Session("intCurrentPageNo") = "1"
        End If

        If strFilter = "" Then
            Call FillTable()
        Else
            Call FillTable(strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString


    End Sub

    Private Sub btnGoPage_Click(sender As Object, e As EventArgs) Handles btnGoPage.Click
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
            Call FillTable()
        Else
            Call FillTable(strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString

    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim intRecordCount As Integer = CInt(Session("intRecordCount"))
        Dim strFilter = CStr(Session("strFilter"))

        If intCurrentPageNo > 0 Then
            Dim intLastPageNo As Integer = Math.Ceiling(intRecordCount / mdlGeneral.cnst_RowsCountInPage)
            intCurrentPageNo = intLastPageNo
            Session("intCurrentPageNo") = CObj(intCurrentPageNo)
        End If

        If strFilter = "" Then
            Call FillTable()
        Else
            Call FillTable(strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString




    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
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
            Call FillTable()
        Else
            Call FillTable(strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString

    End Sub

    Private Sub btnPreviousPage_Click(sender As Object, e As EventArgs) Handles btnPreviousPage.Click
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
            Call FillTable()
        Else
            Call FillTable(strFilter)
        End If
        txtTablePageNo.Value = intCurrentPageNo.ToString

    End Sub

    Private Sub btnSearch_New_Click(sender As Object, e As EventArgs) Handles btnSearch_New.Click
        Dim strFilter As String = txtTableSearch.Text.Trim
        Dim intRecordCount As Integer = 0
        Session("strFilter") = strFilter

        If strFilter = "" Then
            intRecordCount = GetRecordCount()

            If intRecordCount = 0 Then
                Session("intCurrentPageNo") = "0"
            Else
                Session("intCurrentPageNo") = "1"

            End If

            Call FillTable()
        Else

            intRecordCount = GetRecordCount(strFilter)
            If intRecordCount = 0 Then
                Session("intCurrentPageNo") = "0"
            Else
                Session("intCurrentPageNo") = "1"
            End If

            Call FillTable(strFilter)

        End If
        lblTableRecordCount.Text = intRecordCount.ToString("n0")

        Session("intRecordCount") = CObj(intRecordCount)

    End Sub

#End Region

#Region "Table Functions"

    Private Function GetRecordCount() As Integer

        Dim OdbVAS As New BusinessObject.Context.dbVASEntities
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim intRecordCount As Integer

        intRecordCount = OdbVAS.tbl_VASServiceOnDemandManualContent.Where(Function(x) x.tbl_VASServiceOnDemand.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Count

        Return intRecordCount



    End Function
    Public Function GetRecordCount(strFilterText As String) As Integer

        Dim OdbVAS As New BusinessObject.Context.dbVASEntities
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim intRecordCount As Integer = 0

        Dim lnqVASServiceOnDemandManualContent = OdbVAS.tbl_VASServiceOnDemandManualContent.Where(Function(x) x.tbl_VASServiceOnDemand.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
        lnqVASServiceOnDemandManualContent = lnqVASServiceOnDemandManualContent.Where(Function(x) If(x.IsActive = True, "فعال", "غیر فعال").Contains(strFilterText) OrElse x.tbl_VASServiceOnDemand.tbl_VASService.ServiceName.Contains(strFilterText) OrElse x.tbl_VASServiceOnDemand.tbl_VASService.AggergatorServiceID.Contains(strFilterText) OrElse x.theText.Contains(strFilterText))

        intRecordCount = lnqVASServiceOnDemandManualContent.Count

        Return intRecordCount



    End Function
    Private Sub FillTable()
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))
        Dim intStartRecord As Integer = 0
        Dim intEndRecord As Integer = 0

        If intCurrentPageNo > 0 Then

            Dim intSkip As Integer = (intCurrentPageNo - 1) * mdlGeneral.cnst_RowsCountInPage
            Dim OdbVAS As New BusinessObject.Context.dbVASEntities

            Dim lnqVASServiceOnDemandManualContent = OdbVAS.tbl_VASServiceOnDemandManualContent.Where(Function(x) x.tbl_VASServiceOnDemand.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
            Dim lnqVASServiceOnDemandManualContentList = lnqVASServiceOnDemandManualContent.OrderByDescending(Function(x) x.ID).Skip(intSkip).Take(mdlGeneral.cnst_RowsCountInPage).ToList

            intStartRecord = (intCurrentPageNo - 1) * mdlGeneral.cnst_RowsCountInPage + 1
            intEndRecord = intStartRecord + lnqVASServiceOnDemandManualContentList.Count - 1

            Call BuildTable(intStartRecord, lnqVASServiceOnDemandManualContentList)


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

    Private Sub FillTable(strFilterText As String)
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)


        Dim intCurrentPageNo As Integer = CInt(Session("intCurrentPageNo"))

        Dim intStartRecord As Integer = 0
        Dim intEndRecord As Integer = 0

        If intCurrentPageNo > 0 Then
            Dim intSkip As Integer = (intCurrentPageNo - 1) * mdlGeneral.cnst_RowsCountInPage


            Dim OdbVAS As New BusinessObject.Context.dbVASEntities
            Dim lnqVASServiceOnDemandManualContent = OdbVAS.tbl_VASServiceOnDemandManualContent.Where(Function(x) x.tbl_VASServiceOnDemand.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
            lnqVASServiceOnDemandManualContent = lnqVASServiceOnDemandManualContent.Where(Function(x) If(x.IsActive = True, "فعال", "غیر فعال").Contains(strFilterText) OrElse x.tbl_VASServiceOnDemand.tbl_VASService.ServiceName.Contains(strFilterText) OrElse x.tbl_VASServiceOnDemand.tbl_VASService.AggergatorServiceID.Contains(strFilterText) OrElse x.theText.Contains(strFilterText))
            Dim lnqVASServiceOnDemandManualContentList = lnqVASServiceOnDemandManualContent.OrderByDescending(Function(x) x.ID).Skip(intSkip).Take(mdlGeneral.cnst_RowsCountInPage).ToList




            intStartRecord = (intCurrentPageNo - 1) * mdlGeneral.cnst_RowsCountInPage + 1
            intEndRecord = intStartRecord + lnqVASServiceOnDemandManualContentList.Count - 1

            Call BuildTable(intStartRecord, lnqVASServiceOnDemandManualContentList)



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

    Private Sub BuildTable(intStartRecord As Integer, lnqVASServiceOnDemandManualContentList As IEnumerable(Of BusinessObject.Context.tbl_VASServiceOnDemandManualContent))


        Dim i As Integer = intStartRecord
        For Each lnqVASServiceOnDemandManualContentListItem In lnqVASServiceOnDemandManualContentList
            Dim tbRow As New HtmlTableRow
            Dim tbCell As HtmlTableCell = Nothing
            If i Mod 2 = 0 Then
                tbRow.Attributes.Add("class", "even")
            Else
                tbRow.Attributes.Add("class", "odd")
            End If

            tbRow.Attributes.Add("role", "row")


            tbCell = New HtmlTableCell
            tbCell.InnerHtml = "<input type = ""checkbox"" value="""" id=""chkk" & lnqVASServiceOnDemandManualContentListItem.ID & """   />"
            tbRow.Cells.Add(tbCell)

            tbCell = New HtmlTableCell
            tbCell.InnerHtml = i.ToString
            tbRow.Cells.Add(tbCell)


            tbCell = New HtmlTableCell
            tbCell.InnerHtml = "<a href=""#"" onclick=""btnEdit_ClientClick(" & lnqVASServiceOnDemandManualContentListItem.ID & ")"">&nbsp;" & If(lnqVASServiceOnDemandManualContentListItem.theText.Length > 25, lnqVASServiceOnDemandManualContentListItem.theText.Substring(0, 23) & " ...", lnqVASServiceOnDemandManualContentListItem.theText) & "&nbsp;</a>"
            tbCell.NoWrap = False
            tbCell.Attributes.Add("title", "برای ویرایش روی لینک، کلیک نمایید")
            tbRow.Cells.Add(tbCell)

            tbCell = New HtmlTableCell
            tbCell.InnerHtml = lnqVASServiceOnDemandManualContentListItem.tbl_VASServiceOnDemand.tbl_VASService.ServiceName & "<br />" & lnqVASServiceOnDemandManualContentListItem.tbl_VASServiceOnDemand.tbl_VASService.AggergatorServiceID
            tbRow.Cells.Add(tbCell)

            tbCell = New HtmlTableCell
            tbCell.InnerHtml = "از " & mdlGeneral.GetPersianDateTime(lnqVASServiceOnDemandManualContentListItem.ValidFrom) & "<br />تا " & mdlGeneral.GetPersianDateTime(lnqVASServiceOnDemandManualContentListItem.ValidTo)
            tbRow.Cells.Add(tbCell)



            tbCell = New HtmlTableCell
            tbCell.InnerHtml = If(lnqVASServiceOnDemandManualContentListItem.IsActive = True, "فعال", "غیر فعال")
            tbRow.Cells.Add(tbCell)




            tblManagement.Rows.Insert(tblManagement.Rows.Count - 1, tbRow)
            i += 1
        Next lnqVASServiceOnDemandManualContentListItem



    End Sub
#End Region



End Class
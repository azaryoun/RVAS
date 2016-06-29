Imports Service.Security
Public Class VASServiceOnDemandSystematicNew
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Panel.ShowSave = True
        Bootstrap_Panel.ShowCancel = True
        Bootstrap_Panel.Enable_Save_Client_Validate = True
        Bootstrap_Panel.ClearMessage()

        If Page.IsPostBack = False Then


            Dim strURL As String = Request.ServerVariables("HTTP_REFERER")
            Dim intPos As Integer = strURL.IndexOf("?")
            If intPos <> -1 Then
                strURL = strURL.Substring(0, intPos)
            End If

            ViewState("BackPage") = strURL


            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
            Dim odbVAS As New BusinessObject.Context.dbVASEntities

            Dim lnqUserOwner = odbVAS.tbl_User.OrderBy(Function(x) x.Username)
            For Each lnqUserOwnerItem In lnqUserOwner
                cmbUserOwner.Items.Add(New ListItem(lnqUserOwnerItem.Username & " (" & lnqUserOwnerItem.FName & " " & lnqUserOwnerItem.LName & ")", lnqUserOwnerItem.ID))
            Next lnqUserOwnerItem
            cmbUserOwner.Items.Insert(0, "(انتخاب نمایید)")
            cmbUserOwner.SelectedIndex = 0

            Dim lnqAggergators = odbVAS.tbl_Aggregators.OrderBy(Function(x) x.Name)

            For Each lnqAggergatorItem In lnqAggergators
                cmbAggregator.Items.Add(New ListItem(lnqAggergatorItem.Name, lnqAggergatorItem.ID))
            Next lnqAggergatorItem

            cmbAggregator.Items.Insert(0, "(انتخاب نمایید)")
            cmbAggregator.SelectedIndex = 0

            cmbtheWholeNumbers.Items.Insert(0, "(انتخاب نمایید)")
            cmbtheWholeNumbers.SelectedIndex = 0

            txtIMIEnduserTariff.Enabled = False



            Dim lnqCategoryVASService = odbVAS.tbl_CategoryVASService.OrderBy(Function(x) x.Title)

            For Each lnqCategoryVASServiceItem In lnqCategoryVASService
                cmbCategoryVASService.Items.Add(New ListItem(lnqCategoryVASServiceItem.Title, lnqCategoryVASServiceItem.ID))
            Next lnqCategoryVASServiceItem

            cmbCategoryVASService.Items.Insert(0, "(انتخاب نمایید)")
            cmbCategoryVASService.SelectedIndex = 0


        End If

        rdoPardisNo.Attributes.Add("onchange", "rdoPardisNo_onchange();")
        rdoPardisYes.Attributes.Add("onchange", "rdoPardisNo_onchange();")
        cmbtheTypeDemand.Attributes.Add("onchange", "cmbtheTypeDemand_onchange();")


    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim odbVAS As New BusinessObject.Context.dbVASEntities



        Dim strTextHeader As String = txtTextHeader.Text.Trim
        Dim strTextFooter As String = txtTextFooter.Text.Trim
        Dim strTextWelcome As String = txtTextWelcome.Text.Trim
        Dim strTextGoodbye As String = txtTextGoodbye.Text.Trim
        Dim sngServicePrice As Single = Val(txtServicePrice.Text.Replace(",", ""))
        Dim blnIsPardis As Boolean = rdoPardisYes.Checked
        Dim sngIMIEnduserTariff? As Single = Nothing
        If blnIsPardis = False Then
            sngIMIEnduserTariff = Val(txtIMIEnduserTariff.Text.Replace(",", ""))
        End If
        Dim inttheTypeDemand As Integer = cmbtheTypeDemand.SelectedValue
        Dim strtheKeyDemand As String = Nothing

        If inttheTypeDemand <> 3 Then 'Dyanamic
            strtheKeyDemand = txtKeyDemand.Text.Trim
        End If


        Dim intOwnerUserID As Integer = cmbUserOwner.SelectedValue
        Dim intHeadNumberID As Integer = cmbtheWholeNumbers.SelectedValue
        Dim strtheWholeNumber As String = cmbtheWholeNumbers.SelectedItem.Text
        Dim strAggergatorServiceID As String = txtServiceID.Text.Trim
        Dim strServiceName As String = txtServiceName.Text.Trim
        Dim intCategoryVASServiceID As Integer = cmbCategoryVASService.SelectedValue
        Dim blnIsServiceActive As Boolean = rdoServiceActiveYes.Checked
        Dim strRemarks As String = txtRemarks.Text.Trim
        Dim intAggregatorID As Integer = cmbAggregator.SelectedValue

        Dim strWebServicePath As String = txtWebServicePath.Text.Trim
        Dim strWebServiceMethod As String = txtWebServiceMethod.Text.Trim
        Dim strWebServiceUsername As String = txtWebServiceUsername.Text.Trim
        Dim strWebServicePassword As String = txtWebServicePassword.Text
        Dim strWebServiceParameter As String = txtWebServiceParameter.Text.Trim




        Dim lnqVASServiceCount As Integer = 0

        lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.ServiceName = strServiceName).Count

        If lnqVASServiceCount > 0 Then
            Bootstrap_Panel.ShowMessage("عنوان سرویس تکراری است", True)
            Return
        End If

        lnqVASServiceCount = 0
        lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.AggergatorServiceID = strAggergatorServiceID).Count

        If lnqVASServiceCount > 0 Then
            Bootstrap_Panel.ShowMessage("Service ID تکراری است", True)
            Return
        End If


        If inttheTypeDemand = 1 Then 'Fix Key

            lnqVASServiceCount = 0
            lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.FK_HeadNumberID = intHeadNumberID AndAlso (x.tbl_VASServiceMembership.SubscriptionKey = strtheKeyDemand OrElse x.tbl_VASServiceMembership.UnsubscriptionKey = strtheKeyDemand OrElse x.tbl_VASServiceOnDemand.theKey = strtheKeyDemand) AndAlso x.IsActive = True).Count

            If lnqVASServiceCount > 0 Then
                Bootstrap_Panel.ShowMessage("کلید تقاضا روی این سرشماره تکراری است", True)
                Return
            End If

        ElseIf inttheTypeDemand = 2 Then 'Starts With Key

            lnqVASServiceCount = 0
            lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.FK_HeadNumberID = intHeadNumberID AndAlso (x.tbl_VASServiceMembership.SubscriptionKey.StartsWith(strtheKeyDemand) = True OrElse x.tbl_VASServiceMembership.UnsubscriptionKey.StartsWith(strtheKeyDemand) = True OrElse x.tbl_VASServiceOnDemand.theKey.StartsWith(strtheKeyDemand) = True) AndAlso x.IsActive = True).Count

            If lnqVASServiceCount > 0 Then
                Bootstrap_Panel.ShowMessage("کلید تقاضا روی این سرشماره تکراری است", True)
                Return
            End If
        ElseIf inttheTypeDemand = 3 Then 'Dyanmic Key

            lnqVASServiceCount = 0
            lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.FK_HeadNumberID = intHeadNumberID AndAlso x.IsActive = True).Count

            If lnqVASServiceCount > 0 Then
                Bootstrap_Panel.ShowMessage("شماره سرویس باید برای کلید پویا منحصر بفرد باشد", True)
                Return
            End If
        End If



        Dim qryVASService As New BusinessObject.VAS.dstVASServiceTableAdapters.QueriesTableAdapter

        Dim intVASServiceID As Integer = qryVASService.spr_VASService_Insert(intOwnerUserID, intAggregatorID, intHeadNumberID, intCategoryVASServiceID, blnIsPardis, strtheWholeNumber, blnIsServiceActive, strServiceName, strRemarks, strTextHeader, strTextFooter, strTextWelcome, strTextGoodbye, sngServicePrice, sngIMIEnduserTariff, strAggergatorServiceID, True, osctUserInfo.ID, Date.Now)

        Dim qryVASServiceOnDemand As New BusinessObject.VAS.dstVASServiceOnDemandTableAdapters.QueriesTableAdapter
        qryVASServiceOnDemand.spr_VASServiceOnDemand_Insert(intVASServiceID, inttheTypeDemand, strtheKeyDemand, True)

        Dim qryVASServiceOnDemandSystematic As New BusinessObject.VAS.dstVASServiceOnDemandSystematicTableAdapters.QueriesTableAdapter
        qryVASServiceOnDemandSystematic.tbl_VASServiceOnDemandSystematic_Insert(intVASServiceID, strWebServicePath, strWebServiceMethod, strWebServiceUsername, strWebServicePassword, strWebServiceParameter)


        Response.Redirect(ViewState("BackPage") & "?Save=OK")


    End Sub

    Private Sub cmbAggregator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAggregator.SelectedIndexChanged, rdoPardisNo.CheckedChanged, rdoPardisYes.CheckedChanged

        cmbtheWholeNumbers.Items.Clear()

        If cmbAggregator.SelectedIndex <> 0 Then
            Dim intAggregatorID As Integer = cmbAggregator.SelectedValue
            Dim blnIsPardis As Boolean = rdoPardisYes.Checked

            Dim odbVAS As New BusinessObject.Context.dbVASEntities
            Dim lnqHeadNumber = odbVAS.tbl_HeadNumber.Where(Function(x) x.FK_AggregatorID = intAggregatorID AndAlso x.IsParisType = blnIsPardis)

            If blnIsPardis = True Then
                lnqHeadNumber = lnqHeadNumber.OrderBy(Function(x) x.tbl_Aggregators.PardisPreNumber & x.theNumber)

            Else
                lnqHeadNumber = lnqHeadNumber.OrderBy(Function(x) x.tbl_Aggregators.IMIPreNumber & x.theNumber)
            End If

            For Each lnqHeadNumberItem In lnqHeadNumber
                If blnIsPardis = True Then
                    cmbtheWholeNumbers.Items.Add(New ListItem(lnqHeadNumberItem.tbl_Aggregators.PardisPreNumber & lnqHeadNumberItem.theNumber, lnqHeadNumberItem.ID))
                Else
                    cmbtheWholeNumbers.Items.Add(New ListItem(lnqHeadNumberItem.tbl_Aggregators.IMIPreNumber & lnqHeadNumberItem.theNumber, lnqHeadNumberItem.ID))
                End If
            Next lnqHeadNumberItem

        End If

        cmbtheWholeNumbers.Items.Insert(0, "(انتخاب نمایید)")
        cmbtheWholeNumbers.SelectedIndex = 0

        txtIMIEnduserTariff.Enabled = rdoPardisNo.Checked

        If cmbtheTypeDemand.SelectedValue = "3" Then 'Dynamic Key
            txtKeyDemand.Enabled = False
        Else
            txtKeyDemand.Enabled = True
        End If

    End Sub
End Class
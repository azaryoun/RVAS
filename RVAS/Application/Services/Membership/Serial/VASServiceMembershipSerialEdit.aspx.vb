
Imports Service.Security
Public Class VASServiceMembershipSerialEdit
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


            If MyBase.Session("inttheKey") Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))


            Dim OdbVas As New BusinessObject.Context.dbVASEntities
            Dim lnqVASServiceMembershipSerial = OdbVas.tbl_VASServiceMembership.Where(Function(x) x.FK_VASServiceID = inttheKey AndAlso x.IsNewsContent = False).FirstOrDefault

            If lnqVASServiceMembershipSerial Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim lnqCategoryVASService = OdbVas.tbl_CategoryVASService.OrderBy(Function(x) x.Title)

            For Each lnqCategoryVASServiceItem In lnqCategoryVASService
                cmbCategoryVASService.Items.Add(New ListItem(lnqCategoryVASServiceItem.Title, lnqCategoryVASServiceItem.ID))
            Next lnqCategoryVASServiceItem


            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

            With lnqVASServiceMembershipSerial
                txtAggergator.Text = .tbl_VASService.tbl_Aggregators.Name

                If .tbl_VASService.IsPardis = True Then
                    txtIsParis.Text = "پردیس"
                    txtIMIEnduserTariff.Enabled = False
                    txtIMIEnduserTariff.Text = ""
                Else
                    txtIsParis.Text = "IMI"
                    txtIMIEnduserTariff.Text = .tbl_VASService.IMIEnduserTariff
                    txtIMIEnduserTariff.Enabled = True
                End If


                txtKeySubscription.Text = .SubscriptionKey
                txtKeyUnsubscription.Text = .UnsubscriptionKey
                txtRemarks.Text = .tbl_VASService.Remarks
                txtServiceID.Text = .tbl_VASService.AggergatorServiceID
                txtServiceName.Text = .tbl_VASService.ServiceName
                txtServicePrice.Text = .tbl_VASService.ServicePrice
                txtTextFooter.Text = .tbl_VASService.FooterText
                txtTextGoodbye.Text = .tbl_VASService.GoodbyeText
                txtTextHeader.Text = .tbl_VASService.HeaderText
                txtTextWelcome.Text = .tbl_VASService.WelcomeText
                txttheWholeNumber.Text = .tbl_VASService.theWholeNumber
                txtUserOwner.Text = .tbl_VASService.tbl_User.Username & " (" & .tbl_VASService.tbl_User.FName & " " & .tbl_VASService.tbl_User.LName & ")"
                cmbCategoryVASService.SelectedValue = .tbl_VASService.FK_VASServiceCategoryID
                rdoServiceActiveYes.Checked = .tbl_VASService.IsActive
                rdoServiceActiveNo.Checked = Not (.tbl_VASService.IsActive)



            End With

        End If



    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))

        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim odbVAS As New BusinessObject.Context.dbVASEntities

        Dim strTextHeader As String = txtTextHeader.Text.Trim
        Dim strTextFooter As String = txtTextFooter.Text.Trim
        Dim strTextWelcome As String = txtTextWelcome.Text.Trim
        Dim strTextGoodbye As String = txtTextGoodbye.Text.Trim
        Dim sngServicePrice As Single = Val(txtServicePrice.Text.Replace(",", ""))

        Dim sngIMIEnduserTariff? As Single = Nothing

        If txtIMIEnduserTariff.Enabled = True Then
            sngIMIEnduserTariff = Val(txtIMIEnduserTariff.Text.Replace(",", ""))
        End If

        Dim strKeySubscription As String = txtKeySubscription.Text.Trim
        Dim strKeyUnsubscription As String = txtKeyUnsubscription.Text.Trim
        Dim strAggergatorServiceID As String = txtServiceID.Text.Trim
        Dim strServiceName As String = txtServiceName.Text.Trim
        Dim intCategoryVASServiceID As Integer = cmbCategoryVASService.SelectedValue
        Dim blnIsServiceActive As Boolean = rdoServiceActiveYes.Checked
        Dim strRemarks As String = txtRemarks.Text.Trim

        Dim lnqVASServiceHeaderNumberID = odbVAS.tbl_VASService.Where(Function(x) x.ID = inttheKey).Select(Function(x) x.FK_HeadNumberID).FirstOrDefault


        Dim lnqVASServiceCount As Integer = 0

        lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.ServiceName = strServiceName AndAlso x.ID <> inttheKey).Count

        If lnqVASServiceCount > 0 Then
            Bootstrap_Panel.ShowMessage("عنوان سرویس تکراری است", True)
            Return
        End If

        lnqVASServiceCount = 0
        lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.AggergatorServiceID = strAggergatorServiceID AndAlso x.ID <> inttheKey).Count

        If lnqVASServiceCount > 0 Then
            Bootstrap_Panel.ShowMessage("Service ID تکراری است", True)
            Return
        End If


        lnqVASServiceCount = 0
        lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.FK_HeadNumberID = lnqVASServiceHeaderNumberID AndAlso (x.tbl_VASServiceMembership.SubscriptionKey = strKeySubscription OrElse x.tbl_VASServiceMembership.UnsubscriptionKey = strKeySubscription OrElse If(x.tbl_VASServiceOnDemand.theType = 1 AndAlso x.tbl_VASServiceOnDemand.theKey = strKeySubscription, True, False) OrElse If(x.tbl_VASServiceOnDemand.theType = 2 AndAlso x.tbl_VASServiceOnDemand.theKey.StartsWith(strKeySubscription) = True, True, False)) AndAlso x.IsActive = True AndAlso x.ID <> inttheKey).Count


        If lnqVASServiceCount > 0 Then
            Bootstrap_Panel.ShowMessage("کلید مشترک شدن روی این سرشماره تکراری است", True)
            Return
        End If


        lnqVASServiceCount = 0
        lnqVASServiceCount = odbVAS.tbl_VASService.Where(Function(x) x.FK_HeadNumberID = lnqVASServiceHeaderNumberID AndAlso (x.tbl_VASServiceMembership.UnsubscriptionKey = strKeyUnsubscription OrElse x.tbl_VASServiceMembership.SubscriptionKey = strKeyUnsubscription OrElse If(x.tbl_VASServiceOnDemand.theType = 1 AndAlso x.tbl_VASServiceOnDemand.theKey = strKeyUnsubscription, True, False) OrElse If(x.tbl_VASServiceOnDemand.theType = 2 AndAlso x.tbl_VASServiceOnDemand.theKey.StartsWith(strKeyUnsubscription) = True, True, False)) AndAlso x.IsActive = True AndAlso x.ID <> inttheKey).Count


        If lnqVASServiceCount > 0 Then
            Bootstrap_Panel.ShowMessage("کلید لغو اشتراک روی این سرشماره تکراری است", True)
            Return
        End If


        Dim qryVASService As New BusinessObject.VAS.dstVASServiceTableAdapters.QueriesTableAdapter

        qryVASService.spr_VASService_Update(inttheKey, intCategoryVASServiceID, blnIsServiceActive, strServiceName, strRemarks, strTextHeader, strTextFooter, strTextWelcome, strTextGoodbye, sngServicePrice, sngIMIEnduserTariff, strAggergatorServiceID)


        Dim qryVASServiceMembership As New BusinessObject.VAS.dstVASServiceMembershipTableAdapters.QueriesTableAdapter
        qryVASServiceMembership.spr_VASServiceMembership_Update(inttheKey, strKeySubscription, strKeyUnsubscription)


        Response.Redirect(ViewState("BackPage") & "?Edit=OK")


    End Sub

End Class
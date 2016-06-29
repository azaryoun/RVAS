Imports Service.Security
Public Class ContentOnDemandManualNew
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Panel.ShowSave = True
        Bootstrap_Panel.ShowCancel = True
        Bootstrap_Panel.Enable_Save_Client_Validate = True
        Bootstrap_Panel.ClearMessage()

        If Page.IsPostBack = False Then


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


            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)


            Bootstrap_PersianDateTimePicker_ValidFrom.GergorainDateTime = Date.Now
            Bootstrap_PersianDateTimePicker1_ValidTo.GergorainDateTime = Date.Now.AddDays(1)


            Dim odbVAS As New BusinessObject.Context.dbVASEntities
            Dim lnqVASServiceOnDemnadManual = odbVAS.tbl_VASServiceOnDemand.Where(Function(x) x.IsSystematic = False AndAlso x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)

            For Each lnqVASServiceOnDemnadManualItem In lnqVASServiceOnDemnadManual

                cmbVASServices.Items.Add(New ListItem(lnqVASServiceOnDemnadManualItem.tbl_VASService.ServiceName & "(" & lnqVASServiceOnDemnadManualItem.tbl_VASService.AggergatorServiceID & ")", lnqVASServiceOnDemnadManualItem.FK_VASServiceID))

            Next lnqVASServiceOnDemnadManualItem
            cmbVASServices.Items.Insert(0, "(انتخاب نمایید)")
            cmbVASServices.SelectedIndex = 0




        End If




    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim odbVAS As New BusinessObject.Context.dbVASEntities

        Dim dteValidFrom As Date = Bootstrap_PersianDateTimePicker_ValidFrom.GergorainDateTime
        Dim dteValidTo As Date = Bootstrap_PersianDateTimePicker1_ValidTo.GergorainDateTime

        If dteValidFrom > dteValidTo Then
            Bootstrap_Panel.ShowMessage("تاریخ شروع اعتبار نباید بعد از تاریخ پایان اعتبار باشد", True)
            Return
        End If

        Dim strthtText As String = txttheText.Text.Trim
        Dim blnIsActive As Boolean = rdoActiveYes.Checked
        Dim intVASServiceID As Integer = cmbVASServices.SelectedValue

        If blnIsActive = True Then

            Dim lnqVASServiceOnDemandManualContent_Count = odbVAS.tbl_VASServiceOnDemandManualContent.Where(Function(x) x.FK_VASServiceID = intVASServiceID AndAlso x.IsActive = True AndAlso ((x.ValidTo >= dteValidTo AndAlso x.ValidFrom <= dteValidTo) OrElse (x.ValidTo >= dteValidFrom AndAlso x.ValidFrom <= dteValidFrom) OrElse (x.ValidFrom >= dteValidFrom AndAlso x.ValidFrom <= dteValidTo AndAlso x.ValidTo >= dteValidFrom AndAlso x.ValidTo <= dteValidTo))).Count
            If lnqVASServiceOnDemandManualContent_Count <> 0 Then
                Bootstrap_Panel.ShowMessage("بازه ی اعتبار محتوای تعریف شده برای این سرویس با یک محتوای دیگر تداخل دارد", True)
                Return
            End If


        End If


        odbVAS.spr_VASServiceOnDemandManualContent_Insert(intVASServiceID, dteValidFrom, dteValidTo, strthtText, blnIsActive, Date.Now)

        Response.Redirect(ViewState("BackPage") & "?Save=OK")


    End Sub

End Class
Imports Service.Security
Public Class ContentOnDemandManualEdit
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

            If MyBase.Session("inttheKey") Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))
            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
            Dim OdbVas As New BusinessObject.Context.dbVASEntities
            Dim lnqVASServiceOnDemandManualContent = OdbVas.tbl_VASServiceOnDemandManualContent.Where(Function(x) x.ID = inttheKey).FirstOrDefault

            If lnqVASServiceOnDemandManualContent Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If



            With lnqVASServiceOnDemandManualContent

                Bootstrap_PersianDateTimePicker_ValidFrom.GergorainDateTime = .ValidFrom
                Bootstrap_PersianDateTimePicker1_ValidTo.GergorainDateTime = .ValidTo
                txttheText.Text = .theText
                txtVASServiceOnDemandManual.Text = .tbl_VASServiceOnDemand.tbl_VASService.ServiceName & " (" & .tbl_VASServiceOnDemand.tbl_VASService.AggergatorServiceID & ")"
                rdoActiveNo.Checked = Not (.IsActive)
                rdoActiveYes.Checked = .IsActive


            End With





        End If




    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim odbVAS As New BusinessObject.Context.dbVASEntities
        Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))

        Dim dteValidFrom As Date = Bootstrap_PersianDateTimePicker_ValidFrom.GergorainDateTime
        Dim dteValidTo As Date = Bootstrap_PersianDateTimePicker1_ValidTo.GergorainDateTime

        If dteValidFrom > dteValidTo Then
            Bootstrap_Panel.ShowMessage("تاریخ شروع اعتبار نباید بعد از تاریخ پایان اعتبار باشد", True)
            Return
        End If

        Dim strthtText As String = txttheText.Text.Trim
        Dim blnIsActive As Boolean = rdoActiveYes.Checked

        Dim lnqVASServiceID = odbVAS.tbl_VASServiceOnDemandManualContent.Where(Function(x) x.ID = inttheKey).FirstOrDefault.FK_VASServiceID

        If blnIsActive = True Then
            Dim lnqVASServiceOnDemandManualContent_Count = odbVAS.tbl_VASServiceOnDemandManualContent.Where(Function(x) x.ID <> inttheKey AndAlso x.FK_VASServiceID = lnqVASServiceID AndAlso x.IsActive = True AndAlso ((x.ValidTo >= dteValidTo AndAlso x.ValidFrom <= dteValidTo) OrElse (x.ValidTo >= dteValidFrom AndAlso x.ValidFrom <= dteValidFrom) OrElse (x.ValidFrom >= dteValidFrom AndAlso x.ValidFrom <= dteValidTo AndAlso x.ValidTo >= dteValidFrom AndAlso x.ValidTo <= dteValidTo))).Count

            If lnqVASServiceOnDemandManualContent_Count <> 0 Then
                Bootstrap_Panel.ShowMessage("بازه ی اعتبار محتوای تعریف شده برای این سرویس با یک محتوای دیگر تداخل دارد", True)
                Return
            End If


        End If

        odbVAS.spr_VASServiceOnDemandManualContent_Update(inttheKey, dteValidFrom, dteValidTo, strthtText, blnIsActive)

        Response.Redirect(ViewState("BackPage") & "?Edit=OK")


    End Sub

End Class
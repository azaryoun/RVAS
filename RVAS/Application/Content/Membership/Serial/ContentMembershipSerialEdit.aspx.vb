Imports Service.Security
Public Class ContentMembershipSerialEdit
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
            Dim lnqVASServiceMembershipSerialContentHeader = OdbVas.tbl_VASServiceMembershipSerialContentHeader.Where(Function(x) x.FK_VASServiceID = inttheKey).FirstOrDefault

            If lnqVASServiceMembershipSerialContentHeader Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If


            With lnqVASServiceMembershipSerialContentHeader
                txtVASServiceSerial.Text = .tbl_VASServiceMembership.tbl_VASService.ServiceName & " (" & lnqVASServiceMembershipSerialContentHeader.tbl_VASServiceMembership.tbl_VASService.AggergatorServiceID & ")"
                txttheName.Text = .theName
                Bootstrap_PersianDateTimePicker_StartFrom.GergorainDateTime = .StratFrom
                Bootstrap_PersianDateTimePicker_EndAt.GergorainDateTime = .EndAt
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


        Dim dteStartDate As Date = Bootstrap_PersianDateTimePicker_StartFrom.GergorainDateTime
        Dim dteEndDate As Date = Bootstrap_PersianDateTimePicker_EndAt.GergorainDateTime
        If dteStartDate > dteEndDate Then
            Bootstrap_Panel.ShowMessage("تاریخ شروع اعتبار از تاریخ پایان بزرگتر است", True)
            Return

        End If

        Dim blnIsActive As Boolean = rdoActiveYes.Checked
        Dim strtheName As String = txttheName.Text.Trim

        odbVAS.spr_VASServiceMembershipSerialContentHeader_Update(inttheKey, strtheName, dteStartDate, dteEndDate, blnIsActive)


        Response.Redirect(ViewState("BackPage") & "?Edit=OK")


    End Sub

End Class
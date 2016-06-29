Imports Service.Security
Public Class ContentMembershipSerialNew
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


            Bootstrap_PersianDateTimePicker_StartFrom.GergorainDateTime = Date.Now.Date
            Bootstrap_PersianDateTimePicker_EndAt.GergorainDateTime = Date.Now.AddYears(1).Date



            Dim odbVAS As New BusinessObject.Context.dbVASEntities
            Dim lnqVASServiceMembership = odbVAS.tbl_VASServiceMembership.Where(Function(x) x.IsNewsContent = False AndAlso x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID AndAlso x.tbl_VASServiceMembershipSerialContentHeader Is Nothing)

            For Each lnqVASServiceMembershipItem In lnqVASServiceMembership

                cmbVASServices.Items.Add(New ListItem(lnqVASServiceMembershipItem.tbl_VASService.ServiceName & "(" & lnqVASServiceMembershipItem.tbl_VASService.AggergatorServiceID & ")", lnqVASServiceMembershipItem.FK_VASServiceID))

            Next lnqVASServiceMembershipItem
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

        Dim dteStartDate As Date = Bootstrap_PersianDateTimePicker_StartFrom.GergorainDateTime
        Dim dteEndDate As Date = Bootstrap_PersianDateTimePicker_EndAt.GergorainDateTime
        If dteStartDate > dteEndDate Then
            Bootstrap_Panel.ShowMessage("تاریخ شروع اعتبار از تاریخ پایان بزرگتر است", True)
            Return

        End If

        Dim blnIsActive As Boolean = rdoActiveYes.Checked
        Dim strtheName As String = txttheName.Text.Trim
        Dim intVASServiceID As Integer = cmbVASServices.SelectedValue

        odbVAS.spr_VASServiceMembershipSerialContentHeader_Insert(intVASServiceID, strtheName, dteStartDate, dteEndDate, blnIsActive, Date.Now)


        Response.Redirect(ViewState("BackPage") & "?Save=OK")


    End Sub

End Class
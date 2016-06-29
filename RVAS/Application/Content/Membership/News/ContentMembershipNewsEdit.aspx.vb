Imports Service.Security
Public Class ContentMembershipNewsEdit
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
            Dim lnqNewContent = OdbVas.tbl_NewsContent.Where(Function(x) x.ID = inttheKey).FirstOrDefault

            If lnqNewContent Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            With lnqNewContent
                txttheText.Text = .theText
                Dim dteSendDateTime As Date = .SendDate
                Bootstrap_PersianDateTimePicker_SendDateTime.GergorainDateTime = dteSendDateTime.Add(.SendTime)
            End With


            Dim lnqVASServicesNews = OdbVas.tbl_VASServiceMembership.Where(Function(x) x.IsNewsContent = True)
            lnqVASServicesNews = lnqVASServicesNews.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
            lnqVASServicesNews = lnqVASServicesNews.OrderBy(Function(x) x.tbl_VASService.ServiceName)


            Dim strchklstVASServices As String = ""

            For Each lnqVASServicesNewsItem In lnqVASServicesNews

                Dim lnqVASServiceMembership_NewsContent = OdbVas.tbl_VASServiceMembership_NewsContent.Where(Function(x) x.FK_NewsContentID = inttheKey AndAlso x.FK_VASServiceID = lnqVASServicesNewsItem.FK_VASServiceID).FirstOrDefault
                If lnqVASServiceMembership_NewsContent Is Nothing Then
                    strchklstVASServices &= "<div class='checkbox'><label><input type='checkbox' value='" & lnqVASServicesNewsItem.FK_VASServiceID & "' name='chklstVASService" & lnqVASServicesNewsItem.FK_VASServiceID & "' /> &nbsp;&nbsp;&nbsp; " & lnqVASServicesNewsItem.tbl_VASService.ServiceName & " (" & lnqVASServicesNewsItem.tbl_VASService.AggergatorServiceID & ") </label></div>"
                Else
                    strchklstVASServices &= "<div class='checkbox'><label><input type='checkbox'  checked='checked' value='" & lnqVASServicesNewsItem.FK_VASServiceID & "' name='chklstVASService" & lnqVASServicesNewsItem.FK_VASServiceID & "' /> &nbsp;&nbsp;&nbsp; " & lnqVASServicesNewsItem.tbl_VASService.ServiceName & " (" & lnqVASServicesNewsItem.tbl_VASService.AggergatorServiceID & ") </label></div>"
                End If

            Next lnqVASServicesNewsItem

            divchklstVASServices.InnerHtml = strchklstVASServices


        End If




    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim odbVAS As New BusinessObject.Context.dbVASEntities
        Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))

        Dim dteSendDateTime As Date = Bootstrap_PersianDateTimePicker_SendDateTime.GergorainDateTime

        Dim strthtText As String = txttheText.Text.Trim
        odbVAS.spr_NewsContent_Update(inttheKey, strthtText, dteSendDateTime.Date, dteSendDateTime.TimeOfDay)

        odbVAS.spr_VASServiceMembership_NewsContent_Delete(inttheKey)

        For i As Integer = 0 To Request.Form.Keys.Count - 1
            If Request.Form.Keys(i).StartsWith("chklstVASService") = True Then
                odbVAS.spr_VASServiceMembership_NewsContent_Insert(CInt(Request.Form(i)), inttheKey)
            End If
        Next i

        Response.Redirect(ViewState("BackPage") & "?Edit=OK")


    End Sub

End Class
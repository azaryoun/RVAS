Imports Service.Security
Public Class ContentMembershipNewsNew
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


            Bootstrap_PersianDateTimePicker_SendDateTime.GergorainDateTime = Date.Now.AddHours(1)

            Dim odbVAS As New BusinessObject.Context.dbVASEntities
            Dim lnqVASServicesNews = odbVAS.tbl_VASServiceMembership.Where(Function(x) x.IsNewsContent = True)
            lnqVASServicesNews = lnqVASServicesNews.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
            lnqVASServicesNews = lnqVASServicesNews.OrderBy(Function(x) x.tbl_VASService.ServiceName)


            Dim strchklstVASServices As String = ""

            For Each lnqVASServicesNewsItem In lnqVASServicesNews

                strchklstVASServices &= "<div class='checkbox'><label><input type='checkbox' value='" & lnqVASServicesNewsItem.FK_VASServiceID & "' name='chklstVASService" & lnqVASServicesNewsItem.FK_VASServiceID & "' /> &nbsp;&nbsp;&nbsp; " & lnqVASServicesNewsItem.tbl_VASService.ServiceName & " (" & lnqVASServicesNewsItem.tbl_VASService.AggergatorServiceID & ") </label></div>"
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

        Dim dteSendDateTime As Date = Bootstrap_PersianDateTimePicker_SendDateTime.GergorainDateTime

        If dteSendDateTime < Date.Now Then
            Bootstrap_Panel.ShowMessage("تاریخ وارد شده نامعتبر است", True)
            Return
        End If

        Dim strthtText As String = txttheText.Text.Trim

        Dim intNewsContentID As Integer = odbVAS.spr_NewsContent_Insert(osctUserInfo.ID, strthtText, dteSendDateTime.Date, dteSendDateTime.TimeOfDay, Date.Now).FirstOrDefault



        For i As Integer = 0 To Request.Form.Keys.Count - 1
            If Request.Form.Keys(i).StartsWith("chklstVASService") = True Then
                odbVAS.spr_VASServiceMembership_NewsContent_Insert(CInt(Request.Form(i)), intNewsContentID)
            End If
        Next i


        Response.Redirect(ViewState("BackPage") & "?Save=OK")


    End Sub

End Class
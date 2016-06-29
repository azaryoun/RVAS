Imports Service.Security
Public Class ChangePasswordEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Panel.ShowSave = True
        Bootstrap_Panel.ShowUp = True
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
            txtUsername.Text = osctUserInfo.Username



        End If
    End Sub

    Private Sub Bootstrap_Panel_Panel_UP_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Up_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim strCurrentPassword As String = EncDec.Encrypt(txtPasswordCurrent.Text)


        Dim odbVAS As New BusinessObject.Context.dbVASEntities
        Dim lnqUser = odbVAS.tbl_User.Where(Function(x) x.Password = strCurrentPassword).Count

        If lnqUser = 0 Then
            Bootstrap_Panel.ShowMessage("رمز کنونی نامعتبر است", True)
            Return
        End If

        Dim strPasswordNew As String = txtPasswordNew.Text
        Dim strPasswordNewRetype As String = txtPasswordNewRetype.Text

        If strPasswordNew <> strPasswordNewRetype Then
            Bootstrap_Panel.ShowMessage("رمز جدید با ورود مجدد آن همخوانی ندارد", True)
            Return
        End If

        strPasswordNew = EncDec.Encrypt(strPasswordNew)
        Dim qryUser As New BusinessObject.Administration.dstUserTableAdapters.QueriesTableAdapter

        qryUser.spr_User_Password_Update(osctUserInfo.ID, strPasswordNew)
        Bootstrap_Panel.ShowMessage("گذر واژه باموفقیت تغییر کرد", False)



    End Sub
End Class
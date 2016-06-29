Imports Service.Security



Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Callout.Display = False
        If Page.IsPostBack = False Then
            Me.Title = "پنل مدیریت ارزش افزوده شرکت رهیاب رایانه گستر"
        End If

    End Sub

    Protected Sub btnSignIn_Click(sender As Object, e As EventArgs) Handles btnSignIn.Click

        Dim strUsername As String = txtUsername.Text.Trim()
        Dim strPassword As String = txtPassword.Text
        strPassword = EncDec.Encrypt(strPassword)
        Dim OdbVAS As New BusinessObject.Context.dbVASEntities

        Dim lUserLogin = OdbVAS.tbl_User.Where(Function(l) l.Username = strUsername AndAlso l.Password = strPassword AndAlso l.IsActive = True).Select(Function(l) New With {l.ID, l.FName, l.LName, l.Username, l.IsDataAdmin, l.IsItemAdmin, l.IsMale, l.Mobile, l.UserPhoto, l.IsReal, l.STime}).FirstOrDefault()


        If lUserLogin Is Nothing Then

            Bootstrap_Callout.Display = True
            Bootstrap_Callout.ShowWarning = True
            Bootstrap_Callout.Message = "نام کاربری یا گذرواژه نامعتبر است"

        Else

            Dim ostcUserInfo As Service.Security.stcUserInfo
            ostcUserInfo.ID = lUserLogin.ID
            ostcUserInfo.FName = lUserLogin.FName
            ostcUserInfo.LName = lUserLogin.LName
            ostcUserInfo.Username = lUserLogin.Username
            ostcUserInfo.UserPhoto = lUserLogin.UserPhoto
            ostcUserInfo.Mobile = lUserLogin.Mobile
            ostcUserInfo.IsItemAdmin = lUserLogin.IsItemAdmin
            ostcUserInfo.IsDataAdmin = lUserLogin.IsDataAdmin
            ostcUserInfo.IsMale = lUserLogin.IsMale
            ostcUserInfo.IsReal = lUserLogin.IsReal
            ostcUserInfo.STime = lUserLogin.STime

            Session("stcUserInfo") = ostcUserInfo
            FormsAuthentication.SetAuthCookie("VAS_Username", False)
            Response.Redirect("~/Application/StartPage.aspx")
        End If


    End Sub
End Class
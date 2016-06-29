Imports Service.Security
Public Class UserProfile
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



            Dim inttheKey As Integer = osctUserInfo.ID



            Dim OdbVas As New BusinessObject.Context.dbVASEntities
            Dim lnqUser = OdbVas.tbl_User.Where(Function(x) x.ID = inttheKey).FirstOrDefault

            If lnqUser Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If


            With lnqUser
                lblFullName.Text = .FName & " " & .LName
                If .UserPhoto IsNot Nothing Then
                    imgUserPhoto.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(.UserPhoto)
                End If
                txtNationalID.Text = .NationalID

                If .IsDataAdmin = True AndAlso .IsItemAdmin = True Then
                    txtAccessType.Text = "Full"
                ElseIf .IsDataAdmin = True Then
                    txtAccessType.Text = "Data"
                ElseIf .IsItemAdmin = True Then
                    txtAccessType.Text = "Item"
                Else
                    txtAccessType.Text = "Normal"
                End If


                txtAddress.Text = .Address
                txtMobileNo.Text = .Mobile
                txtNationalCode.Text = .NationalNo
                txtTelephone.Text = .Tel
                txtUsername.Text = .Username
                rdoIsRealNo.Checked = Not (.IsReal)
                rdoIsRealYes.Checked = .IsReal
                rdoSexFemale.Checked = Not (.IsMale)
                rdoSexMale.Checked = .IsMale




            End With


        End If
    End Sub

    Private Sub Bootstrap_Panel_Panel_Up_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Up_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

        Dim inttheKey As Integer = osctUserInfo.ID


        Dim strAddress As String = txtAddress.Text.Trim
        Dim strMobileNo As String = txtMobileNo.Text.Trim
        Dim strNationalNo As String = txtNationalCode.Text
        Dim strTelephone As String = txtTelephone.Text

        Dim blnIsMale As Boolean = rdoSexMale.Checked
        Dim blnIsReal As Boolean = rdoIsRealYes.Checked


        Dim OdbVas As New BusinessObject.Context.dbVASEntities



        Dim qryUser As New BusinessObject.Administration.dstUserTableAdapters.QueriesTableAdapter
        OdbVas.spr_User_Profile_Update(inttheKey, blnIsMale, strTelephone, strMobileNo, Date.Now, osctUserInfo.ID, strNationalNo, strAddress, blnIsReal)


        Dim blnUserPhotoChanged As Boolean = False

        Dim arrUserPhoto() As Byte = Nothing

        If fleUserPhoto.PostedFile IsNot Nothing AndAlso fleUserPhoto.PostedFile.ContentLength <> 0 Then

            If fleUserPhoto.PostedFile.ContentType.ToLower.IndexOf("jpeg") = -1 Then
                Bootstrap_Panel.ShowMessage("فرمت تصویر باید jpg باشد", True)
                Return
            End If


            Dim strmUserPhoto As IO.Stream = fleUserPhoto.PostedFile.InputStream
            ReDim arrUserPhoto(strmUserPhoto.Length)
            strmUserPhoto.Read(arrUserPhoto, 0, arrUserPhoto.Length)

            blnUserPhotoChanged = True
        End If

        If blnUserPhotoChanged = True Then
            qryUser.spr_User_Photo_Update(inttheKey, arrUserPhoto)
        End If


        Bootstrap_Panel.ShowMessage("پروفایل شما با موفقیت ویرایش شد", False)


    End Sub
End Class
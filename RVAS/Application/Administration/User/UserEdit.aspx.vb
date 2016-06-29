Imports Service.Security
Public Class UserEdit
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
            Dim lnqUser = OdbVas.tbl_User.Where(Function(x) x.ID = inttheKey).FirstOrDefault

            If lnqUser Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

            If osctUserInfo.IsItemAdmin = False Then
                If lnqUser.IsItemAdmin = True Then
                    Response.Redirect(ViewState("BackPage") & "?NoAccess=Yes")
                    Return
                End If
            End If
            If osctUserInfo.IsDataAdmin = False Then
                If lnqUser.IsDataAdmin = True Then
                    Response.Redirect(ViewState("BackPage") & "?NoAccess=Yes")
                    Return
                End If
            End If

            With lnqUser
                txtAddress.Text = .Address
                txtFName.Text = .FName
                txtLName.Text = .LName
                txtMobileNo.Text = .Mobile
                txtNationalCode.Text = .NationalNo
                txtNationalID.Text = .NationalID
                txtPassword.Text = EncDec.Decrypt(.Password)
                txtTelephone.Text = .Tel
                txtUsername.Text = .Username
                rdoActiveNo.Checked = Not (.IsActive)
                rdoActiveYes.Checked = .IsActive
                rdoIsRealNo.Checked = Not (.IsReal)
                rdoIsRealYes.Checked = .IsReal
                rdoSexFemale.Checked = Not (.IsMale)
                rdoSexMale.Checked = .IsMale

                If .UserPhoto IsNot Nothing Then
                    imgUserPhoto.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(.UserPhoto)
                End If

                If .IsDataAdmin = True AndAlso .IsItemAdmin = True Then
                    cmbAccessType.SelectedValue = 4
                ElseIf .IsDataAdmin = True Then
                    cmbAccessType.SelectedValue = 3
                ElseIf .IsItemAdmin = True Then
                    cmbAccessType.SelectedValue = 2
                Else
                    cmbAccessType.SelectedValue = 1
                End If

            End With




            Dim strchklstMenuLeaves As String = ""

            If osctUserInfo.IsDataAdmin = True Then
                Dim lnqAccessgroup = OdbVas.tbl_Accessgroup.OrderBy(Function(x) x.Desp)

                For Each lnqAccessgroupItem In lnqAccessgroup

                    Dim lnqAccessgroupUser = OdbVas.tbl_AccessgroupUser.Where(Function(x) x.FK_UserID = inttheKey AndAlso x.FK_AccessGroupID = lnqAccessgroupItem.ID).Count

                    If lnqAccessgroupUser = 0 Then

                        strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & lnqAccessgroupItem.ID & "' name='chklstGroup" & lnqAccessgroupItem.ID & "' /> &nbsp;&nbsp;&nbsp; " & lnqAccessgroupItem.Desp & "</label></div>"

                    Else

                        strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & lnqAccessgroupItem.ID & "' name='chklstGroup" & lnqAccessgroupItem.ID & "' checked='checked'/> &nbsp;&nbsp;&nbsp; " & lnqAccessgroupItem.Desp & "</label></div>"

                    End If


                Next lnqAccessgroupItem

                divAccessgroup.InnerHtml = strchklstMenuLeaves

            Else
                Dim lnqAccessgroup = OdbVas.tbl_AccessgroupUser.Select(Function(x) New With {x.FK_AccessGroupID, x.tbl_Accessgroup.Desp}).OrderBy(Function(x) x.Desp)

                For Each lnqAccessgroupItem In lnqAccessgroup


                    Dim lnqAccessgroupUser = OdbVas.tbl_AccessgroupUser.Where(Function(x) x.FK_UserID = inttheKey AndAlso x.FK_AccessGroupID = lnqAccessgroupItem.FK_AccessGroupID).Count

                    If lnqAccessgroupUser = 0 Then

                        strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & lnqAccessgroupItem.FK_AccessGroupID & "' name='chklstGroup" & lnqAccessgroupItem.FK_AccessGroupID & "' /> &nbsp;&nbsp;&nbsp; " & lnqAccessgroupItem.Desp & "</label></div>"

                    Else

                        strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & lnqAccessgroupItem.FK_AccessGroupID & "' name='chklstGroup" & lnqAccessgroupItem.FK_AccessGroupID & "' checked='checked' /> &nbsp;&nbsp;&nbsp; " & lnqAccessgroupItem.Desp & "</label></div>"

                    End If

                Next lnqAccessgroupItem

                divAccessgroup.InnerHtml = strchklstMenuLeaves

                cmbAccessType.Items.RemoveAt(2) 'Erase Data
                cmbAccessType.Items.RemoveAt(2) 'Erase Full


                If osctUserInfo.IsItemAdmin = False Then
                    cmbAccessType.Items.RemoveAt(2) 'Erase Item
                End If

            End If


        End If
    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))

        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

        Dim strAddress As String = txtAddress.Text.Trim
        Dim strFName As String = txtFName.Text.Trim
        Dim strLName As String = txtLName.Text.Trim
        Dim strMobileNo As String = txtMobileNo.Text.Trim
        Dim strNationalNo As String = txtNationalCode.Text
        Dim strNationalID As String = txtNationalID.Text.Trim
        Dim strPassword As String = EncDec.Encrypt(txtPassword.Text)
        Dim strTelephone As String = txtTelephone.Text

        Dim blnIsActive As Boolean = rdoActiveYes.Checked
        Dim blnIsMale As Boolean = rdoSexMale.Checked
        Dim blnIsReal As Boolean = rdoIsRealYes.Checked

        Dim blnIsDataAdmin As Boolean = False
        Dim blnIsItemAdmin As Boolean = False

        If cmbAccessType.SelectedValue = 2 Then
            blnIsDataAdmin = False
            blnIsItemAdmin = True
        ElseIf cmbAccessType.SelectedValue = 3
            blnIsDataAdmin = True
            blnIsItemAdmin = False
        ElseIf cmbAccessType.SelectedValue = 4
            blnIsDataAdmin = True
            blnIsItemAdmin = True
        End If





        Dim qryUser As New BusinessObject.Administration.dstUserTableAdapters.QueriesTableAdapter
        qryUser.spr_User_Update(inttheKey, blnIsActive, strPassword, blnIsDataAdmin, blnIsItemAdmin, strFName, strLName, blnIsMale, strTelephone, strMobileNo, Date.Now, osctUserInfo.ID, strNationalID, strNationalNo, strAddress, blnIsReal)

        Dim qryAccessgroupUser As New BusinessObject.Administration.dstAccessgroupUserTableAdapters.QueriesTableAdapter
        qryAccessgroupUser.spr_AccessgroupUser_Delete(inttheKey)

        For i As Integer = 0 To Request.Form.Keys.Count - 2
            If Request.Form.Keys(i).StartsWith("chklstGroup") = True Then
                qryAccessgroupUser.spr_AccessgroupUser_Insert(inttheKey, CInt(Request.Form(i)))
            End If

        Next i


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

        Response.Redirect(ViewState("BackPage") & "?Edit=OK")



    End Sub
End Class
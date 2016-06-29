Imports Service.Security
Public Class UserNew
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


            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
            Dim odbVAS As New BusinessObject.Context.dbVASEntities

            Dim strchklstMenuLeaves As String = ""

            If osctUserInfo.IsDataAdmin = True Then
                Dim lnqAccessgroup = odbVAS.tbl_Accessgroup.OrderBy(Function(x) x.Desp)

                For Each lnqAccessgroupItem In lnqAccessgroup

                    strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & lnqAccessgroupItem.ID & "' name='chklstGroup" & lnqAccessgroupItem.ID & "' /> &nbsp;&nbsp;&nbsp; " & lnqAccessgroupItem.Desp & "</label></div>"
                Next lnqAccessgroupItem

                divAccessgroup.InnerHtml = strchklstMenuLeaves

            Else
                Dim lnqAccessgroup = odbVAS.tbl_AccessgroupUser.Select(Function(x) New With {x.FK_AccessGroupID, x.tbl_Accessgroup.Desp}).OrderBy(Function(x) x.Desp)

                For Each lnqAccessgroupItem In lnqAccessgroup

                    strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & lnqAccessgroupItem.FK_AccessGroupID & "' name='chklstGroup" & lnqAccessgroupItem.FK_AccessGroupID & "' /> &nbsp;&nbsp;&nbsp; " & lnqAccessgroupItem.Desp & "</label></div>"
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
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)


        Dim strAddress As String = txtAddress.Text.Trim
        Dim strFName As String = txtFName.Text.Trim
        Dim strLName As String = txtLName.Text.Trim
        Dim strMobileNo As String = txtMobileNo.Text.Trim
        Dim strNationalNo As String = txtNationalCode.Text
        Dim strNationalID As String = txtNationalID.Text.Trim
        Dim strPassword As String = EncDec.Encrypt(txtPassword.Text)
        Dim strTelephone As String = txtTelephone.Text
        Dim strUsername As String = txtUsername.Text.Trim

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


        Dim odbVAS As New BusinessObject.Context.dbVASEntities

        Dim lnqUser = odbVAS.tbl_User.Where(Function(x) x.Username = strUsername).Count

        If lnqUser <> 0 Then
            Bootstrap_Panel.ShowMessage("نام کاربری وارد شده تکراری است", True)
            Return
        End If

        Dim arrUserPhoto() As Byte = Nothing

        If fleUserPhoto.PostedFile IsNot Nothing AndAlso fleUserPhoto.PostedFile.ContentLength <> 0 Then

            If fleUserPhoto.PostedFile.ContentType.ToLower.IndexOf("jpeg") = -1 Then
                Bootstrap_Panel.ShowMessage("فرمت تصویر باید jpg باشد", True)
                Return
            End If


            Dim strmUserPhoto As IO.Stream = fleUserPhoto.PostedFile.InputStream
            ReDim arrUserPhoto(strmUserPhoto.Length)
            strmUserPhoto.Read(arrUserPhoto, 0, arrUserPhoto.Length)

        End If


        Dim qryUser As New BusinessObject.Administration.dstUserTableAdapters.QueriesTableAdapter
        Dim intUserID As Integer = qryUser.spr_User_Insert(strUsername, strPassword, blnIsActive, blnIsDataAdmin, blnIsItemAdmin, strFName, strLName, blnIsMale, strTelephone, strMobileNo, Date.Now, osctUserInfo.ID, strNationalID, strNationalNo, strAddress, arrUserPhoto, blnIsReal)


        For i As Integer = 0 To Request.Form.Keys.Count - 2
            If Request.Form.Keys(i).StartsWith("chklstGroup") = True Then
                Dim qryAccessgroupUser As New BusinessObject.Administration.dstAccessgroupUserTableAdapters.QueriesTableAdapter
                qryAccessgroupUser.spr_AccessgroupUser_Insert(intUserID, CInt(Request.Form(i)))
            End If

        Next i



        Response.Redirect(ViewState("BackPage") & "?Save=OK")



    End Sub
End Class
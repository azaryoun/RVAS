Public Class AccessgroupEdit
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
            Dim lnqAccessgroup = OdbVas.tbl_Accessgroup.Where(Function(x) x.ID = inttheKey).FirstOrDefault

            If lnqAccessgroup Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            With lnqAccessgroup
                txtTitle.Text = .Desp
            End With



            Dim tadpMenuLeafList As New BusinessObject.Administration.dstMenuTableAdapters.spr_Menu_Leaf_List_SelectTableAdapter
            Dim dtblMenuLeafList As BusinessObject.Administration.dstMenu.spr_Menu_Leaf_List_SelectDataTable = Nothing
            dtblMenuLeafList = tadpMenuLeafList.GetData()

            Dim strchklstMenuLeaves As String = ""

            For Each drwMeneLeafList As BusinessObject.Administration.dstMenu.spr_Menu_Leaf_List_SelectRow In dtblMenuLeafList.Rows
                Dim lnqAccessgroupMenu = OdbVas.tbl_AccessgroupMenu.Where(Function(x) x.FK_AccessGroupID = inttheKey AndAlso x.FK_MenuID = drwMeneLeafList.ID).Count
                If lnqAccessgroupMenu = 0 Then
                    strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "' /> &nbsp;&nbsp;&nbsp; <i class='" & drwMeneLeafList.IconStyle & "'></i> " & drwMeneLeafList.Menutitlepath & "</label></div>"
                Else
                    strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "' checked='checked' /> &nbsp;&nbsp;&nbsp; <i class='" & drwMeneLeafList.IconStyle & "'></i> " & drwMeneLeafList.Menutitlepath & "</label></div>"
                End If


            Next drwMeneLeafList

            divchklstMenuItems.InnerHtml = strchklstMenuLeaves




        End If
    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))


        Dim strTitle As String = txtTitle.Text.Trim
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

        Dim qryAccessgroup As New BusinessObject.Administration.dstAccessgroupTableAdapters.QueriesTableAdapter
        qryAccessgroup.spr_Accessgroup_Update(inttheKey, strTitle, Date.Now, osctUserInfo.ID)
        Dim qryAccessgroupMenu As New BusinessObject.Administration.dstAccessgroupMenuTableAdapters.QueriesTableAdapter

        qryAccessgroupMenu.spr_AccessgroupMenu_Delete(inttheKey)

        For i As Integer = 0 To Request.Form.Keys.Count - 2
            If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then
                qryAccessgroupMenu.spr_AccessgroupMenu_Insert(inttheKey, CInt(Request.Form(i)))
            End If

        Next i


        Response.Redirect(ViewState("BackPage") & "?Edit=OK")



    End Sub
End Class
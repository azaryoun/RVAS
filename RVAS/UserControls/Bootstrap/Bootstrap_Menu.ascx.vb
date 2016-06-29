Imports BusinessObject.Administration

Public Class Bootstrap_Menu
    Inherits System.Web.UI.UserControl
    Public strMenuText As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub BuildMenu(blnForItemAdmin As Boolean, Optional intUserID As Integer = 0, Optional intActiveMenuID As Integer = 0)
        strMenuText = "<ul class=""sidebar-menu"">"
        strMenuText &= "<li class=""header"">منوی اصلی</li>"
        Dim intAction As Integer = 2

        If blnForItemAdmin = True Then
            intAction = 1
        End If

        Dim tadpParentMenu As New BusinessObject.Administration.dstMenuTableAdapters.spr_Menu_Parent_SelectTableAdapter
        Dim dtblParentMenu As dstMenu.spr_Menu_Parent_SelectDataTable = Nothing
        dtblParentMenu = tadpParentMenu.GetData(intAction, intUserID)
        For Each drwParentMenu As dstMenu.spr_Menu_Parent_SelectRow In dtblParentMenu.Rows
#Region "For drwParentMenu"
            Dim blnIsMenuOpen As Boolean = False
            If intActiveMenuID = drwParentMenu.ID Then
                blnIsMenuOpen = True
            Else
                Dim tadpMenuCheckIsSubMenu As New dstMenuTableAdapters.spr_Menu_CheckSubMenuIsaChild_SelectTableAdapter()
                Dim dtblMenuCheckIsSubMenu As dstMenu.spr_Menu_CheckSubMenuIsaChild_SelectDataTable = Nothing
                dtblMenuCheckIsSubMenu = tadpMenuCheckIsSubMenu.GetData(drwParentMenu.ID, intActiveMenuID)
                If dtblMenuCheckIsSubMenu.Rows.Count > 0 Then
                    blnIsMenuOpen = True
                End If
            End If

            If blnIsMenuOpen = True Then
                strMenuText &= "<li class=""treeview active"">"
            Else
                strMenuText &= "<li class=""treeview"">"
            End If


            If drwParentMenu.IsLeaf = True AndAlso drwParentMenu.IsURLNull() = False Then
                strMenuText &= "<a href =""" & drwParentMenu.URL & """>"
            Else
                strMenuText &= "<a href=""#"" >"
            End If
            strMenuText &= "<i class=""" & drwParentMenu.IconStyle & """></i><span> " & drwParentMenu.MenuTitle & "</span>"

            If drwParentMenu.IsLeaf = False Then
                strMenuText &= "<i class=""fa fa-angle-left pull-right""></i>"
            ElseIf drwParentMenu.HasExtraInfo = True
                strMenuText &= "<small class=""" & drwParentMenu.ExtraInfoStyle & """>" & Me.GetMenuExtraInfo(drwParentMenu.ID) & "</small>"
            End If

            strMenuText &= "</a>"
            strMenuText &= Me.GetMenuChildrenString(blnForItemAdmin, drwParentMenu.ID, intUserID, intActiveMenuID)
            strMenuText &= "</li>"

#End Region
        Next drwParentMenu
        strMenuText &= "</ul>"
    End Sub



    Private Function GetMenuChildrenString(blnForItemAdmin As Boolean, inttheParentID As Integer, Optional intUserID As Integer = 0, Optional intActiveMenuID As Integer = 0) As String
        Dim strResultHTML As String = ""
        Dim intAction As Integer = 2
        If blnForItemAdmin = True Then
            intAction = 1
        End If

        strResultHTML = "<ul class=""treeview-menu"">"
        Dim adtpChildMenu As New dstMenuTableAdapters.spr_Menu_Child_SelectTableAdapter()
        Dim dtblChildMenu As dstMenu.spr_Menu_Child_SelectDataTable = Nothing
        dtblChildMenu = adtpChildMenu.GetData(intAction, intUserID, inttheParentID)
        For Each drwChidMenu As dstMenu.spr_Menu_Child_SelectRow In dtblChildMenu.Rows
            Dim blnIsMenuOpen As Boolean = False
            If intActiveMenuID = drwChidMenu.ID Then
                blnIsMenuOpen = True
            Else
                Dim tadpMenuCheckIsSubMenu As New dstMenuTableAdapters.spr_Menu_CheckSubMenuIsaChild_SelectTableAdapter()
                Dim dtblMenuCheckIsSubMenu As dstMenu.spr_Menu_CheckSubMenuIsaChild_SelectDataTable = Nothing
                dtblMenuCheckIsSubMenu = tadpMenuCheckIsSubMenu.GetData(drwChidMenu.ID, intActiveMenuID)
                If dtblMenuCheckIsSubMenu.Rows.Count > 0 Then
                    blnIsMenuOpen = True

                End If
            End If

            If blnIsMenuOpen = True Then
                strResultHTML &= "<li class=""active"">"
            Else
                strResultHTML &= "<li>"
            End If

            If drwChidMenu.IsLeaf = True AndAlso drwChidMenu.IsURLNull() = False Then
                strResultHTML &= "<a href= """ & drwChidMenu.URL & """>"
            Else
                strResultHTML &= "<a href=""#"">"
            End If

            strResultHTML &= ("<i class=""" & drwChidMenu.IconStyle & """></i><span> ") & drwChidMenu.MenuTitle & "</span>"

            If drwChidMenu.IsLeaf = False Then
                strResultHTML &= "<i class=""fa fa-angle-left pull-right""></i>"
            ElseIf drwChidMenu.HasExtraInfo = True Then

                strResultHTML &= "<small class=""" & drwChidMenu.ExtraInfoStyle & """>" & Me.GetMenuExtraInfo(drwChidMenu.ID) & "</small>"
            End If
            strResultHTML &= "</a>"
            strResultHTML &= Me.GetMenuChildrenString(blnForItemAdmin, drwChidMenu.ID, intUserID, intActiveMenuID)
            strResultHTML &= "</li>"
        Next

        strResultHTML &= "</ul>"
        If strResultHTML = "<ul class=""treeview-menu""></ul>" Then
            Return ""
        Else
            Return strResultHTML
        End If

    End Function

    Private Function GetMenuExtraInfo(intMenuID As Integer) As String
        Dim strExtraInfo As String = ""
        Select Case intMenuID
            Case 4
                strExtraInfo = "5"
                Exit Select
            Case 12
                strExtraInfo = "new!"
                Exit Select
            Case 17
                strExtraInfo = "24"
                Exit Select
        End Select
        Return strExtraInfo
    End Function

End Class
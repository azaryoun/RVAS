Imports Service.Security


Public Class Smart
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Session("stcUserInfo") Is Nothing Then
            Call btnSignOut_Click(sender, e)
            Return
        End If


        Dim strClassName As String = Request.ServerVariables("SCRIPT_NAME")
        If strClassName Is Nothing Then
            Response.Redirect("~/Login.aspx")
            Return
        End If

        Dim intPos As Integer = strClassName.LastIndexOf("/")
        strClassName = strClassName.Substring(intPos + 1)
        strClassName = strClassName.Substring(0, strClassName.Length - 5)


        Dim strPageTitle As String = "پنل مدیریت ارزش افزوده رهیاب"
        Dim intActiveMenuID As Integer = 0
        Dim strMenuTitleMain As String = "خوش آمدید"
        Dim strMenuTitleSamll As String = ""
        Dim strthePath As String = ""
        Dim OstcUserInfo As stcUserInfo = CType(Session("stcUserInfo"), stcUserInfo)


        If strClassName.ToLower <> "startpage" AndAlso strClassName.ToLower <> "userprofile" Then
            If OstcUserInfo.IsItemAdmin = True Then

                Dim tadpPageTitle As New BusinessObject.Administration.dstMenuTableAdapters.spr_Menu_PageTitle_SelectTableAdapter
                Dim dtblPageTitle As BusinessObject.Administration.dstMenu.spr_Menu_PageTitle_SelectDataTable = Nothing
                dtblPageTitle = tadpPageTitle.GetData(strClassName)
                If dtblPageTitle.Rows.Count > 0 Then
                    Dim drwPageTitle As BusinessObject.Administration.dstMenu.spr_Menu_PageTitle_SelectRow = dtblPageTitle.Rows(0)
                    strPageTitle = drwPageTitle.PageTitle
                    intActiveMenuID = drwPageTitle.ID
                    strMenuTitleMain = drwPageTitle.MenuTitle

                    Dim tadpMenuTitlePath As New BusinessObject.Administration.dstMenuTableAdapters.spr_GetMenuPathForTitleBar_SelectTableAdapter
                    Dim dtblMenuTitlePath As BusinessObject.Administration.dstMenu.spr_GetMenuPathForTitleBar_SelectDataTable = Nothing
                    dtblMenuTitlePath = tadpMenuTitlePath.GetData(drwPageTitle.ID)
                    strthePath = dtblMenuTitlePath.First.MenuTitlePath
                    ' strthePath = strthePath.Insert(3, " class='" & drwPageTitle.IconStyle & "' ")
                End If

            Else

                Dim tadpCheckAccess As New BusinessObject.Administration.dstMenuTableAdapters.spr_Menu_CheckUserAccess_SelectTableAdapter
                Dim dtblCheckAccess As BusinessObject.Administration.dstMenu.spr_Menu_CheckUserAccess_SelectDataTable = Nothing
                dtblCheckAccess = tadpCheckAccess.GetData(OstcUserInfo.ID, strClassName)
                If dtblCheckAccess.Rows.Count = 0 Then
                    Response.Redirect(Request.ServerVariables("HTTP_REFERER"))
                    Return
                End If

                Dim drwCheckAccess As BusinessObject.Administration.dstMenu.spr_Menu_CheckUserAccess_SelectRow = dtblCheckAccess.Rows(0)
                strPageTitle = drwCheckAccess.PageTitle
                intActiveMenuID = drwCheckAccess.ID
                strMenuTitleMain = drwCheckAccess.MenuTitle
                Dim tadpMenuTitlePath As New BusinessObject.Administration.dstMenuTableAdapters.spr_GetMenuPathForTitleBar_SelectTableAdapter
                Dim dtblMenuTitlePath As BusinessObject.Administration.dstMenu.spr_GetMenuPathForTitleBar_SelectDataTable = Nothing
                dtblMenuTitlePath = tadpMenuTitlePath.GetData(drwCheckAccess.ID)
                strthePath = dtblMenuTitlePath.First.MenuTitlePath
                '  strthePath = strthePath.Insert(3, " class='" & drwCheckAccess.IconStyle & "' ")

            End If


        End If

        lblApplicationTitle_Big.Text = "رهیاب  <b>VAS</b>"
        lblApplicationTitle_Mini.Text = "VAS"
        lblUserInfo_Menu_Big.Text = "رهیاب - " & OstcUserInfo.FName & " " & OstcUserInfo.LName
        lblUserInfo_Menu_Small.Text = "عضو سیستم از " & mdlGeneral.GetPersianMonthYear(OstcUserInfo.STime)

        If OstcUserInfo.UserPhoto IsNot Nothing Then
            imgUserImageSmallRight.ImageUrl = "data: Image/png;base64," + Convert.ToBase64String(OstcUserInfo.UserPhoto)
            imgUserLeftBig.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(OstcUserInfo.UserPhoto)
            imgUserLeftSmall.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(OstcUserInfo.UserPhoto)
        End If

        Me.Page.Title = strPageTitle
        lblPageMainTitle.Text = strMenuTitleMain

        If strClassName.ToLower.EndsWith("management") = True Then
            strMenuTitleSamll = "مدیریت"
        ElseIf strClassName.ToLower.EndsWith("edit") = True Then
            strMenuTitleSamll = "ویرایش"
        ElseIf strClassName.ToLower.EndsWith("new") = True Then
            strMenuTitleSamll = "جدید"
        ElseIf strClassName.ToLower.EndsWith("report") = True Then
            strMenuTitleSamll = "گزارش"
        ElseIf strClassName.ToLower.EndsWith("profile") = True Then
            strMenuTitleSamll = "پروفایل"
        End If

        lblPageSmallTitle.Text = strMenuTitleSamll
        lblUserFNameLName.Text = OstcUserInfo.FName & " " & OstcUserInfo.LName
        lblUserFNameLNameLeft.Text = OstcUserInfo.FName & " " & OstcUserInfo.LName

        ltrthePath.Text = strthePath

        ltrlSubscriptionTaskBar.Text = GetTotalSubscriptionStirng()
        ltrlUnsubscriptionTaskBar.Text = GetTotalUnsubscriptionStirng()


        Bootstrap_Menu.BuildMenu(OstcUserInfo.IsItemAdmin, OstcUserInfo.ID, intActiveMenuID)

    End Sub

    Private Sub btnProfile_Click(sender As Object, e As EventArgs) Handles btnProfile.Click
        Response.Redirect("~/Application/Administration/User/UserProfile.aspx")
    End Sub

    Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
        Session("stcUserInfo") = Nothing
        Session.Abandon()
        Session.Clear()
        System.Web.Security.FormsAuthentication.SignOut()
        System.Web.Security.FormsAuthentication.RedirectToLoginPage()

    End Sub

    Private Function GetTotalSubscriptionStirng() As String

        Dim odbVAS As New BusinessObject.Context.dbVASEntities
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

        Dim lnqVASServiceMembershipSubscriber = odbVAS.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
        Dim dteToDay As Date = Date.Now.Date

        lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.SubscrptionDate >= dteToDay)
        Dim lnqVASServiceMembershipSubscriberGroupList = lnqVASServiceMembershipSubscriber.GroupBy(Function(x) x.tbl_VASServiceMembership.tbl_VASService.ServiceName).OrderByDescending(Function(x) x.Count).ToList
        Dim intNewSubscriptionCount As Integer = lnqVASServiceMembershipSubscriberGroupList.Sum(Function(x) x.Count)
        lblNewSubscription.Text = intNewSubscriptionCount.ToString("n0")
        Dim strResult As String = "<ul class=""dropdown-menu"">"
        strResult &= "<li class=""header""  style=""text-align:right;font-size:12px;"" >امروز " & intNewSubscriptionCount.ToString("n0") & " اشتراک جدید داریم</li>"

        If intNewSubscriptionCount <> 0 Then
            strResult &= "<li>"
            strResult &= "<ul class=""menu"">"



            Dim i As Integer = 0
            Dim arrstringColors() As String = {"progress-bar progress-bar-aqua", "progress-bar progress-bar-green", "progress-bar progress-bar-red", "progress-bar progress-bar-yellow", "progress-bar progress-bar-blue"}

            For Each lnqVASServiceMembershipSubscriberGroupListItem In lnqVASServiceMembershipSubscriberGroupList



                strResult &= "<li>"
                strResult &= "<a href = ""#"" >"
                strResult &= "<h3>" & lnqVASServiceMembershipSubscriberGroupListItem.Key
                strResult &= "<small class=""pull-right"" style=""font-size: 12px;"">" & lnqVASServiceMembershipSubscriberGroupListItem.Count.ToString("n0") & "</small>"
                strResult &= "</h3>"
                strResult &= "<div Class=""progress xs"">"

                Dim sngThisShare As Single = Math.Round(lnqVASServiceMembershipSubscriberGroupListItem.Count / intNewSubscriptionCount, 1)


                strResult &= " <div class=""" & arrstringColors(i) & """ style=""width:" & sngThisShare.ToString("p0").Replace(" ", "") & """ role=""progressbar"" aria-valuenow=""" & lnqVASServiceMembershipSubscriberGroupListItem.Count & """ aria-valuemin=""0"" aria-valuemax=""" & intNewSubscriptionCount & """>"
                ' strResult &= "<span class=""sr-only"">" & sngThisShare.ToString("p1") & " اشتراک جدید</span>"
                strResult &= "</div></div></a></li>"

                i += 1
                If i = 5 Then Exit For
            Next lnqVASServiceMembershipSubscriberGroupListItem
            strResult &= "</ul></li>"
        End If


        strResult &= "<li Class=""footer"">"
        strResult &= " <a href = ""/Application/Report/Membership/News/MembershipNewsSubscriptionReport.aspx"" > مشاهده تمام اشتراک های خبری</a>"
        strResult &= " <a href = ""/Application/Report/Membership/Serial/MembershipSerialSubscriptionReport.aspx""> مشاهده تمام اشتراک های سریالی</a>"

        strResult &= "</li>"
        strResult &= "</ul>"

        Return strResult

    End Function


    Private Function GetTotalUnsubscriptionStirng() As String

        Dim odbVAS As New BusinessObject.Context.dbVASEntities
        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

        Dim lnqVASServiceMembershipSubscriber = odbVAS.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID)
        Dim dteToDay As Date = Date.Now.Date

        lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.UnsubscriptionDate IsNot Nothing AndAlso x.UnsubscriptionDate >= dteToDay)
        Dim lnqVASServiceMembershipSubscriberGroupList = lnqVASServiceMembershipSubscriber.GroupBy(Function(x) x.tbl_VASServiceMembership.tbl_VASService.ServiceName).OrderByDescending(Function(x) x.Count).ToList
        Dim intNewUnsubscriptionCount As Integer = lnqVASServiceMembershipSubscriberGroupList.Sum(Function(x) x.Count)
        lblNewUnsubscription.Text = intNewUnsubscriptionCount.ToString("n0")
        Dim strResult As String = "<ul class=""dropdown-menu"">"
        strResult &= "<li class=""header""  style=""text-align:right;font-size:12px;"" >امروز " & intNewUnsubscriptionCount.ToString("n0") & " لغواشتراک جدید داریم</li>"

        If intNewUnsubscriptionCount <> 0 Then
            strResult &= "<li>"
            strResult &= "<ul class=""menu"">"



            Dim i As Integer = 0
            Dim arrstringColors() As String = {"progress-bar progress-bar-aqua", "progress-bar progress-bar-green", "progress-bar progress-bar-red", "progress-bar progress-bar-yellow", "progress-bar progress-bar-blue"}

            For Each lnqVASServiceMembershipSubscriberGroupListItem In lnqVASServiceMembershipSubscriberGroupList



                strResult &= "<li>"
                strResult &= "<a href = ""#"" >"
                strResult &= "<h3>" & lnqVASServiceMembershipSubscriberGroupListItem.Key
                strResult &= "<small class=""pull-right"" style=""font-size: 12px;"">" & lnqVASServiceMembershipSubscriberGroupListItem.Count.ToString("n0") & "</small>"
                strResult &= "</h3>"
                strResult &= "<div Class=""progress xs"">"

                Dim sngThisShare As Single = Math.Round(lnqVASServiceMembershipSubscriberGroupListItem.Count / intNewUnsubscriptionCount, 1)


                strResult &= " <div class=""" & arrstringColors(i) & """ style=""width:" & sngThisShare.ToString("p0").Replace(" ", "") & """ role=""progressbar"" aria-valuenow=""" & lnqVASServiceMembershipSubscriberGroupListItem.Count & """ aria-valuemin=""0"" aria-valuemax=""" & intNewUnsubscriptionCount & """>"
                strResult &= "</div></div></a></li>"

                i += 1
                If i = 5 Then Exit For
            Next lnqVASServiceMembershipSubscriberGroupListItem
            strResult &= "</ul></li>"
        End If


        strResult &= "<li Class=""footer"">"
        strResult &= " <a href = ""/Application/Report/Membership/News/MembershipNewsUnsubscriptionReport.aspx"" > مشاهده تمام لغواشتراک های خبری</a>"
        strResult &= " <a href = ""/Application/Report/Membership/Serial/MembershipSerialUnsubscriptionReport.aspx"" > مشاهده تمام لغواشتراک های سریالی</a>"

        strResult &= "</li>"
        strResult &= "</ul>"

        Return strResult

    End Function

End Class
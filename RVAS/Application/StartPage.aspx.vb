Imports EntityFramework.Extensions

Public Class StartPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
            Dim odbVAS As New BusinessObject.Context.dbVASEntities



            odbVAS.tbl_User.Where(Function(x) x.IsMale = True).Update(Function(x) New BusinessObject.Context.tbl_User With {.IsReal = False})

            'Dim k As New BusinessObject.Context.tbl_User
            'k.ID = 6
            'k.LName = "Aghdas"
            'Dim o = odbVAS.tbl_User.Attach(k)
            'odbVAS.Entry(o).State = Entity.EntityState.Modified



            'odbVAS.tbl_User.Where(Function(x) x.IsMale = True).Delete

            'Dim m As New BusinessObject.Context.tbl_User With {.LName = "ds"}
            ''odbVAS.tbl_User.Add(m)
            'odbVAS.SaveChanges()



            'Context.Students.Update(c >= c.Name.Contains("a"),
            '                           c >= New student  { Family="asghar"});

            Dim lnqCRM = odbVAS.tbl_CRM.Where(Function(x) x.FK_OwnerUserID = osctUserInfo.ID).OrderByDescending(Function(x) x.ID)
            lnqCRM = lnqCRM.Where(Function(x) x.Remarks.Contains("r"))

            For Each item In lnqCRM

                Dim strReq As String = item.RequestNo
                Dim strSTime As Date = mdlGeneral.GetPersianDateTime(item.RequestDate)
                Dim intStatus As Byte = item.Status
                Dim strUsername As String = item.tbl_User.Username & item.tbl_User.FName
                Dim strCategoryname As String = item.tbl_CRMCategory.Name


            Next item


            Dim intServiceCountNews = odbVAS.tbl_VASServiceMembership.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Where(Function(x) x.IsNewsContent = True).Count


            Dim intServiceCountSerial = odbVAS.tbl_VASServiceMembership.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Where(Function(x) x.IsNewsContent = False).Count

            Dim intServiceCountManual = odbVAS.tbl_VASServiceOnDemand.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Where(Function(x) x.IsSystematic = False).Count
            Dim intServiceCountSystematic = odbVAS.tbl_VASServiceOnDemand.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Where(Function(x) x.IsSystematic = True).Count

            Dim dteToday As Date = Date.Now.Date

            Dim intSendCountNews = odbVAS.tbl_SendLog.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID AndAlso x.SendDateTime >= dteToday).Where(Function(x) x.tbl_VASServiceMembershipSubscriber.tbl_VASServiceMembership.IsNewsContent = True).Count
            Dim intSendCountSerial = odbVAS.tbl_SendLog.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID AndAlso x.SendDateTime >= dteToday).Where(Function(x) x.tbl_VASServiceMembershipSubscriber.tbl_VASServiceMembership.IsNewsContent = False).Count

            Dim intSendCountManual = odbVAS.tbl_SendLog.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID AndAlso x.SendDateTime >= dteToday).Where(Function(x) x.tbl_VASService.IsOnDemand = True AndAlso x.tbl_VASService.tbl_VASServiceOnDemand.IsSystematic = False).Count
            Dim intSendCountSystematic = odbVAS.tbl_SendLog.Where(Function(x) x.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID AndAlso x.SendDateTime >= dteToday).Where(Function(x) x.tbl_VASService.IsOnDemand = True AndAlso x.tbl_VASService.tbl_VASServiceOnDemand.IsSystematic = True).Count

            Dim intSubscriberCountNews = odbVAS.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Where(Function(x) x.UnsubscriptionDate Is Nothing And x.tbl_VASServiceMembership.IsNewsContent = True).Count
            Dim intSubscriberCountSerial = odbVAS.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Where(Function(x) x.UnsubscriptionDate Is Nothing And x.tbl_VASServiceMembership.IsNewsContent = False).Count

            Dim intSubscribtionNews = odbVAS.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Where(Function(x) x.tbl_VASServiceMembership.IsNewsContent = True).Count
            Dim intSubscribtionSerial = odbVAS.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.tbl_VASService.FK_OwnerUserID = osctUserInfo.ID).Where(Function(x) x.tbl_VASServiceMembership.IsNewsContent = False).Count


            lblSendCountManual.Text = intSendCountManual.ToString("n0")
            lblSendCountNews.Text = intSendCountNews.ToString("n0")
            lblSendCountSerial.Text = intSendCountSerial.ToString("n0")
            lblSendCountSystematic.Text = intSendCountSystematic.ToString("n0")

            lblServiceCountManual.Text = intServiceCountManual.ToString("n0")
            lblServiceCountNews.Text = intServiceCountNews.ToString("n0")
            lblServiceCountSerial.Text = intServiceCountSerial.ToString("n0")
            lblServiceCountSystematic.Text = intServiceCountSystematic.ToString("n0")


            lblSubscriberCountNews.Text = intSubscriberCountNews.ToString("n0")
            lblSubscriberCountSerial.Text = intSubscriberCountSerial.ToString("n0")

            lblSubscriptionNews.Text = intSubscribtionNews.ToString("n0")
            lblSubscriptionSerial.Text = intSubscribtionSerial.ToString("n0")



        End If
    End Sub

End Class
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub TestMethod1()



        'If Date.Now.DayOfWeek <> DayOfWeek.Friday AndAlso Date.Now.DayOfWeek <> DayOfWeek.Wednesday Then

        '    Dim m As Integer = 3


        'End If

        'Return

        'Dim odbVAS As New BusinessObject.Context.dbVASEntities



        'Dim dteFrom As Date = #2/1/2016#
        'Dim dteTo As Date = Date.Now

        'Dim intVASServiceID? As Integer = Nothing



        'Dim intSubscriptionType As Integer = 1

        'Dim blnAscendingSort As Boolean = True


        'Dim lnqVASServiceMembershipSubscriber = odbVAS.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.tbl_VASServiceMembership.tbl_VASService.FK_OwnerUserID = 1)
        'lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.SubscrptionDate >= dteFrom AndAlso x.SubscrptionDate <= dteTo)

        'If intVASServiceID IsNot Nothing Then
        '    lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.FK_VASServiceID = intVASServiceID)
        'End If


        'If intSubscriptionType = 2 Then
        '    lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.UnsubscriptionDate Is Nothing)
        'ElseIf intSubscriptionType = 3
        '    lnqVASServiceMembershipSubscriber = lnqVASServiceMembershipSubscriber.Where(Function(x) x.UnsubscriptionDate IsNot Nothing)
        'End If




        'Dim lnqVASServiceMembershipSubscriberGroup = lnqVASServiceMembershipSubscriber.GroupBy(Function(x) x.tbl_VASServiceMembership.tbl_VASService.ServiceName & " (" & x.tbl_VASServiceMembership.tbl_VASService.AggergatorServiceID & ")")


        'For Each k In lnqVASServiceMembershipSubscriberGroup

        '    Dim m As String = k.Key
        '    Dim h As Integer = k.Count
        '    Dim s As Integer = 3
        'Next k






        'Dim lnqVASServiceMembershipSubscriberList = lnqVASServiceMembershipSubscriber.ToList()










    End Sub

End Class
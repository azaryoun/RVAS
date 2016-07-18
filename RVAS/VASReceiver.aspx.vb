Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Threading.Tasks
Public Class VASReceiver
    Inherits System.Web.UI.Page

    Private Const AGGREGATOR_COMPANY As String = "Taranom"
    Private Const AGGREGATOR_USERNAME As String = "Tauser"
    Private Const AGGREGATOR_PASSWORD As String = "trq34@!"

    Private Const TARANOM_REST_BASE_IP_ADDRESS As String = "http://79.175.172.157:9001/"
    Private Const TARANOM_REST_SEND_SERVICE As String = "taranom/transfer/send"
    Private Const TARANOM_REST_COMPANY As String = "Rahyab"
    Private Const TARANOM_REST_USERNAME As String = "rrgtest"
    Private Const TARANOM_REST_PASSWORD As String = "rRgTeS7"




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strCompany As String = ""
        Dim strUsername As String = ""
        Dim strPassword As String = ""
        Dim strTo As String = ""
        Dim strFrom As String = ""
        Dim strSeviceID As String = ""
        Dim strMessage As String = ""

        If Request.QueryString("sc") Is Nothing OrElse Request.QueryString("sc") <> AGGREGATOR_COMPANY Then
            Return
        End If

        If Request.QueryString("username") Is Nothing OrElse Request.QueryString("username") <> AGGREGATOR_USERNAME Then
            Return
        End If

        If Request.QueryString("password") Is Nothing OrElse Request.QueryString("password") <> AGGREGATOR_PASSWORD Then
            Return
        End If

        If Request.QueryString("serviceId") Is Nothing OrElse Request.QueryString("serviceId") = "" Then
            Return
        End If

        If Request.QueryString("to") Is Nothing OrElse Request.QueryString("to") = "" Then
            Return
        End If

        If Request.QueryString("from") Is Nothing OrElse Request.QueryString("from") = "" Then
            Return
        End If

        If Request.QueryString("message") Is Nothing Then
            Return
        End If

        strSeviceID = Request.QueryString("serviceId")
        strMessage = Request.QueryString("message")
        strTo = Request.QueryString("to")
        strFrom = Request.QueryString("from")

        Dim ctxVar As New BusinessObject.Context.dbVASEntities

        If CheckUnsubscription(ctxVar, strMessage, strFrom, strSeviceID, strTo) = True Then Return

        If CheckSubscription(ctxVar, strMessage, strFrom, strSeviceID, strTo) = True Then Return


        Dim lstVASServiceOnDemand = ctxVar.tbl_VASServiceOnDemand.Where(Function(x) x.tbl_VASService.IsActive = True AndAlso x.tbl_VASService.theWholeNumber = strTo AndAlso x.tbl_VASService.AggergatorServiceID = strSeviceID).ToList()

        If lstVASServiceOnDemand.Count = 0 Then Return

        For Each itmVASServiceOnDemand In lstVASServiceOnDemand
            If itmVASServiceOnDemand.theType = 1 Then 'Fixed Key
                If itmVASServiceOnDemand.theKey.ToLower <> strMessage.ToLower Then Continue For
                Dim strOutputMessage As String = ""
                If GetMessageForService(itmVASServiceOnDemand.FK_VASServiceID, strMessage, strOutputMessage) = False Then Continue For
                Call SendVASMessage(itmVASServiceOnDemand.tbl_VASService.theWholeNumber, strFrom, itmVASServiceOnDemand.tbl_VASService.AggergatorServiceID, strOutputMessage)
                Exit For
            ElseIf itmVASServiceOnDemand.theType = 2 Then 'StartsWith

                If strMessage.ToLower.StartsWith(itmVASServiceOnDemand.theKey.ToLower) = True Then Continue For
                Dim strOutputMessage As String = ""
                If GetMessageForService(itmVASServiceOnDemand.FK_VASServiceID, strMessage, strOutputMessage) = False Then Continue For
                Call SendVASMessage(itmVASServiceOnDemand.tbl_VASService.theWholeNumber, strFrom, itmVASServiceOnDemand.tbl_VASService.AggergatorServiceID, strOutputMessage)
                Exit For
            ElseIf itmVASServiceOnDemand.theType = 3 Then 'Dynamic

                Dim strOutputMessage As String = ""
                If GetMessageForService(itmVASServiceOnDemand.FK_VASServiceID, strMessage, strOutputMessage) = False Then Continue For
                Call SendVASMessage(itmVASServiceOnDemand.tbl_VASService.theWholeNumber, strFrom, itmVASServiceOnDemand.tbl_VASService.AggergatorServiceID, strOutputMessage)
                Exit For

            End If


        Next itmVASServiceOnDemand





    End Sub

    Private Function GetMessageForService(vasServiceId As Integer, inputMessage As String, ByRef outputMessage As String) As Boolean
        Try
            Select Case vasServiceId
                Case 1
            End Select
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function CheckUnsubscription(ctxVar As BusinessObject.Context.dbVASEntities, message As String, fromMobile As String, serviceId As String, serviceNumber As String) As Boolean


        Dim lstVASServiceMembership = ctxVar.tbl_VASServiceMembership.Where(Function(x) x.tbl_VASService.IsActive = True AndAlso x.tbl_VASService.AggergatorServiceID = serviceId AndAlso x.tbl_VASService.theWholeNumber = serviceNumber AndAlso x.UnsubscriptionKey = message).FirstOrDefault()

        If lstVASServiceMembership Is Nothing Then Return False

        Dim lnqVASServiceMembershipSubscriber = ctxVar.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.FK_VASServiceID = lstVASServiceMembership.FK_VASServiceID AndAlso x.SubscriberMobileNo = fromMobile AndAlso x.UnsubscriptionDate Is Nothing).FirstOrDefault()

        If lnqVASServiceMembershipSubscriber Is Nothing Then Return False

        lnqVASServiceMembershipSubscriber.UnsubscriptionDate = Date.Now
        ctxVar.SaveChanges()

        'Send GoodBy Message

        Return True



    End Function

    Private Function CheckSubscription(ctxVar As BusinessObject.Context.dbVASEntities, message As String, fromMobile As String, serviceId As String, serviceNumber As String) As Boolean


        Dim lstVASServiceMembership = ctxVar.tbl_VASServiceMembership.Where(Function(x) x.tbl_VASService.IsActive = True AndAlso x.tbl_VASService.AggergatorServiceID = serviceId AndAlso x.tbl_VASService.theWholeNumber = serviceNumber AndAlso x.SubscriptionKey = message).FirstOrDefault()

        If lstVASServiceMembership Is Nothing Then Return False

        Dim blnIsCurrentMember = ctxVar.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.FK_VASServiceID = lstVASServiceMembership.FK_VASServiceID AndAlso x.SubscriberMobileNo = fromMobile AndAlso x.UnsubscriptionDate Is Nothing).Any()

        If blnIsCurrentMember = True Then Return False

        Dim rowVASServiceMembershipSubscriber As New BusinessObject.Context.tbl_VASServiceMembershipSubscriber
        rowVASServiceMembershipSubscriber.SubscriberMobileNo = fromMobile
        rowVASServiceMembershipSubscriber.FK_VASServiceID = lstVASServiceMembership.FK_VASServiceID
        rowVASServiceMembershipSubscriber.SubscrptionDate = Date.Now
        rowVASServiceMembershipSubscriber.UnsubscriptionDate = Nothing
        ctxVar.tbl_VASServiceMembershipSubscriber.Add(rowVASServiceMembershipSubscriber)
        ctxVar.SaveChanges()

        'Send Welcome Message

        Return True




    End Function

    Private Sub SendVASMessage(serviceNumber As String, subscriberMobile As String, aggregatorServiceId As String, outputMessage As String)
        RunAsync(subscriberMobile, serviceNumber, aggregatorServiceId, outputMessage).Wait()
    End Sub



    Private Shared Async Function RunAsync(subscriberMobile As String, serviceNumber As String, aggregatorServiceId As String, outputMessage As String) As Task

        Using client As New HttpClient()


            client.BaseAddress = New Uri(TARANOM_REST_BASE_IP_ADDRESS)
            client.DefaultRequestHeaders.Accept.Clear()
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Dim strTo As String = subscriberMobile '"989122764983";
            Dim strFrom As String = serviceNumber  '"9830834911";
            Dim strServiceID As String = aggregatorServiceId  '"09118a098cbd4013bb6775c607869034";
            Dim strMessage As String = outputMessage ' "The Message";

            Dim Response As HttpResponseMessage = Nothing
            Dim strURL As String = String.Format("{0}?sc={1}&to={2}&from={3}&serviceId={4}&username={5}&password={6}&message={7}", TARANOM_REST_SEND_SERVICE, TARANOM_REST_COMPANY, strTo, strFrom, strServiceID, TARANOM_REST_USERNAME, TARANOM_REST_PASSWORD, strMessage)

            Response = Await client.GetAsync(strURL)

            If Response.IsSuccessStatusCode = True Then
                Dim strVAS As String = Await Response.Content.ReadAsStringAsync()
            End If

        End Using
    End Function

End Class
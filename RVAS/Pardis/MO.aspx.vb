Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Threading.Tasks
Public Class MO
    Inherits System.Web.UI.Page

    Private Const PARDIS_REST_BASE_IP_ADDRESS As String = "http://10.20.22.18:1090/"
    Private Const PARDIS_REST_SEND_SERVICE As String = "mt.asmx"




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strShortcode As String = "" 'Service Number
        Dim strAddress As String = "" 'Mobile
        Dim strMessage As String = ""
        Dim lngMOId As Long = ""


        If Request.QueryString("Address") Is Nothing OrElse Request.QueryString("Address") = "" Then

            Response.Write("0")
            Return
        End If

        If Request.QueryString("Shortcode") Is Nothing OrElse Request.QueryString("Shortcode") = "" Then

            Response.Write("0")
            Return
        End If

        If Request.QueryString("Message") Is Nothing Then

            Response.Write("0")
            Return
        End If

        lngMOId = Val(Request.QueryString("MOId"))
        strMessage = Request.QueryString("Message")
        strShortcode = Request.QueryString("Shortcode")
        strAddress = Request.QueryString("Address")

        strAddress = "0" & strAddress.Substring(2)
        strShortcode = strShortcode.Substring(2)

        Dim ctxVar As New BusinessObject.Context.dbVASEntities

        If CheckUnsubscription(ctxVar, strMessage, strAddress, strShortcode) = True Then

            Response.Write("1")
            Return
        End If

        If CheckSubscription(ctxVar, strMessage, strAddress, strShortcode) = True Then

            Response.Write("1")
            Return
        End If

        Dim dteToday As Date = Date.Now.Date

        Dim spnFromTime As TimeSpan = DateTime.Now.AddHours(-2).TimeOfDay
        Dim spnToTime As TimeSpan = DateTime.Now.TimeOfDay

        Dim intSendDateID As Integer = ctxVar.tbl_Date.Where(Function(x) x.DateG = dteToday).FirstOrDefault().ID



        Dim lstVASServiceOnDemand = ctxVar.tbl_VASServiceOnDemand.Where(Function(x) x.tbl_VASService.IsActive = True AndAlso x.tbl_VASService.theWholeNumber = strShortcode).ToList()

        If lstVASServiceOnDemand.Count = 0 Then
            Response.Write("-2")
            Return
        End If

        For Each itmVASServiceOnDemand In lstVASServiceOnDemand
            If itmVASServiceOnDemand.theType = 1 Then 'Fixed Key
                If itmVASServiceOnDemand.theKey.ToLower <> strMessage.ToLower Then Continue For
                Dim strOutputMessage As String = ""

                If GetMessageForService(itmVASServiceOnDemand.FK_VASServiceID, strMessage, strOutputMessage) = False Then Continue For
                Dim rowSendLog As BusinessObject.Context.tbl_SendLog = New BusinessObject.Context.tbl_SendLog()

                Try

                    Call SendVASMessage(itmVASServiceOnDemand.tbl_VASService.theWholeNumber, strAddress, itmVASServiceOnDemand.tbl_VASService.AggergatorServiceID, strOutputMessage)

                    rowSendLog.FK_ID = Nothing
                    rowSendLog.FK_SendDateID = intSendDateID
                    rowSendLog.FK_VASMembershipSubscriberID = Nothing
                    rowSendLog.FK_VASServiceID = itmVASServiceOnDemand.FK_VASServiceID
                    rowSendLog.ReceiverMobile = strAddress
                    rowSendLog.SendDateTime = Date.Now
                    rowSendLog.SerialOrder = Nothing
                    rowSendLog.ServicePrice = itmVASServiceOnDemand.tbl_VASService.ServicePrice
                    rowSendLog.theStatus = 1 'Success
                    rowSendLog.theText = strMessage



                Catch ex As Exception

                    rowSendLog.FK_ID = Nothing
                    rowSendLog.FK_SendDateID = intSendDateID
                    rowSendLog.FK_VASMembershipSubscriberID = Nothing
                    rowSendLog.FK_VASServiceID = itmVASServiceOnDemand.FK_VASServiceID
                    rowSendLog.ReceiverMobile = strAddress
                    rowSendLog.SendDateTime = Date.Now
                    rowSendLog.SerialOrder = Nothing
                    rowSendLog.ServicePrice = itmVASServiceOnDemand.tbl_VASService.ServicePrice
                    rowSendLog.theStatus = 0 'Failed
                    rowSendLog.theText = strMessage

                End Try



                Exit For
            ElseIf itmVASServiceOnDemand.theType = 2 Then 'StartsWith

                If strMessage.ToLower.StartsWith(itmVASServiceOnDemand.theKey.ToLower) = True Then Continue For
                Dim strOutputMessage As String = ""

                If GetMessageForService(itmVASServiceOnDemand.FK_VASServiceID, strMessage, strOutputMessage) = False Then Continue For

                Dim rowSendLog As BusinessObject.Context.tbl_SendLog = New BusinessObject.Context.tbl_SendLog()

                Try
                    Call SendVASMessage(itmVASServiceOnDemand.tbl_VASService.theWholeNumber, strAddress, itmVASServiceOnDemand.tbl_VASService.AggergatorServiceID, strOutputMessage)

                    rowSendLog.FK_ID = Nothing
                    rowSendLog.FK_SendDateID = intSendDateID
                    rowSendLog.FK_VASMembershipSubscriberID = Nothing
                    rowSendLog.FK_VASServiceID = itmVASServiceOnDemand.FK_VASServiceID
                    rowSendLog.ReceiverMobile = strAddress
                    rowSendLog.SendDateTime = Date.Now
                    rowSendLog.SerialOrder = Nothing
                    rowSendLog.ServicePrice = itmVASServiceOnDemand.tbl_VASService.ServicePrice
                    rowSendLog.theStatus = 1 'Success
                    rowSendLog.theText = strMessage


                Catch ex As Exception


                    rowSendLog.FK_ID = Nothing
                    rowSendLog.FK_SendDateID = intSendDateID
                    rowSendLog.FK_VASMembershipSubscriberID = Nothing
                    rowSendLog.FK_VASServiceID = itmVASServiceOnDemand.FK_VASServiceID
                    rowSendLog.ReceiverMobile = strAddress
                    rowSendLog.SendDateTime = Date.Now
                    rowSendLog.SerialOrder = Nothing
                    rowSendLog.ServicePrice = itmVASServiceOnDemand.tbl_VASService.ServicePrice
                    rowSendLog.theStatus = 0 'Failed
                    rowSendLog.theText = strMessage
                End Try


                Exit For
            ElseIf itmVASServiceOnDemand.theType = 3 Then 'Dynamic

                Dim strOutputMessage As String = ""
                If GetMessageForService(itmVASServiceOnDemand.FK_VASServiceID, strMessage, strOutputMessage) = False Then Continue For

                Dim rowSendLog As BusinessObject.Context.tbl_SendLog = New BusinessObject.Context.tbl_SendLog()

                Try
                    Call SendVASMessage(itmVASServiceOnDemand.tbl_VASService.theWholeNumber, strAddress, itmVASServiceOnDemand.tbl_VASService.AggergatorServiceID, strOutputMessage)

                    rowSendLog.FK_ID = Nothing
                    rowSendLog.FK_SendDateID = intSendDateID
                    rowSendLog.FK_VASMembershipSubscriberID = Nothing
                    rowSendLog.FK_VASServiceID = itmVASServiceOnDemand.FK_VASServiceID
                    rowSendLog.ReceiverMobile = strAddress
                    rowSendLog.SendDateTime = Date.Now
                    rowSendLog.SerialOrder = Nothing
                    rowSendLog.ServicePrice = itmVASServiceOnDemand.tbl_VASService.ServicePrice
                    rowSendLog.theStatus = 1 'Success
                    rowSendLog.theText = strMessage

                Catch ex As Exception

                    rowSendLog.FK_ID = Nothing
                    rowSendLog.FK_SendDateID = intSendDateID
                    rowSendLog.FK_VASMembershipSubscriberID = Nothing
                    rowSendLog.FK_VASServiceID = itmVASServiceOnDemand.FK_VASServiceID
                    rowSendLog.ReceiverMobile = strAddress
                    rowSendLog.SendDateTime = Date.Now
                    rowSendLog.SerialOrder = Nothing
                    rowSendLog.ServicePrice = itmVASServiceOnDemand.tbl_VASService.ServicePrice
                    rowSendLog.theStatus = 0 'Failed
                    rowSendLog.theText = strMessage

                End Try


                Exit For

            End If


        Next itmVASServiceOnDemand


        Response.Write("1")



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

    Private Function CheckUnsubscription(ctxVar As BusinessObject.Context.dbVASEntities, message As String, fromMobile As String, serviceNumber As String) As Boolean


        Dim lstVASServiceMembership = ctxVar.tbl_VASServiceMembership.Where(Function(x) x.tbl_VASService.IsActive = True AndAlso x.tbl_VASService.theWholeNumber = serviceNumber AndAlso x.UnsubscriptionKey = message).FirstOrDefault()

        If lstVASServiceMembership Is Nothing Then Return False

        Dim lnqVASServiceMembershipSubscriber = ctxVar.tbl_VASServiceMembershipSubscriber.Where(Function(x) x.FK_VASServiceID = lstVASServiceMembership.FK_VASServiceID AndAlso x.SubscriberMobileNo = fromMobile AndAlso x.UnsubscriptionDate Is Nothing).FirstOrDefault()

        If lnqVASServiceMembershipSubscriber Is Nothing Then Return False

        lnqVASServiceMembershipSubscriber.UnsubscriptionDate = Date.Now
        ctxVar.SaveChanges()

        'Send GoodBy Message

        Return True



    End Function

    Private Function CheckSubscription(ctxVar As BusinessObject.Context.dbVASEntities, message As String, fromMobile As String, serviceNumber As String) As Boolean


        Dim lstVASServiceMembership = ctxVar.tbl_VASServiceMembership.Where(Function(x) x.tbl_VASService.IsActive = True AndAlso x.tbl_VASService.theWholeNumber = serviceNumber AndAlso x.SubscriptionKey = message).FirstOrDefault()

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


            client.BaseAddress = New Uri(PARDIS_REST_BASE_IP_ADDRESS)
            client.DefaultRequestHeaders.Accept.Clear()
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Dim strTo As String = subscriberMobile '"989122764983";
            Dim strFrom As String = serviceNumber  '"9830834911";
            Dim strServiceID As String = aggregatorServiceId  '"09118a098cbd4013bb6775c607869034";
            Dim strMessage As String = outputMessage ' "The Message";

            Dim Response As HttpResponseMessage = Nothing
            Dim strURL As String = String.Format("{0}?sc={1}&to={2}&from={3}&serviceId={4}&username={5}&password={6}&message={7}", PARDIS_REST_SEND_SERVICE, PARDIS_REST_COMPANY, strTo, strFrom, strServiceID, PARDIS_REST_USERNAME, PARDIS_REST_PASSWORD, strMessage)

            Response = Await client.GetAsync(strURL)

            If Response.IsSuccessStatusCode = True Then
                Dim strVAS As String = Await Response.Content.ReadAsStringAsync()
            End If

        End Using
    End Function

End Class
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Threading.Tasks
Public Class Notification
    Inherits System.Web.UI.Page

    Private Const PARDIS_REST_BASE_IP_ADDRESS As String = "http://10.20.22.18:1090/"
    Private Const PARDIS_REST_SEND_SERVICE As String = "mt.asmx"




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strAddress As String = "" 'Mobile
        Dim strNewStatus As String = ""
        Dim strServiceID As String = ""
        Dim strEventID As String = ""
        Dim strOldStatus As String = ""

        If Request.QueryString("Address") Is Nothing OrElse Request.QueryString("Address") = "" Then

            Response.Write("0")
            Return
        End If

        If Request.QueryString("ServiceID") Is Nothing OrElse Request.QueryString("ServiceID") = "" Then

            Response.Write("0")
            Return
        End If

        If Request.QueryString("EventID") Is Nothing OrElse Request.QueryString("EventID") = "" Then

            Response.Write("0")
            Return
        End If

        If Request.QueryString("OldStatus") Is Nothing OrElse Request.QueryString("OldStatus") = "" Then
            Response.Write("0")
            Return
        End If

        If Request.QueryString("NewStatus") Is Nothing OrElse Request.QueryString("NewStatus") = "" Then
            Response.Write("0")
            Return
        End If




        strNewStatus = Request.QueryString("NewStatus")
        strOldStatus = Request.QueryString("OldStatus")
        strEventID = Request.QueryString("EventID")
        strServiceID = Request.QueryString("ServiceID")
        strAddress = Request.QueryString("Address")
        strAddress = "0" & strAddress.Substring(2)

        Dim ctxVar As New BusinessObject.Context.dbVASEntities

        Response.Write("1")



    End Sub




End Class
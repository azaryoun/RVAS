Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Threading.Tasks
Public Class Delivery
    Inherits System.Web.UI.Page

    Private Const PARDIS_REST_BASE_IP_ADDRESS As String = "http://10.20.22.18:1090/"
    Private Const PARDIS_REST_SEND_SERVICE As String = "mt.asmx"




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strPardisId As String = ""
        Dim strStatus As String = ""
        Dim strErrorMessage As String = ""

        If Request.QueryString("PardisId") Is Nothing OrElse Request.QueryString("PardisId") = "" Then

            Response.Write("0")
            Return
        End If

        If Request.QueryString("Status") Is Nothing OrElse Request.QueryString("Status") = "" Then

            Response.Write("0")
            Return
        End If

        If Request.QueryString("ErrorMessage") Is Nothing OrElse Request.QueryString("ErrorMessage") = "" Then
            strErrorMessage = ""
        Else
            strErrorMessage = Request.QueryString("ErrorMessage")
        End If

        strStatus = Request.QueryString("Status")
        strPardisId = Request.QueryString("PardisId")

        Dim ctxVar As New BusinessObject.Context.dbVASEntities

        Response.Write("1")



    End Sub




End Class
﻿Public Class AggregatorsNew
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

        End If
    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click

        Dim strName As String = txtTitle.Text.Trim
        Dim strPrePardis As String = txtPrePardis.Text.Trim
        Dim strPreIMI As String = txtPreIMI.Text.Trim
        Dim strRemarks As String = txtRemarks.Text.Trim

        Dim qryAggregators As New BusinessObject.VAS.dstAggregatorsTableAdapters.QueriesTableAdapter
        qryAggregators.spr_Aggregators_Insert(strName, strPrePardis, strPreIMI, strRemarks)

        Response.Redirect(ViewState("BackPage") & "?Save=OK")



    End Sub
End Class
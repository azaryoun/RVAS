Public Class HeadNumberNew
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


            Dim odbVAS As New BusinessObject.Context.dbVASEntities
            Dim lnqAggergators = odbVAS.tbl_Aggregators.OrderBy(Function(x) x.Name)

            For Each lnqAggergatorItem In lnqAggergators
                cmbAggregator.Items.Add(New ListItem(lnqAggergatorItem.Name, lnqAggergatorItem.ID))
            Next lnqAggergatorItem

            cmbAggregator.Items.Insert(0, "(انتخاب نمایید)")
            cmbAggregator.SelectedIndex = 0

        End If
        rdoPardisNo.Attributes.Add("onchange", "rdoPardisNo_onchange();")
        rdoPardisYes.Attributes.Add("onchange", "rdoPardisNo_onchange();")


    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click

        Dim strtheNumber As String = txttheNumber.Text.Trim
        Dim intAggregatorID As Integer = cmbAggregator.SelectedValue
        Dim blnIsForPardis As Boolean = rdoPardisYes.Checked

        Dim sngPardisAmount? As Single = Nothing

        If blnIsForPardis = True Then
            sngPardisAmount = Val(txtPardisTarif.Text)
        End If

        Dim qryHeadNumber As New BusinessObject.VAS.dstHeadNumberTableAdapters.QueriesTableAdapter

        qryHeadNumber.spr_HeadNumber_Insert(intAggregatorID, strtheNumber, blnIsForPardis, sngPardisAmount)

        Response.Redirect(ViewState("BackPage") & "?Save=OK")



    End Sub

    Protected Sub cmbAggregator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAggregator.SelectedIndexChanged
        If cmbAggregator.SelectedIndex = 0 Then
            spnPreNumber.InnerHtml = "#"
            Return
        End If

        Dim intAggergatorID As Integer = cmbAggregator.SelectedValue
        Dim odbVAS As New BusinessObject.Context.dbVASEntities
        Dim lnqAggergator = odbVAS.tbl_Aggregators.Where(Function(x) x.ID = intAggergatorID).FirstOrDefault
        If rdoPardisYes.Checked = True Then
            spnPreNumber.InnerHtml = lnqAggergator.PardisPreNumber

        Else
            spnPreNumber.InnerHtml = lnqAggergator.IMIPreNumber
        End If

        txtPardisTarif.Enabled = rdoPardisYes.Checked


    End Sub

    Private Sub rdoPardisYes_CheckedChanged(sender As Object, e As EventArgs) Handles rdoPardisYes.CheckedChanged, rdoPardisNo.CheckedChanged
        Call cmbAggregator_SelectedIndexChanged(sender, e)


    End Sub
End Class
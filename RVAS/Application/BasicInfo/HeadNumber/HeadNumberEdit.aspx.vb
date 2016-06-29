Public Class HeadNumberEdit
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


            If MyBase.Session("inttheKey") Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))


            Dim lnqHeadNumber = odbVAS.tbl_HeadNumber.Where(Function(x) x.ID = inttheKey).FirstOrDefault

            If lnqHeadNumber Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            With lnqHeadNumber
                cmbAggregator.SelectedValue = .FK_AggregatorID
                txttheNumber.Text = .theNumber
                rdoPardisNo.Checked = Not (.IsParisType)
                rdoPardisYes.Checked = .IsParisType

                If .IsParisType = True Then

                    txtPardisTarif.Text = .PardisServiceTariff.ToString
                    txtPardisTarif.Enabled = True
                    spnPreNumber.InnerHtml = .tbl_Aggregators.PardisPreNumber

                Else

                    txtPardisTarif.Text = ""
                    txtPardisTarif.Enabled = False
                    spnPreNumber.InnerHtml = .tbl_Aggregators.IMIPreNumber

                End If


            End With










        End If
        rdoPardisNo.Attributes.Add("onchange", "rdoPardisNo_onchange();")
        rdoPardisYes.Attributes.Add("onchange", "rdoPardisNo_onchange();")


    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))


        Dim strtheNumber As String = txttheNumber.Text.Trim
        Dim intAggregatorID As Integer = cmbAggregator.SelectedValue
        Dim blnIsForPardis As Boolean = rdoPardisYes.Checked

        Dim sngPardisAmount? As Single = Nothing

        If blnIsForPardis = True Then
            sngPardisAmount = Val(txtPardisTarif.Text)
        End If

        Dim qryHeadNumber As New BusinessObject.VAS.dstHeadNumberTableAdapters.QueriesTableAdapter

        qryHeadNumber.spr_HeadNumber_Update(inttheKey, intAggregatorID, strtheNumber, blnIsForPardis, sngPardisAmount)

        Response.Redirect(ViewState("BackPage") & "?Edit=OK")



    End Sub

    Protected Sub cmbAggregator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAggregator.SelectedIndexChanged

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
Public Class UnsubscriptionTermEdit
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



            If MyBase.Session("inttheKey") Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))


            Dim OdbVas As New BusinessObject.Context.dbVASEntities
            Dim lnqUnsubscriptionTerm = OdbVas.tbl_UnsubscriptionTerm.Where(Function(x) x.ID = inttheKey).FirstOrDefault

            If lnqUnsubscriptionTerm Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            With lnqUnsubscriptionTerm
                txttheTerm.Text = .theTerm
            End With



        End If
    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))

        Dim strtheTerm As String = txttheTerm.Text.Trim

        Dim qryUnsubscriptionTerm As New BusinessObject.VAS.dstUnsubscriptionTermTableAdapters.QueriesTableAdapter
        qryUnsubscriptionTerm.spr_UnsubscriptionTerm_Update(inttheKey, strtheTerm)

        Response.Redirect(ViewState("BackPage") & "?Edit=OK")



    End Sub
End Class
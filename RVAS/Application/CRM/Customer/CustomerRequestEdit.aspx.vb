Imports Service.Security
Public Class CustomerRequestEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Panel.ShowSave = False
        Bootstrap_Panel.ShowCancel = True
        Bootstrap_Panel.Enable_Save_Client_Validate = False
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

            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
            Dim OdbVas As New BusinessObject.Context.dbVASEntities


            Dim lnqCRM = OdbVas.tbl_CRM.Where(Function(x) x.FK_OwnerUserID = osctUserInfo.ID).FirstOrDefault

            If lnqCRM Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If


            With lnqCRM
                cmbRequestCategory.SelectedValue = .FK_CRMCategoryID
                txtRemark.Text = .Remarks
                txtContactNo.Text = .ContactNo
                txtResponseRemark.Text = .ResponseRemarks
            End With





        End If
    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    'Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
    '    Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))

    '    Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)

    '    Dim strRemark As String = txtRemark.Text
    '    Dim bytSatatus As Byte = 2 'Checked
    '    Dim qryCRM As New BusinessObject.VAS.dstCRMTableAdapters.QueriesTableAdapter
    '    qryCRM.spr_CRM_Update(inttheKey, bytSatatus, Date.Now, strRemark)


    '    Response.Redirect(ViewState("BackPage") & "?Edit=OK")



    'End Sub
End Class
Imports Service.Security
Public Class ContentMembershipSerialFooterEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Bootstrap_Panel.ShowSave = True
        Bootstrap_Panel.ShowCancel = True
        Bootstrap_Panel.Enable_Save_Client_Validate = True
        Bootstrap_Panel.ClearMessage()

        If Page.IsPostBack = False Then


            Dim strURL As String = Request.ServerVariables("HTTP_REFERER")
            If strURL Is Nothing Then
                Response.Redirect("~/Login.aspx")
                Return
            End If
            Dim intPos As Integer = strURL.IndexOf("?")
            If intPos <> -1 Then
                strURL = strURL.Substring(0, intPos)
            End If

            ViewState("BackPage") = strURL


            If MyBase.Session("inttheParentKey") Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If



            Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))

            Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
            Dim OdbVas As New BusinessObject.Context.dbVASEntities
            Dim lnqVASServiceMembershipSerialContentHeader = OdbVas.tbl_VASServiceMembershipSerialContentHeader.Where(Function(x) x.FK_VASServiceID = inttheParentKey).FirstOrDefault

            If lnqVASServiceMembershipSerialContentHeader Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Bootstrap_Panel.ShowPath(lnqVASServiceMembershipSerialContentHeader.theName)


            If MyBase.Session("inttheKey") Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))


            Dim lnqVASServiceMembershipSerialContentFooter = OdbVas.tbl_VASServiceMembershipSerialContentFooter.Where(Function(x) x.ID = inttheKey).FirstOrDefault

            If lnqVASServiceMembershipSerialContentFooter Is Nothing Then
                Response.Redirect(ViewState("BackPage"))
                Return
            End If

            Dim lnqVASServiceMembershipSerialContentFooterCount = OdbVas.tbl_VASServiceMembershipSerialContentFooter.Where(Function(x) x.FK_VASServiceID = inttheParentKey).Count

            For i As Integer = 1 To lnqVASServiceMembershipSerialContentFooterCount
                cmbtheOrder.Items.Add(New ListItem("قسمت #" & i.ToString, i))
            Next i


            With lnqVASServiceMembershipSerialContentFooter
                Dim tspnSendTime As TimeSpan = .SendTime
                UC_TimePicker_SendTime.Hour = tspnSendTime.Hours
                UC_TimePicker_SendTime.Minute = tspnSendTime.Minutes
                txttheText.Text = .theText
                cmbDailyInterval.SelectedValue = .IntervalDay
                cmbtheOrder.SelectedValue = .theOrder
            End With






        End If




    End Sub

    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click
        Dim inttheParentKey As Integer = CInt(MyBase.Session("inttheParentKey"))
        Dim inttheKey As Integer = CInt(MyBase.Session("inttheKey"))



        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim odbVAS As New BusinessObject.Context.dbVASEntities

        Dim strtheText As String = txttheText.Text.Trim
        Dim inttheOrder As Integer = cmbtheOrder.SelectedValue
        Dim tspnSendTime As New TimeSpan(UC_TimePicker_SendTime.Hour, UC_TimePicker_SendTime.Minute, 0)
        Dim intDailyInterval As Integer = cmbDailyInterval.SelectedValue


        If intDailyInterval = 0 Then
            Dim lnqVASServiceMembershipSerialContentFooterLast = odbVAS.tbl_VASServiceMembershipSerialContentFooter.Where(Function(x) x.FK_VASServiceID = inttheParentKey AndAlso x.theOrder <= inttheOrder AndAlso x.ID <> inttheKey).OrderByDescending(Function(x) x.theOrder).Take(1).FirstOrDefault
            If lnqVASServiceMembershipSerialContentFooterLast IsNot Nothing Then
                If lnqVASServiceMembershipSerialContentFooterLast.SendTime >= tspnSendTime Then
                    Bootstrap_Panel.ShowMessage("تاریخ ارسال با توجه به ترتیب قسمت صحیح نیست", True)
                    Return
                End If
            End If
        End If


        odbVAS.spr_VASServiceMembershipSerialContentFooter_Update(inttheKey, inttheOrder, strtheText, intDailyInterval, tspnSendTime)


        Response.Redirect(ViewState("BackPage") & "?Edit=OK")


    End Sub

End Class
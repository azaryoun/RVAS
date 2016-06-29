Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bootstrap_Panel.ShowCancel = True
        Bootstrap_Panel.ShowDelete = True
        Bootstrap_Panel.ShowDisplay = True
    End Sub

End Class
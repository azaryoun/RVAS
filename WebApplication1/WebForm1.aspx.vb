Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("~/Default.aspx")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label1.Text = Date.Now.ToString("ss")
    End Sub

    <System.Web.Services.WebMethod>
    Public Shared Sub Test()
        Dim i As Integer = 1
        ' Response.Redirect("~/Default.aspx")
    End Sub


End Class
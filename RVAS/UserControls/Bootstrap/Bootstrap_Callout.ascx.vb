Public Class Bootstrap_Callout
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Property Display() As Boolean

        Get
            If divCallout.Style("display") = "" Then
                Return True
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            If value = True Then
                divCallout.Style("display") = ""
            Else
                divCallout.Style("display") = "none"
            End If
        End Set
    End Property

    Public Property Message() As String
        Get
            Return lblMessage.Text
        End Get
        Set(ByVal value As String)
            lblMessage.Text = value
        End Set
    End Property
    Public Property ShowWarning() As Boolean
        Get
            If iconClass.Attributes("class") = "icon fa fa-ban" Then
                Return True
            Else
                Return False
            End If

        End Get
        Set(ByVal value As Boolean)

            If value = True Then

                iconClass.Attributes("class") = "icon fa fa-ban"
                divCallout.Attributes("class") = "callout callout-danger"
                lblTitle.Text = "خطا"

            Else

                iconClass.Attributes("class") = "icon fa fa-info"
                divCallout.Attributes("class") = "callout callout-info"
                lblTitle.Text = "توجه"
            End If
        End Set
    End Property

End Class
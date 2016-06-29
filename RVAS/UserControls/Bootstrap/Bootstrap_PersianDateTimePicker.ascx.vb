Public Class Bootstrap_PersianDateTimePicker
    Inherits System.Web.UI.UserControl
    Public _ShowTimePicker As Boolean = False


    Public Property ShowTimePicker() As Boolean
        Get
            Return _ShowTimePicker
        End Get
        Set(ByVal value As Boolean)
            _ShowTimePicker = value
        End Set
    End Property


    Public Property PickerLabel As String
        Get
            Return lblTitle.Text
        End Get
        Set(value As String)
            lblTitle.Text = value
        End Set
    End Property
    Public Property PersianDateTime As String
        Get
            Return txtDateTime.Text.Trim
        End Get
        Set(value As String)
            txtDateTime.Text = value
        End Set
    End Property

    Public Property GergorainDateTime As Date

        Get
            Dim dteDate As Date = mdlGeneral.GetGregorianDate(PersianDate)
            If _ShowTimePicker = True Then
                Dim strTime As String = PersianTime
                dteDate = dteDate.AddHours(CInt(strTime.Substring(0, 2))).AddMinutes(strTime.Substring(3, 2)).AddSeconds(strTime.Substring(6, 2))
            End If
            Return dteDate
        End Get
        Set(value As Date)
            txtDateTime.Text = mdlGeneral.GetPersianDateTime(value)
        End Set
    End Property

    Public ReadOnly Property PersianDate As String
        Get
            Return txtDateTime.Text.Substring(0, 10).Trim
        End Get
    End Property

    Public ReadOnly Property PersianTime As String
        Get
            If _ShowTimePicker = False Then
                Return ""
            Else
                Return txtDateTime.Text.Substring(11).Trim
            End If

        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

End Class
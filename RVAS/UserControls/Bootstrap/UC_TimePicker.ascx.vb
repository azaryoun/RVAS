Public Partial Class UC_TimePicker
    Inherits System.Web.UI.UserControl

    'Private intHour As String
    'Private intMinute As String
    Private intTabIndex As Integer = 0


    Public ReadOnly Property Value() As String
        Get
            Return cmbHour.SelectedItem.Text & ":" & cmbMinute.SelectedItem.Text
        End Get
        
    End Property


    Public Property Hour() As Integer
        Get
            Return cmbHour.SelectedValue
        End Get
        Set(ByVal value As Integer)
            Try
                cmbHour.SelectedValue = value.ToString("00")
            Catch ex As Exception
                cmbHour.SelectedValue = -1
            End Try

        End Set
    End Property

    Public Property Minute() As Integer
        Get
            Return cmbMinute.SelectedValue
        End Get
        Set(ByVal value As Integer)
            Try
                cmbMinute.SelectedValue = value.ToString("00")
            Catch ex As Exception
                cmbMinute.SelectedValue = -1
            End Try

        End Set
    End Property







    Public WriteOnly Property TabIndex() As Integer
        Set(ByVal value As Integer)
            intTabIndex = value
        End Set
    End Property


    Public WriteOnly Property Enable() As Boolean
        Set(ByVal value As Boolean)
            cmbHour.Enabled = value
            cmbMinute.Enabled = value

        End Set
    End Property



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        cmbHour.Attributes.Add("TABINDEX", intTabIndex)
        cmbMinute.Attributes.Add("TABINDEX", intTabIndex)



    End Sub

End Class
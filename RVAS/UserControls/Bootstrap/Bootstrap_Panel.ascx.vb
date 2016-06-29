Public Class Bootstrap_Panel
    Inherits System.Web.UI.UserControl
    Public Event Panel_New_Click As EventHandler
    Public Event Panel_Save_Click As EventHandler
    Public Event Panel_Cancel_Click As EventHandler
    Public Event Panel_Delete_Click As EventHandler
    Public Event Panel_Up_Click As EventHandler
    Public Event Panel_Magic_Click As EventHandler
    Public Event Panel_Display_Click As EventHandler
    Public Event Panel_Excel_Click As EventHandler
    Public Event Panel_PDF_Click As EventHandler
    Private P_blnEnable_New_Client_Validate As Boolean = False
    Private P_blnEnable_Save_Client_Validate As Boolean = False
    Private P_blnEnable_Cancel_Client_Validate As Boolean = False
    Private P_blnEnable_Delete_Client_Validate As Boolean = False
    Private P_blnEnable_Magic_Client_Validate As Boolean = False
    Private P_blnEnable_Up_Client_Validate As Boolean = False
    Private P_blnEnable_Display_Client_Validate As Boolean = False
    Private P_blnEnable_Excel_Client_Validate As Boolean = False
    Private P_blnEnable_PDF_Client_Validate As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "Click Events"
    Private Sub btnPanel_Cancel_Click(sender As Object, e As EventArgs) Handles btnPanel_Cancel.Click
        RaiseEvent Panel_Cancel_Click(sender, e)
    End Sub

    Private Sub btnPanel_Delete_Click(sender As Object, e As EventArgs) Handles btnPanel_Delete.Click
        RaiseEvent Panel_Delete_Click(sender, e)

    End Sub

    Private Sub btnPanel_Display_Click(sender As Object, e As EventArgs) Handles btnPanel_Display.Click
        RaiseEvent Panel_Display_Click(sender, e)
    End Sub

    Private Sub btnPanel_Excel_Click(sender As Object, e As EventArgs) Handles btnPanel_Excel.Click
        RaiseEvent Panel_Excel_Click(sender, e)
    End Sub

    Private Sub btnPanel_Magic_Click(sender As Object, e As EventArgs) Handles btnPanel_Magic.Click
        RaiseEvent Panel_Magic_Click(sender, e)
    End Sub

    Private Sub btnPanel_New_Click(sender As Object, e As EventArgs) Handles btnPanel_New.Click
        RaiseEvent Panel_New_Click(sender, e)
    End Sub

    Private Sub btnPanel_PDF_Click(sender As Object, e As EventArgs) Handles btnPanel_PDF.Click
        RaiseEvent Panel_PDF_Click(sender, e)
    End Sub

    Private Sub btnPanel_Save_Click(sender As Object, e As EventArgs) Handles btnPanel_Save.Click
        RaiseEvent Panel_Save_Click(sender, e)
    End Sub

    Private Sub btnPanel_Up_Click(sender As Object, e As EventArgs) Handles btnPanel_Up.Click
        RaiseEvent Panel_Up_Click(sender, e)
    End Sub

#End Region

#Region "Visible Property"
    Public Property ShowCancel As Boolean
        Get
            Return btnPanel_Cancel.Visible
        End Get
        Set(value As Boolean)
            btnPanel_Cancel.Visible = value
        End Set
    End Property

    Public Property ShowDelete As Boolean
        Get
            Return btnPanel_Delete.Visible
        End Get
        Set(value As Boolean)
            btnPanel_Delete.Visible = value
        End Set
    End Property

    Public Property ShowDisplay As Boolean
        Get
            Return btnPanel_Display.Visible
        End Get
        Set(value As Boolean)
            btnPanel_Display.Visible = value
        End Set
    End Property

    Public Property ShowExcel As Boolean
        Get
            Return btnPanel_Excel.Visible
        End Get
        Set(value As Boolean)
            btnPanel_Excel.Visible = value
        End Set
    End Property

    Public Property ShowMagic As Boolean
        Get
            Return btnPanel_Magic.Visible
        End Get
        Set(value As Boolean)
            btnPanel_Magic.Visible = value
        End Set
    End Property

    Public Property ShowNew As Boolean
        Get
            Return btnPanel_New.Visible
        End Get
        Set(value As Boolean)
            btnPanel_New.Visible = value
        End Set
    End Property

    Public Property ShowPDF As Boolean
        Get
            Return btnPanel_PDF.Visible
        End Get
        Set(value As Boolean)
            btnPanel_PDF.Visible = value
        End Set
    End Property

    Public Property ShowSave As Boolean
        Get
            Return btnPanel_Save.Visible
        End Get
        Set(value As Boolean)
            btnPanel_Save.Visible = value
        End Set
    End Property

    Public Property ShowUp As Boolean
        Get
            Return btnPanel_Up.Visible
        End Get
        Set(value As Boolean)
            btnPanel_Up.Visible = value
        End Set
    End Property



#End Region

#Region "Message"
    Public Sub ShowMessage(strMessage As String, blnWarning As Boolean)
        Bootstrap_Callout.Display = True
        Bootstrap_Callout.ShowWarning = blnWarning
        Bootstrap_Callout.Message = strMessage
    End Sub

    Public Sub ClearMessage()
        Bootstrap_Callout.Display = False
    End Sub

    Public Sub ClearPath()
        pPath.Style.Item("display") = "none"
    End Sub
    Public Sub ShowPath(strthePath As String)
        pPath.Style.Item("display") = ""
        pPath.InnerHtml = strthePath
    End Sub

#End Region

#Region "Client Validation"

    Public Property Enable_Cancel_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Cancel_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Cancel_Client_Validate = value
            If value = True Then
                btnPanel_Cancel.Attributes.Item("onclick") = "return CancelOperation_Validate();"
            Else
                btnPanel_Cancel.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_Delete_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Delete_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Delete_Client_Validate = value
            If value = True Then
                btnPanel_Delete.Attributes.Item("onclick") = "return DeleteOperation_Validate();"
            Else
                btnPanel_Delete.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_Display_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Display_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Display_Client_Validate = value
            If value = True Then
                btnPanel_Display.Attributes.Item("onclick") = "return DisplayOperation_Validate();"
            Else
                btnPanel_Display.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_Excel_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Excel_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Excel_Client_Validate = value
            If value = True Then
                btnPanel_Excel.Attributes.Item("onclick") = "return ExcelOperation_Validate();"
            Else
                btnPanel_Excel.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property
    Public Property Enable_Magic_Client_Validate() As Boolean

        Get
            Return P_blnEnable_Magic_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Magic_Client_Validate = value
            If value = True Then
                btnPanel_Magic.Attributes.Item("onclick") = "return MagicOperation_Validate();"
            Else
                btnPanel_Magic.Attributes.Item("onclick") = "return true;"
            End If
        End Set

    End Property

    Public Property Enable_New_Client_Validate() As Boolean
        Get
            Return P_blnEnable_New_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_New_Client_Validate = value
            If value = True Then
                btnPanel_New.Attributes.Item("onclick") = "return NewOperation_Validate();"
            Else
                btnPanel_New.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property
    Public Property Enable_PDF_Client_Validate() As Boolean
        Get
            Return P_blnEnable_PDF_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_PDF_Client_Validate = value
            If value = True Then
                btnPanel_PDF.Attributes.Item("onclick") = "return PDFOperation_Validate();"
            Else
                btnPanel_PDF.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property
    Public Property Enable_Save_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Save_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Save_Client_Validate = value
            If value = True Then
                btnPanel_Save.Attributes.Item("onclick") = "return SaveOperation_Validate();"
            Else
                btnPanel_Save.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property
    Public Property Enable_Up_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Up_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Up_Client_Validate = value
            If value = True Then
                btnPanel_Up.Attributes.Item("onclick") = "return UpOperation_Validate();"
            Else
                btnPanel_Up.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property


#End Region


End Class
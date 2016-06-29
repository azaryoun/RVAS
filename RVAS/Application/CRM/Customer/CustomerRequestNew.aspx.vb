Imports System.Runtime.InteropServices
Public Class CustomerRequestNew
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





            'Dim tadpMenuLeafList As New BusinessObject.Administration.dstMenuTableAdapters.spr_Menu_Leaf_List_SelectTableAdapter
            'Dim dtblMenuLeafList As BusinessObject.Administration.dstMenu.spr_Menu_Leaf_List_SelectDataTable = Nothing
            'dtblMenuLeafList = tadpMenuLeafList.GetData()

            'Dim strchklstMenuLeaves As String = ""

            'For Each drwMeneLeafList As BusinessObject.Administration.dstMenu.spr_Menu_Leaf_List_SelectRow In dtblMenuLeafList.Rows

            '    strchklstMenuLeaves &= "<div class='checkbox'><label><input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "' /> &nbsp;&nbsp;&nbsp; <i class='" & drwMeneLeafList.IconStyle & "'></i> " & drwMeneLeafList.Menutitlepath & "</label></div>"
            'Next drwMeneLeafList





        End If


    End Sub
    <DllImport("urlmon.dll", CharSet:=CharSet.Auto)>
    Private Shared Function FindMimeFromData(pBC As System.UInt32,
                                             <MarshalAs(UnmanagedType.LPStr)> pwzUrl As System.String,
                                             <MarshalAs(UnmanagedType.LPArray)> pBuffer As Byte(),
                                             cbSize As System.UInt32, <MarshalAs(UnmanagedType.LPStr)>
                                             pwzMimeProposed As System.String, dwMimeFlags As System.UInt32,
    ByRef ppwzMimeOut As System.UInt32, dwReserverd As System.UInt32) As System.UInt32
    End Function
    Private Sub Bootstrap_Panel_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Cancel_Click
        Response.Redirect(ViewState("BackPage"))
    End Sub

    Private Sub Bootstrap_Panel_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel.Panel_Save_Click

        Dim osctUserInfo As Service.Security.stcUserInfo = CType(Session("stcUserInfo"), Service.Security.stcUserInfo)
        Dim odbVAS As New BusinessObject.Context.dbVASEntities

        'Dim arrLetter() As Byte = Nothing
        Dim document As Byte() = Nothing
        Dim mime As String = String.Empty
        If fleFileImage.PostedFile IsNot Nothing AndAlso fleFileImage.PostedFile.ContentLength <> 0 Then

            Dim file As HttpPostedFile = fleFileImage.PostedFile
            document = New Byte(file.ContentLength - 1) {}
            file.InputStream.Read(document, 0, file.ContentLength)
            Dim mimetype As System.UInt32
            FindMimeFromData(0, Nothing, document, 256, Nothing, 0, mimetype, 0)
            Dim mimeTypePtr As System.IntPtr = New IntPtr(mimetype)
            mime = Marshal.PtrToStringUni(mimeTypePtr)
            Marshal.FreeCoTaskMem(mimeTypePtr)

            'Dim strmImage As IO.Stream = fleFileImage.PostedFile.InputStream
            'ReDim arrLetter(strmImage.Length)
            'strmImage.Read(arrLetter, 0, arrLetter.Length)

        End If

        Dim intCRMCategoryID As Integer = cmbRequestCategory.SelectedValue
        Dim strRemarks As String = txtRemark.Text.Trim
        Dim strContactNo As String = txtContactNo.Text.Trim
        Dim bytAttachment() As Byte = document
        ' Dim bytAttachment As Byte
        Dim strMimeAttachment As String = mime
        Dim bytStatus As Byte = 1 'UnderCheck
        Dim dteResponseDate? As Date = Nothing
        Dim strResponseRemark As String = String.Empty


        Dim qryCRM As New BusinessObject.VAS.dstCRMTableAdapters.QueriesTableAdapter
        Dim intCRMID As Integer = qryCRM.spr_CRM_Insert(osctUserInfo.ID, intCRMCategoryID, strRemarks, strContactNo, bytAttachment, strMimeAttachment, Date.Now, bytStatus, dteResponseDate, strResponseRemark)
        Response.Redirect(ViewState("BackPage") & "?Save=OK")

    End Sub
End Class
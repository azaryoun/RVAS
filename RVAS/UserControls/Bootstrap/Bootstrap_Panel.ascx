<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Bootstrap_Panel.ascx.vb" Inherits="RVAS.Bootstrap_Panel" %>
<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Callout.ascx" TagPrefix="uc1" TagName="Bootstrap_Callout" %>

<style type="text/css">
    .FixPanel
    {
        position: fixed;
        top: 50px;
        z-index: 999999999;
    }
</style>

<div class="box-body" id="divMainPanel">
    
     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_New" runat="server" ToolTip="تعریف رکورد جدید" Visible="False">  <i class="fa fa-plus-square"></i> جدید</asp:LinkButton>
   
    <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Save" runat="server" ToolTip="ذخیره رکورد جاری" Visible="False">    <i class="fa fa-save"></i> ذخیره</asp:LinkButton>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
    <ContentTemplate>
     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Cancel" runat="server" ToolTip="لغو تغییرات اعمال شده" Visible="False">  <i class="fa fa-close"></i> لغو</asp:LinkButton>
    
 <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Delete" runat="server" ToolTip="حذف ردیف های انتخاب شده" Visible="False">   <i class="fa fa-eraser"></i> حذف</asp:LinkButton>

     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Magic" runat="server" ToolTip="انجام یک معجزه" Visible="False">      <i class="fa fa-magic"></i> جادو</asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Up" runat="server" ToolTip="بازگشت به مرحله قبل" Visible="False">    <i class="fa fa-eject"></i> بالا</asp:LinkButton>
     
           </ContentTemplate>

</asp:UpdatePanel>

       <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Display" runat="server" ToolTip="نمایش گزارش" Visible="False">   <i class="fa fa-table"></i> نمایش</asp:LinkButton>
    
           <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_PDF" runat="server" ToolTip="دانلود خروجی به صورت PDF" Visible="False">    <i class="fa fa-file-pdf-o"></i> PDF</asp:LinkButton>
    <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Excel" runat="server" ToolTip="دانلود خروجی به صورت Excel" Visible="False">    <i class="fa fa-file-excel-o"></i> Excel</asp:LinkButton>
       <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
    <ContentTemplate>
       <p runat="server" id="pPath" style="display:none">
           
       </p>
     
<uc1:Bootstrap_Callout runat="server" ID="Bootstrap_Callout" />
    
        </ContentTemplate></asp:UpdatePanel>
  


   </div>

 

<%@ Page Title="" Language="C#" MasterPageFile="~/Application/MasterPage/Smart.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SmartInterface.Application.WebForm1" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Callout.ascx" TagPrefix="uc1" TagName="Bootstrap_Callout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="box-body">
   
     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_New" runat="server" ToolTip="تعریف رکورد جدید" Visible="False">  <i class="fa fa-plus-square"></i> جدید</asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Save" runat="server" ToolTip="ذخیره رکورد جاری" Visible="False">    <i class="fa fa-save"></i> ذخیره</asp:LinkButton>
     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Cancel" runat="server" ToolTip="لغو تغییرات اعمال شده" Visible="False" OnClick="btnPanel_Cancel_Click">  <i class="fa fa-close"></i> لغو</asp:LinkButton>
     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Delete" runat="server" ToolTip="حذف ردیف های انتخاب شده" Visible="False">   <i class="fa fa-eraser"></i> حذف</asp:LinkButton>
   
     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Magic" runat="server" ToolTip="انجام یک معجزه" Visible="False">      <i class="fa fa-magic"></i> جادو</asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Up" runat="server" ToolTip="بازگشت به مرحله قبل" Visible="False">    <i class="fa fa-eject"></i> بالا</asp:LinkButton>
     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Display" runat="server" ToolTip="نمایش گزارش" Visible="False">   <i class="fa fa-table"></i> نمایش</asp:LinkButton>
     <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_PDF" runat="server" ToolTip="دانلود خروجی به صورت PDF" Visible="False">    <i class="fa fa-file-pdf-o"></i> PDF</asp:LinkButton>
    <asp:LinkButton CssClass="btn btn-app" ID="btnPanel_Excel" runat="server" ToolTip="دانلود خروجی به صورت Excel" Visible="False">    <i class="fa fa-file-excel-o"></i> Excel</asp:LinkButton>
  
   
   
                <uc1:Bootstrap_Callout runat="server" ID="Bootstrap_Callout" />
                 
                  </div>

</asp:Content>

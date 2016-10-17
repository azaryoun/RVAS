<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="ContentMembershipSerialNew.aspx.vb" Inherits="RVAS.ContentMembershipSerialNew" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>
<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_PersianDateTimePicker.ascx" TagPrefix="uc1" TagName="Bootstrap_PersianDateTimePicker" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txttheName = document.getElementById("<%=txttheName.ClientID %>")
            var cmbVASServices = document.getElementById("<%=cmbVASServices.ClientID %>")
         
            if (cmbVASServices.selectedIndex==0) {
                alert("سرویس مرتبط انتخاب نشده است");
                cmbVASServices.focus();  
                return false;
            }
          
            if (trimall(txttheName.value) == "") {
                alert("عنوان سریال وارد نشده است");
                txttheName.focus();
                return false;
            }
            return true;

        }

        
       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   



    <section class="content">
    <div class="row">
    <div class="col-xs-12">
         <uc1:Bootstrap_Panel runat="server" ID="Bootstrap_Panel" />
    </div>
    </div>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                
        
                        <div class="row">
            <div class="col-md-12">
              <div class="box box-danger">
                <div class="box-header">
                  <h3 class="box-title">سرویس سریال</h3>
                </div>
                <div class="box-body">
           

     <div class="form-group has-error">
                      <label  >سرویس مرتبط</label>
             <asp:DropDownList ID="cmbVASServices" CssClass="form-control select2" style="width:100%;direction:rtl" runat="server">
                        </asp:DropDownList>       
               </div>


     


                </div>
              </div>

        <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">سریال</h3>
                </div>
                <div class="box-body">
               
                     <div class="form-group has-error">
                      <label  title="Footer Text">عنوان سریال</label>
               <asp:TextBox ID="txttheName" runat="server" CssClass="form-control" placeholder="عنوان سریال را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                  
                      </div>
                    
                    <div class="form-group">
                      <label  title="Footer Text">تاریخ شروع اعتبار</label>
         <uc1:Bootstrap_PersianDateTimePicker runat="server" ID="Bootstrap_PersianDateTimePicker_StartFrom"   ShowTimePicker="false" />

                      </div>
  
                             <div class="form-group">
                      <label  title="Footer Text">تاریخ پایان اعتبار</label>
         <uc1:Bootstrap_PersianDateTimePicker runat="server" ID="Bootstrap_PersianDateTimePicker_EndAt"   ShowTimePicker="false" />

                      </div>
  

             
                 <div class="form-group">
                    <label>وضعیت سریال</label>
                    <div class="input-group">
                        <asp:RadioButton ID="rdoActiveYes"  Text="فعال"  runat="server" Checked="true"  GroupName="Active" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoActiveNo" Text="غیر فعال"  runat="server" GroupName="Active" />
                    </div>
                  </div>


                </div>
              </div>

      
                


            </div>
            


        
          </div>




                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

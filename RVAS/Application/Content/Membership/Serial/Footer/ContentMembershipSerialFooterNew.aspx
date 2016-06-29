<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="ContentMembershipSerialFooterNew.aspx.vb" Inherits="RVAS.ContentMembershipSerialFooterNew" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>
<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_PersianDateTimePicker.ascx" TagPrefix="uc1" TagName="Bootstrap_PersianDateTimePicker" %>
<%@ Register Src="~/UserControls/Bootstrap/UC_TimePicker.ascx" TagPrefix="uc1" TagName="UC_TimePicker" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txttheText = document.getElementById("<%=txttheText.ClientID %>")
          
           
            if (trimall(txttheText.value) == "") {
                alert("متن قسمت وارد نشده است");
                txttheText.focus();
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
              <div class="box box-info">
                <div class="box-header">
                  <h3 class="box-title">قسمت سریال</h3>
                </div>
                <div class="box-body">
           
                    
     <div class="form-group has-error">
                      <label>متن این قمست</label>
                  <asp:TextBox ID="txttheText" runat="server" CssClass="form-control" placeholder="متن این قسمت را وارد نمایید ..."  TextMode="MultiLine" MaxLength="500" Height="75px" ></asp:TextBox>
                
               </div>


     <div class="form-group has-error">
                      <label>شماره و ترتیب قمست</label>
             <asp:DropDownList ID="cmbtheOrder" CssClass="form-control select2" style="width:100%;direction:ltr" runat="server">
                        </asp:DropDownList>       
               </div>


     


                </div>
              </div>

        <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">ارسال</h3>
                </div>
                <div class="box-body">
               
                     
                <div class="form-group has-error">
                      <label>بازه روزانه</label>
             <asp:DropDownList ID="cmbDailyInterval" CssClass="form-control select2" style="width:100%;direction:ltr" runat="server">
                 <asp:ListItem Text="0" Value="0"></asp:ListItem> 
                 <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem> 
                 <asp:ListItem Text="2" Value="2"></asp:ListItem> 
                 <asp:ListItem Text="3" Value="3"></asp:ListItem> 
                 <asp:ListItem Text="4" Value="4"></asp:ListItem> 
                 <asp:ListItem Text="5" Value="5"></asp:ListItem> 
                 <asp:ListItem Text="6" Value="6"></asp:ListItem> 
                 <asp:ListItem Text="7" Value="7"></asp:ListItem> 
                 <asp:ListItem Text="8" Value="8"></asp:ListItem>    
                 <asp:ListItem Text="9" Value="9"></asp:ListItem>
                 <asp:ListItem Text="10" Value="10"></asp:ListItem>
              </asp:DropDownList> 
              <p class="help-block">اگر می خواهید ارسال در همان روز قسمت قبلی انجام شود، مقدار بازه را برابر با عدد صفر انتخاب نمایید.</p>    
                   
               </div>

                    
                    <div class="form-group  has-error">
                      <label  title="Footer Text">ساعت و دقیقه ارسال</label>
                        <uc1:UC_TimePicker runat="server" id="UC_TimePicker_SendTime" />
                      </div>
  

         

                </div>
              </div>

      
                


            </div>
            


        
          </div>




                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

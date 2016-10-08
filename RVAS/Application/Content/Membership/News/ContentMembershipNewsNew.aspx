<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="ContentMembershipNewsNew.aspx.vb" Inherits="RVAS.ContentMembershipNewsNew" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>
<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_PersianDateTimePicker.ascx" TagPrefix="uc1" TagName="Bootstrap_PersianDateTimePicker" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txttheText = document.getElementById("<%=txttheText.ClientID %>")
         
          
            if (trimall(txttheText.value) == "") {
                alert("متن خبر وارد نشده است");
                txttheText.focus();
                return false;
            }
            return true;

        }

        
       
    </script>
    <style >

        label
        {
width: 100%;

        }

    </style>
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
                  <h3 class="box-title">محتوای خبری</h3>
                </div>
                <div class="box-body">
           

     <div class="form-group has-error">
                      <label>متن خبر</label>
                        <asp:TextBox ID="txttheText" runat="server" CssClass="form-control" placeholder="متن خبر را وارد نمایید ..."  TextMode="MultiLine" MaxLength="500" Height="75px" ></asp:TextBox>
                      </div>


     <div class="form-group">
                      <label  title="Footer Text">تاریخ و ساعت ارسال</label>
         <uc1:Bootstrap_PersianDateTimePicker runat="server" ID="Bootstrap_PersianDateTimePicker_SendDateTime"   ShowTimePicker="True" />

                      </div>
      


                </div>
              </div>

        <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">سرویس ها</h3>
                </div>
                <div class="box-body">
           
                     <div class="form-group">
                     <label>سرویس های خبری مرتبط</label>
                      
                      <div  runat="server" id="divchklstVASServices" style="max-height: 200px; overflow-y: scroll; border:1px dotted #D2D6DE;padding-right:3px">
                      
                      
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

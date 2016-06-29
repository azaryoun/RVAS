<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="VASServiceMembershipNewsEdit.aspx.vb" Inherits="RVAS.VASServiceMembershipNewsEdit" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

             var txtServiceID = document.getElementById("<%=txtServiceID.ClientID %>")
            var txtServiceName = document.getElementById("<%=txtServiceName.ClientID %>")
            var txtKeySubscription = document.getElementById("<%=txtKeySubscription.ClientID %>")
            var txtKeyUnsubscription = document.getElementById("<%=txtKeyUnsubscription.ClientID %>")
      

          
            if (trimall(txtServiceID.value) == "") {
                alert("ServiceID  را وارد نمایید");
                txtServiceID.focus();
                return false;
            }


            if (trimall(txtServiceName.value) == "") {
                alert("عنوان سرویس وارد نشده است");
                txtServiceName.focus();
                return false;
            }

            

            if (trimall(txtKeySubscription.value) == "") {
                alert("کلید مشترک شدن وارد نشده است");
                txtKeySubscription.focus();
                return false;
            }


            if (trimall(txtKeyUnsubscription.value) == "") {
                alert("کلید لغو اشتراک وارد نشده است");
                txtKeyUnsubscription.focus();
                return false;
            }

     

            if (trimall(txtKeySubscription.value.toUpperCase()) == trimall(txtKeyUnsubscription.value.toUpperCase())) {
                alert("کلید اشتراک و لغو اشتراک نباید برابر باشند");
                txtKeySubscription.focus();
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
            <div class="col-md-6">
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">متن ها</h3>
                </div>
                <div class="box-body">
           

     <div class="form-group">
                      <label  title="Header Text">متن سربرگ</label>
                        <asp:TextBox ID="txtTextHeader" runat="server" CssClass="form-control" placeholder="متن سربرگ را وارد نمایید ..."  TextMode="MultiLine" MaxLength="250" Height="75px" ></asp:TextBox>
                      </div>

     <div class="form-group">
                      <label  title="Footer Text">متن پابرگ</label>
                        <asp:TextBox ID="txtTextFooter" runat="server" CssClass="form-control" placeholder="متن پابرگ را وارد نمایید ..."  TextMode="MultiLine" MaxLength="250" Height="75px" ></asp:TextBox>
                      </div>
       <div class="form-group">
                      <label  title="Welcome Text">متن خوش آمد</label>
                        <asp:TextBox ID="txtTextWelcome" runat="server" CssClass="form-control" placeholder="متن خوش آمد گویی را وارد نمایید ..."  TextMode="MultiLine" MaxLength="250" Height="75px" ></asp:TextBox>
                      </div>
       <div class="form-group">
                      <label  title="Good bye Text">متن خداحافظی</label>
                        <asp:TextBox ID="txtTextGoodbye" runat="server" CssClass="form-control" placeholder="متن خداحافظی را وارد نمایید ..." TextMode="MultiLine" MaxLength="250" Height="75px" ></asp:TextBox>
                      </div>



             

                </div>
              </div>

        <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">تعرفه ها</h3>
                </div>
                <div class="box-body">
           
                <div class="form-group">
                    <label >بهای سرویس (تجمیع کننده) به ریال</label>
                        <asp:TextBox ID="txtServicePrice" runat="server" style="text-align:left" CssClass="form-control numericAmount" placeholder="تعرفه خرید سرویس از کارفرما را وارد نمایید"  TextMode="SingleLine" MaxLength="20" ></asp:TextBox>
                </div>
                 <div class="form-group">
                    <label >تعرفه سرویس IMI برای مشتری نهایی به ریال</label>
                        <asp:TextBox ID="txtIMIEnduserTariff" runat="server" style="text-align:left" CssClass="form-control numericAmount" placeholder="تعرفه فروش سرویس به مشتری نهایی را وارد نمایید"  TextMode="SingleLine" MaxLength="20" ></asp:TextBox>
                </div>

             

                </div>
              </div>

       

          <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title">اشتراک</h3>
                </div>
                <div class="box-body">
           
                <div class="form-group has-error">
                      <label >کلید مشترک شدن</label>
                        <asp:TextBox ID="txtKeySubscription" runat="server" style="text-align:left" CssClass="form-control" placeholder="عبارت مشترک شدن را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                        <div class="form-group has-error">
                      <label >کلید لغو اشتراک</label>
                        <asp:TextBox ID="txtKeyUnsubscription" runat="server" style="text-align:left" CssClass="form-control" placeholder="عبارت لغو اشتراک را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                    
             

                </div>
              </div>


                


            </div>
            


            <div class="col-md-6">

              <div class="box box-danger">
                <div class="box-header">
                  <h3 class="box-title">مالک سرویس</h3>
                </div>
                <div class="box-body">
                  <!-- Date dd/mm/yyyy -->

                  <div class="form-group has-success">
                      <label>کاربر مالک </label>
                        <asp:TextBox ID="txtUserOwner" ReadOnly="true" runat="server" CssClass="form-control"  TextMode="SingleLine" MaxLength="80" ></asp:TextBox>
                  </div>

                      

     

                </div><!-- /.box-body -->
              </div>

             <div class="box box-danger">
                <div class="box-header">
                  <h3 class="box-title">اطلاعات تجمیع کننده</h3>
                </div>
                <div class="box-body">
                

                  <div class="form-group has-success">

                      <label>تجمیع کننده </label>
                       <asp:TextBox ID="txtAggergator" ReadOnly="true" runat="server" CssClass="form-control"  TextMode="SingleLine" MaxLength="80" ></asp:TextBox>
           

                     
                  </div>

                            
                 <div class="form-group has-success">
                    <label>نوع</label>
                      <asp:TextBox ID="txtIsParis" ReadOnly="true" runat="server" CssClass="form-control"  TextMode="SingleLine" MaxLength="80" ></asp:TextBox>
           

                  </div>

        <div class="form-group has-success">

                      <label>شماره سرویس </label>
                <asp:TextBox ID="txttheWholeNumber" ReadOnly="true" runat="server" style="text-align:left" CssClass="form-control"  TextMode="SingleLine" MaxLength="80" ></asp:TextBox>
                    

                  </div>

                     <div class="form-group has-error">
                      <label >Service ID</label>
                        <asp:TextBox ID="txtServiceID" runat="server" style="text-align:left" CssClass="form-control" placeholder="Service ID را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>


                </div><!-- /.box-body -->
              </div>


                 <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title">اطلاعات سرویس</h3>
                </div>
                <div class="box-body">
                



                  <div class="form-group has-error">



                      <label>عنوان سرویس</label>
                   
                        <asp:TextBox ID="txtServiceName" runat="server" CssClass="form-control" placeholder="عنوان سرویس را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
              
                  </div>

            <div class="form-group has-error">


                      <label>نوع محتوا </label>
                           <asp:DropDownList ID="cmbCategoryVASService" CssClass="form-control select2" style="width:100%" runat="server" ></asp:DropDownList>
                   
                  </div>
                    
                            
                 <div class="form-group">
                    <label>وضعیت سرویس</label>
                    <div class="input-group">
                        <asp:RadioButton ID="rdoServiceActiveYes"  Text="فعال"  runat="server" Checked="true"  GroupName="Active" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoServiceActiveNo"   Text="غیر فعال"  runat="server" GroupName="Active" />
                    </div><!-- /.input group -->
                  </div>

                        <div class="form-group">
                      <label  >ملاحظات</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" placeholder="سایر توضیحات و ملاحظات ..." TextMode="MultiLine" MaxLength="350" Height="75px" ></asp:TextBox>
                      </div>


                </div><!-- /.box-body -->
              </div>

                
            </div>
        
          </div>




                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

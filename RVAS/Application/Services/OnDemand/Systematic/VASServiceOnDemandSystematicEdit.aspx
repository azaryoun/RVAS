<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="VASServiceOnDemandSystematicEdit.aspx.vb" Inherits="RVAS.VASServiceOnDemandSystematicEdit" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txtServiceID = document.getElementById("<%=txtServiceID.ClientID %>")
            var txtServiceName = document.getElementById("<%=txtServiceName.ClientID %>")
            var cmbtheTypeDemand = document.getElementById("<%=cmbtheTypeDemand.ClientID %>")
            var txtKeyDemand = document.getElementById("<%=txtKeyDemand.ClientID %>")
            var txtWebServicePath = document.getElementById("<%=txtWebServicePath.ClientID %>")
            var txtWebServiceMethod = document.getElementById("<%=txtWebServiceMethod.ClientID %>")
     

          
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

            

            if (cmbtheTypeDemand.selectedIndex != 2) {
                if (trimall(txtKeyDemand.value) == "") {
                    alert("کلید تقاضا وارد نشده است");
                    txtKeyDemand.focus();
                    return false;

                }

            }

            if (trimall(txtWebServicePath.value) == "") {
                alert("نشانی وب سرویس را وارد نمایید");
                txtWebServicePath.focus();
                return false;
            }



            if (trimall(txtWebServiceMethod.value) == "") {
                alert("عنوان متد وب سرویس را وارد نمایید");
                txtWebServiceMethod.focus();
                return false;
            }


            return true;

        }



        
        function cmbtheTypeDemand_onchange() {
            var cmbtheTypeDemand = document.getElementById("<%=cmbtheTypeDemand.ClientID %>")
            var txtKeyDemand = document.getElementById("<%=txtKeyDemand.ClientID %>")
      
            if (cmbtheTypeDemand.selectedIndex==2)
            {
                txtKeyDemand.disabled = true;
            }
            else
            {
                txtKeyDemand.disabled = false;
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


                      <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">وب سرویس</h3>
                </div>
                <div class="box-body">
           
                <div class="form-group has-error">
                    <label >نشانی وب سرویس</label>
                        <asp:TextBox ID="txtWebServicePath" runat="server" style="text-align:left" CssClass="form-control" placeholder="URL وب سرویس را وارد نمایید ..."  TextMode="SingleLine" MaxLength="250" ></asp:TextBox>
                </div>
                 <div class="form-group has-error">
                    <label >متد فراخوانی</label>
                        <asp:TextBox ID="txtWebServiceMethod" runat="server" style="text-align:left" CssClass="form-control" placeholder="متد وب سرویس را وارد نمایید ..."  TextMode="SingleLine" MaxLength="50" ></asp:TextBox>
               </div>
                <div class="form-group">
                    <label >نام کاربری</label>
                        <asp:TextBox ID="txtWebServiceUsername" runat="server" style="text-align:left" CssClass="form-control" placeholder="نام کاربری متد وب سرویس را وارد نمایید ..."  TextMode="SingleLine" MaxLength="50" ></asp:TextBox>
               </div>
                 <div class="form-group">
                    <label >رمز عبور</label>
                        <asp:TextBox ID="txtWebServicePassword" runat="server" style="text-align:left" CssClass="form-control" placeholder="گذر واژه متد وب سرویس را وارد نمایید ..."  TextMode="SingleLine" MaxLength="50" ></asp:TextBox>
               </div>   
                 <div class="form-group">
                    <label >پارامتر</label>
                        <asp:TextBox ID="txtWebServiceParameter" runat="server" style="text-align:left" CssClass="form-control" placeholder="پارامتر اختیاری متد وب سرویس را وارد نمایید ..."  TextMode="SingleLine" MaxLength="50" ></asp:TextBox>
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

                         <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title">تقاضا</h3>
                </div>
                <div class="box-body">
           
                <div class="form-group has-error">
                      <label >نحوه تقاضا</label>
                      <asp:DropDownList ID="cmbtheTypeDemand" CssClass="form-control select2" style="width:100%" runat="server" >
                        <asp:ListItem Value="1" Text="کلید ثابت"></asp:ListItem>
                        <asp:ListItem Value="2" Text="شروع با کلید ثابت" ></asp:ListItem>
                        <asp:ListItem Value="3" Text="کلید پویا"></asp:ListItem>
                      </asp:DropDownList>
                        
                      </div>
                        <div class="form-group has-error">
                      <label >کلید تقاضا</label>
                        <asp:TextBox ID="txtKeyDemand" runat="server" style="text-align:left" CssClass="form-control" placeholder="کلید تقضا را وارد نمایید"  TextMode="SingleLine" MaxLength="10" ></asp:TextBox>
                      </div>
                    
             

                </div>
              </div>
            </div>
        
          </div>




                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

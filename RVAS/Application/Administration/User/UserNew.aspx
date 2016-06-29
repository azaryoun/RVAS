<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="UserNew.aspx.vb" Inherits="RVAS.UserNew" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txtUsername=document.getElementById("<%=txtUsername.ClientID %>")
            var txtFName = document.getElementById("<%=txtFName.ClientID %>")
            var txtLName = document.getElementById("<%=txtLName.ClientID %>")
            var txtNationalID = document.getElementById("<%=txtNationalID.ClientID %>")
            var txtPassword = document.getElementById("<%=txtPassword.ClientID %>")
     

          
            if (trimall(txtUsername.value) == "") {
                alert("نام کاربری را وارد فرمایید");
                txtUsername.focus();
                return false;
            }

            if (!validateEmail(txtUsername.value)){
                alert("نام کاربری باید به صورت ایمیل باشد");
                txtUsername.focus();
                return false;
            }

            if (txtPassword.value == "") {
                alert("رمز عبور را وارد فرمایید");
                txtPassword.focus();
                return false;
            }


            if (trimall(txtFName.value) == "") {
                alert("نام را وارد فرمایید");
                txtFName.focus();
                return false;
            }


            if (trimall(txtLName.value) == "") {
                alert("نام خانوادگی را وارد فرمایید");
                txtLName.focus();
                return false;
            }


            if (trimall(txtNationalID.value) == "") {
                alert("شماره ملی را وارد فرمایید");
                txtNationalID.focus();
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
                  <h3 class="box-title">اطلاعات دسترسی</h3>
                </div>
                <div class="box-body">
                  <!-- Date range -->
                 

                  <!-- Date and time range -->
                
                       <div class="form-group">
                    <label>نوع دسترسی</label>
                    
                        <asp:DropDownList ID="cmbAccessType" CssClass="form-control select2" style="width:100%;direction:ltr" runat="server">
                    <asp:ListItem Value="1" Text="Normal" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Item" Selected="false"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Data" Selected="false"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Full" Selected="false"></asp:ListItem>
                           
                        </asp:DropDownList>
                     
                  </div><!-- /.form group -->


             
                        <div class="form-group">
                     <label>صفحات (اقلام) منو</label>
                      
                      <div  runat="server" id="divAccessgroup" style="height: 170px; overflow-y: scroll; border:1px dotted #D2D6DE;padding-right:3px">
                       
                      </div>
                      </div>

                </div><!-- /.box-body -->
              </div><!-- /.box -->

     
            </div><!-- /.col (right) -->
            <div class="col-md-6">

              <div class="box box-danger">
                <div class="box-header">
                  <h3 class="box-title">اطلاعات سیستمی</h3>
                </div>
                <div class="box-body">
                  <!-- Date dd/mm/yyyy -->
                  <div class="form-group has-error">





                      <label for="txtTitle">نام کاربری</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="نام کاربری را وارد نمایید"  TextMode="Email" MaxLength="40" ToolTip="نام کاربری باید به صورت ایمیل باشد" ></asp:TextBox>
                  <p class="help-block">نام کاربری باید به صورت آدرس ایمیل باشد</p>    
                  </div>

                       <div class="form-group has-error">
                      <label for="txtTitle">رمز عبور</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="رمز عبور را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>

                  
                     <div class="form-group">
                    <label>وضعیت کاربر</label>
                    <div class="input-group">
                        <asp:RadioButton ID="rdoActiveYes"  Text="فعال"  runat="server" Checked="true"  GroupName="Active" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoActiveNo" Text="غیر فعال"  runat="server" GroupName="Active" />
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->

                                     <div class="form-group">
                    <label>نوع مشتری</label>
                    <div class="input-group">
                        <asp:RadioButton ID="rdoIsRealYes"  Text="حقیقی"  runat="server" Checked="true"  GroupName="Real" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoIsRealNo" Text="حقوقی"  runat="server" GroupName="Real" />
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
     

                </div><!-- /.box-body -->
              </div><!-- /.box -->

         

            </div><!-- /.col (left) -->
        
          </div>




                            <div class="row">
                                <div class="col-md-12">
   
                                    <div class="box box-default">
              <div class="box-header">
                  <h3 class="box-title">اطلاعات پرسنلی</h3>
                </div>

        
            <div class="box-body">
              <div class="row">
                <div class="col-md-6">
                <div class="form-group">
                      <label >شماره شناسنامه</label>
                        <asp:TextBox ID="txtNationalCode" runat="server" CssClass="form-control" placeholder="شماره شناسنامه را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>

                       <div class="form-group">
                      <label >شماره تلفن</label>
                        <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control" placeholder="شماره تلفن را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                    
                        <div class="form-group">
                      <label >شماره موبایل</label>
                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="شماره موبایل را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>

                        <div class="form-group">
                      <label>نشانی</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="نشانی را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>


        
                     </div><!-- /.col -->
             
                  
                     <div class="col-md-6">
                <div class="form-group has-error">
                      <label for="txtTitle">نام</label>
                        <asp:TextBox ID="txtFName" runat="server" CssClass="form-control" placeholder="نام را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>

                       <div class="form-group has-error">
                      <label for="txtLName">نام خانوادگی</label>
                        <asp:TextBox ID="txtLName" runat="server" CssClass="form-control" placeholder="نام خانوادگی را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                    
                     
                 <div class="form-group">
                    <label>جنسیت</label>
                    <div class="input-group">
                        <asp:RadioButton ID="rdoSexMale"  Text="مرد"  runat="server" Checked="true"  GroupName="Sex" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoSexFemale" Text="زن"  runat="server" GroupName="Sex" />
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
      
                 <div class="form-group has-error">
                      <label for="txtTitle">کد ملی</label>
                        <asp:TextBox ID="txtNationalID" runat="server" CssClass="form-control" placeholder="کد ملی را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
        
                     </div><!-- /.col -->
             
              </div><!-- /.row -->
            </div><!-- /.box-body -->
            
          </div>




     
            </div><!-- /.col (right) -->
  
        
          </div>


                             <div class="row">
            <div class="col-xs-12">
              
                     <div class="box box-primary">
                <div class="box-header with-border">
                  <h3 class="box-title">اطلاعات تکمیلی</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
             
                  <div class="box-body">
                    <div class="form-group">
                      <label for="txtTitle">عکس کاربر</label>
                 
                        <asp:FileUpload ID="fleUserPhoto" runat="server" />
                        <p class="help-block">فرمت عکس باید JPG بوده و ابعاد آن بهتر است 120*120 باشد</p>
                             </div>
                   
                    
                  </div><!-- /.box-body -->
                          

             

                   
             


              </div><!-- /.box -->

                </div>
                </div>

                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

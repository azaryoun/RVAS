<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="UserProfile.aspx.vb" Inherits="RVAS.UserProfile" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

         
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
            <div class="col-xs-12">
              
                     <div class="box box-info">
                <div class="box-header with-border">
                  <h3 class="box-title"></h3>
                </div><!-- /.box-header -->
                <!-- form start -->
             
                  <div class="box-body">
                    <div class="form-group" style="text-align:center">
                      <label for="txtTitle"></label>
                                    <div>
             <asp:Image ID="imgUserPhoto" runat="server"  CssClass="img-circle" style="border:3px solid;border-color:#62A3C8" ImageUrl="~/Images/System/User.jpg" />
                    
            </div>        
                            </div>
                   
                                 <div class="form-group" style="text-align:center">
                      <label for="txtTitle"></label>
                                    <div >
 
                                        <asp:Label ID="lblFullName" runat="server" Text="" Font-Size="16px"></asp:Label>        
            </div>        
                            </div>
        
<div class="form-group has-success">
                      <label for="txtTitle">نام کاربری</label>
                        <asp:TextBox ID="txtUsername" ReadOnly="true" runat="server" CssClass="form-control"   MaxLength="40"></asp:TextBox>
                  </div>


                                   <div class="form-group has-success">
                      <label for="txtTitle">کد ملی</label>
                        <asp:TextBox ID="txtNationalID" runat="server" CssClass="form-control"  ReadOnly="true"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
     
                                   <div class="form-group has-success">
                      <label for="txtTitle">نوع دسترسی</label>
                        <asp:TextBox ID="txtAccessType" runat="server"  ReadOnly="true" CssClass="form-control"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
     


                  </div><!-- /.box-body -->
                          

             

                   
             


              </div><!-- /.box -->

                </div>
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
                      <label >شماره موبایل</label>
                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="شماره موبایل را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>

                        <div class="form-group">
                      <label>نشانی</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="نشانی را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>

          <div class="form-group">
                    <label>نوع مشتری</label>
                    <div class="input-group">
                        <asp:RadioButton ID="rdoIsRealYes"  Text="حقیقی"  runat="server" Checked="true"  GroupName="Real" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoIsRealNo" Text="حقوقی"  runat="server" GroupName="Real" />
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
                      
        
                     </div>  
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
                    <label>جنسیت</label>
                    <div class="input-group">
                        <asp:RadioButton ID="rdoSexMale"  Text="مرد"  runat="server" Checked="true"  GroupName="Sex" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoSexFemale" Text="زن"  runat="server" GroupName="Sex" />
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
        
                     </div>
         
             
              </div><!-- /.row -->
            </div><!-- /.box-body -->
            
          </div>




     
            </div><!-- /.col (right) -->
  
        
          </div>


                             <div class="row">
            <div class="col-xs-12">
              
                     <div class="box box-primary">
                <div class="box-header with-border">
                  <h3 class="box-title"></h3>
                </div><!-- /.box-header -->
                <!-- form start -->
             
                  <div class="box-body">
                    <div class="form-group">
                      <label for="txtTitle">عکس کاربر</label>
                                    <div class="pull-left image">
                    
            </div>        
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

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="CustomerRequestEdit.aspx.vb" Inherits="RVAS.CustomerRequestEdit" %>

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
              
                     <div class="box box-primary">
                <div class="box-header with-border">
                  <h3 class="box-title"></h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                 <div class="box-body">
                 <div class="form-group">
                    <label>نوع</label>
                    
                        <asp:DropDownList ID="cmbRequestCategory" CssClass="form-control select2" style="width:100%;direction:ltr" runat="server">
                    <asp:ListItem Value="1" Text="پیگیری" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="شکایت" Selected="false"></asp:ListItem>
                    <asp:ListItem Value="3" Text="انتقاد" Selected="false"></asp:ListItem>
                    <asp:ListItem Value="4" Text="پیشنهاد" Selected="false"></asp:ListItem>
                           
                        </asp:DropDownList>
                     
                  </div><!-- /.form group -->
                
                    <div class="form-group">
                      <label>ملاحظات</label>
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="شرح درخواست را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                   
                      <div class="form-group">
                     <label>شماره تماس</label>
                      
                      <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="شماره تماس را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                    
                  </div><!-- /.box-body -->
                          


              </div><!-- /.box -->

                  <div class="box box-primary">
                <div class="box-header with-border">
                  <h3 class="box-title">پاسخ ها</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                 <div class="box-body">
                                
                    <div class="form-group">
                      <label>ملاحظات</label>
                        <asp:TextBox ID="txtResponseRemark" runat="server" CssClass="form-control"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                   
                                         
                  </div><!-- /.box-body -->
                          
   
              </div>
                
                </div>
                </div>
                       
                        
                                </ContentTemplate>

               
                    </asp:UpdatePanel>

         
        </section>

</asp:Content>

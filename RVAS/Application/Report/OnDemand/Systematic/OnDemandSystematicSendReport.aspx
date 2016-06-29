<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="OnDemandSystematicSendReport.aspx.vb" Inherits="RVAS.OnDemandSystematicSendReport" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>
<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_PersianDateTimePicker.ascx" TagPrefix="uc1" TagName="Bootstrap_PersianDateTimePicker" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function DisplayOperation_Validate() {

   
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


                                             <div class="box box-danger">
                <div class="box-header">
                  <h3 class="box-title">تنظیمات</h3>
                </div>
                <div class="box-body">
        
                 
               <div class="form-group has-error">
                      <label  >سرویس مرتبط</label>
             <asp:DropDownList ID="cmbVASServices" CssClass="form-control select2" style="width:100%;direction:rtl" runat="server">
                        </asp:DropDownList>       
               </div>

                 
            
              </div>
                  </div>
     


              </div>

     
           
            <div class="col-md-6">


                          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">بازه زمانی</h3>
                </div>
                <div class="box-body">
                
                
                       <div class="form-group">
                    <label>از تاریخ و ساعت</label>
                           <uc1:Bootstrap_PersianDateTimePicker runat="server" ID="Bootstrap_PersianDateTimePicker_From" ShowTimePicker="true" />
                     
                  </div>


             
                       <div class="form-group">
                    <label>تا تاریخ و ساعت</label>
                           <uc1:Bootstrap_PersianDateTimePicker runat="server" ID="Bootstrap_PersianDateTimePicker_To" ShowTimePicker="true" />
                     
                  </div>
                          

                   

                  </div>
                      

                </div>

              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">فرمت خروجی</h3>
                </div>
                <div class="box-body">
        
                 
            

   
                 
                  
                        <div class="form-group has-feedback">
                    <label>نحوه نمایش</label>
                        <div class="input-group">
                        <asp:RadioButton ID="rdoShowSummaryYes"  Text="خلاصه"  runat="server" Checked="true"  GroupName="ShowType" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoShowSummaryNo" Text="جزئیات"  runat="server" GroupName="ShowType" />
                    </div><!-- /.input group -->

                  </div>

    <div class="form-group">
                    <label>نوع مرتب سازی</label>
                        <div class="input-group">
                        <asp:RadioButton ID="rdoIsAscendingSortYes"  Text="صعودی"  runat="server" Checked="true"  GroupName="Real" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoIsAscendingSortNo" Text="نزولی"  runat="server" GroupName="Real" />
                    </div><!-- /.input group -->
                </div><!-- /.box-body -->
              </div><!-- /.box -->
                  </div>
         

            </div>
        
          </div>




                            <div class="row">
                                <div class="col-md-12">
   
                                    <div class="box box-default">
              <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>

        
            <div class="box-body">
              <div class="row">
                <div class="col-md-12">
      
        

      <table id="tblReport_Detail" runat="server" class="table table-bordered table-striped table-hover" role="grid" >
                    <thead>
                      <tr role="row">
                         
                       <th  style="width:5px;" >#</th>
                          <th>تاریخ</th>
                       <th>سرویس<br />سرشماره</th>
                              <th>مشتری</th>
                          <th>پیام</th>
                       
                      </tr>
                    </thead>
                    <tbody>
                      
                      
                    
                    
                                    
                       </tbody>
                    <tfoot>
                      <tr>
                          
                          
                       <th  style="width:5px;" >#</th>
                           <th>تاریخ</th>
                       <th>سرویس<br />سرشماره</th>
                              <th>مشتری</th>
                          <th>پیام</th>
                            
               
                      </tr>
                           </tfoot>

                  </table>



  <table id="tblReport_Summary" runat="server" class="table table-bordered table-striped table-hover" role="grid" >
                    <thead>
                      <tr role="row">
                         
                       <th  style="width:5px;" >#</th>
                       <th>روز</th>
                              <th>سرویس</th>
                         <th>تعداد</th>
                      
                       
                      </tr>
                    </thead>
                    <tbody>
                      
                      
                    
                    
                                    
                       </tbody>
                    <tfoot>
                     <tr role="row">
                         
                       <th  style="width:5px;" colspan="3" >مجموع</th>
                       <th>
                           <asp:Label ID="lblSummarySum" runat="server" Text="0.0"></asp:Label></th>
                          
                      </tr>
                           </tfoot>

                  </table>











                     </div><!-- /.col -->
             
                  
      
             
              </div><!-- /.row -->
            </div><!-- /.box-body -->
            
          </div>




     
            </div><!-- /.col (right) -->
  
        
          </div>


                       

                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

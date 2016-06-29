<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="WebForm1.aspx.vb" Inherits="RVAS.WebForm1" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
  
    
        <section class="content">
        <div class="row">
            <div class="col-xs-12">
                  <uc1:Bootstrap_Panel runat="server" id="Bootstrap_Panel" />
            </div>
        </div>
          <div class="row">
            <div class="col-xs-12">
     

              <div class="box">
          
                <div class="box-body">
                  <div id="example1_wrapper" class="form-inline dt-bootstrap">
                      <div class="row"><div class="col-sm-6"></div>
                          <div class="col-sm-6"><div>
                          <label>جستجو: 
                              <asp:TextBox ID="txtTableSearch" runat="server" ToolTip="متن مورد نظر جهت جستجو" CssClass="form-control input-sm"></asp:TextBox>
                              
                              </label>
                          
                           <asp:LinkButton CssClass="btn btn-info btn-flat" ID="btnSearch_New" runat="server" ToolTip="جستجوی کلید واژه مورد نظر" >  <i class="fa fa-search"></i></asp:LinkButton>
   
                          
                                                                                                                                                                                                                                                                                                                               </div></div></div><div class="row"><div class="col-sm-12">
                      <table id="tblManagement" runat="server" class="table table-bordered table-striped table-hover" role="grid" >
                    <thead>
                      <tr role="row">
                          <th  style="width: 228px;" aria-sort="ascending">تجمیع کننده</th>
                          <th    style="width: 281px;">پیش شماره پردیس</th>
                          <th  style="width: 249px;">پیش شماره IMI</th>
                      
                      </tr>
                    </thead>
                    <tbody>
                      
                      
                    <tr role="row" class="odd">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Mozilla 1.3</td>
                        <td>Win 95+ / OSX.1+</td>
                      </tr><tr role="row" class="even">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Mozilla 1.4</td>
                        <td>Win 95+ / OSX.1+</td>
                      </tr><tr role="row" class="odd">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Mozilla 1.5</td>
                        <td>Win 95+ / OSX.1+</td>
                      </tr><tr role="row" class="even">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Mozilla 1.6</td>
                        <td>Win 95+ / OSX.1+</td>
                      </tr><tr role="row" class="odd">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Mozilla 1.7</td>
                        <td class="">A</td>
                      </tr><tr role="row" class="even">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Mozilla 1.8</td>
                        <td class="">A</td>
                      </tr><tr role="row" class="odd">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Netscape 7.2</td>
                        <td class="">A</td>
                      </tr><tr role="row" class="even">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Netscape Browser 8</td>
                        <td>Win 98SE+</td>
                      </tr><tr role="row" class="odd">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Netscape Navigator 9</td>
                        <td>Win 98+ / OSX.2+</td>
                      </tr><tr role="row" class="even">
                        <td class="sorting_1">Gecko</td>
                        <td class="">Seamonkey 1.1</td>
                        <td>Win 98+ / OSX.2+</td>
                      </tr></tbody>
                    <tfoot>
                      <tr>
                          <th rowspan="1" colspan="1">تجمیع کننده</th>
                          <th rowspan="1" colspan="1">پیش شماره پردیس</th>
                          <th rowspan="1" colspan="1">پیش شماره IMI</th>
                      </tr>
                           </tfoot>

                  </table>

                                                                                                                                                                                                                                                                                                                                                                  </div>

                                                                                                                                                                                                                                                                                                                                                 </div>
                      <div class="row"><div class="col-sm-5">
                          <div id="example1_info" role="status" aria-live="polite">

                      <asp:Label ID="lblTableFooterDesc" runat="server" Text="نمایش ردیف های 11 تا 20 از 15006 رکورد"></asp:Label></div></div>
                      <div class="col-sm-7">
                      <div class="input-group">
   <asp:LinkButton CssClass="btn btn-default" ID="btnLastPage" runat="server" style="float:right" ToolTip="صفحه آخر" >  <i class="fa fa-fast-forward"></i></asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-default" ID="btnNextPage" runat="server" style="float:right" ToolTip="صفحه بعد" >  <i class="fa fa-forward"></i> </asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-success btn-flat" ID="btnGoPage" runat="server" style="float:right" ToolTip="برو به صفحه" > <i class="fa fa-hand-o-up"  ></i> </asp:LinkButton>
     
                          <asp:TextBox ID="txtTablePageNo" runat="server" CssClass="form-control input-sm" MaxLength="8" ToolTip="شماره صفحه" style="width:100px;text-align:center;float:right" Text="1"></asp:TextBox>       
       <asp:LinkButton CssClass="btn btn-default" ID="btnPreviousPage" runat="server" style="float:right" ToolTip="صفحه قبل" >  <i class="fa fa-backward"></i></asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-default" ID="btnFirstPage" runat="server" style="float:right" ToolTip="صفحه اول" >  <i class="fa fa-fast-backward"></i> </asp:LinkButton>
  
                          



   
    </div>
   </div></div>

                  </div>
                </div><!-- /.box-body -->
              </div><!-- /.box -->





            </div><!-- /.col -->
          </div>
</section>


    

</asp:Content>

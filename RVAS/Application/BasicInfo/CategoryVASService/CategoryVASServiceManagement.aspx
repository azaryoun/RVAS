<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="CategoryVASServiceManagement.aspx.vb" Inherits="RVAS.CategoryVASServiceManagement" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

    function btnEdit_ClientClick(Pkey) {
        var hdnAction_Name = "<%=hdnAction.ClientID%>";
        var hdnAction = document.getElementById(hdnAction_Name);
        hdnAction.value = "E;" + Pkey;
        window.document.forms[0].submit();
        return false;
    }


        function DeleteOperation_Validate() {

            var tblManagement = document.getElementById("<%=tblManagement.ClientID%>");
            var hdnAction = document.getElementById("<%=hdnAction.ClientID%>");
     
      
            hdnAction.value="";
            
            for (i = 1; i < tblManagement.rows.length - 1; i++) {
                if (tblManagement.rows[i].cells[0].firstChild.checked) {
                    hdnAction.value += tblManagement.rows[i].cells[0].firstChild.id + ";"

                }
            }


            if ( hdnAction.value=="") {
                alert("رکوردی انتخاب نشده است");
                return false;
            }

            if (!confirm("حذف شدن رکورد(های) انتخاب شده را تایید نمایید"))
                return false;


             return true;


        }


        function chkSelectAll_Click() {
           
            var tblManagement = document.getElementById("<%=tblManagement.ClientID%>");
            var chkSelectAll = document.getElementById("chkSelectAll");
            var i;
          
         
            for (i = 1; i < tblManagement.rows.length - 1; i++) {
                
                tblManagement.rows[i].cells[0].firstChild.checked = chkSelectAll.checked;
         
           
            }
            return true;
        }

    </script>

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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
    <div class="box">
          
                <div class="box-body">
                  <div id="example1_wrapper" class="form-inline dt-bootstrap">
                      <div class="row"><div class="col-sm-6"></div>
                          <div class="col-sm-6"><div>
                          <label>جستجو: 
                              <asp:TextBox ID="txtTableSearch" runat="server" ToolTip="متن مورد نظر جهت جستجو" CssClass="form-control input-sm" placeholder="(کلید واژه)"></asp:TextBox>
                              
                              </label>
                          
                           <asp:LinkButton CssClass="btn btn-info btn-flat" ID="btnSearch_New" runat="server" ToolTip="جستجوی کلید واژه مورد نظر" >  <i class="fa fa-search"></i></asp:LinkButton>
   
                          
                                                                                                                                                                                                                                                                                                                               </div></div></div><div class="row"><div class="col-sm-12">
                      <table id="tblManagement" runat="server" class="table table-bordered table-striped table-hover" role="grid" >
                    <thead>
                      <tr role="row">
                          <th style="width: 5px;">

                              <input type="checkbox" value="" id="chkSelectAll" onclick="return chkSelectAll_Click();" title="انتخاب یا عدم انتخاب تمام ردیف ها" />

                           

                          </th>
                            <th  style="width:5px;" >#</th>
                          <th  style="width: 228px;" >عنوان</th>
                    
                      
                      </tr>
                    </thead>
                    <tbody>
                      
                      
                    
                    
                                    
                       </tbody>
                    <tfoot>
                      <tr>
                          <th></th>
                          <th  style="width:5px;" >#</th>
                          <th rowspan="1" colspan="1">عنوان</th>
                     
                      </tr>
                           </tfoot>

                  </table>

                                                                                                                                                                                                                                                                                                                                                                  </div>

                                                                                                                                                                                                                                                                                                                                                 </div>
                      <div class="row">
                          <div class="col-sm-5">
                          <div id="example1_info" role="status" aria-live="polite">
                              نمایش ردیف های  
                                 <asp:Label ID="lblTableRowFrom" runat="server" Text="1"></asp:Label>
                              تا    
                              <asp:Label ID="lblTableRowTo" runat="server" Text="1"></asp:Label>
                               از   
                                <asp:Label ID="lblTableRecordCount" runat="server" Text="1"></asp:Label>
                              رکورد

                          </div>
                              
                          </div>
                               
                           <div class="col-sm-7">
                      <div class="input-group">
   <asp:LinkButton CssClass="btn btn-default" ID="btnLastPage" runat="server" style="float:right" ToolTip="صفحه آخر" >  <i class="fa fa-fast-forward"></i></asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-default" ID="btnNextPage" runat="server" style="float:right" ToolTip="صفحه بعد" >  <i class="fa fa-forward"></i> </asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-success btn-flat" ID="btnGoPage" runat="server" style="float:right" ToolTip="برو به صفحه" > <i class="fa fa-hand-o-up"  ></i> </asp:LinkButton>
     <input type="number" id="txtTablePageNo" runat="server" class="form-control input-sm" title="شماره صفحه" style="width:100px;text-align:center;float:right" value="1" maxlength="8" />

                      
       <asp:LinkButton CssClass="btn btn-default" ID="btnPreviousPage" runat="server" style="float:right" ToolTip="صفحه قبل" >  <i class="fa fa-backward"></i></asp:LinkButton>
   <asp:LinkButton CssClass="btn btn-default" ID="btnFirstPage" runat="server" style="float:right" ToolTip="صفحه اول" >  <i class="fa fa-fast-backward"></i> </asp:LinkButton>
  
                          



   
    </div>
   </div></div>

                  </div>
                </div><!-- /.box-body -->
              </div>
  <asp:HiddenField ID="hdnAction" runat="server" />
                    </ContentTemplate>

                </asp:UpdatePanel>

          





            </div><!-- /.col -->
          </div>
</section>


    

</asp:Content>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="AccessgroupNew.aspx.vb" Inherits="RVAS.AccessgroupNew" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txtTitle = document.getElementById("<%=txtTitle.ClientID%>");
         
            if (trimall(txtTitle.value) == "") {
                alert("عنوان گروه دسترسی را وارد نمایید");
                txtTitle.focus();
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
            <div class="col-xs-12">
              
                     <div class="box box-primary">
                <div class="box-header with-border">
                  <h3 class="box-title"></h3>
                </div><!-- /.box-header -->
                <!-- form start -->
             
                  <div class="box-body">
                    <div class="form-group">
                      <label for="txtTitle">عنوان گروه دسترسی</label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="عنوان نوع محتوا را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                   
                      <div class="form-group">
                     <label>صفحات (اقلام) منو</label>
                      
                      <div  runat="server" id="divchklstMenuItems" style="max-height: 200px; overflow-y: scroll; border:1px dotted #D2D6DE;padding-right:3px">
                      
                      
                      </div>
                      </div>
                    
                  </div><!-- /.box-body -->
                          

             

                   
             


              </div><!-- /.box -->

                </div>
                </div>

                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

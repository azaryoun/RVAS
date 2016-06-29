<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="AggregatorsEdit.aspx.vb" Inherits="RVAS.AggregatorsEdit" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txtTitle = document.getElementById("<%=txtTitle.ClientID%>");
            var txtPreIMI = document.getElementById("<%=txtPreIMI.ClientID%>");
            var txtPrePardis = document.getElementById("<%=txtPrePardis.ClientID%>");
          
            if (trimall(txtTitle.value) == "") {
                alert("نام تجمیع کننده را وارد نمایید");
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
                      <label for="txtTitle">نام تجمیع کننده</label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="نام تجمیع کننده را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                    <div class="form-group">
                      <label for="exampleInputPassword1">پیش شماره پردیس</label>
                 <asp:TextBox ID="txtPrePardis" style="text-align:left" runat="server" CssClass="form-control" placeholder="پیش شماره پردیس را وارد نمایید"  TextMode="Number" MaxLength="6" ></asp:TextBox>
              
                    </div>
                   <div class="form-group">
                      <label for="exampleInputPassword1">پیش شماره IMI</label>
                 <asp:TextBox ID="txtPreIMI" style="text-align:left" runat="server" CssClass="form-control" placeholder="پیش شماره پردیس را وارد نمایید"  TextMode="Number" MaxLength="6"></asp:TextBox>
              
                    </div>
             <div class="form-group">
                      <label for="txtTitle">ملاحظات</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" placeholder=""  TextMode="MultiLine" MaxLength="250" Height="75px" ></asp:TextBox>
                      </div>
               
                    
                  </div><!-- /.box-body -->

               
              </div><!-- /.box -->

                </div>
                </div>

                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

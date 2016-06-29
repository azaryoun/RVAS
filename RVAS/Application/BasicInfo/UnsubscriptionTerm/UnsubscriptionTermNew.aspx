﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="UnsubscriptionTermNew.aspx.vb" Inherits="RVAS.UnsubscriptionTermNew" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txttheTerm = document.getElementById("<%=txttheTerm.ClientID%>");
         
            if (trimall(txttheTerm.value) == "") {
                alert("عبارت لغو اشتراک را وارد نمایید");
                txttheTerm.focus();
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
                    <div class="form-group has-error">
                      <label for="txtTitle">عبارت لغو اشتراک</label>
                        <asp:TextBox ID="txttheTerm" runat="server" CssClass="form-control" placeholder="عبارت مورد نظر را وارد نمایید"  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                      </div>
                   
                    
                  </div><!-- /.box-body -->

               
              </div><!-- /.box -->

                </div>
                </div>

                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

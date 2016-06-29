<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="ChangePasswordEdit.aspx.vb" Inherits="RVAS.ChangePasswordEdit" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {
            var txtPasswordNew=document.getElementById("<%=txtPasswordNew.ClientID%>")
         
            if (trimall(txtPasswordNew.value)=="")
            {
                alert("رمز جدید وارد نشده است");
                txtPasswordNew.focus();
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
                </div>
           
              <div class="box-body">
                 <div class="form-group has-success">
                      <label for="txtTitle">نام کاربری</label>
                        <asp:TextBox ID="txtUsername" ReadOnly="true" runat="server" CssClass="form-control"   MaxLength="40"></asp:TextBox>
                  </div>


                    <div class="form-group has-error">
                      <label >رمز کنونی</label>
                        <asp:TextBox ID="txtPasswordCurrent" runat="server" CssClass="form-control" placeholder="رمز فعلی خود را وارد نمایید"  TextMode="Password" MaxLength="40" ></asp:TextBox>
                      </div>
                    <div class="form-group has-error">
                      <label for="exampleInputPassword1">رمز جدید</label>
                 <asp:TextBox ID="txtPasswordNew"  runat="server" CssClass="form-control" placeholder="رمز جدید را وارد نمایید"  TextMode="Password" MaxLength="40" ></asp:TextBox>
              
                    </div>
                   <div class="form-group has-error">
                      <label >ورود مجدد رمز جدید</label>
                 <asp:TextBox ID="txtPasswordNewRetype"  runat="server" CssClass="form-control" placeholder="رمز جدید را مجددا وارد نمایید"  TextMode="Password" MaxLength="40"></asp:TextBox>
              
                    </div>
               
                    
                  </div>

               
              

                </div>
                </div>
                </div>
                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

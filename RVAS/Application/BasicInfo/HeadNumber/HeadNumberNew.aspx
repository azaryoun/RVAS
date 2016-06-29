<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="HeadNumberNew.aspx.vb" Inherits="RVAS.HeadNumberNew" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function rdoPardisNo_onchange() {
            var rdoPardisYes = document.getElementById("<%=rdoPardisYes.ClientID%>");
            var txtPardisTarif = document.getElementById("<%=txtPardisTarif.ClientID%>");
            if (rdoPardisYes.checked) {
                txtPardisTarif.disabled = false;
            }
            else
            {
                txtPardisTarif.disabled = true;
                txtPardisTarif.value = "";
            }

        }


        function SaveOperation_Validate() {

            var txttheNumber = document.getElementById("<%=txttheNumber.ClientID%>");
            var cmbAggregator = document.getElementById("<%=cmbAggregator.ClientID%>");
            var txtPardisTarif = document.getElementById("<%=txtPardisTarif.ClientID%>");
            var rdoPardisYes = document.getElementById("<%=rdoPardisYes.ClientID%>");
          
       

            if (cmbAggregator.selectedIndex == 0) {
                alert("تجمیع کننده را انتخاب نمایید");
                cmbAggregator.focus();
                return false;
            }

          

            if (trimall(txttheNumber.value) == "") {
                alert("سرشماره را وارد نمایید");
                txttheNumber.focus();
                return false;
            }

            txtPardisTarif.value = txtPardisTarif.value.replace(",", "");

            if (rdoPardisYes.checked)
                if (trimall(txtPardisTarif.value) == "" || isNaN(txtPardisTarif.value)) {
                    alert("مبلغ تعرفه پردیس نامعتبر است");
                    txtPardisTarif.focus();
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
              
                     <div class="box box-success">
                <div class="box-header with-border">
                  <h3 class="box-title"></h3>
                </div>
            
             
                  <div class="box-body">
              
                   
                    <div class="form-group has-error">
                      <label>تجمیع کننده</label>
                  <asp:DropDownList ID="cmbAggregator" CssClass="form-control select2" style="width:100%" runat="server" AutoPostBack="True">
               
                           
                        </asp:DropDownList>

                    </div>
                   <div class="form-group">
                      <label for="exampleInputPassword1">نوع سرشماره</label>
                 <div class="input-group">
                        <asp:RadioButton ID="rdoPardisYes"  Text="پردیس"  runat="server" Checked="true"  GroupName="Pardis" AutoPostBack="True" /> &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoPardisNo" Text="IMI"  runat="server" GroupName="Pardis" AutoPostBack="True" />
                    </div>

                    </div>

                  

                        <div class="form-group has-error">
                           <label >سرشماره</label>
                                <div class="input-group">
                    
                      <asp:TextBox ID="txttheNumber" runat="server" style="text-align:left" CssClass="form-control" placeholder=""  TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
                  <span class="input-group-addon" id="spnPreNumber" runat="server">#</span>
                                </div>

                     
                      </div>

             <div class="form-group">
                      <label for="txtTitle">تعرفه پردیس (ریال)</label>
                           <asp:TextBox ID="txtPardisTarif" runat="server" style="text-align:left" CssClass="form-control numericAmount" placeholder="تعرفه پردیس به ریال را وارد فرمایید"  TextMode="SingleLine" MaxLength="20" ></asp:TextBox>
                    </div>
               
                    
         

               
              </div>

                </div>
            </div>

                </div>

                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

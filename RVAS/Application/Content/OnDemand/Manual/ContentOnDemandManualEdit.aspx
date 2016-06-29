<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="ContentOnDemandManualEdit.aspx.vb" Inherits="RVAS.ContentOnDemandManualEdit" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Panel.ascx" TagPrefix="uc1" TagName="Bootstrap_Panel" %>
<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_PersianDateTimePicker.ascx" TagPrefix="uc1" TagName="Bootstrap_PersianDateTimePicker" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function SaveOperation_Validate() {

            var txttheText = document.getElementById("<%=txttheText.ClientID %>")
          
          
            if (trimall(txttheText.value) == "") {
                alert("متن سرویس وارد نشده است");
                txttheText.focus();
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
            <div class="col-md-12">
              <div class="box box-danger">
                <div class="box-header">
                  <h3 class="box-title">سرویس</h3>
                </div>
                <div class="box-body">
           

              <div class="form-group has-feedback">
                      <label>سرویس مرتبط</label>
              <asp:TextBox ID="txtVASServiceOnDemandManual" runat="server" ReadOnly="true" CssClass="form-control"   TextMode="SingleLine" MaxLength="40" ></asp:TextBox>
             
               </div>
     
              <div class="form-group has-error">
                      <label>متن سرویس</label>
                        <asp:TextBox ID="txttheText" runat="server" CssClass="form-control" placeholder="متن مورد نظر را وارد نمایید ..."  TextMode="MultiLine" MaxLength="500" Height="75px" ></asp:TextBox>
               </div>


                </div>
              </div>

        <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">اعتبار</h3>
                </div>
                <div class="box-body">

<div class="form-group">
    <label  title="Footer Text">تاریخ و ساعت شروع اعتبار</label>
<uc1:Bootstrap_PersianDateTimePicker runat="server" ID="Bootstrap_PersianDateTimePicker_ValidFrom"   ShowTimePicker="True" />

    </div>
     
<div class="form-group">
    <label  title="Footer Text">تاریخ و ساعت پایان اعتبار</label>
<uc1:Bootstrap_PersianDateTimePicker runat="server" ID="Bootstrap_PersianDateTimePicker1_ValidTo"   ShowTimePicker="True" />

    </div>   
   
 <div class="form-group">
<label>وضعیت محتوا</label>
<div class="input-group">
    <asp:RadioButton ID="rdoActiveYes"  Text="فعال"  runat="server" Checked="true"  GroupName="Active" /> &nbsp;&nbsp;&nbsp;
    <asp:RadioButton ID="rdoActiveNo" Text="غیر فعال"  runat="server" GroupName="Active" />
</div><!-- /.input group -->
</div>

             

                </div>
              </div>

      
                


            </div>
            


        
          </div>




                        
                                </ContentTemplate>
                    </asp:UpdatePanel>


         
        </section>

</asp:Content>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Bootstrap_PersianDateTimePicker.ascx.vb" Inherits="RVAS.Bootstrap_PersianDateTimePicker" %>

  
        <div class="form-group">
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
            <div class="input-group">
                <div class="input-group-addon" style="font-family:@Arial Unicode MS" data-MdDateTimePicker="true" data-targetselector="#<%=txtDateTime.clientID%>" data-trigger="click" data-enabletimepicker="<%=LCase(_ShowTimePicker)%>">
                    <span class="glyphicon glyphicon-calendar"></span>
                </div>
                <asp:TextBox ID="txtDateTime" CssClass="form-control" runat="server"></asp:TextBox>

               
            </div>


     

  

    </div>
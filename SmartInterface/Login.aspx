<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SmartInterface.Login" %>

<%@ Register Src="~/UserControls/Bootstrap/Bootstrap_Callout.ascx" TagPrefix="uc1" TagName="Bootstrap_Callout" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Rahyab VAS Admin Panel | Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="/CSR/bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="/CSR/dist/css/AdminLTE.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="/CSR/plugins/iCheck/square/blue.css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
  </head>
  <body class="hold-transition login-page">
          <form id="form1" runat="server">
              
    <div class="login-box">
      <div class="login-logo">
        <a href="#"> رهیاب <b>VAS</b></a>
      </div><!-- /.login-logo -->
      <div class="login-box-body">
        <p class="login-box-msg">برای دسترسی به امکانات سامانه، لطفا وارد سیستم شوید:</p>
       
          <div class="form-group has-feedback">

<asp:TextBox ID="txtUsername" runat="server" style="font-family:'Source Sans Pro', 'Helvetica Neue', Helvetica, Arial, sans-serif;direction:ltr" CssClass="form-control"  placeholder="Email" TextMode="Email" required="required" ToolTip="آدرس ایمیل خود را وارد نمایید"></asp:TextBox>

            <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
          </div>
          <div class="form-group has-feedback">
              <asp:TextBox ID="txtPassword" runat="server"  style="font-family:'Source Sans Pro', 'Helvetica Neue', Helvetica, Arial, sans-serif;direction:ltr"  CssClass="form-control"  placeholder="Password" TextMode="Password"  required="required" ToolTip="گذر واژه خود را وارد نمایید"></asp:TextBox>

            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
          </div>
          <div class="row">
            <div class="col-xs-8">
              <div class="checkbox icheck" style="float:left">
                <label>
<asp:CheckBox ID="chkRememberMe" runat="server" />
               مرا بخاطر بسپار
                </label>
              </div>
            </div><!-- /.col -->
            <div class="col-xs-4">
<asp:Button ID="btnSignIn" runat="server" Text="ورود به سیستم" CssClass="btn btn-primary btn-block btn-flat" OnClick="btnSignIn_Click" />
              </div><!-- /.col -->
          </div>
      
   
        <a href="#">گذر واژه خود را فراموش کرده ام</a><br/>
       
          
          <uc1:Bootstrap_Callout runat="server" id="Bootstrap_Callout" />


      </div><!-- /.login-box-body -->
    </div><!-- /.login-box -->
              </form>


    <!-- jQuery 2.1.4 -->
    <script src="/CSR/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.5 -->
    <script src="/CSR/bootstrap/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="/CSR/plugins/iCheck/icheck.min.js"></script>
    <script>
      $(function () {
        $('input').iCheck({
          checkboxClass: 'icheckbox_square-blue',
          radioClass: 'iradio_square-blue',
          increaseArea: '20%' // optional
        });
      });
    </script>
  </body>
</html>


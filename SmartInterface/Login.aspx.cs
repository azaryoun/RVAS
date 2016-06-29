using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject.Context;
using Service.Security;
using System.Web.Security;

namespace SmartInterface
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
            Bootstrap_Callout.Display = false;
            if (Page.IsPostBack==false)
            {
                this.Title = "پنل مدیریت ارزش افزوده شرکت رهیاب رایانه گستر";
            }

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string strUsername = txtUsername.Text.Trim();
            string strPassword = txtPassword.Text;
            strPassword = EncDec.Encrypt(strPassword);
            dbVASEntities OdbVAS = new dbVASEntities();

            var lUserLogin = OdbVAS.tbl_User.Where(l => l.Username == strUsername && l.Password == strPassword && l.IsActive == true).Select(l => new { l.FName, l.LName, l.Username, l.IsDataAdmin, l.IsItemAdmin, l.IsMale, l.Mobile, l.UserPhoto, l.IsReal }).FirstOrDefault();

            if (lUserLogin == null)
            {
                Bootstrap_Callout.Display = true;
                Bootstrap_Callout.ShowWarning = true;
                Bootstrap_Callout.Message = "نام کاربری یا گذرواژه نامعتبر است";
            }
            else
            {
                Service.Security.stcUserInfo sobjUserInfo;
                sobjUserInfo.FName = lUserLogin.FName;
                sobjUserInfo.LName = lUserLogin.LName;
                sobjUserInfo.Username = lUserLogin.Username;
                sobjUserInfo.UserPhoto  = lUserLogin.UserPhoto;
                sobjUserInfo.Mobile = lUserLogin.Mobile;
                sobjUserInfo.IsItemAdmin = lUserLogin.IsItemAdmin;
                sobjUserInfo.IsDataAdmin = lUserLogin.IsDataAdmin ;
                sobjUserInfo.IsMale = lUserLogin.IsMale;
                sobjUserInfo.IsReal = lUserLogin.IsReal;
               
                Session["stcUserInfo"] = sobjUserInfo;

                FormsAuthentication.SetAuthCookie("VAS_Username", false);
                Response.Redirect("~/Application/WebForm1.aspx");
            }


        }
    }
}
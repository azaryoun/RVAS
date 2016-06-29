using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartInterface.Application.MasterPage
{
    public partial class Smart : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (Page.IsPostBack==false)
            {
                lblApplicationTitle_Big.Text = @"رهیاب  <b>VAS</b>";
                lblApplicationTitle_Mini.Text = "VAS";
                lblApplication_Version.Text = "1.0.0";
                lblUserInfo_Menu_Big.Text = "کیوان آذریون - رهیاب";
                lblUserInfo_Menu_Small.Text = "عضو سیستم از دیماه 94";
                lblPageMainTitle.Text = "کاربران";
                lblPageSmallTitle.Text = "مدیریت";
                lblUserFNameLName.Text = "کیوان آذریون";
                //imgUserImageSmallRight.ImageUrl = "";
                //imgUserLeftBig.ImageUrl = "";
                //imgUserLeftSmall.ImageUrl = "";

                lblUserFNameLNameLeft.Text = "کیوان آذریون";
                title.Text = "پنل مدیریت ارزش افزوده رهیاب";

                ltrthePath.Text = "<li><a href =\"#\"><i class=\"fa fa-dashboard\"></i>اطلاعات پایه</a></li><li class=\"active\">انواع محتوا</li>";
                lblNewSubsription.Text = "15000";
                lblNewUnsubsription.Text = "250";
            }
           

            //if (Session("dtblUserLogin") =null) 
            //     Call lbtnSignOut_Click(sender, e)
            //     Return
            // End If

            Bootstrap_Menu.BuildMenu(true, 0, 0);

        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session["stcUserInfo"] = null;
            Session.Abandon();
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
        }
    }
}
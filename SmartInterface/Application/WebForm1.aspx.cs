using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartInterface.Application
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnPanel_Display.Visible = true;
            btnPanel_Excel.Visible = true;
            btnPanel_PDF.Visible = true;

            Bootstrap_Callout.Display = false;
            Bootstrap_Callout.ShowWarning = true;
            Bootstrap_Callout.Message = "نام کاربری یا گذرواژه نامعتبر است";


        }

        protected void btnPanel_Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
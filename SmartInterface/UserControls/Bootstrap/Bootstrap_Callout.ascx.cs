using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartInterface.UserControls.Bootstrap
{
    public partial class Bootstrap_Callout : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool Display {
            get
            {
                if (divCallout.Style["display"] == "")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    divCallout.Style["display"] = "";
                else
                    divCallout.Style["display"] = "none";
            }

        }

        public string Message {
            get {
                return lblMessage.Text;
            }
            set {
                lblMessage.Text = value;
            }
         }

        public bool ShowWarning {
            get
            {
                if (iconClass.Attributes["class"] == "icon fa fa-ban")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    iconClass.Attributes["class"] = "icon fa fa-ban";
                    divCallout.Attributes["class"] = "callout callout-danger";
                    lblTitle.Text = "خطا";
                }
                else
                {
                    iconClass.Attributes["class"] = "icon fa fa-info";
                    divCallout.Attributes["class"] = "callout callout-info";
                    lblTitle.Text = "توجه";
                }
            }
        }

    }
}
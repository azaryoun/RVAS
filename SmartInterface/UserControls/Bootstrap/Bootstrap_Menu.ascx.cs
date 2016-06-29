using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject.Administration;
using BusinessObject.Administration.dstMenuTableAdapters;
using System.Runtime.InteropServices;

namespace SmartInterface.UserControls.Bootstrap
{
    public partial class Bootstrap_Menu : System.Web.UI.UserControl
    {
        public string strMenuText = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BuildMenu(bool blnForItemAdmin, int intUserID = 0, int intActiveMenuID = 0)
        {
            strMenuText = "<ul class=\"sidebar-menu\">";
            strMenuText += "<li Class=\"header\">منوی اصلی</li>";
            int intAction = 2;
            if (blnForItemAdmin == true)
            {
                intAction = 1;
            }

            BusinessObject.Administration.dstMenuTableAdapters.spr_Menu_Parent_SelectTableAdapter tadpParentMenu = new spr_Menu_Parent_SelectTableAdapter();
            dstMenu.spr_Menu_Parent_SelectDataTable dtblParentMenu = null;
            dtblParentMenu = tadpParentMenu.GetData(intAction, intUserID);
            foreach (dstMenu.spr_Menu_Parent_SelectRow drwParentMenu in dtblParentMenu.Rows)
            #region for context
            {
                bool blnIsMenuOpen = false;
                if ((intActiveMenuID == drwParentMenu.ID))
                {
                    blnIsMenuOpen = true;
                }
                else
                {
                    spr_Menu_CheckSubMenuIsaChild_SelectTableAdapter tadpMenuCheckIsSubMenu = new spr_Menu_CheckSubMenuIsaChild_SelectTableAdapter();
                    dstMenu.spr_Menu_CheckSubMenuIsaChild_SelectDataTable dtblMenuCheckIsSubMenu = null;
                    dtblMenuCheckIsSubMenu = tadpMenuCheckIsSubMenu.GetData(drwParentMenu.ID, intActiveMenuID);
                    if (dtblMenuCheckIsSubMenu.Rows.Count > 0)
                    {
                        blnIsMenuOpen = true;
                    }

                }

                if (blnIsMenuOpen == true)
                {
                    strMenuText += "<li class=\"treeview active\">";
                }
                else
                {
                    strMenuText += "<li class=\"treeview\">";
                }




                if (drwParentMenu.IsLeaf == true && drwParentMenu.IsURLNull() == false)
                {
                    strMenuText += "<a href = \"" + drwParentMenu.URL + "\" >";
                }
                else
                {
                    strMenuText += "<a href = \"#\" >";
                }

                strMenuText += "<i class=\"" + drwParentMenu.IconStyle + "\"></i><span> " + drwParentMenu.MenuTitle + "</span>";
                if (drwParentMenu.IsLeaf == false)
                {
                    strMenuText += "<i class=\"fa fa-angle-left pull-right\"></i>";
                }
                else if (drwParentMenu.HasExtraInfo == true)
                {
                    strMenuText += "<small class=\"" + drwParentMenu.ExtraInfoStyle + "\">" + this.GetMenuExtraInfo(drwParentMenu.ID) + "</small>";
                }


                strMenuText += "</a>";
                strMenuText += this.GetMenuChildrenString(blnForItemAdmin, drwParentMenu.ID, intUserID, intActiveMenuID);
                strMenuText += "</li>";
            }
            #endregion
            strMenuText += "</ul>";
            
        }

        private string GetMenuChildrenString(bool blnForItemAdmin, int inttheParentID, int intUserID=0, int intActiveMenuID=0)
        {
          string strResultHTML = "";
          int intAction = 2;
            if (blnForItemAdmin == true)
            {
                intAction = 1;
            }
            
            strResultHTML = "<ul class=\"treeview-menu\">";
            spr_Menu_Child_SelectTableAdapter adtpChildMenu = new spr_Menu_Child_SelectTableAdapter();
            dstMenu.spr_Menu_Child_SelectDataTable dtblChildMenu = null;
            dtblChildMenu = adtpChildMenu.GetData(intAction, intUserID, inttheParentID);
            foreach (dstMenu.spr_Menu_Child_SelectRow drwChidMenu in dtblChildMenu.Rows)
            {
                bool  blnIsMenuOpen = false;
                if (intActiveMenuID == drwChidMenu.ID)
                {
                    blnIsMenuOpen = true;
                }
                else
                {
                    spr_Menu_CheckSubMenuIsaChild_SelectTableAdapter tadpMenuCheckIsSubMenu = new spr_Menu_CheckSubMenuIsaChild_SelectTableAdapter();
                    dstMenu.spr_Menu_CheckSubMenuIsaChild_SelectDataTable dtblMenuCheckIsSubMenu = null;
                    dtblMenuCheckIsSubMenu = tadpMenuCheckIsSubMenu.GetData(drwChidMenu.ID, intActiveMenuID);
                    if (dtblMenuCheckIsSubMenu.Rows.Count > 0)
                    {
                        blnIsMenuOpen = true;
                    }

                }

                if (blnIsMenuOpen == true)
                {
                    strResultHTML += "<li class=\"active\">";
                }
                else
                {
                    strResultHTML += "<li>";
                }

                if (drwChidMenu.IsLeaf == true && drwChidMenu.IsURLNull() == false)
                {
                    strResultHTML += "<a href = \"" + drwChidMenu.URL + "\" >";
                }
                else
                {
                    strResultHTML += "<a href = \"#\" >";
                }

                strResultHTML += "<i class=\"" + drwChidMenu.IconStyle + "\"></i><span> " + drwChidMenu.MenuTitle + "</span>";

                if ((drwChidMenu.IsLeaf == false))
                {
                    strResultHTML += "<i class=\"fa fa-angle-left pull-right\"></i>";
                }
                else if ((drwChidMenu.HasExtraInfo == true))
                {
                    strResultHTML += "<small class=\"" + drwChidMenu.ExtraInfoStyle + "\">" + this.GetMenuExtraInfo(drwChidMenu.ID) + "</small>";

                }

                strResultHTML += this.GetMenuChildrenString(blnForItemAdmin, drwChidMenu.ID, intUserID, intActiveMenuID);
                strResultHTML += "</a></li>";
            }

            strResultHTML += "</ul>";
            if (strResultHTML == "<ul class=\"treeview-menu\"></ul>")
            {
                return "";
            }
            else
            {
                return strResultHTML;
            }

        }

        private string GetMenuExtraInfo(int intMenuID)
        {
            string strExtraInfo = "";
            switch (intMenuID)
            {
                case 4:
                    strExtraInfo = "5";
                    break;
                case 12:
                    strExtraInfo = "new!";
                    break;
                case 17:
                    strExtraInfo = "24";
                    break;
            }
            return strExtraInfo;
        }






    }
}
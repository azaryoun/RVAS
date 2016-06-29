using System.Web.Configuration;

namespace Service.Consts
{
    public class AppSettings
    {
        public static string HashKey
        {
            get { return WebConfigurationManager.AppSettings["HashKey"]; }
        }

        public static string AdminRoleName
        {
            get { return WebConfigurationManager.AppSettings["AdminRoleName"]; }
        }
        public static string FounderRoleGroupId
        {
            get { return WebConfigurationManager.AppSettings["FounderRoleGroupId"]; }
        }
        public static string AccountantRoleGroupId
        {
            get { return WebConfigurationManager.AppSettings["AccountantRoleGroupId"]; }
        }

        public static string FinancialManagerRoleGroupId
        {
            get { return WebConfigurationManager.AppSettings["FinancialManagerRoleGroupId"]; }
        }
        public static string ManagerRoleGroupId
        {
            get { return WebConfigurationManager.AppSettings["ManagerRoleGroupId"]; }
        }
        
        public static string AccountTypeSeporde
        {
            get { return WebConfigurationManager.AppSettings["AccountTypeSeporde"]; }
        }
        public static string CompanyId
        {
            get { return WebConfigurationManager.AppSettings["CompanyId"]; }
        }
        
        
    }
}
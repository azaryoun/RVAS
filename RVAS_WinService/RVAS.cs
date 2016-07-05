using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RVAS_WinService
{
    public partial class RVAS : ServiceBase
    {
        public RVAS()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Threading.Thread trMonitorVASServices 
                = new System.Threading.Thread(MonitorVASServices);
            trMonitorVASServices.Start();

        }

        protected override void OnStop()
        {
        }

        private void MonitorVASServices()
        {
            BusinessObject.Context.dbVASEntities ctxVAS
                = new BusinessObject.Context.dbVASEntities();

            while (true) {
                try
                {
//var lnq= ctxVAS.tbl_NewsContent

                }
                catch
                {
                    continue;
                }
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ChiaSeCode_TH
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.IO.StreamReader stReader = new System.IO.StreamReader(Server.MapPath("~/sl.txt"));
            String s = stReader.ReadLine();
            stReader.Close();
            Application["Luot_truy_cap"] = int.Parse(s);
            Application["So_nguoi_online"] = 0;
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();

            Application["Luot_truy_cap"] = int.Parse(Application["Luot_truy_cap"].ToString()) + 1;
            Application["So_nguoi_online"] = int.Parse(Application["So_nguoi_online"].ToString()) + 1;
            Application.UnLock();

            System.IO.StreamWriter stWriter = new System.IO.StreamWriter(Server.MapPath("~/sl.txt"));
            stWriter.Write(Application["Luot_truy_cap"]);
            stWriter.Close();
        }
        protected void Session_End(object sender, EventArgs e)
        {

            Application.Lock();
            Application["So_nguoi_online"] = int.Parse(Application["So_nguoi_online"].ToString()) - 1;
            Application.UnLock();
        }
    }
}

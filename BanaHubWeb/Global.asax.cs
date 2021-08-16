using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BanaHubWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.IO.StreamReader rd = new System.IO.StreamReader(Server.MapPath("LuotTruyCap.txt"));
            string s = rd.ReadLine();
            rd.Close();
            Application.Add("HitCounter", s);
            Application["Online"] = 0;
        }

        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["Online"] = int.Parse(Application["Online"].ToString()) + 1;
            Application["HitCounter"] = int.Parse(Application["HitCounter"].ToString()) + 1;
            Application.UnLock();
            System.IO.StreamWriter wt = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/LuotTruyCap.txt"));
            wt.Write(Application["HitCounter"]);
            wt.Close();
        }

        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["Online"] = int.Parse(Application["Online"].ToString()) - 1;
            Application.UnLock();
        }
    }
}

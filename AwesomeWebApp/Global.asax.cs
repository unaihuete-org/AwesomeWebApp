using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AwesomeWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {

        //used to store leaking memory
        public static List<byte[]> _memListLeakGen = new List<byte[]>();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            Trace.WriteLine(string.Format("Request: {0}:{1}", Request.HttpMethod, Request.RawUrl));
        }
    }
}

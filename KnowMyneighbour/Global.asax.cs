using KnowMyneighbour.Magic;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KnowMyneighbour
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            //Role rr = new Role();
            //rr.AddUserAndRole("", "");
        }

        protected void Application_Error()
        {
            var telemetry = new TelemetryClient();
            var log = Server.GetLastError();
            Log.Error(log.ToString());
            telemetry.TrackException(log, null, null);
            
        
        }
    }
}

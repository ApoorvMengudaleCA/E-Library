using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using E_Library.Entities;

namespace E_Library.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ApplicationAPIKeys AppKeys = new ApplicationAPIKeys();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ReadConfigurationData();
        }

        private static void ReadConfigurationData()
        {
            AppKeys.DatabaseConnectionString = "data source=198.204.230.226;initial catalog=E-Library;persist security info=True;user id=sa;password=C@2o21@5Q1!@#^;MultipleActiveResultSets=True";
        }
    }


}

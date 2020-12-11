using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            UnityService unityConfig = new UnityService();
            GlobalConfiguration.Configuration.DependencyResolver = unityConfig.Resolver;
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new DefaultHttpControllerSelector(GlobalConfiguration.Configuration));
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(unityConfig.Resolver);
        }
    }
}

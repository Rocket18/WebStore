using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebStore.UI.Infrastructure;
using WebStore.Models.Entities;
using WebStore.Models.Binders;

namespace WebStore.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            ModelBinders.Binders.Add(typeof(Cart), new   CartModelBinder());
           
        }
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var identity = new WebStore.UI.Infrastructure.Concrete.CustomIdentity(HttpContext.Current.User.Identity);
                var principal = new WebStore.UI.Infrastructure.Concrete.CustomPrincipal(identity);
                HttpContext.Current.User = principal;
            }
        }
    }
}
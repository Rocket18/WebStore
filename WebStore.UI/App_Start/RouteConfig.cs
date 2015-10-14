using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebStore.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         

            routes.MapRoute(null,
                "",// /
                new { controller = "Product", action = "List", category = (Int32?)null, page = 1 },
                new { page = @"\d+" },
                new[] { "WebStore.UI.Controllers" }
           );

            routes.MapRoute(
              null,
             "{controller}/Page{page}",// /Page2, /Page n
                new { controller = "Product", action = "List", category = (Int32?)null },
                new { page = @"\d+" },
                new[] { "WebStore.UI.Controllers" }
           );

            routes.MapRoute(
         null,
        "{controller}/Category{category}", // /Category1, /Category n
           new { controller = "Product", action = "List", page = 1 },
                new[] { "WebStore.UI.Controllers" }
      );
            routes.MapRoute(
               null,
              "{controller}/Category{category}/Page{page}",// /Category1/Page1, /Category n/Page n
                 new { controller = "Product", action = "List" },
                  new { page = @"\d+" },
                new[] { "WebStore.UI.Controllers" }
            );


            routes.MapRoute(
                 "Default",
                "{controller}/{action}/{id}",
                 new { controller = "Product", action = "List", id = UrlParameter.Optional },
                new[] { "WebStore.UI.Controllers" }

            );
            routes.MapRoute("NotFound", "{*url}",
       new { controller = "Error", action = "Http404" });
        }
    }
}
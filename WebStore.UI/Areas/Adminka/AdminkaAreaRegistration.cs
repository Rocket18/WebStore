using System.Web.Mvc;

namespace WebStore.UI.Areas.Adminka
{
    public class AdminkaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Adminka";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "Adminka_default",
                "Adminka/{controller}/{action}/{id}",
                new {controller="Product",  action = "Index", id = UrlParameter.Optional}
            );
        
        }
    }
}

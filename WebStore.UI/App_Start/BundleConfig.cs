using System.Web;
using System.Web.Optimization;

namespace WebStore.UI
{
    public class BundleConfig
    {
        // Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //bundles.IgnoreList.Ignore("*.intellisense.js");
            //bundles.IgnoreList.Ignore("*-vsdoc.js");
            //bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                       "~/Scripts/jquery-ui-{version}.min.js"));

            bundles.Add(new StyleBundle("~/Content/Style").Include(
                 "~/Content/main.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.unobtrusive-ajax.min.js",
                       "~/Scripts/jquery.validate-vsdoc.js",
                       "~/Scripts/jquery.validate.min.js",
                       "~/Scripts/jquery.validate.unobtrusive.min.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/Admin").Include(
                "~/Content/Admin.css"
               ));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        }
    }
}
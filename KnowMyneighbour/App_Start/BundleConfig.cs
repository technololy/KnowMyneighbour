using System.Web;
using System.Web.Optimization;

namespace KnowMyneighbour
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));



            //refernce all js file at once by lolade
            //bundles.Add(new StyleBundle("~/assets/js/").IncludeDirectory(
            //   "~/assets/js/", "*.js", true
            //    ));

            //bundles.Add(new StyleBundle("~/assets/js").Include(
            // "~/assets/js/core/bootstrap.min.js"
            //  ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/assets/css/bootstrap.min.css",
                   "~/assets/css/now-ui-kit.css"));

        }
    }
}

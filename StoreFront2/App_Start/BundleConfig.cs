using System.Web.Optimization;

namespace StoreFront2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/jquery-3.3.1.min.js",
                      "~/Content/js/jquery-ui.js", 
                      "~/Content/js/popper.min.js",
                      "~/Content/js/bootstrap.min.js", 
                      "~/Content/js/owl.carousel.min.js", 
                      "~/Content/js/jquery.magnific-popup.min.js", 
                      "~/Content/js/aos.js",
                      "~/Content/js/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/magnific-popup.css",
                      "~/Content/css/jquery-ui.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/owl.theme.default.min.css",
                      "~/Content/css/aos.css",
                      "~/Content/css/style.css"));
        }
    }
}

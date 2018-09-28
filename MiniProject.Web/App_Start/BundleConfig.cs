using System.Web;
using System.Web.Optimization;

namespace MiniProject.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.knob.min.js",
                        "~/Scripts/jquery.slimscroll.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/daterangepicker.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/adminlte.min.js",
                      "~/Scripts/pages/dashboard.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/sweetalert2.all.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js",
                      "~/Scripts/select2.full.min.js",
                      "~/Scripts/jquery.inputmask.js",
                      "~/Scripts/jquery.inputmask.extensions.js",
                      "~/Scripts/jquery.inputmask.date.extensions.js",
                      "~/Scripts/icheck.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/AdminLTE.min.css",
                      "~/Content/skins/_all-skins.min.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/daterangepicker.css",
                      "~/Content/dataTables.bootstrap.min.css",
                      "~/Content/select2.min.css",
                      "~/Content/sweetalert.min.css",
                      "~/Content/blue.css"
                      ));
        }
    }
}

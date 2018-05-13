using System.Web;
using System.Web.Optimization;

namespace GuardianChecks
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                         "~/Scripts/modernizr-*",
                         "~/Scripts/bootstrap.js",
                         "~/Scripts/jquery-ui-1.12.1.js",
                         "~/Scripts/DataTables/jquery.dataTables.js",
                         "~/Scripts/DataTables/dataTables.bootstrap.js",
                         "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/Styles").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/site.css"));
        }
    }
}

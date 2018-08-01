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
									 "~/Scripts/DataTables/jquery.dataTables.min.js",
									"~/Scripts/DataTables/dataTables.bootstrap.min.js",
									"~/Scripts/DataTables/dataTables.fixedHeader.js",
									 "~/Scripts/bootstrap-toggle.js",
									 "~/Scripts/respond.js",
									 "~/Scripts/site.js"));

			bundles.Add(new StyleBundle("~/Content/Styles").Include(
								"~/Content/bootstrap.css",
								"~/Content/bootstrap-toggle.css",
								"~/Content/font-awesome.css",
								"~/Content/datatables/css/buttons.bootstrap.min.css",
								"~/Content/datatables/css/dataTables.bootstrap.min.css",
								"~/Content/datatables/css/fixedHeader.bootstrap4.css",
								"~/Content/themes/base/jquery-ui.css",
								"~/Content/site.css"));
		}
	}
}

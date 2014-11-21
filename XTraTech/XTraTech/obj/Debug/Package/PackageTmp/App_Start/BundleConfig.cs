using System.Web;
using System.Web.Optimization;

namespace XTraTech
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(new string[]
			{
				"~/Scripts/jquery-{version}.min.js"
			}));
            bundles.Add(new ScriptBundle("~/Content/js").Include(new string[]
			{
				"~/Scripts/jquery-ui.js"
			}));
            bundles.Add(new StyleBundle("~/Content/css").Include(new string[]
			{
				"~/Content/style.css",
				"~/Content/jquery-ui.css",
				"~/Content/layout.css"
			}));
        }
    }
}
using System.Web;
using System.Web.Optimization;

namespace WebApplication
{
    /// <summary>
    /// bundle config class
    /// </summary>
    public class BundleConfig
    {
        #region methods
        /// <summary>
        /// method for registering bundles
        /// </summary>
        /// <param name="bundles">bundle collection</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-readable.css",
                      "~/Content/site.css"));
        }
        #endregion methods
    }
}

using System.Web;
using System.Web.Optimization;

namespace Copasa.Atende.WebService
{
    /// <summary>
    /// Classe BundleConfig.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// RegisterBundles
        /// </summary>
        /// <param name="bundles">BundleCollection</param>
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/plugins/jQuery/jQuery-2.1.4.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                "~/Content/bootstrap/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/Copasa/css").Include(
                "~/Content/dist/css/CopasaStyle.css"));

            bundles.Add(new ScriptBundle("~/bundles/app/js").Include(
                "~/Content/dist/js/app.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Content/bootstrap/css/bootstrap.css",
                "~/Content/bootstrap/css/font-awesome.css",
                "~/Content/bootstrap/css/ionicons.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/Admin/css").Include(
                "~/Content/dist/css/AdminLTE.css",
                "~/Content/dist/css/skins/_all-skins.css"));
        }
    }
}

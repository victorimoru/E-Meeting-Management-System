using System.Web;
using System.Web.Optimization;

namespace E_MeetingMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/multiselect.js",
                         "~/Scripts/bootbox.js",
                         "~/Scripts/app.js/w3.js",
                          "~/Scripts/vue.js",
                          "~/Scripts/vue-swal.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/viewer-lib").Include(
                       "~/Scripts/vue.js",
                      "~/Scripts/vue-swal.js"));
                
                      



            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/journal-edited.css",
                      "~/Content/w3_updated.css",
                      "~/Content/site.css",
                      "~/Content/Custom.css"));

            bundles.Add(new StyleBundle("~/Content/Dashboard_css").Include(
                        "~/Content/journal-edited.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/w3_updated.css",
                      "~/Content/Custom.css"));

            bundles.Add(new StyleBundle("~/Content/Home_css").Include(
                     "~/Content/homeStyles.css"));

            bundles.Add(new StyleBundle("~/Content/viewer_css").Include(
                       "~/Content/journal-edited.css",
                     "~/Content/datatables/css/datatables.bootstrap.css",
                     "~/Content/w3_updated.css",
                     "~/Content/Custom.css"));

        }
    }
}

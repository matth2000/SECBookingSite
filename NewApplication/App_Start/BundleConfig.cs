﻿using System.Web;
using System.Web.Optimization;

namespace NewApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables/jquery.datatables.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/adminlte/css/adminlte.min.css",
                      "~/Content/adminlte/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/adminlte/plugins/summernote/summernote-bs4.css",
                      "~/Content/site.css"));
                
            bundles.Add(new StyleBundle("~/Content/css/datatables").Include(
                      "~/Content/css/DataTables/jquery.datatables.css"));

            bundles.Add(new ScriptBundle("~/Content/adminlte/js").Include(
              "~/Content/adminlte/plugins/summernote/summernote-bs4.js2",
             "~/Content/adminlte/js/adminlte.min.js"));

        }
    }
}

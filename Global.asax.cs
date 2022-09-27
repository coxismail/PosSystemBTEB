using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace PosSystem
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            var er = HttpContext.Current.Error;
            if (er == null)
            {

            }
            else if (er.GetType().Equals(typeof(System.Security.SecurityException)))
            {
                HttpContext.Current.Response.Redirect("/account/login.aspx?ReturnUrl=" + HttpContext.Current.Request.Path);
            }
            // Code that runs when an unhandled error occurs

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Natroral.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("About", "about", new { controller = "Home", action = "About" });
            routes.MapRoute("Contact", "contact", new { controller = "Contact", action = "Create" });

            routes.MapRoute("Login", "login", new { controller = "Account", action = "Login" });
            routes.MapRoute("Register", "register", new { controller = "Account", action = "Register" });

            routes.MapRoute("Details", "product-details/{name}", new { controller = "Home", action = "Details", name = UrlParameter.Optional });
            routes.MapRoute("Products", "products", new { controller = "Home", action = "ProductListing" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

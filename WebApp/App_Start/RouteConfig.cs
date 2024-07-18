using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ToolsApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

             routes.MapRoute(
             name: "QuanLyTrang",
             url: "QuanLyTrang/{action}/{id}",
             defaults: new { controller = "PageManagement", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "ToolsApp.Controllers" }
         );
             routes.MapRoute(
             name: "QuanLyDanhMuc",
             url: "QuanLyDanhMuc/{action}/{id}",
             defaults: new { controller = "MenuManagement", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "ToolsApp.Controllers" }
         );
            routes.MapRoute(
            name: "QuanLyNhanSu",
            url: "QuanLyNhanSu/{action}/{id}",
            defaults: new { controller = "ManagerStaff", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "ToolsApp.Controllers" }
         );
           routes.MapRoute(
           name: "KiemTraRoHang",
           url: "KiemTraRoHang/{action}/{id}",
           defaults: new { controller = "CheckCart", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "ToolsApp.Controllers" }
        );
             routes.MapRoute(
             name: "QuanLyRoHang",
             url: "QuanLyRoHang/{action}/{id}",
             defaults: new { controller = "CartManagerment", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "ToolsApp.Controllers" }
        );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ToolsApp.Controllers" }
            );
        }
    }
}

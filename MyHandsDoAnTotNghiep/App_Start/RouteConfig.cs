using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyHandsDoAnTotNghiep
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
               name: "Danh muc san pham",
               url: "san-pham/{metatitle}-{id}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "Tim kiem",
               url: "tim-kiem",
               defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "san pham",
               url: "san-pham",
               defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "dang ky",
               url: "dang-ky",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "dang nhap",
               url: "dang-nhap",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "bai viet",
               url: "bai-viet",
               defaults: new { controller = "BaiViet", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "chi tiet bai viet",
               url: "bai-viet/{metatitle}-{id}",
               defaults: new { controller = "BaiViet", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "tag",
               url: "tag/{tagid}",
               defaults: new { controller = "BaiViet", action = "ViewByTag", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "Chi tiet san pham",
               url: "chi-tiet/{metatitle}-{id}",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "Gioi thieu",
               url: "gioi-thieu",
               defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "lien he",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "them vao gio",
               url: "them-vao-gio",
               defaults: new { controller = "GioHangItem", action = "AddToCart", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
               name: "gio hang",
               url: "gio-hang",
               defaults: new { controller = "GioHangItem", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
           );
            routes.MapRoute(
              name: "thanh toan",
              url: "thanh-toan",
              defaults: new { controller = "GioHangItem", action = "Payment", id = UrlParameter.Optional },
              namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
          );
            routes.MapRoute(
              name: "thanh toan thanh cong",
              url: "hoan-thanh",
              defaults: new { controller = "GioHangItem", action = "Success", id = UrlParameter.Optional },
              namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyHandsDoAnTotNghiep.Controllers" }
            );
        }
    }
}

using Model.DAO;
using MyHandsDoAnTotNghiep.Common;
using MyHandsDoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var sanphamdao = new SanPhamDAO();
            var danhmucspdao = new DanhMucSPDAO();
            var baivietdao = new BaiVietDAO();
            ViewBag.newarrivals = sanphamdao.ListProductNewArrivals(10);
            ViewBag.bestseller = sanphamdao.ListProductBestSeller(10);
            ViewBag.productcategories = danhmucspdao.ListAll();
            ViewBag.baiviethome = baivietdao.listHomeContent(3);
            return View();
        }
        [ChildActionOnly]
        public ActionResult NavBarMenu()
        {
            var model = new MenuDAO().ListByGroupMenuId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDAO().GetFooterDB();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult CartMain()
        {

            var cartsession = Session[CommonConstants.CartSession];
            var cartlist = new List<GioHangItems>();

            if (cartsession != null)
            {
                cartlist = (List<GioHangItems>)cartsession;
            }
            return PartialView(cartlist);
        }

    }
}
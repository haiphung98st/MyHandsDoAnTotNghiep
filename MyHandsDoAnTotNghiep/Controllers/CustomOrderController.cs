using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class CustomOrderController : Controller
    {
        // GET: CustomOrder
        public ActionResult Index()
        {
            //var customCAT = new CustomDAO();

            //ViewBag.customCAT = customCAT.listAllCustomCategories();
            var danhmucspdao = new DanhMucSPDAO();
            ViewBag.productcategories = danhmucspdao.ListAll();
            return View();
        }
        public ActionResult Custom(int id)
        {
            //var category = new CustomDAO().viewByDanhMuc(id);
            //ViewBag.Category = category;
            //var model = new CustomDAO().ListProductByCategoryID(id);
            //return View(model);
            var category = new DanhMucSPDAO().viewByDanhMuc(id);
            ViewBag.Category = category;
            var model = new SanPhamDAO().ViewOptionByIDdanhmuc(id);
            
            var product = new SanPhamDAO().ListByCustomActive(id);
            ViewBag.product = product;

            var TenDanhMuc = new SanPhamDAO().getNameoptionByID(id);
            ViewBag.TenDanhMuc = TenDanhMuc;
            return View(model);

        }
    }
}
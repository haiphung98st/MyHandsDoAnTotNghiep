using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var sanphamdao = new SanPhamDAO();
            ViewBag.newarrivals = sanphamdao.ListProductNewArrivals(100);
            return View("DanhSachSanPham");
        }
        [ChildActionOnly]
        public PartialViewResult NewArriveMenu()
        {
            var model = new DanhMucSPDAO().ListAll();
            return PartialView(model);
        }
        
        public ActionResult Category(int id, int page = 1, int pageSize=3)
        {
            var category = new DanhMucSPDAO().viewByDanhMuc(id);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new SanPhamDAO().ListProductByCategoryID(id, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new SanPhamDAO().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new SanPhamDAO().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.keyword = keyword;

            int maxPage = 10;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var product = new SanPhamDAO().chiTietSanPham(id);
            ViewBag.Category = new DanhMucSPDAO().viewDetail(product.IDDanhMuc.Value);
            return View(product);
        }
    }
}
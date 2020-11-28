using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class DonNhapHangController : BaseController
    {
        // GET: Admin/DonNhapHang
        //[CheckPermission("")]
        public ActionResult Index(string SearchSanPham, int page = 1, int pagesize = 5)
        {
            var dao = new HoaDonNhapDAO();
            var model = dao.ListAllPaging(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }
    }
}
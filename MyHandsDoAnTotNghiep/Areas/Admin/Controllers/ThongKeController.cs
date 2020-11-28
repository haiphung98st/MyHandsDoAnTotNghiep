using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class ThongKeController : BaseController
    {
        MyHandsDbContext db = null;
        // GET: Admin/ThongKe
        [CheckPermission(RoleID = "VIEW_STATISTIC")]
        public ActionResult Index(string SearchSanPham, int page = 1, int pagesize = 4)
        {
            var dao = new SanPhamDAO();
            var model = dao.ListAllPaging(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }
        [CheckPermission(RoleID = "VIEW_STATISTIC")]

        public ActionResult CanceledOrder(string SearchSanPham, int page = 1, int pagesize = 4)
        {
            var dao = new HoaDonDAO();
            var model = dao.ListAllCancelOrderPaging(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }
        [CheckPermission(RoleID = "VIEW_STATISTIC")]

        public ActionResult DoanhThu( int page = 1, int pagesize = 4)
        {
            int totalRecord = 0;
            var dao = new HoaDonDAO();
            var model = dao.ListDetailByOrder( ref totalRecord,page, pagesize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pagesize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
    }
}
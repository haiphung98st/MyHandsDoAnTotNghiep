using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class BaiVietController : Controller
    {
        // GET: BaiViet
        public ActionResult Index(int page=1,int pagesize=10)
        {
            var model = new BaiVietDAO().ListAllPaging(page, pagesize);
            int totalRecord = 0;

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

        public ActionResult Detail(long id)
        {
            var model = new BaiVietDAO().GetBaiVietByID(id);
            ViewBag.Tag = new BaiVietDAO().ListTags(id);
            ViewBag.BaiVietCMT = new BaiVietCommentDAO().ListAll(id);

            return View(model);

        }
        public ActionResult ViewByTag(string tagid, int page = 1, int pagesize = 10)
        {
            var model = new BaiVietDAO().ListAllByTag(tagid,page,pagesize);
            int totalRecord = 0;

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Tagid = new BaiVietDAO().getTag(tagid);
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
        [HttpPost]
        public JsonResult AddComment(string sNoiDung, string IDBaiViet)
        {
            var baivietCMT = new tbl_BaiVietComment();
            var userSession = (UserLogin)Session[MyHandsDoAnTotNghiep.Common.CommonConstants.USER_SESSION];
            long idbaiviet = long.Parse(IDBaiViet);
            baivietCMT.sNoiDung = sNoiDung;
            baivietCMT.IDBaiViet = idbaiviet;
            baivietCMT.dNgayTao = DateTime.Now;
            baivietCMT.sTenNguoiDung = userSession.HoTen;
            baivietCMT.bStatus = true;
            var id = new BaiVietCommentDAO().InsertCMT(baivietCMT);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
                return Json(new
                {
                    status = false
                });
        }
    }
}
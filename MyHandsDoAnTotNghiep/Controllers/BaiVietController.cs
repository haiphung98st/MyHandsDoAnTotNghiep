using Model.DAO;
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
    }
}
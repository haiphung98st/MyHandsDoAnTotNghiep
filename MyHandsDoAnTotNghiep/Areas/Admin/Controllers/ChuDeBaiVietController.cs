using Model.DAO;
using Model.EF;
using MyHandsDoAnTotNghiep.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class ChuDeBaiVietController : BaseController
    {
        // GET: Admin/ChuDeBaiViet
        public ActionResult Index(string SearchChuDe, int page = 1, int pagesize = 10)
        {
            var dao = new DanhMucBaiVietDAO();
            var model = dao.ListAllPaging(SearchChuDe, page, pagesize);
            ViewBag.SearchChuDe = SearchChuDe;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_ChuDe model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiTao = session.UserName;
                new DanhMucBaiVietDAO().Create(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new DanhMucBaiVietDAO();
            var danhmucsanpham = dao.viewDetail(id);

            return View(danhmucsanpham);
        }
        [HttpPost]
        public ActionResult Edit(tbl_ChuDe model, FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {
                long idchude = long.Parse(formcollection["hdnID"]);
                //var dao = new SanPhamDAO();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiSua = session.UserName;
                model.IDChuDe = idchude;

                //var result = dao.Edit(model);
                new DanhMucBaiVietDAO().Update(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            new DanhMucBaiVietDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]

        public JsonResult ChangeStatus(long id)
        {
            var result = new DanhMucBaiVietDAO().ChangeStatusDanhMuc(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
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
    public class DanhMucSanPhamController : BaseController
    {
        // GET: Admin/DanhMucSanPham
        public ActionResult Index(string SearchDanhMuc, int page = 1, int pagesize = 10)
        {
            var dao = new DanhMucSPDAO();
            var model = dao.ListAllPaging(SearchDanhMuc, page, pagesize);
            ViewBag.SearchDanhMuc = SearchDanhMuc;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_DanhMucSanPham model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiTao = session.UserName;
                new DanhMucSPDAO().Create(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new DanhMucSPDAO();
            var danhmucsanpham = dao.viewDetail(id);

            return View(danhmucsanpham);
        }
        [HttpPost]
        public ActionResult Edit(tbl_DanhMucSanPham model,FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {
                long iddanhmuc = long.Parse(formcollection["hdnID"]);
                //var dao = new SanPhamDAO();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiSua = session.UserName;
                model.IDDanhMuc = iddanhmuc;

                //var result = dao.Edit(model);
                new DanhMucSPDAO().Update(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            new DanhMucSPDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]

        public JsonResult ChangeStatus(long id)
        {
            var result = new DanhMucSPDAO().ChangeStatusDanhMuc(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
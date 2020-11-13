using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using MyHandsDoAnTotNghiep.Common;

namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class SanPhamController : BaseController
    {
        // GET: Admin/SanPham
        public ActionResult Index(string SearchSanPham, int page = 1, int pagesize = 5)
        {
            var dao = new SanPhamDAO();
            var model = dao.ListAllPaging(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }

        public ActionResult Edit(long id)
        {
            var dao = new SanPhamDAO();
            var sanpham = dao.GetSanPhamByID(id);

            setViewBag(sanpham.IDDanhMuc);
            return View(sanpham);
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(tbl_SanPham model)
        {
            if (ModelState.IsValid)
            {
                //var dao = new SanPhamDAO();

                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiSua = session.UserName;
                //var result = dao.Edit(model);
                new SanPhamDAO().Update(model);

                return RedirectToAction("Index");
            }

            setViewBag(model.IDDanhMuc);

            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(tbl_SanPham model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiTao = session.UserName;
                new SanPhamDAO().Create(model);
                return RedirectToAction("Index");
            }
            setViewBag();
            return View();
        }
        public void setViewBag(long? selectedID = null)
        {
            var dao = new DanhMucSPDAO();
            ViewBag.IDdanhmuc = new SelectList(dao.ListAll(), "IDDanhMuc", "sTenDanhMuc", selectedID);
        }
        public ActionResult Delete(int id)
        {
            new SanPhamDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]

        public JsonResult ChangeStatus(long id)
        {
            var result = new SanPhamDAO().ChangeStatusSanPham(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
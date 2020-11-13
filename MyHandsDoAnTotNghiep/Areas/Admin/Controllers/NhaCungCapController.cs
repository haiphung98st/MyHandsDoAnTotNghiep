
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
    public class NhaCungCapController : BaseController
    {
        // GET: Admin/NhaCungCap
        public ActionResult Index(string SearchNCC, int page = 1, int pagesize = 5)
        {
            var dao = new NhaCungCapDAO();
            var model = dao.ListAllPaging(SearchNCC, page, pagesize);
            ViewBag.SearchNCC = SearchNCC;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult Edit(long id)
        {
            var dao = new NhaCungCapDAO();
            var ncc = dao.GetByID(id);

            return View(ncc);
        }

        [HttpPost]
        public ActionResult Edit(tbl_NCC model,FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {
                //var dao = new SanPhamDAO();
                long id = long.Parse(formcollection["hdnID"]);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiTao = session.UserName;
                model.IDncc = id;
                //var result = dao.Edit(model);
                new NhaCungCapDAO().Edit(model);

                return RedirectToAction("Index");
            }


            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_NCC model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiTao = session.UserName;
                new NhaCungCapDAO().Create(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpPost]

        public JsonResult ChangeStatus(long id)
        {
            var result = new NhaCungCapDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        
    }
}
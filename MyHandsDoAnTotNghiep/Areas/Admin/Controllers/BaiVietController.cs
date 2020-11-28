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
    public class BaiVietController : BaseController
    {
        // GET: Admin/BaiViet
        [CheckPermission(RoleID = "VIEW_CONTENT")]
        public ActionResult Index(string SearchBaiViet, int page = 1, int pagesize = 4)
        {
            var dao = new BaiVietDAO();
            var model = dao.ListAllPaging(SearchBaiViet, page, pagesize);
            ViewBag.SearchBaiViet = SearchBaiViet;
            return View(model);
        }
        [HttpGet]
        [CheckPermission(RoleID = "ADD_CONTENT")]

        public ActionResult Create()
        {
            setViewBag();
            return View();
        }
        [CheckPermission(RoleID = "EDIT_CONTENT")]

        public ActionResult Edit(long id)
        {
            var dao = new BaiVietDAO();
            var baiviet = dao.GetBaiVietByID(id);

            setViewBag(baiviet.IDChuDe);
            return View(baiviet);
        }

        [HttpPost, ValidateInput(false)]
        [CheckPermission(RoleID = "EDIT_CONTENT")]

        public ActionResult Edit(tbl_BaiViet model, FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {
                //var dao = new BaiVietDAO();
                long idbaiviet = long.Parse( formcollection["hdnIDbaiviet"]);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiSua = session.UserName;
                model.IDBaiViet = idbaiviet;
                //var result = dao.Edit(model);
                new BaiVietDAO().Edit(model);

                return RedirectToAction("Index");
            }
            
            setViewBag(model.IDChuDe);

            return View();
        }
        [HttpPost, ValidateInput(false)]
        [CheckPermission(RoleID = "ADD_CONTENT")]

        public ActionResult Create(tbl_BaiViet model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiTao = session.UserName;
                new BaiVietDAO().Create(model);
                return RedirectToAction("Index");
            }
            setViewBag();
            return View();
        }
        public void setViewBag(long? selectedID = null)
        {
            var dao = new DanhMucBaiVietDAO();
            ViewBag.IDChuDe = new SelectList(dao.ListChude(), "IDChuDe", "sTenChuDe", selectedID);
        }
        [CheckPermission(RoleID = "DELETE_CONTENT")]

        public ActionResult Delete(int id)
        {
            new BaiVietDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [CheckPermission(RoleID = "EDIT_CONTENT")]


        public JsonResult ChangeStatus(long id)
        {
            var result = new BaiVietDAO().ChangeStatusBaiViet(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
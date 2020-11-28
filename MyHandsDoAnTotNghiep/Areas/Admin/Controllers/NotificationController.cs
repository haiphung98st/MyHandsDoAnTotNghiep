using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyHandsDoAnTotNghiep.Common;


namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class NotificationController : BaseController
    {
        // GET: Admin/Notification
        public ActionResult Index(string SearchNotification, int page = 1, int pagesize = 4)
        {
            var dao = new NotificationDAO();
            var model = dao.ListAllPaging(SearchNotification, page, pagesize);
            ViewBag.SearchNotification = SearchNotification;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_Notification model)
        {
            if (ModelState.IsValid)
            {
                new NotificationDAO().Create(model);
                return RedirectToAction("Index");
            }
            setViewBag();
            return View();
        }
        public void setViewBag(long? selectedID = null)
        {
            var dao = new UserDao();
            ViewBag.IDkhachhang = new SelectList(dao.ListAll(), "ID", "sHoTen", selectedID);
        }
        public ActionResult Edit(long id)
        {
            var dao = new NotificationDAO();
            var notification = dao.GetNotificationByID(id);
            setViewBag(notification.ID);

            return View(notification);
        }

        [HttpPost]

        public ActionResult Edit(tbl_Notification model)
        {
            if (ModelState.IsValid)
            {
                new NotificationDAO().Edit(model);
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }


            return View();
        }
        public ActionResult NotificationDetails(int id)
        {
            var model = new NotificationDAO().GetNotificationByID(id);
            return View(model);
        }
    }
}
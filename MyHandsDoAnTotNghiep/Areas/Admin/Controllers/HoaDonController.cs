using MyHandsDoAnTotNghiep.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class HoaDonController : BaseController
    {
        // GET: Admin/HoaDon
        public ActionResult Index(string SearchHoaDon, int page = 1, int pagesize = 4)
        {
            var dao = new HoaDonDAO();
            var model = dao.ListAllPaging(SearchHoaDon, page, pagesize);
            ViewBag.SearchHoaDon = SearchHoaDon;
            return View(model);
        }

        public ActionResult OrderDetails(int id, int page = 1, int pageSize = 10)
        {
           var hoadon = new HoaDonDAO().viewByHoaDoniD(id);
            ViewBag.HoaDon = hoadon;
            int totalRecord = 0;
            var model = new HoaDonDAO().ListDetailByOrderID(id, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public void setViewBag(long? selectedID = null)
        {
            var dao = new TrangThaiHoaDonDAO();
            ViewBag.iMaTrangThai = new SelectList(dao.ListAll(), "ID", "sTenTrangThai", selectedID);
        }

        public ActionResult Edit(long id)
        {
            var dao = new HoaDonDAO();
            var hoadon = dao.GetHoaDonByID(id);

            setViewBag(hoadon.iMaTrangThai);
            return View(hoadon);
        }

        [HttpPost]
        public ActionResult Edit(tbl_HoaDon model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiTiepNhan = session.UserName;
                //var result = dao.Edit(model);
                new HoaDonDAO().Update(model);
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }

            setViewBag(model.iMaTrangThai);

            return View();
        }

    }
}
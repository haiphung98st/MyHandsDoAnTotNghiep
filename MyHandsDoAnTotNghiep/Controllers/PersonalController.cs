using Model.DAO;
using Model.EF;
using MyHandsDoAnTotNghiep.Common;
using MyHandsDoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class PersonalController : Controller
    {
        MyHandsDbContext db = null;
        // GET: Personal
        public ActionResult Index()
        {
            if(Session[Common.CommonConstants.USER_SESSION] == null)
            {
                return Redirect("/User/Login");
            }    
            return View();
        }
        [HttpGet]
        public ActionResult HoSoIndex()
        {
            if (Session[Common.CommonConstants.USER_SESSION] == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult HoSoIndex(string sHoten, string sDiaChi, string sEmail, string sSoDienThoai)
        {
            var user = new tbl_TaiKhoan();
            var dao = new UserDao();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session!= null)
            {
                user.sNguoiSua = session.UserName;
                user.ID = session.UserID;
                user.dNgaySua = DateTime.Now;
                user.sHoTen = sHoten;
                user.sEmail = sEmail;
                user.sSDT = sSoDienThoai;
                user.sDiaChi = sDiaChi;
                user.bStatus = true;
            }
            else
            {
                return Redirect("/User/Login");
            }
            var result = dao.Update(user);
            //var checkuser = db.tbl_TaiKhoan.SingleOrDefault(x => x.sTenTaiKhoan == formCollection["sTenTaiKhoan"]);

            if (result)
            {
                session.HoTen = sHoten;
                session.Email = sEmail;
                session.SDT = sSoDienThoai;
                session.DiaChi = sDiaChi;
                SetAlert("Cập nhật thành công", "success");
                
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật không thành công");
            }
            return RedirectToAction("HoSoIndex");
        }

        public ActionResult ChangePassword()
        {
            if (Session[Common.CommonConstants.USER_SESSION] == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string sOldPassword, string sNewPassword)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                var dao = new UserDao();
                var result = dao.ChangePassword(session.UserName, Encryptor.MD5Hash(sOldPassword), Encryptor.MD5Hash(sNewPassword));
                if (result == 1)
                {
                    SetAlert("Đổi mật khẩu thành công", "success");
                }

                else
                {
                    SetAlert("Đổi mật khẩu thất bại", "error");
                }
            }
            return View();
        }
        public ActionResult MyContent(int page = 1, int pagesize = 10)
        {
            if (Session[Common.CommonConstants.USER_SESSION] == null)
            {
                return Redirect("/User/Login");
            }
            else
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var model = new BaiVietDAO().ListAllByUser(session.UserName, page, pagesize);
                int totalRecord = 0;

                ViewBag.Total = totalRecord;
                ViewBag.Page = page;
                //ViewBag.Tagid = new BaiVietDAO().getTag(tagid);
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

        public void setViewBag(long? selectedID = null)
        {
            var dao = new DanhMucBaiVietDAO();
            ViewBag.IDChuDe = new SelectList(dao.ListChude(), "IDChuDe", "sTenChuDe", selectedID);
        }

        [HttpGet]
        public ActionResult CreateContent()
        {
            setViewBag();
            return View();
        }

        public ActionResult EditContent(long id)
        {
            var dao = new BaiVietDAO();
            var baiviet = dao.GetBaiVietByID(id);

            setViewBag(baiviet.IDChuDe);
            return View(baiviet);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditContent(tbl_BaiViet model,FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {
                //var dao = new BaiVietDAO();
                long idbaiviet = long.Parse(formcollection["hdnIDbaiviet"]);

                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiSua = session.UserName;
                //var result = dao.Edit(model);
                model.IDBaiViet = idbaiviet;

                new BaiVietDAO().Edit(model);
                SetAlert("Sửa bài viết thành công", "success");

                return RedirectToAction("MyContent");
            }

            setViewBag(model.IDChuDe);

            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreateContent(tbl_BaiViet model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.sNguoiTao = session.UserName;
                new BaiVietDAO().Create(model);
                SetAlert("Thêm bài viết thành công, chờ kiểm duyệt", "success");
                return RedirectToAction("MyContent");
            }
            setViewBag();
            return View();
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        public ActionResult DeleteContent(int id)
        {
            new BaiVietDAO().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult HoaDonIndex()
        {
            var sessionUser = (UserLogin)Session[Common.CommonConstants.USER_SESSION];

            if (sessionUser == null)
            {
                return Redirect("/User/Login");
            }
            var dao = new HoaDonDAO();
            var model = dao.getHoaDOnByIDuser(sessionUser.UserID);
            return View(model);
        }
        public JsonResult CancelOrder(long id)
        {
            var result = new HoaDonDAO().CancelOrder(id);
            return Json(new
            {
                status = result
            });
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
    }
}
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
    public class UserController : BaseController
    {
        MyHandsDbContext db = null;
        // GET: Admin/User
        [CheckPermission(RoleID ="VIEW_USER")]
        public ActionResult Index(string SearchUser, int page = 1,int pagesize = 4)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(SearchUser,page, pagesize);
            ViewBag.SearchUserString = SearchUser;
            return View(model);
        }
        [HttpGet]
        [CheckPermission(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_TaiKhoan user)
        {
            if (ModelState.IsValid)
            {

                
                var dao = new UserDao();
                //var passwordMD5 = Encryptor.MD5Hash(user.sMatKhau);
                //user.sMatKhau = passwordMD5;
                if (dao.checkExistUsername(user.sTenTaiKhoan))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.checkExistUserEmail(user.sEmail))
                {
                    ModelState.AddModelError("", "Email đã được sử dụng cho tài khoản khác");
                }
                else if (dao.checkExistUserPhone(user.sSDT))
                {
                    ModelState.AddModelError("", "Số điện thoại đã được sử dụng cho tài khoản khác");

                }

                else
                {
                    var useradd = new tbl_TaiKhoan();
                    useradd.sTenTaiKhoan = user.sTenTaiKhoan;
                    useradd.sMatKhau = Encryptor.MD5Hash(user.sMatKhau);
                    useradd.sEmail = user.sEmail;
                    useradd.sDiaChi = user.sDiaChi;
                    useradd.sSDT = user.sSDT;
                    useradd.dNgayTao = DateTime.Now;
                    useradd.bStatus = true;
                    var result = dao.Insert(useradd);
                    if (result > 0)
                    {
                        SetAlert("Thêm tài khoản thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tài khoản không thành công");
                    }
                    //long id = dao.Insert(user);
                    //if (id > 0)
                    //{
                    //    SetAlert("Thêm tài khoản thành công", "success");
                    //    return RedirectToAction("Index", "User");
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("", "Thêm tài khoản không thành công");
                    //}
                }
                
                //var checkuser = db.tbl_TaiKhoan.SingleOrDefault(x => x.sTenTaiKhoan == formCollection["sTenTaiKhoan"]);
               
                
            }
            return View(user);
        }
        [CheckPermission(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().viewDetailByID(id);
            return View(user);
        }
        [HttpPost]
        [CheckPermission(RoleID = "EDIT_USER")]

        public ActionResult Edit(tbl_TaiKhoan user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.sMatKhau))
                {
                    var passwordMD5 = Encryptor.MD5Hash(user.sMatKhau);
                    user.sMatKhau = passwordMD5;
                }
                
                var result = dao.Update(user);
                //var checkuser = db.tbl_TaiKhoan.SingleOrDefault(x => x.sTenTaiKhoan == formCollection["sTenTaiKhoan"]);

                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        [CheckPermission(RoleID = "DELETE_USER")]

        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [CheckPermission(RoleID = "EDIT_USER")]

        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatusUser(id);
            return Json(new
            {
                status = result
            }); 
        }

    }
}
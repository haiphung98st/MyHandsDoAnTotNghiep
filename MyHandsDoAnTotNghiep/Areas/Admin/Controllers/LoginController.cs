
using Model.DAO;
using MyHandsDoAnTotNghiep.Areas.Admin.Models;
using MyHandsDoAnTotNghiep.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(loginModel.username, Encryptor.MD5Hash(loginModel.password),true);
                if (result == 1)
                {
                    var user = dao.getByID(loginModel.username);
                    var userSession = new UserLogin();
                    userSession.UserName = user.sTenTaiKhoan;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.sQuyen;
                    var listPerrmission = dao.GetListCredential(loginModel.username);
                    Session.Add(CommonConstants.SESSION_PERMISSION, listPerrmission);

                    Session.Add(CommonConstants.USER_SESSION,userSession );
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");

                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");

                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");

                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Bạn không có quyền truy cập chức năng này");

                }
                else
                {
                    ModelState.AddModelError("","Đăng nhập thất bại");
                }
            }
            return View("Index");
            
        }
    }
}
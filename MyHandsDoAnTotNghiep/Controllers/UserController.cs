using BotDetect.Web.Mvc;
using Model.DAO;
using Model.EF;
using MyHandsDoAnTotNghiep.Common;
using MyHandsDoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;
using System.Web.Mvc;
using System.Configuration;
using System.Xml.Linq;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        [HttpGet]
        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            Session[CommonConstants.CartSession] = null;
            return Redirect("/");
        }
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["fbAppID"],
                client_secret = ConfigurationManager.AppSettings["fbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);

        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new tbl_TaiKhoan();
                user.sEmail = email;
                user.sTenTaiKhoan = email;
                user.bStatus = true;
                user.sHoTen = firstname + " " + middlename + " " + lastname;
                user.dNgayTao = DateTime.Now;
                var resultInsert = new UserDao().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.sHoTen;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(loginModel.sTenTaiKhoanLogin, Encryptor.MD5Hash(loginModel.sMatKhauLogin));
                if (result == 1)
                {
                    //var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    var user = dao.getByID(loginModel.sTenTaiKhoanLogin);
                    var userSession = new UserLogin();
                    userSession.UserName = user.sTenTaiKhoan;
                    userSession.UserID = user.ID;
                    userSession.HoTen = user.sHoTen;
                    userSession.Email = user.sEmail;
                    userSession.DiaChi = user.sDiaChi;
                    userSession.SDT = user.sSDT;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    ViewBag.name = userSession.UserName;
                    return Redirect("/");
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
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }

            return View(loginModel);
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCapcha", "Mã xác nhận không chính xác!")]
        
        
        
        public ActionResult Register(RegisterModel model, FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {
                var userdao = new UserDao();
                if (userdao.checkExistUsername(model.sTenDangNhap))
                {
                    ModelState.AddModelError("","Tên đăng nhập đã tồn tại");
                }
                else if (userdao.checkExistUserEmail(model.sEmail))
                {
                    ModelState.AddModelError("", "Email đã được sử dụng cho tài khoản khác");
                }
                else if (userdao.checkExistUserPhone(model.sSDT))
                {
                    ModelState.AddModelError("", "Số điện thoại đã được sử dụng cho tài khoản khác");

                }
                else
                {
                    var TenTinhThanh = formcollection["hdnTenTinhThanh"];
                    var TenQuanHuyen = formcollection["hdnTenQuanHuyen"];


                    var user = new tbl_TaiKhoan();
                    user.sHoTen = model.sTenNguoiDung;
                    user.sTenTaiKhoan = model.sTenDangNhap;
                    user.sMatKhau = Encryptor.MD5Hash(model.sMatKhau);
                    user.sEmail = model.sEmail;
                    user.sDiaChi = model.sDiaChi + ","+  TenQuanHuyen + "," + TenTinhThanh;
                    user.sSDT = model.sSDT;
                    user.dNgayTao = DateTime.Now;
                    if (!string.IsNullOrEmpty(model.iTinhThanhID))
                    {
                        user.iTinhThanhID = int.Parse(model.iTinhThanhID);
                    }
                    if (!string.IsNullOrEmpty(model.iQuanHuyenID))
                    {
                        user.iQuanHuyenID = int.Parse(model.iQuanHuyenID);
                    }
                    user.bStatus = true;
                    var result = userdao.Insert(user);
                    if(result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        
                        model = new RegisterModel();
                        //Session.Add(CommonConstants.USER_SESSION, user);
                        //return Redirect("/");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký thất bại");
                    }
                }
            } 
            return View(model);
        }

        public JsonResult LoadTinhThanh()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/Client/Data/Provinces_Data.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<TinhThanhModel>();
            TinhThanhModel tinhThanh = null;
            foreach (var item in xElements)
            {
                tinhThanh = new TinhThanhModel();
                tinhThanh.ID = int.Parse(item.Attribute("id").Value);
                tinhThanh.Name = item.Attribute("value").Value;
                list.Add(tinhThanh);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult LoadQuanHuyen(int tinhThanhID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/Client/Data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == tinhThanhID);

            var list = new List<QuanHuyenModel>();
            QuanHuyenModel quanhuyen = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                quanhuyen = new QuanHuyenModel();
                quanhuyen.ID = int.Parse(item.Attribute("id").Value);
                quanhuyen.Name = item.Attribute("value").Value;
                quanhuyen.TinhThanhID = int.Parse(xElement.Attribute("id").Value);
                list.Add(quanhuyen);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }

    }
}
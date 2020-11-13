using Common;
using Model.DAO;
using MyHandsDoAnTotNghiep.Common;
using MyHandsDoAnTotNghiep.HUB;
using MyHandsDoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var sanphamdao = new SanPhamDAO();
            var danhmucspdao = new DanhMucSPDAO();
            var baivietdao = new BaiVietDAO();
            ViewBag.newarrivals = sanphamdao.ListProductNewArrivals(10);
            ViewBag.bestseller = sanphamdao.ListProductBestSeller(10);
            ViewBag.productcategories = danhmucspdao.ListAll();
            ViewBag.baiviethome = baivietdao.listHomeContent(3);
            return View();
        }
        [ChildActionOnly]
        public ActionResult NavBarMenu()
        {
            var model = new MenuDAO().ListByGroupMenuId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDAO().GetFooterDB();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult CartMain()
        {

            var cartsession = Session[MyHandsDoAnTotNghiep.Common.CommonConstants.CartSession];
            var cartlist = new List<GioHangItems>();

            if (cartsession != null)
            {
                cartlist = (List<GioHangItems>)cartsession;
            }
            return PartialView(cartlist);
        }
        public JsonResult GetNotificationContacts()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponents NC = new NotificationComponents();
            var list = NC.GetContacts(notificationRegisterTime);
            //update session here for get only new added contacts (notification)
            Session["LastUpdate"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Subcribe(string email)
        {
            try
            {
                string mailcontent = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Templates/SubcribeMail.html"));

            mailcontent = mailcontent.Replace("{{sEmail}}", email);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new Mail().SendMail(email, "Thông báo mới từ MyHands", mailcontent);
            new Mail().SendMail(toEmail, "Lượt đăng ký mới", mailcontent);
            
                return Json(new
                {
                    status = true

                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false

                });
            }


        }

    }
}
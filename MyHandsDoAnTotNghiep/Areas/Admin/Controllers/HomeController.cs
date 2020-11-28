using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using MyHandsDoAnTotNghiep.Areas.Admin.Models;
using Newtonsoft.Json;


namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        [CheckPermission(RoleID ="ADMIN_HOME")]
        public ActionResult Index()
        {
            //    List<PieChartModel> dataPoints = new List<PieChartModel>();
            //    dataPoints.Add(new PieChartModel("Samsung", 25));
            //    dataPoints.Add(new PieChartModel("Micromax", 13));
            //    dataPoints.Add(new PieChartModel("Lenovo", 8));
            //    dataPoints.Add(new PieChartModel("Intex", 7));
            //    dataPoints.Add(new PieChartModel("Reliance", 6.8));
            //    dataPoints.Add(new PieChartModel("Others", 40.2));

            //    ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            var sanphamdao = new SanPhamDAO();
            var baivietdao = new BaiVietDAO();
            var hoadondao = new HoaDonDAO();
            var chitiethoadondao = new ChiTietHoaDonDAO();
            ViewBag.CountProduct = sanphamdao.CountProduct();
            ViewBag.CountContent = baivietdao.CountContent();
            ViewBag.CountPendingOrders = hoadondao.CountPendingOrders();
            ViewBag.SumOrder = chitiethoadondao.SumOrder();
            return View();
        }
    }
}
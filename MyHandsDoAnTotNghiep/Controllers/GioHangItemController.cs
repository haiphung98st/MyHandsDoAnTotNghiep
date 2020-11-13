using Common;
using Model.DAO;
using Model.EF;
using MyHandsDoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class GioHangItemController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: GioHangItem
        public ActionResult Index()
        {
            var cartsession = Session[CartSession];
            var cartlist = new List<GioHangItems>();

            if (cartsession != null)
            {
                cartlist = (List<GioHangItems>)cartsession;
            }
            return View(cartlist);
        }
        public JsonResult Update(String cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<GioHangItems>>(cartModel);
            var sessionCart = (List<GioHangItems>)Session[CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.SanPham.ID == item.SanPham.ID);
                if (jsonItem != null)
                {
                    item.SoLuong = jsonItem.SoLuong;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        //public JsonResult DeleteAll()
        //{
        //    Session[CartSession] = null;
        //    return Json(new
        //    {
        //        status = true
        //    });
        //}
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<GioHangItems>)Session[CartSession];
            sessionCart.RemoveAll(x => x.SanPham.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<GioHangItems>();
            if (cart != null)
            {
                list = (List<GioHangItems>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string sTenNguoiNhan, string sEmail, string sSoDienThoai, string sDiaChi,FormCollection formcollection)
        {
            var order = new tbl_HoaDon();
            var userSession = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            var TenTinhThanh = formcollection["hdnTenTinhThanh"];
            var TenQuanHuyen = formcollection["hdnTenQuanHuyen"];
            string diachi =   TenQuanHuyen + ", " + TenTinhThanh;

            if (userSession != null)
            {

                order.dNgayTao = DateTime.Now;
                order.IDKhachHang = userSession.UserID;
                order.sTenNguoiNhan = userSession.HoTen;
                order.sDiaChi = userSession.DiaChi + ", "+diachi;
                order.sEmailNguoiNhan = userSession.Email;
                order.iMaTrangThai = 1;
            }
            else
            {
                order.dNgayTao = DateTime.Now;
                order.sTenNguoiNhan = sTenNguoiNhan;
                order.sEmailNguoiNhan = sEmail;
                order.sSDTnguoiNhan = sSoDienThoai;
                order.sDiaChi = sDiaChi+", "+ TenQuanHuyen + ", " + TenTinhThanh;
                order.iMaTrangThai = 1;
            }
            
            
            try
            {
                
                var id = new HoaDonDAO().Insert(order);
                var cart = (List<GioHangItems>)Session[CartSession];
                var detailDao = new Model.DAO.ChiTietHoaDonDAO();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var divsp = new SanPhamDAO();
                    var orderDetail = new tbl_ChiTietHoaDon();
                    orderDetail.IDSanPham = item.SanPham.ID;
                    orderDetail.IDHoaDon = id;

                    orderDetail.iSoLuong = item.SoLuong;
                    if (item.SanPham.dGiaKhuyenMai != null)
                    {
                        orderDetail.dDonGia = item.SanPham.dGiaKhuyenMai;
                    }
                    else
                    {
                        orderDetail.dDonGia = item.SanPham.dGiaBan;
                    }
                    detailDao.Insert(orderDetail);
                    if (item.SanPham.dGiaKhuyenMai != null)
                    {
                        total += (item.SanPham.dGiaKhuyenMai.GetValueOrDefault(0) * item.SoLuong);
                    }
                    else
                    {
                        total += (item.SanPham.dGiaBan.GetValueOrDefault(0) * item.SoLuong);
                    }
                   divsp.divSanPham(item.SanPham.ID,item.SoLuong);
                    //ViewBag.Total = total;

                }                
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Templates/MailForm.html"));

                content = content.Replace("{{sTenNguoiNhan}}", sTenNguoiNhan);
                content = content.Replace("{{sSoDienThoai}}", sSoDienThoai);
                content = content.Replace("{{sEmail}}", sEmail);
                content = content.Replace("{{sDiaChi}}", sDiaChi+diachi);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new Mail().SendMail(sEmail, "Đơn hàng mới từ MyHandsStore", content);
                new Mail().SendMail(toEmail, "Đơn hàng mới từ MyHandsStore", content);
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            Session[CartSession] = null;
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult AddToCart(int IDSanPham, int SoLuong)
        {
            var product = new SanPhamDAO().chiTietSanPham(IDSanPham);
            var cartsession = Session[CartSession];
            if(cartsession != null)
            {
                var cartlist = (List < GioHangItems > )cartsession;
                if(cartlist.Exists(x=>x.SanPham.ID == IDSanPham))
                {
                    foreach (var item in cartlist)
                    {
                        if(item.SanPham.ID == IDSanPham)
                        {
                            item.SoLuong += SoLuong;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var cartItem = new GioHangItems();
                    cartItem.SanPham = product;
                    cartItem.SoLuong = SoLuong;
                    cartlist.Add(cartItem);
                }
                // gán vào sesion
                Session[CartSession] = cartlist;
            }
            else
            {
                //tạo mới đối tượng cart item
                var cartItem = new GioHangItems();
                cartItem.SanPham = product;
                cartItem.SoLuong = SoLuong;
                var cartlist = new List<GioHangItems>();
                cartlist.Add(cartItem);
                // gán vào sesion
                Session[CartSession] = cartlist;
            }
            return RedirectToAction("Index");
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
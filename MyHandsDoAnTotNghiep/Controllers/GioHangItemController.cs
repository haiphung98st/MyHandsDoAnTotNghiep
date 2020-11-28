using Common;
using Model.DAO;
using Model.EF;
using MyHandsDoAnTotNghiep.Models;
using MyHandsDoAnTotNghiep.NganLuong;
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
        private string MerchantID = ConfigurationManager.AppSettings["MerchantID"].ToString();
        private string MerchantPassword = ConfigurationManager.AppSettings["MerchantPassword"].ToString();
        private string MerchantEmail = ConfigurationManager.AppSettings["MerchantEmail"].ToString();
        private string currentLink = ConfigurationManager.AppSettings["currentLink"].ToString();
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
        public JsonResult Update(String cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<GioHangItems>>(cartModel);
            var sessionCart = (List<GioHangItems>)Session[CartSession];
            foreach(var item in sessionCart)
            {
               // var iQuantity = new SanPhamDAO().getQuantityByID(item.SanPham.ID);  
                var jsonItem = jsonCart.SingleOrDefault(x => x.SanPham.ID == item.SanPham.ID);
                if (jsonItem != null && jsonItem.SoLuong<= item.SanPham.iSoLuong)
                {
                    item.SoLuong = jsonItem.SoLuong;
                }
                else
                {
                    string outOfquantity = "Sản phẩm " + item.SanPham.sTenSanPham + " chỉ còn " + item.SanPham.iSoLuong ;
                    //TempData["msg"] = "<script>alert('Sản phẩm "+item.SanPham.sTenSanPham +" chỉ còn "+iQuantity+"');</script>";
                    SetAlert(outOfquantity, "error");
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
        [ValidateAntiForgeryToken]
        public ActionResult Payment(string sTenNguoiNhan, string sEmail, string sSoDienThoai, string sDiaChi,string FormOfPayment,string sBankCode, FormCollection formcollection)
        {
            var order = new tbl_HoaDon();
            var userSession = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            var TenTinhThanh = formcollection["hdnTenTinhThanh"];
            var TenQuanHuyen = formcollection["hdnTenQuanHuyen"];
            string diachi =   TenQuanHuyen + ", " + TenTinhThanh;

            if (userSession != null)
            {

                order.IDKhachHang = userSession.UserID;
            }
            order.dNgayTao = DateTime.Now;
            order.sTenNguoiNhan = sTenNguoiNhan;
            order.sEmailNguoiNhan = sEmail;
            order.sSDTnguoiNhan = sSoDienThoai;
            order.sDiaChi = sDiaChi+", "+ TenQuanHuyen + ", " + TenTinhThanh;
            order.sFormOfPayment = FormOfPayment;
            order.iMaTrangThai = 1;
            
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
                    orderDetail.sGhiChu = item.sGhiChu;
                    orderDetail.iSoLuong = item.SoLuong;
                    if (item.SanPham.dGiaKhuyenMai != null && item.SanPham.dGiaKhuyenMai != 0)
                    {
                        orderDetail.dDonGia = item.SanPham.dGiaKhuyenMai;
                    }
                    else
                    {
                        orderDetail.dDonGia = item.SanPham.dGiaBan;
                    }
                    detailDao.Insert(orderDetail);
                    if (item.SanPham.dGiaKhuyenMai != null && item.SanPham.dGiaKhuyenMai != 0)
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
                if (!FormOfPayment.Equals("COD") )
                {
                    RequestInfo info = new RequestInfo();
                    info.Merchant_id = MerchantID;
                    info.Merchant_password = MerchantPassword;
                    info.Receiver_email = MerchantEmail;

                    info.cur_code = "vnd";
                    info.bank_code = sBankCode;

                    info.Order_code = id.ToString();
                    info.Total_amount = total.ToString();
                    info.fee_shipping = "0";
                    info.Discount_amount = "0";
                    info.order_description = "Thanh toán đơn hàng Myhands Store";
                    info.return_url = currentLink + "/hoan-thanh";
                    info.cancel_url = currentLink + "/loi-thanh-toan";

                    info.Buyer_fullname = sTenNguoiNhan;
                    info.Buyer_email = sEmail;
                    info.Buyer_mobile = sSoDienThoai;

                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    ResponseInfo result = objNLChecout.GetUrlCheckout(info, FormOfPayment);

                    if (result.Error_code == "00")
                    {
                        Response.Redirect(result.Checkout_url);
                        //return Redirect("/hoan-thanh");
                    }
                    else
                    {
                        return Redirect("/loi-thanh-toan");
                    }

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
                string script = "<script>alert('" + ex.Message + "');</script>";
                
                return Redirect("/loi-thanh-toan");
            }
            Session[CartSession] = null;
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            var order = new tbl_HoaDon();
            String Token = Request["token"];
            if(Token!= null)
            {
                long orderID = long.Parse(Request["Order_code"]);
                RequestCheckOrder info = new RequestCheckOrder();
                info.Merchant_id = MerchantID;
                info.Merchant_password = MerchantPassword;
                info.Token = Token;
                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
                ViewBag.Message = result.errorCode + result.payerName;
                order.ID = orderID;
                order.iStatus = 1;
                order.iMaTrangThai = 1;
                new HoaDonDAO().Update(order);

            }

            return View();
        }
        public ActionResult PaymentFail()
        {
            //long orderID = long.Parse(Request["Order_code"]);

            //new HoaDonDAO().CancelOrder(orderID);
            return View();
        }
        public ActionResult AddToCart(int IDSanPham, int SoLuong,string sGhiChu)
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
                    cartItem.sGhiChu = sGhiChu;
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
                cartItem.sGhiChu = sGhiChu;

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
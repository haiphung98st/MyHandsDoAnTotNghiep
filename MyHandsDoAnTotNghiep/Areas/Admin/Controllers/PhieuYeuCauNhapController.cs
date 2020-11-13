using Model.DAO;
using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MyHandsDoAnTotNghiep.Areas.Admin.Controllers
{
    public class PhieuYeuCauNhapController : BaseController
    {
        private const string SESSION_PRODUCT_YC = "SESSION_PRODUCT_YC";
        // GET: Admin/PhieuYeuCauNhap
        public ActionResult Index( int page = 1, int pagesize = 5)
        {
            int totalRecord = 0;
            var dao = new PhieuYeuCauDAO();
            var model = dao.ListAllPhieuYC(ref totalRecord, page, pagesize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

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

        [HttpGet]
        public ActionResult Create()
        {
            var productsession = Session[SESSION_PRODUCT_YC];
            var productlist = new List<PhieuYeuCauViewModel>();
            if (productsession != null)
            {
                productlist = (List<PhieuYeuCauViewModel>)productsession;
            }
            setViewBagNCC();
            setViewBagSanPham();
            return View(productlist);
        }
        [HttpPost]
        public ActionResult Create(FormCollection formcollection)
        {
            var phieuyeucau = new tbl_PhieuYeuCauNhap();
            //var userSession = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            //var IDNCC = formcollection["hdnNCC"];
            long IDNCC = long.Parse(formcollection["hdnNCC"]);

            phieuyeucau.IDncc = IDNCC;
            try
            {
                var id = new PhieuYeuCauDAO().Insert(phieuyeucau);
                var phieuyeucaulist = (List<PhieuYeuCauViewModel>)Session[SESSION_PRODUCT_YC];
                var detailDao = new Model.DAO.ChiTietPhieuYCDAO();
                foreach (var item in phieuyeucaulist)
                {
                    var phieuyeucaudetail = new tbl_ChiTietPhieuYC();
                    phieuyeucaudetail.IDSanPham = item.sanPham.ID;
                    phieuyeucaudetail.IDPhieuYeuCau = id;
                    phieuyeucaudetail.iSoLuongYC = item.iSoLuongYC;
                    detailDao.Insert(phieuyeucaudetail);
                }
            }
            catch (Exception ex)
            {
                return Redirect("/Admin/PhieuYeuCauNhap/Create");
            }
            Session[SESSION_PRODUCT_YC] = null;
            return Redirect("/Admin/PhieuYeuCauNhap/Index");
        }
        public ActionResult AddSP(int IDSanPham,int soluong)
        {
            var product = new SanPhamDAO().chiTietSanPham(IDSanPham);
            var productsession = Session[SESSION_PRODUCT_YC];
            if (productsession != null)
            {
                var productlist = (List<PhieuYeuCauViewModel>)productsession;
                if (productlist.Exists(x => x.sanPham.ID == IDSanPham))
                {
                    foreach (var item in productlist)
                    {
                        if (item.sanPham.ID == IDSanPham)
                        {
                            item.iSoLuongYC += soluong;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var productItem = new PhieuYeuCauViewModel();
                    productItem.sanPham = product;
                    productItem.iSoLuongYC = soluong;
                    productlist.Add(productItem);
                }
                // gán vào sesion
                Session[SESSION_PRODUCT_YC] = productlist;
            }
            else
            {
                //tạo mới đối tượng cart item
                var productItem = new PhieuYeuCauViewModel();
                productItem.sanPham = product;
                productItem.iSoLuongYC = soluong;
                var productlist = new List<PhieuYeuCauViewModel>();
                productlist.Add(productItem);
                // gán vào sesion
                Session[SESSION_PRODUCT_YC] = productlist;
            }
            //setViewBagNCC();
            return Redirect("/Admin/PhieuYeuCauNhap/Create");
        }
        //public ActionResult Edit(long id)
        //{
        //    var dao = new SanPhamDAO();
        //    var sanpham = dao.GetSanPhamByID(id);

        //    setViewBag(sanpham.IDDanhMuc);
        //    return View(sanpham);
        //}

        //[HttpPost]
        //public ActionResult Edit(tbl_SanPham model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var dao = new SanPhamDAO();

        //        var session = (UserLogin)Session[CommonConstants.USER_SESSION];
        //        model.sNguoiSua = session.UserName;
        //        //var result = dao.Edit(model);
        //        new SanPhamDAO().Update(model);

        //        return RedirectToAction("Index");
        //    }

        //    setViewBag(model.IDDanhMuc);

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(tbl_SanPham model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var session = (UserLogin)Session[CommonConstants.USER_SESSION];
        //        model.sNguoiTao = session.UserName;
        //        new SanPhamDAO().Create(model);
        //        return RedirectToAction("Index");
        //    }
        //    setViewBag();
        //    return View();
        //}
        public JsonResult Delete(long id)
        {
            var productsession = (List<PhieuYeuCauViewModel>)Session[SESSION_PRODUCT_YC];
            productsession.RemoveAll(x => x.sanPham.ID == id);
            Session[SESSION_PRODUCT_YC] = productsession;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(String productModel)
        {
            var jsonProduct = new JavaScriptSerializer().Deserialize<List<PhieuYeuCauViewModel>>(productModel);
            var sessionProduct = (List<PhieuYeuCauViewModel>)Session[SESSION_PRODUCT_YC];
            foreach (var item in sessionProduct)
            {
                var jsonItem = jsonProduct.SingleOrDefault(x => x.sanPham.ID == item.sanPham.ID);
                if (jsonItem != null)
                {
                    item.iSoLuongYC = jsonItem.iSoLuongYC;
                }
            }
            Session[SESSION_PRODUCT_YC] = sessionProduct;
            return Json(new
            {
                status = true
            });
        }
        public void setViewBagNCC(long? selectedID = null)
        {
            var dao = new NhaCungCapDAO();
            //ViewBag.IDncc = new SelectList(dao.ListAll(), "IDncc", "sTenNCC", selectedID);
            ViewBag.IDncc = dao.ListAll();
        }
        public void setViewBagSanPham(long? selectedID = null)
        {
            var dao = new SanPhamDAO();
            //ViewBag.IDSanPham = new SelectList(dao.ListAll(), "ID", "sTenSanPham", selectedID);
            ViewBag.IDSanPham = dao.ListAll();
        }
    }
}
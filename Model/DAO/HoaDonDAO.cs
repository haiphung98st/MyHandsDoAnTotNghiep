using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class HoaDonDAO
    {
        MyHandsDbContext db = null;
        public HoaDonDAO()
        {
            db = new MyHandsDbContext();
        }
        public long Insert (tbl_HoaDon order)
        {
            db.tbl_HoaDon.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        public IEnumerable<tbl_HoaDon> ListAllPaging(string SearchHoaDon, int page, int pageSize)
        {
            IQueryable<tbl_HoaDon> model = db.tbl_HoaDon;
            if (!string.IsNullOrEmpty(SearchHoaDon))
            {
                model = model.Where(x => x.sTenNguoiNhan.Contains(SearchHoaDon));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }


        public IEnumerable<tbl_HoaDon> ListAllCancelOrderPaging(string SearchHoaDon, int page, int pageSize)
        {
            IQueryable<tbl_HoaDon> model = db.tbl_HoaDon;
            if (!string.IsNullOrEmpty(SearchHoaDon))
            {
                model = model.Where(x => x.sTenNguoiNhan.Contains(SearchHoaDon) && x.iMaTrangThai == 4);
            }
            return model.Where(x=>x.iMaTrangThai == 4).OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public IEnumerable<tbl_HoaDon> ListAllPaging(int page, int pageSize)
        {
            IQueryable<tbl_HoaDon> model = db.tbl_HoaDon;
            return model.OrderByDescending(x => x.dNgayTao).ToPagedList(page, pageSize);
        }

        public tbl_HoaDon viewByHoaDoniD(int id)
        {
            return db.tbl_HoaDon.Find(id);
        }

        public List<HoaDonViewModel> ListDetailByOrder(ref int totalRecord, int page = 1, int pageSize = 10)
        {
            totalRecord = db.tbl_HoaDon.Count();
            //var model = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //lay tong cac san pham trong danh muc. Skip-lay tu ban ghi 
            //return model;

            var model = (from a in db.tbl_HoaDon
                         join b in db.tbl_ChiTietHoaDon on a.ID equals b.IDHoaDon
                         join c in db.tbl_SanPham on b.IDSanPham equals c.ID
                            orderby a.ID
                         group new {b.dDonGia,c.dGiaNhap} by new { a.ID } into g
                         
                        
                         select new
                         {
                             ID = g.Key.ID,
                             //dNgayTao = g.Select(e=>e.dNgayTao),
                             dGiaNhap = g.Sum(l=>l.dGiaNhap),
                             iSoluong = g.Count(),
                             dDongia = g.Sum(w => w.dDonGia),
                             //dGiaNhap = c.dGiaNhap
                         })
                         .AsEnumerable().Select(x => new HoaDonViewModel()
                         {
                            ID = x.ID,
                             //dNgayTao.Cast<DateTime>() = x.dNgayTao,
                             iSoLuong = x.iSoluong,
                             dDongia = x.dDongia,
                            dGiaNhap = x.dGiaNhap
                         });
            model.OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        public List<HoaDonViewModel> ListDetailByOrderID(int orderid, ref int totalRecord, int page = 1, int pageSize = 1)
        {
            totalRecord = db.tbl_ChiTietHoaDon.Where(x => x.IDHoaDon == orderid).Count();
            //var model = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //lay tong cac san pham trong danh muc. Skip-lay tu ban ghi 
            //return model;

            var model = (from a in db.tbl_ChiTietHoaDon
                         join b in db.tbl_HoaDon on a.IDHoaDon equals b.ID
                         join c in db.tbl_TrangThaiHoaDon on b.iMaTrangThai equals c.ID
                         join d in db.tbl_SanPham on a.IDSanPham equals d.ID
                         where a.IDHoaDon == orderid
                         select new
                         {
                             ID = a.IDHoaDon,
                             sTenNguoiNhan = b.sTenNguoiNhan,
                             sSDT = b.sSDTnguoiNhan,
                             sDiaChi = b.sDiaChi,
                             sEmail = b.sEmailNguoiNhan,
                             dngaytao = b.dNgayTao,
                             iSoluong = a.iSoLuong,
                             dDongia = a.dDonGia,
                             sTenSanPham = d.sTenSanPham,
                             sTentrangthai = c.sTenTrangThai,
                             dGiaNhap = d.dGiaNhap
                         })
                         .AsEnumerable().Select(x => new HoaDonViewModel()
                         {
                             ID = x.ID,
                             sTenNguoiNhan = x.sTenNguoiNhan,
                             sEmail = x.sEmail,
                             sDiaChi = x.sDiaChi,
                             sSoDienThoai = x.sSDT,
                             dNgayTao = x.dngaytao,
                             iSoLuong = x.iSoluong,
                             dDongia = x.dDongia,
                             sTenTrangThai = x.sTentrangthai,
                             sTenSanPham = x.sTenSanPham,
                             dGiaNhap = x.dGiaNhap
                         }) ;
            model.OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();

        }
        public bool Update(tbl_HoaDon entity)
        {
            try
            {
                var hoadon = db.tbl_HoaDon.Find(entity.ID);

                hoadon.dNgaySua = DateTime.Now;
                hoadon.iMaTrangThai = entity.iMaTrangThai;
                
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public tbl_HoaDon GetHoaDonByID(long id)
        {
            return db.tbl_HoaDon.Find(id);
        }
        public IEnumerable<tbl_HoaDon> getHoaDOnByIDuser(long id)
        {
            IQueryable<tbl_HoaDon> model = db.tbl_HoaDon;
            return model = model.Where(x => x.IDKhachHang==id).OrderByDescending(x=>x.dNgayTao);
        }
        public long CancelOrder(long id)
        {
            var cancel = db.tbl_HoaDon.Find(id);
            cancel.iMaTrangThai = 4;
            db.SaveChanges();
            return cancel.iMaTrangThai;
        }
    }
}

using Common;
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
    public class PhieuYeuCauDAO
    {
        MyHandsDbContext db = null;
        public PhieuYeuCauDAO()
        {
            db = new MyHandsDbContext();
        }
        public long Insert(tbl_PhieuYeuCauNhap phieuyeucau)
        {
            db.tbl_PhieuYeuCauNhap.Add(phieuyeucau);
            db.SaveChanges();
            return phieuyeucau.IDPhieuYeuCau;
        }
        public IEnumerable<PhieuYeuCauViewModel> ListAllPhieuYC(ref int totalRecord, int page = 1, int pageSize = 10)
        {
            totalRecord = db.tbl_PhieuYeuCauNhap.Count();
            //var model = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //lay tong cac san pham trong danh muc. Skip-lay tu ban ghi 
            //return model;

            var model = (from a in db.tbl_NCC
                         join b in db.tbl_PhieuYeuCauNhap on a.IDncc equals b.IDncc
                         join c in db.tbl_ChiTietPhieuYC on b.IDPhieuYeuCau equals c.IDPhieuYeuCau
                         join d in db.tbl_SanPham on c.IDSanPham equals d.ID


                         select new
                         {
                             iDphieuyc = b.IDPhieuYeuCau,
                             sTenNhaCungCap = a.sTenNCC,
                             sTenSanPham = d.sTenSanPham,
                             iSoLuong = c.iSoLuongYC
                         })
                         .AsEnumerable().Select(x => new PhieuYeuCauViewModel()
                         {
                             IDPhieuYeuCau = x.iDphieuyc,
                             sTenSanPham = x.sTenSanPham,
                             sTenNCC = x.sTenNhaCungCap,
                             iSoLuongYC = x.iSoLuong,

                         }) ;
            model.OrderByDescending(x => x.sTenSanPham).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}

using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CustomDAO
    {

        MyHandsDbContext db = null;
        public CustomDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_DanhMucMauSanPham> listAllCustomCategories()
        {
            return db.tbl_DanhMucMauSanPham.OrderBy(x => x.sTenLoaiMau).ToList();
        }
        public List<tbl_SanPhamMau> ListProductByCategoryID(int id)
        {
            return db.tbl_SanPhamMau.Where(x=>x.IDDanhMucMau == id).OrderBy(x => x.ID).ToList();
        }
        public tbl_DanhMucMauSanPham viewByDanhMuc(int id)
        {
            return db.tbl_DanhMucMauSanPham.Find(id);
        }
    }
}

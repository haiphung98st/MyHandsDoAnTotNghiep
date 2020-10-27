using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class DanhMucSPDAO
    {
        MyHandsDbContext db = null;
        public DanhMucSPDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_DanhMucSanPham> ListAll()
        {
            return db.tbl_DanhMucSanPham.Where(x => x.bStatus == true).OrderBy(x => x.iDisplayOrder).ToList();
        }
        public tbl_DanhMucSanPham viewByDanhMuc(int id)
        {
            return db.tbl_DanhMucSanPham.Find(id);
        }
        public tbl_DanhMucSanPham viewDetail(long id)
        {
            return db.tbl_DanhMucSanPham.Find(id);
        }
        
    }
}

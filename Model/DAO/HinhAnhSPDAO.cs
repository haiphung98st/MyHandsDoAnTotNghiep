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
    public class HinhAnhSPDAO
    {
        MyHandsDbContext db = null;
        public HinhAnhSPDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_HinhAnhSP> ListByProductID(long idSanPham)
        {
            return db.tbl_HinhAnhSP.Where(x => x.IDSanPham != null && x.IDSanPham == idSanPham).OrderByDescending(x => x.IDHinhAnh).ToList();
        }
    }
}

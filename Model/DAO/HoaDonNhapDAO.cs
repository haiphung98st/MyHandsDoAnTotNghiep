using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class HoaDonNhapDAO
    {
        MyHandsDbContext db = null;
        public HoaDonNhapDAO()
        {
            db = new MyHandsDbContext();
        }
        public IEnumerable<tbl_DonNhapHang> ListAllPaging(string SearchSanPham, int page, int pageSize)
        {
            IQueryable<tbl_DonNhapHang> model = db.tbl_DonNhapHang;
            if (!string.IsNullOrEmpty(SearchSanPham))
            {
                model = model.Where(x => x.IDDonNhapHang.ToString().Contains(SearchSanPham));
            }
            return model.OrderByDescending(x => x.IDDonNhapHang).ToPagedList(page, pageSize);
        }
    }
}
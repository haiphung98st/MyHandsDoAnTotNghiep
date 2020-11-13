using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class TrangThaiHoaDonDAO
    {
        MyHandsDbContext db = null;
        public TrangThaiHoaDonDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_TrangThaiHoaDon> ListAll()
        {
            return db.tbl_TrangThaiHoaDon.OrderBy(x => x.dNgayTao).ToList();
        }
    }
}
